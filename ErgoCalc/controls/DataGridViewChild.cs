
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
    public DataGridViewEditingControl m_control = new();

    public DataGridViewChildCell()
        : base()
    {
        // Use the short date format.
        //this.Style.Format = "d";
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

        ListViewEditingControl? ctl = DataGridView?.EditingControl as ListViewEditingControl;
        if (ctl is not null)
        {
            this.Value = ctl.SelectedItems[0].SubItems[0].ToString();
            ctl.EditingControlFormattedValue = String.Empty;
        }

        base.DetachEditingControl();
    }

    public override void PositionEditingControl(bool setLocation, bool setSize, Rectangle cellBounds, Rectangle cellClip, DataGridViewCellStyle cellStyle, bool singleVerticalBorderAdded, bool singleHorizontalBorderAdded, bool isFirstDisplayedColumn, bool isFirstDisplayedRow)
    {
        Rectangle ctlSize = new Rectangle(new Point(cellBounds.Location.X + 10, cellBounds.Location.Y + 10), new Size(cellBounds.Width + 300, 150));
        base.PositionEditingControl(setLocation, setSize, ctlSize, ctlSize, cellStyle, singleVerticalBorderAdded, singleHorizontalBorderAdded, isFirstDisplayedColumn, isFirstDisplayedRow);
        if (m_control != null)
        {
            Point pos = new Point(cellBounds.Location.X + 10, cellBounds.Location.Y + 10);
            pos.Offset(DataGridView.Location);
            m_control.Location = pos;
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
    private bool valueChanged = false;
    int rowIndex;

    public DataGridViewEditingControl()
    {
        this.RowsAdded += new DataGridViewRowsAddedEventHandler(OnRowsAdded);

        this.Columns.Add("Actions", "Technical actions");

        //string[] row = new string[] { "1", "Product 1", "1000" };
        //this.Rows.Add(row);
        //row = new string[] { "2", "Product 2", "2000" };
        //this.Rows.Add(row);
        //row = new string[] { "3", "Product 3", "3000" };
        //this.Rows.Add(row);
        //row = new string[] { "4", "Product 4", "4000" };
        //this.Rows.Add(row);

        DataTable tabBorg = new();
        tabBorg.Columns.Add("Type", typeof(String));
        tabBorg.Columns.Add("TypeValue", typeof(Int32));
        tabBorg.Rows.Add(ErgoCalc.Models.OCRA.BorgCR10.Nothing0, (int)ErgoCalc.Models.OCRA.BorgCR10.Nothing0);
        tabBorg.Rows.Add(ErgoCalc.Models.OCRA.BorgCR10.Weak1, (int)ErgoCalc.Models.OCRA.BorgCR10.Weak1);
        tabBorg.Rows.Add(ErgoCalc.Models.OCRA.BorgCR10.Light2, (int)ErgoCalc.Models.OCRA.BorgCR10.Light2);
        tabBorg.Rows.Add(ErgoCalc.Models.OCRA.BorgCR10.Moderate3, (int)ErgoCalc.Models.OCRA.BorgCR10.Moderate3);
        tabBorg.Rows.Add(ErgoCalc.Models.OCRA.BorgCR10.Strong4, (int)ErgoCalc.Models.OCRA.BorgCR10.Strong4);
        tabBorg.Rows.Add(ErgoCalc.Models.OCRA.BorgCR10.StrongHeavy5, (int)ErgoCalc.Models.OCRA.BorgCR10.StrongHeavy5);
        tabBorg.Rows.Add(ErgoCalc.Models.OCRA.BorgCR10.VeryStrong6, (int)ErgoCalc.Models.OCRA.BorgCR10.VeryStrong6);
        tabBorg.Rows.Add(ErgoCalc.Models.OCRA.BorgCR10.VeryStrong7, (int)ErgoCalc.Models.OCRA.BorgCR10.VeryStrong7);
        tabBorg.Rows.Add(ErgoCalc.Models.OCRA.BorgCR10.VeryStrong8, (int)ErgoCalc.Models.OCRA.BorgCR10.VeryStrong8);
        tabBorg.Rows.Add(ErgoCalc.Models.OCRA.BorgCR10.ExtremelyStrong9, (int)ErgoCalc.Models.OCRA.BorgCR10.ExtremelyStrong9);
        tabBorg.Rows.Add(ErgoCalc.Models.OCRA.BorgCR10.ExtremelyStrongMaximal10, (int)ErgoCalc.Models.OCRA.BorgCR10.ExtremelyStrongMaximal10);

        DataGridViewComboBoxColumn comboBorg = new DataGridViewComboBoxColumn();
        comboBorg.HeaderText = "Borg CR-10";
        comboBorg.DataSource = tabBorg;
        comboBorg.DisplayMember = "Type";
        comboBorg.ValueMember = "TypeValue";
        comboBorg.Name = "BorgScale";
        this.Columns.Add(comboBorg);

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
            str += this[1, i].Value;
        }

        return string.Empty;
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
