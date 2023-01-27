namespace ErgoCalc.controls;


// https://learn.microsoft.com/en-us/dotnet/desktop/winforms/controls/how-to-host-controls-in-windows-forms-datagridview-cells?view=netframeworkdesktop-4.8&redirectedfrom=MSDN
// https://www.codeproject.com/Tips/588733/Use-ListView-control-for-editing-cell-in-DataGridV
// https://www.c-sharpcorner.com/UploadFile/deveshomar/nested-datagrid-in-C-Sharp-window-forms/

public class CustomViewColumn : DataGridViewColumn
{
    public CustomViewColumn() : base()
    {
        this.CellTemplate = new CustomViewCell();
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
            if (value is not null && !value.GetType().IsAssignableFrom(typeof(CustomViewCell)))
                throw new InvalidCastException("Must be a ListViewCell");

            base.CellTemplate = value;
        }
    }
}

/// <summary>
/// Cell edited by listview controls.
/// </summary>
public class CustomViewCell : DataGridViewTextBoxCell
{
    private CustomViewControl
            m_control;

    //-----------------------------------------------------------------------------------------

    public CustomViewCell()
        : base()
    {
    }

    public override object Clone()
    {
        CustomViewCell
            cell = base.Clone() as CustomViewCell;
        if (cell != null)
            cell.m_control = this.m_control;
        return cell;
    }

    //-----------------------------------------------------------------------------------------

    public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
    {
        base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);

        m_control = DataGridView.EditingControl as CustomViewControl;
    }

    public override void DetachEditingControl()
    {
        base.DetachEditingControl();

        DataGridView dataGridView = this.DataGridView;

        if (dataGridView == null || dataGridView.EditingControl == null)
            throw new InvalidOperationException("Cell is detached or its grid has no editing control.");

        CustomViewControl ctl = DataGridView.EditingControl as CustomViewControl;
        if (ctl != null && ctl.SelectedItems.Count > 0)
        {
            this.Value = ctl.SelectedItems[0].SubItems[0].ToString();
            ctl.EditingControlFormattedValue = String.Empty;
        }
    }

    //-----------------------------------------------------------------------------------------

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

    //-----------------------------------------------------------------------------------------

    public override Type EditType
    {
        get
        {
            return typeof(CustomViewControl);
        }
    }

    public override Type ValueType
    {
        get
        {
            return typeof(System.Collections.ICollection);
        }
    }

    public override Type FormattedValueType
    {
        get
        {
            return typeof(System.Collections.ICollection);
        }
    }

    //-----------------------------------------------------------------------------------------

    protected override void Paint(Graphics graphics,
                            Rectangle clipBounds, Rectangle cellBounds, int rowIndex,
                            DataGridViewElementStates elementState, object value,
                            object formattedValue, string errorText,
                            DataGridViewCellStyle cellStyle,
                            DataGridViewAdvancedBorderStyle advancedBorderStyle,
                            DataGridViewPaintParts paintParts)
    {
        formattedValue = null;

        base.Paint(graphics, clipBounds, cellBounds, rowIndex, elementState, value, formattedValue,
                   errorText, cellStyle, advancedBorderStyle, paintParts);

        // Draw the cell background, if specified. 
        if ((paintParts & DataGridViewPaintParts.Background) == DataGridViewPaintParts.Background)
        {
            SolidBrush cellBackground = new SolidBrush(cellStyle.BackColor);
            graphics.FillRectangle(cellBackground, cellBounds);
            cellBackground.Dispose();
        }

        // Draw the cell borders, if specified. 
        if ((paintParts & DataGridViewPaintParts.Border) == DataGridViewPaintParts.Border)
            PaintBorder(graphics, clipBounds, cellBounds, cellStyle, advancedBorderStyle);

        Rectangle
            drawRect = cellBounds;
        // Set format of string.
        StringFormat drawFormat = new StringFormat();
        drawFormat.LineAlignment = StringAlignment.Center;

        if (Value != null)
            graphics.DrawString(Value.ToString(), cellStyle.Font, Brushes.Black, drawRect, drawFormat);
    }

    protected override void OnClick(DataGridViewCellEventArgs e)
    {
        this.DataGridView.BeginEdit(false);
        base.OnClick(e);
    }
}

/// <summary>
/// Listview control for editing content in datagridview.
/// </summary>
public class CustomViewControl : CustomListView, IDataGridViewEditingControl
{
    DataGridView
            m_dataGridView;
    private bool
        m_boValueChanged = false;
    int
        m_iIndex;                           // Index of chosen element in listview
    public int
        m_iRowIndex
    { get; private set; }   // Row in datagridview
    private bool
        m_boEnded = false;                  // Tells if edit has been ended

    //-----------------------------------------------------------------------------------------

    public CustomViewControl()
    {
    }

    //-- Functions from IDataGridViewEditingControl -------------------------------------------

