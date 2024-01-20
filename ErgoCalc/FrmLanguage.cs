using System.Globalization;
using System.Resources;

namespace ErgoCalc;

public partial class FrmLanguage : Form
{
    private CultureInfo _culture = CultureInfo.CurrentCulture;
    private readonly AppSettings? Settings;
    private readonly string _baseName = StringResources.StringRM.BaseName;

    public FrmLanguage()
    {
        InitializeComponent();
        FillDefinedCultures(_baseName, typeof(FrmMain).Assembly);
    }

    public FrmLanguage(AppSettings settings)
    : this()
    {
        Settings = settings;
        _culture = settings.AppCulture;
        UpdateControls(settings.AppCultureName);
    }

    private void Accept_Click(object sender, EventArgs e)
    {
        if (Settings is not null) Settings.AppCulture = _culture;
        DialogResult = DialogResult.OK;
    }

    private void Cancel_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
    }

    private void LabelCulture_Click(object sender, EventArgs e)
    {
        switch ((sender as Label)?.Name)
        {
            case "lblCurrentCulture":
                radCurrentCulture.Checked = true;
                break;
            case "lblInvariantCulture":
                radInvariantCulture.Checked = true;
                break;
            case "lblUserCulture":
            default:
                radUserCulture.Checked = true;
                break;
        }
    }

    private void CurrentCulture_CheckedChanged(object sender, EventArgs e)
    {
        if (radCurrentCulture.Checked)
        {
            _culture = System.Globalization.CultureInfo.CurrentCulture;
            UpdateUI_Language();

            int index = cboAllCultures.SelectedIndex;
            FillDefinedCultures(_baseName, typeof(FrmLanguage).Assembly);
            cboAllCultures.SelectedIndex = index;
        }
    }

    private void InvariantCulture_CheckedChanged(object sender, EventArgs e)
    {
        if (radInvariantCulture.Checked)
        {
            _culture = System.Globalization.CultureInfo.InvariantCulture;
            UpdateUI_Language();

            int index = cboAllCultures.SelectedIndex;
            FillDefinedCultures(_baseName, typeof(FrmLanguage).Assembly);
            cboAllCultures.SelectedIndex = index;
        }
    }

    private void UserCulture_CheckedChanged(object sender, EventArgs e)
    {
        cboAllCultures.Enabled = radUserCulture.Checked;
        if (cboAllCultures.Enabled)
        {
            _culture = new((string?)cboAllCultures.SelectedValue ?? String.Empty);
            UpdateUI_Language();

            int index = cboAllCultures.SelectedIndex;
            FillDefinedCultures(_baseName, typeof(FrmLanguage).Assembly);
            cboAllCultures.SelectedIndex = index;
        }
    }

    private void AllCultures_SelectedValueChanged(object sender, EventArgs e)
    {
        cboAllCultures.Enabled = radUserCulture.Checked;
        if (cboAllCultures.Enabled)
        {
            _culture = new((string?)cboAllCultures.SelectedValue ?? String.Empty);
            UpdateUI_Language();

            int index = cboAllCultures.SelectedIndex;
            FillDefinedCultures(_baseName, typeof(FrmLanguage).Assembly);
            cboAllCultures.SelectedIndex = index;
        }
    }

    /// <summary>
    /// Updates the form's controls with values from the settings class
    /// </summary>
    private void UpdateControls(string cultureName = "")
    {
        cboAllCultures.Enabled = false;
        if (cultureName == string.Empty)
            radInvariantCulture.Checked = true;
        else if (cultureName == System.Globalization.CultureInfo.CurrentCulture.Name)
            radCurrentCulture.Checked = true;
        else
        {
            cboAllCultures.SelectedValue = cultureName;
            radUserCulture.Checked = true;
        }
    }

    /// <summary>
    /// Databind only the cultures found in .resources files for a given type
    /// </summary>
    /// <param name="type">A type from which the resource manager derives all information for finding .resources files</param>
    private void FillDefinedCultures(string baseName, System.Reflection.Assembly assembly)
    {
        string cultureName = _culture.Name;
        //string _cultureUI = CultureInfo.CurrentUICulture.Name;

        // Retrieve the culture list using the culture currently selected. The UI culture needs to be temporarily changed
        CultureInfo.CurrentUICulture = new CultureInfo(cultureName);
        var cultures = System.Globalization.GlobalizationUtilities.GetAvailableCultures(baseName, assembly);

        cboAllCultures.DisplayMember = "DisplayName";
        cboAllCultures.ValueMember = "Name";
        cboAllCultures.DataSource = cultures.ToArray();
        cboAllCultures.SelectedValue = cultureName;

        // Reset the UI culture to its previous value
        //CultureInfo.CurrentUICulture = new(_cultureUI);
    }

    /// <summary>
    /// Update the form's interface language
    /// </summary>
    /// <param name="culture">Culture used to display the UI</param>
    private void UpdateUI_Language()
    {
        UpdateUI_Language(_culture);
    }

    /// <summary>
    /// Update the form's interface language
    /// </summary>
    /// <param name="culture">Culture used to display the UI</param>
    private void UpdateUI_Language(System.Globalization.CultureInfo culture)
    {
        StringResources.Culture = culture;

        this.Text = StringResources.FormLanguageTitle;
        this.lblCurrentCulture.Text = StringResources.RadCurrentCulture + $" ({System.Globalization.CultureInfo.CurrentCulture.Name})";
        this.lblInvariantCulture.Text = StringResources.RadInvariantCulture;
        this.lblUserCulture.Text = StringResources.RadUserCulture;
        this.btnCancel.Text = StringResources.BtnCancel;
        this.btnAccept.Text = StringResources.BtnAccept;

        // Reposition controls to compensate for the culture text length in labels
        this.lblCurrentCulture.Top = this.radCurrentCulture.Top + (this.radCurrentCulture.Height - this.lblCurrentCulture.Height) / 2;
        this.lblInvariantCulture.Top = this.radInvariantCulture.Top + (this.radInvariantCulture.Height - this.lblInvariantCulture.Height) / 2;
        this.lblUserCulture.Top = this.radUserCulture.Top + (this.radUserCulture.Height - this.lblUserCulture.Height) / 2;
    }

}