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
            this.trackZoom = new System.Windows.Forms.TrackBar();
            this.updZoom = new System.Windows.Forms.NumericUpDown();
            this.lblZoom = new System.Windows.Forms.Label();
            this.btnAccept = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.trackZoom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.updZoom)).BeginInit();
            this.SuspendLayout();
            // 
            // trackZoom
            // 
            this.trackZoom.Location = new System.Drawing.Point(12, 61);
            this.trackZoom.Maximum = 50;
            this.trackZoom.Name = "trackZoom";
            this.trackZoom.Size = new System.Drawing.Size(312, 45);
            this.trackZoom.TabIndex = 1;
            this.trackZoom.TickFrequency = 10;
            this.trackZoom.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackZoom.ValueChanged += new System.EventHandler(this.trackZoom_ValueChanged);
            // 
            // updZoom
            // 
            this.updZoom.DecimalPlaces = 1;
            this.updZoom.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.updZoom.Location = new System.Drawing.Point(114, 18);
            this.updZoom.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.updZoom.Name = "updZoom";
            this.updZoom.Size = new System.Drawing.Size(50, 25);
            this.updZoom.TabIndex = 0;
            this.updZoom.ValueChanged += new System.EventHandler(this.updZoom_ValueChanged);
            // 
            // lblZoom
            // 
            this.lblZoom.AutoSize = true;
            this.lblZoom.Location = new System.Drawing.Point(12, 20);
            this.lblZoom.Name = "lblZoom";
            this.lblZoom.Size = new System.Drawing.Size(97, 19);
            this.lblZoom.TabIndex = 2;
            this.lblZoom.Text = "Set zoom level";
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
            this.Controls.Add(this.lblZoom);
            this.Controls.Add(this.updZoom);
            this.Controls.Add(this.trackZoom);
            this.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmZoom";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Zoom level";
            ((System.ComponentModel.ISupportInitialize)(this.trackZoom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.updZoom)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TrackBar trackZoom;
        private NumericUpDown updZoom;
        private Label lblZoom;
        private Button btnAccept;
        private Button btnCancel;
    }
}