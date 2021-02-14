using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ErgoCalc.DLL.NIOSHModel;

namespace ErgoCalc
{
    public partial class frmDataNIOSHmodel : Form
    {
        private modelNIOSH[] _data;
        private bool _composite;

        // Default constructor
        public frmDataNIOSHmodel()
        {
            // VS Designer initialization routine
            InitializeComponent();
            txtConstanteLC.Text = "25";

            // Create the first column (zero index base)
                AddColumn(0);

            // Create the header rows
                gridVariables.RowCount = 8;
                gridVariables.Rows[0].HeaderCell.Value = "Weight lifted (kg)";
                gridVariables.Rows[1].HeaderCell.Value = "Horizontal distance (cm)";
                gridVariables.Rows[2].HeaderCell.Value = "Vertical distance (cm)";
                gridVariables.Rows[3].HeaderCell.Value = "Vertical travel distance (cm)";
                gridVariables.Rows[4].HeaderCell.Value = "Lifting frequency (times/min)";
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

                gridVariables.Rows[7].Cells[0] = celdaC;

        }

        // Overloaded constructor
        public frmDataNIOSHmodel(modelNIOSH[] data)
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
            // Save the values entered
            _data = new modelNIOSH[Convert.ToInt32(updTasks.Value)];
            for (Int32 i = 0; i < _data.Length; i++)
            {
                _data[i].data.weight = Convert.ToDouble(gridVariables[i, 0].Value);
                _data[i].data.h = Convert.ToDouble(gridVariables[i, 1].Value);
                _data[i].data.v = Convert.ToDouble(gridVariables[i, 2].Value);
                _data[i].data.d = Convert.ToDouble(gridVariables[i, 3].Value);
                _data[i].data.f = Convert.ToDouble(gridVariables[i, 4].Value);
                _data[i].data.td = Convert.ToDouble(gridVariables[i, 5].Value);
                _data[i].data.a = Convert.ToDouble(gridVariables[i, 6].Value);
                _data[i].data.c = Convert.ToInt32(gridVariables[i, 7].Value);

                if (!String.IsNullOrEmpty(txtConstanteLC.Text))
                    _data[i].factors.LC = Convert.ToDouble(txtConstanteLC.Text);
            }

            // Save the composite option
            _composite = chkComposite.Checked;

            return;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // Return empty array
            _data = new modelNIOSH[0];
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
            gridVariables.Columns.Add("Column" + (col+1).ToString() , "Task " + strTasks[col]);
            gridVariables.Columns[col].SortMode = DataGridViewColumnSortMode.NotSortable;
            gridVariables.Columns[col].Width = 70;

            // Give format to the cells
            if (col > 0)
                gridVariables.Rows[7].Cells[col] = (DataGridViewComboBoxCell)gridVariables.Rows[7].Cells[col - 1].Clone();

            return;
        }

        /// <summary>
        /// Creates some data to show as an example
        /// </summary>
        private void DataExample()
        {
            _data = new modelNIOSH[8];

            _data[0].data.weight = 3.0;
            _data[0].data.h = 24;
            _data[0].data.v = 38.5;
            _data[0].data.d = 76.5;
            _data[0].data.a = 0;
            _data[0].data.f = 3;
            _data[0].data.td = 2;
            _data[0].data.c = 3;

            _data[1].data.weight = 3.0;
            _data[1].data.h = 24;
            _data[1].data.v = 81;
            _data[1].data.d = 34;
            _data[1].data.a = 0;
            _data[1].data.f = 3;
            _data[1].data.td = 2;
            _data[1].data.c = 3;

            _data[2].data.weight = 3.0;
            _data[2].data.h = 24;
            _data[2].data.v = 123.5;
            _data[2].data.d = 8.5;
            _data[2].data.a = 90;
            _data[2].data.f = 3;
            _data[2].data.td = 2;
            _data[2].data.c = 3;

            _data[3].data.weight = 3.0;
            _data[3].data.h = 24;
            _data[3].data.v = 166;
            _data[3].data.d = 51;
            _data[3].data.a = 90;
            _data[3].data.f = 3;
            _data[3].data.td = 2;
            _data[3].data.c = 3;

            _data[4].data.weight = 7.0;
            _data[4].data.h = 24;
            _data[4].data.v = 33;
            _data[4].data.d = 82;
            _data[4].data.a = 0;
            _data[4].data.f = 1;
            _data[4].data.td = 1;
            _data[4].data.c = 3;

            _data[5].data.weight = 7.0;
            _data[5].data.h = 24;
            _data[5].data.v = 75.5;
            _data[5].data.d = 39.5;
            _data[5].data.a = 0;
            _data[5].data.f = 1;
            _data[5].data.td = 1;
            _data[5].data.c = 3;

            _data[6].data.weight = 7.0;
            _data[6].data.h = 24;
            _data[6].data.v = 118;
            _data[6].data.d = 3;
            _data[6].data.a = 0;
            _data[6].data.f = 1;
            _data[6].data.td = 1;
            _data[6].data.c = 3;

            _data[7].data.weight = 7.0;
            _data[7].data.h = 24;
            _data[7].data.v = 160.5;
            _data[7].data.d = 45.5;
            _data[7].data.a = 0;
            _data[7].data.f = 2;
            _data[7].data.td = 1;
            _data[7].data.c = 3;

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
        private void DataToGrid(modelNIOSH[] data)
        {
            for (Int32 i = 0; i < data.Length; i++)
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
                gridVariables[i, 7].Value = data[i].data.c;
            }

            // Update the control's value
            updTasks.Value = data.Length;
        }

        /// <summary>
        /// Shows the data into the grid control
        /// </summary>
        private void DataToGrid()
        {
            for (Int32 i = 0; i < _data.Length; i++)
            {
                // Add one column whenever necessary
                if (i > 0) AddColumn(i);

                // Populate the DataGridView with data
                gridVariables[i, 0].Value = _data[i].data.weight.ToString();
                gridVariables[i, 1].Value = _data[i].data.h.ToString();
                gridVariables[i, 2].Value = _data[i].data.v.ToString();
                gridVariables[i, 3].Value = _data[i].data.d.ToString();
                gridVariables[i, 4].Value = _data[i].data.f.ToString();
                gridVariables[i, 5].Value = _data[i].data.td.ToString();
                gridVariables[i, 6].Value = _data[i].data.a.ToString();
                gridVariables[i, 7].Value = _data[i].data.c;
            }

            // Update the control's value
            updTasks.Value = _data.Length;
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

        /// <summary>
        /// Returns the data introduced by the user. Data is updated after user has clicked OK button
        /// </summary>
        /// <returns>Array of Model NIOSH data</returns>
        public modelNIOSH[] getData()
        {
            return _data;
        }

        /// <summary>
        /// Returns whether the user has selected the composite index or not
        /// </summary>
        /// <returns>True if the composite index has been selected</returns>
        public bool getComposite()
        {
            return _composite;
        }
    }
}
