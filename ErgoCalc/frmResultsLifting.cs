using System.Text.Json;
using ErgoCalc.Models.Lifting;

namespace ErgoCalc;
public partial class FrmResultsLifting : Form, IChildResults
{
    // Variable definition
    private Job _job = new();
    private readonly System.Globalization.CultureInfo _culture = System.Globalization.CultureInfo.CurrentCulture;

    public FrmResultsLifting()
    {
        InitializeComponent();
        this.ActiveControl = this.rtbShowResult;
        this.Icon = GraphicsResources.Load<Icon>(GraphicsResources.AppLogo);
    }

    public FrmResultsLifting(object? data = null, System.Globalization.CultureInfo? culture = null, ModelType? model = null)
        : this()
    {
        if (data is not null && data.GetType() == typeof(Job))
            _job = (Job)data;

        _culture = culture ?? System.Globalization.CultureInfo.CurrentCulture;
        Model = model;
    }

    private void FrmResultsLifting_Shown(object sender, EventArgs e)
    {
        ShowResults();
    }

    #region Private routines

    /// <summary>
    /// Computes the numerical results and shows them in the RichTextBox
    /// </summary>
    private void ShowResults(bool compute = true)
    {
        bool result = false;

        // Make computations
        if (compute)
        {
            if (_job.Model == IndexType.IndexLI)
            {
                foreach (TaskModel task in _job.Tasks)
                {
                    Lifting.ComputeLI(task.SubTasks);
                }
            }
            else if (_job.Model == IndexType.IndexCLI)
            {
                foreach (TaskModel task in _job.Tasks)
                {
                    Lifting.ComputeCLI(task);
                }
            }
            result = true;
        }

        // If computation is OK, then call the routine that shows the results
        if (result)
            UpdateOutput(_culture);
    }

