using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ErgoCalc.DLL.Strain;
using ErgoCalc;

namespace ErgoCalc
{

    public partial class frmDataStrainIndex : Form
    {
        private modelRSI[] _subtasks;
        private int[][] _tasks;
        private Index _index;



        // Default constructor
        public frmDataStrainIndex()
        {
            // VS Designer initialization routine
            InitializeComponent();

            // Create the first column (zero index base)
            AddColumn(0);

            // Create the header rows
            gridVariables.RowCount = 5;
            gridVariables.Rows[0].HeaderCell.Value = "Intensity of exertion";
            gridVariables.Rows[1].HeaderCell.Value = "Efforts per minute";
            gridVariables.Rows[2].HeaderCell.Value = "Duration per exertion";
            gridVariables.Rows[3].HeaderCell.Value = "Hand/wrist posture";
            gridVariables.Rows[4].HeaderCell.Value = "Duration of task per day";

            /*
                gridVariables.Rows[5].HeaderCell.Value = "Task duration (hours)";
                gridVariables.Rows[6].HeaderCell.Value = "Twisting angle (º)";
                gridVariables.Rows[7].HeaderCell.Value = "Coupling";

            // Create custom cells with combobox display
                DataGridViewComboBoxCell celdaC = new DataGridViewComboBoxCell();
                DataTable tableC = new DataTable();

                tableC.Columns.Add("Display", typeof(String));
                tableC.Columns.Add("Value", typeof(Int32));
                tableC.Rows.Add("Good", 1);
                tableC.Rows.Add("Poor", 2);
                tableC.Rows.Add("No handle", 3);
                celdaC.DataSource = tableC;
                celdaC.DisplayMember = "Display";
                celdaC.ValueMember = "Value";

                gridVariables.Rows[7].Cells[0] = celdaC;*/
        }

        // Overloaded constructor
        public frmDataStrainIndex(modelRSI[] data)
            : this() // Call the base constructor
        {

            for (Int32 i = 0; i < data.Length; i++)
            {
                // Add one column whenever necessary
                if (i > 0) AddColumn(i);

                // Populate the DataGridView with data
                gridVariables[i, 0].Value = data[i].data.i.ToString();
                gridVariables[i, 1].Value = data[i].data.e.ToString();
                gridVariables[i, 2].Value = data[i].data.d.ToString();
                gridVariables[i, 3].Value = data[i].data.p.ToString();
                gridVariables[i, 4].Value = data[i].data.h.ToString();
                //gridVariables[i, 5].Value = data[i].data.td.ToString();
                //gridVariables[i, 6].Value = data[i].data.a.ToString();
                //gridVariables[i, 7].Value = data[i].data.c;
            }

            // Update the control's value
            updSubtasks.Value = data.Length;
        }

        /// <summary>
        /// Returns the data introduced by the user. Data is updated after user has clicked OK button
        /// </summary>
        /// <returns>Array of Model NIOSH data</returns>
        public modelRSI[] getSubTasks()
        {
            return _subtasks;
        }

        public void getTasks()
        {

        }


        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            // Check of the raiser of the event is a checked Checkbox.
            // Of course we also need to to cast it first.
            if (((System.Windows.Forms.RadioButton)sender).Checked)
            {
                // This is the correct control.
                System.Windows.Forms.RadioButton rb = (System.Windows.Forms.RadioButton)sender;
            }

            if (radRSI.Checked) _index = Index.RSI;
            if (radCOSI.Checked) _index = Index.COSI;
            if (radCUSI.Checked) _index = Index.CUSI;

            if (_index == Index.RSI)
            {
                foreach (DataGridViewColumn col in gridVariables.Columns)
                {
                    col.HeaderText = "Task " + col.HeaderText.Substring(col.HeaderText.Length - 1, 1);
                    lblSubtasks.Text = "Number of tasks";
                }
            }
            else
            {
                foreach (DataGridViewColumn col in gridVariables.Columns)
                {
                    col.HeaderText = "SubTask " + col.HeaderText.Substring(col.HeaderText.Length - 1, 1);
                    lblSubtasks.Text = "Number of subtasks";
                }
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
                this.groupIndex.Enabled = true;
                //chkComposite.Enabled = true;
            }
            else
            {
                this.groupIndex.Enabled = false;
                //chkComposite.Checked = false;
                //chkComposite.Enabled = false;
            }

            // Set the maximum tasks
            //this.updTasks.Value = col;

