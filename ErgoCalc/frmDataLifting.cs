using System.Data;
using System.Globalization;

using ErgoCalc.Models.Lifting;

namespace ErgoCalc;

public partial class FrmDataLifting : Form, IChildData
{
    private readonly CultureInfo _culture = CultureInfo.CurrentCulture;
    private IndexType _index;
    private Job _job = new();

    public object GetData => _job;

    // Default constructor
    public FrmDataLifting()
    {
        // VS Designer initialization routine
        InitializeComponent();
        txtConstanteLC.Text = "25";

        // Simulate a click on radRSI
        Rad_CheckedChanged(radLI, EventArgs.Empty);

        listViewTasks.AddGroup(StringResources.Task);
    }

    /// <summary>
    /// Overloaded constructor
    /// </summary>
    /// <param name="job"><see cref="Job"/> object containing data to be shown in the form</param>
    /// <param name="culture">Culture information to be used when showing the form's UI texts</param>
    public FrmDataLifting(Job? job = null, CultureInfo? culture = null)
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

    private void SubTasks_ValueChanged(object sender, EventArgs e)
    {
        // Update the number of columns in the grid
        int col = Convert.ToInt32(updSubTasks.Value);
        (this as IChildData).UpdateGridColumns(gridVariables, col);

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

        // Set tabpages text
        SetTabPagesText();

        return;
    }

    private void SetTabPagesText()
    {
        int col = Convert.ToInt32(updSubTasks.Value);

        // Set the tabpages texts
        if (col > 1)
        {
            if (_index == IndexType.IndexLI)
            {
                tabData.TabPages[0].Text = StringResources.Tasks;
                tabDummy.TabPages[0].Text = StringResources.Tasks;
            }
            else
            {
                tabData.TabPages[0].Text = StringResources.Subtasks;
                tabData.TabPages[1].Text = StringResources.Tasks;
            }
        }
        else
        {
            if (_index == IndexType.IndexLI)
            {
                tabData.TabPages[0].Text = StringResources.Task;
                tabDummy.TabPages[0].Text = StringResources.Task;
            }
            else
            {
                tabData.TabPages[0].Text = StringResources.Subtask;
                tabData.TabPages[1].Text = StringResources.Task;
            }
        }
    }

    private void Tasks_ValueChanged(object sender, EventArgs e)
    {
        (this as IChildData).UpdateListView(listViewTasks, Convert.ToInt32(updTasks.Value), StringResources.Task);
    }

    private void Rad_CheckedChanged(object sender, EventArgs e)
    {
        // Check of the raiser of the event is a selected RadioButton.
        // Of course we also need to to cast it first.
        if (sender is not RadioButton rb) return;
        if (rb.Checked == false) return;    // We only process the check event and disregard the uncheck

        _index = (IndexType)Convert.ToInt32(rb.Tag);

        // Modify tabs and texts
        if (_index == IndexType.IndexLI)
        {
            foreach (DataGridViewColumn col in gridVariables.Columns)
            {
                col.HeaderText = $"{StringResources.Task} {col.HeaderText[^1]}";
                lblSubTasks.Text = StringResources.NumberOfTasks;
            }
            //tabData.TabPages[0].Text = StringResources.Task;
            tabData.TabPages[1].Parent = tabDummy;
            //if (updTasks.Value > 1) updTasks.Value = 1;
        }
        else
        {
            foreach (DataGridViewColumn col in gridVariables.Columns)
            {
                col.HeaderText = $"{StringResources.Subtask} {col.HeaderText[^1]}";
                lblSubTasks.Text = StringResources.NumberOfSubtasks;
            }
            //tabData.TabPages[0].Text = StringResources.Subtask;
            if (tabDummy.TabPages.Count > 0) tabDummy.TabPages[0].Parent = tabData;
            //if (updTasks.Value < 2) updTasks.Value++;
        }

        // Set tabpages text
        SetTabPagesText();

        RelocateControls();
    }

