namespace ErgoCalc
{
    partial class frmNew
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
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnAccept = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.radioButtonClick1 = new System.Windows.Forms.RadioButtonClick();
            this.radioButtonClick2 = new System.Windows.Forms.RadioButtonClick();
            this.radioButtonClick3 = new System.Windows.Forms.RadioButtonClick();
            this.radioButtonClick4 = new System.Windows.Forms.RadioButtonClick();
            this.radioButtonClick5 = new System.Windows.Forms.RadioButtonClick();
            this.radioButtonClick6 = new System.Windows.Forms.RadioButtonClick();
            this.radioButtonClick7 = new System.Windows.Forms.RadioButtonClick();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(21, 103);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(282, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Please, select a model to start working with:";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(382, 86);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // btnAccept
            // 
            this.btnAccept.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnAccept.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnAccept.Location = new System.Drawing.Point(174, 377);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(94, 29);
            this.btnAccept.TabIndex = 5;
            this.btnAccept.Text = "&Accept";
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnCancel.Location = new System.Drawing.Point(274, 377);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(94, 29);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // radioButtonClick1
            // 
            this.radioButtonClick1.AutoSize = true;
            this.radioButtonClick1.Location = new System.Drawing.Point(63, 134);
            this.radioButtonClick1.Name = "radioButtonClick1";
            this.radioButtonClick1.Size = new System.Drawing.Size(91, 21);
            this.radioButtonClick1.TabIndex = 7;
            this.radioButtonClick1.TabStop = true;
            this.radioButtonClick1.Tag = "1";
            this.radioButtonClick1.Text = "WR model";
            this.radioButtonClick1.UseVisualStyleBackColor = true;
            this.radioButtonClick1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.radioButtons_DoubleClick);
            // 
            // radioButtonClick2
            // 
            this.radioButtonClick2.AutoSize = true;
            this.radioButtonClick2.Location = new System.Drawing.Point(63, 167);
            this.radioButtonClick2.Name = "radioButtonClick2";
            this.radioButtonClick2.Size = new System.Drawing.Size(96, 21);
            this.radioButtonClick2.TabIndex = 8;
            this.radioButtonClick2.TabStop = true;
            this.radioButtonClick2.Tag = "2";
            this.radioButtonClick2.Text = "CLM model";
            this.radioButtonClick2.UseVisualStyleBackColor = true;
            this.radioButtonClick2.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.radioButtons_DoubleClick);
            // 
            // radioButtonClick3
            // 
            this.radioButtonClick3.AutoSize = true;
            this.radioButtonClick3.Location = new System.Drawing.Point(63, 200);
            this.radioButtonClick3.Name = "radioButtonClick3";
            this.radioButtonClick3.Size = new System.Drawing.Size(237, 21);
            this.radioButtonClick3.TabIndex = 9;
            this.radioButtonClick3.TabStop = true;
            this.radioButtonClick3.Tag = "3";
            this.radioButtonClick3.Text = "NIOSH equation (LI, CLI, SLI, VLI)";
            this.radioButtonClick3.UseVisualStyleBackColor = true;
            this.radioButtonClick3.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.radioButtons_DoubleClick);
            // 
            // radioButtonClick4
            // 
            this.radioButtonClick4.AutoSize = true;
            this.radioButtonClick4.Location = new System.Drawing.Point(63, 233);
            this.radioButtonClick4.Name = "radioButtonClick4";
            this.radioButtonClick4.Size = new System.Drawing.Size(268, 21);
            this.radioButtonClick4.TabIndex = 10;
            this.radioButtonClick4.TabStop = true;
            this.radioButtonClick4.Tag = "4";
            this.radioButtonClick4.Text = "Revised strain index (RSI, COSI, CUSI)";
            this.radioButtonClick4.UseVisualStyleBackColor = true;
            this.radioButtonClick4.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.radioButtons_DoubleClick);
            // 
            // radioButtonClick5
            // 
            this.radioButtonClick5.AutoSize = true;
            this.radioButtonClick5.Location = new System.Drawing.Point(63, 266);
            this.radioButtonClick5.Name = "radioButtonClick5";
            this.radioButtonClick5.Size = new System.Drawing.Size(123, 21);
            this.radioButtonClick5.TabIndex = 11;
            this.radioButtonClick5.TabStop = true;
            this.radioButtonClick5.Tag = "5";
            this.radioButtonClick5.Text = "OCRA checklist";
            this.radioButtonClick5.UseVisualStyleBackColor = true;
            this.radioButtonClick5.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.radioButtons_DoubleClick);
            // 
            // radioButtonClick6
            // 
            this.radioButtonClick6.AutoSize = true;
            this.radioButtonClick6.Location = new System.Drawing.Point(63, 299);
            this.radioButtonClick6.Name = "radioButtonClick6";
            this.radioButtonClick6.Size = new System.Drawing.Size(115, 21);
            this.radioButtonClick6.TabIndex = 12;
            this.radioButtonClick6.TabStop = true;
            this.radioButtonClick6.Tag = "6";
            this.radioButtonClick6.Text = "Metabolic rate";
            this.radioButtonClick6.UseVisualStyleBackColor = true;
            this.radioButtonClick6.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.radioButtons_DoubleClick);
            // 
            // radioButtonClick7
            // 
            this.radioButtonClick7.AutoSize = true;
            this.radioButtonClick7.Location = new System.Drawing.Point(63, 332);
            this.radioButtonClick7.Name = "radioButtonClick7";
            this.radioButtonClick7.Size = new System.Drawing.Size(208, 21);
            this.radioButtonClick7.TabIndex = 13;
            this.radioButtonClick7.TabStop = true;
            this.radioButtonClick7.Text = "Thermal comfort (PMV, PPD)";
            this.radioButtonClick7.UseVisualStyleBackColor = true;
            this.radioButtonClick7.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.radioButtons_DoubleClick);
            // 
            // frmNew
            // 
            this.AcceptButton = this.btnAccept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(382, 418);
            this.Controls.Add(this.radioButtonClick7);
            this.Controls.Add(this.radioButtonClick6);
            this.Controls.Add(this.radioButtonClick5);
            this.Controls.Add(this.radioButtonClick4);
            this.Controls.Add(this.radioButtonClick3);
            this.Controls.Add(this.radioButtonClick2);
            this.Controls.Add(this.radioButtonClick1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmNew";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "New model";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.RadioButtonClick radioButtonClick1;
        private System.Windows.Forms.RadioButtonClick radioButtonClick2;
        private System.Windows.Forms.RadioButtonClick radioButtonClick3;
        private System.Windows.Forms.RadioButtonClick radioButtonClick4;
        private System.Windows.Forms.RadioButtonClick radioButtonClick5;
        private System.Windows.Forms.RadioButtonClick radioButtonClick6;
        private System.Windows.Forms.RadioButtonClick radioButtonClick7;
    }
}