using ErgoCalc.controls;
using ErgoCalc.Models.OCRA;
using System.Globalization;

namespace ErgoCalc;

public partial class FrmDataOCRAcheck : Form, IChildData
{
    private readonly CultureInfo _culture = CultureInfo.CurrentCulture;
    private Job _job;
    private TaskType _index = TaskType.SingleTask;

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
    public FrmDataOCRAcheck(Job? job = null, CultureInfo? culture = null)
        : this() // Call the base constructor
    {
        // Update the UI language first
        _culture = culture ?? CultureInfo.CurrentCulture;
        UpdateUI_Language(_culture);

        // Then show the data
        if (job is not null)
        {
            _job = job;
            DataToGrid();
        }
    }

    private void btnOK_Click(object sender, EventArgs e)
    {
        // Save the job definition
    }
    private void radioButton_CheckedChanged(object sender, EventArgs e)
    {
    }

    private void Subtasks_ValueChanged(object sender, EventArgs e)
    {
        int col = Convert.ToInt32(updSubtasks.Value);
        (this as IChildData).UpdateGridColumns(gridVariables, col);

        // Modify the chkComposite state
        groupIndex.Enabled = col > 0;
        if (col > 1)
        {
            radCOSI.Enabled = true;
            radCUSI.Enabled = true;
        }
        else if (col == 1)
        {
            radRSI.Checked = true;
            radCOSI.Enabled = false;
            radCUSI.Enabled = false;
        }

        // Set the maximum tasks
        if (col > 1) this.updTasks.Maximum = col - 1;

        // Set tabpages text
        //SetTabPagesText();
    }

    #region Private routines

    /// <summary>
    /// Adds a column to the DataGrid View and formates it
    /// </summary>
    /// <param name="col">Column number (zero based)</param>
    public void AddColumn(int col)
    {
        // By default, the DataGrid always contains a single column
        //if (col == 0) return;
        // Check if the column already exists
        if (gridVariables.Columns.Contains($"Column {(col).ToString()}")) return;

        // Create the new column
        string strName = $"{(_index == TaskType.SingleTask ? StringResources.Task : StringResources.Subtask)} ";
        (this as IChildData).AddColumnBasic(gridVariables, col, strName, 85);

        // Add the row headers after the first column is created
        if (col == 0)
            AddRows();

        FormatRows(col);

        return;
    }

    /// <summary>
    /// Adds a column to the DataGrid View and formates it
    /// </summary>
    private void AddColumn()
    {
        (this as IChildData).AddColumn(gridVariables.Columns.Count);
    }

    /// <summary>
    /// Adds the headercell values for each row
    /// </summary>
    private void AddRows()
    {
        (this as IChildData).AddGridRowHeaders(this.gridVariables, StringResources.OCRA_DataInputHeaders);
    }

    /// <summary>
    /// Format the header row with custom cells
    /// </summary>
    /// <param name="column">Column to be formatted</param>
    private void FormatRows(int column = 0)
    {
        List<string[]> list = new();

        list.Add(new[] { "Nothing0", "Weak1", "Light2", "Moderate3", "Strong4", "StrongHeavy5", "VeryStrong6", "VeryStrong7", "VeryStrong8", "ExtremelyStrong9", "ExtremelyStrongMaximal10" });
        list.Add(new[] { "1/3", "50%", "> 50%", "Almost all the time" });
        list.Add(new[] { "2 sec each 10 min", "1%", "5%", "> 10%" });
        list.Add(new[] { "2 sec each 10 min", "1%", "5%", "> 10%" });

        gridVariables.Rows[0].Cells[column] = new DataGridViewChildCell(list);
    }

    public void LoadExample()
    {

    }

    /// <summary>
    /// Shows the data into the grid control
    /// </summary>
    private void DataToGrid()
    {
        
    }

    #endregion

    /// <summary>
    /// Update the form's interface language
    /// </summary>
    /// <param name="culture">Culture used to display the UI</param>
    private void UpdateUI_Language(System.Globalization.CultureInfo culture)
    {
        StringResources.Culture = culture;

        this.Text = StringResources.FormDataOCRAchecklist;
        this.btnAccept.Text = StringResources.BtnAccept;
        this.btnCancel.Text = StringResources.BtnCancel;

        this.lblSubtasks.Text = StringResources.NumberOfTasks;

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
        this.updSubtasks.Left = this.lblTasks.Left + this.lblTasks.Width + 5;
    }

    
}
