namespace ErgoCalc
{
    partial class frmResultNIOSHmodel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmResultNIOSHmodel));
            this.rtbShowResult = new System.Windows.Forms.RichTextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.toolStripNIOSH = new System.Windows.Forms.ToolStrip();
            this.toolStripNIOSH_Save = new System.Windows.Forms.ToolStripButton();
            this.toolStripNIOSH_Settings = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStripNIOSH.SuspendLayout();
            this.SuspendLayout();
            // 
            // rtbShowResult
            // 
            this.rtbShowResult.AcceptsTab = true;
            this.rtbShowResult.BackColor = System.Drawing.SystemColors.Window;
            this.rtbShowResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbShowResult.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbShowResult.Location = new System.Drawing.Point(0, 0);
            this.rtbShowResult.Name = "rtbShowResult";
            this.rtbShowResult.ReadOnly = true;
            this.rtbShowResult.ShowSelectionMargin = true;
            this.rtbShowResult.Size = new System.Drawing.Size(771, 538);
            this.rtbShowResult.TabIndex = 0;
            this.rtbShowResult.Text = "";
            this.rtbShowResult.WordWrap = false;
            this.rtbShowResult.DoubleClick += new System.EventHandler(this.rtbShowResult_DoubleClick);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.propertyGrid1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.rtbShowResult);
            this.splitContainer1.Size = new System.Drawing.Size(975, 538);
            this.splitContainer1.SplitterDistance = 200;
            this.splitContainer1.TabIndex = 1;
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid1.Location = new System.Drawing.Point(0, 0);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(200, 538);
            this.propertyGrid1.TabIndex = 0;
            // 
            // toolStripNIOSH
            // 
            this.toolStripNIOSH.ImageScalingSize = new System.Drawing.Size(48, 48);
            this.toolStripNIOSH.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripNIOSH_Save,
            this.toolStripNIOSH_Settings});
            this.toolStripNIOSH.Location = new System.Drawing.Point(0, 0);
            this.toolStripNIOSH.Name = "toolStripNIOSH";
            this.toolStripNIOSH.Size = new System.Drawing.Size(975, 70);
            this.toolStripNIOSH.TabIndex = 2;
            this.toolStripNIOSH.Text = "toolStrip1";
            this.toolStripNIOSH.Visible = false;
            // 
            // toolStripNIOSH_Save
            // 
            this.toolStripNIOSH_Save.Image = ((System.Drawing.Image)(resources.GetObject("toolStripNIOSH_Save.Image")));
            this.toolStripNIOSH_Save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripNIOSH_Save.MergeAction = System.Windows.Forms.MergeAction.Replace;
            this.toolStripNIOSH_Save.Name = "toolStripNIOSH_Save";
            this.toolStripNIOSH_Save.Size = new System.Drawing.Size(52, 67);
            this.toolStripNIOSH_Save.Text = "Save";
            this.toolStripNIOSH_Save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolStripNIOSH_Settings
            // 
            this.toolStripNIOSH_Settings.Image = ((System.Drawing.Image)(resources.GetObject("toolStripNIOSH_Settings.Image")));
            this.toolStripNIOSH_Settings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripNIOSH_Settings.MergeAction = System.Windows.Forms.MergeAction.Replace;
            this.toolStripNIOSH_Settings.Name = "toolStripNIOSH_Settings";
            this.toolStripNIOSH_Settings.Size = new System.Drawing.Size(53, 67);
            this.toolStripNIOSH_Settings.Text = "Settings";
            this.toolStripNIOSH_Settings.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripNIOSH_Settings.CheckedChanged += new System.EventHandler(this.toolStripNIOSH_Settings_CheckedChanged);
            // 
            // frmResultNIOSHmodel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(975, 538);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStripNIOSH);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmResultNIOSHmodel";
            this.Text = "NIOSH model results";
            this.Shown += new System.EventHandler(this.frmResultNIOSHModel_Shown);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.toolStripNIOSH.ResumeLayout(false);
            this.toolStripNIOSH.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbShowResult;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.ToolStrip toolStripNIOSH;
        private System.Windows.Forms.ToolStripButton toolStripNIOSH_Save;
        private System.Windows.Forms.ToolStripButton toolStripNIOSH_Settings;
    }
}