using System;
using System.Drawing;
using System.Windows.Forms;

namespace ErgoCalc;

public partial class FrmMain : Form
{
    // For loading and saving program settings.a();
    private AppSettings _settings = new();

    public FrmMain()
    {
        // Load settings
        LoadProgramSettingsJSON();

        // Initilizate components and GUI
        InitializeComponent();
        InitializeToolStripPanel();
        InitializeMenu();
        InitializeToolStrip();

        // Set form icon
        this.Icon = GraphicsResources.Load<Icon>(GraphicsResources.AppLogo);

        UpdateUI_Language();
    }

    #region Form events
    private void FrmMain_Load(object sender, EventArgs e)
    {
        // Creates the fade in animation of the form
        Win32.Win32API.AnimateWindow(this.Handle, 200, Win32.Win32API.AnimateWindowFlags.AW_BLEND | Win32.Win32API.AnimateWindowFlags.AW_CENTER);
    }

    private void FrmMain_Shown(object sender, EventArgs e)
    {
        // signal the native process (that launched us) to close the splash screen
        using var closeSplashEvent = new System.Threading.EventWaitHandle(false, System.Threading.EventResetMode.ManualReset, "CloseSplashScreenEvent");
        closeSplashEvent.Set();
    }

    private void FrmMain_ControlAdded(object sender, ControlEventArgs e)
    {
        //if (e.Control is MdiClient)
    }

    private void FrmMain_ControlRemoved(object sender, ControlEventArgs e)
    {
        if (e.Control is MdiClient)
        {
            if (this.MdiChildren.Length == 0)
            {
                toolStripMain.Items["Save"].Enabled = false;
                //Reset toolbar visibility
            }
        }
    }
    private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
    {
        using (new CenterWinDialog(this))
        {
            if (DialogResult.No == MessageBox.Show(this,
                                    StringResources.MsgBoxExit,
                                    StringResources.MsgBoxExitTitle,
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Question,
                                    MessageBoxDefaultButton.Button2))
            {
                // Cancelar el cierre de la ventana
                e.Cancel = true;
            }
            else
                Win32.Win32API.AnimateWindow(this.Handle, 200, Win32.Win32API.AnimateWindowFlags.AW_BLEND | Win32.Win32API.AnimateWindowFlags.AW_HIDE);
        }

        // Save settings
        SaveProgramSettingsJSON();

    }

    private void FrmMain_MdiChildActivate(object sender, EventArgs e)
    {
        ToolStripManager.RevertMerge(this.toolStripMain);
        if (ActiveMdiChild is FrmResultsWR form)
        {
            // The frmChild.FormToolStrip is a property that exposes the
            // toolstrip on your child form
            // ToolStripManager.Merge(frmChild.toolStripWR, this.toolStripMain);
            ToolStripManager.Merge(form.ChildToolStrip, this.toolStripMain);
            //this.Controls.Add(frmChild.toolStripWR);
            //((frmWRmodel)ActiveMdiChild).toolStripWR.Visible = true;
            //this.tspTop.Join(((frmWRmodel)ActiveMdiChild).toolStripWR, 1);

        }

        /*if (ActiveMdiChild is frmWRmodel frmChild)
        {
            // The frmChild.FormToolStrip is a property that exposes the
            // toolstrip on your child form
            ToolStripManager.Merge(frmChild.toolStripWR, this.toolStripMain);
            //this.Controls.Add(frmChild.toolStripWR);
            //frmChild.toolStripWR.Visible = true;
            //this.tspTop.Join(frmChild.toolStripWR, 1);

        }*/
        //if (ActiveMdiChild is frmResultNIOSHmodel frmChildNIOSH) ToolStripManager.Merge(frmChildNIOSH.toolStripNIOSH, this.toolStripMain);


        if (this.ActiveMdiChild is IChildResults)
        {
            if (this.MdiChildren.Length == 1)   // If we are down to the last child window
            {
                if (this.MdiChildren[0].Disposing || this.MdiChildren[0].IsDisposed)
                {
                    //toolStripMain.Items["toolStripMain_Settings"].Enabled = false;
                    this.ToolBarEnable();
                    //MessageBox.Show("Cerrando última ventana");
                }
                else
                {
                    this.ToolBarEnable(((IChildResults)this.ActiveMdiChild).GetToolbarEnabledState());
                    this.toolStripMain_Settings.Checked = !((IChildResults)this.ActiveMdiChild).PanelCollapsed();
                }

            }
            else
            {
                this.ToolBarEnable(((IChildResults)this.ActiveMdiChild).GetToolbarEnabledState());
                this.toolStripMain_Settings.Checked = !((IChildResults)this.ActiveMdiChild).PanelCollapsed();
            }

        }
        else // this should be impossible
        {
            this.ToolBarEnable();
        }
        //https://stackoverflow.com/questions/10011600/how-to-recognize-the-active-child-form-and-fire-a-common-method-which-exist-in

    }

    #endregion Form events


    #region Private routines

    private void ToolBarEnable(bool[] values)
    {
        for (int i = 0; i < toolStripMain.Items.Count; i++)
        {
            this.toolStripMain.Items[i].Enabled = values[i];
        }

    }
    private void ToolBarEnable()
    {
        // This is the default ToolBar enable status
        ToolBarEnable(new bool[] { true, true, false, false, true, true, false, false, true, false, false, true, true, true });
    }

