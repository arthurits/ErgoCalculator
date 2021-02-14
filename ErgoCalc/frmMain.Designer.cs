namespace ErgoCalc
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.mnuMainFrm = new System.Windows.Forms.MenuStrip();
            this.mnuMainFrm_File = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMainFrm_File_New = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuMainFrm_File_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMainForm_Window = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMainFrm_Help = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMainFrm_Help_About = new System.Windows.Forms.ToolStripMenuItem();
            this.staMainFrm = new System.Windows.Forms.StatusStrip();
            this.toolStripMain = new System.Windows.Forms.ToolStrip();
            this.toolStripMain_Exit = new System.Windows.Forms.ToolStripButton();
            this.toolStripMain_Open = new System.Windows.Forms.ToolStripButton();
            this.toolStripMain_Save = new System.Windows.Forms.ToolStripButton();
            this.toolStripMain_SaveChart = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMain_New = new System.Windows.Forms.ToolStripButton();
            this.toolStripMain_Copy = new System.Windows.Forms.ToolStripButton();
            this.toolStripMain_EditData = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMain_AddLine = new System.Windows.Forms.ToolStripButton();
            this.toolStripMain_RemoveLine = new System.Windows.Forms.ToolStripButton();
            this.toolStripMain_Settings = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMain_About = new System.Windows.Forms.ToolStripButton();
            this.tspTop = new System.Windows.Forms.ToolStripPanel();
            this.tspBottom = new System.Windows.Forms.ToolStripPanel();
            this.mnuMainFrm.SuspendLayout();
            this.toolStripMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnuMainFrm
            // 
            this.mnuMainFrm.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.mnuMainFrm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuMainFrm_File,
            this.mnuMainForm_Window,
            this.mnuMainFrm_Help});
            this.mnuMainFrm.Location = new System.Drawing.Point(0, 0);
            this.mnuMainFrm.MdiWindowListItem = this.mnuMainForm_Window;
            this.mnuMainFrm.Name = "mnuMainFrm";
            this.mnuMainFrm.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.mnuMainFrm.Size = new System.Drawing.Size(957, 24);
            this.mnuMainFrm.TabIndex = 2;
            this.mnuMainFrm.Text = "menuStrip1";
            // 
            // mnuMainFrm_File
            // 
            this.mnuMainFrm_File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuMainFrm_File_New,
            this.toolStripSeparator1,
            this.mnuMainFrm_File_Exit});
            this.mnuMainFrm_File.MergeIndex = 0;
            this.mnuMainFrm_File.Name = "mnuMainFrm_File";
            this.mnuMainFrm_File.Size = new System.Drawing.Size(37, 20);
            this.mnuMainFrm_File.Text = "&File";
            // 
            // mnuMainFrm_File_New
            // 
            this.mnuMainFrm_File_New.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.mnuMainFrm_File_New.MergeIndex = 0;
            this.mnuMainFrm_File_New.Name = "mnuMainFrm_File_New";
            this.mnuMainFrm_File_New.Size = new System.Drawing.Size(107, 22);
            this.mnuMainFrm_File_New.Text = "&New...";
            this.mnuMainFrm_File_New.Click += new System.EventHandler(this.mnuMainFrm_File_New_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.MergeIndex = 1;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(104, 6);
            // 
            // mnuMainFrm_File_Exit
            // 
            this.mnuMainFrm_File_Exit.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.mnuMainFrm_File_Exit.MergeIndex = 4;
            this.mnuMainFrm_File_Exit.Name = "mnuMainFrm_File_Exit";
            this.mnuMainFrm_File_Exit.Size = new System.Drawing.Size(107, 22);
            this.mnuMainFrm_File_Exit.Text = "&Exit";
            this.mnuMainFrm_File_Exit.Click += new System.EventHandler(this.mnuMainFrm_File_Exit_Click);
            // 
            // mnuMainForm_Window
            // 
            this.mnuMainForm_Window.Name = "mnuMainForm_Window";
            this.mnuMainForm_Window.Size = new System.Drawing.Size(63, 20);
            this.mnuMainForm_Window.Text = "&Window";
            // 
            // mnuMainFrm_Help
            // 
            this.mnuMainFrm_Help.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuMainFrm_Help_About});
            this.mnuMainFrm_Help.MergeIndex = 10;
            this.mnuMainFrm_Help.Name = "mnuMainFrm_Help";
            this.mnuMainFrm_Help.Size = new System.Drawing.Size(44, 20);
            this.mnuMainFrm_Help.Text = "&Help";
            // 
            // mnuMainFrm_Help_About
            // 
            this.mnuMainFrm_Help_About.Name = "mnuMainFrm_Help_About";
            this.mnuMainFrm_Help_About.Size = new System.Drawing.Size(116, 22);
            this.mnuMainFrm_Help_About.Text = "&About...";
            this.mnuMainFrm_Help_About.Click += new System.EventHandler(this.mnuMainFrm_Help_About_Click);
            // 
            // staMainFrm
            // 
            this.staMainFrm.Location = new System.Drawing.Point(0, 596);
            this.staMainFrm.Name = "staMainFrm";
            this.staMainFrm.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.staMainFrm.Size = new System.Drawing.Size(957, 22);
            this.staMainFrm.TabIndex = 1;
            this.staMainFrm.Text = "statusStrip1";
            // 
            // toolStripMain
            // 
            this.toolStripMain.ImageScalingSize = new System.Drawing.Size(48, 48);
            this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMain_Exit,
            this.toolStripMain_Open,
            this.toolStripMain_Save,
            this.toolStripMain_SaveChart,
            this.toolStripSeparator2,
            this.toolStripMain_New,
            this.toolStripMain_Copy,
            this.toolStripMain_EditData,
            this.toolStripSeparator4,
            this.toolStripMain_AddLine,
            this.toolStripMain_RemoveLine,
            this.toolStripMain_Settings,
            this.toolStripSeparator3,
            this.toolStripMain_About});
            this.toolStripMain.Location = new System.Drawing.Point(0, 24);
            this.toolStripMain.Name = "toolStripMain";
            this.toolStripMain.Padding = new System.Windows.Forms.Padding(0);
            this.toolStripMain.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStripMain.Size = new System.Drawing.Size(957, 70);
            this.toolStripMain.TabIndex = 4;
            this.toolStripMain.Text = "toolStrip1";
            // 
            // toolStripMain_Exit
            // 
            this.toolStripMain_Exit.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMain_Exit.Image")));
            this.toolStripMain_Exit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripMain_Exit.Name = "toolStripMain_Exit";
            this.toolStripMain_Exit.Size = new System.Drawing.Size(52, 67);
            this.toolStripMain_Exit.Text = "Exit";
            this.toolStripMain_Exit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripMain_Exit.ToolTipText = "From Main";
            this.toolStripMain_Exit.Click += new System.EventHandler(this.toolStripMain_Exit_Click);
            // 
            // toolStripMain_Open
            // 
            this.toolStripMain_Open.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMain_Open.Image")));
            this.toolStripMain_Open.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripMain_Open.Name = "toolStripMain_Open";
            this.toolStripMain_Open.Size = new System.Drawing.Size(77, 67);
            this.toolStripMain_Open.Text = "Open model";
            this.toolStripMain_Open.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolStripMain_Save
            // 
            this.toolStripMain_Save.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMain_Save.Image")));
            this.toolStripMain_Save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripMain_Save.Name = "toolStripMain_Save";
            this.toolStripMain_Save.Size = new System.Drawing.Size(52, 67);
            this.toolStripMain_Save.Text = "Save";
            this.toolStripMain_Save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripMain_Save.ToolTipText = "Save data";
            this.toolStripMain_Save.Click += new System.EventHandler(this.toolStripMain_Save_Click);
            // 
            // toolStripMain_SaveChart
            // 
            this.toolStripMain_SaveChart.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMain_SaveChart.Image")));
            this.toolStripMain_SaveChart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripMain_SaveChart.Name = "toolStripMain_SaveChart";
            this.toolStripMain_SaveChart.Size = new System.Drawing.Size(65, 67);
            this.toolStripMain_SaveChart.Text = "Save chart";
            this.toolStripMain_SaveChart.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripMain_SaveChart.ToolTipText = "Save the chart as an image";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 70);
            // 
            // toolStripMain_New
            // 
            this.toolStripMain_New.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMain_New.Image")));
            this.toolStripMain_New.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripMain_New.Name = "toolStripMain_New";
            this.toolStripMain_New.Size = new System.Drawing.Size(52, 67);
            this.toolStripMain_New.Text = "New";
            this.toolStripMain_New.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripMain_New.ToolTipText = "New model";
            this.toolStripMain_New.Click += new System.EventHandler(this.toolStripMain_New_Click);
            // 
            // toolStripMain_Copy
            // 
            this.toolStripMain_Copy.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMain_Copy.Image")));
            this.toolStripMain_Copy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripMain_Copy.Name = "toolStripMain_Copy";
            this.toolStripMain_Copy.Size = new System.Drawing.Size(61, 67);
            this.toolStripMain_Copy.Text = "Duplicate";
            this.toolStripMain_Copy.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripMain_Copy.ToolTipText = "Duplicate window";
            // 
            // toolStripMain_EditData
            // 
            this.toolStripMain_EditData.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMain_EditData.Image")));
            this.toolStripMain_EditData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripMain_EditData.Name = "toolStripMain_EditData";
            this.toolStripMain_EditData.Size = new System.Drawing.Size(57, 67);
            this.toolStripMain_EditData.Text = "Edit data";
            this.toolStripMain_EditData.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripMain_EditData.ToolTipText = "Edit model data";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 70);
            // 
            // toolStripMain_AddLine
            // 
            this.toolStripMain_AddLine.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMain_AddLine.Image")));
            this.toolStripMain_AddLine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripMain_AddLine.Name = "toolStripMain_AddLine";
            this.toolStripMain_AddLine.Size = new System.Drawing.Size(55, 67);
            this.toolStripMain_AddLine.Text = "Add line";
            this.toolStripMain_AddLine.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripMain_AddLine.ToolTipText = "Add serie to chart";
            // 
            // toolStripMain_RemoveLine
            // 
            this.toolStripMain_RemoveLine.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMain_RemoveLine.Image")));
            this.toolStripMain_RemoveLine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripMain_RemoveLine.Name = "toolStripMain_RemoveLine";
            this.toolStripMain_RemoveLine.Size = new System.Drawing.Size(76, 67);
            this.toolStripMain_RemoveLine.Text = "Remove line";
            this.toolStripMain_RemoveLine.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripMain_RemoveLine.ToolTipText = "Remove selected serie from chart";
            // 
            // toolStripMain_Settings
            // 
            this.toolStripMain_Settings.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMain_Settings.Image")));
            this.toolStripMain_Settings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripMain_Settings.Name = "toolStripMain_Settings";
            this.toolStripMain_Settings.Size = new System.Drawing.Size(53, 67);
            this.toolStripMain_Settings.Text = "Settings";
            this.toolStripMain_Settings.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripMain_Settings.ToolTipText = "Toggle settings";
            this.toolStripMain_Settings.CheckedChanged += new System.EventHandler(this.toolStripMain_Settings_CheckedChanged);
            this.toolStripMain_Settings.Click += new System.EventHandler(this.toolStripMain_Settings_Click);
            this.toolStripMain_Settings.EnabledChanged += new System.EventHandler(this.toolStripMain_Settings_EnabledChanged);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 70);
            // 
            // toolStripMain_About
            // 
            this.toolStripMain_About.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMain_About.Image")));
            this.toolStripMain_About.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripMain_About.Name = "toolStripMain_About";
            this.toolStripMain_About.Size = new System.Drawing.Size(52, 67);
            this.toolStripMain_About.Text = "About";
            this.toolStripMain_About.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripMain_About.Click += new System.EventHandler(this.toolStripMain_About_Click);
            // 
            // tspTop
            // 
            this.tspTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.tspTop.Location = new System.Drawing.Point(0, 0);
            this.tspTop.Name = "tspTop";
            this.tspTop.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.tspTop.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.tspTop.Size = new System.Drawing.Size(957, 0);
            // 
            // tspBottom
            // 
            this.tspBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tspBottom.Location = new System.Drawing.Point(0, 618);
            this.tspBottom.Name = "tspBottom";
            this.tspBottom.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.tspBottom.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.tspBottom.Size = new System.Drawing.Size(957, 0);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(957, 618);
            this.Controls.Add(this.toolStripMain);
            this.Controls.Add(this.staMainFrm);
            this.Controls.Add(this.mnuMainFrm);
            this.Controls.Add(this.tspTop);
            this.Controls.Add(this.tspBottom);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.mnuMainFrm;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ergonomic calculator";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.MdiChildActivate += new System.EventHandler(this.frmMain_MdiChildActivate);
            this.Shown += new System.EventHandler(this.frmMain_Shown);
            this.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.frmMain_ControlAdded);
            this.ControlRemoved += new System.Windows.Forms.ControlEventHandler(this.frmMain_ControlRemoved);
            this.mnuMainFrm.ResumeLayout(false);
            this.mnuMainFrm.PerformLayout();
            this.toolStripMain.ResumeLayout(false);
            this.toolStripMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion


        private System.Windows.Forms.MenuStrip mnuMainFrm;
        private System.Windows.Forms.ToolStripMenuItem mnuMainFrm_File;
        private System.Windows.Forms.ToolStripMenuItem mnuMainFrm_File_New;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnuMainFrm_File_Exit;
        private System.Windows.Forms.ToolStripMenuItem mnuMainFrm_Help;
        private System.Windows.Forms.ToolStripMenuItem mnuMainFrm_Help_About;
        private System.Windows.Forms.StatusStrip staMainFrm;
        private System.Windows.Forms.ToolStrip toolStripMain;
        private System.Windows.Forms.ToolStripButton toolStripMain_Exit;
        private System.Windows.Forms.ToolStripMenuItem mnuMainForm_Window;
        private System.Windows.Forms.ToolStripPanel tspTop;
        private System.Windows.Forms.ToolStripPanel tspBottom;
        private System.Windows.Forms.ToolStripButton toolStripMain_Open;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripMain_New;
        private System.Windows.Forms.ToolStripButton toolStripMain_Copy;
        private System.Windows.Forms.ToolStripButton toolStripMain_Settings;
        private System.Windows.Forms.ToolStripButton toolStripMain_Save;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolStripMain_About;
        private System.Windows.Forms.ToolStripButton toolStripMain_SaveChart;
        private System.Windows.Forms.ToolStripButton toolStripMain_EditData;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton toolStripMain_AddLine;
        private System.Windows.Forms.ToolStripButton toolStripMain_RemoveLine;
    }
}