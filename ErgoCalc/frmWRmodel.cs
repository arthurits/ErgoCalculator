using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Windows.Forms;

using ErgoCalc.DLL.WRmodel;
using LiveCharts; //Core of the library
using LiveCharts.Wpf; //The WPF controls
using LiveCharts.WinForms; //the WinForm wrappers
using LiveCharts.Defaults;

namespace ErgoCalc
{
    public partial class frmWRmodel : Form, IChildResults
    {
        private datosWR _datos;
        private ChartOptions _chartOptions;

        public frmWRmodel()
        {
            InitializeComponent();
            InitializeChart();

            this.mnuSub.Visible = false;
            _chartOptions = new ChartOptions(chartB, 1)
            {
                NúmeroCurva = chartB.plt.GetPlottables().Count - 1
            };

            propertyGrid1.SelectedObject = _chartOptions;
            splitContainer1.Panel1Collapsed = false;
            splitContainer1.SplitterDistance = 0;
            
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
            _datos = datos;
            CalcularCurva();
            _chartOptions.NúmeroCurva = chartB.plt.GetPlottables().Count - 1;
        }

        /// <summary>
        /// Initializes the chart objets
        /// </summary>
        private void InitializeChart()
        {
            chartB.plt.Title("WR model", fontSize: 16);
            chartB.plt.YLabel("% Maximum holding time", fontSize: 14);
            chartB.plt.XLabel("Time / s", fontSize: 14);
            chartB.plt.Colorset(ScottPlot.Drawing.Colorset.Nord);
            chartB.plt.Axis(0, null, 0, 100);
            chartB.plt.Grid(lineWidth: 1, color: Color.FromArgb(234, 234, 234), lineStyle: ScottPlot.LineStyle.Solid);
            chartB.plt.Legend(location: ScottPlot.legendLocation.upperCenter);
        }

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
            this.chartA.LegendLocation = LegendLocation.Top;
            this.chartA.BackColor = System.Drawing.Color.White;
            this.chartA.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chartA.Hoverable = false;
            this.chartA.DisableAnimations = true;
            this.chartA.DataTooltip = null;
            this.chartA.AxisX.Add(new LiveCharts.Wpf.Axis
            {
                Title = "Time / s",
                FontSize = 16,
                Width = 12
            });
            this.chartA.AxisY.Add(new LiveCharts.Wpf.Axis
            {
                Title = "% Maximum holding time",
                FontSize = 16
            });
            this.chartA.AxisY[0].MaxValue = 100;
            this.chartA.AxisY[0].MinValue = 0;
            this.chartA.Series = new SeriesCollection
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
        
