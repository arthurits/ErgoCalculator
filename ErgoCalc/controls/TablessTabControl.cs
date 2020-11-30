using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace System.Windows.Forms
{
    //[System.ComponentModel.Designer("System.Windows.Forms.Design.DocumentDesigner, System.Windows.Forms.Design",
    //typeof(System.ComponentModel.Design.IRootDesigner)),
    //System.ComponentModel.DesignerCategory("")]
    /// <summary>
    /// TablessTabControl: shows tabs at design time and hides them at runtime
    /// https://github.com/coroiu/CustomControls/blob/master/CustomControls/TablessTabControl.cs
    /// </summary>
    public partial class TablessTabControl : TabControl
    {
        private bool _allowTabKey = true;
        public bool AllowTabKey { get { return _allowTabKey; } set { _allowTabKey = value; } }

        private bool _allowArrowKeys = true;
        public bool AllowArrowKeys { get { return _allowArrowKeys; } set { _allowArrowKeys = value; } }

        public TablessTabControl()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        protected override void WndProc(ref Message m)
        {
            // Hide tabs by trapping the TCM_ADJUSTRECT message
            if (m.Msg == 0x1328 && !DesignMode)
                m.Result = (IntPtr)1;
            else
                base.WndProc(ref m);
        }
        protected override void OnKeyDown(KeyEventArgs ke)
        {
            // Block Ctrl+Tab and Ctrl+Shift+Tab hotkeys
            if (ke.Control && ke.KeyCode == Keys.Tab && !_allowTabKey)
                return;
            else if ((ke.KeyCode == Keys.Left || ke.KeyCode == Keys.Right || ke.KeyCode == Keys.Up || ke.KeyCode == Keys.Down) && !_allowArrowKeys)
                ke.SuppressKeyPress = true;
            base.OnKeyDown(ke);
        }
    }
}
