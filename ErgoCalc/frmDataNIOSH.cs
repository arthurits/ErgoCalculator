using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;

using ErgoCalc.Models.Lifting;

namespace ErgoCalc;

public partial class FrmDataNIOSH : Form, IChildData
{
    private readonly CultureInfo _culture = CultureInfo.CurrentCulture;
    private IndexType _index;
    private Job _job;

    public object GetData => _job;

    // Default constructor
    public FrmDataNIOSH()
    {
        // VS Designer initialization routine
        InitializeComponent();
        txtConstanteLC.Text = "25";

        // Simulate a click on radRSI
        rad_CheckedChanged(radLI, null);

        listViewTasks.AddGroup();
    }

    public FrmDataNIOSH(CultureInfo culture)
        : this()
    {
        _culture = culture;
        UpdateUI_Language(culture);
    }

    // Overloaded constructor
    public FrmDataNIOSH(Job data)
        : this() // Call the base constructor
    {
        _job = data;
        DataToGrid();
    }

    private void SubTasks_ValueChanged(object sender, EventArgs e)
    {
        int col = Convert.ToInt32(updSubTasks.Value);
        ((IChildData)this).UpdateGridColumns(gridVariables, col);

        //// Add or remove columns
        //if (col > gridVariables.ColumnCount)
        //    for (int i = gridVariables.ColumnCount; i < col; i++) AddColumn(i);
        //else if (col < gridVariables.ColumnCount)
        //    for (int i = gridVariables.ColumnCount - 1; i >= col; i--) gridVariables.Columns.RemoveAt(i);

        // Modify the chkComposite state
        grpIndex.Enabled = col > 0;
        if (col > 1)
        {
            radCLI.Enabled = true;
        }
        else if (col == 1)
        {
            radLI.Checked = true;
            radCLI.Enabled = false;
        }

        // Set the maximum tasks
        if (col > 1) this.updTasks.Maximum = col - 1;

        return;
    }

    private void Tasks_ValueChanged(object sender, EventArgs e)
    {
        ((IChildData)this).UpdateListView(listViewTasks, Convert.ToInt32(updTasks.Value));

        //Int32 tasks = Convert.ToInt32(updTasks.Value);
        //if (tasks > listViewTasks.Groups.Count)
        //{
        //    for (int i = listViewTasks.Groups.Count; i < tasks; i++)
        //        listViewTasks.AddGroup(i);
        //}
        //else if (tasks < listViewTasks.Groups.Count)
        //{
        //    for (int i = tasks; i < listViewTasks.Groups.Count; i++)
        //        listViewTasks.RemoveGroup(listViewTasks.Groups.Count - 1);
        //}
        return;
    }

    private void rad_CheckedChanged(object sender, EventArgs e)
    {
        // Check of the raiser of the event is a checked Checkbox.
        // Of course we also need to to cast it first.
        RadioButton rb = sender as RadioButton;
        if (rb == null) return;
        if (rb.Checked == false) return;    // We only process the check event and disregard the uncheck

        _index = (IndexType)Convert.ToInt32(rb.Tag);

        // Modify tabs and texts
        if (_index == IndexType.IndexLI)
        {
            foreach (DataGridViewColumn col in gridVariables.Columns)
            {
                col.HeaderText = "Task " + col.HeaderText.Substring(col.HeaderText.Length - 1, 1);
                lblSubTasks.Text = "Number of tasks";
            }
            tabData.TabPages[0].Text = "Tasks";
            tabData.TabPages[1].Parent = tabDummy;
            //if (updTasks.Value > 1) updTasks.Value = 1;
        }
        else
        {
            foreach (DataGridViewColumn col in gridVariables.Columns)
            {
                col.HeaderText = "SubTask " + col.HeaderText.Substring(col.HeaderText.Length - 1, 1);
                lblSubTasks.Text = "Number of subtasks";
            }
            tabData.TabPages[0].Text = "SubTasks";
            if (tabDummy.TabPages.Count > 0) tabDummy.TabPages[0].Parent = tabData;
            //if (updTasks.Value < 2) updTasks.Value++;
        }

    }

