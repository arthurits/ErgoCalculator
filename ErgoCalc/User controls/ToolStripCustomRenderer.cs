//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace System.Windows.Forms
{
    /// <summary>
    /// Custom renderer for ToolBar button checked
    /// https://www.discussiongenerator.com/2009/08/22/33/
    /// https://stackoverflow.com/questions/2097164/how-to-change-system-windows-forms-toolstripbutton-highlight-background-color-wh
    /// https://docs.microsoft.com/en-us/dotnet/framework/winforms/controls/walkthrough-creating-a-professionally-styled-toolstrip-control#implementing-a-custom-renderer
    /// </summary>
    public class customRenderer : System.Windows.Forms.ToolStripProfessionalRenderer
    {
        private System.Drawing.Brush _border;
        private System.Drawing.Brush _checkedBackground;

        /// <summary>
        /// Class constructor. Sets SteelBlue and LightSkyBlue as defaults colors
        /// </summary>
        public customRenderer()
        {
            _border = System.Drawing.Brushes.SteelBlue;
            _checkedBackground = System.Drawing.Brushes.LightSkyBlue;
        }

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="border">Brush for the checked border</param>
        /// <param name="checkedBackground">Brush for the checked background</param>
        public customRenderer(System.Drawing.Brush border, System.Drawing.Brush checkedBackground)
        {
            _border = border;
            _checkedBackground = checkedBackground;
        }

        /// <summary>
        /// Sets and gets the border color of the checked button
        /// </summary>
        public System.Drawing.Brush BorderColor
        {
            get { return _border; }
            set { _border = value; }
        }

        /// <summary>
        /// Sets and gets the background color of the checked button
        /// </summary>
        public System.Drawing.Brush CheckedColor
        {
            get { return _checkedBackground; }
            set { _checkedBackground = value; }
        }

        protected override void OnRenderButtonBackground(System.Windows.Forms.ToolStripItemRenderEventArgs e)
        {
            // check if the object being rendered is actually a ToolStripButton
            if (e.Item is System.Windows.Forms.ToolStripButton)
            {
                System.Windows.Forms.ToolStripButton button = e.Item as System.Windows.Forms.ToolStripButton;

                // only render checked items differently
                if (button.Checked)
                {
                    // fill the entire button with a color (will be used as a border)
                    int buttonHeight = button.Size.Height;
                    int buttonWidth = button.Size.Width;
                    System.Drawing.Rectangle rectButtonFill = new System.Drawing.Rectangle(System.Drawing.Point.Empty, new System.Drawing.Size(buttonWidth, buttonHeight));
                    e.Graphics.FillRectangle(_border, rectButtonFill);

                    // fill the entire button offset by 1,1 and height/width subtracted by 2 used as the fill color
                    int backgroundHeight = button.Size.Height - 2;
                    int backgroundWidth = button.Size.Width - 2;
                    System.Drawing.Rectangle rectBackground = new System.Drawing.Rectangle(1, 1, backgroundWidth, backgroundHeight);
                    e.Graphics.FillRectangle(_checkedBackground, rectBackground);
                }
                // if this button is not checked, use the normal render event
                else
                    base.OnRenderButtonBackground(e);
            }
            // if this object is not a ToolStripButton, use the normal render event
            else
                base.OnRenderButtonBackground(e);
        }
    }

}
