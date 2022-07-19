using System;
using System.Data;
using System.Windows.Forms;
using ErgoCalc.Models.Lifting;

namespace ErgoCalc;

public partial class frmDataNIOSHmodel : Form, IChildData
{
    private IndexTypeNew _index;
    private JobNew _nioshLifting;

    #region IChildData interface
    public object GetData => _nioshLifting;
    #endregion IChildData interface

    public JobNew GetNioshLifting => _nioshLifting;

    // Default constructor
    public frmDataNIOSHmodel()
    {
        // VS Designer initialization routine
        InitializeComponent();
        txtConstanteLC.Text = "25";

        // Simulate a click on radRSI
        rad_CheckedChanged(radLI, null);

        listViewTasks.AddGroup();

        // Create the first column (zero index base)
        AddColumn();

        // Create the header rows
        gridVariables.RowCount = 8;
        gridVariables.Rows[0].HeaderCell.Value = "Weight lifted (kg)";
        gridVariables.Rows[1].HeaderCell.Value = "Horizontal distance (cm)";
        gridVariables.Rows[2].HeaderCell.Value = "Vertical distance (cm)";
        gridVariables.Rows[3].HeaderCell.Value = "Vertical travel distance (cm)";
        gridVariables.Rows[4].HeaderCell.Value = "Lifting frequency (times/min)";
        gridVariables.Rows[5].HeaderCell.Value = "Task duration (hours)";
        gridVariables.Rows[6].HeaderCell.Value = "Twisting angle (°)";
        gridVariables.Rows[7].HeaderCell.Value = "Coupling";

        // Create custom cells with combobox display
        DataGridViewComboBoxCell celdaC = new DataGridViewComboBoxCell();
        DataTable tableC = new DataTable();

        tableC.Columns.Add("Display", typeof(String));
        tableC.Columns.Add("Value", typeof(Int32));
        tableC.Rows.Add("Good", 2);
        tableC.Rows.Add("Poor", 1);
        tableC.Rows.Add("No handle", 0);
        celdaC.DataSource = tableC;
        celdaC.DisplayMember = "Display";
        celdaC.ValueMember = "Value";

        gridVariables.Rows[7].Cells[0] = celdaC;

    }

    // Overloaded constructor
    public frmDataNIOSHmodel(JobNew data)
        : this() // Call the base constructor
    {
        _nioshLifting = data;
        DataToGrid();
    }

    private void updSubTasks_ValueChanged(object sender, EventArgs e)
    {
        Int32 col = Convert.ToInt32(updSubTasks.Value);
        
        // Add or remove columns
        if (col > gridVariables.ColumnCount)
            for (int i = gridVariables.ColumnCount; i < col; i++) AddColumn(i);
        else if (col < gridVariables.ColumnCount)
            for (int i = gridVariables.ColumnCount - 1; i >= col; i--) gridVariables.Columns.RemoveAt(i);

        // Modify the chkComposite state
        if (col > 1)
        {
            radCLI.Enabled = true;
        }
        else if (col == 1)
        {
            radCLI.Enabled = false;
        }
        else
        {
            grpIndex.Enabled = false;
        }

        // Set the maximum tasks
        this.updTasks.Maximum = col - 1;

        return;
    }

    private void updTasks_ValueChanged(object sender, EventArgs e)
    {
        Int32 tasks = Convert.ToInt32(updTasks.Value);
        if (tasks > listViewTasks.Groups.Count)
        {
            for (int i = listViewTasks.Groups.Count; i < tasks; i++)
                listViewTasks.AddGroup(i);
        }
        else if (tasks < listViewTasks.Groups.Count)
        {
            for (int i = tasks; i < listViewTasks.Groups.Count; i++)
                listViewTasks.RemoveGroup(listViewTasks.Groups.Count - 1);
        }
        return;
    }

    private void rad_CheckedChanged(object sender, EventArgs e)
    {
        // Check of the raiser of the event is a checked Checkbox.
        // Of course we also need to to cast it first.
        RadioButton rb = sender as RadioButton;
        if (rb == null) return;
        if (rb.Checked == false) return;    // We only process the check event and disregard the uncheck

        _index = (IndexTypeNew)Convert.ToInt32(rb.Tag);

        // Modify tabs and texts
        if (_index == IndexTypeNew.IndexLI)
        {
            foreach (DataGridViewColumn col in gridVariables.Columns)
            {
                col.HeaderText = "Task " + col.HeaderText.Substring(col.HeaderText.Length - 1, 1);
                lblSubTasks.Text = "Number of tasks";
            }
            tabData.TabPages[0].Text = "Tasks";
            tabData.TabPages[1].Parent = tabDummy;
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
        }

    }

