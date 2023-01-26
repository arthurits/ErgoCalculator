namespace ErgoCalc.controls;


// https://learn.microsoft.com/en-us/dotnet/desktop/winforms/controls/how-to-host-controls-in-windows-forms-datagridview-cells?view=netframeworkdesktop-4.8&redirectedfrom=MSDN
// https://www.codeproject.com/Tips/588733/Use-ListView-control-for-editing-cell-in-DataGridV

public class ListViewColumn : DataGridViewColumn
{
    public ListViewColumn() : base(new ListViewCell())
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
            if (value is not null && !value.GetType().IsAssignableFrom(typeof(ListViewCell)))
                throw new InvalidCastException("Must be a ListViewCell");

            base.CellTemplate = value;
        }
    }
}

public class ListViewCell : DataGridViewTextBoxCell
{
    private ListViewEditingControl
            m_control;

    public ListViewCell()
        : base()
    {
        // Use the short date format.
        //this.Style.Format = "d";
    }

    public override object Clone()
    {
        ListViewCell cell = base.Clone() as ListViewCell;
        if (cell != null)
            cell.m_control = this.m_control;
        return cell;
    }

    public override void InitializeEditingControl(int rowIndex, object
        initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
    {
        // Set the value of the editing control to the current cell value.
        base.InitializeEditingControl(rowIndex, initialFormattedValue,
            dataGridViewCellStyle);
        ListViewEditingControl ctl = DataGridView.EditingControl as ListViewEditingControl;

        // Use the default row value when Value property is null.
        //if (this.Value == null)
        //{
        //    ctl.Value = (DateTime)this.DefaultNewRowValue;
        //}
        //else
        //{
        //    ctl.Value = (DateTime)this.Value;
        //}
    }

    public override void DetachEditingControl()
    {
        DataGridView dataGridView = this.DataGridView;

        if (dataGridView == null || dataGridView.EditingControl == null)
            throw new InvalidOperationException("Cell is detached or its grid has no editing control.");

        ListViewEditingControl ctl = DataGridView.EditingControl as ListViewEditingControl;
        if (ctl != null)
        {
            //                this.Value = ctl.SelectedItems[0].SubItems[0].ToString();
            ctl.EditingControlFormattedValue = String.Empty;
        }

        base.DetachEditingControl();
    }

    public override void PositionEditingControl(bool setLocation, bool setSize, Rectangle cellBounds, Rectangle cellClip, DataGridViewCellStyle cellStyle, bool singleVerticalBorderAdded, bool singleHorizontalBorderAdded, bool isFirstDisplayedColumn, bool isFirstDisplayedRow)
    {
        Rectangle
            ctlSize = new Rectangle(new Point(cellBounds.Location.X + 20, cellBounds.Location.Y + 20), new Size(cellBounds.Width + 100, 100));
        base.PositionEditingControl(setLocation, setSize, ctlSize, ctlSize, cellStyle, singleVerticalBorderAdded, singleHorizontalBorderAdded, isFirstDisplayedColumn, isFirstDisplayedRow);
        if (m_control != null)
        {
            Point
                pos = new Point(cellBounds.Location.X + 20, cellBounds.Location.Y + 20);
            pos.Offset(DataGridView.Location);
            m_control.Location = pos;
        }
    }

    public override Type EditType
    {
        get
        {
            // Return the type of the editing control that CalendarCell uses.
            return typeof(ListViewEditingControl);
        }
    }

    public override Type ValueType
    {
        get
        {
            // Return the type of the value that CalendarCell contains.

            return typeof(DateTime);
        }
    }

    public override object DefaultNewRowValue
    {
        get
        {
            // Use the current date and time as the default value.
            return DateTime.Now;
        }
    }
}

class ListViewEditingControl : ListView, IDataGridViewEditingControl
{
    DataGridView dataGridView;
    private bool valueChanged = false;
    int rowIndex;

    public ListViewEditingControl()
    {
        //this.Format = DateTimePickerFormat.Short;
    }

    // Implements the IDataGridViewEditingControl.EditingControlFormattedValue
    // property.
    public object EditingControlFormattedValue
    {
        get
        {
            return this.Tag;
        }
        set
        {
            //if (value is String)
            //{
            //    try
            //    {
            //        // This will throw an exception of the string is
            //        // null, empty, or not in the format of a date.
            //        this.Value = DateTime.Parse((String)value);
            //    }
            //    catch
            //    {
            //        // In the case of an exception, just use the
            //        // default value so we're not left with a null
            //        // value.
            //        this.Value = DateTime.Now;
            //    }
            //}
        }
    }

    // Implements the
    // IDataGridViewEditingControl.GetEditingControlFormattedValue method.
    public object GetEditingControlFormattedValue(
        DataGridViewDataErrorContexts context)
    {
        return EditingControlFormattedValue;
    }

    /// <summary>
    /// Implements the IDataGridViewEditingControl.ApplyCellStyleToEditingControl method.
    /// </summary>
    /// <param name="dataGridViewCellStyle"></param>
    public void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
    {
        this.Font = dataGridViewCellStyle.Font;
        this.ForeColor = dataGridViewCellStyle.ForeColor;
        this.BackColor = dataGridViewCellStyle.BackColor;

    }

    /// <summary>
    /// Implements the IDataGridViewEditingControl.EditingControlRowIndex 
    /// </summary>
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

    /// <summary>
    /// Implements the IDataGridViewEditingControl.EditingControlWantsInputKey method.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="dataGridViewWantsInputKey"></param>
    /// <returns></returns>
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

    protected override void OnSelectedIndexChanged(EventArgs eventargs)
    {
        // Notify the DataGridView that the contents of the cell
        // have changed.
        valueChanged = true;
        this.EditingControlDataGridView.NotifyCurrentCellDirty(true);
        base.OnSelectedIndexChanged(eventargs);
    }
}
