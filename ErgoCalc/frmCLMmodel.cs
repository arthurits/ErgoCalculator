using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

// Para llamar a la DLL
using ErgoCalc.DLL.CLMmodel;

namespace ErgoCalc
{
    public partial class frmCLMmodel : Form, IChildResults
    {
        // Variable definition
        private modelCLM [] _sDatosCLM;
        private cModelCLM _classDLL;
        private ResultsOptions _options;

        public frmCLMmodel()
        {
            InitializeComponent();

            // Initialize private variable
            _classDLL = new cModelCLM();
            _options = new ResultsOptions(rtbShowResult);

            propertyGrid1.SelectedObject = _options;
            splitContainer1.Panel1Collapsed = true;
            //rtbShowResult.Size = this.ClientSize;
        }

        public frmCLMmodel(modelCLM[] datos)
            :this()
        {
            _sDatosCLM = datos;
        }

        private void frmCLMmodel_Shown(object sender, EventArgs e)
        {

            // Variable definition
            Int32 nSize;
            Boolean error = false;
            //frmDataCLMmodel frm = new frmDataCLMmodel(_sDatosCLM);

            // Show dialog
            //if (frm.ShowDialog() == DialogResult.OK)
            //{
                // Retrieve data from the dialog
                //_sDatosCLM = frm._sData;
                nSize = _sDatosCLM.Length;

                // Call the DLL function
                try
                {
                    _classDLL.CalculateLSI(_sDatosCLM, ref nSize);
                }
                catch (EntryPointNotFoundException)
                {
                    error = true;
                    MessageBox.Show(
                        "The program calculation kernel's been tampered with.\nThe LSI could not be computed.",
                        "LSI index error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                catch (DllNotFoundException)
                {
                    error = true;
                    MessageBox.Show(
                        "Some files are missing. Please\nreinstall the application.",
                        "LSI index error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    error = true;
                    MessageBox.Show(
                        "Error in the calculation kernel:\n" + ex.ToString(),
                        "Unexpected error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }

                // Call the routine that shows the results
                if (error == false) ShowResults(_sDatosCLM);
            //}
            //else
                // When this method is called artificially from code, don't do anything
                //if (sender != null) this.Close();

        }

        private void rtbShowResult_DoubleClick(object sender, EventArgs e)
        {
            frmCLMmodel_Shown(null, EventArgs.Empty);
        }

        private void frmCLMmodel_Resize(object sender, EventArgs e)
        {
            // Resize the control to fit the form's client area
            //rtbShowResult.Size = this.ClientSize;
        }

        #region Private routines
        /// <summary>
        /// Shows the numerical results in the RTF control
        /// </summary>
        /// <param name="sData">Data and results array</param>
        private void ShowResults(modelCLM [] sData)
        {
            Int32 i, length = sData.Length;
            String[] strLineD = new String[13];
            String[] strLineR = new String[14];
            String[] gender = new String[] {"Male", "Female"};            
            String[] coupling = new String[] {"Good", "Poor", "No hndl" };
            String[] strTasks = new String[] { "A", "B", "C", "D", "E" };

            for (i = 0; i < length; i++)
            {
                strLineD[0] += "\t\t" + "Task " + strTasks[i];
                strLineD[1] += "\t\t" + gender[sData[i].data.gender - 1];
                strLineD[2] += "\t\t" + sData[i].data.weight.ToString();
                strLineD[3] += "\t\t" + sData[i].data.h.ToString();
                strLineD[4] += "\t\t" + sData[i].data.v.ToString();
                strLineD[5] += "\t\t" + sData[i].data.d.ToString();
                strLineD[6] += "\t\t" + sData[i].data.f.ToString();
                strLineD[7] += "\t\t" + sData[i].data.td.ToString();
                strLineD[8] += "\t\t" + sData[i].data.t.ToString();
                strLineD[9] += "\t\t" + coupling[sData[i].data.c - 1];
                strLineD[10] += "\t\t" + sData[i].data.hs.ToString();
                strLineD[11] += "\t\t" + sData[i].data.ag.ToString();
                strLineD[12] += "\t\t" + sData[i].data.bw.ToString();

                strLineR[0] += "\t\t" + "Task " + strTasks[i];
                strLineR[1] += "\t\t" + sData[i].factors.fH.ToString("0.####");
                strLineR[2] += "\t\t" + sData[i].factors.fV.ToString("0.####");
                strLineR[3] += "\t\t" + sData[i].factors.fD.ToString("0.####");
                strLineR[4] += "\t\t" + sData[i].factors.fF.ToString("0.####");
                strLineR[5] += "\t\t" + sData[i].factors.fTD.ToString("0.####");
                strLineR[6] += "\t\t" + sData[i].factors.fT.ToString("0.####");
                strLineR[7] += "\t\t" + sData[i].factors.fC.ToString("0.####");
                strLineR[8] += "\t\t" + sData[i].factors.fHS.ToString("0.####");
                strLineR[9] += "\t\t" + sData[i].factors.fAG.ToString("0.####");
                strLineR[10] += "\t\t" + sData[i].factors.fBW.ToString("0.####");
                strLineR[11] += "\t\t" + (sData[i].data.weight / (
                                                            sData[i].factors.fH *
                                                            sData[i].factors.fV *
                                                            sData[i].factors.fD *
                                                            sData[i].factors.fF *
                                                            sData[i].factors.fTD *
                                                            sData[i].factors.fT *
                                                            sData[i].factors.fC *
                                                            sData[i].factors.fHS *
                                                            sData[i].factors.fAG *
                                                            sData[i].factors.fBW)).ToString("0.####");
                strLineR[12] += "\t\t" + (100 - 10 * sData[i].indexLSI).ToString("0.####");
                strLineR[13] += "\t" + sData[i].indexLSI.ToString("0.####");

            }
            rtbShowResult.Text = "The LSI index from the following data:";
            rtbShowResult.AppendText("\n\n");

            // Print data
            rtbShowResult.SelectionFont = new Font(rtbShowResult.Font.Name, rtbShowResult.Font.Size, FontStyle.Underline | FontStyle.Bold);
            rtbShowResult.AppendText("Description\t\t" + strLineD[0] + "\n");
            rtbShowResult.SelectionFont = new Font(rtbShowResult.Font.Name, rtbShowResult.Font.Size, FontStyle.Regular);
            rtbShowResult.AppendText("Gender:\t\t\t" + strLineD[1] + "\n");
            rtbShowResult.AppendText("Weight lifted (kg):\t\t" + strLineD[2] + "\n");
            rtbShowResult.AppendText("Horizontal distance (cm):\t" + strLineD[3] + "\n");
            rtbShowResult.AppendText("Vertical distance (cm):\t" + strLineD[4] + "\n");
            rtbShowResult.AppendText("Vertical travel distance (cm):" + strLineD[5] + "\n");
            rtbShowResult.AppendText("Lifting frequency (times/min):" + strLineD[6] + "\n");
            rtbShowResult.AppendText("Task duration (hours):\t" + strLineD[7] + "\n");
            rtbShowResult.AppendText("Twisting angle (º):\t" + strLineD[8] + "\n");
            rtbShowResult.AppendText("Coupling:\t\t" + strLineD[9] + "\n");
            rtbShowResult.AppendText("WBGT temperature (ºC):\t" + strLineD[10] + "\n");
            rtbShowResult.AppendText("Age (years):\t\t" + strLineD[11] + "\n");
            rtbShowResult.AppendText("Body weight (kg):\t\t" + strLineD[12] + "\n\n");
            
            // Print results
            rtbShowResult.SelectionFont = new Font(rtbShowResult.Font.Name, rtbShowResult.Font.Size, FontStyle.Underline | FontStyle.Bold);
            rtbShowResult.AppendText("Description\t\t" + strLineR[0] + "\n");
            rtbShowResult.SelectionFont = new Font(rtbShowResult.Font.Name, rtbShowResult.Font.Size, FontStyle.Regular);
            rtbShowResult.AppendText("Horizontal multiplier:\t" + strLineR[1] + "\n");
            rtbShowResult.AppendText("Vertical multiplier:\t" + strLineR[2] + "\n");
            rtbShowResult.AppendText("Distance multiplier:\t" + strLineR[3] + "\n");
            rtbShowResult.AppendText("Frequency multiplier:\t" + strLineR[4] + "\n");
            rtbShowResult.AppendText("Task duration multiplier:\t" + strLineR[5] + "\n");
            rtbShowResult.AppendText("Twisting multiplier:\t" + strLineR[6] + "\n");
            rtbShowResult.AppendText("Coupling multiplier:\t" + strLineR[7] + "\n");
            rtbShowResult.AppendText("WBGT multiplier:\t\t" + strLineR[8] + "\n");
            rtbShowResult.AppendText("Age multiplier:\t\t" + strLineR[9] + "\n");
            rtbShowResult.AppendText("Body weight multiplier:\t" + strLineR[10] + "\n\n");
            rtbShowResult.AppendText("Base weight:\t\t" + strLineR[11] + "\n");
            rtbShowResult.AppendText("Population percentage:\t" + strLineR[12] + "\n");
            rtbShowResult.SelectionFont = new Font(rtbShowResult.Font.Name, rtbShowResult.Font.Size + 2, FontStyle.Bold);
            rtbShowResult.AppendText("The LSI index is:\t\t" + strLineR[13] + "\n");
            rtbShowResult.SelectionFont = new Font(rtbShowResult.Font.Name, rtbShowResult.Font.Size - 1, FontStyle.Regular);
        }

        #endregion

        #region IChildResults
        public void Save(string path)
        {
            throw new NotImplementedException();
        }

        public void EditData()
        {
            throw new NotImplementedException();
        }

        public void Duplicate()
        {
            throw new NotImplementedException();
        }

        public bool[] GetToolbarEnabledState()
        {
            return new bool[] { true, true, false, false, true, true, false, false, true, false, false, true, true, true };
        }

        public ToolStrip ChildToolStrip
        {
            get => null;
            set { }
        }

        public void ShowHideSettings()
        {
            splitContainer1.Panel1Collapsed = !splitContainer1.Panel1Collapsed;
            return;
        }

        public bool PanelCollapsed()
        {
            return this.splitContainer1.Panel1Collapsed;
        }

        public void FormatText()
        {
            // Underline
            string line = rtbShowResult.Lines[2];
            rtbShowResult.Select(rtbShowResult.Find("Description", 0, RichTextBoxFinds.MatchCase), line.Length);
            //rtbShowResult.Select(rtbShowResult.GetFirstCharIndexFromLine(2), line.Length);
            rtbShowResult.SelectionFont = new Font(rtbShowResult.SelectionFont, FontStyle.Underline | FontStyle.Bold);

            line = rtbShowResult.Lines[16];
            rtbShowResult.Select(rtbShowResult.Find("Description", rtbShowResult.SelectionStart + 1, RichTextBoxFinds.MatchCase), line.Length);
            //rtbShowResult.Select(rtbShowResult.GetFirstCharIndexFromLine(9), line.Length);
            rtbShowResult.SelectionFont = new Font(rtbShowResult.SelectionFont, FontStyle.Underline | FontStyle.Bold);

            // Bold results
            line = rtbShowResult.Lines[30];
            rtbShowResult.Select(rtbShowResult.Find("The", rtbShowResult.SelectionStart + 1, RichTextBoxFinds.MatchCase), line.Length);
            //rtbShowResult.Select(rtbShowResult.GetFirstCharIndexFromLine(16), line.Length);
            rtbShowResult.SelectionFont = new Font(rtbShowResult.SelectionFont.FontFamily, rtbShowResult.Font.Size + 1, FontStyle.Bold);

            // Set the cursor at the beginning of the text
            rtbShowResult.SelectionStart = 0;
            rtbShowResult.SelectionLength = 0;
        }
        #endregion IChildResults

        private void frmCLMmodel_Load(object sender, EventArgs e)
        {
            //Win32.Win32API.AnimateWindow(this.Handle, 100, Win32.Win32API.AnimateWindowFlags.AW_BLEND | Win32.Win32API.AnimateWindowFlags.AW_CENTER);
        }

        private void frmCLMmodel_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Win32.Win32API.AnimateWindow(this.Handle, 100, Win32.Win32API.AnimateWindowFlags.AW_HIDE | Win32.Win32API.AnimateWindowFlags.AW_BLEND);
        }

    }
}
