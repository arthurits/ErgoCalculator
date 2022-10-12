using System.Drawing;
using System.Windows.Forms;
using System;
using System.IO;

namespace ErgoCalc;

/// <summary>
/// Load graphics resources from disk
/// </summary>
public class GraphicsResources
{
    public const string AppLogo = @"images\logo.ico";
    public const string AppLogo256 = @"images\logo@256.png";
    public const string IconNew = @"images\new.ico";
    public const string IconExit = @"images\log_off.ico";
    public const string IconOpen = @"images\open.ico";
    public const string IconSave = @"images\save.ico";
    public const string IconChartSave = @"images\chart-save.ico";
    public const string IconCopy = @"images\copy.ico";
    public const string IconEdit = @"images\write.ico";
    public const string IconChartAdd = @"images\chart-add.ico";
    public const string IconChartDelete = @"images\chart-delete.ico";
    public const string IconSettings = @"images\settings.ico";
    public const string IconAbout = @"images\about.ico";

    /// <summary>
    /// Loads a graphics resource from a disk location
    /// </summary>
    /// <typeparam name="T">Type of resource to be loaded</typeparam>
    /// <param name="fileName">File name (absolute or relative to the working directory) to load resource from</param>
    /// <returns>The graphics resource</returns>
    public static T? Load<T>(string fileName)
    {
        T? resource = default;
        try
        {
            if (File.Exists(fileName))
            {
                if (typeof(T).Equals(typeof(System.Drawing.Image)))
                    resource = (T)(object)Image.FromFile(fileName);
                else if (typeof(T).Equals(typeof(Icon)))
                    resource = (T)(object)new Icon(fileName);
                else if (typeof(T).Equals(typeof(Cursor)))
                    resource = (T)(object)new Cursor(fileName);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                $"Unexpected error while loading the {fileName} graphics resource.{Environment.NewLine}{ex.Message}",
                "Loading error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
        return resource;
    }
}