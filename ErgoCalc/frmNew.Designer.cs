namespace ErgoCalc
{
    partial class FrmNew
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
            this.lblSelect = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnAccept = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.radModelWR = new System.Windows.Forms.RadioButtonClick();
            this.radModelCLM = new System.Windows.Forms.RadioButtonClick();
            this.radModelNIOSH = new System.Windows.Forms.RadioButtonClick();
            this.radModelStrain = new System.Windows.Forms.RadioButtonClick();
            this.radModelOCRA = new System.Windows.Forms.RadioButtonClick();
            this.radModelMetabolic = new System.Windows.Forms.RadioButtonClick();
            this.radModelThermal = new System.Windows.Forms.RadioButtonClick();
            this.radModelLiberty = new System.Windows.Forms.RadioButtonClick();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblSelect
            // 
            this.lblSelect.AutoSize = true;
            this.lblSelect.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblSelect.Location = new System.Drawing.Point(21, 79);
            this.lblSelect.Name = "lblSelect";
            this.lblSelect.Size = new System.Drawing.Size(282, 17);
            this.lblSelect.TabIndex = 3;
            this.lblSelect.Text = "Please, select a model to start working with:";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(382, 61);
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
            this.btnAccept.Click += new System.EventHandler(this.Accept_Click);
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
            // radModelWR
            // 
            this.radModelWR.AutoSize = true;
            this.radModelWR.Location = new System.Drawing.Point(53, 110);
            this.radModelWR.Name = "radModelWR";
            this.radModelWR.Size = new System.Drawing.Size(91, 21);
            this.radModelWR.TabIndex = 7;
            this.radModelWR.TabStop = true;
            this.radModelWR.Tag = "1";
            this.radModelWR.Text = "WR model";
            this.radModelWR.UseVisualStyleBackColor = true;
            this.radModelWR.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.radioButtons_DoubleClick);
            // 
            // radModelCLM
            // 
            this.radModelCLM.AutoSize = true;
            this.radModelCLM.Location = new System.Drawing.Point(53, 143);
            this.radModelCLM.Name = "radModelCLM";
            this.radModelCLM.Size = new System.Drawing.Size(96, 21);
            this.radModelCLM.TabIndex = 8;
            this.radModelCLM.TabStop = true;
            this.radModelCLM.Tag = "2";
            this.radModelCLM.Text = "CLM model";
            this.radModelCLM.UseVisualStyleBackColor = true;
            this.radModelCLM.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.radioButtons_DoubleClick);
            // 
            // radModelNIOSH
            // 
            this.radModelNIOSH.AutoSize = true;
            this.radModelNIOSH.Location = new System.Drawing.Point(53, 176);
            this.radModelNIOSH.Name = "radModelNIOSH";
            this.radModelNIOSH.Size = new System.Drawing.Size(237, 21);
            this.radModelNIOSH.TabIndex = 9;
            this.radModelNIOSH.TabStop = true;
            this.radModelNIOSH.Tag = "3";
            this.radModelNIOSH.Text = "NIOSH equation (LI, CLI, SLI, VLI)";
            this.radModelNIOSH.UseVisualStyleBackColor = true;
            this.radModelNIOSH.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.radioButtons_DoubleClick);
            // 
            // radModelStrain
            // 
            this.radModelStrain.AutoSize = true;
            this.radModelStrain.Location = new System.Drawing.Point(53, 209);
            this.radModelStrain.Name = "radModelStrain";
            this.radModelStrain.Size = new System.Drawing.Size(268, 21);
            this.radModelStrain.TabIndex = 10;
            this.radModelStrain.TabStop = true;
            this.radModelStrain.Tag = "4";
            this.radModelStrain.Text = "Revised strain index (RSI, COSI, CUSI)";
            this.radModelStrain.UseVisualStyleBackColor = true;
            this.radModelStrain.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.radioButtons_DoubleClick);
            // 
            // radModelOCRA
            // 
            this.radModelOCRA.AutoSize = true;
            this.radModelOCRA.Enabled = false;
            this.radModelOCRA.Location = new System.Drawing.Point(53, 242);
            this.radModelOCRA.Name = "radModelOCRA";
            this.radModelOCRA.Size = new System.Drawing.Size(123, 21);
            this.radModelOCRA.TabIndex = 11;
            this.radModelOCRA.TabStop = true;
            this.radModelOCRA.Tag = "5";
            this.radModelOCRA.Text = "OCRA checklist";
            this.radModelOCRA.UseVisualStyleBackColor = true;
            this.radModelOCRA.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.radioButtons_DoubleClick);
            // 
            // radModelMetabolic
            // 
            this.radModelMetabolic.AutoSize = true;
            this.radModelMetabolic.Enabled = false;
            this.radModelMetabolic.Location = new System.Drawing.Point(53, 275);
            this.radModelMetabolic.Name = "radModelMetabolic";
            this.radModelMetabolic.Size = new System.Drawing.Size(115, 21);
            this.radModelMetabolic.TabIndex = 12;
            this.radModelMetabolic.TabStop = true;
            this.radModelMetabolic.Tag = "6";
            this.radModelMetabolic.Text = "Metabolic rate";
            this.radModelMetabolic.UseVisualStyleBackColor = true;
            this.radModelMetabolic.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.radioButtons_DoubleClick);
            // 
            // radModelThermal
            // 
            this.radModelThermal.AutoSize = true;
            this.radModelThermal.Location = new System.Drawing.Point(53, 308);
            this.radModelThermal.Name = "radModelThermal";
            this.radModelThermal.Size = new System.Drawing.Size(208, 21);
            this.radModelThermal.TabIndex = 13;
            this.radModelThermal.TabStop = true;
            this.radModelThermal.Text = "Thermal comfort (PMV, PPD)";
            this.radModelThermal.UseVisualStyleBackColor = true;
            this.radModelThermal.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.radioButtons_DoubleClick);
            // 
            // radModelLiberty
            // 
            this.radModelLiberty.AutoSize = true;
            this.radModelLiberty.Location = new System.Drawing.Point(53, 341);
            this.radModelLiberty.Name = "radModelLiberty";
            this.radModelLiberty.Size = new System.Drawing.Size(153, 21);
            this.radModelLiberty.TabIndex = 14;
            this.radModelLiberty.TabStop = true;
            this.radModelLiberty.Text = "LM manual handling";
            this.radModelLiberty.UseVisualStyleBackColor = true;
            this.radModelLiberty.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.radioButtons_DoubleClick);
            // 
            // FrmNew
            // 
            this.AcceptButton = this.btnAccept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(382, 418);
            this.ControlBox = false;
            this.Controls.Add(this.radModelLiberty);
            this.Controls.Add(this.radModelThermal);
            this.Controls.Add(this.radModelMetabolic);
            this.Controls.Add(this.radModelOCRA);
            this.Controls.Add(this.radModelStrain);
            this.Controls.Add(this.radModelNIOSH);
            this.Controls.Add(this.radModelCLM);
            this.Controls.Add(this.radModelWR);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblSelect);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FrmNew";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "New model";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSelect;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.RadioButtonClick radModelWR;
        private System.Windows.Forms.RadioButtonClick radModelCLM;
        private System.Windows.Forms.RadioButtonClick radModelNIOSH;
        private System.Windows.Forms.RadioButtonClick radModelStrain;
        private System.Windows.Forms.RadioButtonClick radModelOCRA;
        private System.Windows.Forms.RadioButtonClick radModelMetabolic;
        private System.Windows.Forms.RadioButtonClick radModelThermal;
        private System.Windows.Forms.RadioButtonClick radModelLiberty;
    }
}