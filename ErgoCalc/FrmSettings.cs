﻿using System.Globalization;

namespace ErgoCalc;

public partial class FrmSettings : Form
{
    private CultureInfo _culture = CultureInfo.CurrentCulture;
    private readonly AppSettings Settings = new();
    private readonly string _baseName = StringResources.StringRM.BaseName;

    public FrmSettings()
    {
        InitializeComponent();
        FillDefinedCultures(_baseName, typeof(FrmSettings).Assembly);
    }

    public FrmSettings(AppSettings settings)
        : this()
    {
        Settings = settings;
        _culture = settings.AppCulture;
        lblFont.Text = $"{Settings.FontFamilyName}, size {Settings.FontSize}";
        lblFontStyle.Text = $"{Settings.FontStyle}";
        pctFontColor.BackColor = Color.FromArgb(Settings.FontColor);
        chkWordWrap.Checked = Settings.WordWrap;
        pctBackColor.BackColor = Color.FromArgb(Settings.TextBackColor);
        updZoomFactor.Value = (decimal)Settings.TextZoom;
        UpdateControls(settings);
    }

    private void DlgFont(object sender, EventArgs e)
    {
        FontDialog fontDlg = new()
        {
            ShowApply = false,
            ShowColor = false,
            ShowEffects = true,
            ShowHelp = false,
            FontMustExist = true,
            Font = new(Settings.FontFamilyName, Settings.FontSize, Settings.FontStyle),
            Color = Color.FromArgb(Settings.FontColor)
        };

        if (fontDlg.ShowDialog() != DialogResult.Cancel)
        {
            Settings.FontFamilyName = fontDlg.Font.Name;
            Settings.FontStyle = fontDlg.Font.Style;
            Settings.FontSize = fontDlg.Font.Size;

            this.lblFont.Text = String.Format(StringResources.LblFontName, Settings.FontFamilyName, Settings.FontSize);
            this.lblFontStyle.Text = String.Format(StringResources.LblFontStyle, Settings.FontStyle.ToString());

            // Reposition the button
            this.btnDlgFont.Left = this.lblFont.Left + this.lblFont.Width + 5;
        }
    }

    private void Accept_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.None;

        if (Settings is null) return;

        Settings.WordWrap = chkWordWrap.Checked;
        Settings.TextZoom = (int)updZoomFactor.Value;
        Settings.RememberFileDialogPath = chkDlgPath.Checked;
        Settings.WindowPosition = chkWindowPos.Checked;
        Settings.AppCulture = _culture;

