using System.Globalization;

using ErgoCalc.Models.StrainIndex;

namespace ErgoCalc;

public partial class FrmDataStrainIndex : Form, IChildData
{
    private readonly CultureInfo _culture = CultureInfo.CurrentCulture;
    private Job _job = new();
    private IndexType _index = IndexType.RSI;

    public object GetData => _job;

    // Default constructor
    public FrmDataStrainIndex()
    {
        // VS Designer initialization routine
        InitializeComponent();

        // Simulate a click on radRSI
        Rad_CheckedChanged(radRSI, EventArgs.Empty);

        listViewTasks.AddGroup(StringResources.Task);
    }

    /// <summary>
    /// Overloaded constructor
    /// </summary>
    /// <param name="job"><see cref="Job"/> object containing data to be shown in the form</param>
    /// <param name="culture">Culture information to be used when showing the form's UI texts</param>
    public FrmDataStrainIndex(Job? job = null, CultureInfo? culture = null)
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

    private void Rad_CheckedChanged(object sender, EventArgs e)
    {
        // Check of the raiser of the event is a checked Checkbox.
        // Of course we also need to to cast it first.
        if (sender is not RadioButton rb) return;
        if (rb.Checked == false) return;    // We only process the check event and disregard the uncheck

        if (radRSI.Checked) _index = IndexType.RSI;
        if (radCOSI.Checked) _index = IndexType.COSI;
        if (radCUSI.Checked) _index = IndexType.CUSI;

        if (_index == IndexType.RSI)
        {
            foreach (DataGridViewColumn col in gridVariables.Columns)
            {
                col.HeaderText = $"{StringResources.Task} {col.HeaderText[^1]}";
                lblSubtasks.Text = StringResources.NumberOfTasks;
            }
            //tabDataStrain.TabPages[0].Text = StringResources.Task;
            tabDataStrain.TabPages[1].Parent = tabDummy;
        }
        else
        {
            foreach (DataGridViewColumn col in gridVariables.Columns)
            {
                col.HeaderText = $"{StringResources.Subtask} {col.HeaderText[^1]}";
                lblSubtasks.Text = StringResources.NumberOfSubtasks;
            }
            //tabDataStrain.TabPages[0].Text = StringResources.Subtask;
            if (tabDummy.TabPages.Count > 0) tabDummy.TabPages[0].Parent = tabDataStrain;
        }

        // Set tabpages text
        SetTabPagesText();

        RelocateControls();
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
        else if (col==1)
        {
            radRSI.Checked = true;
            radCOSI.Enabled = false;
            radCUSI.Enabled = false;
        }

        // Set the maximum tasks
        if (col > 1) this.updTasks.Maximum = col - 1;

        // Set tabpages text
        SetTabPagesText();

        return;
    }

    /// <summary>
    /// Sets the <see cref="TabControl"/> pages text
    /// </summary>
    private void SetTabPagesText()
    {
        int col = Convert.ToInt32(updSubtasks.Value);

        // Set the tabpages texts
        if (col > 1)
        {
            if (_index == IndexType.RSI)
            {
                tabDataStrain.TabPages[0].Text = StringResources.Tasks;
                tabDummy.TabPages[0].Text = StringResources.Tasks;
            }
            else
            {
                tabDataStrain.TabPages[0].Text = StringResources.Subtasks;
                tabDataStrain.TabPages[1].Text = StringResources.Tasks;
            }
        }
        else
        {
            if (_index == IndexType.RSI)
            {
                tabDataStrain.TabPages[0].Text = StringResources.Task;
                tabDummy.TabPages[0].Text = StringResources.Task;
            }
            else
            {
                tabDataStrain.TabPages[0].Text = StringResources.Subtask;
                tabDataStrain.TabPages[1].Text = StringResources.Task;
            }
        }
    }

    private void Tasks_ValueChanged(object sender, EventArgs e)
    {
        (this as IChildData).UpdateListView(listViewTasks, Convert.ToInt32(updTasks.Value), StringResources.Task);
    }

