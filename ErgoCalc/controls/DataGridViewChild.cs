
using System.Configuration;
using System.Data;
using System.Windows.Forms;

namespace ErgoCalc.controls;

public class CalendarColumn : DataGridViewColumn
{
    public CalendarColumn() : base(new DataGridViewChildCell())
    {
    }

    public override DataGridViewCell CellTemplate
    {
        get
        {
            return base.CellTemplate;
        }
        set
        {
            // Ensure that the cell used for the template is a CalendarCell.
            if (value != null &&
                !value.GetType().IsAssignableFrom(typeof(DataGridViewChildCell)))
            {
                throw new InvalidCastException("Must be a DataGridViewChildCell");
            }
            base.CellTemplate = value;
        }
    }
}

public class DataGridViewChildCell : DataGridViewTextBoxCell
{
    public DataGridViewEditingControl editControl;

    public DataGridViewChildCell()
        : base()
    {
        editControl = new();
    }

    public DataGridViewChildCell(List<string[]> str)
        : base()
    {
        editControl = new(str);
    }

    public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
    {
        // Set the value of the editing control to the current cell value.
        base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
        DataGridViewEditingControl? ctl = DataGridView?.EditingControl as DataGridViewEditingControl;
        // Use the default row value when Value property is null.
        if (this.Value is null)
        {
            ctl.Value = (string)this.DefaultNewRowValue;
        }
        else
        {
            ctl.Value = (string)this.Value;
        }
    }

    public override void DetachEditingControl()
    {
        DataGridView? dataGridView = this.DataGridView;

        if (dataGridView == null || dataGridView.EditingControl == null)
            throw new InvalidOperationException("Cell is detached or its grid has no editing control.");

        DataGridViewEditingControl? ctl = DataGridView?.EditingControl as DataGridViewEditingControl;
        if (ctl is not null)
        {
            this.Value = ctl.ToString();
            ctl.EditingControlFormattedValue = String.Empty;
        }

        base.DetachEditingControl();
    }

    public override void PositionEditingControl(bool setLocation, bool setSize, Rectangle cellBounds, Rectangle cellClip, DataGridViewCellStyle cellStyle, bool singleVerticalBorderAdded, bool singleHorizontalBorderAdded, bool isFirstDisplayedColumn, bool isFirstDisplayedRow)
    {
        Rectangle ctlSize = new Rectangle(new Point(cellBounds.Location.X + 10, cellBounds.Location.Y + 10), new Size(cellBounds.Width + 300, 150));
        base.PositionEditingControl(setLocation, setSize, ctlSize, ctlSize, cellStyle, singleVerticalBorderAdded, singleHorizontalBorderAdded, isFirstDisplayedColumn, isFirstDisplayedRow);
        if (editControl != null)
        {
            Point pos = new Point(cellBounds.Location.X + 10, cellBounds.Location.Y + 10);
            pos.Offset(DataGridView.Location);
            editControl.Location = pos;
        }
    }
    public override Type EditType
    {
        get
        {
            // Return the type of the editing control that CalendarCell uses.
            return typeof(DataGridViewEditingControl);
        }
    }

    public override Type ValueType
    {
        get
        {
            // Return the type of the value that CalendarCell contains.

            return typeof(string);
        }
    }

    public override object DefaultNewRowValue
    {
        get
        {
            // Use the current date and time as the default value.
            return string.Empty;
        }
    }
}

public class DataGridViewEditingControl : DataGridView, IDataGridViewEditingControl
{
    DataGridView dataGridView;
    
    DataGridViewComboBoxColumn comboBorg;
    DataGridViewComboBoxCell comboModerate;
    DataGridViewComboBoxCell comboIntense;
    DataGridViewComboBoxCell comboMax;

    private bool valueChanged = false;
    int rowIndex;

    public DataGridViewEditingControl()
    {
        this.RowsAdded += new DataGridViewRowsAddedEventHandler(OnRowsAdded);
        this.Columns.Add("Actions", "Technical actions");
    }

