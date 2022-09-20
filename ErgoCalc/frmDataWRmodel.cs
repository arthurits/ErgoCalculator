using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using ErgoCalc.Models.WR;

namespace ErgoCalc;

public partial class frmDataWRmodel : Form, IChildData
{
    // Propiedades de la clase
    public List<DataWR> _data;
    private string strGridHeader = "Task ";
    private System.ComponentModel.ComponentResourceManager _resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDataWRmodel));

    public object GetData => _data;

    public frmDataWRmodel()
    {
        InitializeComponent();

        AddColumn();

        // Create the header rows
        gridVariables.RowCount = 7;
        gridVariables.Rows[0].HeaderCell.Value = "Name";
        gridVariables.Rows[1].HeaderCell.Value = "Max. voluntary contraction (%)";
        gridVariables.Rows[2].HeaderCell.Value = "Maximum holding time (min)";
        gridVariables.Rows[3].HeaderCell.Value = "Working times (min)";
        gridVariables.Rows[4].HeaderCell.Value = "Rest times (min)";
        gridVariables.Rows[5].HeaderCell.Value = "Number of cycles";
        gridVariables.Rows[6].HeaderCell.Value = "Numeric step";

        // Set default cell value
        gridVariables[0, 6].Value = 0.1;

        var cell = new DataGridViewCellStyle();
        cell.BackColor = Color.White;
        cell.Alignment = DataGridViewContentAlignment.MiddleCenter;
        cell.SelectionBackColor = Color.White;
        cell.SelectionForeColor = Color.Gray;
        cell.ForeColor = Color.Gray;
        gridVariables.Rows[2].DefaultCellStyle = cell;
        gridVariables.Rows[2].ReadOnly = true;

        // Initialize private variables
        _data = new List<DataWR>();

        //var str = _resources.GetString("errorValidarMVCFieldLength");

        // Uncomment to load example 
        //LoadExample();

    }

    public frmDataWRmodel (List<DataWR> data)
        :this()
    {
        DataToGrid(data);

        updTasks.Value = data.Count;

    }

    #region Form control's events
    private void btnAceptar_Click(object sender, EventArgs e)
    {
        // The form does not return unless all fields are validated
        this.DialogResult = DialogResult.None;

        DataWR item = new DataWR();
        for (int i = 0; i < gridVariables.ColumnCount; i++)
        {
            item.Legend = gridVariables[i, 0].Value?.ToString();
            // Validation routines
            if (!Validation.IsValidRange(gridVariables[i, 1].Value, 0, 100, true, this)) { gridVariables.CurrentCell = gridVariables[i, 1]; gridVariables.BeginEdit(true); return; }
            item.MVC = Validation.ValidateNumber(gridVariables[i, 1].Value);
            item.MHT = ComputeMHT(item.MVC);
            //gridVariables[i, 2].Value = item._dMHT;

            if (gridVariables[i, 3].Value == null) { gridVariables.CurrentCell = gridVariables[i, 3]; gridVariables.BeginEdit(true); return; }
            //item._dWorkRest = new double[2][];
            var arr = gridVariables[i, 3].Value.ToString().Split(' ');
            foreach( var number in arr)
            {
                if (!Validation.IsValidRange(number, 0, item.MHT, true, this)) { gridVariables.CurrentCell = gridVariables[i, 3]; gridVariables.BeginEdit(true); return; }
            }
            item.WorkingTimes = arr.Select(double.Parse).ToArray();

            if (gridVariables[i, 4].Value == null) { gridVariables.CurrentCell = gridVariables[i, 4]; gridVariables.BeginEdit(true); return; }
            arr = gridVariables[i, 4].Value.ToString().Split(' ');
            foreach (var number in arr)
            {
                if (!Validation.IsValidRange(number, 0, item.MHT, true, this)) { gridVariables.CurrentCell = gridVariables[i, 4]; gridVariables.BeginEdit(true); return; }
            }
            item.RestingTimes = arr.Select(double.Parse).ToArray();

            //if (ValidarTT(item) != true) { return; }
            if (!Validation.IsValidRange(gridVariables[i, 5].Value, 0, 24, true, this)) { gridVariables.CurrentCell = gridVariables[i, 5]; gridVariables.BeginEdit(true); return; }
            if (!Validation.IsValidRange(gridVariables[i, 6].Value, 0.001, 0.1, true, this)) { gridVariables.CurrentCell = gridVariables[i, 6]; gridVariables.BeginEdit(true); return; }

            //item._dWorkRest[0];
            item.Cycles = (byte)Validation.ValidateNumber(gridVariables[i, 5].Value);
            item.PlotStep = Validation.ValidateNumber(gridVariables[i, 6].Value);

            item.Points = ComputeNumberOfPoints(item);
            //item._dPoints = new double[2][];
            item.PointsX = new double[item.Points];
            item.PointsY = new double[item.Points];

            // Save the column data once it's been approved
            _data.Add(item);

        }
        // Return OK and close the dialog
        this.DialogResult = DialogResult.OK;
    }

    private void gridVariables_CellEndEdit(object sender, DataGridViewCellEventArgs e)
    {
        // https://stackoverflow.com/questions/19537784/datagridview-event-to-catch-when-cell-value-has-been-changed-by-user/58062911#58062911

        int? rowIdx = e?.RowIndex;
        int? colIdx = e?.ColumnIndex;

        if (rowIdx != 1) return;

        if (rowIdx.HasValue && colIdx.HasValue)
        {
            var dgv = (DataGridView)sender;
            //var cell = dgv?.Rows?[rowIdx.Value]?.Cells?[colIdx.Value]?.Value;
            dgv.Rows[2].Cells[colIdx.Value].Value = ComputeMHT(Validation.ValidateNumber(dgv.Rows[1].Cells[colIdx.Value].Value)).ToString("0.##");

            //if (dgv.Columns[colIdx.Value].Name == "Maximum voluntary contraction (%)")
            //{
            //    if (!string.IsNullOrEmpty(cell?.ToString()))
            //    {
            //        // your code goes here
            //    };
            //};
        };
    }

    private void updTasks_ValueChanged(object sender, EventArgs e)
    {
        Int32 col = Convert.ToInt32(updTasks.Value);

        // Add or remove columns
        if (col > gridVariables.ColumnCount)
            for (int i = gridVariables.ColumnCount; i < col; i++) AddColumn(i);
        else if (col < gridVariables.ColumnCount)
            for (int i = gridVariables.ColumnCount - 1; i >= col; i--) gridVariables.Columns.RemoveAt(i);
    }
    
    #endregion Form control's events

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

        // Create the new column
        gridVariables.Columns.Add("Column" + (col).ToString(), strGridHeader + ((char)('A' + col)).ToString());
        gridVariables.Columns[col].SortMode = DataGridViewColumnSortMode.NotSortable;
        gridVariables.Columns[col].Width = 70;

        if (col > 0) gridVariables[col, 6].Value = 0.1;

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
    /// Places the data into the grid control, creating a new column for each case
    /// </summary>
    /// <param name="data">List containing the data</param>
    private void DataToGrid(List<DataWR> data)
    {
        int nDataNumber = data.Count;

        for (var j = 0; j < nDataNumber; j++)
        {
            //Column 0 is already created in the constructor;
            if (j > 0) AddColumn();

            // Populate the DataGridView with data
            gridVariables[j, 0].Value = data[j].Legend;
            gridVariables[j, 1].Value = (int)data[j].MVC;
            gridVariables[j, 2].Value = data[j].MHT.ToString("0.##");
            gridVariables[j, 3].Value = string.Join(" ", data[j].WorkingTimes);
            gridVariables[j, 4].Value = string.Join(" ", data[j].RestingTimes);
            gridVariables[j, 5].Value = data[j].Cycles;
            gridVariables[j, 6].Value = data[j].PlotStep;
        }
    }

    private int ComputeNumberOfPoints(DataWR data)
    {
        // Cálculo del número de puntos de la curva y comprobar que cada tiempo de descanso
        //  entre el paso es un entero
        double nSize = data.Cycles * data.WorkingTimes.Length + 1;
        foreach (double d in data.RestingTimes)
        {
            // La opción más corta sería utilizar el operador %, pero no funciona del todo bien
            if (Math.Abs(d / data.PlotStep - Math.Floor(d / data.PlotStep)) >= 0.001)
                throw new NotAnInteger(_resources.GetString("errorValidarPasoNotAnInteger"));

            nSize += ((d / data.PlotStep)) * data.Cycles;
        }

        return Convert.ToInt32(nSize);
    }

    private void LoadExample()
    {
        gridVariables[0, 0].Value = "Example";
        gridVariables[0, 1].Value = 15;
        //gridVariables[0, 2].Value = "Maximum holding time (min)";
        gridVariables[0, 3].Value = "4 3 2";
        gridVariables[0, 4].Value = "4 3 2";
        gridVariables[0, 5].Value = 2;
        gridVariables[0, 6].Value = 0.01;
    }

    private double ComputeMHT(double MVC)
    {
        return (5710.0 / Math.Pow(MVC, 2.14));
    }

    #endregion Private routines
            
}