        private void CalcularCurva()
        {
            cWRmodel model = new cWRmodel();
            double[][] valores;

            // Calcular los puntos de la curva. Si se devuelve una matriz vacía quiere decir
            //  que no se ha podido calcular la curva. Entonces hay que salir de la rutina
            valores = model.Curva(_datos);
            if (valores.Length == 0)
                return;

            //chartB.plt.Add(new ScottPlot.PlottableScatter(valores[0], valores[1]));
            chartB.plt.PlotScatter(valores[0], valores[1], label: "4 3 2", lineWidth: 3, markerShape: ScottPlot.MarkerShape.none);
            chartB.Render();
            chartB.plt.AxisAutoX();

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

        private void chartB_MouseClick(object sender, MouseEventArgs e)
        {
            // https://github.com/ScottPlot/ScottPlot/discussions/645
            double x = chartB.plt.CoordinateFromPixelX(e.X);
            double y = chartB.plt.CoordinateFromPixelY(e.Y);
            using (var bmp = chartB.plt.GetBitmap(false))
            {
                var algo = bmp.GetPixel(e.X, e.Y);
            }
            //Text = $"Clicked X={x} Y={y}";
        }


        private void chartA_DataClick(object sender, ChartPoint chartPoint)
        {
            String strSerieTitle = chartPoint.SeriesView.Title;

            for (int i=0; i< chartA.Series.Count; i++)
            {
                if (chartA.Series[i].Title == strSerieTitle)
                {
                    _chartOptions.NúmeroCurva = i;
                    propertyGrid1.Refresh();
                    break;
                }
            }

            //MessageBox.Show(sender.ToString());
        }

        public void algoToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void toolStripMain_Settings_CheckedChanged(object sender, EventArgs e)
        {
            this.SuspendLayout();
            //splitContainer1.Panel1Collapsed = !splitContainer1.Panel1Collapsed;
            //splitContainer1.SplitterWidth = splitContainer1.Panel1Collapsed ? 4 : 0;
            //splitContainer1.Panel1Collapsed = false;

            if (splitContainer1.SplitterDistance > 0)
                Transitions.Transition.run(this.splitContainer1, "SplitterDistance", 0, new Transitions.TransitionType_Linear(200));
            else
                Transitions.Transition.run(this.splitContainer1, "SplitterDistance", 170, new Transitions.TransitionType_Linear(200));
            this.ResumeLayout();
        }

        private void toolStripWR_SaveChart_Click(object sender, EventArgs e)
        {
            // Displays a SaveFileDialog so the user can save the Image  
            SaveFileDialog saveFileDlg = new SaveFileDialog
            {
                DefaultExt = "*.png",
                Filter = "PNG file (*.png)|*.png|All files (*.*)|*.*",
                FilterIndex = 1,
                Title = "Save plot image",
                OverwritePrompt = true,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };
            DialogResult result = saveFileDlg.ShowDialog();

            // If the file name is not an empty string open it for saving.  
            if (result == DialogResult.OK && saveFileDlg.FileName != "")
            {
                chartB.plt.SaveFig(saveFileDlg.FileName);
            }
        }
        private void toolStripWR_AddLine_Click(object sender, EventArgs e)
        {
            // Llamar al formulario para introducir los datos
            frmDataWRmodel frmDatosWR = new frmDataWRmodel();
            if (frmDatosWR.ShowDialog(this) == DialogResult.OK)
            {
                _datos = frmDatosWR.getData();
                CalcularCurva();
                _chartOptions.NúmeroCurva = chartB.plt.GetPlottables().Count - 1;
            }
            // Cerrar el formulario de entrada de datos
            frmDatosWR.Dispose();
        }

        private void toolStripWR_RemoveLine_Click(object sender, EventArgs e)
        {
            var i = chartB.plt.GetPlottables().Count;
            if (i > 0)
            { 
                chartB.plt.Remove(chartB.plt.GetPlottables()[i - 1]);
                _chartOptions.NúmeroCurva = i - 1;
            }
        }

        #region IChildResults interface

        public void Save(string path)
        {   
            // Displays a SaveFileDialog so the user can save the Image  
            SaveFileDialog saveFileDlg = new SaveFileDialog
            {
                DefaultExt = "*.csv",
                Filter = "CSV file (*.csv)|*.csv|Text file (*.txt)|*.txt|All files (*.*)|*.*",
                FilterIndex = 1,
                Title = "Save scatter-plot data",
                OverwritePrompt = true,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };
            DialogResult result = saveFileDlg.ShowDialog();

            // If the file name is not an empty string open it for saving.  
            if (result == DialogResult.OK && saveFileDlg.FileName != "")
            {   
                foreach (var plot in chartB.plt.GetPlottables())
                {
                    if (plot.GetType() == typeof(ScottPlot.PlottableScatter))
                        ((ScottPlot.PlottableScatter)plot).SaveCSV(saveFileDlg.FileName);
                }
            }

            return;
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
            algoToolStripMenuItem.PerformClick();
        }

        public bool PanelCollapsed()
        {
            return this.splitContainer1.Panel1Collapsed;
        }

        public void FormatText()
        {
            return;
        }


        #endregion
    }
}