    public DataGridViewEditingControl(List<string[]> str)
        : base()
    {
        DataTable tableBorg = new();
        tableBorg.Columns.Add("Type", typeof(String));
        tableBorg.Columns.Add("TypeValue", typeof(Int32));
        for (int i = 0; i < str[0].Length; i++)
            tableBorg.Rows.Add(str[0][i], i);

        DataTable tableModerate = new();
        tableModerate.Columns.Add("Type", typeof(String));
        tableModerate.Columns.Add("TypeValue", typeof(Int32));
        for (int i = 0; i < str[1].Length; i++)
            tableModerate.Rows.Add(str[1][i], i);

        DataTable tableIntense = new();
        tableIntense.Columns.Add("Type", typeof(String));
        tableIntense.Columns.Add("TypeValue", typeof(Int32));
        for (int i = 0; i < str[2].Length; i++)
            tableIntense.Rows.Add(str[2][i], i);
        
        DataTable tableMax = new();
        tableMax.Columns.Add("Type", typeof(String));
        tableMax.Columns.Add("TypeValue", typeof(Int32));
        for (int i = 0; i < str[3].Length; i++)
            tableMax.Rows.Add(str[3][i], i);

        comboBorg = new DataGridViewComboBoxColumn();
        comboBorg.HeaderText = "Borg CR-10";
        comboBorg.DataSource = tableBorg;
        comboBorg.DisplayMember = "Type";
        comboBorg.ValueMember = "TypeValue";
        comboBorg.Name = "BorgScale";
        this.Columns.Add(comboBorg);

        this.Columns.Add("Force", "Force");

        comboModerate = new DataGridViewComboBoxCell();
        //comboModerate.HeaderText = "Force";
        comboModerate.DataSource = tableModerate;
        comboModerate.DisplayMember = "Type";
        comboModerate.ValueMember = "TypeValue";
        //comboModerate.Name = "ForceModerate";

        comboIntense = new DataGridViewComboBoxCell();
        //comboIntense.HeaderText = "Force";
        comboIntense.DataSource = tableIntense;
        comboIntense.DisplayMember = "Type";
        comboIntense.ValueMember = "TypeValue";
        //comboIntense.Name = "ForceIntense";

        comboMax = new DataGridViewComboBoxCell();
        //comboMax.HeaderText = "Force";
        comboMax.DataSource = tableMax;
        comboMax.DisplayMember = "Type";
        comboMax.ValueMember = "TypeValue";
        //comboMax.Name = "ForceMax";
    }

    // Implements the IDataGridViewEditingControl.EditingControlFormattedValue
    // property.
    public object EditingControlFormattedValue
    {
        get
        {
            return ToString();
        }
        set
        {
            if (value is String)
            {
                try
                {
                    // This will throw an exception of the string is
                    // null, empty, or not in the format of a date.
                    this.Value = ((String)value);
                }
                catch
                {
                    // In the case of an exception, just use the
                    // default value so we're not left with a null
                    // value.
                    this.Value = string.Empty;
                }
            }
        }
    }

    public string Value
    {
        get
        {
            return ToString();
        }
        set
        {
            if (value is String)
                ToGrid(value);

        }
    }

    public override string ToString()
    {
        string str = string.Empty;

        for (int i = 0; i < this.Rows.Count; i++)
        {
            for (int j = 0; j < this.Rows.Count; j++)
                str += this[j, i].Value;
        }

        return str;
    }

    private void OnRowsAdded(object? sender, System.Windows.Forms.DataGridViewRowsAddedEventArgs e)
    {
        this[0, e.RowIndex].Value = $"# {e.RowIndex + 1}";
    }

    /// <summary>
    /// Transfers formatted string to DataGridView
    /// </summary>
    /// <param name="value">Formatted string with values</param>
    public void ToGrid(string value)
    {

    }

