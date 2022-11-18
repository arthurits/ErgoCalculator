using System.Globalization;

namespace ErgoCalc;

public partial class FrmZoom : Form
{
    private CultureInfo _culture;
    public float ZoomLevel { get; private set; } = 1f;

    public FrmZoom()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Creat a new instance of this form where the user can modify the text zoom level in the results windows
    /// </summary>
    /// <param name="zoomLevel">Defalut value to show</param>
    /// <param name="culture">Culture to set the control's text properties</param>
    public FrmZoom(float zoomLevel, CultureInfo culture)
        :this()
    {
        _culture = culture;
        ZoomLevel = zoomLevel;
        this.updZoom.Value = (decimal)ZoomLevel;
        UpdateUI_Language(culture);
        RelocateControls();
    }

    private void Accept(object sender, EventArgs e)
    {
        this.DialogResult = DialogResult.None;
        ZoomLevel = (float)this.updZoom.Value;
        this.DialogResult = DialogResult.OK;
    }

    private void updZoom_ValueChanged(object sender, EventArgs e)
    {
        int ratio = Convert.ToInt32(10 * updZoom.Value);
        if (trackZoom.Value != ratio) trackZoom.Value = ratio;
    }

    private void trackZoom_ValueChanged(object sender, EventArgs e)
    {
        decimal ratio = (decimal)trackZoom.Value / 10;
        if (updZoom.Value != ratio) updZoom.Value = ratio;
    }

    /// <summary>
    /// Update the form's interface language
    /// </summary>
    /// <param name="culture">Culture used to display the UI</param>
    private void UpdateUI_Language(System.Globalization.CultureInfo culture)
    {
        this.btnAccept.Text = StringResources.BtnAccept;
        this.btnCancel.Text = StringResources.BtnCancel;
        this.lblZoom.Text = StringResources.LblZoom;
    }

    /// <summary>
    /// Relocate controls to compensate for the culture text length in labels
    /// </summary>
    private void RelocateControls()
    {
        this.updZoom.Left = this.lblZoom.Left + this.lblZoom.Width + 3;
    }
}