    private void Accept_Click(object sender, EventArgs e)
    {
        // The form does not return unless all fields are validated. This avoids closing the dialog
        this.DialogResult = DialogResult.None;

        // Validate input before getting the values
        if (!Validation.IsValidRange(txtConstanteLC.Text, 0, 25, true, this)) { txtConstanteLC.Focus(); txtConstanteLC.SelectAll(); return; }
        //double LC = String.IsNullOrEmpty(txtConstanteLC.Text) ? 0.0 : Convert.ToDouble(txtConstanteLC.Text);

        listViewTasks.RemoveEmptyGroups();
        listViewTasks.RemoveEmptyItems();
        updTasks.Value = listViewTasks.Groups.Count;

        // New test
        int ItemIndex;
        _job = new();
        _job.NumberSubTasks = gridVariables.ColumnCount;
        _job.NumberTasks = _index == IndexType.IndexLI ? 1 : Convert.ToInt32(updTasks.Value);
        _job.Tasks = new TaskModel[_job.NumberTasks];
        _job.Order = new int[_job.NumberTasks];
        
        if (radLI.Checked) _job.Model = IndexType.IndexLI;
        if (radCLI.Checked) _job.Model = IndexType.IndexCLI;
        if (radVLI.Checked) _job.Model = IndexType.IndexVLI;
        if (radSLI.Checked) _job.Model = IndexType.IndexSLI;


        for (int i = 0; i < _job.NumberTasks; i++)
        {
            _job.Tasks[i] = new();
            _job.Order[i] = i;

            _job.Tasks[i].NumberSubTasks = _job.Model == IndexType.IndexLI ? Convert.ToInt32(updSubTasks.Value) : listViewTasks.Groups[i].Items.Count;
            _job.Tasks[i].Model = _job.Model;
            _job.Tasks[i].OrderCLI = new int[_job.Tasks[i].NumberSubTasks];
            _job.Tasks[i].SubTasks = new SubTask[_job.Tasks[i].NumberSubTasks];
            _job.Tasks[i].IndexCLI = -1;

            for (int j = 0; j < _job.Tasks[i].NumberSubTasks; j++)
            {
                _job.Tasks[i].SubTasks[j] = new();
                ItemIndex = _job.Model == IndexType.IndexLI ? j : listViewTasks.Groups[i].Items[j].Index;
                //if (_nioshLifting.model == IndexType.IndexLI)
                //    ItemIndex = j;
                //else
                //{
                //    ItemIndex = 0 - 'A';
                //    for (int k = listViewTasks.Groups[i].Items[j].Text.IndexOf(" "); k < listViewTasks.Groups[i].Items[j].Text.Length-1; k++)
                //    {
                //        ItemIndex += listViewTasks.Groups[i].Items[j].Text[k + 1];
                //    }
                //}

                _job.Tasks[i].SubTasks[j].ItemIndex = ItemIndex;
                _job.Tasks[i].SubTasks[j].Task = i;

                //if (!Validation.IsValidRange(gridVariables[ItemIndex, 0].Value, 0, null, true, this)) { gridVariables.CurrentCell = gridVariables[ItemIndex, 0]; gridVariables.BeginEdit(true); return; }
                //if (!Validation.IsValidRange(gridVariables[ItemIndex, 1].Value, 0, null, true, this)) { gridVariables.CurrentCell = gridVariables[ItemIndex, 1]; gridVariables.BeginEdit(true); return; }
                //if (!Validation.IsValidRange(gridVariables[ItemIndex, 2].Value, 0, null, true, this)) { gridVariables.CurrentCell = gridVariables[ItemIndex, 2]; gridVariables.BeginEdit(true); return; }
                //if (!Validation.IsValidRange(gridVariables[ItemIndex, 3].Value, -180, -180, true, this)) { gridVariables.CurrentCell = gridVariables[ItemIndex, 3]; gridVariables.BeginEdit(true); return; }
                //if (!Validation.IsValidRange(gridVariables[ItemIndex, 4].Value, 0, 8, true, this)) { gridVariables.CurrentCell = gridVariables[ItemIndex, 4]; gridVariables.BeginEdit(true); return; }

                _job.Tasks[i].SubTasks[j].Data.LC = Convert.ToDouble(gridVariables[ItemIndex, 0].Value);
                _job.Tasks[i].SubTasks[j].Data.Weight = Convert.ToDouble(gridVariables[ItemIndex, 1].Value);
                _job.Tasks[i].SubTasks[j].Data.h = Convert.ToDouble(gridVariables[ItemIndex, 2].Value);
                _job.Tasks[i].SubTasks[j].Data.v = Convert.ToDouble(gridVariables[ItemIndex, 3].Value);
                _job.Tasks[i].SubTasks[j].Data.d = Convert.ToDouble(gridVariables[ItemIndex, 4].Value);
                _job.Tasks[i].SubTasks[j].Data.f = Convert.ToDouble(gridVariables[ItemIndex, 5].Value);
                _job.Tasks[i].SubTasks[j].Data.td = Convert.ToDouble(gridVariables[ItemIndex, 6].Value);
                _job.Tasks[i].SubTasks[j].Data.a = Convert.ToDouble(gridVariables[ItemIndex, 7].Value);
                _job.Tasks[i].SubTasks[j].Data.c = (Coupling)gridVariables[ItemIndex, 8].Value;

                //_job.Tasks[i].SubTasks[j].Data.LC = LC;

                //_nioshLifting.jobTasks[i].h += _nioshLifting.jobTasks[i].subTasks[j].data.h;  // Calculate mean
                //_nioshLifting.jobTasks[i].OrderCLI[j] = j;
            }
        }

        // Return OK thus closing the dialog
        this.DialogResult = DialogResult.OK;
        return;
    }

