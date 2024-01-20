using System.Text.Json;

namespace ErgoCalc;

public partial class FrmMain
{
    /// <summary>
    /// Loads all settings from file _settings.FileName into instance <see cref="AppSettings">_settings</see>.
    /// Shows MessageBox error if unsuccessful.
    /// </summary>
    private void LoadProgramSettingsJSON()
    {
        try
        {
            var jsonString = File.ReadAllText(_settings.SettingsFileName);
            _settings = JsonSerializer.Deserialize<AppSettings>(jsonString) ?? _settings;
            //SetWindowPos(_settings.WindowPosition);
        }
        catch (FileNotFoundException)
        {
            _settingsFileExist = false;
        }
        catch (Exception ex)
        {
            using (new CenterWinDialog(this))
            {
                MessageBox.Show(this,
                    $"Error loading settings file{Environment.NewLine}{Environment.NewLine}{ex.Message}",
                    $"Error",
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
    /// Modifies window size and position to the values in <see cref="AppSettings">_settings</see>
    /// </summary>
    private void SetWindowPos()
    {
        this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
        this.DesktopLocation = new Point(_settings.Left, _settings.Top);
        this.ClientSize = new Size(_settings.Width, _settings.Height);
    }
}
