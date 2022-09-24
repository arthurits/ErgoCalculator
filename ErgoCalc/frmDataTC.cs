using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ErgoCalc.Models.ThermalComfort;

namespace ErgoCalc;

public partial class frmDataTC : Form, IChildData
{
    //private List<ModelTC> _data;
    private Job _job;

    public object GetData => _job;
    public Job GetJob => _job;

    public frmDataTC()
    {
        InitializeComponent();

        // Create the first column (zero index base)
        AddColumn();

        // Create the header rows
        gridVariables.RowCount = 7;
        gridVariables.Rows[0].HeaderCell.Value = "Air temperature (°C)";
        gridVariables.Rows[1].HeaderCell.Value = "Radiant temperature (°C)";
        gridVariables.Rows[2].HeaderCell.Value = "Air velocity (m/s)";
        gridVariables.Rows[3].HeaderCell.Value = "Relative humidity (%)";
        gridVariables.Rows[4].HeaderCell.Value = "Clothing insulation (clo)";
        gridVariables.Rows[5].HeaderCell.Value = "Metabolic rate (mets)";
        gridVariables.Rows[6].HeaderCell.Value = "External work (mets)";
        gridVariables[0, 6].Value = 0;

    }

    public frmDataTC(Job job)
        : this()
    {
        _job = job;
        DataToGrid();
    }

    #region Form events
    private void btnOK_Click(object sender, EventArgs e)
    {
        // The form does not return unless all fields are validated. This avoids closing the dialog
        this.DialogResult = DialogResult.None;

        _job = new();
        
        // Save the values entered
        _job.Tasks = new Task[Convert.ToInt32(updSubtasks.Value)];
        for (int i = 0; i < _job.Tasks.Length; i++)
        {
            _job.Tasks[i] = new Task();
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

    private void updSubtasks_ValueChanged(object sender, EventArgs e)
    {
        Int32 col = Convert.ToInt32(updSubtasks.Value);

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

        string strName = "Case ";
        //if (_index != IndexType.RSI) strName = "SubTask ";

        // Create the new column
        gridVariables.Columns.Add("Column" + (col).ToString(), strName + ((char)('A' + col)).ToString());
        gridVariables.Columns[col].SortMode = DataGridViewColumnSortMode.NotSortable;
        gridVariables.Columns[col].Width = 70;

        if (col > 0) gridVariables[col, 6].Value = 0;

        // Give format to the cells
        // if (col > 0) gridVariables.Rows[7].Cells[col] = (DataGridViewComboBoxCell)gridVariables.Rows[7].Cells[col - 1].Clone();

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
    /// Shows the data into the grid control
    /// </summary>
    private void DataToGrid()
    {
        updSubtasks.Value = _job.Tasks.Length;

        for (int i = 0; i < _job.Tasks.Length; i++)
        {
            //Column 0 is already created in the constructor;
            //if (i > 0) AddColumn();

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

    public void LoadExample()
    {
        _job = new Job();
        _job.NumberTasks = 2;
        _job.Tasks = new Task[_job.NumberTasks];

        _job.Tasks[0] = new Task();
        _job.Tasks[0].Data.TempAir = 25;
        _job.Tasks[0].Data.TempRad = 25;
        _job.Tasks[0].Data.Velocity = 0.1;
        _job.Tasks[0].Data.RelHumidity = 50;
        _job.Tasks[0].Data.Clothing = 0.61;
        _job.Tasks[0].Data.MetRate = 1;
        _job.Tasks[0].Data.ExternalWork = 0;

        _job.Tasks[1] = new Task();
        _job.Tasks[1].Data.TempAir = 26;
        _job.Tasks[1].Data.TempRad = 26;
        _job.Tasks[1].Data.Velocity = 0.1;
        _job.Tasks[1].Data.RelHumidity = 50;
        _job.Tasks[1].Data.Clothing = 0.8;
        _job.Tasks[1].Data.MetRate = 1;
        _job.Tasks[1].Data.ExternalWork = 0;

        DataToGrid();
    }
}