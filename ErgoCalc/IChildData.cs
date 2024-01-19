namespace ErgoCalc;

/// <summary>
/// Public interface for data windows
/// </summary>
public interface IChildData
{
    public object GetData { get; }

    /// <summary>
    /// Function to load data examples
    /// </summary>
    //public void LoadExample();

    /// <summary>
    /// Updates the number of columns in a DataGridView control
    /// </summary>
    /// <param name="control">Reference to a <see cref="DataGridView"/> control</param>
    /// <param name="value">Number of columns in the <see cref="DataGridView"/> control</param>
    public void UpdateGridColumns(object control, int value)
    {
        if (control is DataGridView gridVariables)
        {
            // Add or remove columns
            if (value > gridVariables.ColumnCount)
            {
                for (int i = gridVariables.ColumnCount; i < value; i++)
                    AddColumn(i);
            }
            else if (value < gridVariables.ColumnCount)
            {
                for (int i = gridVariables.ColumnCount - 1; i >= value; i--)
                    gridVariables.Columns.RemoveAt(i);
            }
        }
    }

    /// <summary>
    /// Adds a column to a DataGridView control. This function should be implemented in the derived classes
    /// </summary>
    /// <param name="col">Column index (0-based)</param>
    protected internal void AddColumn(int col)
    {
        return;
    }

    /// <summary>
    /// Adds a column to a DataGridView control
    /// </summary>
    /// <param name="control">Reference to a <see cref="DataGridView"/> control</param>
    /// <param name="col">Column index (0-based)</param>
    /// <param name="strName">Header text to be shown in the column</param>
    /// <param name="width">Width, in pixels, of the column. Default value is 75</param>
    public void AddColumnBasic(object control, int col, string strName, int width = 75)
    {
        if (control is not DataGridView gridVariables) return;

        // By default, the DataGrid always contains a single column
        //if (col == 0) return;
        if (gridVariables.Columns.Contains($"Column {(col).ToString()}")) return;

        // Create the new column
        //gridVariables.Columns.Add("Column" + (col + 1).ToString(), "Task " + strTasks[col]);
        gridVariables.Columns.Add($"Column {(col).ToString()}", $"{strName} {((char)('A' + col)).ToString()}");
        gridVariables.Columns[col].SortMode = DataGridViewColumnSortMode.NotSortable;
        gridVariables.Columns[col].Width = width;
        gridVariables.Columns[col].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
        return;
    }

    /// <summary>
    /// Adds the text-based row headers to a DataGridView control.
    /// </summary>
    /// <param name="control">Reference to a <see cref="DataGridView"/> control.</param>
    /// <param name="strHeaders"><see cref="Array"/> of strings containing the row header texts</param>
    public void AddGridRowHeaders(object control, string[] strHeaders)
    {
        if (control is not DataGridView gridVariables)
            return;
        if (strHeaders.Length == 0)
            return;

        // Create the header rows
        gridVariables.RowCount = strHeaders.Length;
        for (int i = 0; i < strHeaders.Length; i++)
        {
            gridVariables.Rows[i].HeaderCell.Value = strHeaders[i];
        }

        // Adjust the column width
        gridVariables.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
    }

    /// <summary>
    /// Updates the number of groups in a ListView control
    /// </summary>
    /// <param name="control">Reference to a <see cref="ListView"/> control.</param>
    /// <param name="value">Number of groups in the <see cref="ListView"/> control.</param>
    /// <param name="strGroup">Group name to be shown on the ListView</param>
    public void UpdateListView(object control, int value, string strGroup = "Task")
    {
        //Int32 tasks = Convert.ToInt32(updTasks.Value);

        if (control is ListViewEx listViewEx)
        {
            if (value > listViewEx.Groups.Count)
            {
                for (int i = listViewEx.Groups.Count; i < value; i++)
                    listViewEx.AddGroup(i, strGroup);
            }
            else if (value < listViewEx.Groups.Count)
            {
                for (int i = value; i < listViewEx.Groups.Count; i++)
                    listViewEx.RemoveGroup(listViewEx.Groups.Count - 1);
            }
        }
    }

}
