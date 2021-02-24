using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Text.Json;
using System.Text.Json.Serialization;

// Para llamar a la DLL
using ErgoCalc.DLL.Strain;

namespace ErgoCalc
{
    public partial class frmResultsStrainIndex : Form, IChildResults
    {
        // Variable definition
        //private ModelSubTask[] _subtasks;
        //private ModelTask[] _tasks;
        private ModelJob _job;
        private IndexType _index;
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
            splitContainer1.Panel1Collapsed = false;
            splitContainer1.SplitterDistance = 0;
            splitContainer1.SplitterWidth = 1;
            splitContainer1.IsSplitterFixed = true;

            //ToolStrip botón = ((frmMain)MdiParent).Controls["toolStripMain"] as ToolStrip;
            //ToolStripButton bot = botón.Items["toolStripMain_Settings"] as ToolStripButton;

            //splitContainer1.Panel1Collapsed = ((ToolStrip)((frmMain)MdiParent).Controls["toolStripMain"]).Items["toolStripMain_Settings"].Enabled == true ? false : true;


        }

        public frmResultsStrainIndex(IndexType index, ModelJob job)
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
                _job = _classDLL.Job;
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
                rtbShowResult.Text = _classDLL.ToString();
                CreatePlots();
                FormatText();
            }

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
        private void CreatePlots()
        {
            int nOrder;
            double nCOSI = 0.0;
            double[] dataX;
            double[] dataRSI;
            double[] dataCOSI;
            string[] labels;
            string strTaskText = string.Empty;
            string strPlotTitle = string.Empty;

            for (int j = 0; j < _job.numberTasks; j++)
            {
                dataX = new double[_job.JobTasks[j].numberSubTasks];
                dataRSI = new double[_job.JobTasks[j].numberSubTasks];
                dataCOSI = new double[_job.JobTasks[j].numberSubTasks];
                labels = new string[_job.JobTasks[j].numberSubTasks];

                if (_job.JobTasks[j].index == -1)
                {
                    strTaskText = "Tasks";
                    strPlotTitle = "RSI results";
                }
                else
                {
                    strTaskText = "SubTasks";
                    strPlotTitle = string.Concat("RSI & COSI results for Task ", ((char)('A' + j)).ToString());
                }

                for (int i = 0; i < _job.JobTasks[j].numberSubTasks; i++)
                {

                    if(_job.JobTasks[j].index == -1)
                        nOrder = _job.JobTasks[j].order[i];
                    else
                        nOrder = _job.JobTasks[j].order[_job.JobTasks[j].numberSubTasks - 1 - i];

                    dataX[i] = i + 1;
                    dataRSI[i] = _job.JobTasks[j].SubTasks[nOrder].index;
                    if (i == 0)
                        nCOSI = _job.JobTasks[j].SubTasks[nOrder].index;
                    else
                        nCOSI += _job.JobTasks[j].SubTasks[nOrder].index * (_job.JobTasks[j].SubTasks[nOrder].factors.EMa - _job.JobTasks[j].SubTasks[nOrder].factors.EMb) / _job.JobTasks[j].SubTasks[nOrder].factors.EM;
                    dataCOSI[i] = nCOSI;
                    //labels[i] = string.Concat(strTaskText, ((char)('A' + _job.JobTasks[j].SubTasks[nOrder].ItemIndex)).ToString());
                    labels[i] = ((char)('A' + _job.JobTasks[j].SubTasks[nOrder].ItemIndex)).ToString();
                }

                //var plot = new ScottPlot.Plot(1200, 900);
                var plot = new ScottPlot.Plot(600, 450);
                //var formsplot2 = new ScottPlot.FormsPlot(plot);

                plot.Title(strPlotTitle, fontSize: 22);
                plot.YLabel("Index value", fontSize: 22);
                plot.XLabel(strTaskText.TrimEnd(), fontSize: 22); ;

                plot.PlotBar(dataX, dataRSI, label: "RSI");
                if (_job.JobTasks[j].index != -1)
                    plot.PlotScatter(dataX, dataCOSI, label: "COSI", markerSize: 10, markerShape: ScottPlot.MarkerShape.filledCircle, lineWidth: 5);
                plot.AxisAuto();
                plot.XTicks(dataX, labels);
                plot.Ticks(fontSize: 18);
                plot.Grid(enableVertical: false, lineStyle: ScottPlot.LineStyle.Dot);
                //formsplot2.Render();
                
                //plot.CoordinateFromPixel();
                
                var orgdata = Clipboard.GetDataObject();
                using (var plotImage = plot.GetBitmap())
                {
                    Clipboard.SetImage(plotImage);
                }
                plot.Clear();
                rtbShowResult.AppendText(Environment.NewLine);
                var read = rtbShowResult.ReadOnly;
                rtbShowResult.ReadOnly = false;
                rtbShowResult.SelectionStart = rtbShowResult.Text.Length;
                rtbShowResult.Paste();
                Clipboard.SetDataObject(orgdata);
                rtbShowResult.ReadOnly = read;
                rtbShowResult.SelectionStart = 0;
                string code = rtbShowResult.Rtf;
            }

            if (_job.index != -1)
            {
                dataX = new double[_job.numberTasks];
                dataRSI = new double[_job.numberTasks];
                dataCOSI = new double[_job.numberTasks];
                labels = new string[_job.numberTasks];
                strPlotTitle = string.Empty;

                for (int i = 0; i < _job.numberTasks; i++)
                {
                    nOrder = _job.order[_job.numberTasks - 1 - i];
                    dataX[i] = i + 1;
                    dataRSI[i] = _job.JobTasks[nOrder].index;
                    if (i == 0)
                        nCOSI = _job.JobTasks[nOrder].index;
                    else
                        nCOSI += _job.JobTasks[nOrder].index * (_job.JobTasks[nOrder].HMa - _job.JobTasks[nOrder].HMb) / _job.JobTasks[nOrder].HM;
                    dataCOSI[i] = nCOSI;
                    //labels[i] = string.Concat("Task ", ((char)('A' + nOrder)).ToString());
                    labels[i] = ((char)('A' + nOrder)).ToString();
                    strPlotTitle += string.Concat(((char)('A' + i)).ToString(), " & ");
                }
                strPlotTitle = strPlotTitle.Remove(strPlotTitle.Length - 3, 3);

                var plot = new ScottPlot.Plot(600, 450);
                plot.Title(string.Concat("CUSI results for Tasks ", strPlotTitle), fontSize: 22);
                plot.YLabel("Index value", fontSize: 22);
                plot.XLabel("Tasks", fontSize: 22); ;

                plot.PlotBar(dataX, dataRSI, label: "COSI");
                plot.PlotScatter(dataX, dataCOSI, label: "CUSI", markerSize: 10, markerShape: ScottPlot.MarkerShape.filledCircle, lineWidth: 5);
                plot.AxisAuto();
                plot.XTicks(dataX, labels);
                plot.Ticks(fontSize: 18);
                plot.Grid(enableVertical: false, lineStyle: ScottPlot.LineStyle.Dot);

                var orgdata = Clipboard.GetDataObject();
                using (var plotImage = plot.GetBitmap())
                {
                    Clipboard.SetImage(plotImage);
                }
                plot.Clear();
                rtbShowResult.AppendText(Environment.NewLine);
                var read = rtbShowResult.ReadOnly;
                rtbShowResult.ReadOnly = false;
                rtbShowResult.SelectionStart = rtbShowResult.Text.Length;
                rtbShowResult.Paste();
                Clipboard.SetDataObject(orgdata);
                rtbShowResult.ReadOnly = read;
                rtbShowResult.SelectionStart = 0;

            }

            /*
            // Resize image
            int positionW = 0;
            int positionH = 0;

            positionW = rtbShowResult.Rtf.IndexOf("picwgoal", positionW);
            //if (positionW == -1) break;
            //var algo = rtbShowResult.Rtf.Substring(positionW + 8, 4);
            rtbShowResult.Rtf = rtbShowResult.Rtf.Replace(rtbShowResult.Rtf.Substring(positionW + 8, 4), @"9000");
        
            positionH = rtbShowResult.Rtf.IndexOf("pichgoal", positionH);
            //if (positionH == -1) break;
            rtbShowResult.Rtf = rtbShowResult.Rtf.Replace(rtbShowResult.Rtf.Substring(positionH + 8, 4), @"6750");
            */

            // https://stackoverflow.com/questions/542850/how-can-i-insert-an-image-into-a-richtextbox
        }

        #endregion Private routines

        #region IChildResults
        public void Save(string path)
        {

            // https://devblogs.microsoft.com/dotnet/try-the-new-system-text-json-apis/
            // https://docs.microsoft.com/es-es/dotnet/standard/serialization/system-text-json-how-to?pivots=dotnet-5-0
            // https://docs.microsoft.com/es-es/dotnet/standard/serialization/write-custom-serializer-deserializer
            // https://docs.microsoft.com/es-es/dotnet/api/system.text.json.jsonelement?view=net-5.0
            // https://stackoverflow.com/questions/37665240/howto-serialize-a-nested-collection-using-jsonwriter-in-c-sharp
            var writerOptions = new JsonWriterOptions
            {
                Indented = true
            };
            using FileStream createStream = File.Create("StrainIndex.json");
            using var writer = new Utf8JsonWriter(createStream, options: writerOptions);
            writer.WriteStartObject();

            writer.WritePropertyName("Tasks");
            writer.WriteStartArray();
            //writer.WriteStartObject("Tasks");
            for (int i = 0; i < _job.numberTasks; i++)
            {
                //writer.WriteNumber("Algo", 0);
                writer.WriteStartArray();
                writer.WriteStartObject();
                for (int j=0;j<_job.JobTasks[i].numberSubTasks; j++)
                {
                    writer.WriteStartArray("Sub task");
                    writer.WriteStartObject();
                        writer.WriteNumber("i", _job.JobTasks[i].SubTasks[j].data.i);
                        writer.WriteNumber("e", _job.JobTasks[i].SubTasks[j].data.e);
                        writer.WriteNumber("ea", _job.JobTasks[i].SubTasks[j].data.ea);
                        writer.WriteNumber("eb", _job.JobTasks[i].SubTasks[j].data.eb);
                        writer.WriteNumber("d", _job.JobTasks[i].SubTasks[j].data.d);
                        writer.WriteNumber("p", _job.JobTasks[i].SubTasks[j].data.p);
                        writer.WriteNumber("h", _job.JobTasks[i].SubTasks[j].data.h);
                    writer.WriteEndObject();

                    writer.WriteStartObject();
                        writer.WriteNumber("I multiplier", _job.JobTasks[i].SubTasks[j].factors.IM);
                        writer.WriteNumber("E multiplier", _job.JobTasks[i].SubTasks[j].factors.EM);
                        writer.WriteNumber("Ea multiplier", _job.JobTasks[i].SubTasks[j].factors.EMa);
                        writer.WriteNumber("Eb multiplier", _job.JobTasks[i].SubTasks[j].factors.EMb);
                        writer.WriteNumber("D multiplier", _job.JobTasks[i].SubTasks[j].factors.DM);
                        writer.WriteNumber("P multiplier", _job.JobTasks[i].SubTasks[j].factors.PM);
                        writer.WriteNumber("H multiplier", _job.JobTasks[i].SubTasks[j].factors.HM);
                    writer.WriteEndObject();

                    writer.WriteStartObject();
                    writer.WriteNumber("RSI index", _job.JobTasks[i].SubTasks[j].index);
                    writer.WriteEndObject();

                    writer.WriteStartObject();
                    writer.WriteNumber("Item index", _job.JobTasks[i].SubTasks[j].ItemIndex);
                    writer.WriteEndObject();

                    writer.WriteEndArray();
                }
                writer.WriteEndObject();
                writer.WriteEndArray();


                writer.WriteStartArray();                
                writer.WriteStartObject();
                for (int j = 0; j < _job.JobTasks[i].numberSubTasks; j++)
                {
                    writer.WriteNumber("Order", _job.JobTasks[i].order[j]);
                }
                writer.WriteEndObject();
                writer.WriteEndArray();

                writer.WriteStartObject();
                writer.WriteNumber("h factor", _job.JobTasks[i].h);
                writer.WriteEndObject();
                writer.WriteStartObject();
                writer.WriteNumber("ha factor", _job.JobTasks[i].ha);
                writer.WriteEndObject();
                writer.WriteStartObject();
                writer.WriteNumber("hb factor", _job.JobTasks[i].hb);
                writer.WriteEndObject();
                writer.WriteStartObject();
                writer.WriteNumber("H multiplier", _job.JobTasks[i].HM);
                writer.WriteEndObject();
                writer.WriteStartObject();
                writer.WriteNumber("Ha multiplier", _job.JobTasks[i].HMa);
                writer.WriteEndObject();
                writer.WriteStartObject();
                writer.WriteNumber("Hb multiplier", _job.JobTasks[i].HMb);
                writer.WriteEndObject();
                writer.WriteStartObject();
                writer.WriteNumber("COSI index", _job.JobTasks[i].index);
                writer.WriteEndObject();
                writer.WriteStartObject();
                writer.WriteNumber("Number of sub-tasks", _job.JobTasks[i].numberSubTasks);
                writer.WriteEndObject();
            }
            //writer.WriteEndObject();
            writer.WriteEndArray();

            writer.WriteStartArray("Tasks order");
            for (int i = 0; i < _job.numberTasks; i++)
            {
                writer.WriteNumberValue(_job.order[i]);
            }
            writer.WriteEndArray();

            writer.WriteNumber("CUSI index", _job.index);
            writer.WriteNumber("Number of tasks", _job.numberTasks);


            writer.WriteEndObject();
            writer.Flush();


            //var str = JsonSerializer.Serialize<ModelJob>(_job, options);
            


            // https://msdn.microsoft.com/en-us/library/ms160336(v=vs.110).aspx
            // Displays a SaveFileDialog so the user can save the Image  
            // assigned to Button2.  
            SaveFileDialog saveFileDialog1 = new SaveFileDialog
            {
                DefaultExt = "*.rtf",
                Filter = "RTF file (*.rtf)|*.rtf|Text file (*.txt)|*.txt|All files (*.*)|*.*",
                FilterIndex = 1,
                Title = "Save Strain Index results",
                OverwritePrompt = true,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };

            DialogResult result;
            using (new CenterWinDialog(this))
            { 
                result = saveFileDialog1.ShowDialog();
            }

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

        public void EditData()
        {
            return;
        }

        public void Duplicate()
        {
            string _strPath = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);

            // Mostrar la ventana de resultados
            frmResultsStrainIndex frmResults = new frmResultsStrainIndex(_index,_job)
            {
                MdiParent = this.MdiParent
            };
            if (File.Exists(_strPath + @"\images\logo.ico")) frmResults.Icon = new Icon(_strPath + @"\images\logo.ico");
            frmResults.Show();
        }

        public bool[] GetToolbarEnabledState()
        {
            return new bool[] { true, true, true, false, true, true, false, false, true, false, false, true, true, true };
        }

        public ToolStrip ChildToolStrip
        {
            get => null;
            set { }
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
            return;
        }

        public bool PanelCollapsed ()
        {
            return splitContainer1.SplitterDistance == 0 ? true : false;
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
