using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ErgoCalc.Models.Strain;
//using ErgoCalc;

namespace ErgoCalc
{
    public partial class frmDataStrainIndex : Form
    {
        //private ModelSubTask[] _subtasks;
        //private int[][] _tasks;
        private ModelJob _job;
        private IndexType _index;

        //public ModelSubTask[] SubTasks { get => _subtasks; }
        //public int[][] Tasks { get => _tasks;}
        public ModelJob Job { get => _job; }
        public IndexType Index { get => _index;}

        // Default constructor
        public frmDataStrainIndex()
        {
            // VS Designer initialization routine
            InitializeComponent();

            // Simulate a click on radRSI
            radioButton_CheckedChanged(radRSI, null);

            listViewTasks.AddGroup();

            // Create the first column (zero index base)
            AddColumn();

            // Create the header rows
            gridVariables.RowCount = 5;
            gridVariables.Rows[0].HeaderCell.Value = "Intensity of exertion";
            gridVariables.Rows[1].HeaderCell.Value = "Efforts per minute";
            gridVariables.Rows[2].HeaderCell.Value = "Duration per exertion";
            gridVariables.Rows[3].HeaderCell.Value = "Hand/wrist posture";
            gridVariables.Rows[4].HeaderCell.Value = "Duration of task per day";

        }

