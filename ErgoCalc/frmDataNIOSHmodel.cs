using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ErgoCalc.Models.NIOSHModel;

namespace ErgoCalc
{
    public partial class frmDataNIOSHmodel : Form, IChildData
    {
        private List<SubTask> _data;
        private bool _composite;
        private string strGridHeader = "Task ";

        #region IChildData interface
        public object GetData => _data;
        #endregion IChildData interface

        public bool GetComposite => _composite;

        // Default constructor
        public frmDataNIOSHmodel()
        {
            // VS Designer initialization routine
            InitializeComponent();
            txtConstanteLC.Text = "25";

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
        public frmDataNIOSHmodel(List<SubTask> data)
            : this() // Call the base constructor
        {
            DataToGrid(data);
        }

        private void updTasks_ValueChanged(object sender, EventArgs e)
        {
            Int32 col = Convert.ToInt32(updTasks.Value);
            
            // Add or remove columns
            if (col > gridVariables.ColumnCount)
                for (int i = gridVariables.ColumnCount; i < col; i++) AddColumn(i);
            else if (col < gridVariables.ColumnCount)
                for (int i = gridVariables.ColumnCount - 1; i >= col; i--) gridVariables.Columns.RemoveAt(i);

            // Modify the chkComposite state
            if (col > 1)
            {
                chkComposite.Enabled = true;
            }
            else
            {
                chkComposite.Checked = false;
                chkComposite.Enabled = false;
            }
            
            return;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            int nSize = Convert.ToInt32(updTasks.Value);
            SubTask item = new SubTask();
            // Save the values entered
            _data = new List<SubTask>();
            for (Int32 i = 0; i < nSize; i++)
            {
                item.data.weight = Convert.ToDouble(gridVariables[i, 0].Value);
                item.data.h = Convert.ToDouble(gridVariables[i, 1].Value);
                item.data.v = Convert.ToDouble(gridVariables[i, 2].Value);
                item.data.d = Convert.ToDouble(gridVariables[i, 3].Value);
                item.data.f = Convert.ToDouble(gridVariables[i, 4].Value);
                item.data.td = Convert.ToDouble(gridVariables[i, 5].Value);
                item.data.a = Convert.ToDouble(gridVariables[i, 6].Value);
                item.data.c = (MNCoupling)gridVariables[i, 7].Value;

                if (!String.IsNullOrEmpty(txtConstanteLC.Text))
                    item.factors.LC = Convert.ToDouble(txtConstanteLC.Text);
                else
                    item.factors.LC = 0.0;
                
                _data.Add(item);
            }

            // Save the composite option
            _composite = chkComposite.Checked;

            return;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // Return empty array
            _data = new List<SubTask>();
        }

        #region Private routines
        /// <summary>
        /// Adds a column to the DataGrid View and formates it
        /// </summary>
        /// <param name="col">Column number (zero based)</param>
        private void AddColumn(Int32 col)
        {
            if (gridVariables.Columns.Contains("Column" + (col).ToString())) return;

            // Create the new column
            gridVariables.Columns.Add("Column" + (col).ToString() , strGridHeader + ((char)('A' + col)).ToString());
            gridVariables.Columns[col].SortMode = DataGridViewColumnSortMode.NotSortable;
            gridVariables.Columns[col].Width = 70;

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
            _data = new List<SubTask>();
            SubTask item = new SubTask();

            item.data.weight = 3.0;
            item.data.h = 24;
            item.data.v = 38.5;
            item.data.d = 76.5;
            item.data.a = 0;
            item.data.f = 3;
            item.data.td = 2;
            item.data.c = MNCoupling.NoHandle;
            _data.Add(item);

            item.data.weight = 3.0;
            item.data.h = 24;
            item.data.v = 81;
            item.data.d = 34;
            item.data.a = 0;
            item.data.f = 3;
            item.data.td = 2;
            item.data.c = MNCoupling.NoHandle;
            _data.Add(item);

            item.data.weight = 3.0;
            item.data.h = 24;
            item.data.v = 123.5;
            item.data.d = 8.5;
            item.data.a = 90;
            item.data.f = 3;
            item.data.td = 2;
            item.data.c = MNCoupling.NoHandle;
            _data.Add(item);

            item.data.weight = 3.0;
            item.data.h = 24;
            item.data.v = 166;
            item.data.d = 51;
            item.data.a = 90;
            item.data.f = 3;
            item.data.td = 2;
            item.data.c = MNCoupling.NoHandle;
            _data.Add(item);

            item.data.weight = 7.0;
            item.data.h = 24;
            item.data.v = 33;
            item.data.d = 82;
            item.data.a = 0;
            item.data.f = 1;
            item.data.td = 1;
            item.data.c = MNCoupling.NoHandle;
            _data.Add(item);

            item.data.weight = 7.0;
            item.data.h = 24;
            item.data.v = 75.5;
            item.data.d = 39.5;
            item.data.a = 0;
            item.data.f = 1;
            item.data.td = 1;
            item.data.c = MNCoupling.NoHandle;
            _data.Add(item);

            item.data.weight = 7.0;
            item.data.h = 24;
            item.data.v = 118;
            item.data.d = 3;
            item.data.a = 0;
            item.data.f = 1;
            item.data.td = 1;
            item.data.c = MNCoupling.NoHandle;
            _data.Add(item);

            item.data.weight = 7.0;
            item.data.h = 24;
            item.data.v = 160.5;
            item.data.d = 45.5;
            item.data.a = 0;
            item.data.f = 2;
            item.data.td = 1;
            item.data.c = MNCoupling.NoHandle;
            _data.Add(item);

            /*
            _data = new modelNIOSH[3];

            _data[0].data.weight = 20.0;
            _data[0].data.h = 25;
            _data[0].data.v = 75;
            _data[0].data.d = 5;
            _data[0].data.a = 0;
            _data[0].data.f = 1;
            _data[0].data.td = 2;
            _data[0].data.c = 2;

            _data[1].data.weight = 25.0;
            _data[1].data.h = 30;
            _data[1].data.v = 75;
            _data[1].data.d = 5;
            _data[1].data.a = 0;
            _data[1].data.f = 2;
            _data[1].data.td = 2;
            _data[1].data.c = 3;

            _data[2].data.weight = 15.0;
            _data[2].data.h = 30;
            _data[2].data.v = 75;
            _data[2].data.d = 5;
            _data[2].data.a = 45;
            _data[2].data.f = 2;
            _data[2].data.td = 2;
            _data[2].data.c = 2;
            */
        }

        /// <summary>
        /// Shows the data into the grid control
        /// </summary>
        /// <param name="data">Array of Model NIOSH data</param>
        private void DataToGrid(List<SubTask> data)
        {
            int nSize = data.Count;
            for (Int32 i = 0; i < nSize; i++)
            {
                // Add one column whenever necessary
                if (i > 0) AddColumn(i);

                // Populate the DataGridView with data
                gridVariables[i, 0].Value = data[i].data.weight.ToString();
                gridVariables[i, 1].Value = data[i].data.h.ToString();
                gridVariables[i, 2].Value = data[i].data.v.ToString();
                gridVariables[i, 3].Value = data[i].data.d.ToString();
                gridVariables[i, 4].Value = data[i].data.f.ToString();
                gridVariables[i, 5].Value = data[i].data.td.ToString();
                gridVariables[i, 6].Value = data[i].data.a.ToString();
                gridVariables[i, 7].Value = (int)data[i].data.c;
            }

            // Update the control's value
            updTasks.Value = nSize;
        }

        /// <summary>
        /// Shows the data into the grid control
        /// </summary>
        private void DataToGrid()
        {
            for (Int32 i = 0; i < _data.Count; i++)
            {
                // Add one column whenever necessary
                if (i > 0) AddColumn();

                // Populate the DataGridView with data
                gridVariables[i, 0].Value = _data[i].data.weight.ToString();
                gridVariables[i, 1].Value = _data[i].data.h.ToString();
                gridVariables[i, 2].Value = _data[i].data.v.ToString();
                gridVariables[i, 3].Value = _data[i].data.d.ToString();
                gridVariables[i, 4].Value = _data[i].data.f.ToString();
                gridVariables[i, 5].Value = _data[i].data.td.ToString();
                gridVariables[i, 6].Value = _data[i].data.a.ToString();
                gridVariables[i, 7].Value = (int)_data[i].data.c;
            }

            // Update the control's value
            updTasks.Value = _data.Count;
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
    }
}
