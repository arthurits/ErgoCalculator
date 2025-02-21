﻿using System.Text.Json;
using ErgoCalc.Models.CLM;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace ErgoCalc;

public partial class FrmResultsCLM : Form, IChildResults
{
    // Variable definition
    private Job _job = new();
    private readonly System.Globalization.CultureInfo _culture = System.Globalization.CultureInfo.CurrentCulture;

    public FrmResultsCLM()
    {
        InitializeComponent();
        this.ActiveControl = this.rtbShowResult;
        this.Icon = GraphicsResources.Load<Icon>(GraphicsResources.AppLogo);
    }

    public FrmResultsCLM(object? data = null, System.Globalization.CultureInfo? culture = null, ModelType? model = null)
        : this()
    {
        if (data is not null && data?.GetType() == typeof(Job))
            _job = (Job)data;

        _culture = culture ?? System.Globalization.CultureInfo.CurrentCulture;
        Model = model;
    }

    private void Form_Shown(object sender, EventArgs e)
    {
        ShowResults();
    }

    /// <summary>
    /// Computes the LSI index and shows the results in the RichTextBox control
    /// </summary>
    /// <param name="compute">False if the index is already computed, true otherwise</param>
    private void ShowResults(bool compute = true)
    {
        // Variable definition
        bool result = false;

        if (compute)
            result = ComprehensiveLifting.CalculateLSI(_job.Tasks);

        // If computation is OK, then call the routine that shows the results
        if (result)
            UpdateOutput(_culture);
    }

    /// <summary>
    /// Serializes the Job data into a json file
    /// </summary>
    /// <param name="writer"></param>
    private void SerializeToJSON(Utf8JsonWriter writer)
    {
        writer.WriteStartObject();
        writer.WriteString("Document type", StringResources.DocumentTypeCLM);
        writer.WriteNumber("Number of tasks", _job.NumberTasks);

        writer.WritePropertyName("Tasks");
        writer.WriteStartArray();

        foreach (TaskModel task in _job.Tasks)
        {
            writer.WriteStartObject();

            writer.WritePropertyName("Data");
            writer.WriteStartObject();
            writer.WriteNumber("Gender", (int)task.Data.Gender);
            writer.WriteNumber("Base weight", task.Data.Weight);
            writer.WriteNumber("Horizontal distance", task.Data.h);
            writer.WriteNumber("Vertical distance", task.Data.v);
            writer.WriteNumber("Distance travelled", task.Data.d);
            writer.WriteNumber("Frequency", task.Data.f);
            writer.WriteNumber("Twisting angle", task.Data.t);
            writer.WriteNumber("Task duration", task.Data.td);
            writer.WriteNumber("Coupling", (int)task.Data.c);
            writer.WriteNumber("Heat stress", task.Data.hs);
            writer.WriteNumber("Age", task.Data.ag);
            writer.WriteNumber("Body weight", task.Data.bw);
            writer.WriteEndObject();

            writer.WritePropertyName("Variables");
            writer.WriteStartObject();
            writer.WriteNumber("H multiplier", task.Factors.fH);
            writer.WriteNumber("V multiplier", task.Factors.fV);
            writer.WriteNumber("D multiplier", task.Factors.fD);
            writer.WriteNumber("F multiplier", task.Factors.fF);
            writer.WriteNumber("T multiplier", task.Factors.fT);
            writer.WriteNumber("TD multiplier", task.Factors.fTD);
            writer.WriteNumber("C multiplier", task.Factors.fC);
            writer.WriteNumber("HS multiplier", task.Factors.fHS);
            writer.WriteNumber("AG multiplier", task.Factors.fAG);
            writer.WriteNumber("BW multiplier", task.Factors.fBW);
            writer.WriteEndObject();

            writer.WriteNumber("Maximum weight", task.BaseWeight);
            writer.WriteNumber("Percentage", task.Percentage);
            writer.WriteNumber("Index LSI", task.IndexLSI);

            writer.WriteEndObject();
        }

        writer.WriteEndArray();
        writer.WriteEndObject();

        writer.Flush();
    }

    #region IChildResults
    public bool[] GetToolbarEnabledState() => [true, true, true, false, true, true, true, true, true, false, false, true, true, true];

    public ToolStrip? ChildToolStrip { get => null; set { } }

    public ModelType? Model { get; set; }

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
            FileName = "CLM results",
            Title = "Save CLM results",
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
            if (fs != null)
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

                FrmMain.SetFormTitle(this, StringResources.FormResultsCLM, SaveDlg.FileName);

