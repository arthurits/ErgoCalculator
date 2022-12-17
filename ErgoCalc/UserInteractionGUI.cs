﻿using System.Text;
using System.Text.Json;

namespace ErgoCalc;

public enum ModelType
{
    WorkRest,
    CumulativeLifting,
    NioshLifting,
    StrainIndex,
    OcraCheck,
    MetabolicRate,
    ThermalComfort,
    LibertyMutual,
}

partial class FrmMain
{
    private void New_Click(object sender, EventArgs e)
    {
        FrmNew frmNew = new();
        frmNew.Icon = GraphicsResources.Load<Icon>(GraphicsResources.AppLogo);

        if (frmNew.ShowDialog() == DialogResult.Cancel) return;

        Form frmData = frmNew.Model switch
        {
            ModelType.WorkRest => new FrmDataWR(_settings.AppCulture),
            ModelType.CumulativeLifting => new FrmDataCLM(_settings.AppCulture),
            ModelType.NioshLifting => new FrmDataNIOSH(_settings.AppCulture),
            ModelType.StrainIndex => new FrmDataStrainIndex(_settings.AppCulture),
            ModelType.OcraCheck => new FrmDataOCRAcheck(),
            ModelType.MetabolicRate => new FrmDataMet(),
            ModelType.ThermalComfort => new FrmDataTC(_settings.AppCulture),
            ModelType.LibertyMutual => new FrmDataLiberty(_settings.AppCulture),
            _ => new Form()
        };

        if (frmData is IChildData frm)
        {
            if (frmData.ShowDialog(this) == DialogResult.OK)
            {
                Form frmResults = frmNew.Model switch
                {
                    ModelType.WorkRest => new FrmResultsWR(frm.GetData),
                    ModelType.CumulativeLifting => new FrmResultsCLM(frm.GetData, _settings.AppCulture),
                    ModelType.NioshLifting => new FrmResultNIOSH(frm.GetData, _settings.AppCulture),
                    ModelType.StrainIndex => new FrmResultsStrainIndex(frm.GetData, _settings.AppCulture),
                    ModelType.OcraCheck => new FrmResultsOCRAcheck(frm.GetData),
                    ModelType.MetabolicRate => new FrmResultsMet(frm.GetData),
                    ModelType.ThermalComfort => new FrmResultsTC(frm.GetData, _settings.AppCulture),
                    ModelType.LibertyMutual => new FrmResultsLiberty(frm.GetData, _settings.AppCulture),
                    _ => new Form()
                };
                frmResults.MdiParent = this;
                SetFormTitle(frmResults, frmResults.Text);
                FormatRichText(frmResults.ActiveControl,
                    _settings.FontFamilyName,
                    _settings.FontSize,
                    _settings.FontStyle,
                    _settings.FontColor,
                    _settings.TextBackColor,
                    _settings.TextZoom,
                    _settings.WordWrap);
                //frmResults.Icon = GraphicsResources.Load<Icon>(GraphicsResources.AppLogo);
                frmResults.Show();
            }
            frmData.Dispose();
        }
    }

    private void Exit_Click(object sender, EventArgs e)
    {
        // Exit the application by calling the frmMain_FormClosing event
        this.Close();
    }

    private void About_Click(object sender, EventArgs e)
    {
        FrmAbout frmAbout = new();
        frmAbout.ShowDialog();
    }

