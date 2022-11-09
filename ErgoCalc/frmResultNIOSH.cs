using System;
using System.Drawing;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;

using ErgoCalc.Models.Lifting;

namespace ErgoCalc;
public partial class FrmResultNIOSH : Form, IChildResults
{
    // Variable definition
    private Job _job;

    public FrmResultNIOSH()
    {
        // VS designer initialization
        InitializeComponent();

        // ToolStrip
        var path = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
        if (File.Exists(path + @"\images\save.ico")) this.toolStripNIOSH_Save.Image = new Icon(path + @"\images\save.ico", 48, 48).ToBitmap();
        if (File.Exists(path + @"\images\settings.ico")) this.toolStripNIOSH_Settings.Image = new Icon(path + @"\images\settings.ico", 48, 48).ToBitmap();
        //this.toolStripNIOSH_Settings.CheckOnClick = true;

        propertyGrid1.SelectedObject = new ResultsOptions(this.rtbShowResult);
        splitContainer1.Panel1Collapsed = false;
        splitContainer1.SplitterDistance = 0;
        splitContainer1.SplitterWidth = 1;
        splitContainer1.IsSplitterFixed = true;
    }

    public FrmResultNIOSH(object data)
        : this()
    {
        if (data.GetType() == typeof(Job))
            _job = (Job)data;
    }

    private void frmResultNIOSHModel_Shown(object sender, EventArgs e)
    {
        ShowResults();
    }

    private void rtbShowResult_DoubleClick(object sender, EventArgs e)
    {
        frmResultNIOSHModel_Shown(null, EventArgs.Empty);
    }

    #region Private routines

    /// <summary>
    /// Computes the numerical results and shows them in the RichTextBox
    /// </summary>
    private void ShowResults()
    {
        // Make computations
        if (_job.Model == IndexType.IndexLI)
        {
            foreach (Task task in _job.Tasks)
            {
                NIOSHLifting.ComputeLI(task.SubTasks);
            }
        }
        else if (_job.Model == IndexType.IndexCLI)
        {
            foreach (Task task in _job.Tasks)
            {
                NIOSHLifting.ComputeCLI(task);
            }
        }

        // Show results
        rtbShowResult.Text = _job.ToString();
        FormatText();

    }

