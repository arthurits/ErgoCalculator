using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace ErgoCalc;

public partial class FrmNew : Form
{
    public ModelType Model { get; private set; }
    
    public FrmNew()
    {
        InitializeComponent();
    }        

    private void Accept_Click(object sender, EventArgs e)
    {
        foreach(RadioButtonClick rad in this.Controls)
        {
            if (rad.Checked)
            {
                switch (rad.Text)
                {
                    case "WR model":
                        Model = ModelType.WorkRest;
                        break;
                    case "CLM model":
                        Model = ModelType.CumulativeLifting;
                        break;
                    case "NIOSH equation (LI, CLI, SLI, VLI)":
                        Model = ModelType.NioshLifting;
                        break;
                    case "Revised strain index (RSI, COSI, CUSI)":
                        Model = ModelType.StrainIndex;
                        break;
                    case "OCRA checklist":
                        Model = ModelType.OcraCheck;
                        break;
                    case "Metabolic rate":
                        Model = ModelType.MetabolicRate;
                        break;
                    case "Thermal comfort (PMV, PPD)":
                        Model = ModelType.ThermalComfort;
                        break;
                    case "LM manual handling":
                        Model = ModelType.LibertyMutual;
                        break;
                }
                break;
            }
        }

        return;
    }

    // https://www.cyotek.com/blog/creating-a-windows-forms-radiobutton-that-supports-the-double-click-event
    private void radioButtons_DoubleClick(object sender, MouseEventArgs e)
    {
        this.Accept_Click(btnAccept, EventArgs.Empty);
        this.DialogResult = DialogResult.OK;
    }
}