            return;
        }

        private void updTasks_ValueChanged(object sender, EventArgs e)
        {
            Int32 tasks = Convert.ToInt32(updTasks.Value);
            if (tasks > listViewC.Groups.Count)
            {
                for (int i = listViewC.Groups.Count; i < tasks; i++)
                    listViewC.AddGroup(i);
            }
            else if (tasks < listViewC.Groups.Count)
            {
                for (int i = tasks; i < listViewC.Groups.Count; i++)
                    listViewC.RemoveGroup();
            }
            return;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            // Save the values entered
            _subtasks = new modelRSI[Convert.ToInt32(updSubtasks.Value)];
            for (Int32 i = 0; i < _subtasks.Length; i++)
            {
                _subtasks[i].data.i = Convert.ToDouble(gridVariables[i, 0].Value);
                _subtasks[i].data.e = Convert.ToDouble(gridVariables[i, 1].Value);
                _subtasks[i].data.d = Convert.ToDouble(gridVariables[i, 2].Value);
                _subtasks[i].data.p = Convert.ToDouble(gridVariables[i, 3].Value);
                _subtasks[i].data.h = Convert.ToDouble(gridVariables[i, 4].Value);
                //_subtasks[i].data.td = Convert.ToDouble(gridVariables[i, 5].Value);
                //_subtasks[i].data.a = Convert.ToDouble(gridVariables[i, 6].Value);
                //_subtasks[i].data.c = Convert.ToInt32(gridVariables[i, 7].Value);

                //if (!String.IsNullOrEmpty(txtConstanteLC.Text))
                //    _subtasks[i].factors.LC = Convert.ToDouble(txtConstanteLC.Text);
            }

            // Save the composite option
            //_index = chkComposite.Checked;

            return;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // Return empty array
            _subtasks = new modelRSI[0];
        }

        #region Private routines
        /// <summary>
        /// Adds a column to the DataGrid View and formates it
        /// </summary>
        /// <param name="col">Column number (zero based)</param>
        private void AddColumn(Int32 col)
        {
            String[] strTasks = new String[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };

            // Create the new column
            gridVariables.Columns.Add("Column" + (col + 1).ToString(), "Task " + strTasks[col]);
            gridVariables.Columns[col].SortMode = DataGridViewColumnSortMode.NotSortable;
            gridVariables.Columns[col].Width = 70;

            // Give format to the cells
            // if (col > 0) gridVariables.Rows[7].Cells[col] = (DataGridViewComboBoxCell)gridVariables.Rows[7].Cells[col - 1].Clone();

            return;
        }

        /// <summary>
        /// Creates some data to show as an example
        /// </summary>
        private void DataExample()
        {
            _subtasks = new modelRSI[8];

            _subtasks[0].data.i = 0.2;
            _subtasks[0].data.e = 5;
            _subtasks[0].data.d = 3;
            _subtasks[0].data.p = 5;
            _subtasks[0].data.h = 4;

            _subtasks[1].data.i = 0.2;
            _subtasks[1].data.e = 5;
            _subtasks[1].data.d = 3;
            _subtasks[1].data.p = -5;
            _subtasks[1].data.h = 4;

            _subtasks[2].data.i = 0.4;
            _subtasks[2].data.e = 6;
            _subtasks[2].data.d = 3;
            _subtasks[2].data.p = -10;
            _subtasks[2].data.h = 3;

            _subtasks[3].data.i = 0.4;
            _subtasks[3].data.e = 4;
            _subtasks[3].data.d = 2;
            _subtasks[3].data.p = 10;
            _subtasks[3].data.h = 3;

            _subtasks[4].data.i = 0.4;
            _subtasks[4].data.e = 4;
            _subtasks[4].data.d = 2;
            _subtasks[4].data.p = -10;
            _subtasks[4].data.h = 3;

            _subtasks[5].data.i = 0.4;
            _subtasks[5].data.e = 4;
            _subtasks[5].data.d = 2;
            _subtasks[5].data.p = 0;
            _subtasks[5].data.h = 1;

            _subtasks[6].data.i = 0.15;
            _subtasks[6].data.e = 2;
            _subtasks[6].data.d = 10;
            _subtasks[6].data.p = 5;
            _subtasks[6].data.h = 8;

            _subtasks[7].data.i = 0.15;
            _subtasks[7].data.e = 2;
            _subtasks[7].data.d = 10;
            _subtasks[7].data.p = 5;
            _subtasks[7].data.h = 8;

            /*
            _subtasks = new modelNIOSH[3];

            _subtasks[0].data.weight = 20.0;
            _subtasks[0].data.h = 25;
            _subtasks[0].data.v = 75;
            _subtasks[0].data.d = 5;
            _subtasks[0].data.a = 0;
            _subtasks[0].data.f = 1;
            _subtasks[0].data.td = 2;
            _subtasks[0].data.c = 2;

            _subtasks[1].data.weight = 25.0;
            _subtasks[1].data.h = 30;
            _subtasks[1].data.v = 75;
            _subtasks[1].data.d = 5;
            _subtasks[1].data.a = 0;
            _subtasks[1].data.f = 2;
            _subtasks[1].data.td = 2;
            _subtasks[1].data.c = 3;

            _subtasks[2].data.weight = 15.0;
            _subtasks[2].data.h = 30;
            _subtasks[2].data.v = 75;
            _subtasks[2].data.d = 5;
            _subtasks[2].data.a = 45;
            _subtasks[2].data.f = 2;
            _subtasks[2].data.td = 2;
            _subtasks[2].data.c = 2;
            */
        }