    /// <summary>
    /// Serialize ModelJob structure to JSON
    /// </summary>
    /// <param name="writer">The already created writer</param>
    private void SerializeToJSON(Utf8JsonWriter writer)
    {
        JsonSerializerOptions options = new ()
        {
            NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowNamedFloatingPointLiterals
        };

        writer.WriteStartObject();
        writer.WriteString("Document type", StringResources.DocumentTypeLifting);

        writer.WriteNumber("Model", (int)_job.Model);
        writer.WriteNumber("Index", _job.Index);
        writer.WriteNumber("Number of tasks", _job.NumberTasks);
        writer.WriteNumber("Number of subtasks", _job.NumberSubTasks);

        //writer.WritePropertyName("Tasks order");
        //writer.WriteStartArray();
        //for (int j = 0; j < _job.NumberTasks; j++)
        //    writer.WriteNumberValue(_job.Order[j]);
        //writer.WriteEndArray();

        writer.WritePropertyName("Tasks order");
        JsonSerializer.Serialize(writer, _job.Order);

        writer.WritePropertyName("Tasks");
        writer.WriteStartArray();
        for (int i = 0; i < _job.NumberTasks; i++)
        {
            writer.WriteStartObject();

            writer.WriteNumber("Model", (int)_job.Tasks[i].Model);
            writer.WritePropertyName("CLI");
            JsonSerializer.Serialize(writer, _job.Tasks[i].IndexCLI, options);
            //writer.WriteNumber("CLI", _job.Tasks[i].IndexCLI);
            writer.WriteNumber("Number of sub-tasks", _job.Tasks[i].NumberSubTasks);

            //writer.WritePropertyName("Sub-tasks order");
            //writer.WriteStartArray();
            //for (int j = 0; j < _job.Tasks[i].NumberSubTasks; j++)
            //    writer.WriteNumberValue(_job.Tasks[i].OrderCLI[j]);
            //writer.WriteEndArray();

            writer.WritePropertyName("Sub-tasks order");
            JsonSerializer.Serialize(writer, _job.Tasks[i].OrderCLI);

            writer.WritePropertyName("Sub-tasks");
            writer.WriteStartArray();
            for (int j = 0; j < _job.Tasks[i].NumberSubTasks; j++)
            {
                writer.WriteStartObject();
                writer.WriteNumber("Gender", (int)_job.Tasks[i].SubTasks[j].Data.gender);
                writer.WriteNumber("Age", _job.Tasks[i].SubTasks[j].Data.age);
                writer.WriteNumber("Weight", _job.Tasks[i].SubTasks[j].Data.Weight);
                writer.WriteNumber("Horizontal distance", _job.Tasks[i].SubTasks[j].Data.h);
                writer.WriteNumber("Vertical distance", _job.Tasks[i].SubTasks[j].Data.v);
                writer.WriteNumber("Distance", _job.Tasks[i].SubTasks[j].Data.d);
                writer.WriteNumber("Asymmetry angle", _job.Tasks[i].SubTasks[j].Data.a);
                writer.WriteNumber("Frequency", _job.Tasks[i].SubTasks[j].Data.f);
                writer.WriteNumber("Frequency (a)", _job.Tasks[i].SubTasks[j].Data.fa);
                writer.WriteNumber("Frequency (b)", _job.Tasks[i].SubTasks[j].Data.fb);
                writer.WriteNumber("Subtask duration", _job.Tasks[i].SubTasks[j].Data.td);
                writer.WriteNumber("Coupling", (int)_job.Tasks[i].SubTasks[j].Data.c);
                writer.WriteBoolean("One-handed", _job.Tasks[i].SubTasks[j].Data.o);
                writer.WriteBoolean("Two person", _job.Tasks[i].SubTasks[j].Data.p);

                writer.WriteNumber("H multiplier", _job.Tasks[i].SubTasks[j].Factors.HM);
                writer.WriteNumber("V multiplier", _job.Tasks[i].SubTasks[j].Factors.VM);
                writer.WriteNumber("D multiplier", _job.Tasks[i].SubTasks[j].Factors.DM);
                writer.WriteNumber("A multiplier", _job.Tasks[i].SubTasks[j].Factors.AM);
                writer.WriteNumber("F multiplier", _job.Tasks[i].SubTasks[j].Factors.FM);
                writer.WriteNumber("Fa multiplier", _job.Tasks[i].SubTasks[j].Factors.FMa);
                writer.WriteNumber("Fb multiplier", _job.Tasks[i].SubTasks[j].Factors.FMb);
                writer.WriteNumber("C multiplier", _job.Tasks[i].SubTasks[j].Factors.CM);
                writer.WriteNumber("O multiplier", _job.Tasks[i].SubTasks[j].Factors.OM);
                writer.WriteNumber("P multiplier", _job.Tasks[i].SubTasks[j].Factors.PM);
                writer.WriteNumber("E multiplier", _job.Tasks[i].SubTasks[j].Factors.EM);

                writer.WritePropertyName("LI index");
                JsonSerializer.Serialize(writer, _job.Tasks[i].SubTasks[j].IndexLI, options);
                //writer.WriteNumber("LI index", _job.Tasks[i].SubTasks[j].IndexLI);
                writer.WritePropertyName("IF index");
                JsonSerializer.Serialize(writer, _job.Tasks[i].SubTasks[j].IndexIF, options);
                //writer.WriteNumber("IF index", _job.Tasks[i].SubTasks[j].IndexIF);
                writer.WriteNumber("Item index", _job.Tasks[i].SubTasks[j].ItemIndex);
                writer.WriteNumber("Task", _job.Tasks[i].SubTasks[j].Task);
                writer.WriteNumber("Order", _job.Tasks[i].SubTasks[j].Order);
                writer.WriteEndObject();
            }
            writer.WriteEndArray();

            writer.WriteEndObject();
        }
        writer.WriteEndArray();

        writer.WriteEndObject();
        writer.Flush();
    }

    #endregion Private routines

    #region IChild interface

    public bool[] GetToolbarEnabledState() => [true, true, true, false, true, true, true, true, true, false, false, true, true, true];

