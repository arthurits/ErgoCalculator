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
        if (error == false)
        {
            rtbShowResult.Text=_job.ToString();
            FormatText();
        }
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
        // Llamar al formulario para introducir los datos
        frmDataCLMmodel frmData = new frmDataCLMmodel(_job);

        if (frmData.ShowDialog(this) == DialogResult.OK)
        {
            // Mostrar la ventana de resultados
            _job = (Job)frmData.GetData;
            this.rtbShowResult.Clear();
            frmCLMmodel_Shown(null, EventArgs.Empty);
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
        return new bool[] { true, true, false, false, true, true, true, true, true, false, false, true, true, true };
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

    private void frmCLMmodel_Load(object sender, EventArgs e)
    {
        //Win32.Win32API.AnimateWindow(this.Handle, 100, Win32.Win32API.AnimateWindowFlags.AW_BLEND | Win32.Win32API.AnimateWindowFlags.AW_CENTER);
    }

    private void frmCLMmodel_FormClosing(object sender, FormClosingEventArgs e)
    {
        //Win32.Win32API.AnimateWindow(this.Handle, 100, Win32.Win32API.AnimateWindowFlags.AW_HIDE | Win32.Win32API.AnimateWindowFlags.AW_BLEND);
    }

}