    private void Accept_Click(object sender, EventArgs e)
    {
        // The form does not return unless all fields are validated. This avoids closing the dialog
        this.DialogResult = DialogResult.None;

        // Clean any empty groups
        listViewTasks.RemoveEmptyGroups();
        listViewTasks.RemoveEmptyItems();
        updTasks.Value = listViewTasks.Groups.Count;

        // Save the job definition
        int ItemIndex;
        _job = new()
        {
            NumberSubTasks = gridVariables.ColumnCount,
            NumberTasks = _index == IndexType.RSI ? 1 : listViewTasks.Groups.Count,
            Order = new int[gridVariables.ColumnCount],
            Tasks = new TaskModel[gridVariables.ColumnCount],
            IndexCUSI = -1,
            Model = radRSI.Checked ? IndexType.RSI : (radCOSI.Checked ? IndexType.COSI : IndexType.CUSI)
        };
        //_job.model = (IndexType)Enum.Parse(typeof(IndexType), this.groupIndex.Handle.ToString());

        for (int i = 0; i < _job.NumberTasks; i++)
        {
            _job.Tasks[i] = new();
            _job.Order[i] = i;

            _job.Tasks[i].NumberSubTasks = _index == IndexType.RSI ? (int)updSubtasks.Value : listViewTasks.Groups[i].Items.Count;
            _job.Tasks[i].Order = new int[_job.Tasks[i].NumberSubTasks];
            _job.Tasks[i].SubTasks = new SubTask[_job.Tasks[i].NumberSubTasks];
            _job.Tasks[i].IndexCOSI = -1;
            for (int j = 0; j < _job.Tasks[i].NumberSubTasks; j++)
            {
                _job.Tasks[i].SubTasks[j] = new();
                ItemIndex = _index == IndexType.RSI ? j : listViewTasks.Groups[i].Items[j].Index;
                _job.Tasks[i].SubTasks[j].ItemIndex = ItemIndex;
                
                //if (!Validation.IsValidRange(gridVariables[ItemIndex, 0].Value, 0, null, true, this)) { gridVariables.CurrentCell = gridVariables[ItemIndex, 0]; gridVariables.BeginEdit(true); return; }
                //if (!Validation.IsValidRange(gridVariables[ItemIndex, 1].Value, 0, null, true, this)) { gridVariables.CurrentCell = gridVariables[ItemIndex, 1]; gridVariables.BeginEdit(true); return; }
                //if (!Validation.IsValidRange(gridVariables[ItemIndex, 2].Value, 0, null, true, this)) { gridVariables.CurrentCell = gridVariables[ItemIndex, 2]; gridVariables.BeginEdit(true); return; }
                //if (!Validation.IsValidRange(gridVariables[ItemIndex, 3].Value, -180, -180, true, this)) { gridVariables.CurrentCell = gridVariables[ItemIndex, 3]; gridVariables.BeginEdit(true); return; }
                //if (!Validation.IsValidRange(gridVariables[ItemIndex, 4].Value, 0, 8, true, this)) { gridVariables.CurrentCell = gridVariables[ItemIndex, 4]; gridVariables.BeginEdit(true); return; }

                _job.Tasks[i].SubTasks[j].Data.i = Convert.ToDouble(gridVariables[ItemIndex, 0].Value);
                _job.Tasks[i].SubTasks[j].Data.e = Convert.ToDouble(gridVariables[ItemIndex, 1].Value);
                _job.Tasks[i].SubTasks[j].Data.d = Convert.ToDouble(gridVariables[ItemIndex, 2].Value);
                _job.Tasks[i].SubTasks[j].Data.p = Convert.ToDouble(gridVariables[ItemIndex, 3].Value);
                _job.Tasks[i].SubTasks[j].Data.h = Convert.ToDouble(gridVariables[ItemIndex, 4].Value);  // This should be the same for these subtasks
                
                _job.Tasks[i].h += _job.Tasks[i].SubTasks[j].Data.h;  // Calculate mean
                _job.Tasks[i].Order[j] = j;
            }

            _job.Tasks[i].h /= _job.Tasks[i].NumberSubTasks;
        }

        // Return OK thus closing the dialog
        this.DialogResult = DialogResult.OK;

        // Save the composite option
        //_index = chkComposite.Checked;
        return;
    }

