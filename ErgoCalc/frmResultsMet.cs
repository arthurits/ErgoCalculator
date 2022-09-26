using System;
using System.Text.Json;
using System.Windows.Forms;

using ErgoCalc.Models.MetabolicRate;

namespace ErgoCalc;

public partial class FrmResultsMet : Form, IChildResults
{
    // Variable definition
    private Job _job;

    public ToolStrip ChildToolStrip { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    // Default constructor
    public FrmResultsMet()
    {
        // VS initializer
        InitializeComponent();
    }

    // Overloaded constructor
    public FrmResultsMet(Job job)
        : this() // Call the base constructor
    {
        _job = job;
    }

    public FrmResultsMet(object data)
        : this() // Call the base constructor
    {
        if (data.GetType() == typeof(Job))
            _job = (Job)data;
    }
    
    private void frmMetResult_Shown(object sender, EventArgs e)
    {
        ShowResults();
    }

    #region Private routines

    /// <summary>
    /// Computes the metabolic rate and shows the results in the RichTextBox control
    /// </summary>
    /// <param name="Compute">False if the index is already computed, true otherwise</param>
    private void ShowResults(bool Compute = true)
    {
        bool error = false;

        if (Compute) MetabolicRate.CalculateMetRate(_job);

        if (error == false)
        {
            // Escribir los resultados en la ventana
            rtbShowResult.Text = _job.ToString();
            FormatText();
        }
    }

    public void Save(string path)
    {
        
    }

    public bool OpenFile(JsonDocument document)
    {
        return false;
    }

    public bool[] GetToolbarEnabledState()
    {
        return new bool[] { true, true, false, false, true, true, false, false, true, false, false, true, true, true };
    }

    public void ShowHideSettings()
    {
        //splitContainer1.Panel1Collapsed = !splitContainer1.Panel1Collapsed;
        return;
    }

    public bool PanelCollapsed()
    {
        //return this.splitContainer1.Panel1Collapsed;
        return true;
    }

    public void FormatText()
    {
        
    }

    public void EditData()
    {
        // Llamar al formulario para introducir los datos
        FrmDataMet frmData = new FrmDataMet(_job);

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
        FrmResultsMet frmResults = new FrmResultsMet(_job) { MdiParent = this.MdiParent };
        //if (File.Exists(_strPath + @"\images\logo.ico")) frmResults.Icon = new Icon(_strPath + @"\images\logo.ico");
        frmResults.Show();
    }
    #endregion


}