    private void Cancel_Click(object sender, EventArgs e)
    {
        // Return empty array
        _job = new();
    }

    private void Example_Click(object sender, EventArgs e)
    {
        // Loads some data example into the grid
        //updSubTasks.Value = 0;
        DataExample();
        DataToGrid();
    }

    #region Private routines
    /// <summary>
    /// Adds a column to the DataGrid View and formates it
    /// </summary>
    /// <param name="col">Column number (zero based)</param>
    private void AddColumn(int col)
    {
        if (gridVariables.Columns.Contains("Column" + (col).ToString())) return;

        string strName = "Task ";
        if (_index != IndexType.IndexLI) strName = "SubTask ";

        // Create the new column
        gridVariables.Columns.Add("Column" + (col).ToString(), strName + ((char)('A' + col)).ToString());
        gridVariables.Columns[col].SortMode = DataGridViewColumnSortMode.NotSortable;
        gridVariables.Columns[col].Width = 85;

        // Add the row headers after the first column is created
        if (col == 0)
        {
            AddRows();

            // Create custom cells with combobox display
            DataGridViewComboBoxCell celdaC = new DataGridViewComboBoxCell();
            DataTable tableC = new DataTable();
            tableC.Columns.Add("Display", typeof(String));
            tableC.Columns.Add("Value", typeof(Int32));
            tableC.Rows.Add(Coupling.Good, (int)Coupling.Good);
            tableC.Rows.Add(Coupling.Poor, (int)Coupling.Poor);
            tableC.Rows.Add(Coupling.NoHandle, (int)Coupling.NoHandle);
            celdaC.DataSource = tableC;
            celdaC.DisplayMember = "Display";
            celdaC.ValueMember = "Value";
            gridVariables.Rows[8].Cells[0] = celdaC;
        }

        // Give format (ComboBox) to the added column cells
        if (col > 0)
            gridVariables.Rows[8].Cells[col] = (DataGridViewComboBoxCell)gridVariables.Rows[8].Cells[col - 1].Clone();

        // Default numeric values after the row headers have been created
        gridVariables[col, 0].Value = 25;

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
        gridVariables.RowCount = 9;
        gridVariables.Rows[0].HeaderCell.Value = "Load constant (kg)";
        gridVariables.Rows[1].HeaderCell.Value = "Weight lifted (kg)";
        gridVariables.Rows[2].HeaderCell.Value = "Horizontal distance (cm)";
        gridVariables.Rows[3].HeaderCell.Value = "Vertical distance (cm)";
        gridVariables.Rows[4].HeaderCell.Value = "Vertical travel distance (cm)";
        gridVariables.Rows[5].HeaderCell.Value = "Lifting frequency (times/min)";
        gridVariables.Rows[6].HeaderCell.Value = "Task duration (hours)";
        gridVariables.Rows[7].HeaderCell.Value = "Twisting angle (°)";
        gridVariables.Rows[8].HeaderCell.Value = "Coupling";
    }

