﻿using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;

using ErgoCalc.Models.LibertyMutual;

namespace ErgoCalc;

public partial class FrmDataLiberty : Form, IChildData
{
    private readonly CultureInfo _culture = CultureInfo.CurrentCulture;
    private Job _job;
    private readonly string strGridHeader = "Task ";

    public object GetData => _job;

    public FrmDataLiberty()
    {
        InitializeComponent();
               
        // This event is used to change cell values upon the user interacting with some other cells
        gridVariables.CurrentCellDirtyStateChanged += Variables_CurrentCellDirtyStateChanged;
    }

    public FrmDataLiberty(CultureInfo culture)
        : this()
    {
        _culture = culture;
        UpdateUI_Language(culture);
    }

    public FrmDataLiberty(Job job)
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

        // Validate each data starting from column 0
        _job = new();
        _job.NumberTasks = gridVariables.ColumnCount;
        _job.Tasks = new ModelLiberty[_job.NumberTasks];
        for (int i = 0; i < gridVariables.ColumnCount; i++)
        {
            _job.Tasks[i] = new();
            _job.Tasks[i].Data.Type = (TaskType)Convert.ToInt32(gridVariables[i, 0].Value);
            _job.Tasks[i].Data.Gender = (Gender)Convert.ToInt32(gridVariables[i, 7].Value);

            if (_job.Tasks[i].Data.Type == TaskType.Lifting || _job.Tasks[i].Data.Type == TaskType.Lowering)
            {
                if (_job.Tasks[i].Data.Gender == Gender.Male)
                {
                    if (!Validation.IsValidRange(gridVariables[i, 1].Value, 0.25, 0.73, true, this)) { gridVariables.CurrentCell = gridVariables[i, 1]; gridVariables.BeginEdit(true); return; }
                    if (!Validation.IsValidRange(gridVariables[i, 2].Value, 0.25, 2.14, true, this)) { gridVariables.CurrentCell = gridVariables[i, 2]; gridVariables.BeginEdit(true); return; }
                    if (!Validation.IsValidRange(gridVariables[i, 4].Value, 0.25, 2.14, true, this)) { gridVariables.CurrentCell = gridVariables[i, 4]; gridVariables.BeginEdit(true); return; }
                }
                else
                {
                    if (!Validation.IsValidRange(gridVariables[i, 1].Value, 0.20, 0.68, true, this)) { gridVariables.CurrentCell = gridVariables[i, 1]; gridVariables.BeginEdit(true); return; }
                    if (!Validation.IsValidRange(gridVariables[i, 2].Value, 0.25, 1.96, true, this)) { gridVariables.CurrentCell = gridVariables[i, 2]; gridVariables.BeginEdit(true); return; }
                    if (!Validation.IsValidRange(gridVariables[i, 4].Value, 0.25, 1.96, true, this)) { gridVariables.CurrentCell = gridVariables[i, 4]; gridVariables.BeginEdit(true); return; }
                }
                if (!Validation.IsValidRange(gridVariables[i, 6].Value, 0.0021, 20.0, true, this)) { gridVariables[i, 6].Selected = true; gridVariables.BeginEdit(true); return; }
            }
            else if (_job.Tasks[i].Data.Type == TaskType.Pulling || _job.Tasks[i].Data.Type == TaskType.Pushing)
            {
                if (_job.Tasks[i].Data.Gender == Gender.Male)
                {
                    if (!Validation.IsValidRange(gridVariables[i, 5].Value, 0.63, 1.44, true, this)) { gridVariables[i, 5].Selected = true; gridVariables.BeginEdit(true); return; }
                }
                else
                {
                    if (!Validation.IsValidRange(gridVariables[i, 5].Value, 0.58, 1.33, true, this)) { gridVariables[i, 5].Selected = true; gridVariables.BeginEdit(true); return; }
                }
                if (!Validation.IsValidRange(gridVariables[i, 3].Value, 2.1, 61.0, true, this)) { gridVariables[i, 3].Selected = true; gridVariables.BeginEdit(true); return; }
                if (!Validation.IsValidRange(gridVariables[i, 6].Value, 0.0021, 10.0, true, this)) { gridVariables[i, 6].Selected = true; gridVariables.BeginEdit(true); return; }
            }
            else if (_job.Tasks[i].Data.Type == TaskType.Carrying)
            {
                if (_job.Tasks[i].Data.Gender == Gender.Male)
                {
                    if (!Validation.IsValidRange(gridVariables[i, 5].Value, 0.78, 1.10, true, this)) { gridVariables[i, 5].Selected = true; gridVariables.BeginEdit(true); return; }
                }
                else
                {
                    if (!Validation.IsValidRange(gridVariables[i, 5].Value, 0.71, 1.03, true, this)) { gridVariables[i, 5].Selected = true; gridVariables.BeginEdit(true); return; }
                }
                if (!Validation.IsValidRange(gridVariables[i, 3].Value, 2.1, 10.0, true, this)) { gridVariables[i, 3].Selected = true; gridVariables.BeginEdit(true); return; }
                if (!Validation.IsValidRange(gridVariables[i, 6].Value, 0.0021, 10.0, true, this)) { gridVariables[i, 6].Selected = true; gridVariables.BeginEdit(true); return; }
            }

            _job.Tasks[i].Data.HorzReach = Validation.ValidateNumber(gridVariables[i, 1].Value);
            _job.Tasks[i].Data.VertRangeM = Validation.ValidateNumber(gridVariables[i, 2].Value);
            _job.Tasks[i].Data.DistHorz = Validation.ValidateNumber(gridVariables[i, 3].Value);
            _job.Tasks[i].Data.DistVert = Validation.ValidateNumber(gridVariables[i, 4].Value);
            _job.Tasks[i].Data.VertHeight = Validation.ValidateNumber(gridVariables[i, 5].Value);
            _job.Tasks[i].Data.Freq = Validation.ValidateNumber(gridVariables[i, 6].Value); 
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
        Int32 col = Convert.ToInt32(updTasks.Value);

        // Add or remove columns
        if (col > gridVariables.ColumnCount)
            for (int i = gridVariables.ColumnCount; i < col; i++) AddColumn(i);
        else if (col < gridVariables.ColumnCount)
            for (int i = gridVariables.ColumnCount - 1; i >= col; i--) gridVariables.Columns.RemoveAt(i);
    }

