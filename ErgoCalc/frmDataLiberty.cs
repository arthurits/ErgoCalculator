﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

using ErgoCalc.Models.LibertyMutual;

namespace ErgoCalc
{
    public partial class frmDataLiberty : Form, IChildData
    {
        private List<ModelLiberty> _data;

        #region IChildData interface
        public object GetData => _data;

        #endregion IChildData interface

        public frmDataLiberty()
        {
            InitializeComponent();
            // Create the first column (zero index base)
            AddColumn();

            // Create the header rows
            gridVariables.RowCount = 8;
            gridVariables.Rows[0].HeaderCell.Value = "Type";
            gridVariables.Rows[1].HeaderCell.Value = "Horizontal reach H (m)";
            gridVariables.Rows[2].HeaderCell.Value = "Vertical range middle VRM (m)";
            gridVariables.Rows[3].HeaderCell.Value = "Horizontal distance DH (m)";
            gridVariables.Rows[4].HeaderCell.Value = "Vertical distance DV (m)";
            gridVariables.Rows[5].HeaderCell.Value = "Vertical height V (m)";
            gridVariables.Rows[6].HeaderCell.Value = "Frequency F (actions/min)";
            gridVariables.Rows[7].HeaderCell.Value = "Gender";
            //gridVariables.Rows[1].Visible = false;

            // Create custom cells with combobox display
            DataGridViewComboBoxCell cellType = new DataGridViewComboBoxCell();
            DataTable tabType = new DataTable();

            tabType.Columns.Add("Type", typeof(String));
            tabType.Columns.Add("TypeValue", typeof(Int32));
            tabType.Rows.Add("Carrying", 0);
            tabType.Rows.Add("Lifting", 1);
            tabType.Rows.Add("Lowering", 2);
            tabType.Rows.Add("Pulling", 3);
            tabType.Rows.Add("Pushing", 4);
            cellType.DataSource = tabType;
            cellType.DisplayMember = "Type";
            cellType.ValueMember = "TypeValue";

            gridVariables.Rows[0].Cells[0] = cellType;

            DataGridViewComboBoxCell cellGender = new DataGridViewComboBoxCell();
            DataTable tabGender = new DataTable();

            tabGender.Columns.Add("Gender", typeof(String));
            tabGender.Columns.Add("GenderValue", typeof(Int32));
            tabGender.Rows.Add("Male", 0);
            tabGender.Rows.Add("Female", 1);
            cellGender.DataSource = tabGender;
            cellGender.DisplayMember = "Gender";
            cellGender.ValueMember = "GenderValue";

            gridVariables.Rows[7].Cells[0] = cellGender;

            gridVariables.CurrentCellDirtyStateChanged += gridVariables_CurrentCellDirtyStateChanged;

            // Initialize private variable
            _data = new List<ModelLiberty>();
        }

        public frmDataLiberty(List<ModelLiberty> data)
            :this()
        {
            DataToGrid(data);
        }

