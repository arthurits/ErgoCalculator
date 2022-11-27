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
[System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
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
             errorNoCurveSelected();
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
    private void errorNoCurveSelected()
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
        System.Windows.Forms.MessageBox.Show(
            resources.GetString("MsgBoxPropiedadesMensaje"),
            resources.GetString("MsgBoxPropiedadesTítulo"),
            System.Windows.Forms.MessageBoxButtons.OK,
            System.Windows.Forms.MessageBoxIcon.Warning);
    }

}

/// <summary>
/// Public interface for child windows
/// </summary>
public interface IChildResults
{
    /// <summary>
    /// Saves the data shown in the child window into a file
    /// </summary>
    /// <param name="path">Path where the data should be saved</param>
    void Save(string path);

    /// <summary>
    /// Opens a document
    /// </summary>
    /// <param name="document">Document</param>
    /// <returns></returns>
    bool OpenFile(System.Text.Json.JsonDocument document);

    /// <summary>
    /// Defines the enabled state for each control in frmMain's ToolBar
    /// </summary>
    /// <returns>An array with 'enabled' boolean values</returns>
    bool[] GetToolbarEnabledState();

    /// <summary>
    /// Gets and set the child's ToolStrip control in order to be merged with the parent's
    /// </summary>
    ToolStrip ChildToolStrip { get; set; }

    ///// <summary>
    ///// Sets the enabled/disabled state of the ToolStripButtons in the parent MDI form
    ///// </summary>
    //void ShowHideSettings();

    ///// <summary>
    ///// Gets the state of the splitContainer1.Panel1Collapsed
    ///// </summary>
    ///// <returns>True if the PropertyGrid is visible, false if it is collapsed</returns>
    //bool PanelCollapsed();

    /// <summary>
    /// Formats the text shown in the RichtTextBox
    /// </summary>
    void FormatText();

    /// <summary>
    /// Edits the original data of the child window
    /// </summary>
    void EditData();

    /// <summary>
    /// Duplicates the current child window
    /// </summary>
    void Duplicate();
}

/// <summary>
/// Public interface for data windows
/// </summary>
public interface IChildData
{
    public object GetData { get; }
    public void LoadExample();
    //object GetData();

    public void UpdateGridColumns(object control, int value)
    {
        if (control is DataGridView gridVariables)
        {
            // Add or remove columns
            if (value > gridVariables.ColumnCount)
                for (int i = gridVariables.ColumnCount; i < value; i++) AddColumn(i);
            else if (value < gridVariables.ColumnCount)
                for (int i = gridVariables.ColumnCount - 1; i >= value; i--) gridVariables.Columns.RemoveAt(i);
        }
    }

    protected internal virtual void AddColumn(Int32 col)
    {
        return;
    }

    public void UpdateListView(object control, int value)
    {
        //Int32 tasks = Convert.ToInt32(updTasks.Value);

        if (control is ListViewEx listViewEx)
        {
            if (value > listViewEx.Groups.Count)
            {
                for (int i = listViewEx.Groups.Count; i < value; i++)
                    listViewEx.AddGroup(i);
            }
            else if (value < listViewEx.Groups.Count)
            {
                for (int i = value; i < listViewEx.Groups.Count; i++)
                    listViewEx.RemoveGroup(listViewEx.Groups.Count - 1);
            }
        }
    }
}