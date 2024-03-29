﻿namespace ErgoCalc
{
    partial class FrmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.mnuMainFrm = new System.Windows.Forms.MenuStrip();
            this.mnuMainFrm_File = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMainFrm_File_New = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuMainFrm_File_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMainFrm_Window = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMainFrm_Help = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMainFrm_Help_About = new System.Windows.Forms.ToolStripMenuItem();
            this.staMainFrm = new System.Windows.Forms.StatusStrip();
            this.statusStripLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStripLabelCulture = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStripLabelFont = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStripLabelFontColor = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStripLabelWordWrap = new System.Windows.Forms.ToolStripStatusLabelEx();
            this.statusStripLabelBackColor = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStripLabelZoom = new System.Windows.Forms.ToolStripStatusLabel();
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
            this.staMainFrm.SuspendLayout();
            this.toolStripMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnuMainFrm
            // 
            this.mnuMainFrm.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.mnuMainFrm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuMainFrm_File,
            this.mnuMainFrm_Window,
            this.mnuMainFrm_Help});
            this.mnuMainFrm.Location = new System.Drawing.Point(0, 0);
            this.mnuMainFrm.MdiWindowListItem = this.mnuMainFrm_Window;
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
            this.mnuMainFrm_File_New.Click += new System.EventHandler(this.New_Click);
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
            this.mnuMainFrm_File_Exit.Click += new System.EventHandler(this.Exit_Click);
            // 
            // mnuMainFrm_Window
            // 
            this.mnuMainFrm_Window.Name = "mnuMainFrm_Window";
            this.mnuMainFrm_Window.Size = new System.Drawing.Size(63, 20);
            this.mnuMainFrm_Window.Text = "&Window";
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
            this.mnuMainFrm_Help_About.Click += new System.EventHandler(this.About_Click);
            // 
            // staMainFrm
            // 
            this.staMainFrm.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.staMainFrm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusStripLabel,
            this.statusStripLabelCulture,
            this.statusStripLabelFont,
            this.statusStripLabelFontColor,
            this.statusStripLabelWordWrap,
            this.statusStripLabelBackColor,
            this.statusStripLabelZoom});
            this.staMainFrm.Location = new System.Drawing.Point(0, 590);
            this.staMainFrm.Name = "staMainFrm";
            this.staMainFrm.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.staMainFrm.ShowItemToolTips = true;
            this.staMainFrm.Size = new System.Drawing.Size(957, 28);
            this.staMainFrm.TabIndex = 1;
            this.staMainFrm.Text = "statusStrip1";
            // 
            // statusStripLabel
            // 
            this.statusStripLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.statusStripLabel.Name = "statusStripLabel";
            this.statusStripLabel.Size = new System.Drawing.Size(694, 23);
            this.statusStripLabel.Spring = true;
            this.statusStripLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // statusStripLabelCulture
            // 
            this.statusStripLabelCulture.AutoSize = false;
            this.statusStripLabelCulture.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
            this.statusStripLabelCulture.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.statusStripLabelCulture.Name = "statusStripLabelCulture";
            this.statusStripLabelCulture.Size = new System.Drawing.Size(70, 23);
            this.statusStripLabelCulture.ToolTipText = "UI culture";
            this.statusStripLabelCulture.Click += new System.EventHandler(this.Language_Click);
            // 
            // statusStripLabelFont
            // 
            this.statusStripLabelFont.BackColor = System.Drawing.Color.Transparent;
            this.statusStripLabelFont.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.statusStripLabelFont.Enabled = false;
            this.statusStripLabelFont.Name = "statusStripLabelFont";
            this.statusStripLabelFont.Size = new System.Drawing.Size(23, 23);
            this.statusStripLabelFont.Text = "—";
            this.statusStripLabelFont.ToolTipText = "Select font";
            this.statusStripLabelFont.Click += new System.EventHandler(this.LabelFont_Click);
            // 
            // statusStripLabelFontColor
            // 
            this.statusStripLabelFontColor.AutoSize = false;
            this.statusStripLabelFontColor.BackColor = System.Drawing.Color.Transparent;
            this.statusStripLabelFontColor.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.statusStripLabelFontColor.Enabled = false;
            this.statusStripLabelFontColor.Name = "statusStripLabelFontColor";
            this.statusStripLabelFontColor.Size = new System.Drawing.Size(28, 23);
            this.statusStripLabelFontColor.ToolTipText = "Select font color";
            this.statusStripLabelFontColor.Click += new System.EventHandler(this.LabelFontColor_Click);
            // 
            // statusStripLabelWordWrap
            // 
            this.statusStripLabelWordWrap.AutoSize = false;
            this.statusStripLabelWordWrap.BackColor = System.Drawing.Color.Transparent;
            this.statusStripLabelWordWrap.Checked = false;
            this.statusStripLabelWordWrap.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.statusStripLabelWordWrap.Enabled = false;
            this.statusStripLabelWordWrap.ForeColor = System.Drawing.Color.LightGray;
            this.statusStripLabelWordWrap.ForeColorChecked = System.Drawing.Color.Black;
            this.statusStripLabelWordWrap.ForeColorUnchecked = System.Drawing.Color.LightGray;
            this.statusStripLabelWordWrap.Name = "statusStripLabelWordWrap";
            this.statusStripLabelWordWrap.Size = new System.Drawing.Size(28, 23);
            this.statusStripLabelWordWrap.Text = "W";
            this.statusStripLabelWordWrap.ToolTipText = "Wrap words to the beginning of the next line when necessary";
            this.statusStripLabelWordWrap.Click += new System.EventHandler(this.LabelWordWrap_Click);
            // 
            // statusStripLabelBackColor
            // 
            this.statusStripLabelBackColor.AutoSize = false;
            this.statusStripLabelBackColor.BackColor = System.Drawing.Color.Transparent;
            this.statusStripLabelBackColor.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.statusStripLabelBackColor.Enabled = false;
            this.statusStripLabelBackColor.ForeColor = System.Drawing.SystemColors.ControlText;
            this.statusStripLabelBackColor.Name = "statusStripLabelBackColor";
            this.statusStripLabelBackColor.Size = new System.Drawing.Size(28, 23);
            this.statusStripLabelBackColor.ToolTipText = "Select text backcolor";
            this.statusStripLabelBackColor.Click += new System.EventHandler(this.LabelBackColor_Click);
            // 
            // statusStripLabelZoom
            // 
            this.statusStripLabelZoom.AutoSize = false;
            this.statusStripLabelZoom.BackColor = System.Drawing.Color.Transparent;
            this.statusStripLabelZoom.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.statusStripLabelZoom.Enabled = false;
            this.statusStripLabelZoom.ForeColor = System.Drawing.SystemColors.ControlText;
            this.statusStripLabelZoom.Name = "statusStripLabelZoom";
            this.statusStripLabelZoom.Size = new System.Drawing.Size(35, 23);
            this.statusStripLabelZoom.Text = "Z";
            this.statusStripLabelZoom.ToolTipText = "Select zoom factor";
            this.statusStripLabelZoom.Click += new System.EventHandler(this.LabelZoom_Click);
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
            this.toolStripMain_Exit.Click += new System.EventHandler(this.Exit_Click);
            // 
            // toolStripMain_Open
            // 
            this.toolStripMain_Open.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripMain_Open.Name = "toolStripMain_Open";
            this.toolStripMain_Open.Size = new System.Drawing.Size(77, 67);
            this.toolStripMain_Open.Text = "Open model";
            this.toolStripMain_Open.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripMain_Open.Click += new System.EventHandler(this.Open_Click);
            // 
            // toolStripMain_Save
            // 
            this.toolStripMain_Save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripMain_Save.Name = "toolStripMain_Save";
            this.toolStripMain_Save.Size = new System.Drawing.Size(35, 67);
            this.toolStripMain_Save.Text = "Save";
            this.toolStripMain_Save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripMain_Save.ToolTipText = "Save data";
            this.toolStripMain_Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // toolStripMain_SaveChart
            // 
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
            this.toolStripMain_New.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripMain_New.Name = "toolStripMain_New";
            this.toolStripMain_New.Size = new System.Drawing.Size(35, 67);
            this.toolStripMain_New.Text = "New";
            this.toolStripMain_New.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripMain_New.ToolTipText = "New model";
            this.toolStripMain_New.Click += new System.EventHandler(this.New_Click);
            // 
            // toolStripMain_Copy
            // 
            this.toolStripMain_Copy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripMain_Copy.Name = "toolStripMain_Copy";
            this.toolStripMain_Copy.Size = new System.Drawing.Size(61, 67);
            this.toolStripMain_Copy.Text = "Duplicate";
            this.toolStripMain_Copy.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripMain_Copy.ToolTipText = "Duplicate window";
            this.toolStripMain_Copy.Click += new System.EventHandler(this.Copy_Click);
            // 
            // toolStripMain_EditData
            // 
            this.toolStripMain_EditData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripMain_EditData.Name = "toolStripMain_EditData";
            this.toolStripMain_EditData.Size = new System.Drawing.Size(57, 67);
            this.toolStripMain_EditData.Text = "Edit data";
            this.toolStripMain_EditData.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripMain_EditData.ToolTipText = "Edit model data";
            this.toolStripMain_EditData.Click += new System.EventHandler(this.EditData_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 70);
            // 
            // toolStripMain_AddLine
            // 
            this.toolStripMain_AddLine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripMain_AddLine.Name = "toolStripMain_AddLine";
            this.toolStripMain_AddLine.Size = new System.Drawing.Size(55, 67);
            this.toolStripMain_AddLine.Text = "Add line";
            this.toolStripMain_AddLine.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripMain_AddLine.ToolTipText = "Add serie to chart";
            // 
            // toolStripMain_RemoveLine
            // 
            this.toolStripMain_RemoveLine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripMain_RemoveLine.Name = "toolStripMain_RemoveLine";
            this.toolStripMain_RemoveLine.Size = new System.Drawing.Size(76, 67);
            this.toolStripMain_RemoveLine.Text = "Remove line";
            this.toolStripMain_RemoveLine.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripMain_RemoveLine.ToolTipText = "Remove selected serie from chart";
            // 
            // toolStripMain_Settings
            // 
            this.toolStripMain_Settings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripMain_Settings.Name = "toolStripMain_Settings";
            this.toolStripMain_Settings.Size = new System.Drawing.Size(53, 67);
            this.toolStripMain_Settings.Text = "Settings";
            this.toolStripMain_Settings.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripMain_Settings.ToolTipText = "Toggle settings";
            this.toolStripMain_Settings.Click += new System.EventHandler(this.Settings_Click);
            this.toolStripMain_Settings.EnabledChanged += new System.EventHandler(this.Settings_EnabledChanged);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 70);
            // 
            // toolStripMain_About
            // 
            this.toolStripMain_About.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripMain_About.Name = "toolStripMain_About";
            this.toolStripMain_About.Size = new System.Drawing.Size(44, 67);
            this.toolStripMain_About.Text = "About";
            this.toolStripMain_About.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripMain_About.Click += new System.EventHandler(this.About_Click);
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
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(957, 618);
            this.Controls.Add(this.toolStripMain);
            this.Controls.Add(this.staMainFrm);
            this.Controls.Add(this.mnuMainFrm);
            this.Controls.Add(this.tspTop);
            this.Controls.Add(this.tspBottom);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.mnuMainFrm;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ergonomic calculator";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.MdiChildActivate += new System.EventHandler(this.FrmMain_MdiChildActivate);
            this.Shown += new System.EventHandler(this.FrmMain_Shown);
            this.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.FrmMain_ControlAdded);
            this.ControlRemoved += new System.Windows.Forms.ControlEventHandler(this.FrmMain_ControlRemoved);
            this.mnuMainFrm.ResumeLayout(false);
            this.mnuMainFrm.PerformLayout();
            this.staMainFrm.ResumeLayout(false);
            this.staMainFrm.PerformLayout();
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
        private System.Windows.Forms.ToolStripMenuItem mnuMainFrm_Window;
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
        private System.Windows.Forms.ToolStripStatusLabel statusStripLabel;
        private System.Windows.Forms.ToolStripStatusLabel statusStripLabelCulture;
        private System.Windows.Forms.ToolStripStatusLabelEx statusStripLabelWordWrap;
        private System.Windows.Forms.ToolStripStatusLabel statusStripLabelBackColor;
        private System.Windows.Forms.ToolStripStatusLabel statusStripLabelZoom;
        private ToolStripStatusLabel statusStripLabelFont;
        private ToolStripStatusLabel statusStripLabelFontColor;
    }
}