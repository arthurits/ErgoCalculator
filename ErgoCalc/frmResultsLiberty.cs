using System.Text.Json;

using ErgoCalc.Models.LibertyMutual;

namespace ErgoCalc;

public partial class FrmResultsLiberty : Form, IChildResults
{
    private Job _job = new();
    private readonly System.Globalization.CultureInfo _culture = System.Globalization.CultureInfo.CurrentCulture;

    public FrmResultsLiberty()
    {
        InitializeComponent();
        InitializePlots();

        this.Icon = GraphicsResources.Load<Icon>(GraphicsResources.AppLogo);
        this.ActiveControl = this.rtbShowResult;
    }

    public FrmResultsLiberty(object? data = null, System.Globalization.CultureInfo? culture = null, ModelType? model = null)
        : this()
    {
        if (data is not null && data.GetType() == typeof(Job))
            _job = (Job)data;

        _culture = culture ?? System.Globalization.CultureInfo.CurrentCulture;
        Model = model;
    }

    private void FrmResultsLiberty_Activated(object sender, EventArgs e)
    {
        this.ActiveControl = this.rtbShowResult;
    }

    private void FrmResultsLiberty_Shown(object sender, EventArgs e)
    {
        ShowResults();
    }

    #region Private routines
    private void InitializePlots()
    {
        formsPlot1.Plot.XLabel(StringResources.LibertyMutual_PlotInitialF);
        //formsPlot1.plt.YLabel("Frequency?");
        //formsPlot1.plt.Legend(backColor: Color.Transparent, frameColor: Color.Transparent, location: legendLocation.upperRight, shadowDirection: shadowDirection.none);
        formsPlot1.Plot.Palette = ScottPlot.Palette.Nord;
        //formsPlot1.plt.TightenLayout(padding: 0);

        formsPlot2.Plot.XLabel(StringResources.LibertyMutual_PlotSustainedF);
        //formsPlot2.plt.YLabel("Frequency?");
        //formsPlot2.plt.Legend(backColor: Color.Transparent, frameColor: Color.Transparent, location: legendLocation.upperRight, shadowDirection: shadowDirection.none);
        formsPlot2.Plot.Palette = ScottPlot.Palette.Nord;
        //formsPlot2.plt.TightenLayout(padding: 0, render: true);

        formsPlot3.Plot.XLabel(StringResources.LibertyMutual_PlotWeight);
        //formsPlot3.plt.YLabel("Frequency?");
        //formsPlot3.plt.Legend(backColor: Color.Transparent, frameColor: Color.Transparent, location: legendLocation.upperRight, shadowDirection: shadowDirection.none);
        formsPlot3.Plot.Palette = ScottPlot.Palette.Nord;
        //formsPlot3.plt.TightenLayout(padding: 0);
        //formsPlot3.plt.AxisAutoY();
        //formsPlot3.plt.Axis(y1: 0, y2: 1);
    }

    /// <summary>
    /// Computes the Liberty Mutual equations and shows the results in the RichTextBox control
    /// </summary>
    /// <param name="Compute">False if the index is already computed, true otherwise</param>
    private void ShowResults(bool Compute = true)
    {
        bool result = false;

        if (Compute)
            result = LibertyMutual.LibertyMutualMMH(_job);

        // Call the routine that shows the results
        if (result == true)
            UpdateLanguage(_culture);
    }

    /// <summary>
    /// Clears all plottables
    /// </summary>
    private void ClearPlots()
    {
        formsPlot1.Plot.Clear();
        formsPlot2.Plot.Clear();
        formsPlot3.Plot.Clear();
    }

