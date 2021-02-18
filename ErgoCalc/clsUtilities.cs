using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ErgoCalc
{
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
        private ScottPlot.PlottableScatter _serie;
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
                    _serie = (ScottPlot.PlottableScatter)_plot.plt.GetPlottables()[_nCurva];
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
        public ScottPlot.Drawing.Colorset ColorPalette
        {
            get => _plot.plt.Colorset();
            set
            {
                _plot.plt.Colorset(value);
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
            get => _plot.plt.GetSettings().HorizontalGridLines.Visible;
            set
            {
                _plot.plt.GetSettings().HorizontalGridLines.Visible = value;
                _plot.plt.GetSettings().VerticalGridLines.Visible = value;
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
            get => _plot.plt.GetSettings().Legend.Visible;
            set
            {
                _plot.plt.GetSettings().Legend.Visible = value;
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
            get => _plot.plt.GetSettings().title.text;
            set { _plot.plt.Title(value); _plot.Render(); }
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
            get => _plot.plt.GetSettings().xLabel.text;
            set { _plot.plt.XLabel(value); _plot.Render(); }
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
            get => _plot.plt.GetSettings().yLabel.text;
            set { _plot.plt.YLabel(value); _plot.Render(); }
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
            get => _nCurva == -1 ? Color.Red : _serie.color;
            set
            {
                if (_nCurva == -1)
                 errorNoCurveSelected();
                else
                {
                    _serie.color = Color.FromArgb(value.A, value.R, value.G, value.B);
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
            get => _serie == null ? 0.0 : _serie.lineWidth;
            set { _serie.lineWidth = value; _plot.Render(); }
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
            get => _serie == null ? String.Empty : _serie.label;
            set { _serie.label = value; _plot.Render(); }
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
            get => _serie == null ? true : _serie.visible;
            set { _serie.visible = value; _plot.Render(); }
        }


        /// <summary>
        /// MessageBox de que no hay curvas seleccionadas
        /// </summary>
        private void errorNoCurveSelected()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            System.Windows.Forms.MessageBox.Show(
                resources.GetString("MsgBoxPropiedadesMensaje"),
                resources.GetString("MsgBoxPropiedadesTítulo"),
                System.Windows.Forms.MessageBoxButtons.OK,
                System.Windows.Forms.MessageBoxIcon.Warning);
        }

    }

    /// <summary>
    /// Defines the properties of the results forms, basically the RichTextBox visual properties
    /// </summary>
    public class ResultsOptions
    {
        #region Propiedades de la clase
        private System.Windows.Forms.RichTextBox _rtbText;
        #endregion

        public ResultsOptions(System.Windows.Forms.RichTextBox rtbText)
        {
            _rtbText = rtbText;
        }

        /// <summary>
        /// Propiedades públicas de la clase
        /// </summary>
        [Browsable(true),
        ReadOnly(false),
        Description("Font"),
        Category("Text")]
        public System.Drawing.Font Font
        {
            get
            {
                return _rtbText.Font;
                //return axis[0].Title;
            }
            set
            {
                _rtbText.Font = value;
                ((IChildResults)_rtbText.Parent.Parent.Parent).FormatText();
            }
        }

        /// <summary>
        /// Propiedades públicas de la clase
        /// </summary>
        [Browsable(true),
        ReadOnly(false),
        Description("Font color"),
        Category("Text")]
        public System.Drawing.Color FontColor
        {
            get
            {
                return _rtbText.ForeColor;
                //return axis[0].Title;
            }
            set
            {
                _rtbText.ForeColor = value;
            }
        }

        /// <summary>
        /// Propiedades públicas de la clase
        /// </summary>
        [Browsable(true),
        ReadOnly(false),
        Description("Wrap text in new line?"),
        Category("Text")]
        public bool WordWrap
        {
            get
            {
                return _rtbText.WordWrap;
                //return axis[0].Title;
            }
            set
            {
                _rtbText.WordWrap = value;
            }
        }

        /// <summary>
        /// Propiedades públicas de la clase
        /// </summary>
        [Browsable(true),
        ReadOnly(false),
        Description("Text back color"),
        Category("Text")]
        public System.Drawing.Color BackColor
        {
            get
            {
                return _rtbText.BackColor;
                //return axis[0].Title;
            }
            set
            {
                _rtbText.BackColor = value;
            }
        }

        /// <summary>
        /// Propiedades públicas de la clase
        /// </summary>
        [Browsable(true),
        ReadOnly(false),
        Description("Text zoom"),
        Category("Text")]
        public float Zoom
        {
            get
            {
                return _rtbText.ZoomFactor;
                //return axis[0].Title;
            }
            set
            {
                _rtbText.ZoomFactor = value;
            }
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
        /// Defines the enabled state for each control in frmMain's ToolBar
        /// </summary>
        /// <returns>An array with 'enabled' boolean values</returns>
        bool[] GetToolbarEnabledState();

        /// <summary>
        /// Gets and set the child's ToolStrip control in order to be merged with the parent's
        /// </summary>
        ToolStrip ChildToolStrip { get; set; }

        /// <summary>
        /// Sets the enabled/disabled state of the ToolStripButtons in the parent MDI form
        /// </summary>
        void ShowHideSettings();

        /// <summary>
        /// Gets the state of the splitContainer1.Panel1Collapsed
        /// </summary>
        /// <returns>True if the PropertyGrid is visible, false if it is collapsed</returns>
        bool PanelCollapsed();

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

}