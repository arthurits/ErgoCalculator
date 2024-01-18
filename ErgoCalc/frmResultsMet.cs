using System;
using System.Text.Json;
using System.Windows.Forms;

using ErgoCalc.Models.MetabolicRate;

namespace ErgoCalc;

public partial class FrmResultsMet : Form, IChildResults
{
    // Variable definition
    private Job _job;
    private System.Globalization.CultureInfo _culture = System.Globalization.CultureInfo.CurrentCulture;

    public ToolStrip ChildToolStrip { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    // Default constructor
    public FrmResultsMet()
    {
        // VS initializer
        InitializeComponent();
    }

    public FrmResultsMet(object? data = null, System.Globalization.CultureInfo? culture = null)
        : this() // Call the base constructor
    {
        if (data is not null && data.GetType() == typeof(Job))
            _job = (Job)data;

        _culture = culture ?? System.Globalization.CultureInfo.CurrentCulture;
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

        // Show results in GUI
        if (error == false)
            UpdateLanguage(_culture);
    }

    public void Save(string path)
    {
        
    }

    public bool OpenFile(JsonDocument document)
    {
        return false;
    }

    public bool[] GetToolbarEnabledState() => [true, true, false, false, true, true, false, false, true, false, false, true, true, true];


    public void UpdateLanguage(System.Globalization.CultureInfo culture)
    {
        rtbShowResult.Text = _job.ToString();
        FormatText();
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
            // Show results window
            _job = (Job)frmData.GetData;
            this.rtbShowResult.Clear();
            ShowResults();
        }
        // Cerrar el formulario de entrada de datos
        frmData.Dispose();
    }

    public void Duplicate()
    {
        // Show results window
        FrmResultsMet frmResults = new FrmResultsMet(_job, _culture)
        {
            MdiParent = this.MdiParent
        };

        int index = this.Text.IndexOf(StringResources.FormTitleUnion) > -1 ? this.Text.IndexOf(StringResources.FormTitleUnion) + StringResources.FormTitleUnion.Length : this.Text.Length;
        FrmMain.SetFormTitle(frmResults, StringResources.FormResultsMetabolic, this.Text[index..]);

        frmResults.rtbShowResult.Font = this.rtbShowResult.Font;
        frmResults.rtbShowResult.ForeColor = this.rtbShowResult.ForeColor;
        frmResults.rtbShowResult.BackColor = this.rtbShowResult.BackColor;
        frmResults.rtbShowResult.ZoomFactor = this.rtbShowResult.ZoomFactor;
        frmResults.rtbShowResult.WordWrap = this.rtbShowResult.WordWrap;

        frmResults.Show();
    }
    #endregion


}