    /// <summary>
    /// Manages the drawing of the 3 plots and the corresponding legends
    /// </summary>
    private void CreatePlots()
    {
        string strLegend;
        int i = 0;

        InitializePlots();

        foreach (ModelLiberty task in _job.Tasks)
        {
            strLegend = $"{StringResources.Task} {((char)('A' + i)).ToString(_culture)}";
            switch (task.Data.Type)
            {
                case TaskType.Pulling:
                case TaskType.Pushing:
                    CreatePlot(task.Initial.MAL, task.Initial.MAL * task.Initial.CV, 1, strLegend);        // Initial force plot
                    CreatePlot(task.Sustained.MAL, task.Sustained.MAL * task.Sustained.CV, 2, strLegend);    // Sustained force plot
                    break;
                case TaskType.Carrying:
                case TaskType.Lifting:
                case TaskType.Lowering:
                    CreatePlot(task.Initial.MAL, task.Initial.MAL * task.Initial.CV, 3, strLegend);        // Weight plot
                    break;
            }
            ++i;
        }
        return;
    }

    /// <summary>
    /// Draws a gaussian distribution curve as well as the lower 90 percentile and the lower 75 percentile
    /// </summary>
    /// <param name="mean">Mean</param>
    /// <param name="std">Standard deviation</param>
    /// <param name="nPlot">Number of plot control: 1 for Initial force, 2 for sustained force, and 3 for weight</param>
    /// <param name="strLegend">Text to show in the legend</param>
    private void CreatePlot(double mean, double std, int nPlot, string strLegend)
    {
        Random rand = new(0);
        var pop = new ScottPlot.Statistics.Population(rand, pointCount: 1000, mean: mean, stdDev: std);
        double[] curveXs = ScottPlot.DataGen.Range(pop.minus3stDev, pop.plus3stDev, 0.1);
        double[] curveYs = pop.GetDistribution(curveXs, normalize: false);

        ScottPlot.FormsPlot plot = formsPlot1;
        PictureBox pctLegend = pictureBox1;
        switch (nPlot)
        {
            case 1:
                plot = formsPlot1;  // Initial force plot
                pctLegend = pictureBox1;
                break;
            case 2:
                plot = formsPlot2;  // Sustained force plot
                pctLegend = pictureBox2;
                break;
            case 3:
                plot = formsPlot3;  // Weight plot
                pctLegend = pictureBox3;
                break;
        }

        //plot.plt.Legend(backColor: Color.Transparent, frameColor: Color.Transparent, location: legendLocation.upperRight, shadowDirection: shadowDirection.none);
        plot.Plot.Palette = ScottPlot.Palette.Nord;
        plot.Plot.AddScatter(curveXs, curveYs, markerSize: 0, lineWidth: 2, label: strLegend);
        //formsPlot1.plt.PlotScatter(hist.bins, hist.countsFracCurve, markerSize: 0, lineWidth: 2, label: "Histogram");

        var limit75 = mean - std * 0.674489750196082;
        var limit90 = mean - std * 1.281551565544601;

        plot.Plot.AddVerticalLine(x: limit75, color: Color.DarkGray, width: 1.2f, style: ScottPlot.LineStyle.Solid).Label = "75%";
        plot.Plot.AddVerticalLine(x: limit90, color: Color.Gray, width: 1.2f, style: ScottPlot.LineStyle.Solid).Label = "90%";
        plot.Plot.SetAxisLimits(yMin: 0, yMax: null);
        plot.Plot.AxisAutoX();
        //plot.Plot.TightenLayout(padding: 1);
        //plot.Plot.Frameless();
        plot.Refresh();

        var legendA = plot.Plot.RenderLegend();
        var bitmapA = new Bitmap(legendA.Width + 2, legendA.Height + 2);
        using Graphics GraphicsA = Graphics.FromImage(bitmapA);
        GraphicsA.DrawRectangle(new Pen(Color.Black),
                                0,
                                0,
                                legendA.Width + 1,
                                legendA.Height + 1);
        GraphicsA.DrawImage(legendA, 1, 1);

        pctLegend.Image = bitmapA;
    }

