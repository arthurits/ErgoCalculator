using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ErgoCalc
{
    public partial class frmResultsOCRAcheck : Form, IChildResults
    {
        private string _strPath;
        public frmResultsOCRAcheck()
        {
            InitializeComponent();

            _strPath = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            if (File.Exists(_strPath + @"\images\logo.ico")) this.Icon = new Icon(_strPath + @"\images\logo.ico");

            // Initialize private variables
            //_classDLL = new cModelStrain();

            propertyGrid1.SelectedObject = new ResultsOptions(rtbShowResult);
            splitContainer1.Panel1Collapsed = false;
            splitContainer1.SplitterDistance = 0;
            splitContainer1.SplitterWidth = 1;
            splitContainer1.IsSplitterFixed = true;
        }

        public frmResultsOCRAcheck(object data)
            :this()
        {
        }

        #region Events

        private void frmResultsOCRAcheck_Shown(object sender, EventArgs e)
        {
            Boolean error = false;
            // Call the DLL function
            try
            {
                //_classDLL.StrainIndex();
                //_job = _classDLL.Job;
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
            if (error == false)
            {
                //rtbShowResult.Text = _job.ToString();
                //CreatePlots();
                //FormatText();
            }

            //}
            //else
            // When this method is called artificially from code, don't do anything
            //if (sender != null) this.Close();
        }

        #endregion Events

        #region Private routines

        /// <summary>
        /// Serialize ModelJob structure to JSON
        /// </summary>
        /// <param name="writer">The already created writer</param>
        private void SerializeToJSON(Utf8JsonWriter writer)
        {
            
        }

        #endregion Private routines

        #region IChildResults

        public ToolStrip ChildToolStrip
        {
            get => null;
            set { }
        }

        public void Duplicate()
        {
            // Create a new instance of this form with the same data
            frmResultsOCRAcheck frmResults = new frmResultsOCRAcheck() { MdiParent = this.MdiParent };
            frmResults.Show();
        }

        public void EditData()
        {
            throw new NotImplementedException();
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

        public bool[] GetToolbarEnabledState()
        {
            return new bool[] { true, true, true, false, true, true, false, true, true, false, false, true, true, true };
        }

        public bool OpenFile(JsonDocument document)
        {
            throw new NotImplementedException();
        }

        public bool PanelCollapsed()
        {
            return splitContainer1.SplitterDistance == 0 ? true : false;
        }

        public void Save(string path)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog
            {
                DefaultExt = "*.rtf",
                Filter = "ERGO file (*.ergo)|*.ergo|RTF file (*.rtf)|*.rtf|Text file (*.txt)|*.txt|All files (*.*)|*.*",
                FilterIndex = 2,
                Title = "Save OCRA checklist results",
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
            }
        }

        public void ShowHideSettings()
        {
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
            return;
        }

        #endregion IChildResults

        
    }
}
