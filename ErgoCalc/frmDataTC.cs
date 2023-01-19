using System.Globalization;

using ErgoCalc.Models.ThermalComfort;

namespace ErgoCalc;

public partial class FrmDataTC : Form, IChildData
{
    private readonly CultureInfo _culture = CultureInfo.CurrentCulture;
    private Job _job;
    private readonly string strGridHeader = "Case ";
    public object GetData => _job;

    public FrmDataTC()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Overloaded constructor
    /// </summary>
    /// <param name="job"><see cref="Job"/> object containing data to be shown in the form</param>
    /// <param name="culture">Culture information to be used when showing the form's UI texts</param>
    public FrmDataTC(Job? job = null, CultureInfo? culture = null)
        : this()
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

    #region Form events
    private void Accept_Click(object sender, EventArgs e)
    {
        // The form does not return unless all fields are validated. This avoids closing the dialog
        this.DialogResult = DialogResult.None;

        _job = new();
        
        // Save the values entered
        _job.Tasks = new TaskModel[Convert.ToInt32(updTasks.Value)];
        for (int i = 0; i < _job.Tasks.Length; i++)
        {
            _job.Tasks[i] = new TaskModel();
            _job.Tasks[i].Data.TempAir = Convert.ToDouble(gridVariables[i, 0].Value);
            _job.Tasks[i].Data.TempRad = Convert.ToDouble(gridVariables[i, 1].Value);
            _job.Tasks[i].Data.Velocity = Convert.ToDouble(gridVariables[i, 2].Value);
            _job.Tasks[i].Data.RelHumidity = Convert.ToDouble(gridVariables[i, 3].Value);
            _job.Tasks[i].Data.Clothing = Convert.ToDouble(gridVariables[i, 4].Value);
            _job.Tasks[i].Data.MetRate = Convert.ToDouble(gridVariables[i, 5].Value);
            _job.Tasks[i].Data.ExternalWork = Convert.ToDouble(gridVariables[i, 6].Value);
        }

        // Return OK thus closing the dialog
        this.DialogResult = DialogResult.OK;
    }

    private void Example_Click(object sender, EventArgs e)
    {
        // Loads some data example into the grid
        DataExample();
        DataToGrid();
    }

    private void Tasks_ValueChanged(object sender, EventArgs e)
    {
        (this as IChildData).UpdateGridColumns(gridVariables, Convert.ToInt32(updTasks.Value));
    }
    #endregion Form events

    #region Private routines
    /// <summary>
    /// Adds a column to the DataGrid View and formates it
    /// </summary>
    /// <param name="col">Column number (zero based)</param>
    void IChildData.AddColumn(Int32 col)
    {
        // By default, the DataGrid always contains a single column
        //if (col == 0) return;
        // Check if the column already exists
        if (gridVariables.Columns.Contains("Column" + (col).ToString())) return;

        // Create the new column
        (this as IChildData).AddColumnBasic(gridVariables, col, strGridHeader, 70);

        // Add the row headers after the first column is created
        if (col == 0)
            AddRows();

        // Default numeric values after the row headers have been created
        gridVariables[col, 6].Value = 0;

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
        string[] rowText = new string[]
        {
            "Air temperature (°C)",
            "Radiant temperature (°C)",
            "Air velocity (m/s)",
            "Relative humidity (%)",
            "Clothing insulation (clo)",
            "Metabolic rate (mets)",
            "External work (mets)"
        };
        (this as IChildData).AddGridRowHeaders(this.gridVariables, rowText);
    }

    private void DataExample()
    {
        _job = new Job();
        _job.NumberTasks = 2;
        _job.Tasks = new TaskModel[_job.NumberTasks];

        _job.Tasks[0] = new TaskModel();
        _job.Tasks[0].Data.TempAir = 25;
        _job.Tasks[0].Data.TempRad = 25;
        _job.Tasks[0].Data.Velocity = 0.1;
        _job.Tasks[0].Data.RelHumidity = 50;
        _job.Tasks[0].Data.Clothing = 0.61;
        _job.Tasks[0].Data.MetRate = 1;
        _job.Tasks[0].Data.ExternalWork = 0;

        _job.Tasks[1] = new TaskModel();
        _job.Tasks[1].Data.TempAir = 26;
        _job.Tasks[1].Data.TempRad = 26;
        _job.Tasks[1].Data.Velocity = 0.1;
        _job.Tasks[1].Data.RelHumidity = 50;
        _job.Tasks[1].Data.Clothing = 0.8;
        _job.Tasks[1].Data.MetRate = 1;
        _job.Tasks[1].Data.ExternalWork = 0;
    }

    /// <summary>
    /// Shows the data into the grid control
    /// </summary>
    private void DataToGrid()
    {
        // This creates the necessary grid columns in the corresponding ValueChanged event
        updTasks.Value = _job.Tasks.Length;

        for (int i = 0; i < _job.Tasks.Length; i++)
        {
            // Populate the DataGridView with data
            gridVariables[i, 0].Value = _job.Tasks[i].Data.TempAir.ToString();
            gridVariables[i, 1].Value = _job.Tasks[i].Data.TempRad.ToString();
            gridVariables[i, 2].Value = _job.Tasks[i].Data.Velocity.ToString();
            gridVariables[i, 3].Value = _job.Tasks[i].Data.RelHumidity.ToString();
            gridVariables[i, 4].Value = _job.Tasks[i].Data.Clothing.ToString();
            gridVariables[i, 5].Value = _job.Tasks[i].Data.MetRate.ToString();
            gridVariables[i, 6].Value = _job.Tasks[i].Data.ExternalWork.ToString();
        }
    }

    #endregion Private routines

    /// <summary>
    /// Update the form's interface language
    /// </summary>
    /// <param name="culture">Culture used to display the UI</param>
    private void UpdateUI_Language(System.Globalization.CultureInfo culture)
    {
        StringResources.Culture = culture;

        this.Text = StringResources.FormDataTC;
        this.btnOK.Text = StringResources.BtnAccept;
        this.btnCancel.Text = StringResources.BtnCancel;
        this.btnExample.Text = StringResources.BtnExample;

        this.lblTasks.Text = StringResources.NumberOfTasks;

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