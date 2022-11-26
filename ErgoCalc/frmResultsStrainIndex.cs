using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

using System.Text.Json;

using ErgoCalc.Models.StrainIndex;

namespace ErgoCalc;

public partial class FrmResultsStrainIndex : Form, IChildResults
{
    // Variable definition
    private Job _job = new();

    public FrmResultsStrainIndex()
    {
        InitializeComponent();

        this.Icon = GraphicsResources.Load<Icon>(GraphicsResources.AppLogo);
    }

    public FrmResultsStrainIndex(object? data, bool wordWrap, int backColor, float zoomFactor)
        : this()
    {
        if(data?.GetType() == typeof(Job)) _job = (Job)data;
        rtbShowResult.WordWrap = wordWrap;
        rtbShowResult.BackColor = Color.FromArgb(backColor);
        rtbShowResult.ZoomFactor = zoomFactor / 100;
    }

    private void frmResultsStrainIndex_Shown(object sender, EventArgs e)
    {
        ShowResults();
    }


    #region Private routines
    /// <summary>
    /// Computes the numerical results and shows them in the RichTextBox
    /// </summary>
    private void ShowResults()
    {
        //splitContainer1.Panel1Collapsed = ((ToolStrip)((frmMain)MdiParent).Controls["toolStripMain"]).Items["toolStripMain_Settings"].Enabled == true ? false : true;
        // Variable definition
        Boolean error = false;

        // Make computations
        if (_job.Model == IndexType.RSI)
        {
            foreach (TaskModel task in _job.Tasks)
                StrainIndex.IndexRSI(task.SubTasks);
        }
        else if (_job.Model == IndexType.COSI)
        {
            foreach (TaskModel task in _job.Tasks)
                StrainIndex.IndexCOSI(task);
        }
        else if (_job.Model == IndexType.CUSI)
        {
            StrainIndex.IndexCUSI(_job);
        }

        // Call the routine that shows the results
        if (error == false)
        {
            rtbShowResult.Text = _job.ToString();
            CreatePlots();
            FormatText();
        }
    }