    /// <summary>
    /// Implements the IDataGridViewEditingControl.GetEditingControlFormattedValue method.
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
    {
        return EditingControlFormattedValue;
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
            //  this.Tag = value;  
        }
    }

    /// <summary>
    /// Implements the IDataGridViewEditingControl.ApplyCellStyleToEditingControl method.
    /// </summary>
    /// <param name="dataGridViewCellStyle"></param>
    public void ApplyCellStyleToEditingControl(
        DataGridViewCellStyle dataGridViewCellStyle)
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
            return m_iRowIndex;
        }
        set
        {
            m_iRowIndex = value;
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
            case Keys.Enter:
                return true;
            default:
                return !dataGridViewWantsInputKey;
        }
    }

    /// <summary>
    /// Implements the IDataGridViewEditingControl.PrepareEditingControlForEdit method.
    /// </summary>
    /// <param name="selectAll"></param>
    public void PrepareEditingControlForEdit(bool selectAll)
    {
        // No preparation needs to be done.
    }

    /// <summary>
    /// Implements the IDataGridViewEditingControl.RepositionEditingControlOnValueChange property.
    /// </summary>
    public bool RepositionEditingControlOnValueChange
    {
        get
        {
            return false;
        }
    }

    /// <summary>
    /// Implements the IDataGridViewEditingControl.EditingControlDataGridView property.
    /// </summary>
    public DataGridView EditingControlDataGridView
    {
        get
        {
            return m_dataGridView;
        }
        set
        {
            m_dataGridView = value;
        }
    }

    /// <summary>
    /// Implements the IDataGridViewEditingControl.EditingControlValueChanged property.
    /// </summary>
    public bool EditingControlValueChanged
    {
        get
        {
            return m_boValueChanged;
        }
        set
        {
            m_boValueChanged = value;
        }
    }

    /// <summary>
    /// Implements the IDataGridViewEditingControl
    /// </summary>
    public Cursor EditingPanelCursor
    {
        get
        {
            return base.Cursor;
        }
    }

    //---------------------------------------------------------------------------------------

    protected virtual void NotifyDataGridViewOfValueChange()
    {
        this.m_boValueChanged = true;
        if (this.m_dataGridView != null)
        {
            this.m_dataGridView.NotifyCurrentCellDirty(true);
        }
    }

    protected override bool ProcessDialogKey(Keys keyData)
    {
        if (keyData == Keys.Enter)
        {
            // Notify the DataGridView that the contents of the cell 
            // have changed.
            m_boValueChanged = true;
            if (this.SelectedItems.Count == 1)
                m_iIndex = this.SelectedItems[0].Index;
            if (m_dataGridView.IsCurrentCellInEditMode)
            {
                m_boEnded = true;
                m_dataGridView.EndEdit();
            }
        }
        return base.ProcessDialogKey(keyData);
    }

    protected override void OnDoubleClick(EventArgs e)
    {
        base.OnDoubleClick(e);

        // Notify the DataGridView that the contents of the cell 
        // have changed.
        m_boValueChanged = true;
        if (this.SelectedItems.Count == 1)
            m_iIndex = this.SelectedItems[0].Index;

        base.OnDoubleClick(e);

        if (m_dataGridView.IsCurrentCellInEditMode)
        {
            m_boEnded = true;
            m_dataGridView.EndEdit();
        }
    }

    protected override void OnCreateControl()
    {
        base.OnCreateControl();
        // Need to add controll to parent ui to get it floating outside of gridview
        FrmDataOCRAcheck topLevel = this.TopLevelControl as FrmDataOCRAcheck;
        if (topLevel != null && this.Parent != topLevel)
        {
            topLevel.Controls.Add(this);
            m_dataGridView.Controls.Remove(this);
            this.BringToFront();
        }
    }

    protected override void OnLeave(EventArgs e)
    {
        // Notify the DataGridView that the contents of the cell
        // have changed.
        base.OnLeave(e);
        if (m_dataGridView.IsCurrentCellInEditMode && !m_boEnded)
            m_dataGridView.EndEdit(); // Ending edit twice will make the program crash.

        // Remove control from parent ui after edit is done.
        FrmDataOCRAcheck parent = this.Parent as FrmDataOCRAcheck;
        if (parent != null)
        {
            parent.Controls.Remove(this);
            m_dataGridView.ClearSelection();
        }
    }
}

public class CustomListView : ListView
{
    private ListViewItem m_item;
    List<String> sourceList = new List<String>();
    private int m_selectedSubItem = 0;
    private string m_subItemText = "";
    public String m_sValue { get; set; }
    private int m_iX = 0, m_iY = 0;
    private System.Windows.Forms.ColumnHeader columnHeader1;

    //---------------------------------------------------------------------------------------

    public CustomListView()
    {
        // Create three items and three sets of subitems for each item.
        ListViewItem item1 = new ListViewItem("item1", 0);
        // Place a check mark next to the item.
        item1.Checked = true;
        item1.SubItems.Add("1");
        item1.SubItems.Add("2");
        item1.SubItems.Add("3");
        ListViewItem item2 = new ListViewItem("item2", 1);
        item2.SubItems.Add("4");
        item2.SubItems.Add("5");
        item2.SubItems.Add("6");
        ListViewItem item3 = new ListViewItem("item3", 0);
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
        this.Size = new System.Drawing.Size(0, 0);
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
        this.columnHeader1 = new ColumnHeader();
        this.Columns.AddRange(new ColumnHeader[] { this.columnHeader1 });
        this.columnHeader1.Text = "Widths";
        this.columnHeader1.Width = 100;
    }

    public void ListViewDoubleClick(object sender, System.EventArgs e)
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

        m_subItemText = m_item.SubItems[m_selectedSubItem].Text;

    }

    public void ListViewMouseDown(object sender, MouseEventArgs e)
    {
        m_item = this.GetItemAt(e.X, e.Y);
        m_iX = e.X;
        m_iY = e.Y;
    }

    protected override void OnParentChanged(
        EventArgs e
    )
    {
        base.OnParentChanged(e);
    }
}