    private void Accept_Click(object sender, EventArgs e)
    {
        // The form does not return unless all fields are validated. This avoids closing the dialog
        this.DialogResult = DialogResult.None;

        // Make sure there aren't more subtasks than the number set in the UpDown control
        // Maybe this should be moved in the UpDown value changed event? Not sure since here we first delete any "Dummy" subtask.
        listViewTasks.RemoveEmptyGroups();
        listViewTasks.RemoveEmptyItems();
        if (listViewTasks.Items.Count > updSubTasks.Value)
        {
            for (int i = listViewTasks.Items.Count; i > updSubTasks.Value; i--)
                listViewTasks.Items.RemoveAt(i - 1);
        }

        // New test
        int numTasks = _index == IndexType.IndexLI ? 1 : listViewTasks.Groups.Count;
        _job = new()
        {
            NumberSubTasks = gridVariables.ColumnCount,
            NumberTasks = numTasks,
            Tasks = new TaskModel[numTasks],
            Order = new int[numTasks]
        };

        if (radLI.Checked) _job.Model = IndexType.IndexLI;
        if (radCLI.Checked) _job.Model = IndexType.IndexCLI;
        if (radVLI.Checked) _job.Model = IndexType.IndexVLI;
        if (radSLI.Checked) _job.Model = IndexType.IndexSLI;

        int itemIndex;
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
                itemIndex = _job.Model == IndexType.IndexLI ? j : listViewTasks.Groups[i].Items[j].Index;
                _job.Tasks[i].SubTasks[j].ItemIndex = itemIndex;
                _job.Tasks[i].SubTasks[j].Task = i;

                //if (!Validation.IsValidRange(gridVariables[ItemIndex, 0].Value, 0, null, true, this)) { gridVariables.CurrentCell = gridVariables[ItemIndex, 0]; gridVariables.BeginEdit(true); return; }
                if (!Validation.IsValidRange(gridVariables[itemIndex, 1].Value, 0, 90, true, this)) { gridVariables.CurrentCell = gridVariables[itemIndex, 1]; gridVariables.BeginEdit(true); return; }
                if (!Validation.IsValidRange(gridVariables[itemIndex, 2].Value, 0, null, true, this)) { gridVariables.CurrentCell = gridVariables[itemIndex, 2]; gridVariables.BeginEdit(true); return; }
                if (!Validation.IsValidRange(gridVariables[itemIndex, 3].Value, 0, null, true, this)) { gridVariables.CurrentCell = gridVariables[itemIndex, 3]; gridVariables.BeginEdit(true); return; }
                if (!Validation.IsValidRange(gridVariables[itemIndex, 3].Value, 0, 150, true, this)) { gridVariables.CurrentCell = gridVariables[itemIndex, 3]; gridVariables.BeginEdit(true); return; }
                if (!Validation.IsValidRange(gridVariables[itemIndex, 4].Value, 0, 250, true, this)) { gridVariables.CurrentCell = gridVariables[itemIndex, 4]; gridVariables.BeginEdit(true); return; }
                if (!Validation.IsValidRange(gridVariables[itemIndex, 5].Value, 0, 175, true, this)) { gridVariables.CurrentCell = gridVariables[itemIndex, 5]; gridVariables.BeginEdit(true); return; }
                if (!Validation.IsValidRange(gridVariables[itemIndex, 6].Value, 0, 60, true, this)) { gridVariables.CurrentCell = gridVariables[itemIndex, 6]; gridVariables.BeginEdit(true); return; }
                if (!Validation.IsValidRange(gridVariables[itemIndex, 7].Value, 0, 12, true, this)) { gridVariables.CurrentCell = gridVariables[itemIndex, 7]; gridVariables.BeginEdit(true); return; }
                if (!Validation.IsValidRange(gridVariables[itemIndex, 8].Value, -180, 180, true, this)) { gridVariables.CurrentCell = gridVariables[itemIndex, 8]; gridVariables.BeginEdit(true); return; }

                _job.Tasks[i].SubTasks[j].Data.gender = (Gender)gridVariables[itemIndex, 0].Value;
                _job.Tasks[i].SubTasks[j].Data.age = Convert.ToDouble(gridVariables[itemIndex, 1].Value);
                _job.Tasks[i].SubTasks[j].Data.Weight = Convert.ToDouble(gridVariables[itemIndex, 2].Value);
                _job.Tasks[i].SubTasks[j].Data.h = Convert.ToDouble(gridVariables[itemIndex, 3].Value);
                _job.Tasks[i].SubTasks[j].Data.v = Convert.ToDouble(gridVariables[itemIndex, 4].Value);
                _job.Tasks[i].SubTasks[j].Data.d = Convert.ToDouble(gridVariables[itemIndex, 5].Value);
                _job.Tasks[i].SubTasks[j].Data.f = Convert.ToDouble(gridVariables[itemIndex, 6].Value);
                _job.Tasks[i].SubTasks[j].Data.td = Convert.ToDouble(gridVariables[itemIndex, 7].Value);
                _job.Tasks[i].SubTasks[j].Data.a = Convert.ToDouble(gridVariables[itemIndex, 8].Value);
                _job.Tasks[i].SubTasks[j].Data.c = (Coupling)gridVariables[itemIndex, 9].Value;
                _job.Tasks[i].SubTasks[j].Data.o = Convert.ToBoolean(gridVariables[itemIndex, 10].Value);
                _job.Tasks[i].SubTasks[j].Data.p = Convert.ToBoolean(gridVariables[itemIndex, 11].Value);
                
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
    void IChildData.AddColumn(int col)
    {
        // By default, the DataGrid always contains a single column
        //if (col == 0) return;
        // Check if the column already exists
        if (gridVariables.Columns.Contains($"Column {col}")) return;

        // Create the new column
        string strName = $"{(_index == IndexType.IndexLI ? StringResources.Task : StringResources.Subtask)} ";
        (this as IChildData).AddColumnBasic(gridVariables, col, strName, 85);

        // Add the row headers after the first column is created
        if (col == 0)
        {
            AddRows();
            FormatRows();
        }

        // Give format (ComboBox) to the added column cells
        if (col > 0)
        {
            gridVariables.Rows[0].Cells[col] = (DataGridViewComboBoxCell)gridVariables.Rows[0].Cells[col - 1].Clone();
            gridVariables.Rows[9].Cells[col] = (DataGridViewComboBoxCell)gridVariables.Rows[9].Cells[col - 1].Clone();
            gridVariables.Rows[10].Cells[col] = (DataGridViewComboBoxCell)gridVariables.Rows[10].Cells[col - 1].Clone();
            gridVariables.Rows[11].Cells[col] = (DataGridViewComboBoxCell)gridVariables.Rows[11].Cells[col - 1].Clone();
        }

        // Default numeric values after the row headers have been created
        gridVariables[col, 10].Value = 0;
        gridVariables[col, 11].Value = 0;

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
        (this as IChildData).AddGridRowHeaders(this.gridVariables, StringResources.Lifting_DataInputHeaders);
    }

    /// <summary>
    /// Format the header row with custom cells
    /// </summary>
    private void FormatRows()
    {
        // Get the couplig type texts
        string[] strGender = StringResources.Lifting_GenderType.Split(", ");
        DataTable tableC = new();
        tableC.Columns.Add("Display", typeof(String));
        tableC.Columns.Add("Value", typeof(Int32));
        tableC.Rows.Add(strGender[(int)Gender.Male], (int)Gender.Male);
        tableC.Rows.Add(strGender[(int)Gender.Female], (int)Gender.Female);
        DataGridViewComboBoxCell cellGender = new()
        {
            DataSource = tableC,
            DisplayMember = "Display",
            ValueMember = "Value"
        };
        gridVariables.Rows[0].Cells[0] = cellGender;


        // Get the couplig type texts
        string[] strCoupling = StringResources.Lifting_CouplingType.Split(", ");
        
        // Create custom cells with combobox display
        tableC = new();
        tableC.Columns.Add("Display", typeof(String));
        tableC.Columns.Add("Value", typeof(Int32));
        tableC.Rows.Add(strCoupling[(int)Coupling.Good], (int)Coupling.Good);
        tableC.Rows.Add(strCoupling[(int)Coupling.Fair], (int)Coupling.Fair);
        tableC.Rows.Add(strCoupling[(int)Coupling.Poor], (int)Coupling.Poor);
        DataGridViewComboBoxCell celdaC = new()
        {
            DataSource = tableC,
            DisplayMember = "Display",
            ValueMember = "Value"
        };
        gridVariables.Rows[9].Cells[0] = celdaC;

        // Get the additional multiplier texts
        string[] strTrueFalse = StringResources.Lifting_OneHandedValue.Split(", ");
        tableC = new();
        tableC.Columns.Add("Display", typeof(String));
        tableC.Columns.Add("Value", typeof(Int32));
        tableC.Rows.Add(strTrueFalse[0], 0);
        tableC.Rows.Add(strTrueFalse[1], 1);
        DataGridViewComboBoxCell cellAdditional1 = new()
        {
            DataSource = tableC,
            DisplayMember = "Display",
            ValueMember = "Value"
        };
        gridVariables.Rows[10].Cells[0] = cellAdditional1;

        DataGridViewComboBoxCell cellAdditional2 = new()
        {
            DataSource = tableC,
            DisplayMember = "Display",
            ValueMember = "Value"
        };
        gridVariables.Rows[11].Cells[0] = cellAdditional2;
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
        _job.Tasks[0].SubTasks[0].Data.gender = Gender.Male;
        _job.Tasks[0].SubTasks[0].Data.age = 25;
        _job.Tasks[0].SubTasks[0].Data.Weight = 3.0;
        _job.Tasks[0].SubTasks[0].Data.Weight = 3.0;
        _job.Tasks[0].SubTasks[0].Data.h = 24;
        _job.Tasks[0].SubTasks[0].Data.v = 38.5;
        _job.Tasks[0].SubTasks[0].Data.d = 76.5;
        _job.Tasks[0].SubTasks[0].Data.a = 0;
        _job.Tasks[0].SubTasks[0].Data.f = 3;
        _job.Tasks[0].SubTasks[0].Data.td = 2;
        _job.Tasks[0].SubTasks[0].Data.c = Coupling.Poor;

        _job.Tasks[0].SubTasks[1] = new();
        _job.Tasks[0].SubTasks[1].ItemIndex = 1;
        _job.Tasks[0].SubTasks[1].Data.gender = Gender.Male;
        _job.Tasks[0].SubTasks[1].Data.age = 25;
        _job.Tasks[0].SubTasks[1].Data.Weight = 3.0;
        _job.Tasks[0].SubTasks[1].Data.h = 24;
        _job.Tasks[0].SubTasks[1].Data.v = 81;
        _job.Tasks[0].SubTasks[1].Data.d = 34;
        _job.Tasks[0].SubTasks[1].Data.a = 0;
        _job.Tasks[0].SubTasks[1].Data.f = 3;
        _job.Tasks[0].SubTasks[1].Data.td = 2;
        _job.Tasks[0].SubTasks[1].Data.c = Coupling.Poor;

        _job.Tasks[0].SubTasks[2] = new();
        _job.Tasks[0].SubTasks[2].ItemIndex = 2;
        _job.Tasks[0].SubTasks[2].Data.gender = Gender.Male;
        _job.Tasks[0].SubTasks[2].Data.age = 25;
        _job.Tasks[0].SubTasks[2].Data.Weight = 3.0;
        _job.Tasks[0].SubTasks[2].Data.h = 24;
        _job.Tasks[0].SubTasks[2].Data.v = 123.5;
        _job.Tasks[0].SubTasks[2].Data.d = 8.5;
        _job.Tasks[0].SubTasks[2].Data.a = 90;
        _job.Tasks[0].SubTasks[2].Data.f = 3;
        _job.Tasks[0].SubTasks[2].Data.td = 2;
        _job.Tasks[0].SubTasks[2].Data.c = Coupling.Poor;

        _job.Tasks[0].SubTasks[3] = new();
        _job.Tasks[0].SubTasks[3].ItemIndex = 3;
        _job.Tasks[0].SubTasks[3].Data.gender = Gender.Male;
        _job.Tasks[0].SubTasks[3].Data.age = 25;
        _job.Tasks[0].SubTasks[3].Data.Weight = 3.0;
        _job.Tasks[0].SubTasks[3].Data.h = 24;
        _job.Tasks[0].SubTasks[3].Data.v = 166;
        _job.Tasks[0].SubTasks[3].Data.d = 51;
        _job.Tasks[0].SubTasks[3].Data.a = 90;
        _job.Tasks[0].SubTasks[3].Data.f = 3;
        _job.Tasks[0].SubTasks[3].Data.td = 2;
        _job.Tasks[0].SubTasks[3].Data.c = Coupling.Poor;

        _job.Tasks[0].SubTasks[4] = new();
        _job.Tasks[0].SubTasks[4].ItemIndex = 4;
        _job.Tasks[0].SubTasks[4].Data.gender = Gender.Male;
        _job.Tasks[0].SubTasks[4].Data.age = 25;
        _job.Tasks[0].SubTasks[4].Data.Weight = 7.0;
        _job.Tasks[0].SubTasks[4].Data.h = 24;
        _job.Tasks[0].SubTasks[4].Data.v = 33;
        _job.Tasks[0].SubTasks[4].Data.d = 82;
        _job.Tasks[0].SubTasks[4].Data.a = 0;
        _job.Tasks[0].SubTasks[4].Data.f = 1;
        _job.Tasks[0].SubTasks[4].Data.td = 1;
        _job.Tasks[0].SubTasks[4].Data.c = Coupling.Poor;

        _job.Tasks[0].SubTasks[5] = new();
        _job.Tasks[0].SubTasks[5].ItemIndex = 5;
        _job.Tasks[0].SubTasks[5].Data.gender = Gender.Male;
        _job.Tasks[0].SubTasks[5].Data.age = 25;
        _job.Tasks[0].SubTasks[5].Data.Weight = 7.0;
        _job.Tasks[0].SubTasks[5].Data.h = 24;
        _job.Tasks[0].SubTasks[5].Data.v = 75.5;
        _job.Tasks[0].SubTasks[5].Data.d = 39.5;
        _job.Tasks[0].SubTasks[5].Data.a = 0;
        _job.Tasks[0].SubTasks[5].Data.f = 1;
        _job.Tasks[0].SubTasks[5].Data.td = 1;
        _job.Tasks[0].SubTasks[5].Data.c = Coupling.Poor;

        _job.Tasks[0].SubTasks[6] = new();
        _job.Tasks[0].SubTasks[6].ItemIndex = 6;
        _job.Tasks[0].SubTasks[6].Data.gender = Gender.Male;
        _job.Tasks[0].SubTasks[6].Data.age = 25;
        _job.Tasks[0].SubTasks[6].Data.Weight = 7.0;
        _job.Tasks[0].SubTasks[6].Data.h = 24;
        _job.Tasks[0].SubTasks[6].Data.v = 118;
        _job.Tasks[0].SubTasks[6].Data.d = 3;
        _job.Tasks[0].SubTasks[6].Data.a = 0;
        _job.Tasks[0].SubTasks[6].Data.f = 1;
        _job.Tasks[0].SubTasks[6].Data.td = 1;
        _job.Tasks[0].SubTasks[6].Data.c = Coupling.Poor;

        _job.Tasks[0].SubTasks[7] = new();
        _job.Tasks[0].SubTasks[7].ItemIndex = 7;
        _job.Tasks[0].SubTasks[7].Data.gender = Gender.Male;
        _job.Tasks[0].SubTasks[7].Data.age = 25;
        _job.Tasks[0].SubTasks[7].Data.Weight = 7.0;
        _job.Tasks[0].SubTasks[7].Data.h = 24;
        _job.Tasks[0].SubTasks[7].Data.v = 160.5;
        _job.Tasks[0].SubTasks[7].Data.d = 45.5;
        _job.Tasks[0].SubTasks[7].Data.a = 0;
        _job.Tasks[0].SubTasks[7].Data.f = 2;
        _job.Tasks[0].SubTasks[7].Data.td = 1;
        _job.Tasks[0].SubTasks[7].Data.c = Coupling.Poor;
    }

    /// <summary>
    /// Shows the data into the grid control
    /// </summary>
    private void DataToGrid()
    {
        switch (_job.Model)
        {
            case IndexType.IndexLI:
                radLI.Checked = true;
                break;
            case IndexType.IndexCLI:
                radCLI.Checked = true;
                break;
            case IndexType.IndexSLI:
                radVLI.Checked = true;
                break;
            case IndexType.IndexVLI:
                radSLI.Checked = true;
                break;
            default:
                radLI.Checked = true;
                break;
        }

        // Set the NumericUpDown values, which in turn create the corresponding gridVariables columns and the listViewTasks groups
        updSubTasks.Value = _job.NumberSubTasks;
        updTasks.Value = _job.NumberTasks;

        // Create empty ListViewTasks items, so we can later insert data into the desired position
        for (int i = 0; i < _job.NumberSubTasks; i++)
            listViewTasks.AddEmptyItem(0);

        int nCol;
        for (var j = 0; j < _job.NumberTasks; j++)
        {
            for (var i = 0; i < _job.Tasks[j].SubTasks.Length; i++)
            {
                nCol = _job.Tasks[j].SubTasks[i].ItemIndex;

                // Populate the DataGridView with data
                gridVariables[nCol, 0].Value = (int)_job.Tasks[j].SubTasks[i].Data.gender;
                gridVariables[nCol, 1].Value = _job.Tasks[j].SubTasks[i].Data.age.ToString();
                gridVariables[nCol, 2].Value = _job.Tasks[j].SubTasks[i].Data.Weight.ToString();
                gridVariables[nCol, 3].Value = _job.Tasks[j].SubTasks[i].Data.h.ToString();
                gridVariables[nCol, 4].Value = _job.Tasks[j].SubTasks[i].Data.v.ToString();
                gridVariables[nCol, 5].Value = _job.Tasks[j].SubTasks[i].Data.d.ToString();
                gridVariables[nCol, 6].Value = _job.Tasks[j].SubTasks[i].Data.f.ToString();
                gridVariables[nCol, 7].Value = _job.Tasks[j].SubTasks[i].Data.td.ToString();
                gridVariables[nCol, 8].Value = _job.Tasks[j].SubTasks[i].Data.a.ToString();
                gridVariables[nCol, 9].Value = (int)_job.Tasks[j].SubTasks[i].Data.c;
                gridVariables[nCol, 10].Value = _job.Tasks[j].SubTasks[i].Data.o ? 1 : 0;
                gridVariables[nCol, 11].Value = _job.Tasks[j].SubTasks[i].Data.p ? 1 : 0;

                // We can now insert into the desired position
                ListViewItem test = new($"{StringResources.Subtask} {(char)('A' + _job.Tasks[j].SubTasks[i].ItemIndex)}", listViewTasks.Groups[j]);
                listViewTasks.Items.Insert(_job.Tasks[j].SubTasks[i].ItemIndex, test);
            }
        }

        // Remove the empty items, which were added before to allow the specific insertion
        listViewTasks.RemoveEmptyItems();
    }

    #endregion

    private void TabData_Selected(object sender, TabControlEventArgs e)
    {
        if (e.TabPageIndex == 1) // tabTasks
        {
            int nDummy = (listViewTasks.Items.Find(listViewTasks.DummyName, false)).Length;
            // Create the subtasks, as many as subtasks
            for (int i = listViewTasks.Items.Count - nDummy; i < updSubTasks.Value; i++)
            {
                if (listViewTasks.Groups.Count != 0)
                    listViewTasks.Items.Add(new ListViewItem($"{StringResources.Subtask} {((char)('A' + i)).ToString(_culture)}", listViewTasks.Groups[0]));
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

        this.Text = StringResources.FormDataLifting;
        this.btnAccept.Text = StringResources.BtnAccept;
        this.btnCancel.Text = StringResources.BtnCancel;
        this.btnExample.Text = StringResources.BtnExample;

        this.grpIndex.Text = StringResources.IndexType;
        this.lblSubTasks.Text = _index == IndexType.IndexLI ? StringResources.NumberOfTasks : StringResources.NumberOfSubtasks;
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
        this.updSubTasks.Left = this.lblSubTasks.Left + this.lblSubTasks.Width + 5;
        this.updTasks.Left = this.lblTasks.Left + this.lblTasks.Width + 5;
    }
}
