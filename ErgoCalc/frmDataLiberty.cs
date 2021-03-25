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
            gridVariables.RowCount = 6;
            gridVariables.Rows[0].HeaderCell.Value = "Horizontal reach (m)";
            gridVariables.Rows[1].HeaderCell.Value = "Vertical reach mean (m)";
            gridVariables.Rows[2].HeaderCell.Value = "Vertical height (m)";
            gridVariables.Rows[3].HeaderCell.Value = "Vertical distance (m)";
            gridVariables.Rows[4].HeaderCell.Value = "Horizontal distance (m)";
            gridVariables.Rows[5].HeaderCell.Value = "Frequency (/m)";
            //gridVariables.Rows[1].Visible = false;
            // Initialize private variable
            _data = new List<ModelLiberty>();
        }

        public frmDataLiberty(int data)
            :this()
        {

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
            gridVariables.Columns[col].Width = 70;

            if (col > 0) gridVariables[col, 6].Value = 0;

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
            }
        }

        #endregion Private routines

    }
}