    private void Open_Click(object sender, EventArgs e)
    {
        OpenFileDialog openDlg = new()
        {
            DefaultExt = "*.ergo",
            Filter = "ERGO file (*.ergo)|*.ergo|All files (*.*)|*.*",
            FilterIndex = 1,
            Title = "Open ErgoCalc file",
            InitialDirectory = _settings.RememberFileDialogPath ? _settings.UserOpenPath : _settings.DefaultOpenPath
        };

        DialogResult result;
        using (new CenterWinDialog(this))
        {
            result = openDlg.ShowDialog(this);
        }

        // If the file name is not an empty string open it for saving.  
        if (result == DialogResult.OK && openDlg.FileName != "")
        {
            // https://stackoverflow.com/questions/897796/how-do-i-open-an-already-opened-file-with-a-net-streamreader
            using var fs = File.Open(openDlg.FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using var sr = new StreamReader(fs, Encoding.UTF8);

            string jsonString = sr.ReadToEnd();

            //string jsonString = File.ReadAllText(openDlg.FileName);
            var options = new JsonDocumentOptions { AllowTrailingCommas = true, CommentHandling = JsonCommentHandling.Skip };

            // Dilemma: this should be wrapped in a try-catch, but variables will be out of scope and syntax would be cumbersome
            using JsonDocument document = JsonDocument.Parse(jsonString, options);
            var strType = document.RootElement.TryGetProperty("Document type", out JsonElement docuValue) ? docuValue.ToString() : "Error";
            //string cultureName = document.RootElement.TryGetProperty("Culture name", out docuValue) ? docuValue.ToString() : string.Empty;

            Form? frm = strType switch
            {
                "Work-Rest model" => new FrmResultsWR(),
                "NIOSH lifting equation" => new FrmResultNIOSH(_settings.AppCulture),
                "Strain index" => new FrmResultsStrainIndex(_settings.AppCulture),
                "Thermal comfort model" => new FrmResultsTC(_settings.AppCulture),
                "LM-MMH model" => new FrmResultsLiberty(_settings.AppCulture),
                "Comprehensive lifting model" => new FrmResultsCLM(_settings.AppCulture),
                _ => default
            };

            if (frm != default)
            {

                if (((IChildResults)frm).OpenFile(document))
                {
                    frm.MdiParent = this;

                    string strTextTitle = strType switch
                    {
                        "Work-Rest model" => StringResources.FormResultsWR,
                        "NIOSH lifting equation" => StringResources.FormResultsNIOSH,
                        "Strain index" => StringResources.FormResultsStrainIndex,
                        "Thermal comfort model" => StringResources.FormResultsTC,
                        "LM-MMH model" => StringResources.FormResultsLiberty,
                        "Comprehensive lifting model" => StringResources.FormResultsCLM,
                        _ => String.Empty
                    };
                    SetFormTitle(frm, strTextTitle, openDlg.FileName);

                    FormatRichText(frm.ActiveControl,
                        _settings.FontFamilyName,
                        _settings.FontSize,
                        _settings.FontStyle,
                        _settings.FontColor,
                        _settings.TextBackColor,
                        _settings.TextZoom,
                        _settings.WordWrap);

                    frm.Show();
                }
                else
                {
                    MessageBox.Show("The document cannot be opened by this application", "Format mismatch", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        return;
    }

    private void Save_Click(object sender, EventArgs e)
    {
        if (this.ActiveMdiChild != null)
            ((IChildResults)this.ActiveMdiChild).Save(string.Empty);
    }

    private void Copy_Click(object sender, EventArgs e)
    {
        if (this.ActiveMdiChild != null)
            ((IChildResults)this.ActiveMdiChild).Duplicate();
    }

    private void EditData_Click(object sender, EventArgs e)
    {
        if (this.ActiveMdiChild != null)
            ((IChildResults)this.ActiveMdiChild).EditData();
    }

    private void Settings_Click(object sender, EventArgs e)
    {
        FrmSettings frm = new(_settings);
        frm.ShowDialog(this);
        if (frm.DialogResult == DialogResult.OK)
        {
            StatusStripFormat_SetValues(_settings.FontFamilyName,
                _settings.FontSize,
                Color.FromArgb(_settings.FontColor),
                _settings.WordWrap,
                Color.FromArgb(_settings.TextBackColor),
                _settings.TextZoom);
            UpdateUI_Language();
        }
    }

    private void Settings_EnabledChanged(object sender, EventArgs e)
    {
        if (this.toolStripMain_Settings.Enabled == false) this.toolStripMain_Settings.Checked = false;
    }

    private void Language_Click(object sender, EventArgs e)
    {
        FrmLanguage frm = new(_settings);
        //frm.Icon = GraphicsResources.Load<Icon>(GraphicsResources.AppLogo);
        frm.ShowDialog();

        if (frm.DialogResult == DialogResult.OK)
            UpdateUI_Language();

        // This is redundant because this asignment is done at the very beginning of UpdateUI_Language
        StringResources.Culture = _settings.AppCulture;
    }

    private void LabelFont_Click(object sender, EventArgs e)
    {
        FontDialog fontDlg = new()
        {
            ShowApply = false,
            ShowColor = false,
            ShowEffects = true,
            ShowHelp = false,
            FontMustExist = true
        };

        Control? control = ActiveMdiChild?.ActiveControl;
        if (control is SplitContainer)
            control = (control as SplitContainer)?.ActiveControl;

        if (control is RichTextBox richText)
        {
            fontDlg.Font = new(richText.Font.Name, richText.Font.Size, richText.Font.Style);
            if (fontDlg.ShowDialog() == DialogResult.OK)
            {
                FormatRichText(control: richText, fontName: fontDlg.Font.Name, fontSize: fontDlg.Font.Size, fontStyle: fontDlg.Font.Style);
                //richText.Font = fontDlg.Font;
                this.statusStripLabelFont.Text = String.Format(StringResources.LblFontName, richText.Font.Name, richText.Font.Size);

                if (ActiveMdiChild is IChildResults frm)
                    frm.FormatText();
            }
        }
    }

    private void LabelFontColor_Click(object sender, EventArgs e)
    {
        if (sender is null) return;

        ColorDialog colorDlg = new()
        {
            AllowFullOpen = true,
            FullOpen = true,
            AnyColor = true
        };

        Control? control = ActiveMdiChild?.ActiveControl;
        if (control is SplitContainer)
            control = (control as SplitContainer)?.ActiveControl;

        if (control is RichTextBox richText)
        {
            colorDlg.Color = richText.ForeColor;
            if (colorDlg.ShowDialog(this) == DialogResult.OK)
            {
                FormatRichText(control: richText, foreColor: colorDlg.Color.ToArgb());
                this.statusStripLabelFontColor.BackColor = colorDlg.Color;
            }
        }
    }

    private void LabelWordWrap_Click(object sender, EventArgs e)
    {
        if (sender is not null && sender is ToolStripStatusLabelEx LabelEx)
        {
            var label = LabelEx;
            label.Checked = !label.Checked;

            // Change the text color
            if (label.Checked)
                label.ForeColor = Color.Black;
            else
                label.ForeColor = Color.LightGray;

            FormatRichText(ActiveMdiChild?.ActiveControl, wordWrap: label.Checked);
        }
    }

    private void LabelBackColor_Click(object sender, EventArgs e)
    {
        if (sender is null) return;

        ColorDialog colorDlg = new()
        {
            AllowFullOpen = true,
            FullOpen = true,
            AnyColor = true
        };

        Control? control = ActiveMdiChild?.ActiveControl;
        if (control is SplitContainer)
            control = (control as SplitContainer)?.ActiveControl;

        if (control is RichTextBox richText)
        {
            colorDlg.Color = richText.BackColor;

            if (colorDlg.ShowDialog(this) == DialogResult.OK)
            {
                FormatRichText(control: richText, backColor: colorDlg.Color.ToArgb());
                this.statusStripLabelBackColor.BackColor = colorDlg.Color;
            }
        }
    }

    private void LabelZoom_Click(object sender, EventArgs e)
    {
        if (sender is null) return;

        FrmZoom frmZoom = new(_settings.TextZoom, _settings.AppCulture);
        frmZoom.Icon = GraphicsResources.Load<Icon>(GraphicsResources.AppLogo);
        if (frmZoom.ShowDialog(this) == DialogResult.OK)
        {
            FormatRichText(ActiveMdiChild?.ActiveControl, zoomFactor: frmZoom.ZoomLevel);
            _settings.TextZoom = frmZoom.ZoomLevel;
            this.statusStripLabelZoom.Text = ($"{((float)_settings.TextZoom) / 100:0.##}x").ToString(_settings.AppCulture);
        }
    }

    /// <summary>
    /// Gives formats to a <see cref="RichTextBox"/> control
    /// </summary>
    /// <param name="richText">The <see cref="RichTextBox"/> control </param>
    /// <param name="fontName">Font family name</param>
    /// <param name="fontSize">Font oem size</param>
    /// <param name="fontStyle">Font style</param>
    /// <param name="foreColor">A <see cref="color"/> that represents the control's foreground color</param>
    /// <param name="backColor">A <see cref="color"/> that represents the control's background color</param>
    /// <param name="zoomFactor">The factor by which the contents of the control is zoomed</param>
    /// <param name="wordWrap"><see langword="True"/> if the multiline text box control wraps words; <see langword="false"/> if the text box control automatically scrolls horizontally when the user types past the right edge of the control</param>
    private void FormatRichText(Control? control = null, string? fontName = null, float? fontSize = null, FontStyle? fontStyle = null, int? foreColor = null, int? backColor = null, int? zoomFactor = null, bool? wordWrap = null)
    {
        if (control is null) return;
       
        if (control is SplitContainer)
            control = (control as SplitContainer)?.ActiveControl;

        if (control is not RichTextBox richText) return;

        if (fontName is not null && fontSize is not null && fontStyle is not null)
            richText.Font = new((string)fontName, (float)fontSize, (FontStyle)fontStyle);

        if (foreColor is not null)
            richText.ForeColor = Color.FromArgb((int)foreColor);

        if (backColor is not null)
            richText.BackColor = Color.FromArgb((int)backColor);

        if (zoomFactor is not null)
            richText.ZoomFactor = ((float)zoomFactor) / 100;

        if (wordWrap is not null)
            richText.WordWrap = (bool)wordWrap;
    }

}