    /// <summary>
    /// Creates the plots and adds them to the end of the RichTextControl
    /// </summary>
    /// <param name="sData">Data and results array</param>
    private void CreatePlots()
    {
        int nOrder;
        double nCOSI = 0.0;
        double[] dataX;
        double[] dataRSI;
        double[] dataCOSI;
        string[] labels;
        string strTaskText;
        string strPlotTitle;

        try
        {
            var orgdata = Clipboard.GetDataObject();

            for (int j = 0; j < _job.NumberTasks; j++)
            {
                dataX = new double[_job.Tasks[j].NumberSubTasks];
                dataRSI = new double[_job.Tasks[j].NumberSubTasks];
                dataCOSI = new double[_job.Tasks[j].NumberSubTasks];
                labels = new string[_job.Tasks[j].NumberSubTasks];

                if (_job.Model == IndexType.RSI)
                {
                    strTaskText = "Tasks";
                    strPlotTitle = "RSI results";
                }
                else
                {
                    strTaskText = "SubTasks";
                    strPlotTitle = string.Concat("RSI & COSI results for Task ", ((char)('A' + j)).ToString());
                }

                for (int i = 0; i < _job.Tasks[j].NumberSubTasks; i++)
                {

                    nOrder = _job.Tasks[j].Order[i];

                    dataX[i] = i + 1;
                    dataRSI[i] = _job.Tasks[j].SubTasks[nOrder].IndexRSI;
                    if (i == 0)
                        nCOSI = _job.Tasks[j].SubTasks[nOrder].IndexRSI;
                    else
                        nCOSI += _job.Tasks[j].SubTasks[nOrder].IndexRSI * (_job.Tasks[j].SubTasks[nOrder].Factors.EMa - _job.Tasks[j].SubTasks[nOrder].Factors.EMb) / _job.Tasks[j].SubTasks[nOrder].Factors.EM;
                    dataCOSI[i] = nCOSI;
                    //labels[i] = string.Concat(strTaskText, ((char)('A' + _job.JobTasks[j].SubTasks[nOrder].ItemIndex)).ToString());
                    labels[i] = ((char)('A' + _job.Tasks[j].SubTasks[nOrder].ItemIndex)).ToString();
                }

                var plot = new ScottPlot.Plot(600, 450);

                plot.XAxis2.Label(strPlotTitle, size: 22);
                plot.YAxis.Label("Index value", size: 22);
                plot.XAxis.Label(strTaskText, size: 22);

                plot.AddBar(dataRSI, dataX).Label = "RSI";
                if (_job.Tasks[j].IndexCOSI != -1)
                    plot.AddScatter(dataX, dataCOSI, label: "COSI", markerSize: 10, markerShape: ScottPlot.MarkerShape.filledCircle, lineWidth: 5);
                plot.AxisAuto();
                plot.XTicks(dataX, labels);
                plot.XAxis.Grid(false);
                plot.Grid(lineStyle: ScottPlot.LineStyle.Dot);

                //plot.CoordinateFromPixel();

                using (var plotImage = plot.Render())
                {
                    Clipboard.SetImage(plotImage);
                }
                plot.Clear();
                rtbShowResult.AppendText(Environment.NewLine);
                var read = rtbShowResult.ReadOnly;
                rtbShowResult.ReadOnly = false;
                rtbShowResult.SelectionStart = rtbShowResult.Text.Length;
                rtbShowResult.Paste();
                Clipboard.SetDataObject(orgdata);
                rtbShowResult.ReadOnly = read;
                rtbShowResult.SelectionStart = 0;
                string code = rtbShowResult.Rtf;
            }

            if (_job.Model == IndexType.CUSI)
            {
                dataX = new double[_job.NumberTasks];
                dataRSI = new double[_job.NumberTasks];
                dataCOSI = new double[_job.NumberTasks];
                labels = new string[_job.NumberTasks];
                strPlotTitle = string.Empty;

                for (int i = 0; i < _job.NumberTasks; i++)
                {
                    nOrder = _job.Order[i];
                    dataX[i] = i + 1;
                    dataRSI[i] = _job.Tasks[nOrder].IndexCOSI;
                    if (i == 0)
                        nCOSI = _job.Tasks[nOrder].IndexCOSI;
                    else
                        nCOSI += _job.Tasks[nOrder].IndexCOSI * (_job.Tasks[nOrder].HMa - _job.Tasks[nOrder].HMb) / _job.Tasks[nOrder].HM;
                    dataCOSI[i] = nCOSI;
                    //labels[i] = string.Concat("Task ", ((char)('A' + nOrder)).ToString());
                    labels[i] = ((char)('A' + nOrder)).ToString();
                    strPlotTitle += string.Concat(((char)('A' + i)).ToString(), " & ");
                }
                strPlotTitle = strPlotTitle.Remove(strPlotTitle.Length - 3, 3);

                var plot = new ScottPlot.Plot(600, 450);
                //plot.Title(string.Concat("CUSI results for Tasks ", strPlotTitle), fontSize: 22);
                plot.XAxis2.Label(string.Concat("CUSI results for Tasks ", strPlotTitle), size: 22);
                plot.YAxis.Label("Index value", size: 22);
                plot.XAxis.Label("Tasks", size: 22); ;

                plot.AddBar(dataRSI, dataX).Label = "COSI";
                plot.AddScatter(dataX, dataCOSI, label: "CUSI", markerSize: 10, markerShape: ScottPlot.MarkerShape.filledCircle, lineWidth: 5);
                plot.AxisAuto();
                plot.XTicks(dataX, labels);
                plot.XAxis.TickLabelStyle(fontSize: 18);
                //plot.Ticks(fontSize: 18);
                plot.XAxis.Grid(false);
                plot.Grid(lineStyle: ScottPlot.LineStyle.Dot);

                using (var plotImage = plot.Render())
                {
                    Clipboard.SetImage(plotImage);
                }
                plot.Clear();
                rtbShowResult.AppendText(Environment.NewLine);
                var read = rtbShowResult.ReadOnly;
                rtbShowResult.ReadOnly = false;
                rtbShowResult.SelectionStart = rtbShowResult.Text.Length;
                rtbShowResult.Paste();
                Clipboard.SetDataObject(orgdata);
                rtbShowResult.ReadOnly = read;
                rtbShowResult.SelectionStart = 0;

            }
        }
        catch (System.Runtime.InteropServices.ExternalException)
        {

        }
        catch (Exception exception) when (!(exception is System.Runtime.InteropServices.ExternalException))
        {
            MessageBox.Show("Unexpected error while plotting results", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /*
        // Resize image
        int positionW = 0;
        int positionH = 0;

        positionW = rtbShowResult.Rtf.IndexOf("picwgoal", positionW);
        //if (positionW == -1) break;
        //var algo = rtbShowResult.Rtf.Substring(positionW + 8, 4);
        rtbShowResult.Rtf = rtbShowResult.Rtf.Replace(rtbShowResult.Rtf.Substring(positionW + 8, 4), @"9000");

        positionH = rtbShowResult.Rtf.IndexOf("pichgoal", positionH);
        //if (positionH == -1) break;
        rtbShowResult.Rtf = rtbShowResult.Rtf.Replace(rtbShowResult.Rtf.Substring(positionH + 8, 4), @"6750");
        */

        // https://stackoverflow.com/questions/542850/how-can-i-insert-an-image-into-a-richtextbox
    }

    /// <summary>
    /// Serialize ModelJob structure to JSON
    /// </summary>
    /// <param name="writer">The already created writer</param>
    private void SerializeToJSON(Utf8JsonWriter writer)
    {
        // https://devblogs.microsoft.com/dotnet/try-the-new-system-text-json-apis/
        // https://docs.microsoft.com/es-es/dotnet/standard/serialization/system-text-json-how-to?pivots=dotnet-5-0
        // https://docs.microsoft.com/es-es/dotnet/standard/serialization/write-custom-serializer-deserializer
        // https://docs.microsoft.com/es-es/dotnet/api/system.text.json.jsonelement?view=net-5.0
        // https://stackoverflow.com/questions/37665240/howto-serialize-a-nested-collection-using-jsonwriter-in-c-sharp
        // https://gist.github.com/richlander/530947180fb95b4df3f1123170ba8701
        //var writerOptions = new JsonWriterOptions
        //{
        //    Indented = true
        //};
        //using FileStream createStream = File.Create(strFileName);
        //using var writer = new Utf8JsonWriter(createStream, options: writerOptions);
        writer.WriteStartObject();
        writer.WriteString("Document type", "Strain index");
        writer.WriteNumber("Index type", (int)_job.Model);
        writer.WriteNumber("CUSI index", _job.IndexCUSI);
        writer.WriteNumber("Number of tasks", _job.NumberTasks);
        writer.WriteNumber("Number of subtasks", _job.NumberSubTasks);

        //writer.WriteStartArray("Tasks order");
        //for (int i = 0; i < _job.NumberTasks; i++)
        //    writer.WriteNumberValue(_job.Order[i]);
        //writer.WriteEndArray();

        writer.WritePropertyName("Tasks order");
        JsonSerializer.Serialize(writer, _job.Order);

        writer.WritePropertyName("Tasks");
        writer.WriteStartArray();
        for (int i = 0; i < _job.NumberTasks; i++)
        {
            writer.WriteStartObject();

            writer.WritePropertyName("Sub-tasks");
            writer.WriteStartArray();
            for (int j = 0; j < _job.Tasks[i].NumberSubTasks; j++)
            {
                writer.WriteStartObject();
                writer.WriteNumber("Intensity", _job.Tasks[i].SubTasks[j].Data.i);
                writer.WriteNumber("Efforts", _job.Tasks[i].SubTasks[j].Data.e);
                writer.WriteNumber("EffortsA", _job.Tasks[i].SubTasks[j].Data.ea);
                writer.WriteNumber("EffortsB", _job.Tasks[i].SubTasks[j].Data.eb);
                writer.WriteNumber("Duration", _job.Tasks[i].SubTasks[j].Data.d);
                writer.WriteNumber("Posture", _job.Tasks[i].SubTasks[j].Data.p);
                writer.WriteNumber("Hours", _job.Tasks[i].SubTasks[j].Data.h);

                writer.WriteNumber("I multiplier", _job.Tasks[i].SubTasks[j].Factors.IM);
                writer.WriteNumber("E multiplier", _job.Tasks[i].SubTasks[j].Factors.EM);
                writer.WriteNumber("Ea multiplier", _job.Tasks[i].SubTasks[j].Factors.EMa);
                writer.WriteNumber("Eb multiplier", _job.Tasks[i].SubTasks[j].Factors.EMb);
                writer.WriteNumber("D multiplier", _job.Tasks[i].SubTasks[j].Factors.DM);
                writer.WriteNumber("P multiplier", _job.Tasks[i].SubTasks[j].Factors.PM);
                writer.WriteNumber("H multiplier", _job.Tasks[i].SubTasks[j].Factors.HM);

                writer.WriteNumber("RSI index", _job.Tasks[i].SubTasks[j].IndexRSI);
                writer.WriteNumber("Item index", _job.Tasks[i].SubTasks[j].ItemIndex);
                writer.WriteEndObject();
            }
            writer.WriteEndArray();

            //writer.WritePropertyName("Sub-tasks order");
            //writer.WriteStartArray();
            //for (int j = 0; j < _job.JobTasks[i].NumberSubTasks; j++)
            //    writer.WriteNumberValue(_job.JobTasks[i].Order[j]);
            //writer.WriteEndArray();

            writer.WritePropertyName("Sub-tasks order");
            JsonSerializer.Serialize(writer, _job.Tasks[i].Order);

            writer.WriteNumber("h factor", _job.Tasks[i].h);
            writer.WriteNumber("ha factor", _job.Tasks[i].ha);
            writer.WriteNumber("hb factor", _job.Tasks[i].hb);
            writer.WriteNumber("H multiplier", _job.Tasks[i].HM);
            writer.WriteNumber("Ha multiplier", _job.Tasks[i].HMa);
            writer.WriteNumber("Hb multiplier", _job.Tasks[i].HMb);
            writer.WriteNumber("COSI index", _job.Tasks[i].IndexCOSI);
            writer.WriteNumber("Number of sub-tasks", _job.Tasks[i].NumberSubTasks);

            writer.WriteEndObject();
        }
        writer.WriteEndArray();

        //object val = Convert.ChangeType(_job.model, _job.model.GetTypeCode());

        writer.WriteEndObject();
        writer.Flush();
    }

    #endregion Private routines

    #region IChildResults
    
    public bool OpenFile(JsonDocument document)
    {
        bool result = true;
        Job job = new();
        JsonElement root = document.RootElement;

        try
        {
            job.Model = (IndexType)root.GetProperty("Index type").GetInt32();
            job.IndexCUSI = root.GetProperty("CUSI index").GetDouble();
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

            job.Tasks = new TaskModel[job.NumberTasks];
            int i = 0;
            JsonElement SubTasks;
            //JsonElement Order;
            foreach (JsonElement Task in root.GetProperty("Tasks").EnumerateArray())
            {
                job.Tasks[i] = new();
                job.Tasks[i].NumberSubTasks = Task.GetProperty("Number of sub-tasks").GetInt32();
                job.Tasks[i].SubTasks = new SubTask[job.Tasks[i].NumberSubTasks];
                job.Tasks[i].Order = new int[job.Tasks[i].NumberSubTasks];
                job.Tasks[i].Order = JsonSerializer.Deserialize<int[]>(Task.GetProperty("Sub-tasks order").ToString());

                SubTasks = Task.GetProperty("Sub-tasks");
                //Order = Task.GetProperty("Sub-tasks order");
                for (int j = 0; j < job.Tasks[i].NumberSubTasks; j++)
                {
                    job.Tasks[i].SubTasks[j] = new();
                    job.Tasks[i].SubTasks[j].Data.i = SubTasks[j].GetProperty("Intensity").GetDouble();
                    job.Tasks[i].SubTasks[j].Data.e = SubTasks[j].GetProperty("Efforts").GetDouble();
                    job.Tasks[i].SubTasks[j].Data.ea = SubTasks[j].GetProperty("EffortsA").GetDouble();
                    job.Tasks[i].SubTasks[j].Data.eb = SubTasks[j].GetProperty("EffortsB").GetDouble();
                    job.Tasks[i].SubTasks[j].Data.d = SubTasks[j].GetProperty("Duration").GetDouble();
                    job.Tasks[i].SubTasks[j].Data.p = SubTasks[j].GetProperty("Posture").GetDouble();
                    job.Tasks[i].SubTasks[j].Data.h = SubTasks[j].GetProperty("Hours").GetDouble();
                    job.Tasks[i].SubTasks[j].Factors.IM = SubTasks[j].GetProperty("I multiplier").GetDouble();
                    job.Tasks[i].SubTasks[j].Factors.EM = SubTasks[j].GetProperty("E multiplier").GetDouble();
                    job.Tasks[i].SubTasks[j].Factors.EMa = SubTasks[j].GetProperty("Ea multiplier").GetDouble();
                    job.Tasks[i].SubTasks[j].Factors.EMb = SubTasks[j].GetProperty("Eb multiplier").GetDouble();
                    job.Tasks[i].SubTasks[j].Factors.DM = SubTasks[j].GetProperty("D multiplier").GetDouble();
                    job.Tasks[i].SubTasks[j].Factors.PM = SubTasks[j].GetProperty("P multiplier").GetDouble();
                    job.Tasks[i].SubTasks[j].Factors.HM = SubTasks[j].GetProperty("H multiplier").GetDouble();
                    job.Tasks[i].SubTasks[j].IndexRSI = SubTasks[j].GetProperty("RSI index").GetDouble();
                    job.Tasks[i].SubTasks[j].ItemIndex = SubTasks[j].GetProperty("Item index").GetInt32();

                    //job.JobTasks[i].Order[j] = Order[j].GetInt32();
                }

                job.Tasks[i].IndexCOSI = Task.GetProperty("COSI index").GetDouble();
                job.Tasks[i].h = Task.GetProperty("h factor").GetDouble();
                job.Tasks[i].ha = Task.GetProperty("ha factor").GetDouble();
                job.Tasks[i].hb = Task.GetProperty("hb factor").GetDouble();
                job.Tasks[i].HM = Task.GetProperty("H multiplier").GetDouble();
                job.Tasks[i].HMa = Task.GetProperty("Ha multiplier").GetDouble();
                job.Tasks[i].HMb = Task.GetProperty("Hb multiplier").GetDouble();

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
    
    public void Save(string path)
    {
        // Displays a SaveFileDialog so the user can save the results. More information here: https://msdn.microsoft.com/en-us/library/ms160336(v=vs.110).aspx
        SaveFileDialog SaveDlg = new()
        {
            DefaultExt = "*.rtf",
            Filter = "ERGO file (*.ergo)|*.ergo|RTF file (*.rtf)|*.rtf|Text file (*.txt)|*.txt|All files (*.*)|*.*",
            FilterIndex = 2,
            FileName = _job.Model switch
            {
                IndexType.RSI => "RSI results",
                IndexType.COSI => "COSI results",
                IndexType.CUSI => "CUSI results",
                _ => "Results",
            },
            Title = "Save Strain Index results",
            OverwritePrompt = true,
            InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        };

        DialogResult result;
        using (new CenterWinDialog(this.MdiParent))
        {
            result = SaveDlg.ShowDialog(this);
        }

        // If the file name is not an empty string open it for saving.  
        if (result==DialogResult.OK && SaveDlg.FileName != "")
        {
            using var fs = SaveDlg.OpenFile();

            // Saves the text via a FileStream created by the OpenFile method.  
            if ( fs != null)
            {
                // Saves the text in the appropriate TextFormat based upon the File type selected in the dialog box.  
                // NOTE that the FilterIndex property is one-based. 
                switch (SaveDlg.FilterIndex)
                {
                    case 1:
                        using (var writer = new Utf8JsonWriter(fs, options: new JsonWriterOptions { Indented = true }))
                        {
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

                FrmMain.SetFormTitle(this, StringResources.FormResultsStrainIndex, SaveDlg.FileName);

                using (new CenterWinDialog(this.MdiParent))
                {
                    MessageBox.Show(this, "The file was successfully saved", "File saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }     
            }
        }

        return;
    }

    public void EditData()
    {
        // Llamar al formulario para introducir los datos
        FrmDataStrainIndex frmDataStrain = new FrmDataStrainIndex(_job);

        if (frmDataStrain.ShowDialog(this) == DialogResult.OK)
        {
            object data = frmDataStrain.GetData;
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
        FrmResultsStrainIndex frmResults = new FrmResultsStrainIndex(_job, rtbShowResult.WordWrap, rtbShowResult.BackColor.ToArgb(), rtbShowResult.ZoomFactor)
        {
            MdiParent = this.MdiParent
        };
        
        frmResults.Show();
    }

    public bool[] GetToolbarEnabledState()
    {
        return new bool[] { true, true, true, false, true, true, false, true, true, false, false, true, true, true };
    }

    public ToolStrip ChildToolStrip
    {
        get => null;
        set { }
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
    
    #endregion IChildResults

}
