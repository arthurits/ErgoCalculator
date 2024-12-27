using System.Globalization;
using System.Text.Json;

using ErgoCalc.Models.ThermalComfort;
using PsychroLib;

namespace ErgoCalc;

public partial class FrmResultsTC : Form, IChildResults
{
    // Variable definition
    private Job _job = new();
    private readonly System.Globalization.CultureInfo _culture = System.Globalization.CultureInfo.CurrentCulture;

    public FrmResultsTC()
    {
        InitializeComponent();
        InitializePlots();

        this.Icon = GraphicsResources.Load<Icon>(GraphicsResources.AppLogo);
        this.ActiveControl = this.rtbShowResult;
    }

    public FrmResultsTC(object? data = null, System.Globalization.CultureInfo? culture = null, ModelType? model = null)
        :this()
    {
        if (data is not null && data?.GetType() == typeof(Job))
            _job = (Job)data;

        _culture = culture ?? System.Globalization.CultureInfo.CurrentCulture;
        Model = model;
    }

    private void FrmResultsTC_Activated(object sender, EventArgs e)
    {
        this.ActiveControl = this.rtbShowResult;
    }

    private void FrmResultsTC_Shown(object sender, EventArgs e)
    {
        ShowResults();
    }

    #region Private routines

    /// <summary>
    /// Computes the PMV and PPD indexes and shows the results in the RichTextBox control
    /// </summary>
    /// <param name="compute">False if the index is already computed, true otherwise</param>
    private void ShowResults(bool compute = true)
    {
        // Variable definition
        bool result = false;

        if (compute)
            result = ThermalComfort.ComfortPMV(_job);

        // If computation is OK, then call the routine that shows the results
        if (result == true)
            UpdateOutput(_culture);
    }

    private void InitializePlots()
    {
        // Draw the basic lines
        var CPsy = new Psychrometrics(UnitSystem.SI);
        var abscissa = new double[360 - 100];
        for (int i = 100; i < 360; i++)
        {
            abscissa[i - 100] = i / 10.0;
        }

        List<double[]> OrdinateVal = new List<double[]>();
        for (int j = 10; j <= 100; j += 10)
        {
            var line = new double[360 - 100];
            for (int i = 100; i < 360; i++)
            {
                line[i - 100] = 1000 * CPsy.GetHumRatioFromRelHum(i / 10.0, j / 100.0, 101325);
            }
            OrdinateVal.Add(line);
            formsPlot1.Plot.AddScatter(abscissa, line, markerShape: ScottPlot.MarkerShape.none, color: Color.LightGray);
        }

        formsPlot1.Plot.XLabel(StringResources.ThermalComfort_PlotAbscissa);
        formsPlot1.Plot.YLabel(StringResources.ThermalComfort_PlotOrdinate);
    }

