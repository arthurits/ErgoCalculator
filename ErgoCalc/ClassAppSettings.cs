using System;
using System.Text.Json.Serialization;

namespace ErgoCalc;

public class AppSettings
{
    /// <summary>
    /// Stores the settings file name
    /// </summary>
    [JsonIgnore]
    public string SettingsFileName { get; set; } = "configuration.json";
    /// <summary>
    /// Absolute path of the executable
    /// </summary>
    [JsonIgnore]
    public string AppPath { get; set; } = System.IO.Path.GetDirectoryName(System.Environment.ProcessPath) ?? String.Empty;

    /// <summary>
    /// Remember window position on start up
    /// </summary>
    [JsonPropertyName("Window position")]
    public bool WindowPosition { get; set; } = true;
    [JsonPropertyName("Window left")]
    public int Left { get; set; } = 0;
    [JsonPropertyName("Window top")]
    public int Top { get; set; } = 0;
    [JsonPropertyName("Window width")]
    public int Width { get; set; } = 973;
    [JsonPropertyName("Window height")]
    public int Height { get; set; } = 657;

    /// <summary>
    /// Culture used throughout the app
    /// </summary>
    [JsonIgnore]
    public System.Globalization.CultureInfo AppCulture { get; set; } = System.Globalization.CultureInfo.CurrentCulture;

    /// <summary>
    /// Define the culture used throughout the app by asigning a culture string name
    /// </summary>
    [JsonPropertyName("Culture name")]
    public string AppCultureName
    {
        get { return AppCulture.Name; }
        set { AppCulture = new System.Globalization.CultureInfo(value); }
    }

    /// <summary>
    /// True if open/save dialogs should remember the user's previous path
    /// </summary>
    [JsonPropertyName("Remember path in FileDlg?")]
    public bool RememberFileDialogPath { get; set; } = true;
    /// <summary>
    /// Default path for saving files to disk
    /// </summary>
    [JsonPropertyName("Default save path")]
    public string DefaultSavePath { get; set; } = System.Environment.GetFolderPath(System.Environment.SpecialFolder.DesktopDirectory);
    /// <summary>
    /// User-defined path for saving files to disk
    /// </summary>
    [JsonPropertyName("User save path")]
    public string UserSavePath { get; set; } = System.Environment.GetFolderPath(System.Environment.SpecialFolder.DesktopDirectory);
    /// <summary>
    /// Default path for reading files from disk
    /// </summary>
    [JsonPropertyName("Default open path")]
    public string DefaultOpenPath { get; set; } = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\examples";
    /// <summary>
    /// User-defined path for reading files from disk
    /// </summary>
    [JsonPropertyName("User open path")]
    public string UserOpenPath { get; set; } = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\examples";

    /// <summary>
    /// Font family name
    /// </summary>
    [JsonPropertyName("Font family name")]
    public string FontFamilyName { get; set; } = "Lucida Sans";

    /// <summary>
    /// Font style
    /// </summary>
    [JsonPropertyName("Font style")]
    public FontStyle FontStyle { get; set; } = FontStyle.Regular;

    /// <summary>
    /// Font size, oem
    /// </summary>
    [JsonPropertyName("Font size")]
    public float FontSize { get; set; } = 9;

    /// <summary>
    /// Font fore color
    /// </summary>
    [JsonPropertyName("Font color")]
    public int FontColor { get; set; } = Color.Black.ToArgb();

    /// <summary>
    /// RichText line word-wrap
    /// </summary>
    [JsonPropertyName("Wrap text in new line?")]
    public bool WordWrap { get; set; } = false;

    /// <summary>
    /// RichText back color
    /// </summary>
    [JsonPropertyName("Text back color")]
    public int TextBackColor { get; set; } = Color.White.ToArgb();

    /// <summary>
    /// RichText zoom
    /// </summary>
    [JsonPropertyName("Text zoom")]
    public float TextZoom { get; set; } = 1f;
}