    /// <summary>
    /// Sets the form's title
    /// </summary>
    /// <param name="frm">Form which title is to be set</param>
    /// <param name="strTextTitle">Base text to be shown as the form's title</param>
    /// <param name="strFileName">String to be added at the default title in 'strFormTitle' string.
    /// If <see langword="null"/>, no string is added.
    /// If <see cref="String.Empty"/>, the current added text is mantained.
    /// Other values are added to the default title.</param>
    public static void SetFormTitle(System.Windows.Forms.Form frm, string strTextTitle, string? strFileName = null)
    {
        string strText = String.Empty;
        string strSep = StringResources.FormTitleUnion;
        if (strFileName is not null)
        {
            if (strFileName != String.Empty)
                strText = $"{strSep}{strFileName}";
            else
            {
                int index = frm.Text.IndexOf(strSep) > -1 ? frm.Text.IndexOf(strSep) : frm.Text.Length;
                strText = frm.Text[index..];
            }
        }
        frm.Text = strTextTitle + strText;
    }

    private void UpdateUI_Language()
    {
        this.SuspendLayout();

        StringResources.Culture = _settings.AppCulture;

        // Update the form's tittle
        SetFormTitle(this, StringResources.FormMainTitle, String.Empty);

        mnuMainFrm_File.Text = StringResources.MenuMainFile;
        mnuMainFrm_File_Exit.Text = StringResources.MenuMainFileExit;
        mnuMainFrm_File_New.Text = StringResources.MenuMainFileNew;
        mnuMainFrm_Window.Text = StringResources.MenuMainWindow;
        mnuMainFrm_Help.Text=StringResources.MenuMainHelp;
        mnuMainFrm_Help_About.Text = StringResources.MenuMainHelpAbout;

        toolStripMain_About.Text=StringResources.ToolStripAbout;
        toolStripMain_AddLine.Text=StringResources.ToolStripAddLine;
        toolStripMain_Copy.Text = StringResources.ToolStripDuplicate;
        toolStripMain_EditData.Text = StringResources.ToolStripEdit;
        toolStripMain_Exit.Text = StringResources.ToolStripExit;
        toolStripMain_New.Text = StringResources.ToolStripNew;
        toolStripMain_Open.Text = StringResources.ToolStripOpen;
        toolStripMain_RemoveLine.Text = StringResources.ToolStripRemoveLine;
        toolStripMain_Save.Text = StringResources.ToolStripSave;
        toolStripMain_SaveChart.Text = StringResources.ToolStripSaveChart;
        toolStripMain_Settings.Text = StringResources.ToolStripSettings;

        toolStripMain_About.ToolTipText = StringResources.ToolTipAbout;
        toolStripMain_AddLine.ToolTipText = StringResources.ToolTipAddLine;
        toolStripMain_Copy.ToolTipText = StringResources.ToolTipDuplicate;
        toolStripMain_EditData.ToolTipText = StringResources.ToolTipEdit;
        toolStripMain_Exit.ToolTipText = StringResources.ToolTipExit;
        toolStripMain_New.ToolTipText = StringResources.ToolTipNew;
        toolStripMain_Open.ToolTipText = StringResources.ToolTipOpen;
        toolStripMain_RemoveLine.ToolTipText= StringResources.ToolTipRemoveLine;
        toolStripMain_Save.ToolTipText = StringResources.ToolTipSave;
        toolStripMain_SaveChart.ToolTipText = StringResources.ToolTipSaveChart;
        toolStripMain_Settings.ToolTipText = StringResources.ToolTipSettings;


        statusStripLabelCulture.Text = _settings.AppCulture.Name == String.Empty ? "Invariant" : _settings.AppCulture.Name;
        statusStripLabelCulture.ToolTipText = StringResources.ToolTipUILanguage + ":" + Environment.NewLine + _settings.AppCulture.NativeName;

        this.ResumeLayout();
    }

    #endregion Private routines

}



/*
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern int GetClassName(IntPtr hWnd, StringBuilder buffer, int buflen);
    [DllImport("user32.dll", SetLastError = true)]
    private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
    [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
    private static extern IntPtr GetParent(IntPtr hWnd);

    
    [System.Security.Permissions.SecurityPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Flags = System.Security.Permissions.SecurityPermissionFlag.UnmanagedCode)]
    protected override void WndProc(ref Message m)
    {
        const int WM_NOTIFY = 0x4E;
        const int WM_MOVE = 0x03;
        const int WM_REFLECT = 0x2000;
        const int WM_ACTIVATE = 0x0006;
        const int WM_WINDOWPOSCHANGING = 0x0046;

        StringBuilder sb = new StringBuilder(260);
        GetClassName(m.HWnd, sb, sb.Capacity);
        //MessageBox.Show(m.Msg.ToString());
        if (sb.ToString() == "#32770")
            MessageBox.Show("Diálogo");

        //if (m.Msg==WM_ACTIVATE)
            //MessageBox.Show("Diálogo activate");

        switch (m.Msg)
        {
            case WM_NOTIFY:
                
                //if (GetParent(FindWindow("#32770", String.Empty)) == this.Handle)
                    if (FindWindow("#32770", String.Empty) != IntPtr.Zero)
                        MessageBox.Show("Diálogo WM_NOTIFY");

                GetClassName(m.HWnd, sb, sb.Capacity);
                if (sb.ToString() == "#32770")
                    MessageBox.Show("Diálogo");
                break;
            case WM_ACTIVATE:
                if (FindWindow("#32770", String.Empty) != IntPtr.Zero)
                    MessageBox.Show("Diálogo activate MsgBox");
                break;
        }
                base.WndProc(ref m);

    }
    */