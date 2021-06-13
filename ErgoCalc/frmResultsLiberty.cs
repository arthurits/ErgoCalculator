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

            //this.rtbShowResult.Font = new Font(FontFamily.GenericMonospace, rtbShowResult.Font.Size);
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
            formsPlot1.Plot.XLabel("Initial force / kg-f");
            //formsPlot1.plt.YLabel("Frequency?");
            //formsPlot1.plt.Legend(backColor: Color.Transparent, frameColor: Color.Transparent, location: legendLocation.upperRight, shadowDirection: shadowDirection.none);
            formsPlot1.Plot.Palette = ScottPlot.Drawing.Palette.Nord;
            //formsPlot1.plt.TightenLayout(padding: 0);

            formsPlot2.Plot.XLabel("Sustained force / kg-f");
            //formsPlot2.plt.YLabel("Frequency?");
            //formsPlot2.plt.Legend(backColor: Color.Transparent, frameColor: Color.Transparent, location: legendLocation.upperRight, shadowDirection: shadowDirection.none);
            formsPlot2.Plot.Palette = ScottPlot.Drawing.Palette.Nord;
            //formsPlot2.plt.TightenLayout(padding: 0, render: true);

            formsPlot3.Plot.XLabel("Weight / kg");
            //formsPlot3.plt.YLabel("Frequency?");
            //formsPlot3.plt.Legend(backColor: Color.Transparent, frameColor: Color.Transparent, location: legendLocation.upperRight, shadowDirection: shadowDirection.none);
            formsPlot3.Plot.Palette = ScottPlot.Drawing.Palette.Nord;
            //formsPlot3.plt.TightenLayout(padding: 0);
            //formsPlot3.plt.AxisAutoY();
            //formsPlot3.plt.Axis(y1: 0, y2: 1);
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

        /// <summary>
        /// Clears all plottables
        /// </summary>
        private void ClearPlots()
        {
            formsPlot1.Plot.Clear();
            formsPlot2.Plot.Clear();
            formsPlot3.Plot.Clear();
        }

        /// <summary>
        /// Manages the drawing of the 3 plots and the corresponding legends
        /// </summary>
        private void CreatePlots()
        {
            string strLegend;
            int i = 0;
            foreach (var data in _data)
            {
                strLegend = "Task " + ((char)('A' + i)).ToString();
                switch (data.data.type)
                {
                    case MNType.Pulling:
                    case MNType.Pushing:
                        CreatePlot(data.Initial.MAL, data.Initial.MAL * data.Initial.CV, 1, strLegend);        // Initial force plot
                        CreatePlot(data.Sustained.MAL, data.Sustained.MAL * data.Sustained.CV, 2, strLegend);    // Sustained force plot
                        break;
                    case MNType.Carrying:
                    case MNType.Lifting:
                    case MNType.Lowering:
                        CreatePlot(data.Initial.MAL, data.Initial.MAL * data.Initial.CV, 3, strLegend);        // Weight plot
                        break;
                }
                ++i;
            }
            return;
        }

        /// <summary>
        /// Draws a gaussian distribution curve as well as the lower 90 percentile and the lower 75 percentile
        /// </summary>
        /// <param name="mean">Mean</param>
        /// <param name="std">Standard deviation</param>
        /// <param name="nPlot">Number of plot control: 1 for Initial force, 2 for sustained force, and 3 for weight</param>
        /// <param name="strLegend">Text to show in the legend</param>
        private void CreatePlot(double mean, double std, int nPlot, string strLegend = null)
        {
            Random rand = new Random(0);
            double[] values = DataGen.RandomNormal(rand, pointCount: 1000, mean: mean, stdDev: std);

            // create a Population object from the data
            var pop = new ScottPlot.Statistics.Population(values);

            var hist = new ScottPlot.Statistics.Histogram(values, min: mean - 3 * std, max: mean + 3 * std);
            double[] curveXs = DataGen.Range(pop.minus3stDev, pop.plus3stDev, .1);
            double[] curveYs = pop.GetDistribution(curveXs, normalize: false);

            ScottPlot.FormsPlot plot = null;
            PictureBox pctLegend = null;
            switch (nPlot)
            {
                case 1:
                    plot = formsPlot1;  // Initial force plot
                    pctLegend = pictureBox1;
                    break;
                case 2:
                    plot = formsPlot2;  // Sustained force plot
                    pctLegend = pictureBox2;
                    break;
                case 3:
                    plot = formsPlot3;  // Weight plot
                    pctLegend = pictureBox3;
                    break;
            }

            //plot.plt.Legend(backColor: Color.Transparent, frameColor: Color.Transparent, location: legendLocation.upperRight, shadowDirection: shadowDirection.none);
            plot.Plot.Palette = ScottPlot.Drawing.Palette.Nord;
            plot.Plot.AddScatter(curveXs, curveYs, markerSize: 0, lineWidth: 2, label: strLegend);
            //formsPlot1.plt.PlotScatter(hist.bins, hist.countsFracCurve, markerSize: 0, lineWidth: 2, label: "Histogram");

            var limit75 = mean - std * 0.674489750196082;
            var limit90 = mean - std * 1.281551565544601;

            plot.Plot.AddVerticalLine(x: limit75, color: Color.DarkGray, width: 1.2f, style: LineStyle.Solid).Label = "75%";
            plot.Plot.AddVerticalLine(x: limit90, color: Color.Gray, width: 1.2f, style: LineStyle.Solid).Label = "90%";
            plot.Plot.SetAxisLimits(yMin: 0, yMax: null);
            plot.Plot.AxisAutoX();
            //plot.Plot.TightenLayout(padding: 1);
            //plot.Plot.Frameless();
            plot.Render();

            var legendA = plot.Plot.RenderLegend();
            var bitmapA = new Bitmap(legendA.Width + 2, legendA.Height + 2);
            using Graphics GraphicsA = Graphics.FromImage(bitmapA);
            GraphicsA.DrawRectangle(new Pen(Color.Black),
                                    0,
                                    0,
                                    legendA.Width + 1,
                                    legendA.Height + 1);
            GraphicsA.DrawImage(legendA, 1, 1);

            pctLegend.Image = bitmapA;
        }

        /// <summary>
        /// Saves and commits data into a JSON structured file
        /// </summary>
        /// <param name="writer">The file writer abstraction. It's created and disposed by the caller (the Save function)</param>
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
                writer.WriteNumber("Type", (int)data.data.type);
                writer.WriteNumber("Horizontal reach (m)", data.data.HorzReach);
                writer.WriteNumber("Vertical range middle (m)", data.data.VertRangeM);
                writer.WriteNumber("Horizontal distance (m)", data.data.DistHorz);
                writer.WriteNumber("Vertical distance (m)", data.data.DistVert);
                writer.WriteNumber("Vertical height (m)", data.data.VertHeight);
                writer.WriteNumber("Frequency (actions/min)", data.data.Freq);
                writer.WriteNumber("Gender", (int)data.data.gender);
                writer.WriteEndObject();
                
                writer.WritePropertyName("Scale factors initial");
                writer.WriteStartObject();
                writer.WriteNumber("Reference load (RF)", data.Initial.RF);
                writer.WriteNumber("Horizontal reach factor (H)", data.Initial.H);
                writer.WriteNumber("Vertical range middle factor (VRM)", data.Initial.VRM);
                writer.WriteNumber("Horizontal travel distance factor (DH)", data.Initial.DH);
                writer.WriteNumber("Vertical travel distance factor (DV)", data.Initial.DV);
                writer.WriteNumber("Vertical height factor (V)", data.Initial.V);
                writer.WriteNumber("Frequency factor (F)", data.Initial.F);
                writer.WriteNumber("Coefficient of variation (CV)", data.Initial.CV);
                writer.WriteNumber("MAL — Maximum acceptable load — Mean (kg or kg-f)", data.Initial.MAL);
                writer.WriteNumber("MAL — Maximum acceptable load — 75% (kg or kg-f)", data.Initial.MAL75);
                writer.WriteNumber("MAL — Maximum acceptable load — 90% (kg or kg-f)", data.Initial.MAL90);
                writer.WriteEndObject();

                writer.WritePropertyName("Scale factors sustained");
                writer.WriteStartObject();
                writer.WriteNumber("Reference load (RF)", data.Sustained.RF);
                writer.WriteNumber("Horizontal reach factor (H)", data.Sustained.H);
                writer.WriteNumber("Vertical range middle factor (VRM)", data.Sustained.VRM);
                writer.WriteNumber("Horizontal travel distance factor (DH)", data.Sustained.DH);
                writer.WriteNumber("Vertical travel distance factor (DV)", data.Sustained.DV);
                writer.WriteNumber("Vertical height factor (V)", data.Sustained.V);
                writer.WriteNumber("Frequency factor (F)", data.Sustained.F);
                writer.WriteNumber("Coefficient of variation (CV)", data.Sustained.CV);
                writer.WriteNumber("MAL — Maximum acceptable load — Mean (kg or kg-f)", data.Sustained.MAL);
                writer.WriteNumber("MAL — Maximum acceptable load — 75% (kg or kg-f)", data.Sustained.MAL75);
                writer.WriteNumber("MAL — Maximum acceptable load — 90% (kg or kg-f)", data.Sustained.MAL90);
                writer.WriteEndObject();

                //writer.WritePropertyName("Results");
                //writer.WriteStartObject();
                //writer.WriteNumber("Coefficient of variation initial", data.results.IniCoeffV);
                //writer.WriteNumber("Coefficient of variation sustained", data.results.SusCoeffV);
                //writer.WriteNumber("Initial force (kg)", data.results.IniForce);
                //writer.WriteNumber("Sustained force (kg)", data.results.SusForce);
                //writer.WriteNumber("Weight (kg)", data.results.Weight);
                //writer.WriteEndObject();

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
                        data.data.type = (MNType)value;
                    else
                        data.data.type = MNType.Carrying;

                    data.data.HorzReach = curve.GetProperty("Data").GetProperty("Horizontal reach (m)").GetDouble();
                    data.data.VertRangeM = curve.GetProperty("Data").GetProperty("Vertical range middle (m)").GetDouble();
                    data.data.DistHorz = curve.GetProperty("Data").GetProperty("Horizontal distance (m)").GetDouble();
                    data.data.DistVert = curve.GetProperty("Data").GetProperty("Vertical distance (m)").GetDouble();
                    data.data.VertHeight = curve.GetProperty("Data").GetProperty("Vertical height (m)").GetDouble();
                    data.data.Freq = curve.GetProperty("Data").GetProperty("Frequency (actions/min)").GetDouble();
                    
                    value = curve.GetProperty("Data").GetProperty("Gender").GetInt32();
                    if ((new[] { MNGender.Male, MNGender.Female }).Contains((MNGender)value))
                        data.data.gender = (MNGender)value;
                    else
                        data.data.gender = MNGender.Male;

                    data.Initial.RF = curve.GetProperty("Scale factors initial").GetProperty("Reference load (RF)").GetDouble();
                    data.Initial.H = curve.GetProperty("Scale factors initial").GetProperty("Horizontal reach factor (H)").GetDouble();
                    data.Initial.VRM = curve.GetProperty("Scale factors initial").GetProperty("Vertical range middle factor (VRM)").GetDouble();
                    data.Initial.DH = curve.GetProperty("Scale factors initial").GetProperty("Horizontal travel distance factor (DH)").GetDouble();
                    data.Initial.DV = curve.GetProperty("Scale factors initial").GetProperty("Vertical travel distance factor (DV)").GetDouble();
                    data.Initial.V = curve.GetProperty("Scale factors initial").GetProperty("Vertical height factor (V)").GetDouble();
                    data.Initial.F = curve.GetProperty("Scale factors initial").GetProperty("Frequency factor (F)").GetDouble();
                    data.Initial.CV = curve.GetProperty("Scale factors initial").GetProperty("Coefficient of variation (CV)").GetDouble();
                    data.Initial.MAL = curve.GetProperty("Scale factors initial").GetProperty("MAL — Maximum acceptable load — Mean (kg or kg-f)").GetDouble();
                    data.Initial.MAL75 = curve.GetProperty("Scale factors initial").GetProperty("MAL — Maximum acceptable load — 75% (kg or kg-f)").GetDouble();
                    data.Initial.MAL90 = curve.GetProperty("Scale factors initial").GetProperty("MAL — Maximum acceptable load — 90% (kg or kg-f)").GetDouble();

                    data.Sustained.RF = curve.GetProperty("Scale factors sustained").GetProperty("Reference load (RF)").GetDouble();
                    data.Sustained.H = curve.GetProperty("Scale factors sustained").GetProperty("Horizontal reach factor (H)").GetDouble();
                    data.Sustained.VRM = curve.GetProperty("Scale factors sustained").GetProperty("Vertical range middle factor (VRM)").GetDouble();
                    data.Sustained.DH = curve.GetProperty("Scale factors sustained").GetProperty("Horizontal travel distance factor (DH)").GetDouble();
                    data.Sustained.DV = curve.GetProperty("Scale factors sustained").GetProperty("Vertical travel distance factor (DV)").GetDouble();
                    data.Sustained.V = curve.GetProperty("Scale factors sustained").GetProperty("Vertical height factor (V)").GetDouble();
                    data.Sustained.F = curve.GetProperty("Scale factors sustained").GetProperty("Frequency factor (F)").GetDouble();
                    data.Sustained.CV = curve.GetProperty("Scale factors sustained").GetProperty("Coefficient of variation (CV)").GetDouble();
                    data.Sustained.MAL = curve.GetProperty("Scale factors sustained").GetProperty("MAL — Maximum acceptable load — Mean (kg or kg-f)").GetDouble();
                    data.Sustained.MAL75 = curve.GetProperty("Scale factors sustained").GetProperty("MAL — Maximum acceptable load — 75% (kg or kg-f)").GetDouble();
                    data.Sustained.MAL90 = curve.GetProperty("Scale factors sustained").GetProperty("MAL — Maximum acceptable load — 90% (kg or kg-f)").GetDouble();

                    //data.results.IniCoeffV = curve.GetProperty("Results").GetProperty("Coefficient of variation initial").GetDouble();
                    //data.results.SusCoeffV = curve.GetProperty("Results").GetProperty("Coefficient of variation sustained").GetDouble();
                    //data.results.IniForce = curve.GetProperty("Results").GetProperty("Initial force (kg)").GetDouble();
                    //data.results.SusForce = curve.GetProperty("Results").GetProperty("Sustained force (kg)").GetDouble();
                    //data.results.Weight = curve.GetProperty("Results").GetProperty("Weight (kg)").GetDouble();

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
                nStart = rtbShowResult.Find("Scale factors", nStart + 1, -1, RichTextBoxFinds.MatchCase);
                if (nStart == -1) break;
                nEnd = rtbShowResult.Find(Environment.NewLine.ToCharArray(), nStart + 1);
                rtbShowResult.Select(nStart, nEnd - nStart);
                rtbShowResult.SelectionFont = new Font(rtbShowResult.SelectionFont, FontStyle.Underline | FontStyle.Bold);
            }

            // Bold results
            nStart = 0;
            while (true)
            {
                nStart = rtbShowResult.Find("Maximum acceptable limit", nStart + 1, -1, RichTextBoxFinds.MatchCase);
                if (nStart == -1) break;
                //nEnd = rtbShowResult.Text.Length;
                nEnd = rtbShowResult.Find(Environment.NewLine.ToCharArray(), nStart + 1);
                rtbShowResult.Select(nStart, nEnd - nStart);
                rtbShowResult.SelectionFont = new Font(rtbShowResult.SelectionFont.FontFamily, rtbShowResult.Font.Size, FontStyle.Underline | FontStyle.Bold);
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
                ClearPlots();
                ShowResults();
            }
        }

        public void Duplicate()
        {
            string _strPath = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);

            // Mostrar la ventana de resultados
            frmResultsLiberty frmResults = new frmResultsLiberty(_data);
            {
                MdiParent = this.MdiParent;
            };
            if (File.Exists(_strPath + @"\images\logo.ico")) frmResults.Icon = new Icon(_strPath + @"\images\logo.ico");
            frmResults.Show();
        }
        #endregion IChildResults inferface
    }
}