        /// <summary>
        /// Shows the data into the grid control
        /// </summary>
        /// <param name="data">Array of Model NIOSH data</param>
        private void DataToGrid(modelRSI[] data)
        {
            for (Int32 i = 0; i < data.Length; i++)
            {
                // Add one column whenever necessary
                if (i > 0) AddColumn(i);

                // Populate the DataGridView with data
                gridVariables[i, 0].Value = data[i].data.i.ToString();
                gridVariables[i, 1].Value = data[i].data.e.ToString();
                gridVariables[i, 2].Value = data[i].data.d.ToString();
                gridVariables[i, 3].Value = data[i].data.p.ToString();
                gridVariables[i, 4].Value = data[i].data.h.ToString();
            }

            // Update the control's value
            updSubtasks.Value = data.Length;
        }

        /// <summary>
        /// Shows the data into the grid control
        /// </summary>
        private void DataToGrid()
        {
            for (Int32 i = 0; i < _subtasks.Length; i++)
            {
                // Add one column whenever necessary
                if (i > 0) AddColumn(i);

                // Populate the DataGridView with data
                gridVariables[i, 0].Value = _subtasks[i].data.i.ToString();
                gridVariables[i, 1].Value = _subtasks[i].data.e.ToString();
                gridVariables[i, 2].Value = _subtasks[i].data.d.ToString();
                gridVariables[i, 3].Value = _subtasks[i].data.p.ToString();
                gridVariables[i, 4].Value = _subtasks[i].data.h.ToString();
            }

            // Update the control's value
            updSubtasks.Value = _subtasks.Length;
        }

        #endregion

        public void LoadExample()
        {
            // Load some data example
            DataExample();
            DataToGrid();

            return;
        }

        private void tabDataStrain_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPageIndex == 1) // tabTasks
            {
                listView1.Groups.Add(new ListViewGroup("Test group", HorizontalAlignment.Center));
                listView1.Items.Add(new ListViewItem("SubTask " + ((char)('A')).ToString(), listView1.Groups[0]));
                

                // Clear all items and remove all groups but one
                listViewC.Items.Clear();
                for (int i = 1; i < listViewC.Groups.Count; i++)
                    listViewC.RemoveGroup();

                updTasks.Value = updSubtasks.Value; // This also creates as many groups as tasks

                // Create the subtasks, as many as subtasks
                for (int i = 0; i < updSubtasks.Value; i++)
                    listViewC.Items.Add(new ListViewItem("SubTask " + ((char)('A' + i)).ToString(), listViewC.Groups[1]));

                //listViewC.Items.Add(new ListViewItem("SubTask A", listViewC.Groups[0]));
                //listViewC.Items.Add(new ListViewItem("SubTask B", listViewC.Groups[0]));
                //listViewC.Items.Add(new ListViewItem("SubTask C", listViewC.Groups[0]));
                //listViewC.Items.Add(new ListViewItem("SubTask D", listViewC.Groups[0]));
                //listViewC.Items.Add(new ListViewItem("SubTask E", listViewC.Groups[0]));
                //listViewA = (ListViewEx)listViewA;
                //listViewC.DeleteEmptyItems();
            }
        }
    }
}
