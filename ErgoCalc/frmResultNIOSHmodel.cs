using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Windows.Forms;

using ErgoCalc.DLL.NIOSHModel;

namespace ErgoCalc
{
    public partial class frmResultNIOSHmodel : Form, IChildResults
    {
        // Variable definition
        private modelNIOSH[] _sDatosNIOSH;
        private cModelINSHT _classNIOSH;
        private ResultsOptions _options;
        private bool _composite;

        public frmResultNIOSHmodel()
        {
            // VS designer initialization
            InitializeComponent();

            // Set private variable default values            
            _classNIOSH = new cModelINSHT();
            _options = new ResultsOptions(this.rtbShowResult);

            // ToolStrip
            var path = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            if (File.Exists(path + @"\images\save.ico")) this.toolStripNIOSH_Save.Image = new Icon(path + @"\images\save.ico", 48, 48).ToBitmap();
            if (File.Exists(path + @"\images\settings.ico")) this.toolStripNIOSH_Settings.Image = new Icon(path + @"\images\settings.ico", 48, 48).ToBitmap();
            //this.toolStripNIOSH_Settings.CheckOnClick = true;

            propertyGrid1.SelectedObject = _options;
            splitContainer1.Panel1Collapsed = false;
            splitContainer1.SplitterDistance = 0;
            splitContainer1.SplitterWidth = 1;
            splitContainer1.IsSplitterFixed = true;
        }

        public frmResultNIOSHmodel(modelNIOSH[] datos, bool composite)
            :this()
        {
            _sDatosNIOSH = datos;
            _composite = composite;

        }

