using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;

namespace ErgoCalc;

public enum ModelType
{
    WorkRest,
    CumulativeLifting,
    NioshLifting,
    StrainIndex,
    OcraCheck,
    MetabolicRate,
    ThermalComfort,
    LibertyMutual,
}

partial class FrmMain
{
    private void New_Click(object sender, EventArgs e)
    {
        FrmNew frmNew = new();
        frmNew.Icon = GraphicsResources.Load<Icon>(GraphicsResources.AppLogo);

        if (frmNew.ShowDialog() == DialogResult.Cancel) return;

        Form frmData = frmNew.Model switch
        {
            ModelType.WorkRest => new FrmDataWR(),
            ModelType.CumulativeLifting => new FrmDataCLM(),
            ModelType.NioshLifting => new FrmDataNIOSH(),
            ModelType.StrainIndex => new FrmDataStrainIndex(_settings.AppCulture),
            ModelType.OcraCheck => new FrmDataOCRAcheck(),
            ModelType.MetabolicRate => new FrmDataMet(),
            ModelType.ThermalComfort => new FrmDataTC(),
            ModelType.LibertyMutual => new FrmDataLiberty(),
            _ => new Form()
        };

        if (frmData is IChildData)
        {
            IChildData frm = (IChildData)frmData;
            frm.LoadExample();

            if (frmData.ShowDialog(this) == DialogResult.OK)
            {
                Form frmResults = frmNew.Model switch
                {
                    ModelType.WorkRest => new FrmResultsWR(frm.GetData),
                    ModelType.CumulativeLifting => new FrmResultsCLM(frm.GetData),
                    ModelType.NioshLifting => new FrmResultNIOSH(frm.GetData),
                    ModelType.StrainIndex => new FrmResultsStrainIndex(frm.GetData),
                    ModelType.OcraCheck => new FrmResultsOCRAcheck(frm.GetData),
                    ModelType.MetabolicRate => new FrmResultsMet(frm.GetData),
                    ModelType.ThermalComfort => new FrmResultsTC(frm.GetData),
                    ModelType.LibertyMutual => new FrmResultsLiberty(frm.GetData),
                    _ => new Form()
                };
                frmResults.MdiParent = this;
                frmResults.Icon = GraphicsResources.Load<Icon>(GraphicsResources.AppLogo);
                frmResults.Show();
            }
            frmData.Dispose();
        }
    }

    private void Exit_Click(object sender, EventArgs e)
    {
        // Exit the application by calling the frmMain_FormClosing event
        this.Close();
    }

    private void About_Click(object sender, EventArgs e)
    {
        FrmAbout frmAbout = new();
        frmAbout.ShowDialog();
    }

