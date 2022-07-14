using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Windows.Forms;

using ErgoCalc.Models.NIOSHModel;
using System.Text.Json;

namespace ErgoCalc
{
    public partial class frmResultNIOSHmodel : Form, IChildResults
    {
        // Variable definition
        public ClassData data { get; set; } 
        private List<SubTask> _sDatosNIOSH;
        private cModelNIOSH _classNIOSH;
        private ResultsOptions _options;
        private bool _composite;

        public frmResultNIOSHmodel()
        {
            // VS designer initialization
            InitializeComponent();

            // Set private variable default values            
            _classNIOSH = new cModelNIOSH();
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

        public frmResultNIOSHmodel(object data, bool composite)
            : this()
        {
            _sDatosNIOSH = (List<SubTask>)data;
            _composite = composite;

            int nSize = ((List<SubTask>)data).Count;
            Int32[] order = new Int32[nSize];
            for (Int32 i = 0; i < nSize; i++) order[i] = i;
            _classNIOSH = new cModelNIOSH((List<SubTask>)data, order, composite);

        }

        public frmResultNIOSHmodel(ClassData Data, bool composite)
            : this()
        {
            data = Data;
            _sDatosNIOSH = data.SubTasks;
            _composite = composite;
            Int32[] order = new Int32[data.SubTasks.Count];
            for (Int32 i = 0; i < data.SubTasks.Count; i++) order[i] = i;
            _classNIOSH = new cModelNIOSH(data.SubTasks, order, composite);
        }

        private void frmResultNIOSHModel_Shown(object sender, EventArgs e)
        {
            ShowResults();
        }

        private void rtbShowResult_DoubleClick(object sender, EventArgs e)
        {
            frmResultNIOSHModel_Shown(null, EventArgs.Empty);
        }

        #region Private routines

        /// <summary>
        /// Computes the numerical results and shows them in the RichTextBox
        /// </summary>
        private void ShowResults()
        {
            // Variable definition
            Int32 nSize = _sDatosNIOSH.Count;
            Int32[] orden = new Int32[nSize];
            Boolean error = false;
            Double resultado = 0.0;

            if (nSize == 0) return;

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
            //if (error == false) ResultsToRichText(_sDatosNIOSH, orden, resultado, _composite);
            if (error == false)
            {
                rtbShowResult.Text = _classNIOSH.ToString();
                FormatText();
            }

        }

        /// <summary>
        /// Serialize ModelJob structure to JSON
        /// </summary>
        /// <param name="writer">The already created writer</param>
        private void SerializeToJSON(Utf8JsonWriter writer)
        {
            writer.WriteStartObject();
            writer.WriteString("Document type", "NIOSH lifting equation");
            //_classNIOSH.ToString();
            writer.WriteEndObject();
            writer.Flush();
        }

        #endregion Private routines

        #region IChild interface

        public void Save(string path)
        {
            // https://msdn.microsoft.com/en-us/library/ms160336(v=vs.110).aspx
            // Displays a SaveFileDialog so the user can save the Image  
            // assigned to Button2
            SaveFileDialog saveFileDialog1 = new SaveFileDialog
            {
                DefaultExt = "*.txt",
                Filter = "ERGO file (*.ergo)|*.ergo|RTF file (*.rtf)|*.rtf|Text file (*.txt)|*.txt|All files (*.*)|*.*",
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
                            using (var writer = new Utf8JsonWriter(fs, options: new JsonWriterOptions { Indented = true }))
                            {
                                SerializeToJSON(writer);
                            }
                            break;
                        case 2:
                            rtbShowResult.SaveFile(fs, RichTextBoxStreamType.RichText);
                            break;
                        case 3:
                            rtbShowResult.SaveFile(fs, RichTextBoxStreamType.PlainText);
                            break;
                        case 4:
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

        public bool OpenFile(JsonDocument document)
        {
            _sDatosNIOSH = new List<SubTask>();
            bool result = false;
            MessageBox.Show("Document opening not yet implemented");
            return result;
        }

        public void EditData()
        {
            using var frm = new frmDataNIOSHmodel(_sDatosNIOSH);

            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                _sDatosNIOSH = (List<SubTask>)frm.GetData;
                //ClearPlots();
                ShowResults();
            }
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
            int nStart = 0, nEnd = 0;

            while (true)
            {
                // Underline
                nStart = rtbShowResult.Find("Initial data", nStart + 1, -1, RichTextBoxFinds.MatchCase);
                if (nStart == -1) break;
                nEnd = rtbShowResult.Find(Environment.NewLine.ToCharArray(), nStart + 1);
                rtbShowResult.Select(nStart, nEnd - nStart);
                rtbShowResult.SelectionFont = new Font(rtbShowResult.SelectionFont, FontStyle.Underline | FontStyle.Bold);

                nStart = rtbShowResult.Find("Multipliers", nStart + 1, -1, RichTextBoxFinds.MatchCase);
                if (nStart == -1) break;
                nEnd = rtbShowResult.Find(Environment.NewLine.ToCharArray(), nStart + 1);
                rtbShowResult.Select(nStart, nEnd - nStart);
                rtbShowResult.SelectionFont = new Font(rtbShowResult.SelectionFont, FontStyle.Underline | FontStyle.Bold);
            }

            // Bold results
            nStart = 0;

            nStart = rtbShowResult.Find("Lifting index:", nStart + 1, -1, RichTextBoxFinds.MatchCase);
            if (nStart > -1)
            {//nEnd = rtbShowResult.Text.Length;
                nEnd = rtbShowResult.Find(Environment.NewLine.ToCharArray(), nStart + 1);
                rtbShowResult.Select(nStart, nEnd - nStart);
                rtbShowResult.SelectionFont = new Font(rtbShowResult.SelectionFont.FontFamily, rtbShowResult.Font.Size + 1, FontStyle.Bold);
            }

            nStart = rtbShowResult.Find("The NIOSH lifting index is:", nStart + 1, -1, RichTextBoxFinds.MatchCase);
            if (nStart > -1)
            {//nEnd = rtbShowResult.Text.Length;
                nEnd = rtbShowResult.Find(Environment.NewLine.ToCharArray(), nStart + 1);
                rtbShowResult.Select(nStart, nEnd - nStart);
                rtbShowResult.SelectionFont = new Font(rtbShowResult.SelectionFont.FontFamily, rtbShowResult.Font.Size + 1, FontStyle.Bold);
            }


            // Set the cursor at the beginning of the text
            rtbShowResult.SelectionStart = 0;
            rtbShowResult.SelectionLength = 0;


            //// Underline
            //string line = rtbShowResult.Lines[2];
            //rtbShowResult.Select(rtbShowResult.Find("Description", 0, RichTextBoxFinds.MatchCase), line.Length);
            ////rtbShowResult.Select(rtbShowResult.GetFirstCharIndexFromLine(2), line.Length);
            //rtbShowResult.SelectionFont = new Font(rtbShowResult.SelectionFont, FontStyle.Underline | FontStyle.Bold);

            //line = rtbShowResult.Lines[9];
            //rtbShowResult.Select(rtbShowResult.Find("Description", rtbShowResult.SelectionStart + 1, RichTextBoxFinds.MatchCase), line.Length);
            ////rtbShowResult.Select(rtbShowResult.GetFirstCharIndexFromLine(9), line.Length);
            //rtbShowResult.SelectionFont = new Font(rtbShowResult.SelectionFont, FontStyle.Underline | FontStyle.Bold);

            //// Bold results
            //line = rtbShowResult.Lines[16];
            //rtbShowResult.Select(rtbShowResult.Find("The", rtbShowResult.SelectionStart + 1, RichTextBoxFinds.MatchCase), line.Length);
            ////rtbShowResult.Select(rtbShowResult.GetFirstCharIndexFromLine(16), line.Length);
            //rtbShowResult.SelectionFont = new Font(rtbShowResult.Font.FontFamily, rtbShowResult.Font.Size + 1, FontStyle.Bold);

            //// Set the cursor at the beginning of the text
            //rtbShowResult.SelectionStart = 0;
            //rtbShowResult.SelectionLength = 0;
        }

        #endregion IChild interface

    }
}
