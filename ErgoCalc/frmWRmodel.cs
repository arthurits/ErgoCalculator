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
            InitializeCharts();

            this.mnuSub.Visible = false;
            _chartOptions = new ChartOptions(this.chartA, 1);     

            // Panel de propiedades
            _chartOptions.NúmeroCurva = this.chartA.Series.Count - 1;
            propertyGrid1.SelectedObject = _chartOptions;
            splitContainer1.Panel1Collapsed = true;

            // ToolStrip
            var path = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            if (File.Exists(path + @"\images\settings.ico")) this.toolStripWR_Settings.Image=new Icon(path + @"\images\settings.ico", 48, 48).ToBitmap();
            this.toolStripWR_Settings.CheckOnClick = true;
            if (File.Exists(path + @"\images\Awicons-Vista-Artistic-Chart-add.ico")) this.toolStripWR_AddLine.Image = new Icon(path + @"\images\Awicons-Vista-Artistic-Chart-add.ico", 48, 48).ToBitmap();
            if (File.Exists(path + @"\images\Awicons-Vista-Artistic-Chart-delete.ico")) this.toolStripWR_RemoveLine.Image = new Icon(path + @"\images\Awicons-Vista-Artistic-Chart-delete.ico", 48, 48).ToBitmap();
            if (File.Exists(path + @"\images\charts_folder_badged.ico")) this.toolStripWR_SaveChart.Image = new Icon(path + @"\images\charts_folder_badged.ico", 48, 48).ToBitmap();

            // https://lvcharts.net/App/examples/v1/wf/Series

            chartA.Series[0].Values.Add(new ObservablePoint(0, 100));
            chartA.Series[0].Values.Add(new ObservablePoint(1, 90));
            chartA.Series[0].Values.Add(new ObservablePoint(2, 80));
            //chartA.Series[1].Values.Add(new ObservablePoint(0, 95));
            //chartA.Series[1].Values.Add(new ObservablePoint(0.5, 90));
            //chartA.Series[1].Values.Add(new ObservablePoint(1, 85));
            //chartA.Series[0].Values.AddRange({ (0,1),(1,2)});

            int[] vals = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            ObservablePoint[] value = { new ObservablePoint(0, 80), new ObservablePoint(0.5, 88), new ObservablePoint(1.0, 84) };

            //ObservablePoint puntos = new ObservablePoint();
            //ChartValues<Point> puntos = new ChartValues<Point> { new Point(0, 80), new Point(5, 88), new Point(10, 75) };
            //chartA.Series[1].Values = puntos;

            //chartA.Series[1].Values.Add(new LineSeries { Values = new ChartPoint<int>(vals) });
            //chartA.Series[1].Values.Add(new ObservablePoint);

            /*
            chartA.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Values = new ChartValues<double> { 3, 5, 7, 4 }
                },
                new LineSeries
                {
                    Values = new ChartValues<decimal> { 5, 6, 2, 7 }
                }
            };
            chartA.AxisX.Add(new Axis
            {
                Title = "Time (s)"
            });

            chartA.AxisY.Add(new Axis
            {
                Title = "% Maximum holding time"
            });
            chartA.Series.Add(new LineSeries
            {
                Values = new ChartValues<double> { 5, 3, 2, 4, 5 },
                LineSmoothness = 0, //straight lines, 1 really smooth lines
                //PointGeometry = Geometry.Parse("m 25 70.36218 20 -28 -20 22 -8 -6 z"),
                PointGeometrySize = 50,
                //PointForeground = Brushes.Gray
            });
            */

        }

        public frmWRmodel(datosWR datos)
            :this()
        {
            _datos = datos;
            CalcularCurva();
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

            ChartValues<System.Windows.Point> puntos = new ChartValues<System.Windows.Point>();
            for (int i=0; i<valores[0].Length; i++)
            {
                puntos.Add(new System.Windows.Point(valores[0][i], valores[1][i]));
            }

            chartA.Series[1].Values = puntos;

            this.chart1.DataSource = valores;
            //this.chart1.Series.Add("Ejemplo");
            this.chart1.Series["Ejemplo"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            this.chart1.Series["Ejemplo"].BorderWidth = 2;
            this.chart1.Series["Ejemplo"].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            this.chart1.Series["Ejemplo"].YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            this.chart1.Series["Ejemplo"].Points.DataBindXY(valores[0], valores[1]);
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
            this.SuspendLayout();
            //splitContainer1.Panel1Collapsed = !splitContainer1.Panel1Collapsed;
            //splitContainer1.SplitterWidth = splitContainer1.Panel1Collapsed ? 4 : 0;

            splitContainer1.Panel1Collapsed = false;
            if (splitContainer1.SplitterDistance>0)
                Transitions.Transition.run(this.splitContainer1, "SplitterDistance", 0, new Transitions.TransitionType_Linear(200));
            else
                Transitions.Transition.run(this.splitContainer1, "SplitterDistance", 170, new Transitions.TransitionType_Linear(200));
            this.ResumeLayout();
        }

        private void toolStripMain_Settings_CheckedChanged(object sender, EventArgs e)
        {
            algoToolStripMenuItem.PerformClick();
            //algoToolStripMenuItem_Click(null, null);
        }

        #region IChildResults interface

        public void Save(string path)
        {
            return;
        }
        public bool[] GetToolbarEnabledState()
        {
            bool[] toolbar = new bool[] { true, true, true, true, true, true, true, true, true, true, true, true, true, true };
            return toolbar;
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