    private void CreatePlots(CultureInfo? culture)
    {
        // Set plots's culture
        formsPlot1.CultureUI = culture ?? _culture;
        formsPlot2.CultureUI = culture ?? _culture;

        InitializePlots();

        // Delete any poits if any
        // https://github.com/ScottPlot/ScottPlot/discussions/673
        foreach (var chart in formsPlot1.Plot.GetPlottables().Where(x => x is ScottPlot.Plottable.ScatterPlot && ((ScottPlot.Plottable.ScatterPlot)x).Ys.Length == 1))
            formsPlot1.Plot.Remove(chart);
        formsPlot2.Plot.Clear();

        // Psychrometric plot
        var CPsy = new Psychrometrics(UnitSystem.SI);
        int i = 0;
        foreach (TaskModel task in _job.Tasks)
        {
            var result = 1000 * CPsy.GetHumRatioFromRelHum(task.Data.TempAir, task.Data.RelHumidity / 100, 101325);
            formsPlot1.Plot.AddPoint(task.Data.TempAir, result, label: $"{StringResources.Case} {((char)('A' + i)).ToString(_culture)}", size: 7);
            i++;
        }
        var legendA = formsPlot1.Plot.RenderLegend();
        var bitmapA = new Bitmap(legendA.Width + 2, legendA.Height + 2);
        using Graphics GraphicsA = Graphics.FromImage(bitmapA);
        GraphicsA.DrawRectangle(new Pen(Color.Black),
                                0,
                                0,
                                legendA.Width + 1,
                                legendA.Height + 1);
        GraphicsA.DrawImage(legendA, 1, 1);
        pictureBox1.Image = bitmapA;

        formsPlot1.Render();

        // Heat-loss plot
        int nSeries = _job.Tasks.Length;
        string[] xsLabels = new string[nSeries];
        double[] xsStacked = new double[nSeries];
        double[] HL_1 = new double[nSeries];
        double[] HL_2 = new double[nSeries];
        double[] HL_3 = new double[nSeries];
        double[] HL_4 = new double[nSeries];
        double[] HL_5 = new double[nSeries];
        double[] HL_6 = new double[nSeries];
        i = 0;
        foreach(TaskModel task in _job.Tasks)
        {
            xsLabels[i] = $"{StringResources.Case} {((char)('A' + i)).ToString(_culture)}";
            xsStacked[i] = i;
            HL_1[i] = task.Variables.HL_Skin;
            HL_2[i] = HL_1[i] + task.Variables.HL_Sweating;
            HL_3[i] = HL_2[i] + task.Variables.HL_Latent;
            HL_4[i] = HL_3[i] + task.Variables.HL_Dry;
            HL_5[i] = HL_4[i] + task.Variables.HL_Radiation;
            HL_6[i] = HL_5[i] + task.Variables.HL_Convection;
            i++;
        }

        // Plot the bar charts in reverse order (highest first)
        //formsPlot2.plt.Legend(backColor: Color.White, shadowDirection: shadowDirection.none, location: legendLocation.lowerRight, fixedLineWidth: true);
        formsPlot2.Plot.Palette = ScottPlot.Palette.Nord;

        string[] strLegend = StringResources.ThermalComfort_PlotLegend.Split(", ");
        formsPlot2.Plot.AddBar(HL_6, xsStacked).Label = strLegend[0];
        formsPlot2.Plot.AddBar(HL_5, xsStacked).Label = strLegend[1];
        formsPlot2.Plot.AddBar(HL_4, xsStacked).Label = strLegend[2];
        formsPlot2.Plot.AddBar(HL_3, xsStacked).Label = strLegend[3];
        formsPlot2.Plot.AddBar(HL_2, xsStacked).Label = strLegend[4];
        formsPlot2.Plot.AddBar(HL_1, xsStacked).Label = strLegend[5];
        formsPlot2.Plot.XTicks(xsStacked, xsLabels);

        var legendB = formsPlot2.Plot.RenderLegend();
        var bitmapB = new Bitmap(legendB.Width + 2, legendB.Height + 2);
        using Graphics GraphicsB = Graphics.FromImage(bitmapB);
        GraphicsB.DrawRectangle(new Pen(Color.Black),
                                0,
                                0,
                                legendB.Width + 1,
                                legendB.Height + 1);
        GraphicsB.DrawImage(legendB, 1, 1);
        pictureBox2.Image = bitmapB;

        formsPlot2.Render();

    }

    private void SerializeToJSON(Utf8JsonWriter writer)
    {
        writer.WriteStartObject();
        writer.WriteString("Document type", StringResources.DocumentTypeTC);
        writer.WriteNumber("Number of tasks", _job.NumberTasks);

        writer.WritePropertyName("Tasks");
        writer.WriteStartArray();

        foreach (TaskModel task in _job.Tasks)
        {
            writer.WriteStartObject();

            writer.WritePropertyName("Data");
            writer.WriteStartObject();
            writer.WriteNumber("Air temperature", task.Data.TempAir);
            writer.WriteNumber("Radiant temperature", task.Data.TempRad);
            writer.WriteNumber("Air velocity", task.Data.Velocity);
            writer.WriteNumber("Relative humidity", task.Data.RelHumidity);
            writer.WriteNumber("Clothing", task.Data.Clothing);
            writer.WriteNumber("Metabolic rate", task.Data.MetRate);
            writer.WriteNumber("External work", task.Data.ExternalWork);
            writer.WriteEndObject();

            writer.WritePropertyName("Variables");
            writer.WriteStartObject();
            writer.WriteNumber("PMV", task.Variables.PMV);
            writer.WriteNumber("PPD", task.Variables.PPD);
            writer.WriteNumber("Heat loss - Skin", task.Variables.HL_Skin);
            writer.WriteNumber("Heat loss - Sweating", task.Variables.HL_Sweating);
            writer.WriteNumber("Heat loss - Latent", task.Variables.HL_Latent);
            writer.WriteNumber("Heat loss - Dry", task.Variables.HL_Dry);
            writer.WriteNumber("Heat loss - Radiation", task.Variables.HL_Radiation);
            writer.WriteNumber("Heat loss - Convection", task.Variables.HL_Convection);
            writer.WriteEndObject();

            writer.WriteEndObject();
        }

        writer.WriteEndArray();
        writer.WriteEndObject();

        writer.Flush();
    }

    #endregion Private routines

    #region IChildResults interface
    public ToolStrip? ChildToolStrip { get => null; set { } }

    public ModelType? Model { get; set; }

    public bool[] GetToolbarEnabledState() => [true, true, true, false, true, true, true, true, true, false, false, true, true, true];

