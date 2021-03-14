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

        object IChildData.GetData => _data;

        public frmDataTC()
        {
            InitializeComponent();

            // Create the first column (zero index base)
            AddColumn();

            // Create the header rows
            gridVariables.RowCount = 5;
            gridVariables.Rows[0].HeaderCell.Value = "Air temperature (C)";
            gridVariables.Rows[1].HeaderCell.Value = "Radiant temperature (C)";
            gridVariables.Rows[2].HeaderCell.Value = "Air velocity (m/s)";
            gridVariables.Rows[3].HeaderCell.Value = "Relative humidity (%)";
            gridVariables.Rows[4].HeaderCell.Value = "Clothing insulation (clo)";
            gridVariables.Rows[5].HeaderCell.Value = "Metabolic rate (mets)";
            gridVariables.Rows[6].HeaderCell.Value = "External work (mets)";
            gridVariables[5, 0].Value = 0;
        }

        #region IChildData interface
        public object GetData()
        {
            throw new NotImplementedException();
        }
        #endregion IChildData interface

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

            gridVariables[5, col].Value = 0;

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

        #endregion Private routines

        private void btnOK_Click(object sender, EventArgs e)
        {
            ModelTC item = new ModelTC();

            for (int i=0; i<gridVariables.ColumnCount;i++)
            {
                item.data.TempAir = Convert.ToDouble(gridVariables[i, 0].Value);
                item.data.TempRad = Convert.ToDouble(gridVariables[i, 1].Value);
                item.data.Velocity = Convert.ToDouble(gridVariables[i, 2].Value);
                item.data.RelHumidity = Convert.ToDouble(gridVariables[i, 3].Value);
                item.data.Clothing = Convert.ToDouble(gridVariables[i, 4].Value);
                item.data.MetRate = Convert.ToDouble(gridVariables[i, 5].Value);
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

        
    }
}
