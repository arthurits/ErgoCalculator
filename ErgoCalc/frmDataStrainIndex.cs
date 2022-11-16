using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Forms;

using ErgoCalc.Models.StrainIndex;

namespace ErgoCalc;

public partial class FrmDataStrainIndex : Form, IChildData
{
    private readonly CultureInfo _culture = CultureInfo.CurrentCulture;
    private Job _job;
    private IndexType _index;

    public object GetData => _job;

    // Default constructor
    public FrmDataStrainIndex()
    {
        // VS Designer initialization routine
        InitializeComponent();

        // Simulate a click on radRSI
        radioButton_CheckedChanged(radRSI, null);

        listViewTasks.AddGroup();
    }

    // Overloaded constructor
    public FrmDataStrainIndex(Job job)
        : this() // Call the base constructor
    {
        _job = job;
        DataToGrid();

        //_culture = culture;
        //UpdateUI_Language(_culture);
    }

    private void radioButton_CheckedChanged(object sender, EventArgs e)
    {
        // Check of the raiser of the event is a checked Checkbox.
        // Of course we also need to to cast it first.
        RadioButton rb = sender as RadioButton;
        if (rb == null) return;
        if (rb.Checked == false) return;    // We only process the check event and disregard the uncheck

        if (radRSI.Checked) _index = IndexType.RSI;
        if (radCOSI.Checked) _index = IndexType.COSI;
        if (radCUSI.Checked) _index = IndexType.CUSI;

        if (_index == IndexType.RSI)
        {
            foreach (DataGridViewColumn col in gridVariables.Columns)
            {
                col.HeaderText = "Task " + col.HeaderText.Substring(col.HeaderText.Length - 1, 1);
                lblSubtasks.Text = "Number of tasks";
            }
            tabDataStrain.TabPages[0].Text = "Tasks";
            tabDataStrain.TabPages[1].Parent = tabDummy;
        }
        else
        {
            foreach (DataGridViewColumn col in gridVariables.Columns)
            {
                col.HeaderText = "SubTask " + col.HeaderText.Substring(col.HeaderText.Length - 1, 1);
                lblSubtasks.Text = "Number of subtasks";
            }
            tabDataStrain.TabPages[0].Text = "SubTasks";
            if (tabDummy.TabPages.Count > 0) tabDummy.TabPages[0].Parent = tabDataStrain;
        }
    }

    private void Subtasks_ValueChanged(object sender, EventArgs e)
    {
        Int32 col = Convert.ToInt32(updSubtasks.Value);

        // Add or remove columns
        if (col > gridVariables.ColumnCount)
            for (int i = gridVariables.ColumnCount; i < col; i++) AddColumn(i);
        else if (col < gridVariables.ColumnCount)
            for (int i = gridVariables.ColumnCount - 1; i >= col; i--) gridVariables.Columns.RemoveAt(i);

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

        return;
    }

    private void Tasks_ValueChanged(object sender, EventArgs e)
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
        _job = new();
        _job.NumberSubTasks = gridVariables.ColumnCount;
        _job.NumberTasks = _index == IndexType.RSI ? 1 : listViewTasks.Groups.Count;
        _job.Order = new int[_job.NumberTasks];
        _job.Tasks = new TaskModel[_job.NumberTasks];
        _job.IndexCUSI = -1;
        _job.Model = radRSI.Checked ? IndexType.RSI : (radCOSI.Checked ? IndexType.COSI : IndexType.CUSI);
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
    private void AddColumn(Int32 col)
    {
        // By default, the DataGrid always contains a single column
        //if (col == 0) return;
        if (gridVariables.Columns.Contains("Column" + (col).ToString())) return;

        string strName = "Task ";
        if (_index != IndexType.RSI) strName = "SubTask ";

        // Create the new column
        //gridVariables.Columns.Add("Column" + (col + 1).ToString(), "Task " + strTasks[col]);
        gridVariables.Columns.Add("Column" + (col).ToString(), strName + ((char)('A' + col)).ToString());
        gridVariables.Columns[col].SortMode = DataGridViewColumnSortMode.NotSortable;
        gridVariables.Columns[col].Width = 85;

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
        AddColumn(gridVariables.Columns.Count);
    }

    /// <summary>
    /// Adds the headercell values for each row
    /// </summary>
    private void AddRows()
    {
        // Create the header rows
        gridVariables.RowCount = 5;
        gridVariables.Rows[0].HeaderCell.Value = "Intensity of exertion";
        gridVariables.Rows[1].HeaderCell.Value = "Efforts per minute";
        gridVariables.Rows[2].HeaderCell.Value = "Duration per exertion";
        gridVariables.Rows[3].HeaderCell.Value = "Hand/wrist posture";
        gridVariables.Rows[4].HeaderCell.Value = "Duration of task per day";
    }
    
