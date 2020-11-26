using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ErgoCalc
{
    [System.ComponentModel.Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design",
        typeof(System.ComponentModel.Design.IDesigner))]
    public class ListViewExt : System.Windows.Forms.ListView
    {
        public ListViewExt()
        {
            this.AutoSize = false;
        }
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        public void InitializeComponent()
        {
        }
        public void RemoveGroup()
        {

        }
        public void DeleteEmptyItems()
        {

        }
        public void AddGroup(int i)
        {

        }
    }
}