    /// <summary>
    /// Creates some data to show as an example
    /// </summary>
    private void DataExample()
    {
        _job = new()
        {
            NumberTasks = 1,
            NumberSubTasks = 8,
            Model = IndexType.IndexLI,
            Tasks = new TaskModel[1]
        };

        _job.Tasks[0] = new TaskModel
        {
            NumberSubTasks = 8,
            Model = IndexType.IndexLI,
            SubTasks = new SubTask[8]
        };

        _job.Tasks[0].SubTasks[0] = new();
        _job.Tasks[0].SubTasks[0].ItemIndex = 0;
        _job.Tasks[0].SubTasks[0].Data.LC = 25;
        _job.Tasks[0].SubTasks[0].Data.Weight = 3.0;
        _job.Tasks[0].SubTasks[0].Data.Weight = 3.0;
        _job.Tasks[0].SubTasks[0].Data.h = 24;
        _job.Tasks[0].SubTasks[0].Data.v = 38.5;
        _job.Tasks[0].SubTasks[0].Data.d = 76.5;
        _job.Tasks[0].SubTasks[0].Data.a = 0;
        _job.Tasks[0].SubTasks[0].Data.f = 3;
        _job.Tasks[0].SubTasks[0].Data.td = 2;
        _job.Tasks[0].SubTasks[0].Data.c = Coupling.NoHandle;

        _job.Tasks[0].SubTasks[1] = new();
        _job.Tasks[0].SubTasks[1].ItemIndex = 1;
        _job.Tasks[0].SubTasks[1].Data.LC = 25;
        _job.Tasks[0].SubTasks[1].Data.Weight = 3.0;
        _job.Tasks[0].SubTasks[1].Data.h = 24;
        _job.Tasks[0].SubTasks[1].Data.v = 81;
        _job.Tasks[0].SubTasks[1].Data.d = 34;
        _job.Tasks[0].SubTasks[1].Data.a = 0;
        _job.Tasks[0].SubTasks[1].Data.f = 3;
        _job.Tasks[0].SubTasks[1].Data.td = 2;
        _job.Tasks[0].SubTasks[1].Data.c = Coupling.NoHandle;

        _job.Tasks[0].SubTasks[2] = new();
        _job.Tasks[0].SubTasks[2].ItemIndex = 2;
        _job.Tasks[0].SubTasks[2].Data.LC = 25;
        _job.Tasks[0].SubTasks[2].Data.Weight = 3.0;
        _job.Tasks[0].SubTasks[2].Data.h = 24;
        _job.Tasks[0].SubTasks[2].Data.v = 123.5;
        _job.Tasks[0].SubTasks[2].Data.d = 8.5;
        _job.Tasks[0].SubTasks[2].Data.a = 90;
        _job.Tasks[0].SubTasks[2].Data.f = 3;
        _job.Tasks[0].SubTasks[2].Data.td = 2;
        _job.Tasks[0].SubTasks[2].Data.c = Coupling.NoHandle;

        _job.Tasks[0].SubTasks[3] = new();
        _job.Tasks[0].SubTasks[3].ItemIndex = 3;
        _job.Tasks[0].SubTasks[3].Data.LC = 25;
        _job.Tasks[0].SubTasks[3].Data.Weight = 3.0;
        _job.Tasks[0].SubTasks[3].Data.h = 24;
        _job.Tasks[0].SubTasks[3].Data.v = 166;
        _job.Tasks[0].SubTasks[3].Data.d = 51;
        _job.Tasks[0].SubTasks[3].Data.a = 90;
        _job.Tasks[0].SubTasks[3].Data.f = 3;
        _job.Tasks[0].SubTasks[3].Data.td = 2;
        _job.Tasks[0].SubTasks[3].Data.c = Coupling.NoHandle;

        _job.Tasks[0].SubTasks[4] = new();
        _job.Tasks[0].SubTasks[4].ItemIndex = 4;
        _job.Tasks[0].SubTasks[4].Data.LC = 25;
        _job.Tasks[0].SubTasks[4].Data.Weight = 7.0;
        _job.Tasks[0].SubTasks[4].Data.h = 24;
        _job.Tasks[0].SubTasks[4].Data.v = 33;
        _job.Tasks[0].SubTasks[4].Data.d = 82;
        _job.Tasks[0].SubTasks[4].Data.a = 0;
        _job.Tasks[0].SubTasks[4].Data.f = 1;
        _job.Tasks[0].SubTasks[4].Data.td = 1;
        _job.Tasks[0].SubTasks[4].Data.c = Coupling.NoHandle;

        _job.Tasks[0].SubTasks[5] = new();
        _job.Tasks[0].SubTasks[5].ItemIndex = 5;
        _job.Tasks[0].SubTasks[5].Data.LC = 25;
        _job.Tasks[0].SubTasks[5].Data.Weight = 7.0;
        _job.Tasks[0].SubTasks[5].Data.h = 24;
        _job.Tasks[0].SubTasks[5].Data.v = 75.5;
        _job.Tasks[0].SubTasks[5].Data.d = 39.5;
        _job.Tasks[0].SubTasks[5].Data.a = 0;
        _job.Tasks[0].SubTasks[5].Data.f = 1;
        _job.Tasks[0].SubTasks[5].Data.td = 1;
        _job.Tasks[0].SubTasks[5].Data.c = Coupling.NoHandle;

        _job.Tasks[0].SubTasks[6] = new();
        _job.Tasks[0].SubTasks[6].ItemIndex = 6;
        _job.Tasks[0].SubTasks[6].Data.LC = 25;
        _job.Tasks[0].SubTasks[6].Data.Weight = 7.0;
        _job.Tasks[0].SubTasks[6].Data.h = 24;
        _job.Tasks[0].SubTasks[6].Data.v = 118;
        _job.Tasks[0].SubTasks[6].Data.d = 3;
        _job.Tasks[0].SubTasks[6].Data.a = 0;
        _job.Tasks[0].SubTasks[6].Data.f = 1;
        _job.Tasks[0].SubTasks[6].Data.td = 1;
        _job.Tasks[0].SubTasks[6].Data.c = Coupling.NoHandle;

        _job.Tasks[0].SubTasks[7] = new();
        _job.Tasks[0].SubTasks[7].ItemIndex = 7;
        _job.Tasks[0].SubTasks[7].Data.LC = 25;
        _job.Tasks[0].SubTasks[7].Data.Weight = 7.0;
        _job.Tasks[0].SubTasks[7].Data.h = 24;
        _job.Tasks[0].SubTasks[7].Data.v = 160.5;
        _job.Tasks[0].SubTasks[7].Data.d = 45.5;
        _job.Tasks[0].SubTasks[7].Data.a = 0;
        _job.Tasks[0].SubTasks[7].Data.f = 2;
        _job.Tasks[0].SubTasks[7].Data.td = 1;
        _job.Tasks[0].SubTasks[7].Data.c = Coupling.NoHandle;
    }

