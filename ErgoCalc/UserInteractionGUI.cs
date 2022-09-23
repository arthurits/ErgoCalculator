using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
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

partial class frmMain
{
    private void mnuMainFrm_File_New_Click(object sender, EventArgs e)
    {
        FrmNew frmNew = new FrmNew();
        if (frmNew.ShowDialog() == DialogResult.Cancel) return;

        Form frmData = frmNew.Model switch
        {
            ModelType.WorkRest => new frmDataWRmodel(),
            ModelType.CumulativeLifting => new frmDataCLMmodel(),
            ModelType.NioshLifting => new frmDataNIOSHmodel(),
            ModelType.StrainIndex => new frmDataStrainIndex(),
            ModelType.OcraCheck => new frmDataOCRAcheck(),
            ModelType.MetabolicRate => new frmMet(),
            ModelType.ThermalComfort => new frmDataTC(),
            ModelType.LibertyMutual => new frmDataLiberty(),
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
                    ModelType.WorkRest => new FrmWRmodel(frm.GetData),
                    ModelType.CumulativeLifting => new frmCLMmodel(frm.GetData),
                    ModelType.NioshLifting => new frmResultNIOSHmodel(frm.GetData),
                    ModelType.StrainIndex => new frmResultsStrainIndex(frm.GetData),
                    ModelType.OcraCheck => new frmResultsOCRAcheck(frm.GetData),
                    ModelType.MetabolicRate => new frmMetResult(frm.GetData),
                    ModelType.ThermalComfort => new frmResultsTC(frm.GetData),
                    ModelType.LibertyMutual => new frmResultsLiberty(frm.GetData),
                    _ => new Form()
                };
                frmResults.MdiParent = this;
                if (File.Exists(_strPath + @"\images\logo.ico")) frmResults.Icon = new Icon(_strPath + @"\images\logo.ico");
                frmResults.Show();
            }
            frmData.Dispose();
        }
    }

    private void mnuMainFrm_File_Exit_Click(object sender, EventArgs e)
    {
        // Exit the application by calling the frmMain_FormClosing event
        this.Close();
    }

    private void mnuMainFrm_Help_About_Click(object sender, EventArgs e)
    {
        frmAbout frmAbout = new frmAbout();
        frmAbout.ShowDialog();
    }
    private void toolStripMain_Exit_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void toolStripMain_New_Click(object sender, EventArgs e)
    {
        mnuMainFrm_File_New_Click(null, null);
    }

    private void toolStripMain_Open_Click(object sender, EventArgs e)
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

            Form frm = default;

            switch (strType)
            {
                case "Work-Rest model":
                    frm = new FrmWRmodel() { MdiParent = this };
                    break;
                case "NIOSH lifting equation":
                    frm = new frmResultNIOSHmodel() { MdiParent = this };
                    break;
                case "Strain index":
                    frm = new frmResultsStrainIndex() { MdiParent = this };
                    break;
                case "Thermal comfort model":
                    frm = new frmResultsTC() { MdiParent = this };
                    break;
                case "LM-MMH model":
                    frm = new frmResultsLiberty() { MdiParent = this };
                    break;
            }

            if (frm != default)
            {
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

    private void toolStripMain_Save_Click(object sender, EventArgs e)
    {
        if (this.ActiveMdiChild != null)
            ((IChildResults)this.ActiveMdiChild).Save("");
    }

    private void toolStripMain_Copy_Click(object sender, EventArgs e)
    {
        if (this.ActiveMdiChild != null)
            ((IChildResults)this.ActiveMdiChild).Duplicate();
    }

    private void toolStripMain_EditData_Click(object sender, EventArgs e)
    {
        if (this.ActiveMdiChild != null)
            ((IChildResults)this.ActiveMdiChild).EditData();
    }

    private void toolStripMain_Settings_Click(object sender, EventArgs e)
    {
        if (this.ActiveMdiChild != null)
            ((IChildResults)this.ActiveMdiChild).ShowHideSettings();
    }

    private void toolStripMain_Settings_EnabledChanged(object sender, EventArgs e)
    {
        if (this.toolStripMain_Settings.Enabled == false) this.toolStripMain_Settings.Checked = false;
    }

    private void toolStripMain_About_Click(object sender, EventArgs e)
    {
        mnuMainFrm_Help_About_Click(null, null);
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