    private void Variables_CurrentCellDirtyStateChanged(object sender, EventArgs e)
    {
        var CurrentCell = gridVariables.CurrentCell;
        if (!(CurrentCell is DataGridViewComboBoxCell)) return;
        if (((DataGridViewComboBoxCell)CurrentCell).DisplayMember != "Type") return;

        // Important to avoid running a 2nd time
        // https://stackoverflow.com/questions/5652957/what-event-catches-a-change-of-value-in-a-combobox-in-a-datagridviewcell
        if (!gridVariables.IsCurrentCellDirty) return;


        // This fires the cell value changed (CellValueChanged) handler below
        // By committing the current cell edition, this function will change
        // the current cell dirty state (ie IsCurrentCellDirty),
        // so it will indeed trigger again this event. Hence the 3rd IF of this routine
        // https://stackoverflow.com/questions/9608343/datagridview-combobox-column-change-cell-value-after-selection-from-dropdown-is/22327701
        gridVariables.CommitEdit(DataGridViewDataErrorContexts.Commit);

        gridVariables.EndEdit();

        DataGridViewColumn col = gridVariables.Columns[gridVariables.CurrentCell.ColumnIndex];

        switch (CurrentCell.Value)
        {
            case 0:     // Carrying
            case 3:     // Pulling
            case 4:     // Pushing
                gridVariables.Rows[1].Cells[CurrentCell.ColumnIndex].Value = "——";
                gridVariables.Rows[2].Cells[CurrentCell.ColumnIndex].Value = "——";
                gridVariables.Rows[4].Cells[CurrentCell.ColumnIndex].Value = "——";
                if ((string)gridVariables.Rows[3].Cells[CurrentCell.ColumnIndex].Value == "——") gridVariables.Rows[3].Cells[CurrentCell.ColumnIndex].Value = string.Empty;
                if ((string)gridVariables.Rows[5].Cells[CurrentCell.ColumnIndex].Value == "——") gridVariables.Rows[5].Cells[CurrentCell.ColumnIndex].Value = string.Empty;
                break;
            case 1:
            case 2:
                if ((string)gridVariables.Rows[1].Cells[CurrentCell.ColumnIndex].Value == "——") gridVariables.Rows[1].Cells[CurrentCell.ColumnIndex].Value = string.Empty;
                if ((string)gridVariables.Rows[2].Cells[CurrentCell.ColumnIndex].Value == "——") gridVariables.Rows[2].Cells[CurrentCell.ColumnIndex].Value = string.Empty;
                if ((string)gridVariables.Rows[4].Cells[CurrentCell.ColumnIndex].Value == "——") gridVariables.Rows[4].Cells[CurrentCell.ColumnIndex].Value = string.Empty;
                gridVariables.Rows[3].Cells[CurrentCell.ColumnIndex].Value = "——";
                gridVariables.Rows[5].Cells[CurrentCell.ColumnIndex].Value = "——";
                break;
            default:
                break;
        }

    }