    public bool OpenFile(JsonDocument document)
    {
        bool result = true;
        Job job = new();
        JsonElement root = document.RootElement;
        job.NumberTasks = root.GetProperty("Number of tasks").GetInt32();
        job.Tasks = new TaskModel[job.NumberTasks];

        int i = 0;
        try
        {
            foreach (JsonElement curve in root.GetProperty("Tasks").EnumerateArray())
            {
                job.Tasks[i] = new();
                job.Tasks[i].Data.TempAir = curve.GetProperty("Data").GetProperty("Air temperature").GetDouble();
                job.Tasks[i].Data.TempRad = curve.GetProperty("Data").GetProperty("Radiant temperature").GetDouble();
                job.Tasks[i].Data.Velocity = curve.GetProperty("Data").GetProperty("Air velocity").GetDouble();
                job.Tasks[i].Data.RelHumidity = curve.GetProperty("Data").GetProperty("Relative humidity").GetDouble();
                job.Tasks[i].Data.Clothing = curve.GetProperty("Data").GetProperty("Clothing").GetDouble();
                job.Tasks[i].Data.MetRate = curve.GetProperty("Data").GetProperty("Metabolic rate").GetDouble();
                job.Tasks[i].Data.ExternalWork = curve.GetProperty("Data").GetProperty("External work").GetDouble();

                job.Tasks[i].Variables.PMV = curve.GetProperty("Variables").GetProperty("PMV").GetDouble();
                job.Tasks[i].Variables.PPD = curve.GetProperty("Variables").GetProperty("PPD").GetDouble();
                job.Tasks[i].Variables.HL_Skin = curve.GetProperty("Variables").GetProperty("Heat loss - Skin").GetDouble();
                job.Tasks[i].Variables.HL_Sweating = curve.GetProperty("Variables").GetProperty("Heat loss - Sweating").GetDouble();
                job.Tasks[i].Variables.HL_Latent = curve.GetProperty("Variables").GetProperty("Heat loss - Latent").GetDouble();
                job.Tasks[i].Variables.HL_Dry = curve.GetProperty("Variables").GetProperty("Heat loss - Dry").GetDouble();
                job.Tasks[i].Variables.HL_Radiation = curve.GetProperty("Variables").GetProperty("Heat loss - Radiation").GetDouble();
                job.Tasks[i].Variables.HL_Convection = curve.GetProperty("Variables").GetProperty("Heat loss - Convection").GetDouble();

                i++;
            }

            _job = job;
        }
        catch (Exception)
        {
            result = false;
        }

        return result;
    }

    public string Save(string directoryPath)
    {
        DialogResult result;
        string userPath = string.Empty;

        // Displays a SaveFileDialog so the user can save the Image  
        SaveFileDialog SaveDlg = new()
        {
            DefaultExt = "*.csv",
            Filter = "ERGO file (*.ergo)|*.ergo|RTF file (*.rtf)|*.rtf|Text file (*.txt)|*.txt|All files (*.*)|*.*",
            FilterIndex = 1,
            FileName = "Thermal comfort",
            Title = "Save thermal comfort results",
            OverwritePrompt = true,
            InitialDirectory = string.IsNullOrWhiteSpace(directoryPath) ? Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) : directoryPath
        };

        using (new CenterWinDialog(this))
        {
            result = SaveDlg.ShowDialog(this.Parent);
        }