    private void Example_Click(object sender, EventArgs e)
    {
        // Load some data example
        DataExample2();
        DataToGrid();
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
        if (gridVariables.Columns.Contains($"Column {col}")) return;

        // Create the new column
        string strName = $"{(_index == IndexType.RSI ? StringResources.Task : StringResources.Subtask)} ";
        (this as IChildData).AddColumnBasic(gridVariables, col, strName, 85);

        // Add the row headers after the first column is created
        if (col == 0)
            AddRows();

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
        (this as IChildData).AddGridRowHeaders(this.gridVariables, StringResources.StrainIndex_DataInputHeaders);
    }
    
    /// <summary>
    /// Creates some data to show as an example
    /// </summary>
    private void DataExample()
    {
        _job = new()
        {
            NumberTasks = 1,
            Tasks = new TaskModel[1]
        };
        _job.Tasks[0].NumberSubTasks = 8;
        _job.Tasks[0].SubTasks = new SubTask[_job.Tasks[0].NumberSubTasks];
        //_job.JobTasks[1].numberSubTasks = 2;
        //_job.JobTasks[1].SubTasks = new ModelSubTask[_job.JobTasks[1].numberSubTasks];

        _job.Tasks[0].SubTasks[0].ItemIndex = 0;
        _job.Tasks[0].SubTasks[0].Data.i = 0.2;
        _job.Tasks[0].SubTasks[0].Data.e = 5;
        _job.Tasks[0].SubTasks[0].Data.d = 3;
        _job.Tasks[0].SubTasks[0].Data.p = 5;
        _job.Tasks[0].SubTasks[0].Data.h = 4;

        _job.Tasks[0].SubTasks[1].ItemIndex = 1;
        _job.Tasks[0].SubTasks[1].Data.i = 0.2;
        _job.Tasks[0].SubTasks[1].Data.e = 5;
        _job.Tasks[0].SubTasks[1].Data.d = 3;
        _job.Tasks[0].SubTasks[1].Data.p = -5;
        _job.Tasks[0].SubTasks[1].Data.h = 4;

        _job.Tasks[0].SubTasks[2].ItemIndex = 2;
        _job.Tasks[0].SubTasks[2].Data.i = 0.4;
        _job.Tasks[0].SubTasks[2].Data.e = 6;
        _job.Tasks[0].SubTasks[2].Data.d = 3;
        _job.Tasks[0].SubTasks[2].Data.p = -10;
        _job.Tasks[0].SubTasks[2].Data.h = 3;

        _job.Tasks[0].SubTasks[3].ItemIndex = 3;
        _job.Tasks[0].SubTasks[3].Data.i = 0.4;
        _job.Tasks[0].SubTasks[3].Data.e = 4;
        _job.Tasks[0].SubTasks[3].Data.d = 2;
        _job.Tasks[0].SubTasks[3].Data.p = 10;
        _job.Tasks[0].SubTasks[3].Data.h = 3;

        _job.Tasks[0].SubTasks[4].ItemIndex = 4;
        _job.Tasks[0].SubTasks[4].Data.i = 0.4;
        _job.Tasks[0].SubTasks[4].Data.e = 4;
        _job.Tasks[0].SubTasks[4].Data.d = 2;
        _job.Tasks[0].SubTasks[4].Data.p = -10;
        _job.Tasks[0].SubTasks[4].Data.h = 3;

        _job.Tasks[0].SubTasks[5].ItemIndex = 5;
        _job.Tasks[0].SubTasks[5].Data.i = 0.4;
        _job.Tasks[0].SubTasks[5].Data.e = 4;
        _job.Tasks[0].SubTasks[5].Data.d = 2;
        _job.Tasks[0].SubTasks[5].Data.p = 0;
        _job.Tasks[0].SubTasks[5].Data.h = 1;

        _job.Tasks[0].SubTasks[6].ItemIndex = 6;
        _job.Tasks[0].SubTasks[6].Data.i = 0.15;
        _job.Tasks[0].SubTasks[6].Data.e = 2;
        _job.Tasks[0].SubTasks[6].Data.d = 10;
        _job.Tasks[0].SubTasks[6].Data.p = 5;
        _job.Tasks[0].SubTasks[6].Data.h = 8;

        _job.Tasks[0].SubTasks[7].ItemIndex = 7;
        _job.Tasks[0].SubTasks[7].Data.i = 0.15;
        _job.Tasks[0].SubTasks[7].Data.e = 2;
        _job.Tasks[0].SubTasks[7].Data.d = 10;
        _job.Tasks[0].SubTasks[7].Data.p = 5;
        _job.Tasks[0].SubTasks[7].Data.h = 8;

    }

