using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;

using ErgoCalc.Models.CLM;

namespace ErgoCalc;

public partial class FrmDataCLM : Form, IChildData
{
    private readonly CultureInfo _culture = CultureInfo.CurrentCulture;
    private Job _job;
    public object GetData => _job;

    public FrmDataCLM()
    {
        InitializeComponent();       
    }

    public FrmDataCLM(CultureInfo culture)
        : this()
    {
        _culture = culture;
        UpdateUI_Language(culture);
    }

    public FrmDataCLM(Job job)
        : this() // Call the base constructor
    {
        _job = job;
        DataToGrid();
    }

    private void Tasks_ValueChanged(object sender, EventArgs e)
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
            //chkComposite.Enabled = true;
        }
        else
        {
            //chkComposite.Checked = false;
            //chkComposite.Enabled = false;
        }

        return;
    }

    private void Accept_Click(object sender, EventArgs e)
    {
        // The form does not return unless all fields are validated. This avoids closing the dialog
        this.DialogResult = DialogResult.None;

        _job = new();

        // Save the values entered
        _job.NumberTasks = Convert.ToInt32(updTasks.Value);
        _job.Tasks = new TaskModel[_job.NumberTasks];
        for (Int32 i = 0; i < _job.NumberTasks; i++)
        {
            _job.Tasks[i] = new TaskModel();
            _job.Tasks[i].Data.Gender = (Gender)Convert.ToInt32(gridVariables[i, 0].Value);
            _job.Tasks[i].Data.Weight = Convert.ToDouble(gridVariables[i, 1].Value);
            _job.Tasks[i].Data.h = Convert.ToDouble(gridVariables[i, 2].Value);
            _job.Tasks[i].Data.v = Convert.ToDouble(gridVariables[i, 3].Value);
            _job.Tasks[i].Data.d = Convert.ToDouble(gridVariables[i, 4].Value);
            _job.Tasks[i].Data.f = Convert.ToDouble(gridVariables[i, 5].Value);
            _job.Tasks[i].Data.td = Convert.ToDouble(gridVariables[i, 6].Value);
            _job.Tasks[i].Data.t = Convert.ToDouble(gridVariables[i, 7].Value);
            _job.Tasks[i].Data.c = (Coupling)Convert.ToInt32(gridVariables[i, 8].Value);
            _job.Tasks[i].Data.hs = Convert.ToDouble(gridVariables[i, 9].Value);
            _job.Tasks[i].Data.ag = Convert.ToDouble(gridVariables[i, 10].Value);
            _job.Tasks[i].Data.bw = Convert.ToDouble(gridVariables[i, 11].Value);
        }

        // Return OK thus closing the dialog
        this.DialogResult = DialogResult.OK;
    }

    private void Cancel_Click(object sender, EventArgs e)
    {
        // Return empty array
        _job = new();
    }

    private void Example_Click(object sender, EventArgs e)
    {
        // Loads some data example into the grid
        DataExample();
        DataToGrid();
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
        gridVariables.Columns.Add("Column" + (col).ToString(), "Task " + ((char)('A' + col)).ToString());
        gridVariables.Columns[col].SortMode = DataGridViewColumnSortMode.NotSortable;
        gridVariables.Columns[col].Width = 85;

        // Give format to the cells
        if (col > 0)
        {
            gridVariables.Rows[0].Cells[col] = (DataGridViewComboBoxCell)gridVariables.Rows[0].Cells[col - 1].Clone();
            gridVariables.Rows[8].Cells[col] = (DataGridViewComboBoxCell)gridVariables.Rows[8].Cells[col - 1].Clone();
        }
        else if (col == 0)
            AddRows();

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
    /// Adds the headercell values for each row
    /// </summary>
    private void AddRows()
    {
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

        tableG.Columns.Add("Display", typeof(string));
        tableG.Columns.Add("Value", typeof(int));
        tableG.Rows.Add(Gender.Male.ToString(), (int)Gender.Male);
        tableG.Rows.Add(Gender.Female.ToString(), (int)Gender.Female);
        celdaG.DataSource = tableG;
        celdaG.DisplayMember = "Display";
        celdaG.ValueMember = "Value";

        tableC.Columns.Add("Display", typeof(string));
        tableC.Columns.Add("Value", typeof(int));
        tableC.Rows.Add(Coupling.Good, (int)Coupling.Good);
        tableC.Rows.Add(Coupling.Poor, (int)Coupling.Poor);
        tableC.Rows.Add(Coupling.NoHandle, (int)Coupling.NoHandle);
        celdaC.DataSource = tableC;
        celdaC.DisplayMember = "Display";
        celdaC.ValueMember = "Value";

        gridVariables.Rows[0].Cells[0] = celdaG;
        gridVariables.Rows[8].Cells[0] = celdaC;
    }

    /// <summary>
    /// Loads an example into the interface
    /// </summary>
    public void LoadExample()
    {
    }

    /// <summary>
    /// Creates some data to show as an example
    /// </summary>
    private void DataExample()
    {
        _job = new();
        _job.NumberTasks = 2;
        _job.Tasks = new TaskModel[_job.NumberTasks];

        _job.Tasks[0] = new TaskModel();
        _job.Tasks[0].Data.Gender = Gender.Male;
        _job.Tasks[0].Data.Weight = 5;
        _job.Tasks[0].Data.h = 50;
        _job.Tasks[0].Data.v = 70;
        _job.Tasks[0].Data.d = 55;
        _job.Tasks[0].Data.f = 2;
        _job.Tasks[0].Data.td = 1;
        _job.Tasks[0].Data.t = 45;
        _job.Tasks[0].Data.c = Coupling.Poor;
        _job.Tasks[0].Data.hs = 27;
        _job.Tasks[0].Data.ag = 50;
        _job.Tasks[0].Data.bw = 70;

        _job.Tasks[1] = new();
        _job.Tasks[1].Data.Gender = Gender.Female;
        _job.Tasks[1].Data.Weight = 5;
        _job.Tasks[1].Data.h = 50;
        _job.Tasks[1].Data.v = 70;
        _job.Tasks[1].Data.d = 55;
        _job.Tasks[1].Data.f = 2;
        _job.Tasks[1].Data.td = 1;
        _job.Tasks[1].Data.t = 45;
        _job.Tasks[1].Data.c = Coupling.Poor;
        _job.Tasks[1].Data.hs = 27;
        _job.Tasks[1].Data.ag = 50;
        _job.Tasks[1].Data.bw = 70;
    }

    /// <summary>
    /// Shows the data into the grid control
    /// </summary>
    private void DataToGrid()
    {
        // Update the control's value. This creates the necessary griVariables columns
        updTasks.Value = _job.Tasks.Length;

        for (Int32 i = 0; i < _job.Tasks.Length; i++)
        {
            // Populate the DataGridView with data
            gridVariables[i, 0].Value = (int)_job.Tasks[i].Data.Gender;
            gridVariables[i, 1].Value = _job.Tasks[i].Data.Weight.ToString();
            gridVariables[i, 2].Value = _job.Tasks[i].Data.h.ToString();
            gridVariables[i, 3].Value = _job.Tasks[i].Data.v.ToString();
            gridVariables[i, 4].Value = _job.Tasks[i].Data.d.ToString();
            gridVariables[i, 5].Value = _job.Tasks[i].Data.f.ToString();
            gridVariables[i, 6].Value = _job.Tasks[i].Data.td.ToString();
            gridVariables[i, 7].Value = _job.Tasks[i].Data.t.ToString();
            gridVariables[i, 8].Value = (int)_job.Tasks[i].Data.c;
            gridVariables[i, 9].Value = _job.Tasks[i].Data.hs.ToString();
            gridVariables[i, 10].Value = _job.Tasks[i].Data.ag.ToString();
            gridVariables[i, 11].Value = _job.Tasks[i].Data.bw.ToString();
        }
    }

    #endregion

    /// <summary>
    /// Update the form's interface language
    /// </summary>
    /// <param name="culture">Culture used to display the UI</param>
    private void UpdateUI_Language(System.Globalization.CultureInfo culture)
    {
        StringResources.Culture = culture;

        this.btnAccept.Text = StringResources.BtnAccept;
        this.btnCancel.Text = StringResources.BtnCancel;
        this.btnExample.Text = StringResources.BtnExample;

        // Relocate controls
        RelocateControls();
    }

    /// <summary>
    /// Relocate controls to compensate for the culture text length in labels
    /// </summary>
    private void RelocateControls()
    {
    }
}