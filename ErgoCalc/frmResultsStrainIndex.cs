using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

// Para llamar a la DLL
using ErgoCalc.DLL.Strain;

namespace ErgoCalc
{
    public partial class frmResultsStrainIndex : Form, IChildResults
    {
        // Variable definition
        private ModelSubTask[] _subtasks;
        private ModelTask[] _tasks;
        private ModelJob _job;
        private Index _index;
        private cModelStrain _classDLL;
        private ResultsOptions _options;

        public frmResultsStrainIndex()
        {
            InitializeComponent();

            //rtbShowResult.Size = this.ClientSize;
            
            // Initialize private variables
            // _classDLL = new cModelStrain();
            _options = new ResultsOptions(rtbShowResult);

            propertyGrid1.SelectedObject = _options;
            splitContainer1.Panel1Collapsed = true;

            //ToolStrip botón = ((frmMain)MdiParent).Controls["toolStripMain"] as ToolStrip;
            //ToolStripButton bot = botón.Items["toolStripMain_Settings"] as ToolStripButton;

            //splitContainer1.Panel1Collapsed = ((ToolStrip)((frmMain)MdiParent).Controls["toolStripMain"]).Items["toolStripMain_Settings"].Enabled == true ? false : true;


        }

        public frmResultsStrainIndex(Index index, ModelJob job)
            :this()
        {
            _index = index;
            _job = job;
            _classDLL = new cModelStrain(job, index);
            //_subtasks = subtasks;
            //_tasks = tasks;
            //_classDLL.IndexType = index;
            //_classDLL.SubTasks = subtasks;
            //_classDLL.Tasks = tasks;
            //_sData = datos;
        }

        private void frmResultsStrainIndex_Shown(object sender, EventArgs e)
        {
            //splitContainer1.Panel1Collapsed = ((ToolStrip)((frmMain)MdiParent).Controls["toolStripMain"]).Items["toolStripMain_Settings"].Enabled == true ? false : true;
            // Variable definition
            Boolean error = false;

            // Call the DLL function
            try
            {
                //_classDLL.StrainIndex(_classDLL.Parameters, orden, ref nSize);
                //_classDLL.RSI(_subtasks, orden, ref nSize);
                _classDLL.StrainIndex();
            }
            catch (EntryPointNotFoundException)
            {
                error = true;
                MessageBox.Show(
                    "The program calculation kernel's been tampered with.\nThe RSI could not be computed.",
                    "RSI index error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (DllNotFoundException)
            {
                error = true;
                MessageBox.Show(
                    "DLL files are missing. Please\nreinstall the application.",
                    "RSI index error",
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
            if (error == false) ShowResults(_subtasks);

            //}
            //else
                // When this method is called artificially from code, don't do anything
                //if (sender != null) this.Close();

        }

        private void rtbShowResult_DoubleClick(object sender, EventArgs e)
        {
            frmResultsStrainIndex_Shown(null, EventArgs.Empty);
        }

        private void frmResultsStrainIndex_Resize(object sender, EventArgs e)
        {
            // Resize the control to fit the form's client area
            //rtbShowResult.Size = this.ClientSize;
        }

        #region Private routines
        /// <summary>
        /// Shows the numerical results in the RTF control
        /// </summary>
        /// <param name="sData">Data and results array</param>
        private void ShowResults(ModelSubTask [] sData)
        {
            rtbShowResult.Text = _classDLL.ToString();
            FormatText();
        }

        #endregion

        #region IChildResults
        public void Save(string path)
        {
            // https://msdn.microsoft.com/en-us/library/ms160336(v=vs.110).aspx
            // Displays a SaveFileDialog so the user can save the Image  
            // assigned to Button2.  
            SaveFileDialog saveFileDialog1 = new SaveFileDialog
            {
                DefaultExt = "*.txt",
                Filter = "RTF file (*.rtf)|*.rtf|Text file (*.txt)|*.txt|All files (*.*)|*.*",
                FilterIndex = 2,
                Title = "Save Strain Index results",
                OverwritePrompt = true,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };
            DialogResult result = saveFileDialog1.ShowDialog();

            // If the file name is not an empty string open it for saving.  
            if (result==DialogResult.OK && saveFileDialog1.FileName != "")
            {
                System.IO.FileStream fs;

                // Saves the text via a FileStream created by the OpenFile method.  
                if ( (fs = (System.IO.FileStream)saveFileDialog1.OpenFile()) != null)
                {
                    // Saves the text in the appropriate TextFormat based upon the File type selected in the dialog box.  
                    // NOTE that the FilterIndex property is one-based. 
                    switch (saveFileDialog1.FilterIndex)
                    {
                        case 1:
                            rtbShowResult.SaveFile(fs,RichTextBoxStreamType.RichText);
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

        public bool[] GetToolbarEnabledState()
        {
            return new bool[] { true, true, true, false, true, true, false, false, true, false, false, true, true, true };
        }

        public void ShowHideSettings()
        {
            splitContainer1.Panel1Collapsed = !splitContainer1.Panel1Collapsed;
            return;
        }

        public bool PanelCollapsed ()
        {
            return this.splitContainer1.Panel1Collapsed;
        }

        public void FormatText()
        {
            int nStart = 0, nEnd = 0;

            while (true)
            {
                // Underline
                nStart = rtbShowResult.Find("Description", nStart + 1, -1, RichTextBoxFinds.MatchCase);
                if (nStart == -1) break;
                nEnd = rtbShowResult.Find(Environment.NewLine.ToCharArray(), nStart + 1);
                rtbShowResult.Select(nStart, nEnd - nStart);
                rtbShowResult.SelectionFont = new Font(rtbShowResult.SelectionFont, FontStyle.Underline | FontStyle.Bold);
            } 

            //nStart = rtbShowResult.Find("Description", nStart + 1, RichTextBoxFinds.MatchCase);
            //nEnd = rtbShowResult.Find(Environment.NewLine.ToCharArray(), nStart + 1);
            //rtbShowResult.Select(nStart, nEnd - nStart);
            //rtbShowResult.SelectionFont = new Font(rtbShowResult.SelectionFont, FontStyle.Underline | FontStyle.Bold);

            // Bold results
            nStart = 0;
            while (true)
            {
                nStart = rtbShowResult.Find("The ", nStart + 1, -1, RichTextBoxFinds.MatchCase);
                if (nStart == -1) break;
                //nEnd = rtbShowResult.Text.Length;
                nEnd = rtbShowResult.Find(Environment.NewLine.ToCharArray(), nStart + 1);
                rtbShowResult.Select(nStart, nEnd - nStart);
                rtbShowResult.SelectionFont = new Font(rtbShowResult.SelectionFont.FontFamily, rtbShowResult.Font.Size + 1, FontStyle.Bold);
            }

            // Set the cursor at the beginning of the text
            rtbShowResult.SelectionStart = 0;
            rtbShowResult.SelectionLength = 0;
        }
        #endregion

        private void frmResultsStrainIndex_Load(object sender, EventArgs e)
        {
            //Win32.Win32API.AnimateWindow(this.Handle, 100, Win32.Win32API.AnimateWindowFlags.AW_BLEND | Win32.Win32API.AnimateWindowFlags.AW_CENTER);
        }

        private void frmResultsStrainIndex_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Win32.Win32API.AnimateWindow(this.Handle, 3000, Win32.Win32API.AnimateWindowFlags.AW_HIDE | Win32.Win32API.AnimateWindowFlags.AW_BLEND);
        }
    }
}
