using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
// Para guardar la posición en el archivo XML
//using System.Runtime.Serialization.Formatters.Soap;
using System.Text.Json;

// Namespaces personales
using Utilidades;

namespace ErgoCalc
{

    public partial class frmMain : Form
    {

        // For loading and saving program settings.
        //private ApplicationSettingsData _settings = new ApplicationSettingsData();
        private AppSettings _settings = new AppSettings();
        private static readonly string _settingsFileName = "Configuration.json";
        private clsApplicationSettings _programSettings;
        private static readonly string _programSettingsFileName = "Configuration.xml";
        
        //private ToolStripPanel tspTop;
        //private ToolStripPanel tspBottom;
        private readonly string _strPath = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);

        public frmMain()
        {
            // Load settings

            // Initilizate components and GUI
            InitializeComponent();
            InitializeToolStripPanel();
            InitializeMenu();
            InitializeToolStrip();

            // Set form icon
            if (File.Exists(_strPath + @"\images\logo.ico")) this.Icon = new Icon(_strPath + @"\images\logo.ico");
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

        #region Initialization routines

        /// <summary>
        /// Initialize the ToolStripPanel component: add the child components to it
        /// </summary>
        private void InitializeToolStripPanel()
        {
            //tspTop = new ToolStripPanel();
            //tspBottom = new ToolStripPanel();
            //tspTop.Join(toolStripMain);
            //tspTop.Join(mnuMainFrm);
            //tspBottom.Join(this.statusStrip);


            //tspTop = new ToolStripPanel();
            //tspBottom = new ToolStripPanel();
            //tspTop.Dock = DockStyle.Top;
            tspTop.Join(toolStripMain);
            tspTop.Join(mnuMainFrm);
            //tspBottom.Dock = DockStyle.Bottom;
            //this.Controls.Add(tspTop);
            //this.Controls.Add(tspBottom);


            // Exit the method
            return;
        }
        /// <summary>
        /// Set menu icons
        /// </summary>
        private void InitializeMenu()
        {
            if (File.Exists(_strPath + @"\images\new.ico")) this.mnuMainFrm_File_New.Image = new Icon(_strPath + @"\images\new.ico", 16, 16).ToBitmap();
            if (File.Exists(_strPath + @"\images\log_off.ico")) this.mnuMainFrm_File_Exit.Image = new Icon(_strPath + @"\images\log_off.ico", 16, 16).ToBitmap();
            if (File.Exists(_strPath + @"\images\about.ico")) this.mnuMainFrm_Help_About.Image = new Icon(_strPath + @"\images\about.ico", 16, 16).ToBitmap();
        }

        /// <summary>
        /// Initialize the ToolStrip component
        /// </summary>
        private void InitializeToolStrip()
        {
            toolStripMain.Renderer = new customRenderer(Brushes.SteelBlue, Brushes.LightSkyBlue);

            // Set ToolStrip icons
            if (File.Exists(_strPath + @"\images\log_off.ico")) this.toolStripMain_Exit.Image = new Icon(_strPath + @"\images\log_off.ico", 48, 48).ToBitmap();
            if (File.Exists(_strPath + @"\images\open.ico")) this.toolStripMain_Open.Image = new Icon(_strPath + @"\images\open.ico", 48, 48).ToBitmap();
            if (File.Exists(_strPath + @"\images\save.ico")) this.toolStripMain_Save.Image = new Icon(_strPath + @"\images\save.ico", 48, 48).ToBitmap();
            if (File.Exists(_strPath + @"\images\chart-save.ico")) this.toolStripMain_SaveChart.Image = new Icon(_strPath + @"\images\chart-save.ico", 48, 48).ToBitmap();
            //this.toolStripMain_SaveChart.Enabled = false;
            if (File.Exists(_strPath + @"\images\new.ico")) this.toolStripMain_New.Image = new Icon(_strPath + @"\images\new.ico", 48, 48).ToBitmap();
            if (File.Exists(_strPath + @"\images\copy.ico")) this.toolStripMain_Copy.Image = new Icon(_strPath + @"\images\copy.ico", 48, 48).ToBitmap();
            if (File.Exists(_strPath + @"\images\write.ico")) this.toolStripMain_EditData.Image = new Icon(_strPath + @"\images\write.ico", 48, 48).ToBitmap();
            if (File.Exists(_strPath + @"\images\chart-add.ico")) this.toolStripMain_AddLine.Image = new Icon(_strPath + @"\images\chart-add.ico", 48, 48).ToBitmap();
            //this.toolStripMain_AddLine.Enabled = false;
            if (File.Exists(_strPath + @"\images\chart-delete.ico")) this.toolStripMain_RemoveLine.Image = new Icon(_strPath + @"\images\chart-delete.ico", 48, 48).ToBitmap();
            //this.toolStripMain_RemoveLine.Enabled = false;
            if (File.Exists(_strPath + @"\images\settings.ico")) this.toolStripMain_Settings.Image = new Icon(_strPath + @"\images\settings.ico", 48, 48).ToBitmap();
            this.toolStripMain_Settings.CheckOnClick = true;
            //this.toolStripMain_Settings.Enabled = false;
            if (File.Exists(_strPath + @"\images\about.ico")) this.toolStripMain_About.Image = new Icon(_strPath + @"\images\about.ico", 48, 48).ToBitmap();

            ToolBarEnable();
            // https://stackoverflow.com/questions/6389722/toolstrip-with-check-button-group
        }

        #endregion Initialization routines

        #region Form events
        private void frmMain_Load(object sender, EventArgs e)
        {
            // Load any saved program settings.
            _programSettings = new clsApplicationSettings(_programSettingsFileName);
            //LoadProgramSettingsXML();
            LoadProgramSettingsJSON();
            
            // Creates the fade in animation of the form
            Win32.Win32API.AnimateWindow(this.Handle, 200, Win32.Win32API.AnimateWindowFlags.AW_BLEND | Win32.Win32API.AnimateWindowFlags.AW_CENTER);
            
            // Close splash screen
            //if (_frmSplash != null) _frmSplash.Close();

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

        #region Main menu

        private void mnuMainFrm_File_New_Click(object sender, EventArgs e)
        {
            frmNew frmNew = new frmNew();
            Form frmData = new Form();
            Form frmResults = new Form();
            if (frmNew.ShowDialog() == DialogResult.Cancel) return;

            switch (frmNew.Model)
            {
                case 1: // WR model

                    // Llamar al formulario para introducir los datos
                    frmDataWRmodel frmDatosWR = new frmDataWRmodel();
                    if (frmDatosWR.ShowDialog(this) == DialogResult.OK)
                    {
                        // Mostrar la ventana de resultados
                        frmWRmodel frmWR = new frmWRmodel(frmDatosWR.GetData);
                        frmWR.MdiParent = this;
                        if (File.Exists(_strPath + @"\images\logo.ico")) frmWR.Icon = new Icon(_strPath + @"\images\logo.ico");
                        frmWR.Show();
                        
                        // Cerrar el formulario de entrada de datos
                        frmDatosWR.Dispose();
                    }
                    
                    break;
                    
                case 2: // CLM model

                    // Llamar al formulario para introducir los datos y cargar un ejemplo
                    frmDataCLMmodel frmDatosCLM = new frmDataCLMmodel();
                    frmDatosCLM.LoadExample();

                    if (frmDatosCLM.ShowDialog(this)==DialogResult.OK)
                    {
                        // Mostrar la ventaja de resultados
                        frmCLMmodel frmCLM = new frmCLMmodel(frmDatosCLM.getData());
                        frmCLM.MdiParent = this;
                        if (File.Exists(_strPath + @"\images\logo.ico")) frmCLM.Icon = new Icon(_strPath + @"\images\logo.ico");
                        frmCLM.Show();

                        // Cerrar el formulario de entrada de datos
                        frmDatosCLM.Dispose();
                    }

                    break;

                case 3: // NIOSH model

                    // Llamar al formulario para introducir los datos y cargar un ejemplo
                    frmDataNIOSHmodel frmDatosNIOSH = new frmDataNIOSHmodel();
                    frmDatosNIOSH.LoadExample();

                    if (frmDatosNIOSH.ShowDialog(this)==DialogResult.OK)
                    {
                        // Mostrar la ventana de resultados
                        frmResultNIOSHmodel frmNIOSH = new frmResultNIOSHmodel(frmDatosNIOSH.GetData, frmDatosNIOSH.GetComposite);
                        frmNIOSH.MdiParent = this;
                        if (File.Exists(_strPath + @"\images\logo.ico")) frmNIOSH.Icon = new Icon(_strPath + @"\images\logo.ico");
                        frmNIOSH.Show();

                        // Cerrar el formulario de entrada de datos
                        frmDatosNIOSH.Dispose();
                    }
                    
                    break;

                case 4: // Revised strain index

                    // Llamar al formulario para introducir los datos
                    frmDataStrainIndex frmDataStrain = new frmDataStrainIndex();
                    frmDataStrain.LoadExample();

                    if (frmDataStrain.ShowDialog(this)==DialogResult.OK)
                    {
                        // Mostrar la ventana de resultados
                        frmResultsStrainIndex frmStrainIndex = new frmResultsStrainIndex(frmDataStrain.Job);
                        frmStrainIndex.MdiParent = this;
                        //if (File.Exists(_strPath + @"\images\logo.ico")) frmStrainIndex.Icon = new Icon(_strPath + @"\images\logo.ico");
                        frmStrainIndex.Show();
                    }
                    // Cerrar el formulario de entrada de datos
                    frmDataStrain.Dispose();
                    break;
                
                case 5: // OCRA checklist
                    frmData = new frmDataOCRAcheck();
                    frmResults = new frmResultsOCRAcheck(((IChildData)frmData).GetData);
                    break;

                case 6: // Metabolic rate

                    // Llamar al formulario para introducir los datos
                    frmMet frmMet = new frmMet();
                    if (frmMet.ShowDialog(this) == DialogResult.OK)
                    {
                        // Recuperar los datos introducidos por el usuario
                        Int32[] nDatos = frmMet.getData();
                        frmMet.Dispose();
                        

                        // Mostrar el formulario con los resultados del cálculo
                        frmMetResult frmResult = new frmMetResult(nDatos);
                        frmResult.MdiParent = this;
                        if (File.Exists(_strPath + @"\images\logo.ico")) frmResult.Icon = new Icon(_strPath + @"\images\logo.ico");
                        frmResult.Show();
                    }
                    break;

                case 7: // Thermal comfort
                    frmData = new frmDataTC();
                    if (frmData.ShowDialog(this) == DialogResult.OK)
                    {
                        // Mostrar la ventana de resultados
                        frmResults = new frmResultsTC(((IChildData)frmData).GetData);
                        frmResults.MdiParent = this;
                        frmResults.Show();
                    }
                    break;
                case 8: // Liberty mutual 
                    frmData = new frmDataLiberty();
                    if (frmData.ShowDialog(this) == DialogResult.OK)
                    {
                        // Mostrar la ventana de resultados
                        frmResults = new frmResultsLiberty(((IChildData)frmData).GetData);
                        frmResults.MdiParent = this;
                        frmResults.Show();
                    }
                    break;
            }

            
            //if (frmData.ShowDialog(this) == DialogResult.OK)
            //{
            //    frmResults.MdiParent = this;
            //    frmResults.Show();
            //}

            frmData.Dispose();
            
        }

        private void mnuMainFrm_File_Exit_Click(object sender, EventArgs e)
        {
            // Exit the application by calling the frmMain_FormClosing event
            this.Close();
        }

        private void mnuMainFrm_Help_About_Click(object sender, EventArgs e)
        {
            frmAbout frmAbout = new frmAbout();
            frmAbout.ShowDialog();
        }

        #endregion Main menu

        #region Application settings

        /// <summary>
        /// Loads any saved program settings.
        /// </summary>
        private void LoadProgramSettingsXML()
        {
            // Load the saved window settings and resize the window.
            try
            {
                // Load the saved window settings.
                Int32 left = System.Int32.Parse(_programSettings.GetValue("Window", "Left"));
                Int32 top = System.Int32.Parse(_programSettings.GetValue("Window", "Top"));
                Int32 width = System.Int32.Parse(_programSettings.GetValue("Window", "Width"));
                Int32 height = System.Int32.Parse(_programSettings.GetValue("Window", "Height"));

                // Reposition and resize the window.
                //this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
                this.StartPosition = FormStartPosition.Manual;
                this.DesktopLocation = new Point(left, top);
                this.ClientSize = new Size(width, height);

                // Load saved options.
                //_idioma = (Language)Int32.Parse(_programSettings.GetValue("Options", "LanguageLCID"));
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

        private void LoadProgramSettingsJSON()
        {
            try
            {
                var jsonString = File.ReadAllText(_settingsFileName);
                _settings = JsonSerializer.Deserialize<AppSettings>(jsonString);

                this.StartPosition = FormStartPosition.Manual;
                this.DesktopLocation = new Point(_settings.Left, _settings.Top);
                this.ClientSize = new Size(_settings.Width, _settings.Height);
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
        private void SaveProgramSettingsXML()
        {
            // Save window settings.      
            _programSettings.SetValue("Window", "Left", this.DesktopLocation.X.ToString());
            _programSettings.SetValue("Window", "Top", this.DesktopLocation.Y.ToString());
            _programSettings.SetValue("Window", "Width", this.ClientSize.Width.ToString());
            _programSettings.SetValue("Window", "Height", this.ClientSize.Height.ToString());

            // Save options.
            //_programSettings.SetValue("Options", "LanguageLCID", ((Int32)_idioma).ToString());

            // Save the program settings.
            _programSettings.Save();
        }

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
            if (ActiveMdiChild is frmWRmodel)
            {
                // The frmChild.FormToolStrip is a property that exposes the
                // toolstrip on your child form
                // ToolStripManager.Merge(frmChild.toolStripWR, this.toolStripMain);
                ToolStripManager.Merge(((frmWRmodel)ActiveMdiChild).ChildToolStrip, this.toolStripMain);
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

        private void toolStripMain_Exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        #region toolStripMain events

        private void toolStripMain_New_Click(object sender, EventArgs e)
        {
            mnuMainFrm_File_New_Click(null, null);
        }

        private void toolStripMain_Open_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDlg = new OpenFileDialog
            {
                DefaultExt = "*.ergo",
                Filter = "ERGO file (*.ergo)|*.ergo|All files (*.*)|*.*",
                FilterIndex = 1,
                Title = "Open ErgoCalc file",
                InitialDirectory = _strPath + @"\Examples"
            };

            DialogResult result;
            using (new CenterWinDialog(this))
            {
                result = openDlg.ShowDialog(this);
            }

            // If the file name is not an empty string open it for saving.  
            if (result == DialogResult.OK && openDlg.FileName != "")
            {
                // https://stackoverflow.com/questions/897796/how-do-i-open-an-already-opened-file-with-a-net-streamreader
                using var fs = File.Open(openDlg.FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                using var sr = new StreamReader(fs, Encoding.UTF8);

                string jsonString = sr.ReadToEnd();

                //string jsonString = File.ReadAllText(openDlg.FileName);
                var options = new JsonDocumentOptions { AllowTrailingCommas = true, CommentHandling = JsonCommentHandling.Skip };

                // Dilemma: this should be wrapped in a try-catch, but variables will be out of scope and syntax would be cumbersome
                using JsonDocument document = JsonDocument.Parse(jsonString, options);
                var strType = document.RootElement.TryGetProperty("Document type", out JsonElement docuValue) ? docuValue.ToString() : "Error";

                Form frm = default;

                switch (strType)
                {
                    case "Work-Rest model":
                        frm = new frmWRmodel() { MdiParent = this };
                        break;
                    case "NIOSH lifting equation":
                        frm = new frmResultNIOSHmodel() { MdiParent = this };
                        break;
                    case "Strain index":
                        frm = new frmResultsStrainIndex() { MdiParent = this };
                        break;
                    case "Thermal comfort model":
                        frm = new frmResultsTC() { MdiParent = this };
                        break;
                    case "LM-MMH model":
                        frm = new frmResultsLiberty() { MdiParent = this };
                        break;
                }

                if(frm != default)
                {
                    if (((IChildResults)frm).OpenFile(document))
                    {
                        if (File.Exists(_strPath + @"\images\logo.ico")) frm.Icon = new Icon(_strPath + @"\images\logo.ico");
                        frm.Show();
                    }
                    else
                    {
                        MessageBox.Show("The document cannot be opened by this application", "Format mismatch", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }

            return;
        }

        private void toolStripMain_Save_Click(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild != null)
                ((IChildResults)this.ActiveMdiChild).Save("");
        }

        private void toolStripMain_Copy_Click(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild != null)
                ((IChildResults)this.ActiveMdiChild).Duplicate();
        }

        private void toolStripMain_EditData_Click(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild != null)
                ((IChildResults)this.ActiveMdiChild).EditData();
        }

        private void toolStripMain_Settings_Click(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild != null)
                ((IChildResults)this.ActiveMdiChild).ShowHideSettings();
        }

        private void toolStripMain_Settings_EnabledChanged(object sender, EventArgs e)
        {
            if (this.toolStripMain_Settings.Enabled == false) this.toolStripMain_Settings.Checked = false;
        }

        private void toolStripMain_About_Click(object sender, EventArgs e)
        {
            mnuMainFrm_Help_About_Click(null, null);
        }

        #endregion toolStripMain events



        #region Private routines

        private void ToolBarEnable(bool[] values)
        {
            for (int i =0;i<toolStripMain.Items.Count;i++)
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
        
    }

}