    /// <summary>
    /// Shows the data into the grid control
    /// </summary>
    /// <param name="data">Array of Model NIOSH data</param>
    private void DataToGrid()
    {
        switch ((int)_job.Model)
        {
            case 0:
                radLI.Checked = true;
                break;
            case 1:
                radCLI.Checked = true;
                break;
            case 2:
                radVLI.Checked = true;
                break;
            case 3:
                radSLI.Checked = true;
                break;
            default:
                radLI.Checked = true;
                break;
        }
        updSubTasks.Value = _job.NumberSubTasks;
        updTasks.Value = _job.NumberTasks;
        int nCol = 0;
        for (var j = 0; j < _job.NumberTasks; j++)
        {
            for (var i = 0; i < _job.Tasks[j].SubTasks.Length; i++)
            {
                // Populate the DataGridView with data
                gridVariables[_job.Tasks[j].SubTasks[i].ItemIndex, 0].Value = _job.Tasks[j].SubTasks[i].Data.LC.ToString();
                gridVariables[_job.Tasks[j].SubTasks[i].ItemIndex, 1].Value = _job.Tasks[j].SubTasks[i].Data.Weight.ToString();
                gridVariables[_job.Tasks[j].SubTasks[i].ItemIndex, 2].Value = _job.Tasks[j].SubTasks[i].Data.h.ToString();
                gridVariables[_job.Tasks[j].SubTasks[i].ItemIndex, 3].Value = _job.Tasks[j].SubTasks[i].Data.v.ToString();
                gridVariables[_job.Tasks[j].SubTasks[i].ItemIndex, 4].Value = _job.Tasks[j].SubTasks[i].Data.d.ToString();
                gridVariables[_job.Tasks[j].SubTasks[i].ItemIndex, 5].Value = _job.Tasks[j].SubTasks[i].Data.f.ToString();
                gridVariables[_job.Tasks[j].SubTasks[i].ItemIndex, 6].Value = _job.Tasks[j].SubTasks[i].Data.td.ToString();
                gridVariables[_job.Tasks[j].SubTasks[i].ItemIndex, 7].Value = _job.Tasks[j].SubTasks[i].Data.a.ToString();
                gridVariables[_job.Tasks[j].SubTasks[i].ItemIndex, 8].Value = (int)_job.Tasks[j].SubTasks[i].Data.c;

                listViewTasks.Items.Add(new ListViewItem("SubTask " + ((char)('A' + _job.Tasks[j].SubTasks[i].ItemIndex)).ToString(), listViewTasks.Groups[j]));

                nCol++;
            }
        }
        
    }

    #endregion

    /// <summary>
    /// Loads an example into the interface
    /// </summary>
    public void LoadExample()
    {

    }

    private void tabData_Selected(object sender, TabControlEventArgs e)
    {
        if (e.TabPageIndex == 1) // tabTasks
        {
            int nDummy = (listViewTasks.Items.Find("Dummy", false)).Length;
            // Create the subtasks, as many as subtasks
            for (int i = listViewTasks.Items.Count - nDummy; i < updSubTasks.Value; i++)
            {
                if (listViewTasks.Groups.Count != 0)
                    listViewTasks.Items.Add(new ListViewItem("SubTask " + ((char)('A' + i)).ToString(), listViewTasks.Groups[0]) { Name = "SubTask " + ((char)('A' + i)).ToString() });
            }
            for (int i = listViewTasks.Items.Count - nDummy; i > updSubTasks.Value; i--)
            {
                listViewTasks.Items.RemoveAt(i - 1);
            }

            listViewTasks.RemoveEmptyItems();
        }
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
