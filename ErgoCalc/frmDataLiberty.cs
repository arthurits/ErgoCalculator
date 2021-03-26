using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
            gridVariables.Rows[0].HeaderCell.Value = "Horizontal reach (m)";
            gridVariables.Rows[1].HeaderCell.Value = "Vertical reach mean (m)";
            gridVariables.Rows[2].HeaderCell.Value = "Vertical height (m)";
            gridVariables.Rows[3].HeaderCell.Value = "Vertical distance (m)";
            gridVariables.Rows[4].HeaderCell.Value = "Horizontal distance (m)";
            gridVariables.Rows[5].HeaderCell.Value = "Frequency (/m)";
            gridVariables.Rows[6].HeaderCell.Value = "Type";
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

            gridVariables.Rows[6].Cells[0] = cellType;

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
                item.data.HorzReach = Convert.ToDouble(gridVariables[i, 0].Value);
                item.data.VRM = Convert.ToDouble(gridVariables[i, 1].Value);
                item.data.VertHeight = Convert.ToDouble(gridVariables[i, 2].Value);
                item.data.DistVert = Convert.ToDouble(gridVariables[i, 3].Value);
                item.data.DistHorz = Convert.ToDouble(gridVariables[i, 4].Value);
                item.data.Freq = Convert.ToDouble(gridVariables[i, 5].Value);
                item.type = (MNType)Convert.ToByte(gridVariables[i, 6].Value);
                item.gender = (MNGender)Convert.ToByte(gridVariables[i, 6].Value);
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
                gridVariables.Rows[6].Cells[col] = (DataGridViewComboBoxCell)gridVariables.Rows[6].Cells[col - 1].Clone();
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
                gridVariables[j, 0].Value = data[j].data.HorzReach.ToString();
                gridVariables[j, 1].Value = data[j].data.VRM.ToString();
                gridVariables[j, 2].Value = data[j].data.VertHeight.ToString();
                gridVariables[j, 3].Value = data[j].data.DistVert.ToString();
                gridVariables[j, 4].Value = data[j].data.DistHorz.ToString();
                gridVariables[j, 5].Value = data[j].data.Freq.ToString();
                gridVariables[j, 6].Value = data[j].type;
                gridVariables[j, 7].Value = data[j].gender;
            }
        }

        #endregion Private routines

    }
}