    /// <summary>
    /// Serialize ModelJob structure to JSON
    /// </summary>
    /// <param name="writer">The already created writer</param>
    private void SerializeToJSON(Utf8JsonWriter writer)
    {
        writer.WriteStartObject();
        writer.WriteString("Document type", "NIOSH lifting equation");

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
            writer.WriteNumber("CLI", _job.Tasks[i].IndexCLI);
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
                writer.WriteNumber("Load constant", _job.Tasks[i].SubTasks[j].Data.LC);
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

                writer.WriteNumber("H multiplier", _job.Tasks[i].SubTasks[j].Factors.HM);
                writer.WriteNumber("V multiplier", _job.Tasks[i].SubTasks[j].Factors.VM);
                writer.WriteNumber("D multiplier", _job.Tasks[i].SubTasks[j].Factors.DM);
                writer.WriteNumber("A multiplier", _job.Tasks[i].SubTasks[j].Factors.AM);
                writer.WriteNumber("F multiplier", _job.Tasks[i].SubTasks[j].Factors.FM);
                writer.WriteNumber("Fa multiplier", _job.Tasks[i].SubTasks[j].Factors.FMa);
                writer.WriteNumber("Fb multiplier", _job.Tasks[i].SubTasks[j].Factors.FMb);
                writer.WriteNumber("C multiplier", _job.Tasks[i].SubTasks[j].Factors.CM);

                writer.WriteNumber("LI index", _job.Tasks[i].SubTasks[j].IndexLI);
                writer.WriteNumber("IF index", _job.Tasks[i].SubTasks[j].IndexIF);
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

    public void Save(string path)
    {
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
            Title = "Save NIOSH model results",
            OverwritePrompt = true,
            InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        };

        DialogResult result;
        using (new CenterWinDialog(this.MdiParent))
        {
            result = SaveDlg.ShowDialog(this);
        }

        // If the file name is not an empty string open it for saving.  
        if (result == DialogResult.OK && SaveDlg.FileName != "")
        {
            System.IO.FileStream fs;

            // Saves the text via a FileStream created by the OpenFile method.  
            if ((fs = (System.IO.FileStream)SaveDlg.OpenFile()) != null)
            {
                // Saves the text in the appropriate TextFormat based upon the File type selected in the dialog box.  
                // NOTE that the FilterIndex property is one-based. 
                switch (SaveDlg.FilterIndex)
                {
                    case 1:
                        using (var writer = new Utf8JsonWriter(fs, options: new JsonWriterOptions { Indented = true }))
                            SerializeToJSON(writer);
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

                fs.Close();

                using (new CenterWinDialog(this.MdiParent))
                {
                    MessageBox.Show(this, "The file was successfully saved", "File saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        return;
    }

    public bool OpenFile(JsonDocument document)
    {
        bool result = true;
        Job job = new();
        JsonElement root = document.RootElement;

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
            job.Order = JsonSerializer.Deserialize<int[]>(root.GetProperty("Tasks order").ToString());

            job.Tasks = new Task[job.NumberTasks];
            int i = 0;
            JsonElement SubTasks;
            //JsonElement Order;
            foreach (JsonElement Task in root.GetProperty("Tasks").EnumerateArray())
            {
                job.Tasks[i] = new();
                job.Tasks[i].Model = (IndexType)Task.GetProperty("Model").GetInt32();
                job.Tasks[i].IndexCLI = Task.GetProperty("CLI").GetDouble();
                job.Tasks[i].NumberSubTasks = Task.GetProperty("Number of sub-tasks").GetInt32();
                job.Tasks[i].SubTasks = new SubTask[job.Tasks[i].NumberSubTasks];
                job.Tasks[i].OrderCLI = new int[job.Tasks[i].NumberSubTasks];
                
                //Order = Task.GetProperty("Sub-tasks order");
                //for (int j = 0; j < job.Tasks[i].NumberSubTasks; j++)
                //    job.Tasks[i].OrderCLI[j] = Order[j].GetInt32();

                //Length = Task.GetProperty("Sub-tasks order").GetArrayLength();
                job.Tasks[i].OrderCLI = JsonSerializer.Deserialize<int[]>(Task.GetProperty("Sub-tasks order").ToString());

                SubTasks = Task.GetProperty("Sub-tasks");
                for (int j = 0; j < job.Tasks[i].NumberSubTasks; j++)
                {
                    job.Tasks[i].SubTasks[j] = new();
                    job.Tasks[i].SubTasks[j].Data.LC = SubTasks[j].GetProperty("Load constant").GetDouble();
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
                    
                    job.Tasks[i].SubTasks[j].Factors.HM = SubTasks[j].GetProperty("H multiplier").GetDouble();
                    job.Tasks[i].SubTasks[j].Factors.VM = SubTasks[j].GetProperty("V multiplier").GetDouble();
                    job.Tasks[i].SubTasks[j].Factors.DM = SubTasks[j].GetProperty("D multiplier").GetDouble();
                    job.Tasks[i].SubTasks[j].Factors.AM = SubTasks[j].GetProperty("A multiplier").GetDouble();
                    job.Tasks[i].SubTasks[j].Factors.FM = SubTasks[j].GetProperty("F multiplier").GetDouble();
                    job.Tasks[i].SubTasks[j].Factors.FMa = SubTasks[j].GetProperty("Fa multiplier").GetDouble();
                    job.Tasks[i].SubTasks[j].Factors.FMb = SubTasks[j].GetProperty("Fb multiplier").GetDouble();
                    job.Tasks[i].SubTasks[j].Factors.CM = SubTasks[j].GetProperty("C multiplier").GetDouble();

                    job.Tasks[i].SubTasks[j].IndexLI = SubTasks[j].GetProperty("LI index").GetDouble();
                    job.Tasks[i].SubTasks[j].IndexIF = SubTasks[j].GetProperty("IF index").GetDouble();
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
        using var frm = new FrmDataNIOSH(_job);

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
        string _strPath = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);

        // Mostrar la ventana de resultados
        FrmResultNIOSH frmResults = new FrmResultNIOSH(_job)
        {
            MdiParent = this.MdiParent
        };
        if (File.Exists(_strPath + @"\images\logo.ico")) frmResults.Icon = new Icon(_strPath + @"\images\logo.ico");
        frmResults.Show();
    }

    public bool[] GetToolbarEnabledState()
    {
        bool[] toolbar = new bool[] { true, true, true, false, true, true, true, true, true, false, false, true, true, true };
        return toolbar;
    }

    public ToolStrip ChildToolStrip
    {
        get => toolStripNIOSH;
        set => toolStripNIOSH = value;
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
    }

    public bool PanelCollapsed()
    {
        return this.splitContainer1.SplitterDistance == 0 ? true: false;
    }

    public void FormatText()
    {
        int nStart = 0, nEnd = 0;

        while (true)
        {
            // Underline
            nStart = rtbShowResult.Find("Initial data", nStart + 1, -1, RichTextBoxFinds.MatchCase);
            if (nStart == -1) break;
            nEnd = rtbShowResult.Find(Environment.NewLine.ToCharArray(), nStart + 1);
            rtbShowResult.Select(nStart, nEnd - nStart);
            rtbShowResult.SelectionFont = new Font(rtbShowResult.SelectionFont, FontStyle.Underline | FontStyle.Bold);

            nStart = rtbShowResult.Find("Multipliers", nStart + 1, -1, RichTextBoxFinds.MatchCase);
            if (nStart == -1) break;
            nEnd = rtbShowResult.Find(Environment.NewLine.ToCharArray(), nStart + 1);
            rtbShowResult.Select(nStart, nEnd - nStart);
            rtbShowResult.SelectionFont = new Font(rtbShowResult.SelectionFont, FontStyle.Underline | FontStyle.Bold);
        }

        // Bold results
        nStart = 0;

        nStart = rtbShowResult.Find("Lifting index:", nStart + 1, -1, RichTextBoxFinds.MatchCase);
        if (nStart > -1)
        {//nEnd = rtbShowResult.Text.Length;
            nEnd = rtbShowResult.Find(Environment.NewLine.ToCharArray(), nStart + 1);
            rtbShowResult.Select(nStart, nEnd - nStart);
            rtbShowResult.SelectionFont = new Font(rtbShowResult.SelectionFont.FontFamily, rtbShowResult.Font.Size + 1, FontStyle.Bold);
        }

        while (true)
        {
            nStart = rtbShowResult.Find("The NIOSH lifting index is:", nStart + 1, -1, RichTextBoxFinds.MatchCase);
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

    #endregion IChild interface

}
