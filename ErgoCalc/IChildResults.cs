namespace ErgoCalc;

/// <summary>
/// Public interface for child windows used to show the results
/// </summary>
public interface IChildResults
{
    ModelType? Model { get; set; }

    /// <summary>
    /// Gets and set the child's ToolStrip control in order to be merged with the parent's
    /// </summary>
    ToolStrip? ChildToolStrip { get; set; }

    /// <summary>
    /// Defines the enabled state for each control in frmMain's ToolBar
    /// </summary>
    /// <returns>An array with 'enabled' boolean values</returns>
    bool[] GetToolbarEnabledState();

    /// <summary>
    /// Saves the data shown in the child window into a file
    /// </summary>
    /// <param name="path">Path where the data should be saved</param>
    void Save(string path);

    /// <summary>
    /// Opens a document
    /// </summary>
    /// <param name="document">Document</param>
    /// <returns></returns>
    bool OpenFile(System.Text.Json.JsonDocument document);
    
    /// <summary>
    /// Updates the text output to the active culture
    /// </summary>
    void UpdateLanguage(System.Globalization.CultureInfo culture);

    /// <summary>
    /// Formats the text shown in the RichtTextBox
    /// </summary>
    void FormatText();

    /// <summary>
    /// Edits the original data of the child window
    /// </summary>
    void EditData();

    /// <summary>
    /// Duplicates the current child window
    /// </summary>
    void Duplicate();

    /// <summary>
    /// Computes the tabs positions in pixels.
    /// </summary>
    /// <param name="g">Graphics object</param>
    /// <param name="font">Font used to write the text and to compute the text space</param>
    /// <param name="numberTabs">Number of tabs to be computed</param>
    /// <param name="rowHeaders">The first-column texts used to computed the first tab position</param>
    /// <param name="columnHeaders">All the other column header texts used to compute the rest of tabs</param>
    /// <param name="tabFactor">Factor (percentage) of the maximum measure to be used as tab space</param>
    /// <param name="tabMinSpace">Minimum tab space in pixels. Default value is 10</param>
    /// <returns>Array of tab positions in pixels</returns>
    public int[] ComputeTabs(Graphics g, Font font, int numberTabs, string[] rowHeaders, string[] columnHeaders, double tabFactor = 0.1, int tabMinSpace = 10)
    {
        (int rowMax, int rowTab) = ComputeTabSpace(g, font, rowHeaders, tabFactor, tabMinSpace);
        (int colMax, int colTab) = ComputeTabSpace(g, font, columnHeaders, tabFactor, tabMinSpace);
        int tab = Math.Min(rowTab, colTab);

        int[] tabs = new int[numberTabs];
        for (int i = 0; i < numberTabs; i++)
        {
            if (i == 0)
                tabs[i] = rowMax + tab;
            else
                tabs[i] = tabs[i - 1] + colMax + tab;
        }

        return tabs;
    }

    /// <summary>
    /// Computes the tabs using a particular <see cref="Graphics"/> object and <see cref="Font"/>
    /// </summary>
    /// <param name="strings">Array of strings that will be measured. The greatest measure is used to compute the tab space</param>
    /// <param name="tabFactor">Factor (percentage) of the maximum measure to be used as tab space</param>
    /// <param name="tabMinSpace">Minimum tab space in pixels. Default value is 10</param>
    /// <returns>The maximum width of the string array in pixels and the corresponding tab space to be added to the width</returns>
    public (int maxWidth, int tabSpace) ComputeTabSpace(Graphics g, Font font, string[] strings, double tabFactor = 0.1, int tabMinSpace = 10)
    {
        SizeF size;
        int nWidth = 0;
        int tabSpace;

        //using var g = rtbShowResult.CreateGraphics();
        foreach (string strRow in strings)
        {
            size = g.MeasureString(strRow, font);
            if (size.Width > nWidth)
                nWidth = (int)size.Width;
        }

        tabSpace = (int)(nWidth * tabFactor);
        tabSpace = tabSpace > tabMinSpace ? tabSpace : tabMinSpace;

        return (nWidth, tabSpace);
    }

}
