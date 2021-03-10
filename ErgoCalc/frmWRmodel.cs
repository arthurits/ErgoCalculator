using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Windows.Forms;

using ErgoCalc.Models.WRmodel;
using System.Text.Json;
//using LiveCharts; //Core of the library
//using LiveCharts.Wpf; //The WPF controls
//using LiveCharts.WinForms; //the WinForm wrappers
//using LiveCharts.Defaults;

namespace ErgoCalc
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
    public partial class frmWRmodel : Form, IChildResults
    {
        private List<datosWR> _datos;
        private ChartOptions _chartOptions;

        public frmWRmodel()
        {
            InitializeComponent();
            InitializeChart();

            _datos = new List<datosWR>();

            this.mnuSub.Visible = false;
            _chartOptions = new ChartOptions(chart, 1)
            {
                NúmeroCurva = chart.plt.GetPlottables().Count - 1
            };

            propertyGrid1.SelectedObject = _chartOptions;
            splitContainer1.Panel1Collapsed = false;
            splitContainer1.SplitterDistance = 0;
            splitContainer1.SplitterWidth = 1;
            splitContainer1.IsSplitterFixed = true;

            // ToolStrip
            var path = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            if (File.Exists(path + @"\images\settings.ico")) this.toolStripWR_Settings.Image=new Icon(path + @"\images\settings.ico", 48, 48).ToBitmap();
            this.toolStripWR_Settings.CheckOnClick = true;
            if (File.Exists(path + @"\images\chart-add.ico")) this.toolStripWR_AddLine.Image = new Icon(path + @"\images\chart-add.ico", 48, 48).ToBitmap();
            if (File.Exists(path + @"\images\chart-delete.ico")) this.toolStripWR_RemoveLine.Image = new Icon(path + @"\images\chart-delete.ico", 48, 48).ToBitmap();
            if (File.Exists(path + @"\images\chart-save.ico")) this.toolStripWR_SaveChart.Image = new Icon(path + @"\images\chart-save.ico", 48, 48).ToBitmap();

            // https://lvcharts.net/App/examples/v1/wf/Series
            //chartA.Series[0].Values.Add(new ObservablePoint(0, 100));
            //chartA.Series[0].Values.Add(new ObservablePoint(1, 90));
            //chartA.Series[0].Values.Add(new ObservablePoint(2, 80));
            
        }

        public frmWRmodel(datosWR datos)
            :this()
        {
            _datos.Add(datos);
            CalcularCurva();
            _chartOptions.NúmeroCurva = chart.plt.GetPlottables().Count - 1;
        }

        /// <summary>
        /// Initializes the chart objets
        /// </summary>
        private void InitializeChart()
        {
            chart.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(chart_DoubleClick);
            chart.MouseClicked += new System.Windows.Forms.MouseEventHandler(chart_MouseClicked);
            //chart.MouseMoved += new System.Windows.Forms.MouseEventHandler(chart_DoubleClick);
            //chart.MouseMove += new System.Windows.Forms.MouseEventHandler(chart_DoubleClick);
            chart.plt.Title("WR model", fontSize: 16);
            chart.plt.YLabel("% Maximum holding time", fontSize: 14);
            chart.plt.XLabel("Time / s", fontSize: 14);
            chart.plt.Colorset(ScottPlot.Drawing.Colorset.Nord);
            chart.plt.Axis(0, null, 0, 100);
            chart.plt.Grid(lineWidth: 1, color: Color.FromArgb(234, 234, 234), lineStyle: ScottPlot.LineStyle.Solid);
            chart.plt.Legend(location: ScottPlot.legendLocation.upperCenter);
        }


        private void CalcularCurva()
        {
            cWRmodel model = new cWRmodel();
            double[][] valores;
                        
            // Calcular los puntos de la curva. Si se devuelve una matriz vacía quiere decir
            //  que no se ha podido calcular la curva. Entonces hay que salir de la rutina
            valores = model.Curva(_datos.Last());
            if (valores.Length == 0)
                return;

            //chartB.plt.Add(new ScottPlot.PlottableScatter(valores[0], valores[1]));
            chart.plt.PlotScatter(valores[0], valores[1], label: "4 3 2", lineWidth: 3, markerShape: ScottPlot.MarkerShape.none);
            chart.plt.Axis(0, null, 0, 100);
            chart.plt.AxisAutoX();
            chart.Render();

            /*
           ChartValues<System.Windows.Point> puntos = new ChartValues<System.Windows.Point>();
           for (int i=0; i<valores[0].Length; i++)
           {
               puntos.Add(new System.Windows.Point(valores[0][i], valores[1][i]));
           }
            */

            //chartA.Series[1].Values = puntos;

            /*
            this.chart1.DataSource = valores;
            this.chart1.Series.Add("Ejemplo");
            this.chart1.Series["Ejemplo"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            this.chart1.Series["Ejemplo"].BorderWidth = 2;
            this.chart1.Series["Ejemplo"].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            this.chart1.Series["Ejemplo"].YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            this.chart1.Series["Ejemplo"].Points.DataBindXY(valores[0], valores[1]);
            */
        }

        private void chart_MouseClicked(object sender, MouseEventArgs e)
        {
            // https://github.com/ScottPlot/ScottPlot/discussions/645
            double x = chart.plt.CoordinateFromPixelX(e.X);
            double y = chart.plt.CoordinateFromPixelY(e.Y);

            using var bmp = chart.plt.GetBitmap(false);
            var algo = bmp.GetPixel(e.X, e.Y);
            
            //Text = $"Clicked X={x} Y={y}";
            foreach (var plot in chart.plt.GetPlottables())
            {
                if ( algo.Name == ((ScottPlot.PlottableScatter)plot).color.Name)
                {
                    ((ScottPlot.PlottableScatter)plot).lineWidth = 1 + 2 * ((ScottPlot.PlottableScatter)plot).lineWidth;
                    chart.Render();
                }
            }
            var colores = chart.plt.Colorset();

            
        }

        private void chart_Click(object sender, EventArgs e)
        {

        }

        private void chart_DoubleClick(object sender, EventArgs e)
        {

        }

        public void algoToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void toolStripMain_Settings_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void toolStripWR_SaveChart_Click(object sender, EventArgs e)
        {
            // Displays a SaveFileDialog so the user can save the Image  
            SaveFileDialog saveFileDlg = new SaveFileDialog
            {
                DefaultExt = "*.png",
                //Filter = "PNG file (*.png)|*.png|All files (*.*)|*.*",
                Filter = "PNG Files (*.png)|*.png;*.png" +
                          "|JPG Files (*.jpg, *.jpeg)|*.jpg;*.jpeg" +
                          "|BMP Files (*.bmp)|*.bmp;*.bmp" +
                          "|All files (*.*)|*.*",
                FilterIndex = 1,
                Title = "Save plot image",
                OverwritePrompt = true,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };

            using (new CenterWinDialog(this))
            {
                DialogResult result = saveFileDlg.ShowDialog();

                // If the file name is not an empty string open it for saving.  
                if (result == DialogResult.OK && saveFileDlg.FileName != "")
                {
                    chart.plt.SaveFig(saveFileDlg.FileName);
                }
            }
        }
        private void toolStripWR_AddLine_Click(object sender, EventArgs e)
        {
            // Llamar al formulario para introducir los datos
            frmDataWRmodel frmDatosWR = new frmDataWRmodel();
            if (frmDatosWR.ShowDialog(this) == DialogResult.OK)
            {
                _datos.Add(frmDatosWR.getData());
                CalcularCurva();
                _chartOptions.NúmeroCurva = chart.plt.GetPlottables().Count - 1;
                propertyGrid1.Refresh();
            }
            // Cerrar el formulario de entrada de datos
            frmDatosWR.Dispose();
        }

        private void toolStripWR_RemoveLine_Click(object sender, EventArgs e)
        {
            var i = chart.plt.GetPlottables().Count;
            if (i > 0)
            { 
                chart.plt.Remove(chart.plt.GetPlottables()[i - 1]);
                chart.Render();
                _chartOptions.NúmeroCurva = i - 2;
                propertyGrid1.Refresh();
            }
        }

        private void SerializeToJSON(Utf8JsonWriter writer)
        {
            writer.WriteStartObject();
            writer.WriteString("Document type", "Work-Rest model");
            
            writer.WritePropertyName("WR curves");
            writer.WriteStartArray();

            foreach ( var data in _datos)
            {
                writer.WriteStartObject();
                
                writer.WriteNumber("MHT", data._dMHT);
                writer.WriteNumber("MVC", data._dMVC);
                writer.WriteNumber("Step", data._dPaso);
                writer.WriteNumber("Curve #", data._nCurva);
                writer.WriteNumber("Points", data._nPuntos);
                writer.WriteNumber("Cycles", data._bCiclos);
                writer.WriteStartArray("Trabajo-Descanso");
                for (int i = 0; i < data._dTrabajoDescanso[0].Length; i++)
                {
                    writer.WriteStartArray();
                    writer.WriteNumberValue(data._dTrabajoDescanso[0][i]);
                    writer.WriteNumberValue(data._dTrabajoDescanso[1][i]);
                    writer.WriteEndArray();
                }
                writer.WriteEndArray();
                writer.WriteStartArray("Trabajo-Descanso p");
                for (int i = 0; i < data._dTrabajoDescansop[0].Length; i++)
                {
                    writer.WriteStartArray();
                    writer.WriteNumberValue(data._dTrabajoDescansop[0][i]);
                    writer.WriteNumberValue(data._dTrabajoDescansop[1][i]);
                    writer.WriteEndArray();
                }
                writer.WriteEndArray();

                writer.WriteEndObject();

            }

            writer.WriteEndArray();
            writer.WriteEndObject();

            writer.Flush();
        }

        #region IChildResults interface

        public void Save(string path)
        {   
            // Displays a SaveFileDialog so the user can save the Image  
            SaveFileDialog SaveDlg = new SaveFileDialog
            {
                DefaultExt = "*.csv",
                Filter = "ERGO file (*.ergo)|*.ergo|CSV file (*.csv)|*.csv|Text file (*.txt)|*.txt|All files (*.*)|*.*",
                FilterIndex = 1,
                Title = "Save scatter-plot data",
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
                switch (SaveDlg.FilterIndex)
                {
                    case 1:
                        var fs = SaveDlg.OpenFile();
                        if (fs != null)
                        {
                            using var writer = new Utf8JsonWriter(fs, options: new JsonWriterOptions { Indented = true });
                            SerializeToJSON(writer);
                        }
                        break;
                    case 2:
                        foreach (var plot in chart.plt.GetPlottables())
                        {
                            if (plot.GetType() == typeof(ScottPlot.PlottableScatter))
                                ((ScottPlot.PlottableScatter)plot).SaveCSV(SaveDlg.FileName);
                        }
                        break;
                }
                using (new CenterWinDialog(this.MdiParent))
                {
                    MessageBox.Show(this, "The file was successfully saved", "File saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            return;
        }

        public bool OpenFile(JsonDocument document)
        {
            bool result = true;
            int Length;
            datosWR data = new datosWR();
            _datos = new List<datosWR>();
            JsonElement root = document.RootElement;

            try
            {
                foreach (JsonElement curve in root.GetProperty("WR curves").EnumerateArray())
                {
                    data._dMHT = curve.GetProperty("MHT").GetDouble();
                    data._dMVC = curve.GetProperty("MVC").GetDouble();
                    data._dPaso = curve.GetProperty("Step").GetDouble();
                    data._nCurva = curve.GetProperty("Curve #").GetInt32();
                    data._nPuntos = curve.GetProperty("Points").GetInt32();
                    data._bCiclos = curve.GetProperty("Cycles").GetByte();
                    
                    Length = curve.GetProperty("Trabajo-Descanso").GetArrayLength();
                    data._dTrabajoDescanso = new double[2][];
                    data._dTrabajoDescanso[0] = new double[Length];
                    data._dTrabajoDescanso[1] = new double[Length];
                    for (int i = 0; i < Length; i++)
                    {                       
                        data._dTrabajoDescanso[0][i] = curve.GetProperty("Trabajo-Descanso")[i][0].GetDouble();
                        data._dTrabajoDescanso[1][i] = curve.GetProperty("Trabajo-Descanso")[i][1].GetDouble();
                    }

                    Length = curve.GetProperty("Trabajo-Descanso p").GetArrayLength();
                    data._dTrabajoDescansop = new double[2][];
                    data._dTrabajoDescansop[0] = new double[Length];
                    data._dTrabajoDescansop[1] = new double[Length];
                    for (int i = 0; i < Length; i++)
                    {
                        data._dTrabajoDescansop[0][i] = curve.GetProperty("Trabajo-Descanso p")[i][0].GetDouble();
                        data._dTrabajoDescansop[1][i] = curve.GetProperty("Trabajo-Descanso p")[i][1].GetDouble();
                    }

                    _datos.Add(data);
                }
            }
            catch (Exception)
            {
                result = false;
            }

            if (result)
            {
                CalcularCurva();
                _chartOptions.NúmeroCurva = chart.plt.GetPlottables().Count - 1;
            }

            return result;
        }

        public void EditData()
        {
            // Show the form with the data in order to edit it
            frmDataWRmodel frmDatosWR = new frmDataWRmodel(_datos.Last());
            if (frmDatosWR.ShowDialog(this) == DialogResult.OK)
            {
                // Get the edited input data
                _datos.Add(frmDatosWR.getData());
                chart.plt.Clear();
                CalcularCurva();
                _chartOptions.NúmeroCurva = chart.plt.GetPlottables().Count - 1;
            }
            return;
        }

        public void Duplicate()
        {
            string _strPath = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);

            // Mostrar la ventana de resultados
            frmWRmodel frmResults = new frmWRmodel(_datos.Last())
            {
                MdiParent = this.MdiParent
            };
            if (File.Exists(_strPath + @"\images\logo.ico")) frmResults.Icon = new Icon(_strPath + @"\images\logo.ico");
            frmResults.Show();

        }

        public bool[] GetToolbarEnabledState()
        {
            bool[] toolbar = new bool[] { true, true, true, true, true, true, true, true, true, true, true, true, true, true };
            return toolbar;
        }

        public ToolStrip ChildToolStrip
        {
            get => toolStripWR;
            set => toolStripWR = value;
        }

        public void ShowHideSettings()
        {
            //algoToolStripMenuItem.PerformClick();
            this.SuspendLayout();
            //splitContainer1.Panel1Collapsed = !splitContainer1.Panel1Collapsed;
            //splitContainer1.SplitterWidth = splitContainer1.Panel1Collapsed ? 4 : 0;
            //splitContainer1.Panel1Collapsed = false;

            if (splitContainer1.SplitterDistance > 0)
            {
                Transitions.Transition.run(this.splitContainer1, "SplitterDistance", 0, new Transitions.TransitionType_Linear(200));
                splitContainer1.SplitterWidth = 1;
                splitContainer1.IsSplitterFixed = true;
            }
            else
            {
                Transitions.Transition.run(this.splitContainer1, "SplitterDistance", 170, new Transitions.TransitionType_Linear(200));
                splitContainer1.SplitterWidth = 4;
                splitContainer1.IsSplitterFixed = false;
            }
            this.ResumeLayout();
        }

        public bool PanelCollapsed()
        {
            return splitContainer1.SplitterDistance == 0 ? true : false;
        }

        public void FormatText()
        {
            return;
        }

        #endregion IChildResults interface


        /*
        /// <summary>
        /// Initializes the chart objets
        /// </summary>
        private void InitializeCharts()
        {
            // https://github.com/Live-Charts/Live-Charts/issues/124
            var mapper = LiveCharts.Configurations.Mappers.Xy<System.Windows.Point>() //in this case value is of type <ObservablePoint>
                                                            .X(value => value.X) //use the X property as X
                                                            .Y(value => value.Y); //use the Y property as Y

            // Chart A initialization
            chartLive.LegendLocation = LegendLocation.Top;
            chartLive.BackColor = System.Drawing.Color.White;
            chartLive.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartLive.Hoverable = false;
            chartLive.DisableAnimations = true;
            chartLive.DataTooltip = null;
            chartLive.AxisX.Add(new LiveCharts.Wpf.Axis
            {
                Title = "Time / s",
                FontSize = 16,
                Width = 12
            });
            chartLive.AxisY.Add(new LiveCharts.Wpf.Axis
            {
                Title = "% Maximum holding time",
                FontSize = 16
            });
            chartLive.AxisY[0].MaxValue = 100;
            chartLive.AxisY[0].MinValue = 0;
            chartLive.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Sagital V",
                    Values = new ChartValues<ObservablePoint> (),
                    PointGeometry = null,
                    LineSmoothness = 1,
                    //Stroke = System.Windows.Media.Brushes.Aqua,
                    Fill = System.Windows.Media.Brushes.Transparent

                },
                new LineSeries (mapper)
                {
                    Title = "Sagital",
                    Values = new ChartValues<System.Windows.Point> (),
                    PointGeometry = null,
                    LineSmoothness = 0,
                    Fill = System.Windows.Media.Brushes.Transparent
                }
            };

        }

        private void chartLive_DataClick(object sender, ChartPoint chartPoint)
        {
            String strSerieTitle = chartPoint.SeriesView.Title;

            for (int i=0; i< chartLive.Series.Count; i++)
            {
                if (chartLive.Series[i].Title == strSerieTitle)
                {
                    _chartOptions.NúmeroCurva = i;
                    propertyGrid1.Refresh();
                    break;
                }
            }

            //MessageBox.Show(sender.ToString());
        }
        */

    }
}
