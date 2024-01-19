using System.Reflection;

namespace ErgoCalc
{
    partial class FrmAbout : Form
    {

        

        public FrmAbout()
        {
            InitializeComponent();
            // this.Text = String.Format("About {0}", AssemblyTitle);
            this.labelProductName.Text = AssemblyProduct;
            this.labelVersion.Text = String.Format("Version {0}", AssemblyVersion);
            this.labelCopyright.Text = AssemblyCopyright;
            this.labelCompanyName.Text = AssemblyCompany;
            this.textBoxDescription.Text = AssemblyDescription;
            
            // Set form icons and images
            this.logoPictureBox.Image = GraphicsResources.Load<Image>(GraphicsResources.AppLogo256);
        }

        //[System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        protected override void WndProc(ref Message m)
        {
            const int WM_PARENTNOTIFY = 0x210;
            const int WM_LBUTTONDOWN = 0x201;
            //const int WM_LBUTTONUP = 0x202;
            //const int WM_KEYDOWN = 0x100;
            const int WM_KEYUP = 0x101;
            const int VK_ESCAPE = 0x1B;
            const int VK_RETURN = 0x0D;

            // https://stackoverflow.com/questions/27646476/how-to-fire-form-click-event-even-when-clicking-on-user-controls-in-c-sharp
            if (m.Msg == WM_PARENTNOTIFY && m.WParam.ToInt32() == WM_LBUTTONDOWN)
            {
                this.Close();
                /*
                // get the clicked position
                var x = (int)(m.LParam.ToInt32() & 0xFFFF);
                var y = (int)(m.LParam.ToInt32() >> 16);

                // get the clicked control
                var childControl = this.GetChildAtPoint(new Point(x, y));

                // call onClick (which fires Click event)
                OnClick(EventArgs.Empty)
                */
                // do something else...
            }
            else if (m.Msg == WM_KEYUP && ((m.WParam.ToInt32() == VK_ESCAPE) | (m.WParam.ToInt32() == VK_RETURN)))
                this.Close();

            base.WndProc(ref m);
        }
    }
}
