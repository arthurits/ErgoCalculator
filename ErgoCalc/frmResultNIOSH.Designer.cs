namespace ErgoCalc
{
    partial class FrmResultNIOSH
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmResultNIOSH));
            this.toolStripNIOSH = new System.Windows.Forms.ToolStrip();
            this.toolStripNIOSH_Save = new System.Windows.Forms.ToolStripButton();
            this.toolStripNIOSH_Settings = new System.Windows.Forms.ToolStripButton();
            this.rtbShowResult = new System.Windows.Forms.RichTextBox();
            this.toolStripNIOSH.SuspendLayout();
            this.SuspendLayout();
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
            this.toolStripNIOSH_Settings.MergeAction = System.Windows.Forms.MergeAction.MatchOnly;
            this.toolStripNIOSH_Settings.Name = "toolStripNIOSH_Settings";
            this.toolStripNIOSH_Settings.Size = new System.Drawing.Size(53, 67);
            this.toolStripNIOSH_Settings.Text = "Settings";
            this.toolStripNIOSH_Settings.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // rtbShowResult
            // 
            this.rtbShowResult.AcceptsTab = true;
            this.rtbShowResult.BackColor = System.Drawing.SystemColors.Window;
            this.rtbShowResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbShowResult.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.rtbShowResult.Location = new System.Drawing.Point(0, 0);
            this.rtbShowResult.Name = "rtbShowResult";
            this.rtbShowResult.ReadOnly = true;
            this.rtbShowResult.ShowSelectionMargin = true;
            this.rtbShowResult.Size = new System.Drawing.Size(975, 538);
            this.rtbShowResult.TabIndex = 3;
            this.rtbShowResult.Text = "";
            this.rtbShowResult.WordWrap = false;
            // 
            // FrmResultNIOSH
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(975, 538);
            this.Controls.Add(this.rtbShowResult);
            this.Controls.Add(this.toolStripNIOSH);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Name = "FrmResultNIOSH";
            this.Text = "NIOSH model results";
            this.Shown += new System.EventHandler(this.frmResultNIOSHModel_Shown);
            this.toolStripNIOSH.ResumeLayout(false);
            this.toolStripNIOSH.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStripNIOSH;
        private System.Windows.Forms.ToolStripButton toolStripNIOSH_Save;
        private System.Windows.Forms.ToolStripButton toolStripNIOSH_Settings;
        private RichTextBox rtbShowResult;
    }
}