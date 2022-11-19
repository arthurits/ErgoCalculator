namespace ErgoCalc
{
    partial class FrmZoom
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
            this.trackZoomFactor = new System.Windows.Forms.TrackBar();
            this.updZoomFactor = new System.Windows.Forms.NumericUpDown();
            this.lblZoomFactor = new System.Windows.Forms.Label();
            this.btnAccept = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.trackZoomFactor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.updZoomFactor)).BeginInit();
            this.SuspendLayout();
            // 
            // trackZoomFactor
            // 
            this.trackZoomFactor.Location = new System.Drawing.Point(12, 61);
            this.trackZoomFactor.Maximum = 500;
            this.trackZoomFactor.Name = "trackZoomFactor";
            this.trackZoomFactor.Size = new System.Drawing.Size(312, 45);
            this.trackZoomFactor.TabIndex = 1;
            this.trackZoomFactor.TickFrequency = 10;
            this.trackZoomFactor.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackZoomFactor.ValueChanged += new System.EventHandler(this.trackZoom_ValueChanged);
            // 
            // updZoomFactor
            // 
            this.updZoomFactor.Location = new System.Drawing.Point(114, 18);
            this.updZoomFactor.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.updZoomFactor.Name = "updZoomFactor";
            this.updZoomFactor.Size = new System.Drawing.Size(50, 25);
            this.updZoomFactor.TabIndex = 0;
            this.updZoomFactor.ValueChanged += new System.EventHandler(this.updZoom_ValueChanged);
            // 
            // lblZoomFactor
            // 
            this.lblZoomFactor.AutoSize = true;
            this.lblZoomFactor.Location = new System.Drawing.Point(12, 20);
            this.lblZoomFactor.Name = "lblZoomFactor";
            this.lblZoomFactor.Size = new System.Drawing.Size(97, 19);
            this.lblZoomFactor.TabIndex = 2;
            this.lblZoomFactor.Text = "Set zoom level";
            // 
            // btnAccept
            // 
            this.btnAccept.Location = new System.Drawing.Point(234, 126);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(90, 32);
            this.btnAccept.TabIndex = 3;
            this.btnAccept.Text = "&Accept";
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.Accept);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(129, 126);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 32);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // FrmZoom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(336, 170);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.lblZoomFactor);
            this.Controls.Add(this.updZoomFactor);
            this.Controls.Add(this.trackZoomFactor);
            this.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmZoom";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Zoom level";
            ((System.ComponentModel.ISupportInitialize)(this.trackZoomFactor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.updZoomFactor)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TrackBar trackZoomFactor;
        private NumericUpDown updZoomFactor;
        private Label lblZoomFactor;
        private Button btnAccept;
        private Button btnCancel;
    }
}