using System.Globalization;

namespace ErgoCalc;

public partial class FrmZoom : Form
{
    private CultureInfo _culture;
    public int ZoomLevel { get; private set; } = 100;

    public FrmZoom()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Creat a new instance of this form where the user can modify the text zoom level in the results windows
    /// </summary>
    /// <param name="zoomLevel">Defalut value to show</param>
    /// <param name="culture">Culture to set the control's text properties</param>
    public FrmZoom(int zoomLevel, CultureInfo culture)
        :this()
    {
        _culture = culture;
        ZoomLevel = zoomLevel;
        this.updZoomFactor.Value = (decimal)ZoomLevel;
        UpdateUI_Language(culture);
        RelocateControls();
    }

    private void Accept(object sender, EventArgs e)
    {
        this.DialogResult = DialogResult.None;
        ZoomLevel = (int)this.updZoomFactor.Value;
        this.DialogResult = DialogResult.OK;
    }

    private void updZoom_ValueChanged(object sender, EventArgs e)
    {
        int ratio = Convert.ToInt32(updZoomFactor.Value);
        if (trackZoomFactor.Value != ratio) trackZoomFactor.Value = ratio;
    }

    private void trackZoom_ValueChanged(object sender, EventArgs e)
    {
        decimal ratio = (decimal)trackZoomFactor.Value;
        if (updZoomFactor.Value != ratio) updZoomFactor.Value = ratio;
    }

    /// <summary>
    /// Update the form's interface language
    /// </summary>
    /// <param name="culture">Culture used to display the UI</param>
    private void UpdateUI_Language(System.Globalization.CultureInfo culture)
    {
        this.btnAccept.Text = StringResources.BtnAccept;
        this.btnCancel.Text = StringResources.BtnCancel;
        this.lblZoomFactor.Text = StringResources.LblZoomFactor;
    }

    /// <summary>
    /// Relocate controls to compensate for the culture text length in labels
    /// </summary>
    private void RelocateControls()
    {
        this.updZoomFactor.Left = this.lblZoomFactor.Left + this.lblZoomFactor.Width + 3;
    }
}
