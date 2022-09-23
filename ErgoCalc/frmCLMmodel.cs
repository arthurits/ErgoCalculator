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
        // Variable definition
        Boolean error = false;

        ComprehensiveLifting.CalculateLSI(_job.Tasks);
        if (error == false) ShowResults();
    }

    private void rtbShowResult_DoubleClick(object sender, EventArgs e)
    {
        frmCLMmodel_Shown(null, EventArgs.Empty);
    }

    private void frmCLMmodel_Resize(object sender, EventArgs e)
    {
        // Resize the control to fit the form's client area
        //rtbShowResult.Size = this.ClientSize;
    }

    #region Private routines

    /// <summary>
    /// Shows the numerical results in the RTF control
    /// </summary>
    /// <param name="sData">Data and results array</param>
    private void ShowResults()
    {
        Int32 i, length = _job.Tasks.Length;
        String[] strLineD = new String[13];
        String[] strLineR = new String[14];
        String[] gender = new String[] { "Male", "Female" };
        String[] coupling = new String[] { "Good", "Poor", "No hndl" };
        String[] strTasks = new String[] { "A", "B", "C", "D", "E" };

        for (i = 0; i < length; i++)
        {
            strLineD[0] += "\t\t" + "Task " + strTasks[i];
            strLineD[1] += "\t\t" + _job.Tasks[i].Data.Gender;
            strLineD[2] += "\t\t" + _job.Tasks[i].Data.Weight.ToString();
            strLineD[3] += "\t\t" + _job.Tasks[i].Data.h.ToString();
            strLineD[4] += "\t\t" + _job.Tasks[i].Data.v.ToString();
            strLineD[5] += "\t\t" + _job.Tasks[i].Data.d.ToString();
            strLineD[6] += "\t\t" + _job.Tasks[i].Data.f.ToString();
            strLineD[7] += "\t\t" + _job.Tasks[i].Data.td.ToString();
            strLineD[8] += "\t\t" + _job.Tasks[i].Data.t.ToString();
            strLineD[9] += "\t\t" + _job.Tasks[i].Data.c;
            strLineD[10] += "\t\t" + _job.Tasks[i].Data.hs.ToString();
            strLineD[11] += "\t\t" + _job.Tasks[i].Data.ag.ToString();
            strLineD[12] += "\t\t" + _job.Tasks[i].Data.bw.ToString();

            strLineR[0] += "\t\t" + "Task " + strTasks[i];
            strLineR[1] += "\t\t" + _job.Tasks[i].Factors.fH.ToString("0.####");
            strLineR[2] += "\t\t" + _job.Tasks[i].Factors.fV.ToString("0.####");
            strLineR[3] += "\t\t" + _job.Tasks[i].Factors.fD.ToString("0.####");
            strLineR[4] += "\t\t" + _job.Tasks[i].Factors.fF.ToString("0.####");
            strLineR[5] += "\t\t" + _job.Tasks[i].Factors.fTD.ToString("0.####");
            strLineR[6] += "\t\t" + _job.Tasks[i].Factors.fT.ToString("0.####");
            strLineR[7] += "\t\t" + _job.Tasks[i].Factors.fC.ToString("0.####");
            strLineR[8] += "\t\t" + _job.Tasks[i].Factors.fHS.ToString("0.####");
            strLineR[9] += "\t\t" + _job.Tasks[i].Factors.fAG.ToString("0.####");
            strLineR[10] += "\t\t" + _job.Tasks[i].Factors.fBW.ToString("0.####");
            strLineR[11] += "\t\t" + (_job.Tasks[i].Data.Weight / (
                                                        _job.Tasks[i].Factors.fH *
                                                        _job.Tasks[i].Factors.fV *
                                                        _job.Tasks[i].Factors.fD *
                                                        _job.Tasks[i].Factors.fF *
                                                        _job.Tasks[i].Factors.fTD *
                                                        _job.Tasks[i].Factors.fT *
                                                        _job.Tasks[i].Factors.fC *
                                                        _job.Tasks[i].Factors.fHS *
                                                        _job.Tasks[i].Factors.fAG *
                                                        _job.Tasks[i].Factors.fBW)).ToString("0.####");
            strLineR[12] += "\t\t" + (100 - 10 * _job.Tasks[i].IndexLSI).ToString("0.####");
            strLineR[13] += "\t" + _job.Tasks[i].IndexLSI.ToString("0.####");

        }
        rtbShowResult.Text = "The LSI index from the following data:";
        rtbShowResult.AppendText("\n\n");

        // Print data
        rtbShowResult.SelectionFont = new Font(rtbShowResult.Font.Name, rtbShowResult.Font.Size, FontStyle.Underline | FontStyle.Bold);
        rtbShowResult.AppendText("Description\t\t" + strLineD[0] + "\n");
        rtbShowResult.SelectionFont = new Font(rtbShowResult.Font.Name, rtbShowResult.Font.Size, FontStyle.Regular);
        rtbShowResult.AppendText("Gender:\t\t\t" + strLineD[1] + "\n");
        rtbShowResult.AppendText("Weight lifted (kg):\t\t" + strLineD[2] + "\n");
        rtbShowResult.AppendText("Horizontal distance (cm):\t" + strLineD[3] + "\n");
        rtbShowResult.AppendText("Vertical distance (cm):\t" + strLineD[4] + "\n");
        rtbShowResult.AppendText("Vertical travel distance (cm):" + strLineD[5] + "\n");
        rtbShowResult.AppendText("Lifting frequency (times/min):" + strLineD[6] + "\n");
        rtbShowResult.AppendText("Task duration (hours):\t" + strLineD[7] + "\n");
        rtbShowResult.AppendText("Twisting angle (º):\t" + strLineD[8] + "\n");
        rtbShowResult.AppendText("Coupling:\t\t" + strLineD[9] + "\n");
        rtbShowResult.AppendText("WBGT temperature (ºC):\t" + strLineD[10] + "\n");
        rtbShowResult.AppendText("Age (years):\t\t" + strLineD[11] + "\n");
        rtbShowResult.AppendText("Body weight (kg):\t\t" + strLineD[12] + "\n\n");

        // Print results
        rtbShowResult.SelectionFont = new Font(rtbShowResult.Font.Name, rtbShowResult.Font.Size, FontStyle.Underline | FontStyle.Bold);
        rtbShowResult.AppendText("Description\t\t" + strLineR[0] + "\n");
        rtbShowResult.SelectionFont = new Font(rtbShowResult.Font.Name, rtbShowResult.Font.Size, FontStyle.Regular);
        rtbShowResult.AppendText("Horizontal multiplier:\t" + strLineR[1] + "\n");
        rtbShowResult.AppendText("Vertical multiplier:\t" + strLineR[2] + "\n");
        rtbShowResult.AppendText("Distance multiplier:\t" + strLineR[3] + "\n");
        rtbShowResult.AppendText("Frequency multiplier:\t" + strLineR[4] + "\n");
        rtbShowResult.AppendText("Task duration multiplier:\t" + strLineR[5] + "\n");
        rtbShowResult.AppendText("Twisting multiplier:\t" + strLineR[6] + "\n");
        rtbShowResult.AppendText("Coupling multiplier:\t" + strLineR[7] + "\n");
        rtbShowResult.AppendText("WBGT multiplier:\t\t" + strLineR[8] + "\n");
        rtbShowResult.AppendText("Age multiplier:\t\t" + strLineR[9] + "\n");
        rtbShowResult.AppendText("Body weight multiplier:\t" + strLineR[10] + "\n\n");
        rtbShowResult.AppendText("Base weight:\t\t" + strLineR[11] + "\n");
        rtbShowResult.AppendText("Population percentage:\t" + strLineR[12] + "\n");
        rtbShowResult.SelectionFont = new Font(rtbShowResult.Font.Name, rtbShowResult.Font.Size + 2, FontStyle.Bold);
        rtbShowResult.AppendText("The LSI index is:\t\t" + strLineR[13] + "\n");
        rtbShowResult.SelectionFont = new Font(rtbShowResult.Font.Name, rtbShowResult.Font.Size - 1, FontStyle.Regular);
    }

    #endregion Private routines

    #region IChildResults
    public void Save(string path)
    {
        throw new NotImplementedException();
    }

    public bool OpenFile(JsonDocument document)
    {
        bool result = true;
        MessageBox.Show("Json Open not yet implemented");
        return result;
    }

    public void EditData()
    {
        throw new NotImplementedException();
    }

    public void Duplicate()
    {
        throw new NotImplementedException();
    }

    public bool[] GetToolbarEnabledState()
    {
        return new bool[] { true, true, false, false, true, true, false, false, true, false, false, true, true, true };
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
        //rtbShowResult.Select(rtbShowResult.GetFirstCharIndexFromLine(9), line.Length);
        rtbShowResult.SelectionFont = new Font(rtbShowResult.SelectionFont, FontStyle.Underline | FontStyle.Bold);

        // Bold results
        line = rtbShowResult.Lines[30];
        rtbShowResult.Select(rtbShowResult.Find("The", rtbShowResult.SelectionStart + 1, RichTextBoxFinds.MatchCase), line.Length);
        //rtbShowResult.Select(rtbShowResult.GetFirstCharIndexFromLine(16), line.Length);
        rtbShowResult.SelectionFont = new Font(rtbShowResult.SelectionFont.FontFamily, rtbShowResult.Font.Size + 1, FontStyle.Bold);

        // Set the cursor at the beginning of the text
        rtbShowResult.SelectionStart = 0;
        rtbShowResult.SelectionLength = 0;
    }
    #endregion IChildResults

    private void frmCLMmodel_Load(object sender, EventArgs e)
    {
        //Win32.Win32API.AnimateWindow(this.Handle, 100, Win32.Win32API.AnimateWindowFlags.AW_BLEND | Win32.Win32API.AnimateWindowFlags.AW_CENTER);
    }

    private void frmCLMmodel_FormClosing(object sender, FormClosingEventArgs e)
    {
        //Win32.Win32API.AnimateWindow(this.Handle, 100, Win32.Win32API.AnimateWindowFlags.AW_HIDE | Win32.Win32API.AnimateWindowFlags.AW_BLEND);
    }

}
