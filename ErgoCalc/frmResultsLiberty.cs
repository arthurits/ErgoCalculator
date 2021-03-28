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

using ScottPlot;
using ErgoCalc.Models.LibertyMutual;

namespace ErgoCalc
{
    public partial class frmResultsLiberty : Form, IChildResults
    {
        private List<ModelLiberty> _data;
        private CLibertyMutual _modelLM;
        private readonly string _strPath;

        public ToolStrip ChildToolStrip { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public frmResultsLiberty()
        {
            _strPath = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            if (File.Exists(_strPath + @"\images\logo.ico")) this.Icon = new Icon(_strPath + @"\images\logo.ico");

            InitializeComponent();
            propertyGrid1.SelectedObject = new ResultsOptions(rtbShowResult);
            splitContainer1.Panel1Collapsed = false;
            splitContainer1.SplitterDistance = 0;
            splitContainer1.SplitterWidth = 1;
            splitContainer1.IsSplitterFixed = true;

            InitializePlot();

            // Initialize private variables
        }


        public frmResultsLiberty(object data)
            : this()
        {
            _data = (List<ModelLiberty>)data;
        }

        private void frmResultsLiberty_Shown(object sender, EventArgs e)
        {
            ShowResults();
        }

        #region Private routines
        private void InitializePlot()
        {
            formsPlot1.plt.XLabel("Maximum limit (kg)");
            formsPlot1.plt.YLabel("Frequency?");
        }

        private void ShowResults()
        {
            Boolean error = false;

            _modelLM = new CLibertyMutual(_data);
            // Call the DLL function
            try
            {
                _modelLM.ComputeMMH();
                _data = _modelLM.GetData;
            }
            catch (EntryPointNotFoundException)
            {
                error = true;
                MessageBox.Show(
                    "The program calculation kernel's been tampered with.\nLM-MMH could not be computed.",
                    "LM-MMH error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (DllNotFoundException)
            {
                error = true;
                MessageBox.Show(
                    "DLL files are missing. Please\nreinstall the application.",
                    "LM-MMH error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                error = true;
                MessageBox.Show(
                    "Error in the LM-MMH calculation kernel:\n" + ex.ToString(),
                    "Unexpected error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

            // Call the routine that shows the results
            if (error == false)
            {
                rtbShowResult.Text = _modelLM.ToString();
                FormatText();
                CreatePlots();
            }
        }

        private void CreatePlots()
        {
            var mean = _data[0].results.Weight;
            var std = mean * _data[0].results.IniCoeffV;

            Random rand = new Random(0);
            double[] values = DataGen.RandomNormal(rand, pointCount: 1000, mean: mean, stdDev: std);

            // create a Population object from the data
            var pop = new ScottPlot.Statistics.Population(values);

            var hist = new ScottPlot.Statistics.Histogram(values, min: mean - 3 * std, max: mean + 3 * std);
            double[] curveXs = DataGen.Range(pop.minus3stDev, pop.plus3stDev, .1);
            double[] curveYs = pop.GetDistribution(curveXs, normalize: false);



            formsPlot1.plt.Legend(backColor: Color.Transparent, frameColor: Color.Transparent, location: legendLocation.upperRight, shadowDirection: shadowDirection.none);
            formsPlot1.plt.Colorset(ScottPlot.Drawing.Colorset.Nord);
            formsPlot1.plt.PlotScatter(curveXs, curveYs, markerSize: 0, lineWidth: 2, label: "Population");
            //formsPlot1.plt.PlotScatter(hist.bins, hist.countsFracCurve, markerSize: 0, lineWidth: 2, label: "Histogram");
            
            var limit75 = mean - std * 0.674489750196082;
            var limit90 = mean - std * 1.281551565544601;

            formsPlot1.plt.PlotVLine(x: limit75, label: "75%", color: Color.DarkGray, lineWidth: 1.2, lineStyle: LineStyle.Solid);
            formsPlot1.plt.PlotVLine(x: limit90, label: "90%", color: Color.Gray, lineWidth: 1.2, lineStyle: LineStyle.Solid);

            formsPlot1.Render();

            return;
        }

        private void SerializeToJSON(Utf8JsonWriter writer)
        {
            writer.WriteStartObject();
            writer.WriteString("Document type", "LM-MMH model");

            writer.WritePropertyName("Cases");
            writer.WriteStartArray();

            foreach (var data in _data)
            {
                writer.WriteStartObject();

                writer.WritePropertyName("Data");
                writer.WriteStartObject();
                writer.WriteNumber("Type", (int)data.type);
                writer.WriteNumber("Horizontal reach (m)", data.data.HorzReach);
                writer.WriteNumber("Vertical range middle (m)", data.data.VertRangeM);
                writer.WriteNumber("Horizontal distance (m)", data.data.DistHorz);
                writer.WriteNumber("Vertical distance (m)", data.data.DistVert);
                writer.WriteNumber("Vertical height (m)", data.data.VertHeight);
                writer.WriteNumber("Frequency (actions/min)", data.data.Freq);
                writer.WriteNumber("Gender", (int)data.gender);
                writer.WriteEndObject();

                writer.WritePropertyName("Results");
                writer.WriteStartObject();
                writer.WriteNumber("Coefficient of variation initial", data.results.IniCoeffV);
                writer.WriteNumber("Coefficient of variation sustained", data.results.SusCoeffV);
                writer.WriteNumber("Initial force (kg)", data.results.IniForce);
                writer.WriteNumber("Sustained force (kg)", data.results.SusForce);
                writer.WriteNumber("Weight (kg)", data.results.Weight);
                writer.WriteEndObject();

                writer.WriteEndObject();
            }

            writer.WriteEndArray();
            writer.WriteEndObject();

            writer.Flush();
        }

        #endregion Private routines

        #region IChildResults inferface
        public void Save(string path)
        {
            // Displays a SaveFileDialog so the user can save the Image  
            SaveFileDialog SaveDlg = new SaveFileDialog
            {
                DefaultExt = "*.csv",
                Filter = "ERGO file (*.ergo)|*.ergo|RTF file (*.rtf)|*.rtf|Text file (*.txt)|*.txt|All files (*.*)|*.*",
                FilterIndex = 1,
                Title = "Save LM-MMH data",
                OverwritePrompt = true,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };

            DialogResult result;
            using (new CenterWinDialog(this))
            {
                result = SaveDlg.ShowDialog(this.Parent);
            }

            // If the file name is not an empty string open it for saving.  
            if (result == DialogResult.OK && SaveDlg.FileName != "")
            {
                using var fs = SaveDlg.OpenFile();

                switch (SaveDlg.FilterIndex)
                {
                    case 1:
                        if (fs != null)
                        {
                            using var writer = new Utf8JsonWriter(fs, options: new JsonWriterOptions { Indented = true });
                            SerializeToJSON(writer);
                            //var jsonString = JsonSerializer.Serialize(_datos[0]._points[0], new JsonSerializerOptions { WriteIndented = true });
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

                using (new CenterWinDialog(this.MdiParent))
                {
                    MessageBox.Show(this, "The file was successfully saved", "File saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        public bool OpenFile(JsonDocument document)
        {
            bool result = true;
            ModelLiberty data = new ModelLiberty();
            _data = new List<ModelLiberty>();
            JsonElement root = document.RootElement;

            try
            {
                foreach (JsonElement curve in root.GetProperty("Cases").EnumerateArray())
                {
                    // https://stackoverflow.com/questions/29482/how-can-i-cast-int-to-enum
                    // safe, as it explicitly specifies supported values
                    // unsafe: Enum.IsDefined(typeof(MyEnum), value)
                    var value = curve.GetProperty("Data").GetProperty("Type").GetInt32();
                    if ((new[] { MNType.Carrying, MNType.Lifting, MNType.Lowering, MNType.Pulling, MNType.Pushing }).Contains((MNType)value))
                        data.type = (MNType)value;
                    else
                        data.type = MNType.Carrying;

                    data.data.HorzReach = curve.GetProperty("Data").GetProperty("Horizontal reach (m)").GetDouble();
                    data.data.VertRangeM = curve.GetProperty("Data").GetProperty("Vertical range middle (m)").GetDouble();
                    data.data.DistHorz = curve.GetProperty("Data").GetProperty("Horizontal distance (m)").GetDouble();
                    data.data.DistVert = curve.GetProperty("Data").GetProperty("Vertical distance (m)").GetDouble();
                    data.data.VertHeight = curve.GetProperty("Data").GetProperty("Vertical height (m)").GetDouble();
                    data.data.Freq = curve.GetProperty("Data").GetProperty("Frequency (actions/min)").GetDouble();
                    
                    value = curve.GetProperty("Data").GetProperty("Gender").GetInt32();
                    if ((new[] { MNGender.Male, MNGender.Female }).Contains((MNGender)value))
                        data.gender = (MNGender)value;
                    else
                        data.gender = MNGender.Male;

                    data.results.IniCoeffV = curve.GetProperty("Results").GetProperty("Coefficient of variation initial").GetDouble();
                    data.results.SusCoeffV = curve.GetProperty("Results").GetProperty("Coefficient of variation sustained").GetDouble();
                    data.results.IniForce = curve.GetProperty("Results").GetProperty("Initial force (kg)").GetDouble();
                    data.results.SusForce = curve.GetProperty("Results").GetProperty("Sustained force (kg)").GetDouble();
                    data.results.Weight = curve.GetProperty("Results").GetProperty("Weight (kg)").GetDouble();

                    _data.Add(data);
                }
            }
            catch (Exception)
            {
                result = false;
            }

            if (result)
            {
                //CalcularCurva();
                //PlotCurves();
                //_chartOptions.NúmeroCurva = chart.plt.GetPlottables().Count - 1;
            }

            return result;
        }

        public bool[] GetToolbarEnabledState()
        {
            return new bool[] { true, true, true, false, true, true, false, true, true, false, false, true, true, true }; ;
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
            return; ;
        }

        public bool PanelCollapsed()
        {
            return splitContainer1.SplitterDistance == 0 ? true : false;
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

                // Underline
                nStart = rtbShowResult.Find("Intermediate results", nStart + 1, -1, RichTextBoxFinds.MatchCase);
                if (nStart == -1) break;
                nEnd = rtbShowResult.Find(Environment.NewLine.ToCharArray(), nStart + 1);
                rtbShowResult.Select(nStart, nEnd - nStart);
                rtbShowResult.SelectionFont = new Font(rtbShowResult.SelectionFont, FontStyle.Underline | FontStyle.Bold);
            }

            // Bold results
            nStart = 0;
            while (true)
            {
                nStart = rtbShowResult.Find("Maximum acceptable load", nStart + 1, -1, RichTextBoxFinds.MatchCase);
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

        public void EditData()
        {
            using var frm = new frmDataLiberty(_data);

            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                _data = (List<ModelLiberty>)frm.GetData;
                ShowResults();
            }
        }

        public void Duplicate()
        {
            throw new NotImplementedException();
        }
        #endregion IChildResults inferface
    }
}
