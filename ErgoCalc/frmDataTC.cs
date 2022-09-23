using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ErgoCalc.Models.ThermalComfort;

namespace ErgoCalc
{
    public partial class frmDataTC : Form, IChildData
    {
        private List<ModelTC> _data;

        #region IChildData interface
        public object GetData => _data;

        #endregion IChildData interface


        public frmDataTC()
        {
            InitializeComponent();

            // Create the first column (zero index base)
            AddColumn();

            // Create the header rows
            gridVariables.RowCount = 7;
            gridVariables.Rows[0].HeaderCell.Value = "Air temperature (C)";
            gridVariables.Rows[1].HeaderCell.Value = "Radiant temperature (C)";
            gridVariables.Rows[2].HeaderCell.Value = "Air velocity (m/s)";
            gridVariables.Rows[3].HeaderCell.Value = "Relative humidity (%)";
            gridVariables.Rows[4].HeaderCell.Value = "Clothing insulation (clo)";
            gridVariables.Rows[5].HeaderCell.Value = "Metabolic rate (mets)";
            gridVariables.Rows[6].HeaderCell.Value = "External work (mets)";
            gridVariables[0, 6].Value = 0;

            // Initialize private variable
            _data = new List<ModelTC>();
        }

        public frmDataTC(List<ModelTC> data)
            :this()
        {
            //DataToGrid(data);

            int nDataNumber = data.Count;
            
            // Update the control which in turn updates the grid's column number
            updTasks.Value = nDataNumber;
            
            // Shows the data into the grid control
            for (var j = 0; j < nDataNumber; j++)
            {
                //Column 0 is already created in the constructor;
                if (j > 0) AddColumn();

                // Populate the DataGridView with data
                gridVariables[j, 0].Value = data[j].data.TempAir.ToString();
                gridVariables[j, 1].Value = data[j].data.TempRad.ToString();
                gridVariables[j, 2].Value = data[j].data.Velocity.ToString();
                gridVariables[j, 3].Value = data[j].data.RelHumidity.ToString();
                gridVariables[j, 4].Value = data[j].data.Clothing.ToString();
                gridVariables[j, 5].Value = data[j].data.MetRate.ToString();
                gridVariables[j, 6].Value = data[j].data.ExternalWork.ToString();
            }
        }

        #region Form events
        private void btnOK_Click(object sender, EventArgs e)
        {
            ModelTC item = new ModelTC();

            for (int i = 0; i < gridVariables.ColumnCount; i++)
            {
                item.data.TempAir = Convert.ToDouble(gridVariables[i, 0].Value);
                item.data.TempRad = Convert.ToDouble(gridVariables[i, 1].Value);
                item.data.Velocity = Convert.ToDouble(gridVariables[i, 2].Value);
                item.data.RelHumidity = Convert.ToDouble(gridVariables[i, 3].Value);
                item.data.Clothing = Convert.ToDouble(gridVariables[i, 4].Value);
                item.data.MetRate = Convert.ToDouble(gridVariables[i, 5].Value);
                item.data.ExternalWork = Convert.ToDouble(gridVariables[i, 6].Value);
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
        private void DataToGrid(List<ModelTC> data)
        {
            int nDataNumber = data.Count;
            
            updTasks.Value = nDataNumber;

            for (var j = 0; j < nDataNumber; j++)
            {
                //Column 0 is already created in the constructor;
                if (j > 0) AddColumn();

                // Populate the DataGridView with data
                gridVariables[j, 0].Value = data[j].data.TempAir.ToString();
                gridVariables[j, 1].Value = data[j].data.TempRad.ToString();
                gridVariables[j, 2].Value = data[j].data.Velocity.ToString();
                gridVariables[j, 3].Value = data[j].data.RelHumidity.ToString();
                gridVariables[j, 4].Value = data[j].data.Clothing.ToString();
                gridVariables[j, 5].Value = data[j].data.MetRate.ToString();
                gridVariables[j, 6].Value = data[j].data.ExternalWork.ToString();
            }
        }

        #endregion Private routines

        public void LoadExample()
        {

        }

    }
}