    /// <summary>
    /// Saves and commits data into a JSON structured file
    /// </summary>
    /// <param name="writer">The file writer abstraction. It's created and disposed by the caller (the Save function)</param>
    private void SerializeToJSON(Utf8JsonWriter writer)
    {
        writer.WriteStartObject();
        writer.WriteString("Document type", StringResources.DocumentTypeLiberty);
        writer.WriteNumber("Number of tasks", _job.NumberTasks);
        
        writer.WritePropertyName("Tasks");
        writer.WriteStartArray();

        foreach (ModelLiberty task in _job.Tasks)
        {
            writer.WriteStartObject();

            writer.WritePropertyName("Data");
            writer.WriteStartObject();
            writer.WriteNumber("Type", (int)task.Data.Type);
            writer.WriteNumber("Horizontal reach (m)", task.Data.HorzReach);
            writer.WriteNumber("Vertical range middle (m)", task.Data.VertRangeM);
            writer.WriteNumber("Horizontal distance (m)", task.Data.DistHorz);
            writer.WriteNumber("Vertical distance (m)", task.Data.DistVert);
            writer.WriteNumber("Vertical height (m)", task.Data.VertHeight);
            writer.WriteNumber("Frequency (actions/min)", task.Data.Freq);
            writer.WriteNumber("Gender", (int)task.Data.Gender);
            writer.WriteEndObject();
            
            writer.WritePropertyName("Scale factors initial");
            writer.WriteStartObject();
            writer.WriteNumber("Reference load (RL)", task.Initial.RL);
            writer.WriteNumber("Horizontal reach factor (H)", task.Initial.H);
            writer.WriteNumber("Vertical range middle factor (VRM)", task.Initial.VRM);
            writer.WriteNumber("Horizontal travel distance factor (DH)", task.Initial.DH);
            writer.WriteNumber("Vertical travel distance factor (DV)", task.Initial.DV);
            writer.WriteNumber("Vertical height factor (V)", task.Initial.V);
            writer.WriteNumber("Frequency factor (F)", task.Initial.F);
            writer.WriteNumber("Coefficient of variation (CV)", task.Initial.CV);
            writer.WriteNumber("MAL — Maximum acceptable load — Mean (kg or kg-f)", task.Initial.MAL);
            writer.WriteNumber("MAL — Maximum acceptable load — 75% (kg or kg-f)", task.Initial.MAL75);
            writer.WriteNumber("MAL — Maximum acceptable load — 90% (kg or kg-f)", task.Initial.MAL90);
            writer.WriteEndObject();

            writer.WritePropertyName("Scale factors sustained");
            writer.WriteStartObject();
            writer.WriteNumber("Reference load (RL)", task.Sustained.RL);
            writer.WriteNumber("Horizontal reach factor (H)", task.Sustained.H);
            writer.WriteNumber("Vertical range middle factor (VRM)", task.Sustained.VRM);
            writer.WriteNumber("Horizontal travel distance factor (DH)", task.Sustained.DH);
            writer.WriteNumber("Vertical travel distance factor (DV)", task.Sustained.DV);
            writer.WriteNumber("Vertical height factor (V)", task.Sustained.V);
            writer.WriteNumber("Frequency factor (F)", task.Sustained.F);
            writer.WriteNumber("Coefficient of variation (CV)", task.Sustained.CV);
            writer.WriteNumber("MAL — Maximum acceptable load — Mean (kg or kg-f)", task.Sustained.MAL);
            writer.WriteNumber("MAL — Maximum acceptable load — 75% (kg or kg-f)", task.Sustained.MAL75);
            writer.WriteNumber("MAL — Maximum acceptable load — 90% (kg or kg-f)", task.Sustained.MAL90);
            writer.WriteEndObject();

            //writer.WritePropertyName("Results");
            //writer.WriteStartObject();
            //writer.WriteNumber("Coefficient of variation initial", data.results.IniCoeffV);
            //writer.WriteNumber("Coefficient of variation sustained", data.results.SusCoeffV);
            //writer.WriteNumber("Initial force (kg)", data.results.IniForce);
            //writer.WriteNumber("Sustained force (kg)", data.results.SusForce);
            //writer.WriteNumber("Weight (kg)", data.results.Weight);
            //writer.WriteEndObject();

            writer.WriteEndObject();
        }

        writer.WriteEndArray();
        writer.WriteEndObject();

        writer.Flush();
    }