    /// <summary>
    /// Creates some data to show as an example
    /// </summary>
    private void DataExample()
    {
        _job = new();
        _job.NumberTasks = 1;
        _job.Tasks = new TaskModel[_job.NumberTasks];
        _job.Tasks[0].NumberSubTasks = 8;
        _job.Tasks[0].SubTasks = new SubTask[_job.Tasks[0].NumberSubTasks];
        //_job.JobTasks[1].numberSubTasks = 2;
        //_job.JobTasks[1].SubTasks = new ModelSubTask[_job.JobTasks[1].numberSubTasks];

        _job.Tasks[0].SubTasks[0].Data.i = 0.2;
        _job.Tasks[0].SubTasks[0].Data.e = 5;
        _job.Tasks[0].SubTasks[0].Data.d = 3;
        _job.Tasks[0].SubTasks[0].Data.p = 5;
        _job.Tasks[0].SubTasks[0].Data.h = 4;

        _job.Tasks[0].SubTasks[1].Data.i = 0.2;
        _job.Tasks[0].SubTasks[1].Data.e = 5;
        _job.Tasks[0].SubTasks[1].Data.d = 3;
        _job.Tasks[0].SubTasks[1].Data.p = -5;
        _job.Tasks[0].SubTasks[1].Data.h = 4;

        _job.Tasks[0].SubTasks[2].Data.i = 0.4;
        _job.Tasks[0].SubTasks[2].Data.e = 6;
        _job.Tasks[0].SubTasks[2].Data.d = 3;
        _job.Tasks[0].SubTasks[2].Data.p = -10;
        _job.Tasks[0].SubTasks[2].Data.h = 3;

        _job.Tasks[0].SubTasks[3].Data.i = 0.4;
        _job.Tasks[0].SubTasks[3].Data.e = 4;
        _job.Tasks[0].SubTasks[3].Data.d = 2;
        _job.Tasks[0].SubTasks[3].Data.p = 10;
        _job.Tasks[0].SubTasks[3].Data.h = 3;

        _job.Tasks[0].SubTasks[4].Data.i = 0.4;
        _job.Tasks[0].SubTasks[4].Data.e = 4;
        _job.Tasks[0].SubTasks[4].Data.d = 2;
        _job.Tasks[0].SubTasks[4].Data.p = -10;
        _job.Tasks[0].SubTasks[4].Data.h = 3;

        _job.Tasks[0].SubTasks[5].Data.i = 0.4;
        _job.Tasks[0].SubTasks[5].Data.e = 4;
        _job.Tasks[0].SubTasks[5].Data.d = 2;
        _job.Tasks[0].SubTasks[5].Data.p = 0;
        _job.Tasks[0].SubTasks[5].Data.h = 1;

        _job.Tasks[0].SubTasks[6].Data.i = 0.15;
        _job.Tasks[0].SubTasks[6].Data.e = 2;
        _job.Tasks[0].SubTasks[6].Data.d = 10;
        _job.Tasks[0].SubTasks[6].Data.p = 5;
        _job.Tasks[0].SubTasks[6].Data.h = 8;

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
        _job.Tasks[0].SubTasks[0].Data.i = 0.7;
        _job.Tasks[0].SubTasks[0].Data.e = 1;
        _job.Tasks[0].SubTasks[0].Data.d = 1;
        _job.Tasks[0].SubTasks[0].Data.p = 0;
        _job.Tasks[0].SubTasks[0].Data.h = 4;

        _job.Tasks[0].SubTasks[1] = new();
        _job.Tasks[0].SubTasks[1].Data.i = 0.4;
        _job.Tasks[0].SubTasks[1].Data.e = 2.6;
        _job.Tasks[0].SubTasks[1].Data.d = 1.2;
        _job.Tasks[0].SubTasks[1].Data.p = 0;
        _job.Tasks[0].SubTasks[1].Data.h = 4;

        _job.Tasks[0].SubTasks[2] = new();
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
        _job.Tasks[1].SubTasks[0].Data.i = 0.1;
        _job.Tasks[1].SubTasks[0].Data.e = 0.5;
        _job.Tasks[1].SubTasks[0].Data.d = 1;
        _job.Tasks[1].SubTasks[0].Data.p = -15;
        _job.Tasks[1].SubTasks[0].Data.h = 4;

        _job.Tasks[1].SubTasks[1] = new();
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
        switch ((int)_job.Model)
        {
            case 0:
                radRSI.Checked = true;
                break;
            case 1:
                radCOSI.Checked = true;
                break;
            case 2:
                radCUSI.Checked = true;
                break;
        }
        updSubtasks.Value = _job.NumberSubTasks;
        updTasks.Value = _job.NumberTasks;
        int nCol = 0;
        for (var j = 0; j < _job.NumberTasks; j++)
        {
            for (var i = 0; i < _job.Tasks[j].SubTasks.Length; i++)
            {
                // Populate the DataGridView with data
                gridVariables[nCol, 0].Value = _job.Tasks[j].SubTasks[i].Data.i.ToString();
                gridVariables[nCol, 1].Value = _job.Tasks[j].SubTasks[i].Data.e.ToString();
                gridVariables[nCol, 2].Value = _job.Tasks[j].SubTasks[i].Data.d.ToString();
                gridVariables[nCol, 3].Value = _job.Tasks[j].SubTasks[i].Data.p.ToString();
                gridVariables[nCol, 4].Value = _job.Tasks[j].SubTasks[i].Data.h.ToString();

                // Classify
                listViewTasks.Items.Add(new ListViewItem("SubTask " + ((char)('A' + nCol)).ToString(), listViewTasks.Groups[j]));

                nCol++;
            }
        }
        
    }

    #endregion

    public void LoadExample()
    {

    }

    private void tabDataStrain_Selected(object sender, TabControlEventArgs e)
    {
        if (e.TabPageIndex == 1) // tabTasks
        {
            int nDummy = (listViewTasks.Items.Find("Dummy", false)).Length;
            // Create the subtasks, as many as subtasks
            for (int i = listViewTasks.Items.Count - nDummy; i < updSubtasks.Value; i++)
            {
                if (listViewTasks.Groups.Count != 0)
                    listViewTasks.Items.Add(new ListViewItem("SubTask " + ((char)('A' + i)).ToString(), listViewTasks.Groups[0]));
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

        this.btnOK.Text = StringResources.BtnAccept;
        this.btnCancel.Text = StringResources.BtnCancel;
        this.btnExample.Text = StringResources.BtnExample;

        // Relocate controls
        //RelocateControls();
    }

    /// <summary>
    /// Relocate controls to compensate for the culture text length in labels
    /// </summary>
    private void RelocateControls()
    {
    }
}
