using System.Drawing;
using System.Windows.Forms;

namespace ErgoCalc;

public partial class FrmMain
{
    /// <summary>
    /// Initialize the ToolStripPanel component: add the child components to it
    /// </summary>
    private void InitializeToolStripPanel()
    {
        //tspTop = new ToolStripPanel();
        //tspBottom = new ToolStripPanel();
        //tspTop.Join(toolStripMain);
        //tspTop.Join(mnuMainFrm);
        //tspBottom.Join(this.statusStrip);

        //tspTop = new ToolStripPanel();
        //tspBottom = new ToolStripPanel();
        //tspTop.Dock = DockStyle.Top;
        tspTop.Join(toolStripMain);
        tspTop.Join(mnuMainFrm);
        //tspBottom.Dock = DockStyle.Bottom;
        //this.Controls.Add(tspTop);
        //this.Controls.Add(tspBottom);

    }
    /// <summary>
    /// Set menu icons
    /// </summary>
    private void InitializeMenu()
    {
        this.mnuMainFrm_File_New.Image = new System.Drawing.Icon(GraphicsResources.IconNew, 16, 16).ToBitmap();
        this.mnuMainFrm_File_Exit.Image = new System.Drawing.Icon(GraphicsResources.IconExit, 16, 16).ToBitmap();
        this.mnuMainFrm_Help_About.Image = new System.Drawing.Icon(GraphicsResources.IconAbout, 16, 16).ToBitmap();
    }

    /// <summary>
    /// Initialize the ToolStrip component
    /// </summary>
    private void InitializeToolStrip()
    {
        toolStripMain.Renderer = new customRenderer(Brushes.SteelBlue, Brushes.LightSkyBlue);

        // Set ToolStrip icons
        this.toolStripMain_Exit.Image = new System.Drawing.Icon(GraphicsResources.IconExit, 48, 48).ToBitmap();
        this.toolStripMain_Open.Image = new System.Drawing.Icon(GraphicsResources.IconOpen, 48, 48).ToBitmap();
        this.toolStripMain_Save.Image = new System.Drawing.Icon(GraphicsResources.IconSave, 48, 48).ToBitmap();
        this.toolStripMain_SaveChart.Image = new System.Drawing.Icon(GraphicsResources.IconChartSave, 48, 48).ToBitmap();
        this.toolStripMain_New.Image = new System.Drawing.Icon(GraphicsResources.IconNew, 48, 48).ToBitmap();
        this.toolStripMain_Copy.Image = new System.Drawing.Icon(GraphicsResources.IconCopy, 48, 48).ToBitmap();
        this.toolStripMain_EditData.Image = new System.Drawing.Icon(GraphicsResources.IconEdit, 48, 48).ToBitmap();
        this.toolStripMain_AddLine.Image = new System.Drawing.Icon(GraphicsResources.IconChartAdd, 48, 48).ToBitmap();
        this.toolStripMain_RemoveLine.Image = new System.Drawing.Icon(GraphicsResources.IconChartDelete, 48, 48).ToBitmap();
        this.toolStripMain_Settings.Image = new System.Drawing.Icon(GraphicsResources.IconSettings, 48, 48).ToBitmap();
        this.toolStripMain_About.Image = new System.Drawing.Icon(GraphicsResources.IconAbout, 48, 48).ToBitmap();

        ToolBarEnable();
        // https://stackoverflow.com/questions/6389722/toolstrip-with-check-button-group
    }
}
