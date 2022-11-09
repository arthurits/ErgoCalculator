using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows.Forms;

using ErgoCalc.Models.ThermalComfort;
using PsychroLib;

namespace ErgoCalc;

public partial class FrmResultsTC : Form, IChildResults
{
    private Job _job;
    private string _strPath;

    public FrmResultsTC()
    {
        _strPath = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
        if (File.Exists(_strPath + @"\images\logo.ico")) this.Icon = new Icon(_strPath + @"\images\logo.ico");

        InitializeComponent();
        InitializePlot();

        propertyGrid1.SelectedObject = new ResultsOptions(rtbShowResult);
        splitContainer1.Panel1Collapsed = false;
        splitContainer1.SplitterDistance = 0;
        splitContainer1.SplitterWidth = 1;
        splitContainer1.IsSplitterFixed = true;
    }

    public FrmResultsTC(Job job)
        : this()
    {
        _job = job;
    }

    public FrmResultsTC(object data)
        :this()
    {
        if (data.GetType() == typeof(Job)) _job = (Job)data;
    }

    private void frmResultsTC_Shown(object sender, EventArgs e)
    {
        ShowResults();
    }

    #region Private routines

    /// <summary>
    /// Computes the PMV and PPD indexes and shows the results in the RichTextBox control
    /// </summary>
    /// <param name="Compute">False if the index is already computed, true otherwise</param>
    private void ShowResults(bool Compute = true)
    {
        // Variable definition
        bool result = false;

        if (Compute)
            result = ThermalComfort.ComfortPMV(_job);

        if (result == true)
        {
            rtbShowResult.Text = _job.ToString();
            CreatePlots();
            FormatText();
        }
    }

    private void InitializePlot()
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

