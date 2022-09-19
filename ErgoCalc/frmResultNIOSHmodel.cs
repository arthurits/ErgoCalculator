using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Windows.Forms;

using ErgoCalc.Models.Lifting;
using System.Text.Json;

namespace ErgoCalc
{
    public partial class frmResultNIOSHmodel : Form, IChildResults
    {
        // Variable definition
        //public ClassData data { get; set; } 
        //private List<SubTask> _sDatosNIOSH;
        //private cModelNIOSH _classNIOSH;
        private ResultsOptions _options;
        //private bool _composite;
        private Job _job;

        public frmResultNIOSHmodel()
        {
            // VS designer initialization
            InitializeComponent();

            // Set private variable default values            
            //_classNIOSH = new cModelNIOSH();
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

        public frmResultNIOSHmodel(Job Data)
            : this()
        {
            _job = Data;

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
            // Make computations
            if (_job.model == IndexType.IndexLI)
            {
                foreach (Task task in _job.jobTasks)
                {
                    NIOSHLifting.ComputeLI(task.subTasks);
                }
            }
            else if (_job.model == IndexType.IndexCLI)
            {
                foreach (Task task in _job.jobTasks)
                {
                    NIOSHLifting.ComputeCLI(task);
                }
            }

            // Show results
            rtbShowResult.Text = _job.ToString();
            FormatText();

            //// Variable definition
            ////Int32 nSize = _sDatosNIOSH.Count;
            //Int32 nSize = 0;
            //Int32[] orden = new Int32[nSize];
            //Boolean error = false;
            //Double resultado = 0.0;

            //if (nSize == 0) return;

            //for (Int32 i = 0; i < nSize; i++) orden[i] = i;

            //// Call the DLL function
            //try
            //{
            //    resultado = _classNIOSH.CalculateNIOSH(_sDatosNIOSH, orden, ref nSize);
            //}
            //catch (EntryPointNotFoundException)
            //{
            //    error = true;
            //    MessageBox.Show(
            //        "The program calculation kernel's been tampered with.\nThe NIOSH index could not be computed.",
            //        "NIOSH index error",
            //        MessageBoxButtons.OK,
            //        MessageBoxIcon.Error);
            //    this.Close();
            //}
            //catch (DllNotFoundException)
            //{
            //    error = true;
            //    MessageBox.Show(
            //        "Some files are missing. Please\nreinstall the application.",
            //        "NIOSH index error",
            //        MessageBoxButtons.OK,
            //        MessageBoxIcon.Error);
            //    this.Close();
            //}
            //catch (Exception ex)
            //{
            //    error = true;
            //    MessageBox.Show(
            //        "Error in the calculation kernel:\n" + ex.ToString(),
            //        "Unexpected error",
            //        MessageBoxButtons.OK,
            //        MessageBoxIcon.Error);
            //    this.Close();
            //}

            //// Call the routine that shows the results
            ////if (error == false) ResultsToRichText(_sDatosNIOSH, orden, resultado, _composite);
            //if (error == false)
            //{
            //    rtbShowResult.Text = _classNIOSH.ToString();
            //    FormatText();
            //}

        }

        /// <summary>
        /// Serialize ModelJob structure to JSON
        /// </summary>
        /// <param name="writer">The already created writer</param>
        private void SerializeToJSON(Utf8JsonWriter writer)
        {
            writer.WriteStartObject();
            writer.WriteString("Document type", "NIOSH lifting equation");

            writer.WriteNumber("Model", (int)_job.model);
            writer.WriteNumber("Index", _job.index);
            writer.WriteNumber("Number of tasks", _job.numberTasks);

            writer.WritePropertyName("Tasks order");
            writer.WriteStartArray();
            for (int j = 0; j < _job.numberTasks; j++)
                writer.WriteNumberValue(_job.order[j]);
            writer.WriteEndArray();

            writer.WritePropertyName("Tasks");
            writer.WriteStartArray();
            for (int i = 0; i < _job.numberTasks; i++)
            {
                writer.WriteStartObject();

                writer.WriteNumber("Model", (int)_job.jobTasks[i].model);
                writer.WriteNumber("CLI", _job.jobTasks[i].CLI);
                writer.WriteNumber("Number of sub-tasks", _job.jobTasks[i].numberSubTasks);

                writer.WritePropertyName("Sub-tasks order");
                writer.WriteStartArray();
                for (int j = 0; j < _job.jobTasks[i].numberSubTasks; j++)
                    writer.WriteNumberValue(_job.jobTasks[i].OrderCLI[j]);
                writer.WriteEndArray();

                writer.WritePropertyName("Sub-tasks");
                writer.WriteStartArray();
                for (int j = 0; j < _job.jobTasks[i].numberSubTasks; j++)
                {
                    writer.WriteStartObject();
                    writer.WriteNumber("Weight", _job.jobTasks[i].subTasks[j].data.weight);
                    writer.WriteNumber("Horizontal distance", _job.jobTasks[i].subTasks[j].data.h);
                    writer.WriteNumber("Vertical distance", _job.jobTasks[i].subTasks[j].data.v);
                    writer.WriteNumber("Distance", _job.jobTasks[i].subTasks[j].data.d);
                    writer.WriteNumber("Asymmetry angle", _job.jobTasks[i].subTasks[j].data.a);
                    writer.WriteNumber("Frequency", _job.jobTasks[i].subTasks[j].data.f);
                    writer.WriteNumber("Frequency (a)", _job.jobTasks[i].subTasks[j].data.fa);
                    writer.WriteNumber("Frequency (b)", _job.jobTasks[i].subTasks[j].data.fb);
                    writer.WriteNumber("Subtask duration", _job.jobTasks[i].subTasks[j].data.td);
                    writer.WriteNumber("Coupling", (int)_job.jobTasks[i].subTasks[j].data.c);

                    writer.WriteNumber("Load constant", _job.jobTasks[i].subTasks[j].factors.LC);
                    writer.WriteNumber("H multiplier", _job.jobTasks[i].subTasks[j].factors.HM);
                    writer.WriteNumber("V multiplier", _job.jobTasks[i].subTasks[j].factors.VM);
                    writer.WriteNumber("D multiplier", _job.jobTasks[i].subTasks[j].factors.DM);
                    writer.WriteNumber("A multiplier", _job.jobTasks[i].subTasks[j].factors.AM);
                    writer.WriteNumber("F multiplier", _job.jobTasks[i].subTasks[j].factors.FM);
                    writer.WriteNumber("Fa multiplier", _job.jobTasks[i].subTasks[j].factors.FMa);
                    writer.WriteNumber("Fb multiplier", _job.jobTasks[i].subTasks[j].factors.FMb);
                    writer.WriteNumber("C multiplier", _job.jobTasks[i].subTasks[j].factors.CM);

                    writer.WriteNumber("LI index", _job.jobTasks[i].subTasks[j].indexLI);
                    writer.WriteNumber("IF index", _job.jobTasks[i].subTasks[j].indexIF);
                    writer.WriteNumber("Item index", _job.jobTasks[i].subTasks[j].itemIndex);
                    writer.WriteNumber("Task", _job.jobTasks[i].subTasks[j].task);
                    writer.WriteNumber("Order", _job.jobTasks[i].subTasks[j].order);
                    writer.WriteEndObject();
                }
                writer.WriteEndArray();

                writer.WriteEndObject();
            }
            writer.WriteEndArray();

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
            SaveFileDialog SaveDlg = new()
            {
                DefaultExt = "*.txt",
                Filter = "ERGO file (*.ergo)|*.ergo|RTF file (*.rtf)|*.rtf|Text file (*.txt)|*.txt|All files (*.*)|*.*",
                FilterIndex = 1,
                FileName = _job.model switch
                {
                    IndexType.IndexLI => "LI results",
                    IndexType.IndexCLI => "CLI results",
                    _ => "Results",
                },
                Title = "Save NIOSH model results",
                OverwritePrompt = true,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };

            DialogResult result;
            using (new CenterWinDialog(this.MdiParent))
            {
                result = SaveDlg.ShowDialog(this);
            }

            // If the file name is not an empty string open it for saving.  
            if (result == DialogResult.OK && SaveDlg.FileName != "")
            {
                System.IO.FileStream fs;

                // Saves the text via a FileStream created by the OpenFile method.  
                if ((fs = (System.IO.FileStream)SaveDlg.OpenFile()) != null)
                {
                    // Saves the text in the appropriate TextFormat based upon the File type selected in the dialog box.  
                    // NOTE that the FilterIndex property is one-based. 
                    switch (SaveDlg.FilterIndex)
                    {
                        case 1:
                            using (var writer = new Utf8JsonWriter(fs, options: new JsonWriterOptions { Indented = true }))
                                SerializeToJSON(writer);
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
            bool result = true;
            Job job = new();
            JsonElement root = document.RootElement;

            try
            {
                job.model = (IndexType)root.GetProperty("Model").GetInt32();
                job.index = root.GetProperty("Index").GetDouble();
                job.numberTasks = root.GetProperty("Number of tasks").GetInt32();

                job.order = new int[job.numberTasks];
                int i = 0;
                foreach (JsonElement TaskOrder in root.GetProperty("Tasks order").EnumerateArray())
                {
                    job.order[i] = TaskOrder.GetInt32();
                    i++;
                }

                job.jobTasks = new Task[job.numberTasks];
                i = 0;
                JsonElement SubTasks;
                JsonElement Order;
                foreach (JsonElement Task in root.GetProperty("Tasks").EnumerateArray())
                {
                    job.jobTasks[i] = new();
                    job.jobTasks[i].model = (IndexType)Task.GetProperty("Model").GetInt32();
                    job.jobTasks[i].CLI = Task.GetProperty("CLI").GetDouble();
                    job.jobTasks[i].numberSubTasks = Task.GetProperty("Number of sub-tasks").GetInt32();
                    job.jobTasks[i].subTasks = new SubTask[job.jobTasks[i].numberSubTasks];
                    job.jobTasks[i].OrderCLI = new int[job.jobTasks[i].numberSubTasks];
                    
                    Order = Task.GetProperty("Sub-tasks order");
                    for (int j = 0; j < job.jobTasks[i].numberSubTasks; j++)
                        job.jobTasks[i].OrderCLI[j] = Order[j].GetInt32();

                    SubTasks = Task.GetProperty("Sub-tasks");
                    for (int j = 0; j < job.jobTasks[i].numberSubTasks; j++)
                    {
                        job.jobTasks[i].subTasks[j] = new();
                        job.jobTasks[i].subTasks[j].data.weight = SubTasks[j].GetProperty("Weight").GetDouble();
                        job.jobTasks[i].subTasks[j].data.h = SubTasks[j].GetProperty("Horizontal distance").GetDouble();
                        job.jobTasks[i].subTasks[j].data.v = SubTasks[j].GetProperty("Vertical distance").GetDouble();
                        job.jobTasks[i].subTasks[j].data.d = SubTasks[j].GetProperty("Distance").GetDouble();
                        job.jobTasks[i].subTasks[j].data.a = SubTasks[j].GetProperty("Asymmetry angle").GetDouble();
                        job.jobTasks[i].subTasks[j].data.f = SubTasks[j].GetProperty("Frequency").GetDouble();
                        job.jobTasks[i].subTasks[j].data.fa = SubTasks[j].GetProperty("Frequency (a)").GetDouble();
                        job.jobTasks[i].subTasks[j].data.fb = SubTasks[j].GetProperty("Frequency (b)").GetDouble();
                        job.jobTasks[i].subTasks[j].data.td = SubTasks[j].GetProperty("Subtask duration").GetDouble();
                        job.jobTasks[i].subTasks[j].data.c = (Coupling)SubTasks[j].GetProperty("Coupling").GetInt32();
                        
                        job.jobTasks[i].subTasks[j].factors.LC = SubTasks[j].GetProperty("Load constant").GetDouble();
                        job.jobTasks[i].subTasks[j].factors.HM = SubTasks[j].GetProperty("H multiplier").GetDouble();
                        job.jobTasks[i].subTasks[j].factors.VM = SubTasks[j].GetProperty("V multiplier").GetDouble();
                        job.jobTasks[i].subTasks[j].factors.DM = SubTasks[j].GetProperty("D multiplier").GetDouble();
                        job.jobTasks[i].subTasks[j].factors.AM = SubTasks[j].GetProperty("A multiplier").GetDouble();
                        job.jobTasks[i].subTasks[j].factors.FM = SubTasks[j].GetProperty("F multiplier").GetDouble();
                        job.jobTasks[i].subTasks[j].factors.FMa = SubTasks[j].GetProperty("Fa multiplier").GetDouble();
                        job.jobTasks[i].subTasks[j].factors.FMb = SubTasks[j].GetProperty("Fb multiplier").GetDouble();
                        job.jobTasks[i].subTasks[j].factors.CM = SubTasks[j].GetProperty("C multiplier").GetDouble();

                        job.jobTasks[i].subTasks[j].indexLI = SubTasks[j].GetProperty("LI index").GetDouble();
                        job.jobTasks[i].subTasks[j].indexIF = SubTasks[j].GetProperty("IF index").GetDouble();
                        job.jobTasks[i].subTasks[j].itemIndex = SubTasks[j].GetProperty("Item index").GetInt32();
                        job.jobTasks[i].subTasks[j].task = SubTasks[j].GetProperty("Task").GetInt32();
                        job.jobTasks[i].subTasks[j].order = SubTasks[j].GetProperty("Order").GetInt32();
                    }

                    i++;
                }

                _job = job;
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }

        public void EditData()
        {
            using var frm = new frmDataNIOSHmodel(_job);

            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                _job = frm.GetNioshLifting;
                ShowResults();
            }
            return;
        }

        public void Duplicate()
        {
            string _strPath = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);

            // Mostrar la ventana de resultados
            frmResultNIOSHmodel frmResults = new frmResultNIOSHmodel(_job)
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

            while (true)
            {
                nStart = rtbShowResult.Find("The NIOSH lifting index is:", nStart + 1, -1, RichTextBoxFinds.MatchCase);
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

        #endregion IChild interface

    }
}