    private void DataExample2()
    {
        _job = new()
        {
            NumberTasks = 2,
            NumberSubTasks = 5,
            Model = IndexType.RSI,
            Tasks = new TaskModel[2]
        };
        
        _job.Tasks[0] = new()
        {
            NumberSubTasks = 3,
            SubTasks = new SubTask[3]
        };

        _job.Tasks[0].SubTasks[0] = new();
        _job.Tasks[0].SubTasks[0].ItemIndex = 0;
        _job.Tasks[0].SubTasks[0].Data.i = 0.7;
        _job.Tasks[0].SubTasks[0].Data.e = 1;
        _job.Tasks[0].SubTasks[0].Data.d = 1;
        _job.Tasks[0].SubTasks[0].Data.p = 0;
        _job.Tasks[0].SubTasks[0].Data.h = 4;

        _job.Tasks[0].SubTasks[1] = new();
        _job.Tasks[0].SubTasks[1].ItemIndex = 1;
        _job.Tasks[0].SubTasks[1].Data.i = 0.4;
        _job.Tasks[0].SubTasks[1].Data.e = 2.6;
        _job.Tasks[0].SubTasks[1].Data.d = 1.2;
        _job.Tasks[0].SubTasks[1].Data.p = 0;
        _job.Tasks[0].SubTasks[1].Data.h = 4;

        _job.Tasks[0].SubTasks[2] = new();
        _job.Tasks[0].SubTasks[2].ItemIndex = 2;
        _job.Tasks[0].SubTasks[2].Data.i = 0.2;
        _job.Tasks[0].SubTasks[2].Data.e = 5;
        _job.Tasks[0].SubTasks[2].Data.d = 3;
        _job.Tasks[0].SubTasks[2].Data.p = 0;
        _job.Tasks[0].SubTasks[2].Data.h = 4;

        _job.Tasks[1] = new()
        {
            NumberSubTasks = 2,
            SubTasks = new SubTask[2]
        };

        _job.Tasks[1].SubTasks[0] = new();
        _job.Tasks[1].SubTasks[0].ItemIndex = 3;
        _job.Tasks[1].SubTasks[0].Data.i = 0.1;
        _job.Tasks[1].SubTasks[0].Data.e = 0.5;
        _job.Tasks[1].SubTasks[0].Data.d = 1;
        _job.Tasks[1].SubTasks[0].Data.p = -15;
        _job.Tasks[1].SubTasks[0].Data.h = 4;

        _job.Tasks[1].SubTasks[1] = new();
        _job.Tasks[1].SubTasks[1].ItemIndex = 4;
        _job.Tasks[1].SubTasks[1].Data.i = 0.5;
        _job.Tasks[1].SubTasks[1].Data.e = 2;
        _job.Tasks[1].SubTasks[1].Data.d = 3;
        _job.Tasks[1].SubTasks[1].Data.p = -15;
        _job.Tasks[1].SubTasks[1].Data.h = 4;

    }
    