    private void Open_Click(object sender, EventArgs e)
    {
        OpenFileDialog openDlg = new()
        {
            DefaultExt = "*.ergo",
            Filter = "ERGO file (*.ergo)|*.ergo|All files (*.*)|*.*",
            FilterIndex = 1,
            Title = "Open ErgoCalc file",
            InitialDirectory = _settings.RememberFileDialogPath ? _settings.UserOpenPath : _settings.DefaultOpenPath
        };

        DialogResult result;
        using (new CenterWinDialog(this))
        {
            result = openDlg.ShowDialog(this);
        }

        // If the file name is not an empty string open it for saving.  
        if (result == DialogResult.OK && openDlg.FileName != "")
        {
            // https://stackoverflow.com/questions/897796/how-do-i-open-an-already-opened-file-with-a-net-streamreader
            using var fs = File.Open(openDlg.FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using var sr = new StreamReader(fs, Encoding.UTF8);

            string jsonString = sr.ReadToEnd();

            //string jsonString = File.ReadAllText(openDlg.FileName);
            var options = new JsonDocumentOptions { AllowTrailingCommas = true, CommentHandling = JsonCommentHandling.Skip };

            // Dilemma: this should be wrapped in a try-catch, but variables will be out of scope and syntax would be cumbersome
            using JsonDocument document = JsonDocument.Parse(jsonString, options);
            var strType = document.RootElement.TryGetProperty("Document type", out JsonElement docuValue) ? docuValue.ToString() : "Error";
            //string cultureName = document.RootElement.TryGetProperty("Culture name", out docuValue) ? docuValue.ToString() : string.Empty;

            Form? frm = strType switch
            {
                "Work-Rest model" => new FrmResultsWR(),
                "NIOSH lifting equation" => new FrmResultNIOSH(),
                "Strain index" => new FrmResultsStrainIndex(),
                "Thermal comfort model" => new FrmResultsTC(),
                "LM-MMH model" => new FrmResultsLiberty(),
                "Comprehensive lifting model" => new FrmResultsCLM(),
                _ => default
            };

            if (frm != default)
            {
                frm.MdiParent = this;

                var strTextTitle = strType switch
                {
                    "Work-Rest model" => StringResources.FormResultsWR,
                    "NIOSH lifting equation" => StringResources.FormResultsNIOSH,
                    "Strain index" => StringResources.FormResultsStrainIndex,
                    "Thermal comfort model" => StringResources.FormResultsTC,
                    "LM-MMH model" => StringResources.FormResultsLiberty,
                    "Comprehensive lifting model" => StringResources.FormResultsCLM,
                    _ => String.Empty
                };

                SetFormTitle(frm, strTextTitle, openDlg.FileName);

                if (((IChildResults)frm).OpenFile(document))
                {
                    frm.Icon = GraphicsResources.Load<Icon>(GraphicsResources.AppLogo);
                    frm.Show();
                }
                else
                {
                    MessageBox.Show("The document cannot be opened by this application", "Format mismatch", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        return;
    }

    private void Save_Click(object sender, EventArgs e)
    {
        if (this.ActiveMdiChild != null)
            ((IChildResults)this.ActiveMdiChild).Save(string.Empty);
    }

    private void Copy_Click(object sender, EventArgs e)
    {
        if (this.ActiveMdiChild != null)
            ((IChildResults)this.ActiveMdiChild).Duplicate();
    }

    private void EditData_Click(object sender, EventArgs e)
    {
        if (this.ActiveMdiChild != null)
            ((IChildResults)this.ActiveMdiChild).EditData();
    }

    private void Settings_Click(object sender, EventArgs e)
    {
        FrmSettings frm = new(_settings);
        frm.ShowDialog(this);
        if (frm.DialogResult == DialogResult.OK)
        {
            UpdateUI_Language();
        }
        
        if (this.ActiveMdiChild != null)
            ((IChildResults)this.ActiveMdiChild).ShowHideSettings();
    }

    private void Settings_EnabledChanged(object sender, EventArgs e)
    {
        if (this.toolStripMain_Settings.Enabled == false) this.toolStripMain_Settings.Checked = false;
    }

    private void Language_Click(object sender, EventArgs e)
    {
        FrmLanguage frm = new(_settings);
        //frm.Icon = GraphicsResources.Load<Icon>(GraphicsResources.AppLogo);
        frm.ShowDialog();

        if (frm.DialogResult == DialogResult.OK)
            UpdateUI_Language();

        // This is redundant because this asignment is done at the very beginning of UpdateUI_Language
        StringResources.Culture=_settings.AppCulture;
    }

    private void LabelColor_Click(object sender, EventArgs e)
    {
        if (sender is null) return;

        ColorDialog colorDlg = new();
        colorDlg.AllowFullOpen = true;
        colorDlg.FullOpen = true;
        colorDlg.AnyColor = true;
        colorDlg.Color = Color.FromArgb(_settings.TextBackColor);
        if (colorDlg.ShowDialog(this) == DialogResult.OK)
        {
            _settings.TextBackColor = colorDlg.Color.ToArgb();
            foreach (Form frm in MdiChildren)
            {
                //var rtbText = frm.Controls.Find("rtbShowResult", false).FirstOrDefault() as RichTextBox;
                var rtbText = frm.Controls[0].Controls[1].Controls[0];
                if (rtbText is not null)
                    rtbText.BackColor = colorDlg.Color;
            }
            this.statusStripLabelBackColor.BackColor = colorDlg.Color;
        }
    }

    private void LabelZoom_Click(object sender, EventArgs e)
    {
        if (sender is null) return;

        FrmZoom frmZoom = new(_settings.TextZoom, _settings.AppCulture);
        frmZoom.Icon = GraphicsResources.Load<Icon>(GraphicsResources.AppLogo);
        if (frmZoom.ShowDialog(this) == DialogResult.OK)
        {
            _settings.TextZoom = frmZoom.ZoomLevel;
            foreach (Form frm in MdiChildren)
            {
                //var rtbText = frm.Controls.Find("rtbShowResult", false).FirstOrDefault() as RichTextBox;
                var rtbText = frm.Controls[0].Controls[1].Controls[0] as RichTextBox;
                if (rtbText is not null)
                    rtbText.ZoomFactor = _settings.TextZoom;
            }
            this.statusStripLabelZoom.Text = $"{_settings.TextZoom}x";
        }
    }

    private void LabelWordWrap_Click(object sender, EventArgs e)
    {
        if (sender is not null && sender is ToolStripStatusLabelEx LabelEx)
        {
            var label = LabelEx;
            label.Checked = !label.Checked;

            // Change the text color
            if (label.Checked)
                label.ForeColor = Color.Black;
            else
                label.ForeColor = Color.LightGray;

            // Update the settings
            _settings.WordWrap = label.Checked;
            foreach (Form frm in MdiChildren)
            {
                //var rtbText = frm.Controls.Find("rtbShowResult", false).FirstOrDefault() as RichTextBox;
                var rtbText = frm.Controls[0].Controls[1].Controls[0] as RichTextBox;
                if (rtbText is not null)
                    rtbText.WordWrap = _settings.WordWrap;
            }
        }
    }

}