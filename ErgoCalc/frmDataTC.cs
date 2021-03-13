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
    public partial class frmDataTC : Form, IChildData
    {
        public frmDataTC()
        {
            InitializeComponent();
        }

        #region IChildData interface
        public object GetData()
        {
            throw new NotImplementedException();
        }
        #endregion IChildData interface
    }
}
