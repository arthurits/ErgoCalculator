using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ErgoCalc
{
    public partial class frmDataOCRAcheck : Form, IChildData
    {
        private double _data;
        object IChildData.GetData => _data;

        public frmDataOCRAcheck()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            // Save the job definition
        }
        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
        }

        public void LoadExample()
        {

        }
    }
}
