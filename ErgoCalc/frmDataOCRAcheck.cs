using System;
using System.Windows.Forms;

namespace ErgoCalc;

public partial class FrmDataOCRAcheck : Form, IChildData
{
    private double _data;
    object IChildData.GetData => _data;

    public FrmDataOCRAcheck()
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
