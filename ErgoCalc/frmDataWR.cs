﻿using System.Data;
using System.Globalization;

using ErgoCalc.Models.WR;

namespace ErgoCalc;

public partial class FrmDataWR : Form, IChildData
{
    // Propiedades de la clase
    private readonly CultureInfo _culture = CultureInfo.CurrentCulture;
    public Job _job;
    private System.ComponentModel.ComponentResourceManager _resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDataWR));

    public object GetData => _job;

    public FrmDataWR()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Overloaded constructor
    /// </summary>
    /// <param name="job"><see cref="Job"/> object containing data to be shown in the form</param>
    /// <param name="culture">Culture information to be used when showing the form's UI texts</param>
    public FrmDataWR(Job? job = null, CultureInfo? culture = null)
        : this()
    {
        // Update the UI language first
        _culture = culture ?? CultureInfo.CurrentCulture;
        UpdateUI_Language(_culture);

        // Then show the data
        if (job is not null)
        {
            _job = job;
            DataToGrid();
        }
    }

    #region Form control's events
    private void Accept_Click(object sender, EventArgs e)
    {
        // The form does not return unless all fields are validated
        this.DialogResult = DialogResult.None;

        _job = new();
        _job.NumberTasks = gridVariables.ColumnCount;
        _job.Tasks = new DataWR[_job.NumberTasks];

        for (int i = 0; i < _job.NumberTasks; i++)
        {
            _job.Tasks[i] = new();
            _job.Tasks[i].Legend = gridVariables[i, 0].Value?.ToString();
            // Validation routines
            if (!Validation.IsValidRange(gridVariables[i, 1].Value, 0, 100, true, this)) { gridVariables.CurrentCell = gridVariables[i, 1]; gridVariables.BeginEdit(true); return; }
            _job.Tasks[i].MVC = Validation.ValidateNumber(gridVariables[i, 1].Value);
            _job.Tasks[i].MHT = WorkRest.ComputeMHT(_job.Tasks[i].MVC);
            //gridVariables[i, 2].Value = item._dMHT;

            if (gridVariables[i, 3].Value == null) { gridVariables.CurrentCell = gridVariables[i, 3]; gridVariables.BeginEdit(true); return; }
            //item._dWorkRest = new double[2][];
            string[] arr = gridVariables[i, 3].Value.ToString().Split(' ');
            foreach( var number in arr)
            {
                if (!Validation.IsValidRange(number, 0, _job.Tasks[i].MHT, true, this)) { gridVariables.CurrentCell = gridVariables[i, 3]; gridVariables.BeginEdit(true); return; }
            }
            _job.Tasks[i].WorkingTimes = arr.Select(double.Parse).ToArray();

            if (gridVariables[i, 4].Value == null) { gridVariables.CurrentCell = gridVariables[i, 4]; gridVariables.BeginEdit(true); return; }
            arr = gridVariables[i, 4].Value.ToString().Split(' ');
            foreach (var number in arr)
            {
                if (!Validation.IsValidRange(number, 0, _job.Tasks[i].MHT, true, this)) { gridVariables.CurrentCell = gridVariables[i, 4]; gridVariables.BeginEdit(true); return; }
            }
            _job.Tasks[i].RestingTimes = arr.Select(double.Parse).ToArray();

            //if (ValidarTT(item) != true) { return; }
            if (!Validation.IsValidRange(gridVariables[i, 5].Value, 0, 24, true, this)) { gridVariables.CurrentCell = gridVariables[i, 5]; gridVariables.BeginEdit(true); return; }
            if (!Validation.IsValidRange(gridVariables[i, 6].Value, 0.001, 0.1, true, this)) { gridVariables.CurrentCell = gridVariables[i, 6]; gridVariables.BeginEdit(true); return; }

            //item._dWorkRest[0];
            _job.Tasks[i].Cycles = (byte)Validation.ValidateNumber(gridVariables[i, 5].Value);
            _job.Tasks[i].PlotStep = Validation.ValidateNumber(gridVariables[i, 6].Value);

            _job.Tasks[i].Points = ComputeNumberOfPoints(_job.Tasks[i]);
            //item._dPoints = new double[2][];
            _job.Tasks[i].PointsX = new double[_job.Tasks[i].Points];
            _job.Tasks[i].PointsY = new double[_job.Tasks[i].Points];

        }
        // Return OK and close the dialog
        this.DialogResult = DialogResult.OK;
    }

    private void Example_Click(object sender, EventArgs e)
    {
        // Loads some data example into the grid
        DataExample();
        DataToGrid();
    }

    private void Variables_CellEndEdit(object sender, DataGridViewCellEventArgs e)
    {
        // https://stackoverflow.com/questions/19537784/datagridview-event-to-catch-when-cell-value-has-been-changed-by-user/58062911#58062911

        int? rowIdx = e?.RowIndex;
        int? colIdx = e?.ColumnIndex;

        if (rowIdx != 1) return;

        if (rowIdx.HasValue && colIdx.HasValue)
        {
            var dgv = (DataGridView)sender;
            //var cell = dgv?.Rows?[rowIdx.Value]?.Cells?[colIdx.Value]?.Value;
            dgv.Rows[2].Cells[colIdx.Value].Value = WorkRest.ComputeMHT(Validation.ValidateNumber(dgv.Rows[1].Cells[colIdx.Value].Value)).ToString("0.##");

            //if (dgv.Columns[colIdx.Value].Name == "Maximum voluntary contraction (%)")
            //{
            //    if (!string.IsNullOrEmpty(cell?.ToString()))
            //    {
            //        // your code goes here
            //    };
            //};
        };
    }

    private void Tasks_ValueChanged(object sender, EventArgs e)
    {
        // Update the number of columns in the grid
        int col = Convert.ToInt32(updTasks.Value);
        (this as IChildData).UpdateGridColumns(gridVariables, col);
    }
    
    #endregion Form control's events

    #region Private routines
    /// <summary>
    /// Adds a column to the DataGrid View and formates it
    /// </summary>
    /// <param name="col">Column number (zero based)</param>
    void IChildData.AddColumn(Int32 col)
    {
        // By default, the DataGrid always contains a single column
        //if (col == 0) return;
        // Check if the column already exists
        if (gridVariables.Columns.Contains($"Column {(col).ToString()}")) return;

        // Create the new column
        (this as IChildData).AddColumnBasic(gridVariables, col, StringResources.Task, 70);

        // Add the row headers after the first column is created        
        if (col == 0)
        {
            AddRows();
            FormatRows();
        }
        
        // Default numeric values after the row headers have been created
        gridVariables[col, 6].Value = 0.1;

        return;
    }

    /// <summary>
    /// Adds a column to the DataGrid View and formates it
    /// </summary>
    private void AddColumn()
    {
        (this as IChildData).AddColumn(gridVariables.Columns.Count);
    }

    /// <summary>
    /// Adds the headercell values for each row
    /// </summary>
    private void AddRows()
    {
        string[] rowText = new string[]
        {
            "Name",
            "Max. voluntary contraction (%)",
            "Maximum holding time (min)",
            "Working times (min)",
            "Rest times (min)",
            "Number of cycles",
            "Numeric step"
        };
        (this as IChildData).AddGridRowHeaders(this.gridVariables, rowText);
    }

    /// <summary>
    /// Format the header row with custom cells
    /// </summary>
    private void FormatRows()
    {
        // Set the default cell style
        DataGridViewCellStyle cell = new()
        {
            BackColor = Color.White,
            Alignment = DataGridViewContentAlignment.MiddleCenter,
            SelectionBackColor = Color.White,
            SelectionForeColor = Color.Gray,
            ForeColor = Color.Gray
        };
        gridVariables.Rows[2].DefaultCellStyle = cell;
        gridVariables.Rows[2].ReadOnly = true;
    }

    /// <summary>
    /// Places the data into the grid control, creating a new column for each case
    /// </summary>
    /// <param name="data">List containing the data</param>
    private void DataToGrid()
    {
        // This creates the necessary grid columns in the corresponding ValueChanged event
        updTasks.Value = _job.NumberTasks;

        for (int i = 0; i < _job.NumberTasks; i++)
        {
            // Populate the DataGridView with data
            gridVariables[i, 0].Value = _job.Tasks[i].Legend;
            gridVariables[i, 1].Value = (int)_job.Tasks[i].MVC;
            gridVariables[i, 2].Value = _job.Tasks[i].MHT.ToString("0.##");
            gridVariables[i, 3].Value = string.Join(" ", _job.Tasks[i].WorkingTimes);
            gridVariables[i, 4].Value = string.Join(" ", _job.Tasks[i].RestingTimes);
            gridVariables[i, 5].Value = _job.Tasks[i].Cycles;
            gridVariables[i, 6].Value = _job.Tasks[i].PlotStep;
        }
    }

    private void DataExample()
    {
        _job = new();
        _job.NumberTasks = 4;
        _job.Tasks = new DataWR[_job.NumberTasks];

        _job.Tasks[0] = new();
        _job.Tasks[0].Legend = "Case 2-1";
        _job.Tasks[0].MVC = 20;
        _job.Tasks[0].MHT = WorkRest.ComputeMHT(_job.NumberTasks);
        _job.Tasks[0].WorkingTimes = new double[] { 8 };
        _job.Tasks[0].RestingTimes = new double[] { 8 };
        _job.Tasks[0].Cycles = 2;
        _job.Tasks[0].PlotStep = 0.01;

        _job.Tasks[1] = new();
        _job.Tasks[1].Legend = "Case 2-2";
        _job.Tasks[1].MVC = 20;
        _job.Tasks[1].MHT = WorkRest.ComputeMHT(_job.NumberTasks);
        _job.Tasks[1].WorkingTimes = new double[] { 4 };
        _job.Tasks[1].RestingTimes = new double[] { 4 };
        _job.Tasks[1].Cycles = 4;
        _job.Tasks[1].PlotStep = 0.01;

        _job.Tasks[2] = new();
        _job.Tasks[2].Legend = "Case 2-3";
        _job.Tasks[2].MVC = 20;
        _job.Tasks[2].MHT = WorkRest.ComputeMHT(_job.NumberTasks);
        _job.Tasks[2].WorkingTimes = new double[] { 2 };
        _job.Tasks[2].RestingTimes = new double[] { 2 };
        _job.Tasks[2].Cycles = 8;
        _job.Tasks[2].PlotStep = 0.01;

        _job.Tasks[3] = new();
        _job.Tasks[3].Legend = "Case 2-4";
        _job.Tasks[3].MVC = 20;
        _job.Tasks[3].MHT = WorkRest.ComputeMHT(_job.NumberTasks);
        _job.Tasks[3].WorkingTimes = new double[] { 1 };
        _job.Tasks[3].RestingTimes = new double[] { 1 };
        _job.Tasks[3].Cycles = 16;
        _job.Tasks[3].PlotStep = 0.01;
    }

    /// <summary>
    /// Computes the number of point needed to create the plot according to the WR model
    /// </summary>
    /// <param name="data">Data defining the WR model</param>
    /// <returns></returns>
    /// <exception cref="NotAnInteger">If PlotStep is not an integer</exception>
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

    #endregion Private routines            

    /// <summary>
    /// Update the form's interface language
    /// </summary>
    /// <param name="culture">Culture used to display the UI</param>
    private void UpdateUI_Language(System.Globalization.CultureInfo culture)
    {
        StringResources.Culture = culture;

        this.Text = StringResources.FormDataWR;
        this.btnAccept.Text = StringResources.BtnAccept;
        this.btnCancel.Text = StringResources.BtnCancel;
        this.btnExample.Text = StringResources.BtnExample;

        this.lblTasks.Text = StringResources.NumberOfTasks;

        // Relocate controls
        RelocateControls();
    }

    /// <summary>
    /// Relocate controls to compensate for the culture text length in labels
    /// </summary>
    private void RelocateControls()
    {
        this.updTasks.Left = this.lblTasks.Left + this.lblTasks.Width + 5;
    }
}