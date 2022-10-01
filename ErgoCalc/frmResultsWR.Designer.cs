namespace ErgoCalc
{
    partial class frmResultsWR
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmResultsWR));
            this.mnuSub = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setupPageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.algoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.chart = new ScottPlot.FormsPlot();
            this.toolStripWR = new System.Windows.Forms.ToolStrip();
            this.toolStripWR_SaveChart = new System.Windows.Forms.ToolStripButton();
            this.toolStripWR_AddLine = new System.Windows.Forms.ToolStripButton();
            this.toolStripWR_RemoveLine = new System.Windows.Forms.ToolStripButton();
            this.toolStripWR_Settings = new System.Windows.Forms.ToolStripButton();
            this.mnuSub.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStripWR.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnuSub
            // 
            this.mnuSub.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem});
            this.mnuSub.Location = new System.Drawing.Point(0, 0);
            this.mnuSub.Name = "mnuSub";
            this.mnuSub.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.mnuSub.Size = new System.Drawing.Size(673, 24);
            this.mnuSub.TabIndex = 0;
            this.mnuSub.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setupPageToolStripMenuItem,
            this.toolStripSeparator1});
            this.fileToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.MatchOnly;
            this.fileToolStripMenuItem.MergeIndex = 0;
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // setupPageToolStripMenuItem
            // 
            this.setupPageToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.setupPageToolStripMenuItem.MergeIndex = 2;
            this.setupPageToolStripMenuItem.Name = "setupPageToolStripMenuItem";
            this.setupPageToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.setupPageToolStripMenuItem.Text = "&Setup page";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.toolStripSeparator1.MergeIndex = 3;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(130, 6);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.algoToolStripMenuItem});
            this.editToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.editToolStripMenuItem.MergeIndex = 1;
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "&Edit";
            // 
            // algoToolStripMenuItem
            // 
            this.algoToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.MatchOnly;
            this.algoToolStripMenuItem.Name = "algoToolStripMenuItem";
            this.algoToolStripMenuItem.Size = new System.Drawing.Size(99, 22);
            this.algoToolStripMenuItem.Text = "Algo";
            this.algoToolStripMenuItem.Click += new System.EventHandler(this.algoToolStripMenuItem_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.propertyGrid1);
            this.splitContainer1.Panel1MinSize = 0;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.chart);
            this.splitContainer1.Size = new System.Drawing.Size(673, 450);
            this.splitContainer1.SplitterDistance = 198;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 1;
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid1.Location = new System.Drawing.Point(0, 0);
            this.propertyGrid1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(198, 450);
            this.propertyGrid1.TabIndex = 0;
            // 
            // chart
            // 
            this.chart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chart.Location = new System.Drawing.Point(0, 0);
            this.chart.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.chart.Name = "chart";
            this.chart.Size = new System.Drawing.Size(470, 450);
            this.chart.TabIndex = 2;
            // 
            // toolStripWR
            // 
            this.toolStripWR.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.toolStripWR.ImageScalingSize = new System.Drawing.Size(48, 48);
            this.toolStripWR.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripWR_SaveChart,
            this.toolStripWR_AddLine,
            this.toolStripWR_RemoveLine,
            this.toolStripWR_Settings});
            this.toolStripWR.Location = new System.Drawing.Point(0, 24);
            this.toolStripWR.Name = "toolStripWR";
            this.toolStripWR.Size = new System.Drawing.Size(673, 70);
            this.toolStripWR.TabIndex = 2;
            this.toolStripWR.Text = "toolStrip1";
            this.toolStripWR.Visible = false;
            // 
            // toolStripWR_SaveChart
            // 
            this.toolStripWR_SaveChart.Image = ((System.Drawing.Image)(resources.GetObject("toolStripWR_SaveChart.Image")));
            this.toolStripWR_SaveChart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripWR_SaveChart.MergeAction = System.Windows.Forms.MergeAction.Replace;
            this.toolStripWR_SaveChart.Name = "toolStripWR_SaveChart";
            this.toolStripWR_SaveChart.Size = new System.Drawing.Size(65, 67);
            this.toolStripWR_SaveChart.Text = "Save chart";
            this.toolStripWR_SaveChart.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripWR_SaveChart.ToolTipText = "5Save the chart as an image";
            this.toolStripWR_SaveChart.Click += new System.EventHandler(this.toolStripWR_SaveChart_Click);
            // 
            // toolStripWR_AddLine
            // 
            this.toolStripWR_AddLine.Image = ((System.Drawing.Image)(resources.GetObject("toolStripWR_AddLine.Image")));
            this.toolStripWR_AddLine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripWR_AddLine.MergeAction = System.Windows.Forms.MergeAction.Replace;
            this.toolStripWR_AddLine.Name = "toolStripWR_AddLine";
            this.toolStripWR_AddLine.Size = new System.Drawing.Size(55, 67);
            this.toolStripWR_AddLine.Text = "Add line";
            this.toolStripWR_AddLine.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripWR_AddLine.ToolTipText = "Add serie to chart";
            this.toolStripWR_AddLine.Click += new System.EventHandler(this.toolStripWR_AddLine_Click);
            // 
            // toolStripWR_RemoveLine
            // 
            this.toolStripWR_RemoveLine.Image = ((System.Drawing.Image)(resources.GetObject("toolStripWR_RemoveLine.Image")));
            this.toolStripWR_RemoveLine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripWR_RemoveLine.MergeAction = System.Windows.Forms.MergeAction.Replace;
            this.toolStripWR_RemoveLine.Name = "toolStripWR_RemoveLine";
            this.toolStripWR_RemoveLine.Size = new System.Drawing.Size(76, 67);
            this.toolStripWR_RemoveLine.Text = "Remove line";
            this.toolStripWR_RemoveLine.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripWR_RemoveLine.ToolTipText = "Remove selected serie from chart";
            this.toolStripWR_RemoveLine.Click += new System.EventHandler(this.toolStripWR_RemoveLine_Click);
            // 
            // toolStripWR_Settings
            // 
            this.toolStripWR_Settings.Image = ((System.Drawing.Image)(resources.GetObject("toolStripWR_Settings.Image")));
            this.toolStripWR_Settings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripWR_Settings.MergeAction = System.Windows.Forms.MergeAction.MatchOnly;
            this.toolStripWR_Settings.Name = "toolStripWR_Settings";
            this.toolStripWR_Settings.Size = new System.Drawing.Size(53, 67);
            this.toolStripWR_Settings.Text = "Settings";
            this.toolStripWR_Settings.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripWR_Settings.ToolTipText = "Toggle settings";
            this.toolStripWR_Settings.CheckedChanged += new System.EventHandler(this.toolStripMain_Settings_CheckedChanged);
            // 
            // frmWRmodel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(673, 474);
            this.Controls.Add(this.toolStripWR);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.mnuSub);
            this.MainMenuStrip = this.mnuSub;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "frmWRmodel";
            this.Text = "WR model";
            this.mnuSub.ResumeLayout(false);
            this.mnuSub.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.toolStripWR.ResumeLayout(false);
            this.toolStripWR.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mnuSub;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setupPageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem algoToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.ToolStrip toolStripWR;
        private System.Windows.Forms.ToolStripButton toolStripWR_Settings;
        private System.Windows.Forms.ToolStripButton toolStripWR_AddLine;
        private System.Windows.Forms.ToolStripButton toolStripWR_RemoveLine;
        private System.Windows.Forms.ToolStripButton toolStripWR_SaveChart;
        private ScottPlot.FormsPlot chart;
    }
}