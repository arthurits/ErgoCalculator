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
            ModelType.WorkRest => new FrmDataWR(_settings.AppCulture),
            ModelType.CumulativeLifting => new FrmDataCLM(_settings.AppCulture),
            ModelType.NioshLifting => new FrmDataNIOSH(_settings.AppCulture),
            ModelType.StrainIndex => new FrmDataStrainIndex(_settings.AppCulture),
            ModelType.OcraCheck => new FrmDataOCRAcheck(),
            ModelType.MetabolicRate => new FrmDataMet(),
            ModelType.ThermalComfort => new FrmDataTC(_settings.AppCulture),
            ModelType.LibertyMutual => new FrmDataLiberty(_settings.AppCulture),
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
                    ModelType.CumulativeLifting => new FrmResultsCLM(frm.GetData, _settings.WordWrap, _settings.TextBackColor, _settings.TextZoom),
                    ModelType.NioshLifting => new FrmResultNIOSH(frm.GetData, _settings.WordWrap, _settings.TextBackColor, _settings.TextZoom),
                    ModelType.StrainIndex => new FrmResultsStrainIndex(frm.GetData, _settings.WordWrap, _settings.TextBackColor, _settings.TextZoom),
                    ModelType.OcraCheck => new FrmResultsOCRAcheck(frm.GetData),
                    ModelType.MetabolicRate => new FrmResultsMet(frm.GetData),
                    ModelType.ThermalComfort => new FrmResultsTC(frm.GetData, _settings.WordWrap, _settings.TextBackColor, _settings.TextZoom),
                    ModelType.LibertyMutual => new FrmResultsLiberty(frm.GetData, _settings.WordWrap, _settings.TextBackColor, _settings.TextZoom),
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
                "NIOSH lifting equation" => new FrmResultNIOSH(default, _settings.WordWrap, _settings.TextBackColor, _settings.TextZoom),
                "Strain index" => new FrmResultsStrainIndex(default, _settings.WordWrap, _settings.TextBackColor, _settings.TextZoom),
                "Thermal comfort model" => new FrmResultsTC(default, _settings.WordWrap, _settings.TextBackColor, _settings.TextZoom),
                "LM-MMH model" => new FrmResultsLiberty(default, _settings.WordWrap, _settings.TextBackColor, _settings.TextZoom),
                "Comprehensive lifting model" => new FrmResultsCLM(default, _settings.WordWrap, _settings.TextBackColor, _settings.TextZoom),
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
        StringResources.Culture = _settings.AppCulture;
    }

    private void LabelFont_Click(object sender, EventArgs e)
    {
        FontDialog fontDlg = new()
        {
            ShowApply = false,
            ShowColor = false,
            ShowEffects = true,
            ShowHelp = false,
            FontMustExist = true
        };

        if (ActiveMdiChild?.ActiveControl is RichTextBox richText)
        {
            fontDlg.Font = new(richText.Font.Name, richText.Font.Size, richText.Font.Style);
            if (fontDlg.ShowDialog() == DialogResult.OK)
            {
                richText.Font = fontDlg.Font;
                this.statusStripLabelFont.Text = String.Format(StringResources.LblFontName, richText.Font.Name, richText.Font.Size);
            }
        }
    }

    private void LabelFontColor_Click(object sender, EventArgs e)
    {
        if (sender is null) return;

        ColorDialog colorDlg = new()
        {
            AllowFullOpen = true,
            FullOpen = true,
            AnyColor = true
        };

        if (ActiveMdiChild?.ActiveControl is RichTextBox richText)
        {
            colorDlg.Color = richText.ForeColor;
            if (colorDlg.ShowDialog(this) == DialogResult.OK)
            {
                richText.ForeColor = colorDlg.Color;
                this.statusStripLabelFontColor.BackColor = colorDlg.Color;
            }
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

            if (ActiveMdiChild?.ActiveControl is RichTextBox richText)
                richText.WordWrap = label.Checked;
        }
    }

    private void LabelBackColor_Click(object sender, EventArgs e)
    {
        if (sender is null) return;

        ColorDialog colorDlg = new()
        {
            AllowFullOpen = true,
            FullOpen = true,
            AnyColor = true
        };

        if (ActiveMdiChild?.ActiveControl is RichTextBox richText)
        {
            colorDlg.Color = richText.BackColor;
            if (colorDlg.ShowDialog(this) == DialogResult.OK)
            {
                richText.BackColor = colorDlg.Color;
                this.statusStripLabelBackColor.BackColor = colorDlg.Color;
            }
        }
    }

    private void LabelZoom_Click(object sender, EventArgs e)
    {
        if (sender is null) return;

        FrmZoom frmZoom = new(_settings.TextZoom, _settings.AppCulture);
        frmZoom.Icon = GraphicsResources.Load<Icon>(GraphicsResources.AppLogo);
        if (frmZoom.ShowDialog(this) == DialogResult.OK)
        {
            if (ActiveMdiChild?.ActiveControl is RichTextBox richText)
            {
                richText.ZoomFactor = frmZoom.ZoomLevel / 100;
                this.statusStripLabelZoom.Text = $"{(frmZoom.ZoomLevel / 100).ToString("0.##")}x";
            }
        }
    }

}