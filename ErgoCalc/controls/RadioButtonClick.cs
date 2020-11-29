//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace System.Windows.Forms
{
    /// <summary>
    /// Subclassed RadioButton to accept double click events
    /// </summary>
    public class RadioButtonClick : System.Windows.Forms.RadioButton
    {
        public RadioButtonClick()
        {
            //InitializeComponent();
            this.SetStyle(ControlStyles.StandardClick | ControlStyles.StandardDoubleClick, true);
        }

        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Always), System.ComponentModel.Browsable(true)]
        public new event MouseEventHandler MouseDoubleClick;

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);

            // raise the event
            if (this.MouseDoubleClick != null)
                this.MouseDoubleClick(this, e);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ResumeLayout(false);
        }
    }

}
