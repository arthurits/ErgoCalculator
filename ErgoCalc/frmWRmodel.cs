using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;

using ErgoCalc.Models.WR;

namespace ErgoCalc;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
public partial class FrmWRmodel : Form, IChildResults
{
    private List<DataWR> _datos;
    private ChartOptions _chartOptions;

    public FrmWRmodel()
    {
        InitializeComponent();
        InitializeChart();

        _datos = new List<DataWR>();

        this.mnuSub.Visible = false;
        _chartOptions = new ChartOptions(chart, 1)
        {
            NúmeroCurva = chart.Plot.GetPlottables().Length - 1
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

    public FrmWRmodel(object datos)
        :this()
    {
        _datos = (List<DataWR>)datos;
        CalcularCurva();
        _chartOptions.NúmeroCurva = chart.Plot.GetPlottables().Length - 1;
    }

    /// <summary>
    /// Initializes the chart objets
    /// </summary>
    private void InitializeChart()
    {
        chart.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(chart_DoubleClick);
        chart.MouseClick += new System.Windows.Forms.MouseEventHandler(chart_MouseClick);
        //chart.MouseMoved += new System.Windows.Forms.MouseEventHandler(chart_DoubleClick);
        //chart.MouseMove += new System.Windows.Forms.MouseEventHandler(chart_DoubleClick);
        chart.Plot.XAxis2.Label("WR model", size: 16);
        chart.Plot.YAxis.Label("% Maximum holding time", size: 14);
        chart.Plot.XAxis.Label("Time / s", size: 14);
        chart.Plot.Palette = ScottPlot.Palette.Nord;
        chart.Plot.SetAxisLimits(0, null, 0, 100);
        chart.Plot.Grid(color: Color.FromArgb(234, 234, 234), lineStyle: ScottPlot.LineStyle.Solid);
        chart.Plot.XAxis.MajorGrid(lineWidth: 1);
        chart.Plot.YAxis.MajorGrid(lineWidth: 1);
        chart.Plot.Legend(location: ScottPlot.Alignment.LowerRight);
    }

    private void PlotCurves()
    {
        foreach (var datos in _datos)
        {
            //chartB.plt.Add(new ScottPlot.PlottableScatter(valores[0], valores[1]));
            chart.Plot.AddScatter(datos.PointsX, datos.PointsY, label: datos.Legend, lineWidth: 3, markerShape: ScottPlot.MarkerShape.none);
        }

        chart.Plot.AxisAutoX();
        chart.Plot.SetAxisLimits(0, null, 0, 100);
        chart.Render();
    }

    private void CalcularCurva()
    {
        foreach (var datos in _datos)
        {
            // Calcular los puntos de la curva.
            WorkRest.WRCurve(datos);
            //chartB.plt.Add(new ScottPlot.PlottableScatter(valores[0], valores[1]));
            chart.Plot.AddScatter(datos.PointsX, datos.PointsY, label: datos.Legend, lineWidth: 3, markerShape: ScottPlot.MarkerShape.none);
        }
        
        chart.Plot.AxisAutoX();
        chart.Plot.SetAxisLimits(0, null, 0, 100);
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

    private void chart_MouseClick(object sender, MouseEventArgs e)
    {
        // https://github.com/ScottPlot/ScottPlot/discussions/645
        //double x = chart.plt.CoordinateFromPixelX(e.X);
        //double y = chart.plt.CoordinateFromPixelY(e.Y);

        //using var bmp = chart.plt.GetBitmap(false);
        //var algo = bmp.GetPixel(e.X, e.Y);
        
        ////Text = $"Clicked X={x} Y={y}";
        //foreach (var plot in chart.plt.GetPlottables())
        //{
        //    if ( algo.Name == ((ScottPlot.PlottableScatter)plot).color.Name)
        //    {
        //        ((ScottPlot.PlottableScatter)plot).lineWidth = 1 + 2 * ((ScottPlot.PlottableScatter)plot).lineWidth;
        //        chart.Render();
        //    }
        //}
        //var colores = chart.plt.Colorset();

        
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
        SaveFileDialog SaveDlg = new()
        {
            DefaultExt = "*.png",
            //Filter = "PNG file (*.png)|*.png|All files (*.*)|*.*",
            Filter = "PNG Files (*.png)|*.png;*.png" +
                      "|JPG Files (*.jpg, *.jpeg)|*.jpg;*.jpeg" +
                      "|BMP Files (*.bmp)|*.bmp;*.bmp" +
                      "|All files (*.*)|*.*",
            FilterIndex = 1,
            FileName = "WorkRest plot",
            Title = "Save plot image",
            OverwritePrompt = true,
            InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        };

        using (new CenterWinDialog(this))
        {
            DialogResult result = SaveDlg.ShowDialog();

            // If the file name is not an empty string open it for saving.  
            if (result == DialogResult.OK && SaveDlg.FileName != "")
            {
                chart.Plot.SaveFig(SaveDlg.FileName);
            }
        }
    }
    private void toolStripWR_AddLine_Click(object sender, EventArgs e)
    {
        // Llamar al formulario para introducir los datos
        frmDataWRmodel frmDatosWR = new frmDataWRmodel(_datos);
        if (frmDatosWR.ShowDialog(this) == DialogResult.OK)
        {
            _datos = (List<DataWR>)frmDatosWR.GetData;
            //_datos.Add(frmDatosWR.getData());
            CalcularCurva();
            _chartOptions.NúmeroCurva = chart.Plot.GetPlottables().Length - 1;
            propertyGrid1.Refresh();
        }
        // Cerrar el formulario de entrada de datos
        frmDatosWR.Dispose();
    }

    private void toolStripWR_RemoveLine_Click(object sender, EventArgs e)
    {
        var i = chart.Plot.GetPlottables().Length;
        if (i > 0)
        { 
            chart.Plot.Remove(chart.Plot.GetPlottables()[i - 1]);
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

            writer.WriteNumber("Curve #", data.PlotCurve);
            writer.WriteString("Curve legend", data.Legend);
            writer.WriteNumber("MHT", data.MHT);
            writer.WriteNumber("MVC", data.MVC);
            writer.WriteNumber("Step", data.PlotStep);
            writer.WriteNumber("Points", data.Points);
            writer.WriteNumber("Cycles", data.Cycles);
            
            writer.WritePropertyName("Work times (minutes)");
            JsonSerializer.Serialize(writer, data.WorkingTimes, new JsonSerializerOptions { WriteIndented = true });

            writer.WritePropertyName("Rest times (minutes)");
            JsonSerializer.Serialize(writer, data.RestingTimes, new JsonSerializerOptions { WriteIndented = true });

            //writer.WritePropertyName("Work-Rest drop (REC)");
            //JsonSerializer.Serialize(writer, data._dWorkRestDrop, new JsonSerializerOptions { WriteIndented = true });

            writer.WritePropertyName("Points X");
            JsonSerializer.Serialize(writer, data.PointsX, new JsonSerializerOptions { WriteIndented = true });

            writer.WritePropertyName("Points Y");
            JsonSerializer.Serialize(writer, data.PointsY, new JsonSerializerOptions { WriteIndented = true });

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
        SaveFileDialog SaveDlg = new ()
        {
            DefaultExt = "*.csv",
            Filter = "ERGO file (*.ergo)|*.ergo|CSV file (*.csv)|*.csv|Text file (*.txt)|*.txt|All files (*.*)|*.*",
            FilterIndex = 1,
            FileName = "WorkRest results",
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
                        //var jsonString = JsonSerializer.Serialize(_datos[0]._points[0], new JsonSerializerOptions { WriteIndented = true });
                    }
                    break;
                case 2:
                    foreach (var plot in chart.Plot.GetPlottables())
                    {
                        if (plot.GetType() == typeof(ScottPlot.Plottable.ScatterPlot))
                        {
                            //((ScottPlot.Plottable.ScatterPlot)plot).SaveCSV(SaveDlg.FileName);
                            var chart = (ScottPlot.Plottable.ScatterPlot)plot;
                            StringBuilder csv = new();
                            for (int i = 0; i < chart.Ys.Length; i++)
                                csv.AppendFormat(System.Globalization.CultureInfo.InvariantCulture, "{0}{1}{2}{3}", chart.Xs[i], ", ", chart.Ys[i], "\n");
                            File.WriteAllText(SaveDlg.FileName, csv.ToString());
                        }

                        /*
                        public void SaveCSV(string filePath, string delimiter = ", ", string separator = "\n")
                        {
                            System.IO.File.WriteAllText(filePath, GetCSV(delimiter, separator));
                        }

                        public string GetCSV(string delimiter = ", ", string separator = "\n")
                        {
                            StringBuilder csv = new StringBuilder();
                            for (int i = 0; i < ys.Length; i++)
                                csv.AppendFormat("{0}{1}{2}{3}", xs[i], delimiter, ys[i], separator);
                            return csv.ToString();
                        }
                        */
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
        DataWR data;
        _datos = new List<DataWR>();
        JsonElement root = document.RootElement;

        try
        {
            foreach (JsonElement curve in root.GetProperty("WR curves").EnumerateArray())
            {
                data = new();

                data.PlotCurve = curve.GetProperty("Curve #").GetInt32();
                data.Legend = curve.GetProperty("Curve legend").GetString();
                data.MHT = curve.GetProperty("MHT").GetDouble();
                data.MVC = curve.GetProperty("MVC").GetDouble();
                data.PlotStep = curve.GetProperty("Step").GetDouble();
                data.Points = curve.GetProperty("Points").GetInt32();
                data.Cycles = curve.GetProperty("Cycles").GetByte();

                //data._dPoints = new double[2][];
                Length = curve.GetProperty("Points X").GetArrayLength();
                data.PointsX = new double[Length];
                data.PointsX= JsonSerializer.Deserialize<double[]>(curve.GetProperty("Points X").ToString());

                //Length = curve.GetProperty("Points Y").GetArrayLength();
                data.PointsY = new double[Length];
                data.PointsY = JsonSerializer.Deserialize<double[]>(curve.GetProperty("Points Y").ToString());

                //data._dWorkRest = new double[2][];
                Length = curve.GetProperty("Work times (minutes)").GetArrayLength();
                data.WorkingTimes = new double[Length];
                data.WorkingTimes = JsonSerializer.Deserialize<double[]>(curve.GetProperty("Work times (minutes)").ToString());

                //Length = curve.GetProperty("Rest times (minutes)").GetArrayLength();
                data.RestingTimes = new double[Length];
                data.RestingTimes = JsonSerializer.Deserialize<double[]>(curve.GetProperty("Rest times (minutes)").ToString());

                //Length = curve.GetProperty("Work-Rest drop (REC)").GetArrayLength();
                //data._dWorkRestDrop = new double[Length];
                //data._dWorkRestDrop = JsonSerializer.Deserialize<double[]>(curve.GetProperty("Work-Rest drop (REC)").ToString());

                _datos.Add(data);
            }
        }
        catch (Exception)
        {
            result = false;
        }

        if (result)
        {
            //CalcularCurva();
            PlotCurves();
            _chartOptions.NúmeroCurva = chart.Plot.GetPlottables().Length - 1;
        }

        return result;
    }

    public void EditData()
    {
        // Show the form with the data in order to edit it
        frmDataWRmodel frmDatosWR = new frmDataWRmodel(_datos);
        if (frmDatosWR.ShowDialog(this) == DialogResult.OK)
        {
            // Get the edited input data
            _datos = (List<DataWR>)frmDatosWR.GetData;
            //_datos.Add(frmDatosWR.getData());
            chart.Plot.Clear();
            CalcularCurva();
            _chartOptions.NúmeroCurva = chart.Plot.GetPlottables().Length - 1;
        }
        return;
    }

    public void Duplicate()
    {
        string _strPath = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);

        // Mostrar la ventana de resultados
        FrmWRmodel frmResults = new FrmWRmodel(_datos)
        {
            MdiParent = this.MdiParent
        };
        if (File.Exists(_strPath + @"\images\logo.ico")) frmResults.Icon = new Icon(_strPath + @"\images\logo.ico");
        frmResults.Show();
    }

    public bool[] GetToolbarEnabledState()
    {
        bool[] toolbar = new bool[] { true, true, true, true, true, true, true, true, true, false, false, true, true, true };
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
}