    private void radioButton_CheckedChanged(object sender, EventArgs e)
    {

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

        //string strName = "Case ";
        //if (_index != IndexType.RSI) strName = "SubTask ";

        // Create the new column
        gridVariables.Columns.Add("Column" + (col).ToString(), strGridHeader + ((char)('A' + col)).ToString());
        gridVariables.Columns[col].SortMode = DataGridViewColumnSortMode.NotSortable;
        gridVariables.Columns[col].Width = 90;

        // Add the row headers after the first column is created
        if (col == 0)
        {
            AddRows();

            // Create custom cells with combobox display
            DataGridViewComboBoxCell cellType = new DataGridViewComboBoxCell();
            DataTable tabType = new DataTable();
            tabType.Columns.Add("Type", typeof(String));
            tabType.Columns.Add("TypeValue", typeof(Int32));
            tabType.Rows.Add("Carrying", 0);
            tabType.Rows.Add("Lifting", 1);
            tabType.Rows.Add("Lowering", 2);
            tabType.Rows.Add("Pulling", 3);
            tabType.Rows.Add("Pushing", 4);
            cellType.DataSource = tabType;
            cellType.DisplayMember = "Type";
            cellType.ValueMember = "TypeValue";
            gridVariables.Rows[0].Cells[0] = cellType;

            DataGridViewComboBoxCell cellGender = new DataGridViewComboBoxCell();
            DataTable tabGender = new DataTable();
            tabGender.Columns.Add("Gender", typeof(String));
            tabGender.Columns.Add("GenderValue", typeof(Int32));
            tabGender.Rows.Add("Male", 0);
            tabGender.Rows.Add("Female", 1);
            cellGender.DataSource = tabGender;
            cellGender.DisplayMember = "Gender";
            cellGender.ValueMember = "GenderValue";
            gridVariables.Rows[7].Cells[0] = cellGender;
        }

        // Give format (ComboBox) to the added column cells
        if (col > 0)
        {
            gridVariables.Rows[0].Cells[col] = (DataGridViewComboBoxCell)gridVariables.Rows[0].Cells[col - 1].Clone();
            gridVariables.Rows[7].Cells[col] = (DataGridViewComboBoxCell)gridVariables.Rows[7].Cells[col - 1].Clone();
        }

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
        gridVariables.RowCount = 8;
        gridVariables.Rows[0].HeaderCell.Value = "Type";
        gridVariables.Rows[1].HeaderCell.Value = "Horizontal reach H (m)";
        gridVariables.Rows[2].HeaderCell.Value = "Vertical range middle VRM (m)";
        gridVariables.Rows[3].HeaderCell.Value = "Horizontal distance DH (m)";
        gridVariables.Rows[4].HeaderCell.Value = "Vertical distance DV (m)";
        gridVariables.Rows[5].HeaderCell.Value = "Vertical height V (m)";
        gridVariables.Rows[6].HeaderCell.Value = "Frequency F (actions/min)";
        gridVariables.Rows[7].HeaderCell.Value = "Gender";
    }