                using (new CenterWinDialog(this.MdiParent))
                {
                    MessageBox.Show(this, "The file was successfully saved", "File saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        return userPath;
    }

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
                job.Tasks[i].Data.Gender = (Gender)curve.GetProperty("Data").GetProperty("Gender").GetInt32();
                job.Tasks[i].Data.Weight = curve.GetProperty("Data").GetProperty("Base weight").GetDouble();
                job.Tasks[i].Data.h = curve.GetProperty("Data").GetProperty("Horizontal distance").GetDouble();
                job.Tasks[i].Data.v = curve.GetProperty("Data").GetProperty("Vertical distance").GetDouble();
                job.Tasks[i].Data.d = curve.GetProperty("Data").GetProperty("Distance travelled").GetDouble();
                job.Tasks[i].Data.f = curve.GetProperty("Data").GetProperty("Frequency").GetDouble();
                job.Tasks[i].Data.t = curve.GetProperty("Data").GetProperty("Twisting angle").GetDouble();
                job.Tasks[i].Data.td = curve.GetProperty("Data").GetProperty("Task duration").GetDouble();
                job.Tasks[i].Data.c = (Coupling)curve.GetProperty("Data").GetProperty("Coupling").GetInt32();
                job.Tasks[i].Data.hs = curve.GetProperty("Data").GetProperty("Heat stress").GetDouble();
                job.Tasks[i].Data.ag = curve.GetProperty("Data").GetProperty("Age").GetDouble();
                job.Tasks[i].Data.bw = curve.GetProperty("Data").GetProperty("Body weight").GetDouble();

                job.Tasks[i].Factors.fH = curve.GetProperty("Variables").GetProperty("H multiplier").GetDouble();
                job.Tasks[i].Factors.fV = curve.GetProperty("Variables").GetProperty("V multiplier").GetDouble();
                job.Tasks[i].Factors.fD = curve.GetProperty("Variables").GetProperty("D multiplier").GetDouble();
                job.Tasks[i].Factors.fF = curve.GetProperty("Variables").GetProperty("F multiplier").GetDouble();
                job.Tasks[i].Factors.fT = curve.GetProperty("Variables").GetProperty("T multiplier").GetDouble();
                job.Tasks[i].Factors.fTD = curve.GetProperty("Variables").GetProperty("TD multiplier").GetDouble();
                job.Tasks[i].Factors.fC = curve.GetProperty("Variables").GetProperty("C multiplier").GetDouble();
                job.Tasks[i].Factors.fHS = curve.GetProperty("Variables").GetProperty("HS multiplier").GetDouble();
                job.Tasks[i].Factors.fAG = curve.GetProperty("Variables").GetProperty("AG multiplier").GetDouble();
                job.Tasks[i].Factors.fBW = curve.GetProperty("Variables").GetProperty("BW multiplier").GetDouble();

                job.Tasks[i].BaseWeight = curve.GetProperty("Maximum weight").GetDouble();
                job.Tasks[i].Percentage = curve.GetProperty("Percentage").GetDouble();
                job.Tasks[i].IndexLSI = curve.GetProperty("Index LSI").GetDouble();

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

    public void EditData()
    {
        // Llamar al formulario para introducir los datos
        using FrmDataCLM frm = new(_job, _culture);

        if (frm.ShowDialog(this) == DialogResult.OK)
        {
            object data = frm.GetData;
            if (data.GetType() == typeof(Job))
                _job = (Job)data;
            else
                _job = new();

            ShowResults();
        }
        return;
    }

    public void Duplicate()
    {
        // Show the results window
        FrmResultsCLM frmResults = new(_job, _culture, Model)
        {
            MdiParent = this.MdiParent
        };

        int index = this.Text.IndexOf(StringResources.FormTitleUnion) > -1 ? this.Text.IndexOf(StringResources.FormTitleUnion) + StringResources.FormTitleUnion.Length : this.Text.Length;
        FrmMain.SetFormTitle(frmResults, StringResources.FormResultsCLM, this.Text[index..]);

        frmResults.rtbShowResult.Font = this.rtbShowResult.Font;
        frmResults.rtbShowResult.ForeColor = this.rtbShowResult.ForeColor;
        frmResults.rtbShowResult.BackColor = this.rtbShowResult.BackColor;
        frmResults.rtbShowResult.ZoomFactor = this.rtbShowResult.ZoomFactor;
        frmResults.rtbShowResult.WordWrap = this.rtbShowResult.WordWrap;

        frmResults.Show();
    }

    public void UpdateOutput(System.Globalization.CultureInfo culture)
    {
        rtbShowResult.Text = _job.ToString(StringResources.CLM_ResultsHeaders, _culture);
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
                                                                        StringResources.CLM_RowHeaders,
                                                                        StringResources.CLM_ColumnHeaders);
        rtbShowResult.DeselectAll();

        // Formats (font, size, and style) the text
        int nStart = 0, nEnd = 0;
        while (true)
        {
            // Underline
            nStart = rtbShowResult.Find(StringResources.CLM_Data, nStart + 1, -1, RichTextBoxFinds.MatchCase);
            if (nStart == -1) break;
            nEnd = rtbShowResult.Find(Environment.NewLine.ToCharArray(), nStart + 1);
            rtbShowResult.Select(nStart, nEnd - nStart);
            rtbShowResult.SelectionFont = new Font(rtbShowResult.SelectionFont ?? rtbShowResult.Font, FontStyle.Underline | FontStyle.Bold);

            nStart = rtbShowResult.Find(StringResources.CLM_Multipliers, nStart + 1, -1, RichTextBoxFinds.MatchCase);
            if (nStart == -1) break;
            nEnd = rtbShowResult.Find(Environment.NewLine.ToCharArray(), nStart + 1);
            rtbShowResult.Select(nStart, nEnd - nStart);
            rtbShowResult.SelectionFont = new Font(rtbShowResult.SelectionFont ?? rtbShowResult.Font, FontStyle.Underline | FontStyle.Bold);
        }

        // Bold results
        nStart = 0;
        while (true)
        {
            nStart = rtbShowResult.Find(StringResources.CLM_LSIindex, nStart + 1, -1, RichTextBoxFinds.MatchCase);
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

    #endregion IChildResults

}