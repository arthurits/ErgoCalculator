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
    public class ChartOptions
    {
        #region Propiedades de la clase
        private LiveCharts.WinForms.CartesianChart _chart;
        private LiveCharts.Wpf.AxesCollection _axisX;
        private LiveCharts.Wpf.AxesCollection _axisY;
        LiveCharts.Wpf.LineSeries _serie;
        //private ZedGraph.GraphPane _zedG;
        private Int32 _nCurva;
        #endregion

        /// <summary>
        /// Constructor de la clase. Inicializar propiedades.
        /// </summary>
        public ChartOptions(LiveCharts.WinForms.CartesianChart chart, Int32 i)
        {
            _chart = chart;
            _nCurva = i;
        }

        [Browsable(false)]
        public LiveCharts.WinForms.CartesianChart chart
        {
            set { _chart = value; }
        }

        [Browsable(false)]
        public Int32 NúmeroCurva
        {
            get { return _nCurva; }
            set
            {
                _nCurva = value;
                if (value > 0) _serie = (LiveCharts.Wpf.LineSeries)_chart.Series[_nCurva];
            }
        }

        /// <summary>
        /// Propiedades públicas de la clase
        /// </summary>
        [Browsable(true),
        ReadOnly(false),
        Description("Texto del eje de abscisas"),
        Category("Text and labels")]
        public string AxisXTitle
        {
            get
            {
                LiveCharts.Wpf.AxesCollection axis = _chart.AxisX;
                return ((LiveCharts.Wpf.AxesCollection)_chart.AxisX)[0].Title;
                //return axis[0].Title;
            }
            set
            {
                ((LiveCharts.Wpf.AxesCollection)_chart.AxisX)[0].Title = value;
            }
        }

        /// <summary>
        /// Propiedades públicas de la clase
        /// </summary>
        [Browsable(true),
        ReadOnly(false),
        Description("Texto del eje de ordenadas"),
        Category("Text and labels")]
        public string AxisYTitle
        {
            get
            {
                return ((LiveCharts.Wpf.AxesCollection)_chart.AxisY)[0].Title;
            }
            set
            {
                ((LiveCharts.Wpf.AxesCollection)_chart.AxisY)[0].Title = value;
            }
        }


        /// <summary>
        /// Propiedades públicas de la clase
        /// </summary>
        [Browsable(true),
        ReadOnly(false),
        Description("Color de la serie de datos"),
        Category("Series options")]
        public Color CurvaColor
        {
            get
            {
                LiveCharts.Wpf.LineSeries serie = (LiveCharts.Wpf.LineSeries)_chart.Series[_nCurva];
                var drawingcolor = System.Drawing.Color.Red;
                if (serie.Stroke!=null)
                {
                    System.Windows.Media.Color mediacolor = ((System.Windows.Media.SolidColorBrush)serie.Stroke).Color;
                    drawingcolor = System.Drawing.Color.FromArgb(mediacolor.A, mediacolor.R, mediacolor.G, mediacolor.B);
                }
                

                return _nCurva == -1 ? Color.Red : drawingcolor; //.CurveList[_nCurva].Color;
            }
            set
            {
                if (_nCurva == -1) ;
                //errorNoCurveSelected();
                else
                    ((LiveCharts.Wpf.LineSeries)_chart.Series[_nCurva]).Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(value.A, value.R, value.G, value.B));
            }
        }

        /// <summary>
        /// Propiedades públicas de la clase
        /// </summary>
        [Browsable(true),
        ReadOnly(false),
        Description("Line width"),
        Category("Series options")]
        public double Width
        {
            get { return _serie.StrokeThickness; }
            set { _serie.StrokeThickness = value; }
        }

        /// <summary>
        /// Propiedades públicas de la clase
        /// </summary>
        [Browsable(true),
        ReadOnly(false),
        Description("Line smoothness"),
        Category("Series options")]
        public double Smoothness
        {
            get { return _serie.LineSmoothness; }
            set { _serie.LineSmoothness = value; }
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
            get { return _serie.Title; }
            set { _serie.Title = value; }
        }

        /// <summary>
        /// Propiedades públicas de la clase
        /// </summary>
        [Browsable(true),
        ReadOnly(false),
        Description("Serie's Visibility"),
        Category("Series options")]
        public System.Windows.Visibility Visibility
        {
            get { return _serie.Visibility; }
            set { _serie.Visibility = value; }
        }


        /* [DescriptionAttribute("Color de la serie de datos"),
        CategoryAttribute("Series options")]
        public Color SerieColor
        {
            get
            {
                return _nCurva == -1 ? Color.Red : ((LineItem)_zedG.CurveList[_nCurva]).Color;
            }
            set { ((LineItem)_zedG.CurveList[_nCurva]).Color = value; }
        }*/
        /*
        [SRDescriptionAttribute("PropertyDescriptionWidth"),
        SRCategoryAttribute("PropertyCategorySerie")]
        public float SerieGrueso
        {
            get
            {
                return _nCurva == -1 ? -1 : ((LineItem)_zedG.CurveList[_nCurva]).Line.Width;
            }
            set
            {
                if (_nCurva == -1)
                    errorNoCurveSelected();
                else
                    ((LineItem)_zedG.CurveList[_nCurva]).Line.Width = value;
            }
        }

        [SRDescriptionAttribute("PropertyDescriptionStyle"),
        SRCategoryAttribute("PropertyCategorySerie")]
        public DashStyle SerieEstilo
        {
            get
            {
                return _nCurva == -1 ? DashStyle.Solid : ((LineItem)_zedG.CurveList[_nCurva]).Line.Style;
            }
            set
            {
                if (_nCurva == -1)
                    errorNoCurveSelected();
                else
                    ((LineItem)_zedG.CurveList[_nCurva]).Line.Style = value;
            }
        }
        */
        /* [DescriptionAttribute("Símbolo de la serie de datos"),
        CategoryAttribute("Series options")]
        public SymbolType Símbolo
        {
            get
            {
                return _nCurva == -1 ? SymbolType.Default : ((LineItem)_zedG.CurveList[_nCurva]).Symbol.Type;
            }
            set { ((LineItem) _zedG.CurveList[_nCurva]).Symbol.Type = value; }
        }*/

        /* [DescriptionAttribute("Color del símbolo de la serie de datos"),
        CategoryAttribute("Series options")]
        public Color SímboloColor
        {
            get
            {
                return _nCurva == -1 ? Color.Red : ((LineItem)_zedG.CurveList[_nCurva]).Symbol.Fill.Color;
            }
            set { ((LineItem)_zedG.CurveList[_nCurva]).Symbol.Fill.Color = value; }
        } */

        /* [DescriptionAttribute("Tamaño del símbolo de la serie de datos"),
        CategoryAttribute("Series options")]
        public float SímboloTamaño
        {
            get
            {
                return _nCurva == -1 ? -1 : ((LineItem)_zedG.CurveList[_nCurva]).Symbol.Size;
            }
            set { ((LineItem)_zedG.CurveList[_nCurva]).Symbol.Size  = value; }
        } */
        /*
        [SRDescriptionAttribute("PropertyDescriptionLabelSerie"),
        SRCategoryAttribute("PropertyCategorySerie")]
        public String SerieEtiqueta
        {
            get
            {
                return _nCurva == -1 ? String.Empty : _zedG.CurveList[_nCurva].Label.Text;
            }
            set
            {
                if (_nCurva == -1)
                    errorNoCurveSelected();
                else
                    _zedG.CurveList[_nCurva].Label.Text = value;
            }
        }

        [SRDescriptionAttribute("PropertyDescriptionLabelGraph"),
        SRCategoryAttribute("PropertyCategoryGraph")]
        public String TítuloGráfico
        {
            get { return _zedG.Title.Text; }
            set { _zedG.Title.Text = value; }
        }

        [SRDescriptionAttribute("PropertyDescriptionLabelXAxis"),
        SRCategoryAttribute("PropertyCategoryGraph")]
        public String TítuloEjeX
        {
            get { return _zedG.XAxis.Title.Text; }
            set { _zedG.XAxis.Title.Text = value; }
        }

        [SRDescriptionAttribute("PropertyDescriptionLabelYAxis"),
        SRCategoryAttribute("PropertyCategoryGraph")]
        public String TítuloEjeY
        {
            get { return _zedG.YAxis.Title.Text; }
            set { _zedG.YAxis.Title.Text = value; }
        }
        */
        //[DescriptionAttribute("Valor mínimo del eje de abscisas"),
        //CategoryAttribute("Ejes")]

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
    }

}