    /// <summary>
    /// Shows the data into the grid control
    /// </summary>
    private void DataToGrid()
    {
        switch (_job.Model)
        {
            case IndexType.RSI:
                radRSI.Checked = true;
                break;
            case IndexType.COSI:
                radCOSI.Checked = true;
                break;
            case IndexType.CUSI:
                radCUSI.Checked = true;
                break;
        }

        // Set the NumericUpDown values, which in turn create the corresponding gridVariables columns and the listViewTasks groups
        updSubtasks.Value = _job.NumberSubTasks;
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
                gridVariables[nCol, 0].Value = _job.Tasks[j].SubTasks[i].Data.i.ToString();
                gridVariables[nCol, 1].Value = _job.Tasks[j].SubTasks[i].Data.e.ToString();
                gridVariables[nCol, 2].Value = _job.Tasks[j].SubTasks[i].Data.d.ToString();
                gridVariables[nCol, 3].Value = _job.Tasks[j].SubTasks[i].Data.p.ToString();
                gridVariables[nCol, 4].Value = _job.Tasks[j].SubTasks[i].Data.h.ToString();

                // Classify
                //listViewTasks.Items.Add(new ListViewItem("SubTask " + ((char)('A' + nCol)).ToString(), listViewTasks.Groups[j]));
                // We can now insert into the desired position
                ListViewItem test = new($"{StringResources.Subtask} {(char)('A' + _job.Tasks[j].SubTasks[i].ItemIndex)}", listViewTasks.Groups[j]);
                listViewTasks.Items.Insert(_job.Tasks[j].SubTasks[i].ItemIndex, test);
            }
        }

        // Remove the empty items, which were added before to allow the specific insertion
        listViewTasks.RemoveEmptyItems();
    }

    #endregion

    private void TabDataStrain_Selected(object sender, TabControlEventArgs e)
    {
        if (e.TabPageIndex == 1) // tabTasks
        {
            int nDummy = (listViewTasks.Items.Find("Dummy", false)).Length;
            // Create the subtasks, as many as subtasks
            for (int i = listViewTasks.Items.Count - nDummy; i < updSubtasks.Value; i++)
            {
                if (listViewTasks.Groups.Count != 0)
                    listViewTasks.Items.Add(new ListViewItem($"{StringResources.Subtask} {((char)('A' + i)).ToString(_culture)}", listViewTasks.Groups[0]));
            }
            for (int i = listViewTasks.Items.Count - nDummy; i > updSubtasks.Value; i--)
            {
                listViewTasks.Items.RemoveAt(i - 1);
            }
            
            listViewTasks.RemoveEmptyItems();
            listViewTasks.ShowAllGroups();
        }
    }

    /// <summary>
    /// Update the form's interface language
    /// </summary>
    /// <param name="culture">Culture used to display the UI</param>
    private void UpdateUI_Language()
    {
        UpdateUI_Language(_culture);
    }

    /// <summary>
    /// Update the form's interface language
    /// </summary>
    /// <param name="culture">Culture used to display the UI</param>
    private void UpdateUI_Language(System.Globalization.CultureInfo culture)
    {
        StringResources.Culture = culture;

        this.Text = StringResources.FormDataStrainIndex;

        this.btnAccept.Text = StringResources.BtnAccept;
        this.btnCancel.Text = StringResources.BtnCancel;
        this.btnExample.Text = StringResources.BtnExample;

        this.groupIndex.Text = StringResources.IndexType;
        this.lblSubtasks.Text = _index == IndexType.RSI ? StringResources.NumberOfTasks : StringResources.NumberOfSubtasks;
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
        this.updSubtasks.Left = this.lblSubtasks.Left + this.lblSubtasks.Width + 5;
        this.updTasks.Left = this.lblTasks.Left + this.lblTasks.Width + 5;
    }
}
