namespace ErgoCalc
{
    partial class frmResultsLifting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmResultsLifting));
            this.toolStripLifting = new System.Windows.Forms.ToolStrip();
            this.toolStripLifting_Save = new System.Windows.Forms.ToolStripButton();
            this.toolStripLifting_Settings = new System.Windows.Forms.ToolStripButton();
            this.rtbShowResult = new System.Windows.Forms.RichTextBox();
            this.toolStripLifting.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripNIOSH
            // 
            this.toolStripLifting.ImageScalingSize = new System.Drawing.Size(48, 48);
            this.toolStripLifting.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLifting_Save,
            this.toolStripLifting_Settings});
            this.toolStripLifting.Location = new System.Drawing.Point(0, 0);
            this.toolStripLifting.Name = "toolStripNIOSH";
            this.toolStripLifting.Size = new System.Drawing.Size(975, 70);
            this.toolStripLifting.TabIndex = 2;
            this.toolStripLifting.Text = "toolStrip1";
            this.toolStripLifting.Visible = false;
            // 
            // toolStripNIOSH_Save
            // 
            this.toolStripLifting_Save.Image = ((System.Drawing.Image)(resources.GetObject("toolStripNIOSH_Save.Image")));
            this.toolStripLifting_Save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripLifting_Save.MergeAction = System.Windows.Forms.MergeAction.Replace;
            this.toolStripLifting_Save.Name = "toolStripNIOSH_Save";
            this.toolStripLifting_Save.Size = new System.Drawing.Size(52, 67);
            this.toolStripLifting_Save.Text = "Save";
            this.toolStripLifting_Save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolStripNIOSH_Settings
            // 
            this.toolStripLifting_Settings.Image = ((System.Drawing.Image)(resources.GetObject("toolStripNIOSH_Settings.Image")));
            this.toolStripLifting_Settings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripLifting_Settings.MergeAction = System.Windows.Forms.MergeAction.MatchOnly;
            this.toolStripLifting_Settings.Name = "toolStripNIOSH_Settings";
            this.toolStripLifting_Settings.Size = new System.Drawing.Size(53, 67);
            this.toolStripLifting_Settings.Text = "Settings";
            this.toolStripLifting_Settings.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
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
            // FrmResultsLifting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(975, 538);
            this.Controls.Add(this.rtbShowResult);
            this.Controls.Add(this.toolStripLifting);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Name = "FrmResultsLifting";
            this.Text = "Lifting model results";
            this.Shown += new System.EventHandler(this.FrmResultsLifting_Shown);
            this.toolStripLifting.ResumeLayout(false);
            this.toolStripLifting.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStripLifting;
        private System.Windows.Forms.ToolStripButton toolStripLifting_Save;
        private System.Windows.Forms.ToolStripButton toolStripLifting_Settings;
        private RichTextBox rtbShowResult;
    }
}