    public ToolStrip? ChildToolStrip
    {
        get => toolStripLifting;
        set => toolStripLifting = value;
    }
    public ModelType? Model { get; set; }

    public string Save(string directoryPath)
    {
        DialogResult result;
        string userPath = string.Empty;

        // Displays a SaveFileDialog so the user can save the results. More information here: https://msdn.microsoft.com/en-us/library/ms160336(v=vs.110).aspx
        SaveFileDialog SaveDlg = new()
        {
            DefaultExt = "*.txt",
            Filter = "ERGO file (*.ergo)|*.ergo|RTF file (*.rtf)|*.rtf|Text file (*.txt)|*.txt|All files (*.*)|*.*",
            FilterIndex = 1,
            FileName = _job.Model switch
            {
                IndexType.IndexLI => "LI results",
                IndexType.IndexCLI => "CLI results",
                IndexType.IndexVLI => "VLI results",
                IndexType.IndexSLI => "SLI results",
                _ => "Results",
            },
            Title = "Save lifting model results",
            OverwritePrompt = true,
            InitialDirectory = string.IsNullOrWhiteSpace(directoryPath) ? Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) : directoryPath
        };

        using (new CenterWinDialog(this.MdiParent))
        {
            result = SaveDlg.ShowDialog(this);
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

                FrmMain.SetFormTitle(this, StringResources.FormResultsLifting, SaveDlg.FileName);

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
        JsonSerializerOptions options = new()
        {
            NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowNamedFloatingPointLiterals
        };

        try
        {
            job.Model = (IndexType)root.GetProperty("Model").GetInt32();
            job.Index = root.GetProperty("Index").GetDouble();
            job.NumberTasks = root.GetProperty("Number of tasks").GetInt32();
            job.NumberSubTasks = root.GetProperty("Number of subtasks").GetInt32();

            //job.Order = new int[job.NumberTasks];
            //int i = 0;
            //foreach (JsonElement TaskOrder in root.GetProperty("Tasks order").EnumerateArray())
            //{
            //    job.Order[i] = TaskOrder.GetInt32();
            //    i++;
            //}

            int Length = root.GetProperty("Tasks order").GetArrayLength();
            job.Order = new int[Length];
            job.Order = JsonSerializer.Deserialize<int[]>(root.GetProperty("Tasks order").ToString()) ?? [];

            job.Tasks = new TaskModel[job.NumberTasks];
            int i = 0;
            JsonElement SubTasks;
            //JsonElement Order;
            foreach (JsonElement Task in root.GetProperty("Tasks").EnumerateArray())
            {
                job.Tasks[i] = new()
                {
                    Model = (IndexType)Task.GetProperty("Model").GetInt32(),
                    IndexCLI = JsonSerializer.Deserialize<double>(Task.GetProperty("CLI"), options),
                    NumberSubTasks = Task.GetProperty("Number of sub-tasks").GetInt32()
                };
                job.Tasks[i].SubTasks = new SubTask[job.Tasks[i].NumberSubTasks];
                job.Tasks[i].OrderCLI = new int[job.Tasks[i].NumberSubTasks];
                
                //Order = Task.GetProperty("Sub-tasks order");
                //for (int j = 0; j < job.Tasks[i].NumberSubTasks; j++)
                //    job.Tasks[i].OrderCLI[j] = Order[j].GetInt32();

                //Length = Task.GetProperty("Sub-tasks order").GetArrayLength();
                job.Tasks[i].OrderCLI = JsonSerializer.Deserialize<int[]>(Task.GetProperty("Sub-tasks order").ToString()) ?? [];

                SubTasks = Task.GetProperty("Sub-tasks");
                for (int j = 0; j < job.Tasks[i].NumberSubTasks; j++)
                {
                    job.Tasks[i].SubTasks[j] = new();
                    job.Tasks[i].SubTasks[j].Data.gender = (Gender)SubTasks[j].GetProperty("Gender").GetDouble();
                    job.Tasks[i].SubTasks[j].Data.age = SubTasks[j].GetProperty("Age").GetDouble();
                    job.Tasks[i].SubTasks[j].Data.Weight = SubTasks[j].GetProperty("Weight").GetDouble();
                    job.Tasks[i].SubTasks[j].Data.h = SubTasks[j].GetProperty("Horizontal distance").GetDouble();
                    job.Tasks[i].SubTasks[j].Data.v = SubTasks[j].GetProperty("Vertical distance").GetDouble();
                    job.Tasks[i].SubTasks[j].Data.d = SubTasks[j].GetProperty("Distance").GetDouble();
                    job.Tasks[i].SubTasks[j].Data.a = SubTasks[j].GetProperty("Asymmetry angle").GetDouble();
                    job.Tasks[i].SubTasks[j].Data.f = SubTasks[j].GetProperty("Frequency").GetDouble();
                    job.Tasks[i].SubTasks[j].Data.fa = SubTasks[j].GetProperty("Frequency (a)").GetDouble();
                    job.Tasks[i].SubTasks[j].Data.fb = SubTasks[j].GetProperty("Frequency (b)").GetDouble();
                    job.Tasks[i].SubTasks[j].Data.td = SubTasks[j].GetProperty("Subtask duration").GetDouble();
                    job.Tasks[i].SubTasks[j].Data.c = (Coupling)SubTasks[j].GetProperty("Coupling").GetInt32();
                    job.Tasks[i].SubTasks[j].Data.o = SubTasks[j].GetProperty("One-handed").GetBoolean();
                    job.Tasks[i].SubTasks[j].Data.o = SubTasks[j].GetProperty("Two person").GetBoolean();

                    job.Tasks[i].SubTasks[j].Factors.HM = SubTasks[j].GetProperty("H multiplier").GetDouble();
                    job.Tasks[i].SubTasks[j].Factors.VM = SubTasks[j].GetProperty("V multiplier").GetDouble();
                    job.Tasks[i].SubTasks[j].Factors.DM = SubTasks[j].GetProperty("D multiplier").GetDouble();
                    job.Tasks[i].SubTasks[j].Factors.AM = SubTasks[j].GetProperty("A multiplier").GetDouble();
                    job.Tasks[i].SubTasks[j].Factors.FM = SubTasks[j].GetProperty("F multiplier").GetDouble();
                    job.Tasks[i].SubTasks[j].Factors.FMa = SubTasks[j].GetProperty("Fa multiplier").GetDouble();
                    job.Tasks[i].SubTasks[j].Factors.FMb = SubTasks[j].GetProperty("Fb multiplier").GetDouble();
                    job.Tasks[i].SubTasks[j].Factors.CM = SubTasks[j].GetProperty("C multiplier").GetDouble();
                    job.Tasks[i].SubTasks[j].Factors.OM = SubTasks[j].GetProperty("O multiplier").GetDouble();
                    job.Tasks[i].SubTasks[j].Factors.PM = SubTasks[j].GetProperty("P multiplier").GetDouble();
                    job.Tasks[i].SubTasks[j].Factors.EM = SubTasks[j].GetProperty("E multiplier").GetDouble();

                    job.Tasks[i].SubTasks[j].IndexLI = JsonSerializer.Deserialize<double>(SubTasks[j].GetProperty("LI index"), options);
                    //job.Tasks[i].SubTasks[j].IndexLI = SubTasks[j].GetProperty("LI index").GetDouble();
                    job.Tasks[i].SubTasks[j].IndexLI = JsonSerializer.Deserialize<double>(SubTasks[j].GetProperty("IF index"), options);
                    //job.Tasks[i].SubTasks[j].IndexIF = SubTasks[j].GetProperty("IF index").GetDouble();
                    job.Tasks[i].SubTasks[j].ItemIndex = SubTasks[j].GetProperty("Item index").GetInt32();
                    job.Tasks[i].SubTasks[j].Task = SubTasks[j].GetProperty("Task").GetInt32();
                    job.Tasks[i].SubTasks[j].Order = SubTasks[j].GetProperty("Order").GetInt32();
                }

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

    public void EditData()
    {
        using FrmDataLifting frm = new (_job, _culture);

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
        // Mostrar la ventana de resultados
        FrmResultsLifting frmResults = new(_job, _culture, Model)
        {
            MdiParent = this.MdiParent
        };

        int index = this.Text.IndexOf(StringResources.FormTitleUnion) > -1 ? this.Text.IndexOf(StringResources.FormTitleUnion) + StringResources.FormTitleUnion.Length : this.Text.Length;
        FrmMain.SetFormTitle(frmResults, StringResources.FormResultsLifting, this.Text[index..]);

        frmResults.rtbShowResult.Font = this.rtbShowResult.Font;
        frmResults.rtbShowResult.ForeColor = this.rtbShowResult.ForeColor;
        frmResults.rtbShowResult.BackColor = this.rtbShowResult.BackColor;
        frmResults.rtbShowResult.ZoomFactor = this.rtbShowResult.ZoomFactor;
        frmResults.rtbShowResult.WordWrap = this.rtbShowResult.WordWrap;

        frmResults.Show();
    }

    public void UpdateOutput(System.Globalization.CultureInfo culture)
    {
        //rtbShowResult.Clear();
        rtbShowResult.Text = _job.ToString(StringResources.Lifting_ResultsHeaders, culture);
        FormatText();
    }

    public void FormatText()
    {
        // Set the control's tabs
        rtbShowResult.SelectAll();
        using var g = rtbShowResult.CreateGraphics();
        rtbShowResult.SelectionTabs = (this as IChildResults).ComputeTabs(g,
                                                                        rtbShowResult.Font,
                                                                        _job.NumberSubTasks,
                                                                        StringResources.Lifting_RowHeaders,
                                                                        StringResources.Lifting_ColumnHeaders);
        rtbShowResult.DeselectAll();

        // Formats (font, size, and style) the text
        int nStart = 0, nEnd = 0;
        while (true)
        {
            // Underline
            nStart = rtbShowResult.Find(StringResources.Lifting_Data, nStart + 1, -1, RichTextBoxFinds.MatchCase);
            if (nStart == -1) break;
            nEnd = rtbShowResult.Find(Environment.NewLine.ToCharArray(), nStart + 1);
            rtbShowResult.Select(nStart, nEnd - nStart);
            rtbShowResult.SelectionFont = new Font(rtbShowResult.SelectionFont ?? rtbShowResult.Font, FontStyle.Underline | FontStyle.Bold);

            nStart = rtbShowResult.Find(StringResources.Lifting_Multipliers, nStart + 1, -1, RichTextBoxFinds.MatchCase);
            if (nStart == -1) break;
            nEnd = rtbShowResult.Find(Environment.NewLine.ToCharArray(), nStart + 1);
            rtbShowResult.Select(nStart, nEnd - nStart);
            rtbShowResult.SelectionFont = new Font(rtbShowResult.SelectionFont ?? rtbShowResult.Font, FontStyle.Underline | FontStyle.Bold);
        }

        // Bold results
        nStart = 0;

        if (_job.Model == IndexType.IndexLI)
        {
            nStart = rtbShowResult.Find(StringResources.Lifting_LI, nStart + 1, -1, RichTextBoxFinds.MatchCase);
            if (nStart > -1)
            {//nEnd = rtbShowResult.Text.Length;
                nEnd = rtbShowResult.Find(Environment.NewLine.ToCharArray(), nStart + 1);
                rtbShowResult.Select(nStart, nEnd - nStart);
                rtbShowResult.SelectionFont = new Font(rtbShowResult.SelectionFont?.FontFamily ?? rtbShowResult.Font.FontFamily, rtbShowResult.Font.Size + 1, FontStyle.Bold);
            }
        }

        while (true)
        {
            nStart = rtbShowResult.Find(StringResources.Lifting_Index, nStart + 1, -1, RichTextBoxFinds.MatchCase);
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
    
    #endregion IChild interface
}
