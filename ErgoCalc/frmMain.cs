using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
// Para guardar la posición en el archivo XML
//using System.Runtime.Serialization.Formatters.Soap;
using System.Text.Json;
using ErgoCalc.models;

namespace ErgoCalc
{

    public partial class FrmMain : Form
    {

        // For loading and saving program settings.
        //private ApplicationSettingsData _settings = new ApplicationSettingsData();
        private AppSettings _settings = new();
        private static readonly string _settingsFileName = "Configuration.json";
        //private clsApplicationSettings _programSettings;
        //private static readonly string _programSettingsFileName = "Configuration.xml";

        //private ToolStripPanel tspTop;
        //private ToolStripPanel tspBottom;
        private readonly string _strPath = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);

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


        #region Form events
        private void frmMain_Load(object sender, EventArgs e)
        {
            // Creates the fade in animation of the form
            Win32.Win32API.AnimateWindow(this.Handle, 200, Win32.Win32API.AnimateWindowFlags.AW_BLEND | Win32.Win32API.AnimateWindowFlags.AW_CENTER);
        }

        private void frmMain_Shown(object sender, EventArgs e)
        {
            // signal the native process (that launched us) to close the splash screen
            using var closeSplashEvent = new System.Threading.EventWaitHandle(false, System.Threading.EventResetMode.ManualReset, "CloseSplashScreenEvent");
            closeSplashEvent.Set();
        }

        private void frmMain_ControlAdded(object sender, ControlEventArgs e)
        {
            //if (e.Control is MdiClient)
        }

        private void frmMain_ControlRemoved(object sender, ControlEventArgs e)
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
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            using (new CenterWinDialog(this))
            {
                if (DialogResult.No == MessageBox.Show(this,
                                                        "Are you sure you want to exit\nthe application?",
                                                        "Exit?",
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

            // Guardar los datos de configuración
            //SaveProgramSettingsXML();
            SaveProgramSettingsJSON();

        }
        #endregion Form events


        #region Application settings

        /// <summary>
        /// Loads any saved program settings.
        /// </summary>
         private void LoadProgramSettingsJSON()
        {
            try
            {
                var jsonString = File.ReadAllText(_settingsFileName);
                _settings = JsonSerializer.Deserialize<AppSettings>(jsonString);

                if (_settings.WindowPosition)
                {
                    this.StartPosition = FormStartPosition.Manual;
                    this.DesktopLocation = new Point(_settings.Left, _settings.Top);
                    this.ClientSize = new Size(_settings.Width, _settings.Height);
                }
            }
            catch (FileNotFoundException)
            {
            }
            catch (Exception ex)
            {
                using (new CenterWinDialog(this))
                {
                    MessageBox.Show(this,
                        "Error loading settings file\n\n" + ex.Message,
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }
        /// <summary>
        /// Saves the current program settings.
        /// </summary>
        private void SaveProgramSettingsJSON()
        {
            _settings.Left = DesktopLocation.X;
            _settings.Top = DesktopLocation.Y;
            _settings.Width = ClientSize.Width;
            _settings.Height = ClientSize.Height;

            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            var jsonString = JsonSerializer.Serialize(_settings, options);
            File.WriteAllText(_settingsFileName, jsonString);
        }


        #endregion Application settings

        private void frmMain_MdiChildActivate(object sender, EventArgs e)
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
            ToolBarEnable(new bool[] { true, true, false, false, true, true, false, false, true, false, false, false, true, true });
        }

        #endregion

        /// <summary>
        /// Sets the form's title
        /// </summary>
        /// <param name="frm">Form which title is to be set</param>
        /// <param name="strFileName">String to be added at the default title in 'strFormTitle' string.
        /// If <see langword="null"/>, no string is added.
        /// If <see cref="String.Empty"/>, the current added text is mantained.
        /// Other values are added to the default title.</param>
        private void SetFormTitle(System.Windows.Forms.Form frm, string? strFileName = null)
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
            frm.Text = StringResources.FormMainTitle + strText;
        }

        private void UpdateUI_Language(int DataLength = default)
        {
            this.SuspendLayout();

            StringResources.Culture = _settings.AppCulture;

            // Update the form's tittle
            SetFormTitle(this, String.Empty);

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


            statusStripLabelUILanguage.Text = _settings.AppCulture.Name == String.Empty ? "Invariant" : _settings.AppCulture.Name;
            statusStripLabelUILanguage.ToolTipText = StringResources.ToolTipUILanguage + ":" + Environment.NewLine + _settings.AppCulture.NativeName;

            this.ResumeLayout();
        }

    }
}