        private void frmResultNIOSHModel_Shown(object sender, EventArgs e)
        {
            // Variable definition
            Int32[] orden;
            Int32 nSize;
            Boolean error = false;
            Double resultado = 0.0;
            //Boolean composite;         
            //frmDataNIOSHmodel frm = new frmDataNIOSHmodel(_sDatosNIOSH);

            //if (frm.ShowDialog() == DialogResult.OK)
            //{
                // Retrieve data from the dialog
                //_sDatosNIOSH = frm.getData();
                //composite = frm._composite;

                nSize = _sDatosNIOSH.Length;
                orden = new Int32[nSize];
                for (Int32 i = 0; i < nSize; i++) orden[i] = i;

                // Call the DLL function
                try
                {
                    resultado = _classNIOSH.CalculateNIOSH(_sDatosNIOSH, orden, ref nSize);
                }
                catch (EntryPointNotFoundException)
                {
                    error = true;
                    MessageBox.Show(
                        "The program calculation kernel's been tampered with.\nThe NIOSH index could not be computed.",
                        "NIOSH index error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    this.Close();
                }
                catch (DllNotFoundException)
                {
                    error = true;
                    MessageBox.Show(
                        "Some files are missing. Please\nreinstall the application.",
                        "NIOSH index error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    this.Close();
                }
                catch (Exception ex)
                {
                    error = true;
                    MessageBox.Show(
                        "Error in the calculation kernel:\n" + ex.ToString(),
                        "Unexpected error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    this.Close();
                }

                // Call the routine that shows the results
                if (error == false) ShowResults(_sDatosNIOSH, orden, resultado, _composite);
            //}
            //else
                // When this method is called artificially from code, don't do anything
                //if (sender != null) this.Close();
        }

        private void rtbShowResult_DoubleClick(object sender, EventArgs e)
        {            
            frmResultNIOSHModel_Shown(null, EventArgs.Empty);
        }

        #region Private routines
        /// <summary>
        /// Shows the numerical results in the RTF control
        /// </summary>
        /// <param name="sModel">Data and results array</param>
        /// <param name="indexTable">Array containing the tasks order</param>
        /// <param name="resultado">NIOSH compound index</param>
        /// <param name="composite">Boolean value which shows if the composite index has been calculated</param>
        private void ShowResults(modelNIOSH[] sModel, Int32[] indexTable, Double resultado, Boolean composite)
        {
            Int32 i, length = sModel.Length;
            Int32[] ordenacion = new Int32[length];
            String strEquationT;
            String strEquationN;
            String[] strLineD = new String[11];
            String[] strLineR = new String[13];
            String[] strTasks = new String[] {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J"};
            String[] coupling = new String[] { "Good", "Poor", "No hndl" };

            for (i = 0; i < length; i++) ordenacion[indexTable[i]] = length - i;

            for (i=0; i<length;i++)
            {
                strLineD[0] += "\t\t" + "Task " + ((char)('A' + i)).ToString();
                strLineD[1] += "\t\t" + sModel[i].data.weight.ToString();
                strLineD[2] += "\t\t" + sModel[i].data.h.ToString();
                strLineD[3] += "\t\t" + sModel[i].data.v.ToString();
                strLineD[4] += "\t\t" + sModel[i].data.d.ToString();
                strLineD[5] += "\t\t" + sModel[i].data.f.ToString();
                strLineD[6] += "\t\t" + sModel[i].data.fa.ToString();
                strLineD[7] += "\t\t" + sModel[i].data.fb.ToString();
                strLineD[8] += "\t\t" + sModel[i].data.td.ToString();
                strLineD[9] += "\t\t" + sModel[i].data.a.ToString();
                strLineD[10] += "\t" + coupling[sModel[i].data.c - 1];

                strLineR[0] += "\t\t" + "Task " + ((char)('A' + i)).ToString();
                strLineR[1] += "\t\t" + sModel[i].factors.LC.ToString("0.####");
                strLineR[2] += "\t\t" + sModel[i].factors.HM.ToString("0.####");
                strLineR[3] += "\t\t" + sModel[i].factors.VM.ToString("0.####");
                strLineR[4] += "\t\t" + sModel[i].factors.DM.ToString("0.####");
                strLineR[5] += "\t\t" + sModel[i].factors.FM.ToString("0.####");
                strLineR[6] += "\t\t" + sModel[i].factors.FMa.ToString("0.####");
                strLineR[7] += "\t\t" + sModel[i].factors.FMb.ToString("0.####");
                strLineR[8] += "\t\t" + sModel[i].factors.AM.ToString("0.####");
                strLineR[9] += "\t\t" + sModel[i].factors.CM.ToString("0.####");
                strLineR[10] += "\t\t" + sModel[i].indexIF.ToString("0.####");
                if (composite == true)
                    strLineR[11] += "\t";
                strLineR[11] += "\t" + sModel[i].index.ToString("0.####");
                strLineR[12] += "\t\t" + ordenacion[i].ToString();

                //ordenacion[length - indexTable[i] - 1] = i;
                //ordenacion[indexTable[i]] = length - i - 1;
            }

            for (i = 0; i < length; i++) ordenacion[i] = indexTable[length - i - 1];
            
            //rtbShowResult.AcceptsTab = true;
            //rtbShowResult.SelectionTabs = new int[] { 200, 300, 400, 500, 600, 700, 800, 900, 1000, 1100, 1200, 1300, 1400, 1500};

            rtbShowResult.Text = "These are the results obtained from the NIOSH lifting model:";
            rtbShowResult.AppendText("\n\n");

            // Print data
            rtbShowResult.SelectionFont = new Font(rtbShowResult.Font.Name, rtbShowResult.Font.Size, FontStyle.Underline | FontStyle.Bold);
            rtbShowResult.AppendText("Description\t\t" + strLineD[0] + "\n");
            rtbShowResult.SelectionFont = new Font(rtbShowResult.Font.Name, rtbShowResult.Font.Size, FontStyle.Regular);            
            rtbShowResult.AppendText("Weight lifted (kg):\t" + strLineD[1] + "\n");            
            rtbShowResult.AppendText("Horizontal distance (cm):" + strLineD[2] + "\n");
            rtbShowResult.AppendText("Vertical distance (cm):" + strLineD[3] + "\n");
            rtbShowResult.AppendText("Vertical travel distance (cm):\t" + strLineD[4].TrimStart('\t') + "\n");
            rtbShowResult.AppendText("Lifting frequency (times/min):\t" + strLineD[5].TrimStart('\t') + "\n");
            if (length > 1 && composite == true)
            {
                rtbShowResult.AppendText("Lifting frequency A (times/min):\t" + strLineD[6].TrimStart('\t') + "\n");
                rtbShowResult.AppendText("Lifting frequency B (times/min):\t" + strLineD[7].TrimStart('\t') + "\n");
            }
            rtbShowResult.AppendText("Task duration (hours):" + strLineD[8] + "\n");
            rtbShowResult.AppendText("Twisting angle (º):\t" + strLineD[9] + "\n");
            rtbShowResult.AppendText("Coupling:\t\t\t" + strLineD[10] + "\n\n");

            // Print factors
            rtbShowResult.SelectionFont = new Font(rtbShowResult.Font.Name, rtbShowResult.Font.Size, FontStyle.Underline | FontStyle.Bold);
            rtbShowResult.AppendText("Description\t\t" + strLineR[0] + "\n");
            rtbShowResult.SelectionFont = new Font(rtbShowResult.Font.Name, rtbShowResult.Font.Size, FontStyle.Regular);
            rtbShowResult.AppendText("Lifting constant (LC):" + strLineR[1] + "\n");
            rtbShowResult.AppendText("Horizontal multiplier (HM):" + strLineR[2] + "\n");
            rtbShowResult.AppendText("Vertical multiplier (VM):" + strLineR[3] + "\n");
            rtbShowResult.AppendText("Distance multiplier(DM):" + strLineR[4] + "\n");
            rtbShowResult.AppendText("Frequency multiplier(FM):" + strLineR[5] + "\n");
            if (length > 1 && composite == true)
            {
                rtbShowResult.AppendText("Frequency A multiplier (FMa):\t" + strLineR[6].TrimStart('\t') + "\n");
                rtbShowResult.AppendText("Frequency B multiplier (FMb):\t" + strLineR[7].TrimStart('\t') + "\n");
            }
            rtbShowResult.AppendText("Twisting angle multiplier (AM):\t" + strLineR[8].TrimStart('\t') + "\n");
            rtbShowResult.AppendText("Coupling multiplier (CM):" + strLineR[9] + "\n\n");

            if (length > 1)
            {
                if (composite == true)
                {
                    rtbShowResult.AppendText("Lifting index (IF):\t" + strLineR[10] + "\n");
                    rtbShowResult.AppendText("Lifting index:\t" + strLineR[11] + "\n");
                    rtbShowResult.AppendText("Tasks order:\t\t" + strLineR[12] + "\n\n");
                }
                else
                {
                    rtbShowResult.SelectionFont = new Font(rtbShowResult.Font.Name, rtbShowResult.Font.Size + 1, FontStyle.Bold);
                    rtbShowResult.AppendText("Lifting index:\t\t" + strLineR[11] + "\n\n");
                    rtbShowResult.SelectionFont = new Font(rtbShowResult.Font.Name, rtbShowResult.Font.Size - 1, FontStyle.Regular);
                }
            }

            // Print NIOSH final equation
            rtbShowResult.AppendText("The NIOSH lifting index is computed as follows:\n\n");
            if (length > 1)
            {
                if (composite == true)
                {
                    strEquationT = "CLI = Index(" + strTasks[ordenacion[0]] + ")"; //((char)('A' + i)).ToString()
                    strEquationN = "CLI = " + sModel[ordenacion[0]].index.ToString("0.####");
                    for (i = 1; i < length; i++)
                    {
                        strEquationT += " + IndexIF(" + strTasks[ordenacion[i]] + ") * (1/FMa(" + strTasks[ordenacion[i]] + ") - 1/FMb(" + strTasks[ordenacion[i]] + "))";
                        strEquationN += " + " + sModel[ordenacion[i]].indexIF.ToString("0.####") + " * (1/" + sModel[ordenacion[i]].factors.FMa.ToString("0.####") + " - 1/" + sModel[ordenacion[i]].factors.FMb.ToString("0.####") + ")";
                    }
                    strEquationN += " = " + resultado.ToString("0.####");
                    rtbShowResult.AppendText(strEquationT + "\n");
                    rtbShowResult.AppendText(strEquationN + "\n\n");
                    rtbShowResult.SelectionFont = new Font(rtbShowResult.Font.Name, rtbShowResult.Font.Size + 1, FontStyle.Bold);
                    rtbShowResult.AppendText("The NIOSH lifting index is: " + resultado.ToString("0.####") + "\n");
                    rtbShowResult.SelectionFont = new Font(rtbShowResult.Font.Name, rtbShowResult.Font.Size - 1, FontStyle.Regular);
                }
                else
                {
                    strEquationT = "LI = Weight / (LC * HM * VM * DM * FM * AM * CM)";
                    rtbShowResult.AppendText(strEquationT + "\n");
                    for (i = 0; i < length; i++)
                    {
                        strEquationN = "LI = " + sModel[i].data.weight.ToString("0.####") + " / (";
                        strEquationN += sModel[i].factors.LC.ToString("0.####") + " * ";
                        strEquationN += sModel[i].factors.HM.ToString("0.####") + " * ";
                        strEquationN += sModel[i].factors.VM.ToString("0.####") + " * ";
                        strEquationN += sModel[i].factors.DM.ToString("0.####") + " * ";
                        strEquationN += sModel[i].factors.FM.ToString("0.####") + " * ";
                        strEquationN += sModel[i].factors.AM.ToString("0.####") + " * ";
                        strEquationN += sModel[i].factors.CM.ToString("0.####") + ") = ";
                        strEquationN += sModel[i].index.ToString("0.####");
                        rtbShowResult.AppendText(strEquationN + "\n");
                    }
                    rtbShowResult.AppendText("\n");
                }
            }
            else
            {
                strEquationT = "LI = Weight / (LC * HM * VM * DM * FM * AM * CM)";
                strEquationN = "LI = " + sModel[0].data.weight.ToString("0.####") + " / (";
                strEquationN += sModel[0].factors.LC.ToString("0.####") + " * ";
                strEquationN += sModel[0].factors.HM.ToString("0.####") + " * ";
                strEquationN += sModel[0].factors.VM.ToString("0.####") + " * ";
                strEquationN += sModel[0].factors.DM.ToString("0.####") + " * ";
                strEquationN += sModel[0].factors.FM.ToString("0.####") + " * ";
                strEquationN += sModel[0].factors.AM.ToString("0.####") + " * ";
                strEquationN += sModel[0].factors.CM.ToString("0.####") + ") = ";
                strEquationN += sModel[0].index.ToString("0.####");

                rtbShowResult.AppendText(strEquationT + "\n");
                rtbShowResult.AppendText(strEquationN + "\n\n");
                rtbShowResult.SelectionFont = new Font(rtbShowResult.Font.Name, rtbShowResult.Font.Size + 2, FontStyle.Bold);
                rtbShowResult.AppendText("The NIOSH lifting index is: " + resultado.ToString("0.####") + "\n");
                rtbShowResult.SelectionFont = new Font(rtbShowResult.Font.Name, rtbShowResult.Font.Size - 1, FontStyle.Regular);
            }

        }
        #endregion

        #region IChild interface

        public void Save(string path)
        {
            // https://msdn.microsoft.com/en-us/library/ms160336(v=vs.110).aspx
            // Displays a SaveFileDialog so the user can save the Image  
            // assigned to Button2
            SaveFileDialog saveFileDialog1 = new SaveFileDialog
            {
                DefaultExt = "*.txt",
                Filter = "RTF file (*.rtf)|*.rtf|Text file (*.txt)|*.txt|All files (*.*)|*.*",
                FilterIndex = 1,
                Title = "Save NIOSH model results",
                OverwritePrompt = true,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };

            DialogResult result;
            using (new CenterWinDialog(this))
            {
                result = saveFileDialog1.ShowDialog();
            }

            // If the file name is not an empty string open it for saving.  
            if (result == DialogResult.OK && saveFileDialog1.FileName != "")
            {
                System.IO.FileStream fs;

                // Saves the text via a FileStream created by the OpenFile method.  
                if ((fs = (System.IO.FileStream)saveFileDialog1.OpenFile()) != null)
                {
                    // Saves the text in the appropriate TextFormat based upon the File type selected in the dialog box.  
                    // NOTE that the FilterIndex property is one-based. 
                    switch (saveFileDialog1.FilterIndex)
                    {
                        case 1:
                            rtbShowResult.SaveFile(fs, RichTextBoxStreamType.RichText);
                            break;
                        case 2:
                            rtbShowResult.SaveFile(fs, RichTextBoxStreamType.PlainText);
                            break;
                        case 3:
                            rtbShowResult.SaveFile(fs, RichTextBoxStreamType.UnicodePlainText);
                            break;
                    }

                    fs.Close();

                    using (new CenterWinDialog(this.MdiParent))
                    {
                        MessageBox.Show(this, "The file was successfully saved", "File saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }

            return;
        }

        public void EditData()
        {
            return;
        }

        public void Duplicate()
        {
            string _strPath = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);

            // Mostrar la ventana de resultados
            frmResultNIOSHmodel frmResults = new frmResultNIOSHmodel(_sDatosNIOSH, _composite)
            {
                MdiParent = this.MdiParent
            };
            if (File.Exists(_strPath + @"\images\logo.ico")) frmResults.Icon = new Icon(_strPath + @"\images\logo.ico");
            frmResults.Show();
        }

        public bool[] GetToolbarEnabledState()
        {
            bool[] toolbar = new bool[] { true, true, true, false, true, true, true, true, true, false, false, true, true, true };
            return toolbar;
        }

        public ToolStrip ChildToolStrip
        {
            get => toolStripNIOSH;
            set => toolStripNIOSH = value;
        }
        public void ShowHideSettings()
        {
            //splitContainer1.Panel1Collapsed = !splitContainer1.Panel1Collapsed;
            this.SuspendLayout();
            if (splitContainer1.SplitterDistance > 0)
            {
                Transitions.Transition.run(this.splitContainer1, "SplitterDistance", 0, new Transitions.TransitionType_Linear(200));
                splitContainer1.SplitterWidth = 1;
                splitContainer1.IsSplitterFixed = true;
            }
            else
            {
                Transitions.Transition.run(this.splitContainer1, "SplitterDistance", 200, new Transitions.TransitionType_Linear(200));
                splitContainer1.SplitterWidth = 4;
                splitContainer1.IsSplitterFixed = false;
            }
            this.ResumeLayout();
        }

        public bool PanelCollapsed()
        {
            return this.splitContainer1.SplitterDistance == 0 ? true: false;
        }

        public void FormatText()
        {
            // Underline
            string line = rtbShowResult.Lines[2];
            rtbShowResult.Select(rtbShowResult.Find("Description", 0, RichTextBoxFinds.MatchCase), line.Length);
            //rtbShowResult.Select(rtbShowResult.GetFirstCharIndexFromLine(2), line.Length);
            rtbShowResult.SelectionFont = new Font(rtbShowResult.SelectionFont, FontStyle.Underline | FontStyle.Bold);

            line = rtbShowResult.Lines[9];
            rtbShowResult.Select(rtbShowResult.Find("Description", rtbShowResult.SelectionStart + 1, RichTextBoxFinds.MatchCase), line.Length);
            //rtbShowResult.Select(rtbShowResult.GetFirstCharIndexFromLine(9), line.Length);
            rtbShowResult.SelectionFont = new Font(rtbShowResult.SelectionFont, FontStyle.Underline | FontStyle.Bold);

            // Bold results
            line = rtbShowResult.Lines[16];
            rtbShowResult.Select(rtbShowResult.Find("The", rtbShowResult.SelectionStart + 1, RichTextBoxFinds.MatchCase), line.Length);
            //rtbShowResult.Select(rtbShowResult.GetFirstCharIndexFromLine(16), line.Length);
            rtbShowResult.SelectionFont = new Font(rtbShowResult.Font.FontFamily, rtbShowResult.Font.Size + 1, FontStyle.Bold);

            // Set the cursor at the beginning of the text
            rtbShowResult.SelectionStart = 0;
            rtbShowResult.SelectionLength = 0;
        }

        #endregion IChild interface

    }
}
