using System.Text.Json;

namespace ErgoCalc;

public partial class FrmResultsOCRAcheck : Form, IChildResults
{
    private readonly System.Globalization.CultureInfo _culture = System.Globalization.CultureInfo.CurrentCulture;

    public FrmResultsOCRAcheck()
    {
        InitializeComponent();

        this.Icon = GraphicsResources.Load<Icon>(GraphicsResources.AppLogo);
    }

    public FrmResultsOCRAcheck(object? data = null, System.Globalization.CultureInfo? culture = null, ModelType? model = null)
        :this()
    {
        //if (data is not null && data.GetType() == typeof(Job))
        //    _job = (Job)data;

        _culture = culture ?? System.Globalization.CultureInfo.CurrentCulture;
        Model = model;
    }

    #region Events

    private void frmResultsOCRAcheck_Shown(object sender, EventArgs e)
    {
        bool result = true;
        // Call the DLL function
        try
        {
            //_classDLL.StrainIndex();
            //_job = _classDLL.Job;
        }
        catch (EntryPointNotFoundException)
        {
            result = false;
            MessageBox.Show(
                "The program calculation kernel's been tampered with.\nThe RSI could not be computed.",
                "RSI index error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
        catch (DllNotFoundException)
        {
            result = false;
            MessageBox.Show(
                "DLL files are missing. Please\nreinstall the application.",
                "RSI index error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
        catch (Exception ex)
        {
            result = false;
            MessageBox.Show(
                "Error in the calculation kernel:\n" + ex.ToString(),
                "Unexpected error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        // If computation is OK, then call the routine that shows the results
        if (result)
            UpdateOutput(_culture);

    }

    #endregion Events

    #region Private routines

    /// <summary>
    /// Serialize ModelJob structure to JSON
    /// </summary>
    /// <param name="writer">The already created writer</param>
    private void SerializeToJSON(Utf8JsonWriter writer)
    {
        
    }

    #endregion Private routines

    #region IChildResults

    public ToolStrip? ChildToolStrip { get => null; set { } }

    public ModelType? Model { get; set; }

    public bool[] GetToolbarEnabledState() => [true, true, true, false, true, true, false, true, true, false, false, true, true, true];

    public string Save(string directoryPath)
    {
        DialogResult result;
        string userPath = string.Empty;

        SaveFileDialog SaveDlg = new()
        {
            DefaultExt = "*.rtf",
            Filter = "ERGO file (*.ergo)|*.ergo|RTF file (*.rtf)|*.rtf|Text file (*.txt)|*.txt|All files (*.*)|*.*",
            FilterIndex = 2,
            FileName = "OCRA checklist results",
            Title = "Save OCRA checklist results",
            OverwritePrompt = true,
            InitialDirectory = string.IsNullOrWhiteSpace(directoryPath) ? Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) : directoryPath
        };

        using (new CenterWinDialog(this))
        {
            result = SaveDlg.ShowDialog();
        }

        // If the file name is not an empty string open it for saving.  
        if (result == DialogResult.OK && SaveDlg.FileName != "")
        {
            using var fs = SaveDlg.OpenFile();

            // Saves the text via a FileStream created by the OpenFile method.  
            if (fs is not null)
            {
                // Get the actual directory path selected by the user in order to store it later in the settings
                userPath = Path.GetDirectoryName(SaveDlg.FileName) ?? string.Empty;
            }
        }

        return userPath;
    }

    public bool OpenFile(JsonDocument document)
    {
        throw new NotImplementedException();
    }

    public void UpdateOutput(System.Globalization.CultureInfo culture)
    {
        //rtbShowResult.Text = _job.ToString();
        //CreatePlots();
        //FormatText();
    }

    public void FormatText()
    {
        int nStart = 0, nEnd = 0;

        while (true)
        {
            // Underline
            nStart = rtbShowResult.Find("Description", nStart + 1, -1, RichTextBoxFinds.MatchCase);
            if (nStart == -1) break;
            nEnd = rtbShowResult.Find(Environment.NewLine.ToCharArray(), nStart + 1);
            rtbShowResult.Select(nStart, nEnd - nStart);
            rtbShowResult.SelectionFont = new Font(rtbShowResult.SelectionFont ?? rtbShowResult.Font, FontStyle.Underline | FontStyle.Bold);
        }

        // Bold results
        nStart = 0;
        while (true)
        {
            nStart = rtbShowResult.Find("The ", nStart + 1, -1, RichTextBoxFinds.MatchCase);
            if (nStart == -1) break;
            //nEnd = rtbShowResult.Text.Length;
            nEnd = rtbShowResult.Find(Environment.NewLine.ToCharArray(), nStart + 1);
            rtbShowResult.Select(nStart, nEnd - nStart);
            rtbShowResult.SelectionFont = new Font(rtbShowResult.SelectionFont?.FontFamily ?? rtbShowResult.Font.FontFamily, rtbShowResult.Font.Size + 1, FontStyle.Bold);
        }

        // Set the cursor at the beginning of the text
        rtbShowResult.SelectionStart = 0;
        rtbShowResult.SelectionLength = 0;
    }

    public void EditData()
    {
        throw new NotImplementedException();
    }

    public void Duplicate()
    {
        // Create a new instance of this form with the same data
        FrmResultsOCRAcheck frmResults = new(culture:_culture, model: Model)
        {
            MdiParent = this.MdiParent
        };

        int index = this.Text.IndexOf(StringResources.FormTitleUnion) > -1 ? this.Text.IndexOf(StringResources.FormTitleUnion) + StringResources.FormTitleUnion.Length : this.Text.Length;
        FrmMain.SetFormTitle(frmResults, StringResources.FormResultsOCRAchecklist, this.Text[index..]);

        frmResults.rtbShowResult.Font = this.rtbShowResult.Font;
        frmResults.rtbShowResult.ForeColor = this.rtbShowResult.ForeColor;
        frmResults.rtbShowResult.BackColor = this.rtbShowResult.BackColor;
        frmResults.rtbShowResult.ZoomFactor = this.rtbShowResult.ZoomFactor;
        frmResults.rtbShowResult.WordWrap = this.rtbShowResult.WordWrap;

        frmResults.Show();
    }    

    #endregion IChildResults

}
