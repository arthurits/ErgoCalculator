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

using ErgoCalc.Models.ThermalComfort;
using PsychroLib;
using ScottPlot;

namespace ErgoCalc
{
    public partial class frmResultsTC : Form, IChildResults
    {
        private List<ModelTC> _data;
        private CThermalModels _modelTC;
        private string _strPath;

        public frmResultsTC()
        {
            _strPath = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            if (File.Exists(_strPath + @"\images\logo.ico")) this.Icon = new Icon(_strPath + @"\images\logo.ico");

            InitializeComponent();
            InitializePlot();
            
            // Initialize private variables
            _modelTC = new CThermalModels();

            propertyGrid1.SelectedObject = new ResultsOptions(rtbShowResult);
            splitContainer1.Panel1Collapsed = false;
            splitContainer1.SplitterDistance = 0;
            splitContainer1.SplitterWidth = 1;
            splitContainer1.IsSplitterFixed = true;
        }

        public frmResultsTC(object data)
            :this()
        {
            _data = (List<ModelTC>)data;
        }

        private void frmResultsTC_Shown(object sender, EventArgs e)
        {
            ShowResults();
        }

        #region Private routines

        private void ShowResults()
        {
            Boolean error = false;

            _modelTC = new CThermalModels(_data);
            // Call the DLL function
            try
            {
                //_classDLL.StrainIndex(_classDLL.Parameters, orden, ref nSize);
                //_classDLL.RSI(_subtasks, orden, ref nSize);
                _modelTC.ThermalComfort();
                _data = _modelTC.GetData;
            }
            catch (EntryPointNotFoundException)
            {
                error = true;
                MessageBox.Show(
                    "The program calculation kernel's been tampered with.\nThermal Comfort could not be computed.",
                    "Thermal Comfort error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (DllNotFoundException)
            {
                error = true;
                MessageBox.Show(
                    "DLL files are missing. Please\nreinstall the application.",
                    "Thermal Comfort error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                error = true;
                MessageBox.Show(
                    "Error in the Thermal Comfort calculation kernel:\n" + ex.ToString(),
                    "Unexpected error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

            // Call the routine that shows the results
            if (error == false)
            {
                rtbShowResult.Text = _modelTC.ToString();
                CreatePlots();
                FormatText();
            }
        }

        private void InitializePlot()
        {
            // Draw the basic lines
            var CPsy = new Psychrometrics(UnitSystem.SI);
            var abscissa = new double[360 - 100];
            for (int i = 100; i < 360; i++)
            {
                abscissa[i - 100] = i / 10.0;
            }

            List<double[]> OrdinateVal = new List<double[]>();
            for (int j = 10; j <= 100; j += 10)
            {
                var line = new double[360 - 100];
                for (int i = 100; i < 360; i++)
                {
                    line[i - 100] = 1000 * CPsy.GetHumRatioFromRelHum(i / 10.0, j / 100.0, 101325);
                }
                OrdinateVal.Add(line);
                formsPlot1.plt.PlotScatter(abscissa, line, markerShape: ScottPlot.MarkerShape.none, color: Color.LightGray);
            }

            formsPlot1.plt.XLabel("Air temperature (°C)");
            formsPlot1.plt.YLabel("g water / kg dry air");
        }

        private void CreatePlots()
        {
            // Delete any poits if any
            // https://github.com/ScottPlot/ScottPlot/discussions/673
            formsPlot1.plt.GetPlottables().RemoveAll(x => x is ScottPlot.PlottableScatter && ((ScottPlot.PlottableScatter)x).ys.Length == 1);
            formsPlot2.plt.GetPlottables().Clear();

            // Psychrometric plot
            var CPsy = new Psychrometrics(UnitSystem.SI);
            int i = 0;
            foreach (var data in _data)
            {
                var result = 1000 * CPsy.GetHumRatioFromRelHum(data.data.TempAir, data.data.RelHumidity / 100, 101325);
                formsPlot1.plt.PlotPoint(data.data.TempAir, result, label: "Case " + ((char)('A' + i)).ToString(), markerSize: 7);
                i++;
            }
            pictureBox1.Image = formsPlot1.plt.GetLegendBitmap();
            formsPlot1.Render();

            // Heat-loss plot
            int nSeries = _data.Count;
            string[] xsLabels = new string[nSeries];
            double[] xsStacked = new double[nSeries];
            double[] HL_1 = new double[nSeries];
            double[] HL_2 = new double[nSeries];
            double[] HL_3 = new double[nSeries];
            double[] HL_4 = new double[nSeries];
            double[] HL_5 = new double[nSeries];
            double[] HL_6 = new double[nSeries];
            i = 0;
            foreach(var data in _data)
            {
                xsLabels[i] = "Case " + ((char)('A' + i)).ToString();
                xsStacked[i] = i;
                HL_1[i] = data.factors.HL_Skin;
                HL_2[i] = HL_1[i] + data.factors.HL_Sweating;
                HL_3[i] = HL_2[i] + data.factors.HL_Latent;
                HL_4[i] = HL_3[i] + data.factors.HL_Dry;
                HL_5[i] = HL_4[i] + data.factors.HL_Radiation;
                HL_6[i] = HL_5[i] + data.factors.HL_Convection;
                i++;
            }

            // Plot the bar charts in reverse order (highest first)
            //formsPlot2.plt.Legend(backColor: Color.White, shadowDirection: shadowDirection.none, location: legendLocation.lowerRight, fixedLineWidth: true);
            formsPlot2.plt.Colorset(ScottPlot.Drawing.Colorset.Nord);
            
            formsPlot2.plt.PlotBar(xsStacked, HL_6, label: "Convection");
            formsPlot2.plt.PlotBar(xsStacked, HL_5, label: "Radiation");
            formsPlot2.plt.PlotBar(xsStacked, HL_4, label: "Dry");
            formsPlot2.plt.PlotBar(xsStacked, HL_3, label: "Latent");
            formsPlot2.plt.PlotBar(xsStacked, HL_2, label: "Sweating");
            formsPlot2.plt.PlotBar(xsStacked, HL_1, label: "Skin");
            formsPlot2.plt.XTicks(xsStacked, xsLabels);
            pictureBox2.Image = formsPlot2.plt.GetLegendBitmap();

            formsPlot2.Render();

        }

        private void SerializeToJSON(Utf8JsonWriter writer)
        {
            writer.WriteStartObject();
            writer.WriteString("Document type", "Thermal comfort model");
            
            writer.WritePropertyName("Cases");
            writer.WriteStartArray();

            foreach (var data in _data)
            {
                writer.WriteStartObject();

                writer.WritePropertyName("Data");
                writer.WriteStartObject();
                writer.WriteNumber("Air temperature", data.data.TempAir);
                writer.WriteNumber("Radiant temperature", data.data.TempRad);
                writer.WriteNumber("Air velocity", data.data.Velocity);
                writer.WriteNumber("Relative humidity", data.data.RelHumidity);
                writer.WriteNumber("Clothing", data.data.Clothing);
                writer.WriteNumber("Metabolic rate", data.data.MetRate);
                writer.WriteNumber("External work", data.data.ExternalWork);
                writer.WriteEndObject();

                writer.WritePropertyName("Results");
                writer.WriteStartObject();
                writer.WriteNumber("PMV", data.factors.PMV);
                writer.WriteNumber("PPD", data.factors.PPD);
                writer.WriteNumber("Heat loss - Skin", data.factors.HL_Skin);
                writer.WriteNumber("Heat loss - Sweating", data.factors.HL_Sweating);
                writer.WriteNumber("Heat loss - Latent", data.factors.HL_Latent);
                writer.WriteNumber("Heat loss - Dry", data.factors.HL_Dry);
                writer.WriteNumber("Heat loss - Radiation", data.factors.HL_Radiation);
                writer.WriteNumber("Heat loss - Convection", data.factors.HL_Convection);
                writer.WriteEndObject();

                writer.WriteEndObject();
            }

            writer.WriteEndArray();
            writer.WriteEndObject();

            writer.Flush();
        }

        #endregion Private routines

        #region IChildResults interface
        public ToolStrip ChildToolStrip
        {
            get => null;
            set { }
        }

        public void Duplicate()
        {
            string _strPath = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);

            // Mostrar la ventana de resultados
            frmResultsTC frmResults = new frmResultsTC(_data);
            {
                MdiParent = this.MdiParent;
            };
            if (File.Exists(_strPath + @"\images\logo.ico")) frmResults.Icon = new Icon(_strPath + @"\images\logo.ico");
            frmResults.Show();
        }

        public void EditData()
        {
            using var frm = new frmDataTC(_data);

            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                _data = (List<ModelTC>)frm.GetData;
                ShowResults();
            }
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
            bool result = true;
            ModelTC data = new ModelTC();
            _data = new List<ModelTC>();
            JsonElement root = document.RootElement;

            try
            {
                foreach (JsonElement curve in root.GetProperty("Cases").EnumerateArray())
                {
                    data.data.TempAir = curve.GetProperty("Data").GetProperty("Air temperature").GetDouble();
                    data.data.TempRad = curve.GetProperty("Data").GetProperty("Radiant temperature").GetDouble();
                    data.data.Velocity = curve.GetProperty("Data").GetProperty("Air velocity").GetDouble();
                    data.data.RelHumidity = curve.GetProperty("Data").GetProperty("Relative humidity").GetDouble();
                    data.data.Clothing = curve.GetProperty("Data").GetProperty("Clothing").GetDouble();
                    data.data.MetRate = curve.GetProperty("Data").GetProperty("Metabolic rate").GetDouble();
                    data.data.ExternalWork = curve.GetProperty("Data").GetProperty("External work").GetDouble();

                    data.factors.PMV = curve.GetProperty("Results").GetProperty("PMV").GetDouble();
                    data.factors.PPD = curve.GetProperty("Results").GetProperty("PPD").GetDouble();
                    data.factors.HL_Skin = curve.GetProperty("Results").GetProperty("Heat loss - Skin").GetDouble();
                    data.factors.HL_Sweating = curve.GetProperty("Results").GetProperty("Heat loss - Sweating").GetDouble();
                    data.factors.HL_Latent = curve.GetProperty("Results").GetProperty("Heat loss - Latent").GetDouble();
                    data.factors.HL_Dry = curve.GetProperty("Results").GetProperty("Heat loss - Dry").GetDouble();
                    data.factors.HL_Radiation = curve.GetProperty("Results").GetProperty("Heat loss - Radiation").GetDouble();
                    data.factors.HL_Convection = curve.GetProperty("Results").GetProperty("Heat loss - Convection").GetDouble();

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

        public bool PanelCollapsed()
        {
            return splitContainer1.SplitterDistance == 0 ? true : false;
        }

        public void Save(string path)
        {
            // Displays a SaveFileDialog so the user can save the Image  
            SaveFileDialog SaveDlg = new SaveFileDialog
            {
                DefaultExt = "*.csv",
                Filter = "ERGO file (*.ergo)|*.ergo|RTF file (*.rtf)|*.rtf|Text file (*.txt)|*.txt|All files (*.*)|*.*",
                FilterIndex = 1,
                Title = "Save thermal comfort data",
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

            return;
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

        #endregion IChildResults interface

    }
}
