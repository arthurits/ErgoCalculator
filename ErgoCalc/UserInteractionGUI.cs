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
        FrmNew frmNew = new FrmNew();
        if (frmNew.ShowDialog() == DialogResult.Cancel) return;

        Form frmData = frmNew.Model switch
        {
            ModelType.WorkRest => new frmDataWR(),
            ModelType.CumulativeLifting => new frmDataCLM(),
            ModelType.NioshLifting => new frmDataNIOSH(),
            ModelType.StrainIndex => new FrmDataStrainIndex(),
            ModelType.OcraCheck => new FrmDataOCRAcheck(),
            ModelType.MetabolicRate => new FrmDataMet(),
            ModelType.ThermalComfort => new FrmDataTC(),
            ModelType.LibertyMutual => new FrmDataLiberty(),
            _ => new Form()
        };

        if (frmData is IChildData)
        {
            IChildData frm = frmData as IChildData;
            frm.LoadExample();

            if (frmData.ShowDialog(this) == DialogResult.OK)
            {
                Form frmResults = frmNew.Model switch
                {
                    ModelType.WorkRest => new frmResultsWR(frm.GetData),
                    ModelType.CumulativeLifting => new frmResultsCLM(frm.GetData),
                    ModelType.NioshLifting => new frmResultNIOSH(frm.GetData),
                    ModelType.StrainIndex => new FrmResultsStrainIndex(frm.GetData),
                    ModelType.OcraCheck => new FrmResultsOCRAcheck(frm.GetData),
                    ModelType.MetabolicRate => new FrmResultsMet(frm.GetData),
                    ModelType.ThermalComfort => new FrmResultsTC(frm.GetData),
                    ModelType.LibertyMutual => new FrmResultsLiberty(frm.GetData),
                    _ => new Form()
                };
                frmResults.MdiParent = this;
                if (File.Exists(_strPath + @"\images\logo.ico")) frmResults.Icon = new Icon(_strPath + @"\images\logo.ico");
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
        frmAbout frmAbout = new frmAbout();
        frmAbout.ShowDialog();
    }

    private void Open_Click(object sender, EventArgs e)
    {
        OpenFileDialog openDlg = new OpenFileDialog
        {
            DefaultExt = "*.ergo",
            Filter = "ERGO file (*.ergo)|*.ergo|All files (*.*)|*.*",
            FilterIndex = 1,
            Title = "Open ErgoCalc file",
            InitialDirectory = _strPath + @"\Examples"
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

            Form frm = strType switch
            {
                "Work-Rest model" => new frmResultsWR(),
                "NIOSH lifting equation" => new frmResultNIOSH(),
                "Strain index" => new FrmResultsStrainIndex(),
                "Thermal comfort model" => new FrmResultsTC(),
                "LM-MMH model" => new FrmResultsLiberty(),
                "Comprehensive lifting model" => new frmResultsCLM(),
                _ => default
            };

            if (frm != default)
            {
                frm.MdiParent = this;

                if (((IChildResults)frm).OpenFile(document))
                {
                    if (File.Exists(_strPath + @"\images\logo.ico")) frm.Icon = new Icon(_strPath + @"\images\logo.ico");
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
    }

}