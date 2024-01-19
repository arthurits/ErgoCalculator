using System.ComponentModel;

namespace ErgoCalc;

#region Definición de excepciones 
class InvalidRange : Exception
{
    // Use the default Exception constructors
    public InvalidRange() : base() { }
    public InvalidRange(string s) : base(s) { }
    public InvalidRange(string s, Exception ex) : base(s, ex) { }
}

class FieldLength : Exception
{
    // Use the default Exception constructors
    public FieldLength() : base() { }
    public FieldLength(string s) : base(s) { }
    public FieldLength(string s, Exception ex) : base(s, ex) { }
}

class DifferentSize : Exception
{
    // Use the default Exception constructors
    public DifferentSize() : base() { }
    public DifferentSize(string s) : base(s) { }
    public DifferentSize(string s, Exception ex) : base(s, ex) { }
}

class NotAnInteger : Exception
{
    // Use the default Exception constructors
    public NotAnInteger() : base() { }
    public NotAnInteger(string s) : base(s) { }
    public NotAnInteger(string s, Exception ex) : base(s, ex) { }
}

class MemoryAllocation : Exception
{
    // Use the default Exception constructors
    public MemoryAllocation() : base() { }
    public MemoryAllocation(string s) : base(s) { }
    public MemoryAllocation(string s, Exception ex) : base(s, ex) { }
}
#endregion

/// <summary>
/// Defines the properties of the chart to be displayed in the ProperyGrid control
/// </summary>
public class ChartOptions
{
    #region Propiedades de la clase
    private ScottPlot.FormsPlot _plot;
    private ScottPlot.Plottable.ScatterPlot _serie;
    //private LiveCharts.WinForms.CartesianChart _chart;
    //private LiveCharts.Wpf.AxesCollection _axisX;
    //private LiveCharts.Wpf.AxesCollection _axisY;
    //private ZedGraph.GraphPane _zedG;
    private Int32 _nCurva;
    #endregion

    /// <summary>
    /// Constructor de la clase. Inicializar propiedades.
    /// </summary>
    public ChartOptions(ScottPlot.FormsPlot plot, Int32 i)
    {
        _plot = plot;
        _nCurva = i;
    }

    [Browsable(false)]
    public ScottPlot.FormsPlot plot
    {
        set { _plot = value; }
    }

    [Browsable(false)]
    public Int32 NúmeroCurva
    {
        get { return _nCurva; }
        set
        {
            _nCurva = value;
            if (value > -1)
            {
                //var plots = _plot.plt.GetPlottables()[_nCurva];
                _serie = (ScottPlot.Plottable.ScatterPlot)_plot.Plot.GetPlottables()[_nCurva];
            }
        }
    }

    /// <summary>
    /// Propiedades públicas de la clase
    /// </summary>
    [Browsable(true),
    ReadOnly(false),
    Description("Color palette"),
    Category("Plot options")]
    public ScottPlot.IPalette ColorPalette
    {
        get => _plot.Plot.Palette;
        set
        {
            _plot.Plot.Palette = value;
            _plot.Render();
        }
    }

    /// <summary>
    /// Propiedades públicas de la clase
    /// </summary>
    [Browsable(true),
    ReadOnly(false),
    Description("Show / hide grid"),
    Category("Plot options")]
    public bool ShowGrid
    {
        get => true;
        set
        {
            _plot.Plot.YAxis.Grid(enable: value);
            _plot.Plot.XAxis.Grid(enable: value);
            _plot.Render();
        }
    }

    // <summary>
    /// Propiedades públicas de la clase
    /// </summary>
    [Browsable(true),
    ReadOnly(false),
    Description("Show / hide legend"),
    Category("Plot options")]
    public bool LegendGrid
    {
        get => _plot.Plot.Legend().IsVisible;
        set
        {
            _plot.Plot.Legend(enable: value);
            _plot.Render();
        }
    }

    /// <summary>
    /// Propiedades públicas de la clase
    /// </summary>
    [Browsable(true),
    ReadOnly(false),
    Description("Plot title"),
    Category("Text and labels")]
    public string PlotTitle
    {
        get => _plot.Plot.YAxis2.Label();
        set { _plot.Plot.Title(label: value); _plot.Render(); }
    }

    /// <summary>
    /// Propiedades públicas de la clase
    /// </summary>
    [Browsable(true),
    ReadOnly(false),
    Description("Abscissa title"),
    Category("Text and labels")]
    public string AxisXTitle
    {
        get => _plot.Plot.XAxis.Label();
        set { _plot.Plot.XAxis.Label(label: value); _plot.Render(); }
    }

    /// <summary>
    /// Propiedades públicas de la clase
    /// </summary>
    [Browsable(true),
    ReadOnly(false),
    Description("Ordinate title"),
    Category("Text and labels")]
    public string AxisYTitle
    {
        get => _plot.Plot.YAxis.Label();
        set { _plot.Plot.YAxis.Label(label: value); _plot.Render(); }
    }


    /// <summary>
    /// Propiedades públicas de la clase
    /// </summary>
    [Browsable(true),
    ReadOnly(false),
    Description("Serie's color"),
    Category("Series options")]
    public Color CurvaColor
    {
        get => _nCurva == -1 ? Color.Red : _serie.Color;
        set
        {
            if (_nCurva == -1)
             ErrorNoCurveSelected();
            else
            {
                _serie.Color = Color.FromArgb(value.A, value.R, value.G, value.B);
                _plot.Render();
            }
        }
    }

    /// <summary>
    /// Propiedades públicas de la clase
    /// </summary>
    [Browsable(true),
    ReadOnly(false),
    Description("Serie's thickness"),
    Category("Series options")]
    public double Width
    {
        get => _serie == null ? 0.0 : _serie.LineWidth;
        set { _serie.LineWidth = value; _plot.Render(); }
    }

    /// <summary>
    /// Propiedades públicas de la clase
    /// </summary>
    [Browsable(true),
    ReadOnly(false),
    Description("Serie's name (legend)"),
    Category("Series options")]
    public string Name
    {
        get => _serie == null ? String.Empty : _serie.Label;
        set { _serie.Label = value; _plot.Render(); }
    }

    /// <summary>
    /// Propiedades públicas de la clase
    /// </summary>
    [Browsable(true),
    ReadOnly(false),
    Description("Serie's Visibility"),
    Category("Series options")]
    public bool Visibility
    {
        get => _serie == null ? true : _serie.IsVisible;
        set { _serie.IsVisible = value; _plot.Render(); }
    }


    /// <summary>
    /// MessageBox de que no hay curvas seleccionadas
    /// </summary>
    private void ErrorNoCurveSelected()
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
        System.Windows.Forms.MessageBox.Show(
            resources.GetString("MsgBoxPropiedadesMensaje"),
            resources.GetString("MsgBoxPropiedadesTítulo"),
            System.Windows.Forms.MessageBoxButtons.OK,
            System.Windows.Forms.MessageBoxIcon.Warning);
    }

}
