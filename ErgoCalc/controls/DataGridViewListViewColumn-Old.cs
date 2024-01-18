using System.Windows.Forms;

namespace ErgoCalc.controls;


// https://learn.microsoft.com/en-us/dotnet/desktop/winforms/controls/how-to-host-controls-in-windows-forms-datagridview-cells?view=netframeworkdesktop-4.8&redirectedfrom=MSDN
// https://www.codeproject.com/Tips/588733/Use-ListView-control-for-editing-cell-in-DataGridV
// https://www.c-sharpcorner.com/UploadFile/deveshomar/nested-datagrid-in-C-Sharp-window-forms/

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
    public ListViewEditingControl m_control = new();

    public ListViewCell()
        : base()
    {
        // Use the short date format.
        //this.Style.Format = "d";
        m_control.Columns.Add("Item Column", -2, HorizontalAlignment.Left);
    }

    public override object Clone()
    {
        ListViewCell? cell = base.Clone() as ListViewCell;
        if (cell is not  null)
            cell.m_control = this.m_control;
        return cell ?? new ListViewCell();
    }

    public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
    {
        // Set the value of the editing control to the current cell value.
        base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
        ListViewEditingControl? ctl = DataGridView?.EditingControl as ListViewEditingControl;

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
        DataGridView? dataGridView = this.DataGridView;

        if (dataGridView is null || dataGridView.EditingControl is null)
            throw new InvalidOperationException("Cell is detached or its grid has no editing control.");

        if (DataGridView?.EditingControl is ListViewEditingControl ctl)
        {
            // this.Value = ctl.SelectedItems[0].SubItems[0].ToString();
            ctl.EditingControlFormattedValue = String.Empty;
        }

        base.DetachEditingControl();
    }

    public override void PositionEditingControl(bool setLocation, bool setSize, Rectangle cellBounds, Rectangle cellClip, DataGridViewCellStyle cellStyle, bool singleVerticalBorderAdded, bool singleHorizontalBorderAdded, bool isFirstDisplayedColumn, bool isFirstDisplayedRow)
    {
        Rectangle ctlSize = new(new Point(cellBounds.Location.X + 10, cellBounds.Location.Y + 10), new Size(cellBounds.Width + 300, 150));
        base.PositionEditingControl(setLocation, setSize, ctlSize, ctlSize, cellStyle, singleVerticalBorderAdded, singleHorizontalBorderAdded, isFirstDisplayedColumn, isFirstDisplayedRow);
        if (m_control is not null)
        {
            Point pos = new(cellBounds.Location.X + 10, cellBounds.Location.Y + 10);
            pos.Offset(DataGridView?.Location ?? new Point(0, 0));
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

public class ListViewEditingControl : ListViewCustom, IDataGridViewEditingControl
{
    DataGridView? dataGridView;
    private bool valueChanged = false;
    int index;                           // Index of chosen element in listview
    public int RowIndex { get; private set; }   // Row in datagridview
    private bool ended = false;                  // Tells if edit has been ended

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
            return this?.Tag ?? string.Empty;
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
            return RowIndex;
        }
        set
        {
            RowIndex = value;
        }
    }

    /// <summary>
    /// Implements the IDataGridViewEditingControl.EditingControlWantsInputKey method.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="dataGridViewWantsInputKey"></param>
    /// <returns></returns>
    public bool EditingControlWantsInputKey(Keys key, bool dataGridViewWantsInputKey)
    {
        // Let the DateTimePicker handle the keys listed.
        return (key & Keys.KeyCode) switch
        {
            Keys.Left or Keys.Up or Keys.Down or Keys.Right or Keys.Home or Keys.End or Keys.PageDown or Keys.PageUp => true,
            _ => !dataGridViewWantsInputKey,
        };
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
    public DataGridView? EditingControlDataGridView
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

    protected virtual void NotifyDataGridViewOfValueChange()
    {
        this.valueChanged = true;
        this.dataGridView?.NotifyCurrentCellDirty(true);
    }

    protected override bool ProcessDialogKey(Keys keyData)
    {
        if (keyData == Keys.Enter)
        {
            // Notify the DataGridView that the contents of the cell 
            // have changed.
            valueChanged = true;
            if (this.SelectedItems.Count == 1)
                index = this.SelectedItems[0].Index;
            if (dataGridView?.IsCurrentCellInEditMode ?? false)
            {
                ended = true;
                dataGridView.EndEdit();
            }
        }
        return base.ProcessDialogKey(keyData);
    }

    protected override void OnDoubleClick(EventArgs e)
    {
        base.OnDoubleClick(e);

        // Notify the DataGridView that the contents of the cell 
        // have changed.
        valueChanged = true;
        if (this.SelectedItems.Count == 1)
            index = this.SelectedItems[0].Index;
        base.OnDoubleClick(e);

        if (dataGridView?.IsCurrentCellInEditMode ?? false)
        {
            ended = true;
            dataGridView.EndEdit();
        }
    }

    protected override void OnCreateControl()
    {
        base.OnCreateControl();
        // Need to add controll to parent ui to get it floating outside of gridview


        if (this.TopLevelControl is FrmDataOCRAcheck topLevel && this.Parent != topLevel)
        {
            topLevel.Controls.Add(this);
            dataGridView?.Controls.Remove(this);
            this.BringToFront();
        }
    }

    protected override void OnLeave(EventArgs e)
    {
        // Notify the DataGridView that the contents of the cell
        // have changed.
        base.OnLeave(e);
        if ((dataGridView?.IsCurrentCellInEditMode ?? false) && !ended)
            dataGridView?.EndEdit(); // Ending edit twice will make the program crash.

        // Remove control from parent ui after edit is done.
        if (this.Parent is FrmDataOCRAcheck parent)
        {
            parent.Controls.Remove(this);
            dataGridView?.ClearSelection();
        }
    }

    protected override void OnSelectedIndexChanged(EventArgs eventargs)
    {
        // Notify the DataGridView that the contents of the cell
        // have changed.
        valueChanged = true;
        this.EditingControlDataGridView?.NotifyCurrentCellDirty(true);
        base.OnSelectedIndexChanged(eventargs);
    }
}

public class ListViewCustom : ListView
{
    private ListViewItem? m_item;
    readonly List<String> sourceList = [];
    private int m_selectedSubItem = 0;
    private string m_subItemText = "";
    public string? m_sValue { get; set; }
    private int m_iX = 0, m_iY = 0;
    private readonly System.Windows.Forms.ColumnHeader columnHeader1;

    //---------------------------------------------------------------------------------------

    public ListViewCustom()
    {
        // Create three items and three sets of subitems for each item.
        ListViewItem item1 = new("item1", 0);
        // Place a check mark next to the item.
        item1.Checked = true;
        item1.SubItems.Add("1");
        item1.SubItems.Add("2");
        item1.SubItems.Add("3");
        ListViewItem item2 = new("item2", 1);
        item2.SubItems.Add("4");
        item2.SubItems.Add("5");
        item2.SubItems.Add("6");
        ListViewItem item3 = new("item3", 0);
        // Place a check mark next to the item.
        item3.Checked = true;
        item3.SubItems.Add("7");
        item3.SubItems.Add("8");
        item3.SubItems.Add("9");
        // Create columns for the items and subitems. 
        // Width of -2 indicates auto-size.
        this.Columns.Add("Item Column", -2, HorizontalAlignment.Left);
        this.Columns.Add("Column 2", -2, HorizontalAlignment.Left);
        this.Columns.Add("Column 3", -2, HorizontalAlignment.Left);
        this.Columns.Add("Column 4", -2, HorizontalAlignment.Center);
        //Add the items to the ListView.
        this.Items.AddRange(new ListViewItem[] { item1, item2, item3 });


        this.Name = "listViewWithComboBox1";
        this.Size = new(0, 0);
        this.TabIndex = 0;
        this.View = System.Windows.Forms.View.Details;
        this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ListViewMouseDown);
        this.DoubleClick += new System.EventHandler(this.ListViewDoubleClick);
        this.GridLines = true;
        this.HeaderStyle = ColumnHeaderStyle.None;
        this.MultiSelect = false;

        // 
        // columnHeader1
        // 
        this.columnHeader1 = new();
        this.Columns.AddRange([this.columnHeader1]);
        this.columnHeader1.Text = "Widths";
        this.columnHeader1.Width = 100;
    }

    public void ListViewDoubleClick(object? sender, System.EventArgs e)
    {
        // Check whether the subitem was clicked
        int start = m_iX;
        int position = 0;
        int end = this.Columns[0].Width;
        for (int i = 0; i < this.Columns.Count; i++)
        {
            if (start > position && start < end)
            {
                m_selectedSubItem = i;
                break;
            }

            position = end;
            end += this.Columns[i].Width;
        }

        m_subItemText = m_item?.SubItems[m_selectedSubItem].Text ?? string.Empty;

    }

    public void ListViewMouseDown(object? sender, MouseEventArgs e)
    {
        m_item = this.GetItemAt(e.X, e.Y);
        m_iX = e.X;
        m_iY = e.Y;
    }

}


