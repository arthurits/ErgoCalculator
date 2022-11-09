using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;

using ErgoCalc.Models.WR;

namespace ErgoCalc;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
public partial class FrmResultsWR : Form, IChildResults
{
    //private List<DataWR> _datos;
    private Job _job;
    private readonly ChartOptions _plotOptions;

    public FrmResultsWR()
    {
        InitializeComponent();
        InitializeChart();

        //_datos = new List<DataWR>();

        this.mnuSub.Visible = false;
        _plotOptions = new ChartOptions(plot, 1)
        {
            NúmeroCurva = plot.Plot.GetPlottables().Length - 1
        };

        propertyGrid1.SelectedObject = _plotOptions;
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

    public FrmResultsWR(object data)
        : this()
    {
        if (data.GetType() == typeof(Job))
        {
            _job = (Job)data;
            CalcularCurva();
            _plotOptions.NúmeroCurva = plot.Plot.GetPlottables().Length - 1;
        }
    }

    /// <summary>
    /// Initializes the chart objets
    /// </summary>
    private void InitializeChart()
    {
        plot.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(chart_DoubleClick);
        plot.MouseClick += new System.Windows.Forms.MouseEventHandler(chart_MouseClick);
        //chart.MouseMoved += new System.Windows.Forms.MouseEventHandler(chart_DoubleClick);
        //chart.MouseMove += new System.Windows.Forms.MouseEventHandler(chart_DoubleClick);
        plot.Plot.XAxis2.Label("WR model", size: 16);
        plot.Plot.YAxis.Label("% Maximum holding time", size: 14);
        plot.Plot.XAxis.Label("Time / s", size: 14);
        plot.Plot.Palette = ScottPlot.Palette.Nord;
        plot.Plot.SetAxisLimits(0, null, 0, 100);
        plot.Plot.Grid(color: Color.FromArgb(234, 234, 234), lineStyle: ScottPlot.LineStyle.Solid);
        plot.Plot.XAxis.MajorGrid(lineWidth: 1);
        plot.Plot.YAxis.MajorGrid(lineWidth: 1);
        plot.Plot.Legend(location: ScottPlot.Alignment.LowerRight);
    }

    private void PlotCurves()
    {
        foreach (DataWR task in _job.Tasks)
        {
            //chartB.plt.Add(new ScottPlot.PlottableScatter(valores[0], valores[1]));
            plot.Plot.AddScatter(task.PointsX, task.PointsY, label: task.Legend, lineWidth: 3, markerShape: ScottPlot.MarkerShape.none);
        }

        plot.Plot.AxisAutoX();
        plot.Plot.SetAxisLimits(0, null, 0, 100);
        plot.Render();
    }

    private void CalcularCurva()
    {
        foreach (DataWR task in _job.Tasks)
        {
            // Calcular los puntos de la curva.
            WorkRest.WRCurve(task);
            //chartB.plt.Add(new ScottPlot.PlottableScatter(valores[0], valores[1]));
            plot.Plot.AddScatter(task.PointsX, task.PointsY, label: task.Legend, lineWidth: 3, markerShape: ScottPlot.MarkerShape.none);
        }
        
        plot.Plot.AxisAutoX();
        plot.Plot.SetAxisLimits(0, null, 0, 100);
        plot.Render();
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
                plot.Plot.SaveFig(SaveDlg.FileName);
            }
        }
    }
    private void toolStripWR_AddLine_Click(object sender, EventArgs e)
    {
        // Llamar al formulario para introducir los datos
        FrmDataWR frmDatosWR = new FrmDataWR(_job);
        if (frmDatosWR.ShowDialog(this) == DialogResult.OK)
        {
            _job = (Job)frmDatosWR.GetData;
            //_datos.Add(frmDatosWR.getData());
            CalcularCurva();
            _plotOptions.NúmeroCurva = plot.Plot.GetPlottables().Length - 1;
            propertyGrid1.Refresh();
        }
        // Cerrar el formulario de entrada de datos
        frmDatosWR.Dispose();
    }

    private void toolStripWR_RemoveLine_Click(object sender, EventArgs e)
    {
        var i = plot.Plot.GetPlottables().Length;
        if (i > 0)
        { 
            plot.Plot.Remove(plot.Plot.GetPlottables()[i - 1]);
            plot.Render();
            _plotOptions.NúmeroCurva = i - 2;
            propertyGrid1.Refresh();
        }
    }

    private void SerializeToJSON(Utf8JsonWriter writer)
    {
        writer.WriteStartObject();
        writer.WriteString("Document type", "Work-Rest model");
        writer.WriteNumber("Number of tasks", _job.NumberTasks);
        
        writer.WritePropertyName("WR curves");
        writer.WriteStartArray();

        foreach (DataWR task in _job.Tasks)
        {
            writer.WriteStartObject();

            writer.WriteNumber("Curve #", task.PlotCurve);
            writer.WriteString("Curve legend", task.Legend);
            writer.WriteNumber("MHT", task.MHT);
            writer.WriteNumber("MVC", task.MVC);
            writer.WriteNumber("Step", task.PlotStep);
            writer.WriteNumber("Points", task.Points);
            writer.WriteNumber("Cycles", task.Cycles);
            
            writer.WritePropertyName("Working times (minutes)");
            JsonSerializer.Serialize(writer, task.WorkingTimes, new JsonSerializerOptions { WriteIndented = true });

            writer.WritePropertyName("Resting times (minutes)");
            JsonSerializer.Serialize(writer, task.RestingTimes, new JsonSerializerOptions { WriteIndented = true });

            //writer.WritePropertyName("Work-Rest drop (REC)");
            //JsonSerializer.Serialize(writer, data._dWorkRestDrop, new JsonSerializerOptions { WriteIndented = true });

            writer.WritePropertyName("Points X");
            JsonSerializer.Serialize(writer, task.PointsX, new JsonSerializerOptions { WriteIndented = true });

            writer.WritePropertyName("Points Y");
            JsonSerializer.Serialize(writer, task.PointsY, new JsonSerializerOptions { WriteIndented = true });

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
                    {
                        using var fs = SaveDlg.OpenFile();
                        if (fs != null)
                        {
                            using var writer = new Utf8JsonWriter(fs, options: new JsonWriterOptions { Indented = true });
                            SerializeToJSON(writer);
                            //var jsonString = JsonSerializer.Serialize(_datos[0]._points[0], new JsonSerializerOptions { WriteIndented = true });
                        }
                    }
                    break;
                case 2:
                    foreach (var plot in plot.Plot.GetPlottables())
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

            FrmMain.SetFormTitle(this, StringResources.FormResultsWR, SaveDlg.FileName);

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

        Job job = new();
        JsonElement root = document.RootElement;

        try
        {
            job.NumberTasks = root.GetProperty("Number of tasks").GetInt32();
            job.Tasks = new DataWR[job.NumberTasks];
            int i = 0;
            foreach (JsonElement curve in root.GetProperty("WR curves").EnumerateArray())
            {
                job.Tasks[i] = new();

                job.Tasks[i].PlotCurve = curve.GetProperty("Curve #").GetInt32();
                job.Tasks[i].Legend = curve.GetProperty("Curve legend").GetString();
                job.Tasks[i].MHT = curve.GetProperty("MHT").GetDouble();
                job.Tasks[i].MVC = curve.GetProperty("MVC").GetDouble();
                job.Tasks[i].PlotStep = curve.GetProperty("Step").GetDouble();
                job.Tasks[i].Points = curve.GetProperty("Points").GetInt32();
                job.Tasks[i].Cycles = curve.GetProperty("Cycles").GetByte();

                Length = curve.GetProperty("Points X").GetArrayLength();
                job.Tasks[i].PointsX = new double[Length];
                job.Tasks[i].PointsX= JsonSerializer.Deserialize<double[]>(curve.GetProperty("Points X").ToString());

                //Length = curve.GetProperty("Points Y").GetArrayLength();
                job.Tasks[i].PointsY = new double[Length];
                job.Tasks[i].PointsY = JsonSerializer.Deserialize<double[]>(curve.GetProperty("Points Y").ToString());

                Length = curve.GetProperty("Working times (minutes)").GetArrayLength();
                job.Tasks[i].WorkingTimes = new double[Length];
                job.Tasks[i].WorkingTimes = JsonSerializer.Deserialize<double[]>(curve.GetProperty("Working times (minutes)").ToString());

                Length = curve.GetProperty("Resting times (minutes)").GetArrayLength();
                job.Tasks[i].RestingTimes = new double[Length];
                job.Tasks[i].RestingTimes = JsonSerializer.Deserialize<double[]>(curve.GetProperty("Resting times (minutes)").ToString());

                i++;
            }

            _job = job;

        }
        catch (Exception)
        {
            result = false;
        }

        if (result)
        {
            //CalcularCurva();
            PlotCurves();
            _plotOptions.NúmeroCurva = plot.Plot.GetPlottables().Length - 1;
        }

        return result;
    }

    public void EditData()
    {
        // Show the form with the data in order to edit it
        FrmDataWR frmDatosWR = new FrmDataWR(_job);
        if (frmDatosWR.ShowDialog(this) == DialogResult.OK)
        {
            // Get the edited input data
            _job = (Job)frmDatosWR.GetData;
            //_datos.Add(frmDatosWR.getData());
            plot.Plot.Clear();
            CalcularCurva();
            _plotOptions.NúmeroCurva = plot.Plot.GetPlottables().Length - 1;
        }
        return;
    }

    public void Duplicate()
    {
        string _strPath = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);

        // Mostrar la ventana de resultados
        FrmResultsWR frmResults = new FrmResultsWR(_job)
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