        // Overloaded constructor
        public frmDataStrainIndex(ModelJob job)
            : this() // Call the base constructor
        {
            _job = job;
            DataToGrid();
            tabDataStrain_Selected(null, new TabControlEventArgs(tabTasks, 1, TabControlAction.Selecting));
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

        private void updSubtasks_ValueChanged(object sender, EventArgs e)
        {
            Int32 col = Convert.ToInt32(updSubtasks.Value);

            // Add or remove columns
            if (col > gridVariables.ColumnCount)
                for (int i = gridVariables.ColumnCount; i < col; i++) AddColumn(i);
            else if (col < gridVariables.ColumnCount)
                for (int i = gridVariables.ColumnCount - 1; i >= col; i--) gridVariables.Columns.RemoveAt(i);

            // Modify the chkComposite state
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
            else
            {
                groupIndex.Enabled = false;
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

        private void btnOK_Click(object sender, EventArgs e)
        {
            // Save the job definition
            int ItemIndex;
            _job.numberTasks = _index == IndexType.RSI ? 1 : listViewTasks.Groups.Count;
            _job.order = new int[_job.numberTasks];
            _job.JobTasks = new ModelTask[_job.numberTasks];
            _job.index = -1;
            _job.model = radRSI.Checked ? IndexType.RSI : (radCOSI.Checked ? IndexType.COSI : IndexType.CUSI);
            //_job.model = (IndexType)Enum.Parse(typeof(IndexType), this.groupIndex.Handle.ToString());


            for (int i = 0; i < _job.numberTasks; i++)
            {
                _job.order[i] = i;

                _job.JobTasks[i].numberSubTasks = _index == IndexType.RSI ? (int)updSubtasks.Value : listViewTasks.Groups[i].Items.Count;
                _job.JobTasks[i].order = new int[_job.JobTasks[i].numberSubTasks];
                _job.JobTasks[i].SubTasks = new ModelSubTask[_job.JobTasks[i].numberSubTasks];
                _job.JobTasks[i].index = -1;
                for (int j = 0; j < _job.JobTasks[i].numberSubTasks; j++)
                {
                    ItemIndex = _index == IndexType.RSI ? j : listViewTasks.Groups[i].Items[j].Index;
                    _job.JobTasks[i].SubTasks[j].ItemIndex = ItemIndex;
                    _job.JobTasks[i].SubTasks[j].data.i = Convert.ToDouble(gridVariables[ItemIndex, 0].Value);
                    _job.JobTasks[i].SubTasks[j].data.e = Convert.ToDouble(gridVariables[ItemIndex, 1].Value);
                    _job.JobTasks[i].SubTasks[j].data.d = Convert.ToDouble(gridVariables[ItemIndex, 2].Value);
                    _job.JobTasks[i].SubTasks[j].data.p = Convert.ToDouble(gridVariables[ItemIndex, 3].Value);
                    _job.JobTasks[i].SubTasks[j].data.h = Convert.ToDouble(gridVariables[ItemIndex, 4].Value);  // This should be the same for these subtasks
                    
                    _job.JobTasks[i].h += _job.JobTasks[i].SubTasks[j].data.h;  // Calculate mean
                    _job.JobTasks[i].order[j] = j;
                }

                _job.JobTasks[i].h /= _job.JobTasks[i].numberSubTasks;
            }

            // Save the composite option
            //_index = chkComposite.Checked;
            return;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // Return empty array
            //_subtasks = new ModelSubTask[0];
        }



        #region Private routines
        /// <summary>
        /// Adds a column to the DataGrid View and formates it
        /// </summary>
        /// <param name="col">Column number (zero based)</param>
        private void AddColumn(Int32 col)
        {
            //String[] strTasks = new String[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };

            // By default, the DataGrid always contains a single column
            //if (col == 0) return;
            if (gridVariables.Columns.Contains("Column" + (col).ToString())) return;

            string strName = "Task ";
            if (_index != IndexType.RSI) strName = "SubTask ";

            // Create the new column
            //gridVariables.Columns.Add("Column" + (col + 1).ToString(), "Task " + strTasks[col]);
            gridVariables.Columns.Add("Column" + (col).ToString(), strName + ((char)('A' + col)).ToString());
            gridVariables.Columns[col].SortMode = DataGridViewColumnSortMode.NotSortable;
            gridVariables.Columns[col].Width = 70;

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
        /// Creates some data to show as an example
        /// </summary>
        private void DataExample()
        {
            _job.numberTasks = 1;
            _job.JobTasks = new ModelTask[_job.numberTasks];
            _job.JobTasks[0].numberSubTasks = 8;
            _job.JobTasks[0].SubTasks = new ModelSubTask[_job.JobTasks[0].numberSubTasks];
            //_job.JobTasks[1].numberSubTasks = 2;
            //_job.JobTasks[1].SubTasks = new ModelSubTask[_job.JobTasks[1].numberSubTasks];

            _job.JobTasks[0].SubTasks[0].data.i = 0.2;
            _job.JobTasks[0].SubTasks[0].data.e = 5;
            _job.JobTasks[0].SubTasks[0].data.d = 3;
            _job.JobTasks[0].SubTasks[0].data.p = 5;
            _job.JobTasks[0].SubTasks[0].data.h = 4;

            _job.JobTasks[0].SubTasks[1].data.i = 0.2;
            _job.JobTasks[0].SubTasks[1].data.e = 5;
            _job.JobTasks[0].SubTasks[1].data.d = 3;
            _job.JobTasks[0].SubTasks[1].data.p = -5;
            _job.JobTasks[0].SubTasks[1].data.h = 4;

            _job.JobTasks[0].SubTasks[2].data.i = 0.4;
            _job.JobTasks[0].SubTasks[2].data.e = 6;
            _job.JobTasks[0].SubTasks[2].data.d = 3;
            _job.JobTasks[0].SubTasks[2].data.p = -10;
            _job.JobTasks[0].SubTasks[2].data.h = 3;

            _job.JobTasks[0].SubTasks[3].data.i = 0.4;
            _job.JobTasks[0].SubTasks[3].data.e = 4;
            _job.JobTasks[0].SubTasks[3].data.d = 2;
            _job.JobTasks[0].SubTasks[3].data.p = 10;
            _job.JobTasks[0].SubTasks[3].data.h = 3;

            _job.JobTasks[0].SubTasks[4].data.i = 0.4;
            _job.JobTasks[0].SubTasks[4].data.e = 4;
            _job.JobTasks[0].SubTasks[4].data.d = 2;
            _job.JobTasks[0].SubTasks[4].data.p = -10;
            _job.JobTasks[0].SubTasks[4].data.h = 3;

            _job.JobTasks[0].SubTasks[5].data.i = 0.4;
            _job.JobTasks[0].SubTasks[5].data.e = 4;
            _job.JobTasks[0].SubTasks[5].data.d = 2;
            _job.JobTasks[0].SubTasks[5].data.p = 0;
            _job.JobTasks[0].SubTasks[5].data.h = 1;

            _job.JobTasks[0].SubTasks[6].data.i = 0.15;
            _job.JobTasks[0].SubTasks[6].data.e = 2;
            _job.JobTasks[0].SubTasks[6].data.d = 10;
            _job.JobTasks[0].SubTasks[6].data.p = 5;
            _job.JobTasks[0].SubTasks[6].data.h = 8;

            _job.JobTasks[0].SubTasks[7].data.i = 0.15;
            _job.JobTasks[0].SubTasks[7].data.e = 2;
            _job.JobTasks[0].SubTasks[7].data.d = 10;
            _job.JobTasks[0].SubTasks[7].data.p = 5;
            _job.JobTasks[0].SubTasks[7].data.h = 8;

        }

        private void DataExample2()
        {
            _job.numberTasks = 2;
            _job.JobTasks = new ModelTask[_job.numberTasks];
            _job.JobTasks[0].numberSubTasks = 3;
            _job.JobTasks[0].SubTasks = new ModelSubTask[_job.JobTasks[0].numberSubTasks];
            _job.JobTasks[1].numberSubTasks = 2;
            _job.JobTasks[1].SubTasks = new ModelSubTask[_job.JobTasks[1].numberSubTasks];

            _job.JobTasks[0].SubTasks[0].data.i = 0.7;
            _job.JobTasks[0].SubTasks[0].data.e = 1;
            _job.JobTasks[0].SubTasks[0].data.d = 1;
            _job.JobTasks[0].SubTasks[0].data.p = 0;
            _job.JobTasks[0].SubTasks[0].data.h = 4;

            _job.JobTasks[0].SubTasks[1].data.i = 0.4;
            _job.JobTasks[0].SubTasks[1].data.e = 2.6;
            _job.JobTasks[0].SubTasks[1].data.d = 1.2;
            _job.JobTasks[0].SubTasks[1].data.p = 0;
            _job.JobTasks[0].SubTasks[1].data.h = 4;

            _job.JobTasks[0].SubTasks[2].data.i = 0.2;
            _job.JobTasks[0].SubTasks[2].data.e = 5;
            _job.JobTasks[0].SubTasks[2].data.d = 3;
            _job.JobTasks[0].SubTasks[2].data.p = 0;
            _job.JobTasks[0].SubTasks[2].data.h = 4;

            _job.JobTasks[1].SubTasks[0].data.i = 0.1;
            _job.JobTasks[1].SubTasks[0].data.e = 0.5;
            _job.JobTasks[1].SubTasks[0].data.d = 1;
            _job.JobTasks[1].SubTasks[0].data.p = -15;
            _job.JobTasks[1].SubTasks[0].data.h = 4;

            _job.JobTasks[1].SubTasks[1].data.i = 0.5;
            _job.JobTasks[1].SubTasks[1].data.e = 2;
            _job.JobTasks[1].SubTasks[1].data.d = 3;
            _job.JobTasks[1].SubTasks[1].data.p = -15;
            _job.JobTasks[1].SubTasks[1].data.h = 4;

        }
        
        /// <summary>
        /// Shows the data into the grid control
        /// </summary>
        private void DataToGrid()
        {
            switch ((int)_job.model)
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

            updTasks.Value = _job.numberTasks;
            Int32 nCol = 0;
            for (var j = 0; j < _job.numberTasks; j++)
            {
                //nCol++;
                for (var i = 0; i < _job.JobTasks[j].SubTasks.Length; i++)
                {
                    //Column 0 is already created in the constructor;
                    if (i > 0) AddColumn();

                    // Populate the DataGridView with data
                    gridVariables[nCol, 0].Value = _job.JobTasks[j].SubTasks[i].data.i.ToString();
                    gridVariables[nCol, 1].Value = _job.JobTasks[j].SubTasks[i].data.e.ToString();
                    gridVariables[nCol, 2].Value = _job.JobTasks[j].SubTasks[i].data.d.ToString();
                    gridVariables[nCol, 3].Value = _job.JobTasks[j].SubTasks[i].data.p.ToString();
                    gridVariables[nCol, 4].Value = _job.JobTasks[j].SubTasks[i].data.h.ToString();

                    // Classify
                    listViewTasks.Items.Add(new ListViewItem("SubTask " + ((char)('A' + nCol)).ToString(), listViewTasks.Groups[j]));
                    //listViewTasks.Items[nCol].Group = listViewTasks.Groups[j];

                    nCol++;
                }
            }
            // Update the control's value
            updSubtasks.Value = nCol;
            updTasks.Value = _job.numberTasks;
        }

        #endregion

        public void LoadExample()
        {
            // Load some data example
            DataExample2();
            DataToGrid();
            tabDataStrain_Selected(null, new TabControlEventArgs(tabTasks, 1, TabControlAction.Selecting));

            return;
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
            }
        }
    }
}