// https://learn.microsoft.com/en-us/troubleshoot/developer/visualstudio/csharp/language-compilers/use-combobox-edit-listview

public class ListViewComboBox : ListView
{
    private readonly ComboBox cbListViewCombo = new();

    protected override void OnMouseUp(MouseEventArgs e)
    {
        // Get the item on the row that is clicked.
        ListViewItem? lvItem = this.GetItemAt(e.X, e.Y);

        // Make sure that an item is clicked.
        if (lvItem is not null)
        {
            // Get the bounds of the item that is clicked.
            Rectangle ClickedItem = lvItem.Bounds;

            // Verify that the column is completely scrolled off to the left.
            if ((ClickedItem.Left + this.Columns[0].Width) < 0)
            {
                // If the cell is out of view to the left, do nothing.
                return;
            }

            // Verify that the column is partially scrolled off to the left.
            else if (ClickedItem.Left < 0)
            {
                // Determine if column extends beyond right side of ListView.
                if ((ClickedItem.Left + this.Columns[0].Width) > this.Width)
                {
                    // Set width of column to match width of ListView.
                    ClickedItem.Width = this.Width;
                    ClickedItem.X = 0;
                }
                else
                {
                    // Right side of cell is in view.
                    ClickedItem.Width = this.Columns[0].Width + ClickedItem.Left;
                    ClickedItem.X = 2;
                }
            }
            else if (this.Columns[0].Width > this.Width)
            {
                ClickedItem.Width = this.Width;
            }
            else
            {
                ClickedItem.Width = this.Columns[0].Width;
                ClickedItem.X = 2;
            }

            // Adjust the top to account for the location of the ListView.
            ClickedItem.Y += this.Top;
            ClickedItem.X += this.Left;

            // Assign calculated bounds to the ComboBox.
            this.cbListViewCombo.Bounds = ClickedItem;

            // Set default text for ComboBox to match the item that is clicked.
            this.cbListViewCombo.Text = lvItem.Text;

            // Display the ComboBox, and make sure that it is on top with focus.
            this.cbListViewCombo.Visible = true;
            this.cbListViewCombo.BringToFront();
            this.cbListViewCombo.Focus();
        }
    }

    private const int WM_HSCROLL = 0x114;
    private const int WM_VSCROLL = 0x115;

    protected override void WndProc(ref Message msg)
    {
        // Look for the WM_VSCROLL or the WM_HSCROLL messages.
        if ((msg.Msg == WM_VSCROLL) || (msg.Msg == WM_HSCROLL))
        {
            // Move focus to the ListView to cause ComboBox to lose focus.
            this.Focus();
        }

        // Pass message to default handler.
        base.WndProc(ref msg);
    }
}