        #region Form events
        private void btnOK_Click(object sender, EventArgs e)
        {
            ModelLiberty item = new ModelLiberty();

            for (int i = 0; i < gridVariables.ColumnCount; i++)
            {
                item.data.type = (MNType)Convert.ToInt32(gridVariables[i, 0].Value);
                item.data.HorzReach = IsValidNumber(gridVariables[i, 1].Value) ? Convert.ToDouble(gridVariables[i, 1].Value) : 0;
                item.data.VertRangeM = IsValidNumber(gridVariables[i, 2].Value) ? Convert.ToDouble(gridVariables[i, 2].Value) : 0;
                item.data.DistHorz = IsValidNumber(gridVariables[i, 3].Value) ? Convert.ToDouble(gridVariables[i, 3].Value) : 0;
                item.data.DistVert = IsValidNumber(gridVariables[i, 4].Value) ? Convert.ToDouble(gridVariables[i, 4].Value) : 0;
                item.data.VertHeight = IsValidNumber(gridVariables[i, 5].Value) ? Convert.ToDouble(gridVariables[i, 5].Value) : 0;
                item.data.Freq = IsValidNumber(gridVariables[i, 6].Value) ? Convert.ToDouble(gridVariables[i, 6].Value) : 0;
                item.data.gender = (MNGender)Convert.ToInt32(gridVariables[i, 7].Value);
                _data.Add(item);
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
        }

        void gridVariables_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            // Important to avoid running a 2nd time
            // https://stackoverflow.com/questions/5652957/what-event-catches-a-change-of-value-in-a-combobox-in-a-datagridviewcell
            if (!gridVariables.IsCurrentCellDirty) return;

            var CurrentCell = gridVariables.CurrentCell;
            if (CurrentCell is DataGridViewComboBoxCell)
            {
                // This fires the cell value changed (CellValueChanged) handler below
                // By committing the current cell edition, this function will change
                // the current cell dirty state (ie IsCurrentCellDirty),
                // so it will indeed trigger again this event. Hence the first IF of the routine
                // https://stackoverflow.com/questions/9608343/datagridview-combobox-column-change-cell-value-after-selection-from-dropdown-is/22327701
                gridVariables.CommitEdit(DataGridViewDataErrorContexts.Commit); // this mofi

                gridVariables.EndEdit();

                DataGridViewColumn col = gridVariables.Columns[gridVariables.CurrentCell.ColumnIndex];

                switch (CurrentCell.Value)
                {
                    case 0:     // Carrying
                    case 3:     // Pulling
                    case 4:     // Pushing
                        gridVariables.Rows[1].Cells[gridVariables.CurrentCell.ColumnIndex].Value = "——";
                        gridVariables.Rows[2].Cells[gridVariables.CurrentCell.ColumnIndex].Value = "——";
                        gridVariables.Rows[4].Cells[gridVariables.CurrentCell.ColumnIndex].Value = "——";
                        break;
                    case 1:
                    case 2:
                        gridVariables.Rows[3].Cells[gridVariables.CurrentCell.ColumnIndex].Value = "——";
                        gridVariables.Rows[5].Cells[gridVariables.CurrentCell.ColumnIndex].Value = "——";
                        break;
                    default:
                        break;
                }
            }
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {

        }

        #endregion Form events

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

            string strName = "Case ";
            //if (_index != IndexType.RSI) strName = "SubTask ";

            // Create the new column
            gridVariables.Columns.Add("Column" + (col).ToString(), strName + ((char)('A' + col)).ToString());
            gridVariables.Columns[col].SortMode = DataGridViewColumnSortMode.NotSortable;
            gridVariables.Columns[col].Width = 90;

            //if (col > 0) gridVariables[col, 6].Value = 0;

            // Give format (ComboBox) to the added column cells
            if (col > 0)
            {
                gridVariables.Rows[0].Cells[col] = (DataGridViewComboBoxCell)gridVariables.Rows[0].Cells[col - 1].Clone();
                gridVariables.Rows[7].Cells[col] = (DataGridViewComboBoxCell)gridVariables.Rows[7].Cells[col - 1].Clone();
            }

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
        /// Shows the data into the grid control
        /// </summary>
        private void DataToGrid(List<ModelLiberty> data)
        {
            int nDataNumber = data.Count;

            updTasks.Value = nDataNumber;

            for (var j = 0; j < nDataNumber; j++)
            {
                //Column 0 is already created in the constructor;
                if (j > 0) AddColumn();

                // Populate the DataGridView with data
                gridVariables[j, 0].Value = (int)data[j].data.type;
                gridVariables[j, 1].Value = data[j].data.HorzReach.ToString();
                gridVariables[j, 2].Value = data[j].data.VertRangeM.ToString();
                gridVariables[j, 3].Value = data[j].data.DistHorz.ToString();
                gridVariables[j, 4].Value = data[j].data.DistVert.ToString();
                gridVariables[j, 5].Value = data[j].data.VertHeight.ToString();
                gridVariables[j, 6].Value = data[j].data.Freq.ToString();
                gridVariables[j, 7].Value = (int)data[j].data.gender;
            }
        }

        #endregion Private routines

        private bool IsValidNumber(object str)
        {
            // https://stackoverflow.com/questions/894263/identify-if-a-string-is-a-number
            // https://stackoverflow.com/questions/33939770/regex-for-decimal-number-validation-in-c-sharp
            //string input = "132456789";
            
            if (str == null) return false;

            Match m = Regex.Match(str.ToString(), @"^-?\+?[0-9]*\.?\,?[0-9]+$");
            return m.Success && m.Value != "";
        }

    }
}
