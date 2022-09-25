using System;
using System.Drawing;
using System.Text.Json;
using System.Windows.Forms;

using ErgoCalc.Models.CLM;

namespace ErgoCalc;

public partial class frmCLMmodel : Form, IChildResults
{
    // Variable definition
    private Job _job;
    private ResultsOptions _options;

    public frmCLMmodel()
    {
        InitializeComponent();

        // Initialize private variable
        _options = new ResultsOptions(rtbShowResult);

        propertyGrid1.SelectedObject = _options;
        splitContainer1.Panel1Collapsed = true;
        //rtbShowResult.Size = this.ClientSize;
    }

    public frmCLMmodel(Job job)
        : this()
    {
        _job = job;
    }

    public frmCLMmodel(object data)
        : this()
    {
        if (data.GetType() == typeof(Job)) _job = (Job)data;
    }

    private void frmCLMmodel_Shown(object sender, EventArgs e)
    {
        ShowResults();
    }

    /// <summary>
    /// Computes the LSI index and shows the results in the RichTextBox control
    /// </summary>
    /// <param name="Compute">False if the index is already computed, true otherwise</param>
    private void ShowResults(bool Compute = true)
    {
        // Variable definition
        Boolean error = false;

        if (Compute) ComprehensiveLifting.CalculateLSI(_job.Tasks);
        if (error == false)
        {
            rtbShowResult.Text = _job.ToString();
            FormatText();
        }
    }

    /// <summary>
    /// Serializes the Job data into a json file
    /// </summary>
    /// <param name="writer"></param>
    private void SerializeToJSON(Utf8JsonWriter writer)
    {
        writer.WriteStartObject();
        writer.WriteString("Document type", "Comprehensive lifting model");
        writer.WriteNumber("Number of tasks", _job.NumberTasks);

        writer.WritePropertyName("Tasks");
        writer.WriteStartArray();

        foreach (Task task in _job.Tasks)
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
    public void Save(string path)
    {
        // Displays a SaveFileDialog so the user can save the Image  
        SaveFileDialog SaveDlg = new SaveFileDialog
        {
            DefaultExt = "*.csv",
            Filter = "ERGO file (*.ergo)|*.ergo|RTF file (*.rtf)|*.rtf|Text file (*.txt)|*.txt|All files (*.*)|*.*",
            FilterIndex = 1,
            Title = "Save CLM data",
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
        job.Tasks = new Task[job.NumberTasks];

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
        frmDataCLMmodel frmData = new frmDataCLMmodel(_job);

        if (frmData.ShowDialog(this) == DialogResult.OK)
        {
            // Mostrar la ventana de resultados
            _job = (Job)frmData.GetData;
            this.rtbShowResult.Clear();
            ShowResults();
        }
        // Cerrar el formulario de entrada de datos
        frmData.Dispose();
    }

    public void Duplicate()
    {
        //string _strPath = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);

        // Mostrar la ventana de resultados
        frmCLMmodel frmResults = new frmCLMmodel(_job) { MdiParent = this.MdiParent };
        //if (File.Exists(_strPath + @"\images\logo.ico")) frmResults.Icon = new Icon(_strPath + @"\images\logo.ico");
        frmResults.Show();
    }

    public bool[] GetToolbarEnabledState()
    {
        return new bool[] { true, true, true, false, true, true, true, true, true, false, false, true, true, true };
    }

    public ToolStrip ChildToolStrip
    {
        get => null;
        set { }
    }

    public void ShowHideSettings()
    {
        splitContainer1.Panel1Collapsed = !splitContainer1.Panel1Collapsed;
        return;
    }

    public bool PanelCollapsed()
    {
        return this.splitContainer1.Panel1Collapsed;
    }

    public void FormatText()
    {
        // Underline
        string line = rtbShowResult.Lines[2];
        rtbShowResult.Select(rtbShowResult.Find("Description", 0, RichTextBoxFinds.MatchCase), line.Length);
        //rtbShowResult.Select(rtbShowResult.GetFirstCharIndexFromLine(2), line.Length);
        rtbShowResult.SelectionFont = new Font(rtbShowResult.SelectionFont, FontStyle.Underline | FontStyle.Bold);

        line = rtbShowResult.Lines[16];
        rtbShowResult.Select(rtbShowResult.Find("Description", rtbShowResult.SelectionStart + 1, RichTextBoxFinds.MatchCase), line.Length);
        //rtbShowResult.Select(rtbShowResult.GetFirstCharIndexFromLine(16), line.Length);
        rtbShowResult.SelectionFont = new Font(rtbShowResult.SelectionFont, FontStyle.Underline | FontStyle.Bold);

        // Bold results
        line = rtbShowResult.Lines[31];
        rtbShowResult.Select(rtbShowResult.Find("The LSI", rtbShowResult.SelectionStart + 1, RichTextBoxFinds.MatchCase), line.Length);
        //rtbShowResult.Select(rtbShowResult.GetFirstCharIndexFromLine(31), line.Length);
        rtbShowResult.SelectionFont = new Font(rtbShowResult.SelectionFont.FontFamily, rtbShowResult.Font.Size, FontStyle.Bold);

        // Set the cursor at the beginning of the text
        rtbShowResult.SelectionStart = 0;
        rtbShowResult.SelectionLength = 0;
    }
    #endregion IChildResults

}