using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ErgoCalc
{
    public partial class frmNew : Form
    {
        private Int32 _nOption;
        public Int32 Model { get => _nOption; }

        //private String[] _strArray = { "WR model", "CLM model", "NIOSH equation (LI, CLI, SLI, VLI)", "Revised strain index (RSI, COSI, CUSI)", "Metabolic rate" };
        //private RadioButton[] radioButtons = new RadioButton[5];
        
        public frmNew()
        {
            InitializeComponent();

            /*           
            for (Int32 i = 0; i < _strArray.Length; ++i)
            {
                radioButtons[i] = new RadioButtonClick
                {
                    Text = _strArray[i],
                    Location = new System.Drawing.Point(63, 158 + i * 33),
                    Tag = i,
                    Width = 450
                };
                // https://www.cyotek.com/blog/creating-a-windows-forms-radiobutton-that-supports-the-double-click-event
                radioButtons[i].MouseDoubleClick += radioButtons_DoubleClick;
                this.Controls.Add(radioButtons[i]);
            }
            */
        }

        

        private void btnAccept_Click(object sender, EventArgs e)
        {
            foreach(RadioButtonClick rad in this.Controls)
            {
                if (rad.Checked)
                {
                    switch (rad.Text)
                    {
                        case "WR model":
                            _nOption = 1;
                            break;
                        case "CLM model":
                            _nOption = 2;
                            break;
                        case "NIOSH equation (LI, CLI, SLI, VLI)":
                            _nOption = 3;
                            break;
                        case "Revised strain index (RSI, COSI, CUSI)":
                            _nOption = 4;
                            break;
                        case "OCRA checklist":
                            _nOption = 5;
                            break;
                        case "Metabolic rate":
                            _nOption = 6;
                            break;
                        case "Thermal comfort (PMV, PPD)":
                            _nOption = 7;
                            break;
                        case "LM manual handling":
                            _nOption = 8;
                            break;
                    }
                    //_nOption = (int)rad.Tag;
                    break;
                }
            }

            /*
            for (Int32 i = 0; i < _strArray.Length; i++)
            {
                if (radioButtons[i].Checked == true)
                {
                    _nOption = i + 1;
                    break;
                }
            }
            */
            return;
        }

        private void radioButtons_DoubleClick(object sender, MouseEventArgs e)
        {
            this.btnAccept_Click(btnAccept, EventArgs.Empty);
            this.DialogResult = DialogResult.OK;
        }
    }


}
