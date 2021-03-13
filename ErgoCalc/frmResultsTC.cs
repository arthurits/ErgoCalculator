using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ErgoCalc
{
    public partial class frmResultsTC : Form, IChildResults
    {
        public frmResultsTC()
        {
            InitializeComponent();
        }

        private void frmResultsTC_Shown(object sender, EventArgs e)
        {

        }

        #region IChildResults interface
        public ToolStrip ChildToolStrip { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void Duplicate()
        {
            throw new NotImplementedException();
        }

        public void EditData()
        {
            throw new NotImplementedException();
        }

        public void FormatText()
        {
            throw new NotImplementedException();
        }

        public bool[] GetToolbarEnabledState()
        {
            throw new NotImplementedException();
        }

        public bool OpenFile(JsonDocument document)
        {
            throw new NotImplementedException();
        }

        public bool PanelCollapsed()
        {
            throw new NotImplementedException();
        }

        public void Save(string path)
        {
            throw new NotImplementedException();
        }

        public void ShowHideSettings()
        {
            throw new NotImplementedException();
        }

        #endregion IChildResults interface

    }
}