        DialogResult = DialogResult.OK;
    }

    private void Cancel_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
    }

    private void Reset_Click(object sender, EventArgs e)
    {
        DialogResult DlgResult;
        using (new CenterWinDialog(this))
        {
            DlgResult = MessageBox.Show(this,
                StringResources.DlgReset,
                StringResources.DlgResetTitle,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);
        }

        if (DlgResult == DialogResult.Yes)
        {
            UpdateControls(new AppSettings());
        }
    }

    /// <summary>
    /// Event handler for <see cref="RadioButton.CheckedChanged"/> and <see cref="ComboBox.SelectionChangeCommitted"/>.
    /// </summary>
    /// <param name="sender">The source of the event</param>
    /// <param name="e">Object that contains event data, and provides a value to use for events that do not include event data</param>
    private void CultureChanged(object sender, EventArgs e)
    {
        // Dismiss events due to RadioButton.Checked set to false
        if (sender is RadioButton radioButton)
            if (!radioButton.Checked) return;

        // Set the culture value as a function of the button selected
        if (radCurrentCulture.Checked)
            _culture = System.Globalization.CultureInfo.CurrentCulture;
        else if (radInvariantCulture.Checked)
            _culture = System.Globalization.CultureInfo.InvariantCulture;
        else if (radUserCulture.Checked)
        {
            cboAllCultures.Enabled = radUserCulture.Checked;
            if (cboAllCultures.Enabled)
                _culture = new((string?)cboAllCultures.SelectedValue ?? String.Empty);
        }

        // Update the GUI with the current language
        UpdateUI_Language();

        // Fill the combo box with the culture names in the selected language
        int index = cboAllCultures.SelectedIndex;
        FillDefinedCultures(_baseName, typeof(FrmLanguage).Assembly);
        cboAllCultures.SelectedIndex = index;
    }

    /// <summary>
    /// Updates the form's controls with values from the settings class
    /// </summary>
    /// <param name="settings">Class containing the values to show on the form's controls</param>
    private void UpdateControls(AppSettings settings)
    {
        chkDlgPath.Checked = settings.RememberFileDialogPath;
        chkWindowPos.Checked = settings.WindowPosition;

        if (_culture.Name == string.Empty)
            radInvariantCulture.Checked = true;
        else if (_culture.Name == System.Globalization.CultureInfo.CurrentCulture.Name)
            radCurrentCulture.Checked = true;
        else
        {
            cboAllCultures.SelectedValue = _culture.Name;
            radUserCulture.Checked = true;
        }

        chkDlgPath.Checked = settings.RememberFileDialogPath;
        //txtDataFormat.Text = settings.DataFormat;
    }

    /// <summary>
    /// Databind only the cultures found in .resources files for a given type
    /// </summary>
    /// <param name="type">A type from which the resource manager derives all information for finding .resources files</param>
    private void FillDefinedCultures(string baseName, System.Reflection.Assembly assembly)
    {
        string cultureName = _culture.Name;

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

        this.Text = StringResources.FrmSettings;

        this.tabPlot.Text = StringResources.TabPlot;
        this.tabGUI.Text = StringResources.TabGUI;

        this.btnDlgFont.Text = StringResources.BtnDlgFont;
        this.lblFont.Text = String.Format(StringResources.LblFontName, Settings?.FontFamilyName, Settings?.FontSize);
        this.lblFontStyle.Text = String.Format(StringResources.LblFontStyle, Settings?.FontStyle.ToString());
        this.lblFontColor.Text = String.Format(StringResources.LblFontColor, Settings?.FontColor.ToString("X"));
        this.chkWordWrap.Text = StringResources.ChkWordWrap;
        this.lblBackColor.Text = String.Format(StringResources.LblBackColor, Settings?.TextBackColor.ToString("X"));
        this.lblZoomFactor.Text = StringResources.LblZoomFactor;

        this.grpCulture.Text = StringResources.GrpCulture;
        this.radCurrentCulture.Text = StringResources.RadCurrentCulture + $" ({System.Globalization.CultureInfo.CurrentCulture.Name})";
        this.radInvariantCulture.Text = StringResources.RadInvariantCulture;
        this.radUserCulture.Text = StringResources.RadUserCulture;
        this.chkDlgPath.Text = StringResources.ChkDlgPath;
        this.chkWindowPos.Text = StringResources.ChkWindowPos;
        this.lblDataFormat.Text = StringResources.LblDataFormat;

        this.btnReset.Text = StringResources.BtnReset;
        this.btnCancel.Text = StringResources.BtnCancel;
        this.btnAccept.Text = StringResources.BtnAccept;

        // Relocate controls
        RelocateControls();
    }

    /// <summary>
    /// Relocate controls to compensate for the culture text length in labels
    /// </summary>
    private void RelocateControls()
    {
        this.btnDlgFont.Left = this.lblFont.Left + this.lblFont.Width + 5;
        this.pctFontColor.Left = this.lblFontColor.Left + this.lblFontColor.Width + 5;
        this.pctBackColor.Left = this.lblBackColor.Left + this.lblBackColor.Width + 5;
        this.updZoomFactor.Left = this.lblZoomFactor.Left + this.lblZoomFactor.Width + 5;

        this.txtDataFormat.Left = this.lblDataFormat.Left + this.lblDataFormat.Width;
        this.lblDataFormat.Top = this.txtDataFormat.Top + (txtDataFormat.Height - lblDataFormat.Height) / 2;
    }

    private void UpdZoomFactor_ValueChanged(object sender, EventArgs e)
    {
        int ratio = Convert.ToInt32(updZoomFactor.Value);
        if (trackZoomFactor.Value != ratio) trackZoomFactor.Value = ratio;
    }

    private void TrackZoomFactor_ValueChanged(object sender, EventArgs e)
    {
        decimal ratio = (decimal)trackZoomFactor.Value;
        if (updZoomFactor.Value != ratio) updZoomFactor.Value = ratio;
    }

    private void FontColor_Click(object sender, EventArgs e)
    {
        ColorDialog colorDlg = new()
        {
            AllowFullOpen = true,
            FullOpen = true,
            AnyColor = true,
            Color = Color.FromArgb(Settings.FontColor)
        };
        if (colorDlg.ShowDialog(this) == DialogResult.OK)
        {
            pctFontColor.BackColor = colorDlg.Color;
            Settings.FontColor = colorDlg.Color.ToArgb();
        }
    }

    private void BackColor_Click(object sender, EventArgs e)
    {
        ColorDialog colorDlg = new()
        {
            AllowFullOpen = true,
            FullOpen = true,
            AnyColor = true,
            Color = Color.FromArgb(Settings.TextBackColor)
        };
        if (colorDlg.ShowDialog(this) == DialogResult.OK)
        {
            pctBackColor.BackColor = colorDlg.Color;
            Settings.TextBackColor = colorDlg.Color.ToArgb();
        }
    }
}