    private void btnOK_Click(object sender, EventArgs e)
    {
        // The form does not return unless all fields are validated. This avoids closing the dialog
        this.DialogResult = DialogResult.None;

        // Validate input before getting the values
        if (!Validation.IsValidRange(txtConstanteLC.Text, 0, 25, true, this)) { txtConstanteLC.Focus(); txtConstanteLC.SelectAll(); return; }

        // New test
        int ItemIndex;
        _nioshLifting = new();
        if (_index == IndexTypeNew.IndexLI)
            _nioshLifting.numberTasks = 1;
        else
            _nioshLifting.numberTasks = Convert.ToInt32(updTasks.Value);

        _nioshLifting.jobTasks = new TaskNew[_nioshLifting.numberTasks];
        _nioshLifting.order = new int[_nioshLifting.numberTasks];
        _nioshLifting.model = radLI.Checked ? IndexTypeNew.IndexLI : IndexTypeNew.IndexCLI;

        double LC = String.IsNullOrEmpty(txtConstanteLC.Text) ? 0.0 : Convert.ToDouble(txtConstanteLC.Text);
        for (int i = 0; i < _nioshLifting.numberTasks; i++)
        {
            _nioshLifting.jobTasks[i] = new();
            _nioshLifting.order[i] = i;

            _nioshLifting.jobTasks[i].numberSubTasks = _nioshLifting.model == IndexTypeNew.IndexLI ? Convert.ToInt32(updSubTasks.Value) : listViewTasks.Groups[i].Items.Count;
            _nioshLifting.jobTasks[i].model = _nioshLifting.model;
            _nioshLifting.jobTasks[i].OrderCLI = new int[_nioshLifting.jobTasks[i].numberSubTasks];
            _nioshLifting.jobTasks[i].subTasks = new SubTaskNew[_nioshLifting.jobTasks[i].numberSubTasks];
            _nioshLifting.jobTasks[i].CLI = -1;

            for (int j = 0; j < _nioshLifting.jobTasks[i].numberSubTasks; j++)
            {
                _nioshLifting.jobTasks[i].subTasks[j] = new();
                //ItemIndex = _nioshLifting.model == IndexTypeNew.IndexLI ? j : listViewTasks.Groups[i].Items[j].Index;
                if (_nioshLifting.model == IndexTypeNew.IndexLI)
                    ItemIndex = j;
                else
                {
                    ItemIndex = 0 - 'A';
                    for (int k = listViewTasks.Groups[i].Items[j].Text.IndexOf(" "); k < listViewTasks.Groups[i].Items[j].Text.Length-1; k++)
                    {
                        ItemIndex += listViewTasks.Groups[i].Items[j].Text[k + 1];
                    }
                }
                _nioshLifting.jobTasks[i].subTasks[j].itemIndex = ItemIndex;
                _nioshLifting.jobTasks[i].subTasks[j].task = i;

                //if (!Validation.IsValidRange(gridVariables[ItemIndex, 0].Value, 0, null, true, this)) { gridVariables.CurrentCell = gridVariables[ItemIndex, 0]; gridVariables.BeginEdit(true); return; }
                //if (!Validation.IsValidRange(gridVariables[ItemIndex, 1].Value, 0, null, true, this)) { gridVariables.CurrentCell = gridVariables[ItemIndex, 1]; gridVariables.BeginEdit(true); return; }
                //if (!Validation.IsValidRange(gridVariables[ItemIndex, 2].Value, 0, null, true, this)) { gridVariables.CurrentCell = gridVariables[ItemIndex, 2]; gridVariables.BeginEdit(true); return; }
                //if (!Validation.IsValidRange(gridVariables[ItemIndex, 3].Value, -180, -180, true, this)) { gridVariables.CurrentCell = gridVariables[ItemIndex, 3]; gridVariables.BeginEdit(true); return; }
                //if (!Validation.IsValidRange(gridVariables[ItemIndex, 4].Value, 0, 8, true, this)) { gridVariables.CurrentCell = gridVariables[ItemIndex, 4]; gridVariables.BeginEdit(true); return; }

                _nioshLifting.jobTasks[i].subTasks[j].data.weight = Convert.ToDouble(gridVariables[ItemIndex, 0].Value);
                _nioshLifting.jobTasks[i].subTasks[j].data.h = Convert.ToDouble(gridVariables[ItemIndex, 1].Value);
                _nioshLifting.jobTasks[i].subTasks[j].data.v = Convert.ToDouble(gridVariables[ItemIndex, 2].Value);
                _nioshLifting.jobTasks[i].subTasks[j].data.d = Convert.ToDouble(gridVariables[ItemIndex, 3].Value);
                _nioshLifting.jobTasks[i].subTasks[j].data.f = Convert.ToDouble(gridVariables[ItemIndex, 4].Value);
                _nioshLifting.jobTasks[i].subTasks[j].data.td = Convert.ToDouble(gridVariables[ItemIndex, 5].Value);
                _nioshLifting.jobTasks[i].subTasks[j].data.a = Convert.ToDouble(gridVariables[ItemIndex, 6].Value);
                _nioshLifting.jobTasks[i].subTasks[j].data.c = (Coupling)gridVariables[ItemIndex, 7].Value;

                _nioshLifting.jobTasks[i].subTasks[j].factors.LC = LC;

                //_nioshLifting.jobTasks[i].h += _nioshLifting.jobTasks[i].subTasks[j].data.h;  // Calculate mean
                //_nioshLifting.jobTasks[i].OrderCLI[j] = j;
            }
        }

        // Return OK thus closing the dialog
        this.DialogResult = DialogResult.OK;
        return;
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        // Return empty array
        _nioshLifting = new();
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
        if (_index != IndexTypeNew.IndexLI) strName = "SubTask ";

        // Create the new column
        gridVariables.Columns.Add("Column" + (col).ToString(), strName + ((char)('A' + col)).ToString());
        gridVariables.Columns[col].SortMode = DataGridViewColumnSortMode.NotSortable;
        gridVariables.Columns[col].Width = 85;

        // Give format to the cells
        if (col > 0)
            gridVariables.Rows[7].Cells[col] = (DataGridViewComboBoxCell)gridVariables.Rows[7].Cells[col - 1].Clone();

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
    /// Creates some data to show as an example
    /// </summary>
    private void DataExample()
    {
        _nioshLifting = new()
        {
            numberTasks = 1,
            model = IndexTypeNew.IndexLI,
            jobTasks = new TaskNew[1]
        };

        _nioshLifting.jobTasks[0] = new TaskNew
        {
            numberSubTasks = 8,
            model = IndexTypeNew.IndexLI,
            subTasks = new SubTaskNew[8]
        };

        _nioshLifting.jobTasks[0].subTasks[0] = new();
        _nioshLifting.jobTasks[0].subTasks[0].itemIndex = 0;
        _nioshLifting.jobTasks[0].subTasks[0].data.weight = 3.0;
        _nioshLifting.jobTasks[0].subTasks[0].data.weight = 3.0;
        _nioshLifting.jobTasks[0].subTasks[0].data.h = 24;
        _nioshLifting.jobTasks[0].subTasks[0].data.v = 38.5;
        _nioshLifting.jobTasks[0].subTasks[0].data.d = 76.5;
        _nioshLifting.jobTasks[0].subTasks[0].data.a = 0;
        _nioshLifting.jobTasks[0].subTasks[0].data.f = 3;
        _nioshLifting.jobTasks[0].subTasks[0].data.td = 2;
        _nioshLifting.jobTasks[0].subTasks[0].data.c = Coupling.NoHandle;

        _nioshLifting.jobTasks[0].subTasks[1] = new();
        _nioshLifting.jobTasks[0].subTasks[1].itemIndex = 1;
        _nioshLifting.jobTasks[0].subTasks[1].data.weight = 3.0;
        _nioshLifting.jobTasks[0].subTasks[1].data.h = 24;
        _nioshLifting.jobTasks[0].subTasks[1].data.v = 81;
        _nioshLifting.jobTasks[0].subTasks[1].data.d = 34;
        _nioshLifting.jobTasks[0].subTasks[1].data.a = 0;
        _nioshLifting.jobTasks[0].subTasks[1].data.f = 3;
        _nioshLifting.jobTasks[0].subTasks[1].data.td = 2;
        _nioshLifting.jobTasks[0].subTasks[1].data.c = Coupling.NoHandle;

        _nioshLifting.jobTasks[0].subTasks[2] = new();
        _nioshLifting.jobTasks[0].subTasks[2].itemIndex = 2;
        _nioshLifting.jobTasks[0].subTasks[2].data.weight = 3.0;
        _nioshLifting.jobTasks[0].subTasks[2].data.h = 24;
        _nioshLifting.jobTasks[0].subTasks[2].data.v = 123.5;
        _nioshLifting.jobTasks[0].subTasks[2].data.d = 8.5;
        _nioshLifting.jobTasks[0].subTasks[2].data.a = 90;
        _nioshLifting.jobTasks[0].subTasks[2].data.f = 3;
        _nioshLifting.jobTasks[0].subTasks[2].data.td = 2;
        _nioshLifting.jobTasks[0].subTasks[2].data.c = Coupling.NoHandle;

        _nioshLifting.jobTasks[0].subTasks[3] = new();
        _nioshLifting.jobTasks[0].subTasks[3].itemIndex = 3;
        _nioshLifting.jobTasks[0].subTasks[3].data.weight = 3.0;
        _nioshLifting.jobTasks[0].subTasks[3].data.h = 24;
        _nioshLifting.jobTasks[0].subTasks[3].data.v = 166;
        _nioshLifting.jobTasks[0].subTasks[3].data.d = 51;
        _nioshLifting.jobTasks[0].subTasks[3].data.a = 90;
        _nioshLifting.jobTasks[0].subTasks[3].data.f = 3;
        _nioshLifting.jobTasks[0].subTasks[3].data.td = 2;
        _nioshLifting.jobTasks[0].subTasks[3].data.c = Coupling.NoHandle;

        _nioshLifting.jobTasks[0].subTasks[4] = new();
        _nioshLifting.jobTasks[0].subTasks[4].itemIndex = 4;
        _nioshLifting.jobTasks[0].subTasks[4].data.weight = 7.0;
        _nioshLifting.jobTasks[0].subTasks[4].data.h = 24;
        _nioshLifting.jobTasks[0].subTasks[4].data.v = 33;
        _nioshLifting.jobTasks[0].subTasks[4].data.d = 82;
        _nioshLifting.jobTasks[0].subTasks[4].data.a = 0;
        _nioshLifting.jobTasks[0].subTasks[4].data.f = 1;
        _nioshLifting.jobTasks[0].subTasks[4].data.td = 1;
        _nioshLifting.jobTasks[0].subTasks[4].data.c = Coupling.NoHandle;

        _nioshLifting.jobTasks[0].subTasks[5] = new();
        _nioshLifting.jobTasks[0].subTasks[5].itemIndex = 5;
        _nioshLifting.jobTasks[0].subTasks[5].data.weight = 7.0;
        _nioshLifting.jobTasks[0].subTasks[5].data.h = 24;
        _nioshLifting.jobTasks[0].subTasks[5].data.v = 75.5;
        _nioshLifting.jobTasks[0].subTasks[5].data.d = 39.5;
        _nioshLifting.jobTasks[0].subTasks[5].data.a = 0;
        _nioshLifting.jobTasks[0].subTasks[5].data.f = 1;
        _nioshLifting.jobTasks[0].subTasks[5].data.td = 1;
        _nioshLifting.jobTasks[0].subTasks[5].data.c = Coupling.NoHandle;

        _nioshLifting.jobTasks[0].subTasks[6] = new();
        _nioshLifting.jobTasks[0].subTasks[6].itemIndex = 6;
        _nioshLifting.jobTasks[0].subTasks[6].data.weight = 7.0;
        _nioshLifting.jobTasks[0].subTasks[6].data.h = 24;
        _nioshLifting.jobTasks[0].subTasks[6].data.v = 118;
        _nioshLifting.jobTasks[0].subTasks[6].data.d = 3;
        _nioshLifting.jobTasks[0].subTasks[6].data.a = 0;
        _nioshLifting.jobTasks[0].subTasks[6].data.f = 1;
        _nioshLifting.jobTasks[0].subTasks[6].data.td = 1;
        _nioshLifting.jobTasks[0].subTasks[6].data.c = Coupling.NoHandle;

        _nioshLifting.jobTasks[0].subTasks[7] = new();
        _nioshLifting.jobTasks[0].subTasks[7].itemIndex = 7;
        _nioshLifting.jobTasks[0].subTasks[7].data.weight = 7.0;
        _nioshLifting.jobTasks[0].subTasks[7].data.h = 24;
        _nioshLifting.jobTasks[0].subTasks[7].data.v = 160.5;
        _nioshLifting.jobTasks[0].subTasks[7].data.d = 45.5;
        _nioshLifting.jobTasks[0].subTasks[7].data.a = 0;
        _nioshLifting.jobTasks[0].subTasks[7].data.f = 2;
        _nioshLifting.jobTasks[0].subTasks[7].data.td = 1;
        _nioshLifting.jobTasks[0].subTasks[7].data.c = Coupling.NoHandle;
    }

    /// <summary>
    /// Shows the data into the grid control
    /// </summary>
    /// <param name="data">Array of Model NIOSH data</param>
    private void DataToGrid()
    {
        switch ((int)_nioshLifting.model)
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

        updTasks.Value = _nioshLifting.numberTasks;
        int nCol = 0;
        for (var j = 0; j < _nioshLifting.numberTasks; j++)
        {
            //nCol++;
            for (var i = 0; i < _nioshLifting.jobTasks[j].subTasks.Length; i++)
            {
                //Column 0 is already created in the constructor;
                //if ((i + j) > 0) AddColumn(nCol);

                for (int k = nCol; k <= _nioshLifting.jobTasks[j].subTasks[i].itemIndex; k++)
                {
                    if (k > 0)
                    {
                        nCol = k;
                        AddColumn(nCol);
                        nCol++;
                    }
                }

                // Populate the DataGridView with data
                gridVariables[_nioshLifting.jobTasks[j].subTasks[i].itemIndex, 0].Value = _nioshLifting.jobTasks[j].subTasks[i].data.weight.ToString();
                gridVariables[_nioshLifting.jobTasks[j].subTasks[i].itemIndex, 1].Value = _nioshLifting.jobTasks[j].subTasks[i].data.h.ToString();
                gridVariables[_nioshLifting.jobTasks[j].subTasks[i].itemIndex, 2].Value = _nioshLifting.jobTasks[j].subTasks[i].data.v.ToString();
                gridVariables[_nioshLifting.jobTasks[j].subTasks[i].itemIndex, 3].Value = _nioshLifting.jobTasks[j].subTasks[i].data.d.ToString();
                gridVariables[_nioshLifting.jobTasks[j].subTasks[i].itemIndex, 4].Value = _nioshLifting.jobTasks[j].subTasks[i].data.f.ToString();
                gridVariables[_nioshLifting.jobTasks[j].subTasks[i].itemIndex, 5].Value = _nioshLifting.jobTasks[j].subTasks[i].data.td.ToString();
                gridVariables[_nioshLifting.jobTasks[j].subTasks[i].itemIndex, 6].Value = _nioshLifting.jobTasks[j].subTasks[i].data.a.ToString();
                gridVariables[_nioshLifting.jobTasks[j].subTasks[i].itemIndex, 7].Value = (int)_nioshLifting.jobTasks[j].subTasks[i].data.c;

                // Classify
                listViewTasks.Items.Add(new ListViewItem("SubTask " + ((char)('A' + _nioshLifting.jobTasks[j].subTasks[i].itemIndex)).ToString(), listViewTasks.Groups[j]));
                //listViewTasks.Items.Insert(_nioshLifting.jobTasks[j].subTasks[i].itemIndex, new ListViewItem("SubTask " + ((char)('A' + _nioshLifting.jobTasks[j].subTasks[i].itemIndex)).ToString(), listViewTasks.Groups[j]));
                //listViewTasks.Items[nCol].Group = listViewTasks.Groups[j];
            }
        }
        // Update the control's value
        updSubTasks.Value = nCol;
        updTasks.Value = _nioshLifting.numberTasks;
    }

    #endregion

    /// <summary>
    /// Loads an example into the interface
    /// </summary>
    public void LoadExample()
    {
        // Load some data example
        DataExample();
        DataToGrid();
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
}