    #endregion Private routines

    #region IChildResults inferface
    public ModelType? Model { get ; set ; }

    public ToolStrip? ChildToolStrip { get => null; set { } }

    public bool[] GetToolbarEnabledState() => [true, true, true, false, true, true, true, true, true, false, false, true, true, true];

    public void Save(string path)
    {
        // Displays a SaveFileDialog so the user can save the Image  
        SaveFileDialog SaveDlg = new SaveFileDialog
        {
            DefaultExt = "*.csv",
            Filter = "ERGO file (*.ergo)|*.ergo|RTF file (*.rtf)|*.rtf|Text file (*.txt)|*.txt|All files (*.*)|*.*",
            FilterIndex = 1,
            Title = "Save LM-MMH data",
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

            FrmMain.SetFormTitle(this, StringResources.FormResultsLiberty, SaveDlg.FileName);

            using (new CenterWinDialog(this.MdiParent))
            {
                MessageBox.Show(this, "The file was successfully saved", "File saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }

    public bool OpenFile(JsonDocument document)
    {
        bool result = true;
        Job job = new();
        JsonElement root = document.RootElement;
        job.NumberTasks = root.GetProperty("Number of tasks").GetInt32();
        job.Tasks = new ModelLiberty[job.NumberTasks];

        int i = 0;
        try
        {
            foreach (JsonElement task in root.GetProperty("Tasks").EnumerateArray())
            {
                job.Tasks[i] = new();

                // https://stackoverflow.com/questions/29482/how-can-i-cast-int-to-enum
                // safe, as it explicitly specifies supported values
                // unsafe: Enum.IsDefined(typeof(MyEnum), value)
                var value = task.GetProperty("Data").GetProperty("Type").GetInt32();
                if ((new[] { TaskType.Carrying, TaskType.Lifting, TaskType.Lowering, TaskType.Pulling, TaskType.Pushing }).Contains((TaskType)value))
                    job.Tasks[i].Data.Type = (TaskType)value;
                else
                    job.Tasks[i].Data.Type = TaskType.Carrying;
                
                job.Tasks[i].Data.HorzReach = task.GetProperty("Data").GetProperty("Horizontal reach (m)").GetDouble();
                job.Tasks[i].Data.VertRangeM = task.GetProperty("Data").GetProperty("Vertical range middle (m)").GetDouble();
                job.Tasks[i].Data.DistHorz = task.GetProperty("Data").GetProperty("Horizontal distance (m)").GetDouble();
                job.Tasks[i].Data.DistVert = task.GetProperty("Data").GetProperty("Vertical distance (m)").GetDouble();
                job.Tasks[i].Data.VertHeight = task.GetProperty("Data").GetProperty("Vertical height (m)").GetDouble();
                job.Tasks[i].Data.Freq = task.GetProperty("Data").GetProperty("Frequency (actions/min)").GetDouble();
                
                value = task.GetProperty("Data").GetProperty("Gender").GetInt32();
                if ((new[] { Gender.Male, Gender.Female }).Contains((Gender)value))
                    job.Tasks[i].Data.Gender = (Gender)value;
                else
                    job.Tasks[i].Data.Gender = Gender.Male;

                job.Tasks[i].Initial.RL = task.GetProperty("Scale factors initial").GetProperty("Reference load (RL)").GetDouble();
                job.Tasks[i].Initial.H = task.GetProperty("Scale factors initial").GetProperty("Horizontal reach factor (H)").GetDouble();
                job.Tasks[i].Initial.VRM = task.GetProperty("Scale factors initial").GetProperty("Vertical range middle factor (VRM)").GetDouble();
                job.Tasks[i].Initial.DH = task.GetProperty("Scale factors initial").GetProperty("Horizontal travel distance factor (DH)").GetDouble();
                job.Tasks[i].Initial.DV = task.GetProperty("Scale factors initial").GetProperty("Vertical travel distance factor (DV)").GetDouble();
                job.Tasks[i].Initial.V = task.GetProperty("Scale factors initial").GetProperty("Vertical height factor (V)").GetDouble();
                job.Tasks[i].Initial.F = task.GetProperty("Scale factors initial").GetProperty("Frequency factor (F)").GetDouble();
                job.Tasks[i].Initial.CV = task.GetProperty("Scale factors initial").GetProperty("Coefficient of variation (CV)").GetDouble();
                job.Tasks[i].Initial.MAL = task.GetProperty("Scale factors initial").GetProperty("MAL — Maximum acceptable load — Mean (kg or kg-f)").GetDouble();
                job.Tasks[i].Initial.MAL75 = task.GetProperty("Scale factors initial").GetProperty("MAL — Maximum acceptable load — 75% (kg or kg-f)").GetDouble();
                job.Tasks[i].Initial.MAL90 = task.GetProperty("Scale factors initial").GetProperty("MAL — Maximum acceptable load — 90% (kg or kg-f)").GetDouble();

                job.Tasks[i].Sustained.RL = task.GetProperty("Scale factors sustained").GetProperty("Reference load (RL)").GetDouble();
                job.Tasks[i].Sustained.H = task.GetProperty("Scale factors sustained").GetProperty("Horizontal reach factor (H)").GetDouble();
                job.Tasks[i].Sustained.VRM = task.GetProperty("Scale factors sustained").GetProperty("Vertical range middle factor (VRM)").GetDouble();
                job.Tasks[i].Sustained.DH = task.GetProperty("Scale factors sustained").GetProperty("Horizontal travel distance factor (DH)").GetDouble();
                job.Tasks[i].Sustained.DV = task.GetProperty("Scale factors sustained").GetProperty("Vertical travel distance factor (DV)").GetDouble();
                job.Tasks[i].Sustained.V = task.GetProperty("Scale factors sustained").GetProperty("Vertical height factor (V)").GetDouble();
                job.Tasks[i].Sustained.F = task.GetProperty("Scale factors sustained").GetProperty("Frequency factor (F)").GetDouble();
                job.Tasks[i].Sustained.CV = task.GetProperty("Scale factors sustained").GetProperty("Coefficient of variation (CV)").GetDouble();
                job.Tasks[i].Sustained.MAL = task.GetProperty("Scale factors sustained").GetProperty("MAL — Maximum acceptable load — Mean (kg or kg-f)").GetDouble();
                job.Tasks[i].Sustained.MAL75 = task.GetProperty("Scale factors sustained").GetProperty("MAL — Maximum acceptable load — 75% (kg or kg-f)").GetDouble();
                job.Tasks[i].Sustained.MAL90 = task.GetProperty("Scale factors sustained").GetProperty("MAL — Maximum acceptable load — 90% (kg or kg-f)").GetDouble();

                //data.results.IniCoeffV = curve.GetProperty("Results").GetProperty("Coefficient of variation initial").GetDouble();
                //data.results.SusCoeffV = curve.GetProperty("Results").GetProperty("Coefficient of variation sustained").GetDouble();
                //data.results.IniForce = curve.GetProperty("Results").GetProperty("Initial force (kg)").GetDouble();
                //data.results.SusForce = curve.GetProperty("Results").GetProperty("Sustained force (kg)").GetDouble();
                //data.results.Weight = curve.GetProperty("Results").GetProperty("Weight (kg)").GetDouble();

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

    public void UpdateLanguage(System.Globalization.CultureInfo culture)
    {
        rtbShowResult.Text = _job.ToString(StringResources.LibertyMutual_ResultsHeaders, culture);
        FormatText();
        ClearPlots();
        CreatePlots();
    }

    public void FormatText()
    {
        // Set the control's tabs
        rtbShowResult.SelectAll();
        using var g = rtbShowResult.CreateGraphics();
        rtbShowResult.SelectionTabs = (this as IChildResults).ComputeTabs(g,
                                                                        rtbShowResult.Font,
                                                                        _job.NumberTasks,
                                                                        StringResources.LibertyMutual_RowHeaders,
                                                                        StringResources.LibertyMutual_ColumnHeaders);
        rtbShowResult.DeselectAll();

        // Formats (font, size, and style) the text
        int nStart = 0, nEnd = 0;

        while (true)
        {
            // Underline
            nStart = rtbShowResult.Find(StringResources.LibertyMutual_Data, nStart + 1, -1, RichTextBoxFinds.MatchCase);
            if (nStart == -1) break;
            nEnd = rtbShowResult.Find(Environment.NewLine.ToCharArray(), nStart + 1);
            rtbShowResult.Select(nStart, nEnd - nStart);
            rtbShowResult.SelectionFont = new Font(rtbShowResult.SelectionFont ?? rtbShowResult.Font, FontStyle.Underline | FontStyle.Bold);

            // Underline
            nStart = rtbShowResult.Find(StringResources.LibertyMutual_Multipliers, nStart + 1, -1, RichTextBoxFinds.MatchCase);
            if (nStart == -1) break;
            nEnd = rtbShowResult.Find(Environment.NewLine.ToCharArray(), nStart + 1);
            rtbShowResult.Select(nStart, nEnd - nStart);
            rtbShowResult.SelectionFont = new Font(rtbShowResult.SelectionFont ?? rtbShowResult.Font, FontStyle.Underline | FontStyle.Bold);
        }

        // Bold results
        nStart = 0;
        while (true)
        {
            nStart = rtbShowResult.Find(StringResources.LibertyMutual_MAL, nStart + 1, -1, RichTextBoxFinds.MatchCase);
            if (nStart == -1) break;
            //nEnd = rtbShowResult.Text.Length;
            nEnd = rtbShowResult.Find(Environment.NewLine.ToCharArray(), nStart + 1);
            rtbShowResult.Select(nStart, nEnd - nStart);
            rtbShowResult.SelectionFont = new Font(rtbShowResult.SelectionFont?.FontFamily ?? rtbShowResult.Font.FontFamily, rtbShowResult.Font.Size, FontStyle.Underline | FontStyle.Bold);
        }

        // Subindex in kgf
        nStart = 0;
        while (true)
        {
            nStart = rtbShowResult.Find("(kgf)", nStart + 1, -1, RichTextBoxFinds.MatchCase);
            if (nStart == -1) break;
            rtbShowResult.Select(nStart + 3, 1);
            rtbShowResult.SelectionCharOffset = -(int)(rtbShowResult.Font.Size / 2);
            rtbShowResult.SelectionFont = new Font(rtbShowResult.SelectionFont?.FontFamily ?? rtbShowResult.Font.FontFamily, (float)(rtbShowResult.Font.Size * 0.8));
        }

        // Set the cursor at the beginning of the text
        rtbShowResult.SelectionStart = 0;
        rtbShowResult.SelectionLength = 0;
    }

    public void EditData()
    {
        using FrmDataLiberty frm = new(_job);

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
        // Show results window
        FrmResultsLiberty frmResults = new(_job, _culture, Model)
        {
            MdiParent = this.MdiParent,
        };
        
        int index = this.Text.IndexOf(StringResources.FormTitleUnion) > -1 ? this.Text.IndexOf(StringResources.FormTitleUnion) + StringResources.FormTitleUnion.Length : this.Text.Length;
        FrmMain.SetFormTitle(frmResults, StringResources.FormResultsLiberty, this.Text[index..]);

        frmResults.rtbShowResult.Font = this.rtbShowResult.Font;
        frmResults.rtbShowResult.ForeColor= this.rtbShowResult.ForeColor;
        frmResults.rtbShowResult.BackColor = this.rtbShowResult.BackColor;
        frmResults.rtbShowResult.ZoomFactor = this.rtbShowResult.ZoomFactor;
        frmResults.rtbShowResult.WordWrap = this.rtbShowResult.WordWrap;

        frmResults.Show();
    }
    
    #endregion IChildResults inferface

}
