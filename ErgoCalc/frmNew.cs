using System;
using System.Windows.Forms;

namespace ErgoCalc;

public partial class FrmNew : Form
{
    public ModelType Model { get; private set; }
    
    public FrmNew()
    {
        InitializeComponent();
        UpdateUI_Language();
    }        

    private void Accept_Click(object sender, EventArgs e)
    {
        foreach(RadioButtonClick rad in this.Controls)
        {
            if (rad.Checked)
            {
                switch (rad.Name)
                {
                    case "radModelWR":
                        Model = ModelType.WorkRest;
                        break;
                    case "radModelCLM":
                        Model = ModelType.CumulativeLifting;
                        break;
                    case "radModelLifting":
                        Model = ModelType.LiftingLowering;
                        break;
                    case "radModelStrain":
                        Model = ModelType.StrainIndex;
                        break;
                    case "radModelOCRA":
                        Model = ModelType.OcraCheck;
                        break;
                    case "radModelMetabolic":
                        Model = ModelType.MetabolicRate;
                        break;
                    case "radModelThermal":
                        Model = ModelType.ThermalComfort;
                        break;
                    case "radModelLiberty":
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

    private void UpdateUI_Language()
    {
        this.Text = StringResources.FormNewTitle;
        this.lblSelect.Text = StringResources.LblSelect;
        this.radModelWR.Text = StringResources.RadModelWR;
        this.radModelCLM.Text = StringResources.RadModelCLM;
        this.radModelLifting.Text = StringResources.RadModelLifting;
        this.radModelStrain.Text = StringResources.RadModelStrain;
        this.radModelOCRA.Text = StringResources.RadModelOCRA;
        this.radModelMetabolic.Text = StringResources.RadModelMetabolic;
        this.radModelThermal.Text = StringResources.RadModelThermal;
        this.radModelLiberty.Text = StringResources.RadModelLiberty;
    }
}