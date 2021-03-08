namespace ErgoCalc
{
    partial class frmDataWRmodel
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
            this.lblMVC = new System.Windows.Forms.Label();
            this.lblTT = new System.Windows.Forms.Label();
            this.lblTD = new System.Windows.Forms.Label();
            this.lblCiclos = new System.Windows.Forms.Label();
            this.lblPaso = new System.Windows.Forms.Label();
            this.btnAccept = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtMVC = new System.Windows.Forms.TextBox();
            this.txtTT = new System.Windows.Forms.TextBox();
            this.txtTD = new System.Windows.Forms.TextBox();
            this.txtCiclos = new System.Windows.Forms.TextBox();
            this.txtPaso = new System.Windows.Forms.TextBox();
            this.lblTDmax = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblMVC
            // 
            this.lblMVC.AutoSize = true;
            this.lblMVC.Location = new System.Drawing.Point(35, 96);
            this.lblMVC.Name = "lblMVC";
            this.lblMVC.Size = new System.Drawing.Size(53, 17);
            this.lblMVC.TabIndex = 0;
            this.lblMVC.Text = "% MVC";
            // 
            // lblTT
            // 
            this.lblTT.AutoSize = true;
            this.lblTT.Location = new System.Drawing.Point(35, 133);
            this.lblTT.Name = "lblTT";
            this.lblTT.Size = new System.Drawing.Size(92, 17);
            this.lblTT.TabIndex = 1;
            this.lblTT.Text = "Work time (s)";
            // 
            // lblTD
            // 
            this.lblTD.AutoSize = true;
            this.lblTD.Location = new System.Drawing.Point(35, 170);
            this.lblTD.Name = "lblTD";
            this.lblTD.Size = new System.Drawing.Size(88, 17);
            this.lblTD.TabIndex = 2;
            this.lblTD.Text = "Rest time (s)";
            // 
            // lblCiclos
            // 
            this.lblCiclos.AutoSize = true;
            this.lblCiclos.Location = new System.Drawing.Point(35, 207);
            this.lblCiclos.Name = "lblCiclos";
            this.lblCiclos.Size = new System.Drawing.Size(113, 17);
            this.lblCiclos.TabIndex = 3;
            this.lblCiclos.Text = "Number of cicles";
            // 
            // lblPaso
            // 
            this.lblPaso.AutoSize = true;
            this.lblPaso.Location = new System.Drawing.Point(35, 244);
            this.lblPaso.Name = "lblPaso";
            this.lblPaso.Size = new System.Drawing.Size(37, 17);
            this.lblPaso.TabIndex = 4;
            this.lblPaso.Text = "Step";
            // 
            // btnAccept
            // 
            this.btnAccept.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnAccept.Location = new System.Drawing.Point(113, 336);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(95, 30);
            this.btnAccept.TabIndex = 5;
            this.btnAccept.Text = "&Accept";
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(214, 336);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(95, 30);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // txtMVC
            // 
            this.txtMVC.Location = new System.Drawing.Point(158, 93);
            this.txtMVC.Name = "txtMVC";
            this.txtMVC.Size = new System.Drawing.Size(124, 23);
            this.txtMVC.TabIndex = 0;
            this.txtMVC.Enter += new System.EventHandler(this.txtMVC_Enter);
            this.txtMVC.Validating += new System.ComponentModel.CancelEventHandler(this.txtMVC_Validating);
            // 
            // txtTT
            // 
            this.txtTT.Location = new System.Drawing.Point(158, 130);
            this.txtTT.Name = "txtTT";
            this.txtTT.Size = new System.Drawing.Size(124, 23);
            this.txtTT.TabIndex = 1;
            this.txtTT.Enter += new System.EventHandler(this.txtTT_Enter);
            // 
            // txtTD
            // 
            this.txtTD.Location = new System.Drawing.Point(158, 167);
            this.txtTD.Name = "txtTD";
            this.txtTD.Size = new System.Drawing.Size(124, 23);
            this.txtTD.TabIndex = 2;
            this.txtTD.Enter += new System.EventHandler(this.txtTD_Enter);
            // 
            // txtCiclos
            // 
            this.txtCiclos.Location = new System.Drawing.Point(158, 204);
            this.txtCiclos.Name = "txtCiclos";
            this.txtCiclos.Size = new System.Drawing.Size(124, 23);
            this.txtCiclos.TabIndex = 3;
            this.txtCiclos.Enter += new System.EventHandler(this.txtCiclos_Enter);
            // 
            // txtPaso
            // 
            this.txtPaso.Location = new System.Drawing.Point(158, 241);
            this.txtPaso.Name = "txtPaso";
            this.txtPaso.Size = new System.Drawing.Size(124, 23);
            this.txtPaso.TabIndex = 4;
            this.txtPaso.Enter += new System.EventHandler(this.txtPaso_Enter);
            // 
            // lblTDmax
            // 
            this.lblTDmax.AutoSize = true;
            this.lblTDmax.Location = new System.Drawing.Point(288, 133);
            this.lblTDmax.Name = "lblTDmax";
            this.lblTDmax.Size = new System.Drawing.Size(0, 17);
            this.lblTDmax.TabIndex = 12;
            // 
            // frmDataWRmodel
            // 
            this.AcceptButton = this.btnAccept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(321, 378);
            this.Controls.Add(this.lblTDmax);
            this.Controls.Add(this.txtPaso);
            this.Controls.Add(this.txtCiclos);
            this.Controls.Add(this.txtTD);
            this.Controls.Add(this.txtTT);
            this.Controls.Add(this.txtMVC);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.lblPaso);
            this.Controls.Add(this.lblCiclos);
            this.Controls.Add(this.lblTD);
            this.Controls.Add(this.lblTT);
            this.Controls.Add(this.lblMVC);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDataWRmodel";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "WR model parameters";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMVC;
        private System.Windows.Forms.Label lblTT;
        private System.Windows.Forms.Label lblTD;
        private System.Windows.Forms.Label lblCiclos;
        private System.Windows.Forms.Label lblPaso;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtMVC;
        private System.Windows.Forms.TextBox txtTT;
        private System.Windows.Forms.TextBox txtTD;
        private System.Windows.Forms.TextBox txtCiclos;
        private System.Windows.Forms.TextBox txtPaso;
        private System.Windows.Forms.Label lblTDmax;
    }
}