    private void Variables_CurrentCellDirtyStateChanged(object? sender, EventArgs? e)
    {
        var CurrentCell = this.CurrentCell;
        if (CurrentCell is not DataGridViewComboBoxCell) return;
        if (((DataGridViewComboBoxCell)CurrentCell).DisplayMember != "Type") return;

        // Important to avoid running a 2nd time
        // https://stackoverflow.com/questions/5652957/what-event-catches-a-change-of-value-in-a-combobox-in-a-datagridviewcell
        if (!this.IsCurrentCellDirty) return;


        // This fires the cell value changed (CellValueChanged) handler below
        // By committing the current cell edition, this function will change
        // the current cell dirty state (ie IsCurrentCellDirty),
        // so it will indeed trigger again this event. Hence the 3rd IF of this routine
        // https://stackoverflow.com/questions/9608343/datagridview-combobox-column-change-cell-value-after-selection-from-dropdown-is/22327701
        this.CommitEdit(DataGridViewDataErrorContexts.Commit);

        this.EndEdit();

        DataGridViewColumn col = this.Columns[this.CurrentCell.ColumnIndex];

        switch (CurrentCell.Value)
        {
            case 0:     // Carrying
            case 1:
            case 2:
                this.Rows[CurrentCell.RowIndex].Cells[CurrentCell.ColumnIndex + 1].Value = "——";
                break;
            case 3:     // Pulling
            case 4:     // Pushing
                this.Rows[CurrentCell.RowIndex].Cells[CurrentCell.ColumnIndex + 1] = comboModerate;
                break;
            case 5:
            case 6:
            case 7:
                this.Rows[CurrentCell.RowIndex].Cells[CurrentCell.ColumnIndex + 1] = comboIntense;
                break;
            case 8:
            case 9:
            case 10:
                this.Rows[CurrentCell.RowIndex].Cells[CurrentCell.ColumnIndex + 1] = comboMax;
                break;
            default:
                break;
        }

    }

    // Implements the
    // IDataGridViewEditingControl.GetEditingControlFormattedValue method.
    public object GetEditingControlFormattedValue(
        DataGridViewDataErrorContexts context)
    {
        return EditingControlFormattedValue;
    }

    // Implements the
    // IDataGridViewEditingControl.ApplyCellStyleToEditingControl method.
    public void ApplyCellStyleToEditingControl(
        DataGridViewCellStyle dataGridViewCellStyle)
    {
        this.Font = dataGridViewCellStyle.Font;
        this.ForeColor = dataGridViewCellStyle.ForeColor;
        this.BackColor = dataGridViewCellStyle.BackColor;
    }

    // Implements the IDataGridViewEditingControl.EditingControlRowIndex
    // property.
    public int EditingControlRowIndex
    {
        get
        {
            return rowIndex;
        }
        set
        {
            rowIndex = value;
        }
    }

    // Implements the IDataGridViewEditingControl.EditingControlWantsInputKey
    // method.
    public bool EditingControlWantsInputKey(
        Keys key, bool dataGridViewWantsInputKey)
    {
        // Let the DateTimePicker handle the keys listed.
        switch (key & Keys.KeyCode)
        {
            case Keys.Left:
            case Keys.Up:
            case Keys.Down:
            case Keys.Right:
            case Keys.Home:
            case Keys.End:
            case Keys.PageDown:
            case Keys.PageUp:
                return true;
            default:
                return !dataGridViewWantsInputKey;
        }
    }

    // Implements the IDataGridViewEditingControl.PrepareEditingControlForEdit
    // method.
    public void PrepareEditingControlForEdit(bool selectAll)
    {
        // No preparation needs to be done.
    }

    // Implements the IDataGridViewEditingControl
    // .RepositionEditingControlOnValueChange property.
    public bool RepositionEditingControlOnValueChange
    {
        get
        {
            return false;
        }
    }

    // Implements the IDataGridViewEditingControl
    // .EditingControlDataGridView property.
    public DataGridView EditingControlDataGridView
    {
        get
        {
            return dataGridView;
        }
        set
        {
            dataGridView = value;
        }
    }

    // Implements the IDataGridViewEditingControl
    // .EditingControlValueChanged property.
    public bool EditingControlValueChanged
    {
        get
        {
            return valueChanged;
        }
        set
        {
            valueChanged = value;
        }
    }

    // Implements the IDataGridViewEditingControl
    // .EditingPanelCursor property.
    public Cursor EditingPanelCursor
    {
        get
        {
            return base.Cursor;
        }
    }

    protected override void OnCellValueChanged(DataGridViewCellEventArgs e)
    {
        // Notify the DataGridView that the contents of the cell
        // have changed.
        valueChanged = true;
        this.EditingControlDataGridView?.NotifyCurrentCellDirty(true);
        base.OnCellValueChanged(e);
    }
}
