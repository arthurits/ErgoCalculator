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

partial class frmMain
{
    private void mnuMainFrm_File_New_Click(object sender, EventArgs e)
    {
        frmNew frmNew = new frmNew();
        Form frmData = new Form();
        Form frmResults = new Form();
        if (frmNew.ShowDialog() == DialogResult.Cancel) return;

        switch (frmNew.Model)
        {
            case 1: // WR model

                // Llamar al formulario para introducir los datos
                frmDataWRmodel frmDatosWR = new frmDataWRmodel();
                if (frmDatosWR.ShowDialog(this) == DialogResult.OK)
                {
                    // Mostrar la ventana de resultados
                    FrmWRmodel frmWR = new FrmWRmodel(frmDatosWR.GetData);
                    frmWR.MdiParent = this;
                    if (File.Exists(_strPath + @"\images\logo.ico")) frmWR.Icon = new Icon(_strPath + @"\images\logo.ico");
                    frmWR.Show();

                    // Cerrar el formulario de entrada de datos
                    frmDatosWR.Dispose();
                }

                break;

            case 2: // CLM model

                // Llamar al formulario para introducir los datos y cargar un ejemplo
                frmDataCLMmodel frmDatosCLM = new frmDataCLMmodel();
                frmDatosCLM.LoadExample();

                if (frmDatosCLM.ShowDialog(this) == DialogResult.OK)
                {
                    // Mostrar la ventaja de resultados
                    frmCLMmodel frmCLM = new frmCLMmodel(frmDatosCLM.getData());
                    frmCLM.MdiParent = this;
                    if (File.Exists(_strPath + @"\images\logo.ico")) frmCLM.Icon = new Icon(_strPath + @"\images\logo.ico");
                    frmCLM.Show();

                    // Cerrar el formulario de entrada de datos
                    frmDatosCLM.Dispose();
                }

                break;

            case 3: // NIOSH model

                // Llamar al formulario para introducir los datos y cargar un ejemplo
                frmDataNIOSHmodel frmDatosNIOSH = new frmDataNIOSHmodel();
                frmDatosNIOSH.LoadExample();

                if (frmDatosNIOSH.ShowDialog(this) == DialogResult.OK)
                {
                    // Mostrar la ventana de resultados
                    frmResultNIOSHmodel frmNIOSH = new frmResultNIOSHmodel(frmDatosNIOSH.GetNioshLifting);
                    frmNIOSH.MdiParent = this;
                    if (File.Exists(_strPath + @"\images\logo.ico")) frmNIOSH.Icon = new Icon(_strPath + @"\images\logo.ico");
                    frmNIOSH.Show();

                    // Cerrar el formulario de entrada de datos
                    frmDatosNIOSH.Dispose();
                }

                break;

            case 4: // Revised strain index

                // Llamar al formulario para introducir los datos
                frmDataStrainIndex frmDataStrain = new frmDataStrainIndex();
                frmDataStrain.LoadExample();

                if (frmDataStrain.ShowDialog(this) == DialogResult.OK)
                {
                    // Mostrar la ventana de resultados
                    frmResultsStrainIndex frmStrainIndex = new frmResultsStrainIndex(frmDataStrain.Job);
                    frmStrainIndex.MdiParent = this;
                    //if (File.Exists(_strPath + @"\images\logo.ico")) frmStrainIndex.Icon = new Icon(_strPath + @"\images\logo.ico");
                    frmStrainIndex.Show();
                }
                // Cerrar el formulario de entrada de datos
                frmDataStrain.Dispose();
                break;

            case 5: // OCRA checklist
                frmData = new frmDataOCRAcheck();
                frmResults = new frmResultsOCRAcheck(((IChildData)frmData).GetData);
                break;

            case 6: // Metabolic rate

                // Llamar al formulario para introducir los datos
                frmMet frmMet = new frmMet();
                if (frmMet.ShowDialog(this) == DialogResult.OK)
                {
                    // Recuperar los datos introducidos por el usuario
                    Int32[] nDatos = frmMet.getData();
                    frmMet.Dispose();


                    // Mostrar el formulario con los resultados del cálculo
                    frmMetResult frmResult = new frmMetResult(nDatos);
                    frmResult.MdiParent = this;
                    if (File.Exists(_strPath + @"\images\logo.ico")) frmResult.Icon = new Icon(_strPath + @"\images\logo.ico");
                    frmResult.Show();
                }
                break;

            case 7: // Thermal comfort
                frmData = new frmDataTC();
                if (frmData.ShowDialog(this) == DialogResult.OK)
                {
                    // Mostrar la ventana de resultados
                    frmResults = new frmResultsTC(((IChildData)frmData).GetData);
                    frmResults.MdiParent = this;
                    frmResults.Show();
                }
                break;
            case 8: // Liberty mutual 
                frmData = new frmDataLiberty();
                if (frmData.ShowDialog(this) == DialogResult.OK)
                {
                    // Mostrar la ventana de resultados
                    frmResults = new frmResultsLiberty(((IChildData)frmData).GetData);
                    frmResults.MdiParent = this;
                    frmResults.Show();
                }
                break;
        }


        //if (frmData.ShowDialog(this) == DialogResult.OK)
        //{
        //    frmResults.MdiParent = this;
        //    frmResults.Show();
        //}

        frmData.Dispose();

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