        formsPlot1.Plot.XLabel("Air temperature (°C)");
        formsPlot1.Plot.YLabel("g water / kg dry air");
    }

    private void CreatePlots()
    {
        // Delete any poits if any
        // https://github.com/ScottPlot/ScottPlot/discussions/673
        foreach (var chart in formsPlot1.Plot.GetPlottables().Where(x => x is ScottPlot.Plottable.ScatterPlot && ((ScottPlot.Plottable.ScatterPlot)x).Ys.Length == 1))
            formsPlot1.Plot.Remove(chart);
        formsPlot2.Plot.Clear();

        // Psychrometric plot
        var CPsy = new Psychrometrics(UnitSystem.SI);
        int i = 0;
        foreach (Task task in _job.Tasks)
        {
            var result = 1000 * CPsy.GetHumRatioFromRelHum(task.Data.TempAir, task.Data.RelHumidity / 100, 101325);
            formsPlot1.Plot.AddPoint(task.Data.TempAir, result, label: "Case " + ((char)('A' + i)).ToString(), size: 7);
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
        foreach(Task task in _job.Tasks)
        {
            xsLabels[i] = "Case " + ((char)('A' + i)).ToString();
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
        
        formsPlot2.Plot.AddBar(HL_6, xsStacked).Label = "Convection";
        formsPlot2.Plot.AddBar(HL_5, xsStacked).Label = "Radiation";
        formsPlot2.Plot.AddBar(HL_4, xsStacked).Label = "Dry";
        formsPlot2.Plot.AddBar(HL_3, xsStacked).Label = "Latent";
        formsPlot2.Plot.AddBar(HL_2, xsStacked).Label = "Sweating";
        formsPlot2.Plot.AddBar(HL_1, xsStacked).Label = "Skin";
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
        writer.WriteString("Document type", "Thermal comfort model");
        writer.WriteNumber("Number of tasks", _job.NumberTasks);

        writer.WritePropertyName("Tasks");
        writer.WriteStartArray();

        foreach (Task task in _job.Tasks)
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
    public ToolStrip ChildToolStrip
    {
        get => null;
        set { }
    }

    public void Duplicate()
    {
        string _strPath = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);

        // Mostrar la ventana de resultados
        FrmResultsTC frmResults = new FrmResultsTC(_job);
        {
            MdiParent = this.MdiParent;
        };
        if (File.Exists(_strPath + @"\images\logo.ico")) frmResults.Icon = new Icon(_strPath + @"\images\logo.ico");
        frmResults.Show();
    }

    public void EditData()
    {
        using var frm = new FrmDataTC(_job);

        if (frm.ShowDialog(this) == DialogResult.OK)
        {
            _job = (Job)frm.GetData;
            ShowResults();
        }
    }

    public void FormatText()
    {
        int nStart = 0, nEnd = 0;

        while (true)
        {
            // Underline
            nStart = rtbShowResult.Find("Description", nStart + 1, -1, RichTextBoxFinds.MatchCase);
            if (nStart == -1) break;
            nEnd = rtbShowResult.Find(Environment.NewLine.ToCharArray(), nStart + 1);
            rtbShowResult.Select(nStart, nEnd - nStart);
            rtbShowResult.SelectionFont = new Font(rtbShowResult.SelectionFont, FontStyle.Underline | FontStyle.Bold);
        }

        // Bold results
        nStart = 0;
        while (true)
        {
            nStart = rtbShowResult.Find("The ", nStart + 1, -1, RichTextBoxFinds.MatchCase);
            if (nStart == -1) break;
            //nEnd = rtbShowResult.Text.Length;
            nEnd = rtbShowResult.Find(Environment.NewLine.ToCharArray(), nStart + 1);
            rtbShowResult.Select(nStart, nEnd - nStart);
            rtbShowResult.SelectionFont = new Font(rtbShowResult.SelectionFont.FontFamily, rtbShowResult.Font.Size + 1, FontStyle.Bold);
        }

        // Set the cursor at the beginning of the text
        rtbShowResult.SelectionStart = 0;
        rtbShowResult.SelectionLength = 0;
    }

    public bool[] GetToolbarEnabledState()
    {
        return new bool[] { true, true, true, false, true, true, false, true, true, false, false, true, true, true };
    }

    public bool OpenFile(JsonDocument document)
    {
        bool result = true;
        Job job = new();
        JsonElement root = document.RootElement;
        job.NumberTasks = root.GetProperty("Number of tasks").GetInt32();
        job.Tasks = new Task[job.NumberTasks];
        
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

        if (result)
        {
            ShowResults(false);
        }

        return result;
    }

    public bool PanelCollapsed()
    {
        return splitContainer1.SplitterDistance == 0 ? true : false;
    }

    public void Save(string path)
    {
        // Displays a SaveFileDialog so the user can save the Image  
        SaveFileDialog SaveDlg = new SaveFileDialog
        {
            DefaultExt = "*.csv",
            Filter = "ERGO file (*.ergo)|*.ergo|RTF file (*.rtf)|*.rtf|Text file (*.txt)|*.txt|All files (*.*)|*.*",
            FilterIndex = 1,
            Title = "Save thermal comfort data",
            OverwritePrompt = true,
            InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        };

        DialogResult result;
        using (new CenterWinDialog(this))
        {
            result = SaveDlg.ShowDialog(this.Parent);
        }

        // If the file name is not an empty string open it for saving.  
        if (result == DialogResult.OK && SaveDlg.FileName != "")
        {
            using var fs = SaveDlg.OpenFile();

            switch (SaveDlg.FilterIndex)
            {
                case 1:
                    if (fs != null)
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

        return;
    }

    public void ShowHideSettings()
    {
        //splitContainer1.Panel1Collapsed = !splitContainer1.Panel1Collapsed;

        this.SuspendLayout();
        if (splitContainer1.SplitterDistance > 0)
        {
            Transitions.Transition.run(this.splitContainer1, "SplitterDistance", 0, new Transitions.TransitionType_Linear(200));
            splitContainer1.SplitterWidth = 1;
            splitContainer1.IsSplitterFixed = true;
        }
        else
        {
            Transitions.Transition.run(this.splitContainer1, "SplitterDistance", 200, new Transitions.TransitionType_Linear(200));
            splitContainer1.SplitterWidth = 4;
            splitContainer1.IsSplitterFixed = false;
        }
        this.ResumeLayout();
        return;
    }

    #endregion IChildResults interface

}