using System;
using System.Windows.Forms;

using ErgoCalc.Models.ThermalComfort;

namespace ErgoCalc;

public partial class FrmDataTC : Form, IChildData
{
    private Job _job;
    private readonly string strGridHeader = "Case ";
    public object GetData => _job;

    public FrmDataTC()
    {
        InitializeComponent();
    }

    public FrmDataTC(Job job)
        : this()
    {
        _job = job;
        DataToGrid();
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
        int col = Convert.ToInt32(updTasks.Value);

        // Add or remove columns
        if (col > gridVariables.ColumnCount)
            for (int i = gridVariables.ColumnCount; i < col; i++) AddColumn(i);
        else if (col < gridVariables.ColumnCount)
            for (int i = gridVariables.ColumnCount - 1; i >= col; i--) gridVariables.Columns.RemoveAt(i);
    }
    #endregion Form events

    #region Private routines
    /// <summary>
    /// Adds a column to the DataGrid View and formates it
    /// </summary>
    /// <param name="col">Column number (zero based)</param>
    private void AddColumn(Int32 col)
    {
        // By default, the DataGrid always contains a single column
        //if (col == 0) return;
        if (gridVariables.Columns.Contains("Column" + (col).ToString())) return;

        // Create the new column
        gridVariables.Columns.Add("Column" + (col).ToString(), strGridHeader + ((char)('A' + col)).ToString());
        gridVariables.Columns[col].SortMode = DataGridViewColumnSortMode.NotSortable;
        gridVariables.Columns[col].Width = 70;

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
        AddColumn(gridVariables.Columns.Count);
    }

    /// <summary>
    /// Adds the headercell values for each row
    /// </summary>
    private void AddRows()
    {
        // Create the header rows
        gridVariables.RowCount = 7;
        gridVariables.Rows[0].HeaderCell.Value = "Air temperature (°C)";
        gridVariables.Rows[1].HeaderCell.Value = "Radiant temperature (°C)";
        gridVariables.Rows[2].HeaderCell.Value = "Air velocity (m/s)";
        gridVariables.Rows[3].HeaderCell.Value = "Relative humidity (%)";
        gridVariables.Rows[4].HeaderCell.Value = "Clothing insulation (clo)";
        gridVariables.Rows[5].HeaderCell.Value = "Metabolic rate (mets)";
        gridVariables.Rows[6].HeaderCell.Value = "External work (mets)";
    }

    public void LoadExample()
    {

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

}