    public void LoadExample()
    {

    }

    /// <summary>
    /// Shows the data into the grid control
    /// </summary>
    private void DataToGrid()
    {
        // This creates the necessary grid columns in the corresponding ValueChanged event
        updTasks.Value = _job.NumberTasks;

        for (int i = 0; i < _job.NumberTasks; i++)
        {
            // Populate the DataGridView with data
            gridVariables[i, 0].Value = (int)_job.Tasks[i].Data.Type;
            gridVariables[i, 1].Value = _job.Tasks[i].Data.HorzReach.ToString();
            gridVariables[i, 2].Value = _job.Tasks[i].Data.VertRangeM.ToString();
            gridVariables[i, 3].Value = _job.Tasks[i].Data.DistHorz.ToString();
            gridVariables[i, 4].Value = _job.Tasks[i].Data.DistVert.ToString();
            gridVariables[i, 5].Value = _job.Tasks[i].Data.VertHeight.ToString();
            gridVariables[i, 6].Value = _job.Tasks[i].Data.Freq.ToString();
            gridVariables[i, 7].Value = (int)_job.Tasks[i].Data.Gender;

            if ((_job.Tasks[i].Data.Type == TaskType.Pulling) || (_job.Tasks[i].Data.Type == TaskType.Pushing))
            {
                gridVariables[i, 1].Value = "——";
                gridVariables[i, 2].Value = "——";
                gridVariables[i, 4].Value = "——";
            }
            else
            {
                gridVariables[i, 3].Value = "——";
                gridVariables[i, 5].Value = "——";
            }

        }
    }

    private void DataExample()
    {
        _job = new Job();
        _job.NumberTasks = 2;
        _job.Tasks = new ModelLiberty[_job.NumberTasks];

        _job.Tasks[0] = new();
        _job.Tasks[0].Data.Type = TaskType.Lifting;
        _job.Tasks[0].Data.Gender = Gender.Female;
        _job.Tasks[0].Data.HorzReach = 0.38;
        _job.Tasks[0].Data.VertRangeM = 1.03;
        _job.Tasks[0].Data.DistHorz = 0;
        _job.Tasks[0].Data.DistVert = 0.51;
        _job.Tasks[0].Data.VertHeight = 0;
        _job.Tasks[0].Data.Freq = 1;

        _job.Tasks[1] = new();
        _job.Tasks[1].Data.Type = TaskType.Pulling;
        _job.Tasks[1].Data.Gender = Gender.Male;
        _job.Tasks[1].Data.HorzReach = 0;
        _job.Tasks[1].Data.VertRangeM = 0;
        _job.Tasks[1].Data.DistHorz = 20;
        _job.Tasks[1].Data.DistVert = 0;
        _job.Tasks[1].Data.VertHeight = 1.2;
        _job.Tasks[1].Data.Freq = 1;
    }

    #endregion Private routines

    /// <summary>
    /// Update the form's interface language
    /// </summary>
    /// <param name="culture">Culture used to display the UI</param>
    private void UpdateUI_Language(System.Globalization.CultureInfo culture)
    {
        StringResources.Culture = culture;

        this.btnOK.Text = StringResources.BtnAccept;
        this.btnCancel.Text = StringResources.BtnCancel;
        this.btnExample.Text = StringResources.BtnExample;

        // Relocate controls
        RelocateControls();
    }

    /// <summary>
    /// Relocate controls to compensate for the culture text length in labels
    /// </summary>
    private void RelocateControls()
    {
    }
}