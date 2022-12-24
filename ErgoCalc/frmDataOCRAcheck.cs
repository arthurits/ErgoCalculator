using System;
using System.Globalization;
using System.Reflection;
using System.Windows.Forms;

namespace ErgoCalc;

public partial class FrmDataOCRAcheck : Form, IChildData
{
    private readonly CultureInfo _culture = CultureInfo.CurrentCulture;
    private double _job;
    object IChildData.GetData => _job;

    public FrmDataOCRAcheck()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Overloaded constructor
    /// </summary>
    /// <param name="job"><see cref="Job"/> object containing data to be shown in the form</param>
    /// <param name="culture">Culture information to be used when showing the form's UI texts</param>
    //public FrmDataOCRAcheck(Job? job = null, CultureInfo? culture = null)
    //    : this() // Call the base constructor
    //{
    //    // Update the UI language first
    //    _culture = culture ?? CultureInfo.CurrentCulture;
    //    UpdateUI_Language(_culture);

    //    // Then show the data
    //    if (job is not null)
    //    {
    //        _job = job;
    //        DataToGrid();
    //    }
    //}

    private void btnOK_Click(object sender, EventArgs e)
    {
        // Save the job definition
    }
    private void radioButton_CheckedChanged(object sender, EventArgs e)
    {
    }

    public void LoadExample()
    {

    }

    /// <summary>
    /// Update the form's interface language
    /// </summary>
    /// <param name="culture">Culture used to display the UI</param>
    private void UpdateUI_Language(System.Globalization.CultureInfo culture)
    {
        StringResources.Culture = culture;

        this.btnAccept.Text = StringResources.BtnAccept;
        this.btnCancel.Text = StringResources.BtnCancel;

        this.lblTasks.Text = StringResources.NumberOfTasks;

        if (this.tabDummy.TabPages.Count > 0)
            this.tabDummy.TabPages[0].Text = StringResources.Task;

        // Relocate controls
        RelocateControls();
    }

    /// <summary>
    /// Relocate controls to compensate for the culture text length in labels
    /// </summary>
    private void RelocateControls()
    {
        this.updTasks.Left = this.lblTasks.Left + this.lblTasks.Width + 5;
    }
}
