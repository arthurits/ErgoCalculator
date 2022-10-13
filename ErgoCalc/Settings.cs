

using System.Drawing;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;
using System;

namespace ErgoCalc;

public partial class FrmMain
{
    /// <summary>
    /// Loads all settings from file _settings.FileName into instance <see cref="AppSettings">_settings</see>.
    /// Shows MessageBox error if unsuccessful.
    /// </summary>
    /// <returns><see langword="True"/> if successful, <see langword="false"/> otherwise</returns>
    private void LoadProgramSettingsJSON()
    {
        try
        {
            var jsonString = File.ReadAllText(_settings.SettingsFileName);
            _settings = JsonSerializer.Deserialize<AppSettings>(jsonString);

            if (_settings.WindowPosition)
            {
                this.StartPosition = FormStartPosition.Manual;
                this.DesktopLocation = new Point(_settings.Left, _settings.Top);
                this.ClientSize = new Size(_settings.Width, _settings.Height);
            }
        }
        catch (FileNotFoundException)
        {
        }
        catch (Exception ex)
        {
            using (new CenterWinDialog(this))
            {
                MessageBox.Show(this,
                    "Error loading settings file\n\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
    /// <summary>
    /// Saves the current program settings from <see cref="AppSettings">_settings</see> into _settings.FileName.
    /// </summary>
    private void SaveProgramSettingsJSON()
    {
        _settings.Left = DesktopLocation.X;
        _settings.Top = DesktopLocation.Y;
        _settings.Width = ClientSize.Width;
        _settings.Height = ClientSize.Height;

        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };
        var jsonString = JsonSerializer.Serialize(_settings, options);
        File.WriteAllText(_settings.SettingsFileName, jsonString);
    }

    /// <summary>
    /// Update UI with settings
    /// </summary>
    /// <param name="WindowSettings"><see langword="True"/> if the window position and size should be applied. <see langword="False"/> if omitted</param>
    private void ApplySettingsJSON(bool WindowPosition = false)
    {
        if (WindowPosition)
        {
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.DesktopLocation = new Point(_settings.Left, _settings.Top);
            this.ClientSize = new Size(_settings.Width, _settings.Height);
        }
    }
}