        // If the file name is not an empty string open it for saving.  
        if (result == DialogResult.OK && SaveDlg.FileName != "")
        {
            using var fs = SaveDlg.OpenFile();

            // Saves the text via a FileStream created by the OpenFile method.  
            if (fs is not null)
            {
                // Get the actual directory path selected by the user in order to store it later in the settings
                userPath = Path.GetDirectoryName(SaveDlg.FileName) ?? string.Empty;

                // Saves the text in the appropriate TextFormat based upon the File type selected in the dialog box.  
                // NOTE that the FilterIndex property is one-based. 
                switch (SaveDlg.FilterIndex)
                {
                    case 1:
                        {
                            using var writer = new Utf8JsonWriter(fs, options: new JsonWriterOptions { Indented = true });
                            SerializeToJSON(writer);
                            //var jsonString = JsonSerializer.Serialize(_datos[0]._points[0], new JsonSerializerOptions { WriteIndented = true });
                        }
                        break;
                    case 2:
                        rtbShowResult.SaveFile(fs, RichTextBoxStreamType.RichText);
                        break;
                    case 3:
                        rtbShowResult.SaveFile(fs, RichTextBoxStreamType.PlainText);
                        break;
                    case 4:
                        rtbShowResult.SaveFile(fs, RichTextBoxStreamType.UnicodePlainText);
                        break;
                }

                FrmMain.SetFormTitle(this, StringResources.FormResultsTC, SaveDlg.FileName);

                using (new CenterWinDialog(this.MdiParent))
                {
                    MessageBox.Show(this, "The file was successfully saved", "File saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        return userPath;
    }

    public void UpdateOutput(System.Globalization.CultureInfo culture)
    {
        rtbShowResult.Text = _job.ToString(StringResources.ThermalComfort_ResultsHeaders, culture);
        CreatePlots(culture);
        FormatText();
    }

    public void FormatText()
    {
        // Set the control's tabs
        rtbShowResult.SelectAll();
        using var g = rtbShowResult.CreateGraphics();
        rtbShowResult.SelectionTabs = (this as IChildResults).ComputeTabs(g,
                                                                        rtbShowResult.Font,
                                                                        _job.NumberTasks,
                                                                        StringResources.ThermalComfort_RowHeaders,
                                                                        StringResources.ThermalComfort_ColumnHeaders);
        rtbShowResult.DeselectAll();

        // Format (font, size, and style) the text
        int nStart = 0, nEnd = 0;
        while (true)
        {
            // Underline
            nStart = rtbShowResult.Find(StringResources.ThermalComfort_Data, nStart + 1, -1, RichTextBoxFinds.MatchCase);
            if (nStart == -1) break;
            nEnd = rtbShowResult.Find(Environment.NewLine.ToCharArray(), nStart + 1);
            rtbShowResult.Select(nStart, nEnd - nStart);
            rtbShowResult.SelectionFont = new Font(rtbShowResult.SelectionFont ?? rtbShowResult.Font, FontStyle.Underline | FontStyle.Bold);

            nStart = rtbShowResult.Find(StringResources.ThermalComfort_Multipliers, nStart + 1, -1, RichTextBoxFinds.MatchCase);
            if (nStart == -1) break;
            nEnd = rtbShowResult.Find(Environment.NewLine.ToCharArray(), nStart + 1);
            rtbShowResult.Select(nStart, nEnd - nStart);
            rtbShowResult.SelectionFont = new Font(rtbShowResult.SelectionFont ?? rtbShowResult.Font, FontStyle.Underline | FontStyle.Bold);
        }

        // Bold results
        nStart = 0;
        while (true)
        {
            nStart = rtbShowResult.Find(StringResources.ThermalComfort_PMVindex, nStart + 1, -1, RichTextBoxFinds.MatchCase);
            if (nStart == -1) break;
            //nEnd = rtbShowResult.Text.Length;
            nEnd = rtbShowResult.Find(Environment.NewLine.ToCharArray(), nStart + 1);
            rtbShowResult.Select(nStart, nEnd - nStart);
            rtbShowResult.SelectionFont = new Font(rtbShowResult.SelectionFont?.FontFamily ?? rtbShowResult.Font.FontFamily, rtbShowResult.Font.Size + 1, FontStyle.Bold);

            nStart = rtbShowResult.Find(StringResources.ThermalComfort_PPDindex, nStart + 1, -1, RichTextBoxFinds.MatchCase);
            if (nStart == -1) break;
            //nEnd = rtbShowResult.Text.Length;
            nEnd = rtbShowResult.Find(Environment.NewLine.ToCharArray(), nStart + 1);
            rtbShowResult.Select(nStart, nEnd - nStart);
            rtbShowResult.SelectionFont = new Font(rtbShowResult.SelectionFont?.FontFamily ?? rtbShowResult.Font.FontFamily, rtbShowResult.Font.Size + 1, FontStyle.Bold);
        }

        // Set the cursor at the beginning of the text
        rtbShowResult.SelectionStart = 0;
        rtbShowResult.SelectionLength = 0;
    }

    public void EditData()
    {
        using FrmDataTC frm = new(_job, _culture);

        if (frm.ShowDialog(this) == DialogResult.OK)
        {
            object data = frm.GetData;
            if (data.GetType() == typeof(Job))
                _job = (Job)data;
            else
                _job = new();

            ShowResults();
        }
    }

    public void Duplicate()
    {
        // Mostrar la ventana de resultados
        FrmResultsTC frmResults = new(_job, _culture, Model)
        {
            MdiParent = this.MdiParent
        };

        int index = this.Text.IndexOf(StringResources.FormTitleUnion) > -1 ? this.Text.IndexOf(StringResources.FormTitleUnion) + StringResources.FormTitleUnion.Length : this.Text.Length;
        FrmMain.SetFormTitle(frmResults, StringResources.FormResultsTC, this.Text[index..]);

        frmResults.rtbShowResult.Font = this.rtbShowResult.Font;
        frmResults.rtbShowResult.ForeColor = this.rtbShowResult.ForeColor;
        frmResults.rtbShowResult.BackColor = this.rtbShowResult.BackColor;
        frmResults.rtbShowResult.ZoomFactor = this.rtbShowResult.ZoomFactor;
        frmResults.rtbShowResult.WordWrap = this.rtbShowResult.WordWrap;

        frmResults.Show();
    }

    #endregion IChildResults interface

}