using System;
using System.Data;
using System.Windows.Forms;

using ErgoCalc.Models.CLMmodel;

namespace ErgoCalc;

    public partial class frmDataCLMmodel : Form
    {
        public modelCLM [] _sData;
        public frmDataCLMmodel()
        {
            // VS initialization routine
            InitializeComponent();

            #region DataGrid View customization
            // Create the first column (zero index base)
            AddColumn(0);

            // Create the header rows
            gridVariables.RowCount = 12;
            gridVariables.Rows[0].HeaderCell.Value = "Gender";
            gridVariables.Rows[1].HeaderCell.Value = "Weight lifted (kg)";
            gridVariables.Rows[2].HeaderCell.Value = "Horizontal distance (cm)";
            gridVariables.Rows[3].HeaderCell.Value = "Vertical distance (cm)";
            gridVariables.Rows[4].HeaderCell.Value = "Vertical travel distance (cm)";
            gridVariables.Rows[5].HeaderCell.Value = "Lifting frequency (times/min)";
            gridVariables.Rows[6].HeaderCell.Value = "Task duration (hours)";
            gridVariables.Rows[7].HeaderCell.Value = "Twisting angle (º)";
            gridVariables.Rows[8].HeaderCell.Value = "Coupling";
            gridVariables.Rows[9].HeaderCell.Value = "WBGT temperature (ºC)";
            gridVariables.Rows[10].HeaderCell.Value = "Age (years)";
            gridVariables.Rows[11].HeaderCell.Value = "Body weight (kg)";

            // Create custom cells with combobox display
            DataGridViewComboBoxCell celdaG = new DataGridViewComboBoxCell();
            DataGridViewComboBoxCell celdaC = new DataGridViewComboBoxCell();
            DataTable tableG = new DataTable();
            DataTable tableC = new DataTable();

            tableG.Columns.Add("Display", typeof(String));
            tableG.Columns.Add("Value", typeof(Int32));
            tableG.Rows.Add("Male", 1);
            tableG.Rows.Add("Female", 2);
            celdaG.DataSource = tableG;
            celdaG.DisplayMember = "Display";
            celdaG.ValueMember = "Value";

            tableC.Columns.Add("Display", typeof(String));
            tableC.Columns.Add("Value", typeof(Int32));
            tableC.Rows.Add("Good", 1);
            tableC.Rows.Add("Poor", 2);
            tableC.Rows.Add("No handle", 3);
            celdaC.DataSource = tableC;
            celdaC.DisplayMember = "Display";
            celdaC.ValueMember = "Value";

            gridVariables.Rows[0].Cells[0] = celdaG;
            gridVariables.Rows[8].Cells[0] = celdaC;
            #endregion
        }

        public frmDataCLMmodel(modelCLM [] data)
            : this() // Call the base constructor
        {

            for (Int32 i = 0; i < data.Length; i++)
            {
                // Add one column whenever necessary
                if (i > 0) AddColumn(i);

                // Populate the DataGridView with data
                gridVariables[i, 0].Value = data[i].data.gender;
                gridVariables[i, 1].Value = data[i].data.weight.ToString();
                gridVariables[i, 2].Value = data[i].data.h.ToString();
                gridVariables[i, 3].Value = data[i].data.v.ToString();
                gridVariables[i, 4].Value = data[i].data.d.ToString();
                gridVariables[i, 5].Value = data[i].data.f.ToString();
                gridVariables[i, 6].Value = data[i].data.td.ToString();
                gridVariables[i, 7].Value = data[i].data.t.ToString();
                gridVariables[i, 8].Value = data[i].data.c;
                gridVariables[i, 9].Value = data[i].data.hs.ToString();
                gridVariables[i, 10].Value = data[i].data.ag.ToString();
                gridVariables[i, 11].Value = data[i].data.bw.ToString();
            }

            // Update the control's value
            updTasks.Value = data.Length;
        }

        private void updTasks_ValueChanged(object sender, EventArgs e)
        {
            Int32 col = Convert.ToInt32(updTasks.Value);

            // Add or remove columns
            if (col > gridVariables.ColumnCount)
                AddColumn(col - 1);
            else if (col < gridVariables.ColumnCount)
                gridVariables.Columns.RemoveAt(col);

            // Modify the chkComposite state
            if (col > 1)
            {
                //chkComposite.Enabled = true;
            }
            else
            {
                //chkComposite.Checked = false;
                //chkComposite.Enabled = false;
            }

            return;            
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            // Save the values entered
            _sData = new modelCLM[Convert.ToInt32(updTasks.Value)];
            for (Int32 i = 0; i < _sData.Length; i++)
            {
                _sData[i].data.gender = Convert.ToInt32(gridVariables[i, 0].Value);
                _sData[i].data.weight = Convert.ToDouble(gridVariables[i, 1].Value);
                _sData[i].data.h = Convert.ToDouble(gridVariables[i, 2].Value);
                _sData[i].data.v = Convert.ToDouble(gridVariables[i, 3].Value);
                _sData[i].data.d = Convert.ToDouble(gridVariables[i, 4].Value);
                _sData[i].data.f = Convert.ToDouble(gridVariables[i, 5].Value);
                _sData[i].data.td = Convert.ToDouble(gridVariables[i, 6].Value);
                _sData[i].data.t = Convert.ToDouble(gridVariables[i, 7].Value);
                _sData[i].data.c = Convert.ToInt32(gridVariables[i, 8].Value);
                _sData[i].data.hs = Convert.ToDouble(gridVariables[i, 9].Value);
                _sData[i].data.ag = Convert.ToDouble(gridVariables[i, 10].Value);
                _sData[i].data.bw = Convert.ToDouble(gridVariables[i, 11].Value);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // Return empty array
            _sData = new modelCLM[0];
        }

        #region Private routines
        /// <summary>
        /// Adds a column to the DataGrid View and formates it
        /// </summary>
        /// <param name="col">Column number (zero based)</param>
        private void AddColumn(Int32 col)
        {
            String[] strTasks = new String[] { "A", "B", "C", "D", "E" };

            // Create the new column
            gridVariables.Columns.Add("Column" + (col + 1).ToString(), "Task " + strTasks[col]);
            gridVariables.Columns[col].SortMode = DataGridViewColumnSortMode.NotSortable;
            gridVariables.Columns[col].Width = 70;

            // Give format to the cells
            if (col > 0)
            {
                gridVariables.Rows[0].Cells[col] = (DataGridViewComboBoxCell)gridVariables.Rows[0].Cells[col - 1].Clone();
                gridVariables.Rows[8].Cells[col] = (DataGridViewComboBoxCell)gridVariables.Rows[8].Cells[col - 1].Clone();
            }

            return;
        }

        /// <summary>
        /// Creates some data to show as an example
        /// </summary>
        /// <returns>Array of Model CLM data</returns>
        private modelCLM[] DataExample()
        {
            modelCLM[] data = new modelCLM[2];

            data[0].data.gender = 1;
            data[0].data.weight = 5;
            data[0].data.h = 50;
            data[0].data.v = 70;
            data[0].data.d = 55;
            data[0].data.f = 2;
            data[0].data.td = 1;
            data[0].data.t = 45;
            data[0].data.c = 2;
            data[0].data.hs = 27;
            data[0].data.ag = 50;
            data[0].data.bw = 70;

            data[1].data.gender = 2;
            data[1].data.weight = 5;
            data[1].data.h = 50;
            data[1].data.v = 70;
            data[1].data.d = 55;
            data[1].data.f = 2;
            data[1].data.td = 1;
            data[1].data.t = 45;
            data[1].data.c = 2;
            data[1].data.hs = 27;
            data[1].data.ag = 50;
            data[1].data.bw = 70;

            return data;
        }

        /// <summary>
        /// Shows the data into the grid control
        /// </summary>
        /// <param name="data">Array of Model CLM data</param>
        private void DataToGrid(modelCLM[] data)
        {
            for (Int32 i = 0; i < data.Length; i++)
            {
                // Add one column whenever necessary
                if (i > 0) AddColumn(i);

                // Populate the DataGridView with data
                gridVariables[i, 0].Value = data[i].data.gender;
                gridVariables[i, 1].Value = data[i].data.weight.ToString();
                gridVariables[i, 2].Value = data[i].data.h.ToString();
                gridVariables[i, 3].Value = data[i].data.v.ToString();
                gridVariables[i, 4].Value = data[i].data.d.ToString();
                gridVariables[i, 5].Value = data[i].data.f.ToString();
                gridVariables[i, 6].Value = data[i].data.td.ToString();
                gridVariables[i, 7].Value = data[i].data.t.ToString();
                gridVariables[i, 8].Value = data[i].data.c;
                gridVariables[i, 9].Value = data[i].data.hs.ToString();
                gridVariables[i, 10].Value = data[i].data.ag.ToString();
                gridVariables[i, 11].Value = data[i].data.bw.ToString();
            }

            // Update the control's value
            updTasks.Value = data.Length;
        }

        #endregion

        /// <summary>
        /// Loads an example into the interface
        /// </summary>
        public void LoadExample()
        {
            // Loads some data example into the grid
            DataToGrid(DataExample());
        }

        /// <summary>
        /// Returns the data introduced by the user. Data is up-to-date after user has clicked OK button
        /// </summary>
        /// <returns>Array of Model NIOSH data</returns>
        public modelCLM[] getData()
        {
            return _sData;
        }
    }
