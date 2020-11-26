using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Security;
using System.Runtime.Versioning;
using System.Diagnostics.CodeAnalysis;

namespace EjemploTool
{
    // This code example demonstrates an MDI form 
    // that supports menu merging and moveable 
    // ToolStrip controls
    public partial class Form1 : Form
    {
        private MenuStrip menuStrip1;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem newToolStripMenuItem;
        private ToolStripPanel toolStripPanel1;
        private ToolStrip toolStrip1;
        private ToolStripPanel toolStripPanel2;
        private ToolStrip toolStrip2;
        private ToolStripPanel toolStripPanel3;
        private ToolStrip toolStrip3;
        private ToolStripPanel toolStripPanel4;
        private ToolStrip toolStrip4;



        public Form1()
        {
            InitializeComponent();
        }


        // This method creates a new ChildForm instance 
        // and attaches it to the MDI parent form.
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChildForm f = new ChildForm
            {
                MdiParent = this,
                Text = "Form - " + this.MdiChildren.Length.ToString()
            };
            f.Show();
        }

        private void Form1_MdiChildActivate(object sender, EventArgs e)
        {
            ToolStripManager.RevertMerge(this.toolStrip2);
            if (ActiveMdiChild is ChildForm frmChild)
            {
                // The frmChild.FormToolStrip is a property that exposes the
                // toolstrip on your child form
                ToolStripManager.Merge(frmChild.ChildtoolStrip1, this.toolStrip2);
            }
        }

        private void msgBoxToolStripMenuItem_Click(object sender, EventArgs e)
        {

            DialogResult dialogResult = MessageBox.Show(this, "Ejemplo");

            UnsafeNativeMethods.SetParent(new HandleRef(null, UnsafeNativeMethods.GetActiveWindow()), new HandleRef(this, Handle));

            UnsafeNativeMethods.SetWindowLong(new HandleRef(this, UnsafeNativeMethods.GetActiveWindow()), -8, new HandleRef(this, Handle));

        }
    }

    public class ChildForm : Form
    {
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.ComponentModel.IContainer components = null;
        //private ToolStripPanel ChildtoolStripPanel1;
        public ToolStrip ChildtoolStrip1;
        private System.Windows.Forms.ToolStripButton ayudaToolStripButton;
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));


        public ChildForm()
        {
            InitializeComponent();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            //this.ChildtoolStripPanel1 = new System.Windows.Forms.ToolStripPanel();
            //this.ChildtoolStripPanel1.SuspendLayout();
            this.ChildtoolStrip1 = new System.Windows.Forms.ToolStrip();
            this.ayudaToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(292, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.Visible = false;
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(90, 120);
            this.toolStripMenuItem1.Text = "ChildMenuItem";
            // 
            // toolStripPanel1
            // 
            /*this.ChildtoolStripPanel1.Controls.Add(this.ChildtoolStrip1);
            this.ChildtoolStripPanel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.ChildtoolStripPanel1.Location = new System.Drawing.Point(0, 49);
            this.ChildtoolStripPanel1.Name = "toolStripPanel1";
            this.ChildtoolStripPanel1.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.ChildtoolStripPanel1.RowMargin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.ChildtoolStripPanel1.Size = new System.Drawing.Size(126, 299);*/
            // 
            // toolStrip1
            // 
            this.ChildtoolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.ChildtoolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ayudaToolStripButton});
            this.ChildtoolStrip1.Location = new System.Drawing.Point(100, 103);
            this.ChildtoolStrip1.Name = "ChildtoolStrip1";
            this.ChildtoolStrip1.Size = new System.Drawing.Size(126, 209);
            this.ChildtoolStrip1.TabIndex = 0;
            this.ChildtoolStrip1.AllowMerge = true;
            // 
            // ayudaToolStripButton
            // 
            this.ayudaToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ayudaToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("ayudaToolStripButton.Image")));
            this.ayudaToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ayudaToolStripButton.Name = "ayudaToolStripButton";
            this.ayudaToolStripButton.Size = new System.Drawing.Size(52, 52);
            this.ayudaToolStripButton.Text = "Ay&uda";
            // 
            // ChildForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ChildForm";
            this.Text = "ChildForm";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }


    [SuppressUnmanagedCodeSecurity]
    internal static class UnsafeNativeMethods
    {
        [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern IntPtr SetParent(HandleRef hWnd, HandleRef hWndParent);

        [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.Process)]
        public static extern IntPtr GetActiveWindow();



        [SuppressMessage("Microsoft.Portability", "CA1901:PInvokeDeclarationsShouldBePortable")]
        [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "GetWindowLong")]
        [ResourceExposure(ResourceScope.None)]
        internal static extern IntPtr GetWindowLong32(HandleRef hWnd, int nIndex);

        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "GetWindowLongPtr")]
        [ResourceExposure(ResourceScope.None)]
        internal static extern IntPtr GetWindowLongPtr64(HandleRef hWnd, int nIndex);

        //SetWindowLong won't work correctly for 64-bit: we should use SetWindowLongPtr instead.  On
        //32-bit, SetWindowLongPtr is just #defined as SetWindowLong.  SetWindowLong really should 
        //take/return int instead of IntPtr/HandleRef, but since we're running this only for 32-bit
        //it'll be OK.
        public static IntPtr SetWindowLong(HandleRef hWnd, int nIndex, HandleRef dwNewLong)
        {
            if (IntPtr.Size == 4)
            {
                return SetWindowLongPtr32(hWnd, nIndex, dwNewLong);
            }
            return SetWindowLongPtr64(hWnd, nIndex, dwNewLong);
        }
        [SuppressMessage("Microsoft.Portability", "CA1901:PInvokeDeclarationsShouldBePortable")]
        [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "SetWindowLong")]
        [ResourceExposure(ResourceScope.None)]
        public static extern IntPtr SetWindowLongPtr32(HandleRef hWnd, int nIndex, HandleRef dwNewLong);
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "SetWindowLongPtr")]
        [ResourceExposure(ResourceScope.None)]
        public static extern IntPtr SetWindowLongPtr64(HandleRef hWnd, int nIndex, HandleRef dwNewLong);


        public static IntPtr SetWindowLong(HandleRef hWnd, int nIndex, WndProc wndproc)
        {
            if (IntPtr.Size == 4)
            {
                return SetWindowLongPtr32(hWnd, nIndex, wndproc);
            }
            return SetWindowLongPtr64(hWnd, nIndex, wndproc);
        }
        [SuppressMessage("Microsoft.Portability", "CA1901:PInvokeDeclarationsShouldBePortable")]
        [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "SetWindowLong")]
        [ResourceExposure(ResourceScope.None)]
        public static extern IntPtr SetWindowLongPtr32(HandleRef hWnd, int nIndex, WndProc wndproc);
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "SetWindowLongPtr")]
        [ResourceExposure(ResourceScope.None)]
        public static extern IntPtr SetWindowLongPtr64(HandleRef hWnd, int nIndex, WndProc wndproc);

        public delegate IntPtr WndProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

    }
}
