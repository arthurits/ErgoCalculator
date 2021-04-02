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
            this.lblTasks = new System.Windows.Forms.Label();
            this.btnAccept = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtMVC = new System.Windows.Forms.TextBox();
            this.txtTT = new System.Windows.Forms.TextBox();
            this.txtTD = new System.Windows.Forms.TextBox();
            this.txtCiclos = new System.Windows.Forms.TextBox();
            this.txtPaso = new System.Windows.Forms.TextBox();
            this.lblTDmax = new System.Windows.Forms.Label();
            this.gridVariables = new System.Windows.Forms.DataGridView();
            this.updTasks = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.gridVariables)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.updTasks)).BeginInit();
            this.SuspendLayout();
            // 
            // lblMVC
            // 
            this.lblMVC.AutoSize = true;
            this.lblMVC.Location = new System.Drawing.Point(35, 14);
            this.lblMVC.Name = "lblMVC";
            this.lblMVC.Size = new System.Drawing.Size(53, 17);
            this.lblMVC.TabIndex = 0;
            this.lblMVC.Text = "% MVC";
            // 
            // lblTT
            // 
            this.lblTT.AutoSize = true;
            this.lblTT.Location = new System.Drawing.Point(35, 41);
            this.lblTT.Name = "lblTT";
            this.lblTT.Size = new System.Drawing.Size(92, 17);
            this.lblTT.TabIndex = 1;
            this.lblTT.Text = "Work time (s)";
            // 
            // lblTD
            // 
            this.lblTD.AutoSize = true;
            this.lblTD.Location = new System.Drawing.Point(35, 68);
            this.lblTD.Name = "lblTD";
            this.lblTD.Size = new System.Drawing.Size(88, 17);
            this.lblTD.TabIndex = 2;
            this.lblTD.Text = "Rest time (s)";
            // 
            // lblCiclos
            // 
            this.lblCiclos.AutoSize = true;
            this.lblCiclos.Location = new System.Drawing.Point(35, 94);
            this.lblCiclos.Name = "lblCiclos";
            this.lblCiclos.Size = new System.Drawing.Size(113, 17);
            this.lblCiclos.TabIndex = 3;
            this.lblCiclos.Text = "Number of cicles";
            // 
            // lblTasks
            // 
            this.lblTasks.AutoSize = true;
            this.lblTasks.Location = new System.Drawing.Point(35, 121);
            this.lblTasks.Name = "lblTasks";
            this.lblTasks.Size = new System.Drawing.Size(37, 17);
            this.lblTasks.TabIndex = 4;
            this.lblTasks.Text = "Step";
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
            this.txtMVC.Location = new System.Drawing.Point(158, 11);
            this.txtMVC.Name = "txtMVC";
            this.txtMVC.Size = new System.Drawing.Size(124, 23);
            this.txtMVC.TabIndex = 0;
            this.txtMVC.Enter += new System.EventHandler(this.txtMVC_Enter);
            this.txtMVC.Validating += new System.ComponentModel.CancelEventHandler(this.txtMVC_Validating);
            // 
            // txtTT
            // 
            this.txtTT.Location = new System.Drawing.Point(158, 38);
            this.txtTT.Name = "txtTT";
            this.txtTT.Size = new System.Drawing.Size(124, 23);
            this.txtTT.TabIndex = 1;
            this.txtTT.Enter += new System.EventHandler(this.txtTT_Enter);
            // 
            // txtTD
            // 
            this.txtTD.Location = new System.Drawing.Point(158, 65);
            this.txtTD.Name = "txtTD";
            this.txtTD.Size = new System.Drawing.Size(124, 23);
            this.txtTD.TabIndex = 2;
            this.txtTD.Enter += new System.EventHandler(this.txtTD_Enter);
            // 
            // txtCiclos
            // 
            this.txtCiclos.Location = new System.Drawing.Point(158, 91);
            this.txtCiclos.Name = "txtCiclos";
            this.txtCiclos.Size = new System.Drawing.Size(124, 23);
            this.txtCiclos.TabIndex = 3;
            this.txtCiclos.Enter += new System.EventHandler(this.txtCiclos_Enter);
            // 
            // txtPaso
            // 
            this.txtPaso.Location = new System.Drawing.Point(158, 118);
            this.txtPaso.Name = "txtPaso";
            this.txtPaso.Size = new System.Drawing.Size(124, 23);
            this.txtPaso.TabIndex = 4;
            this.txtPaso.Enter += new System.EventHandler(this.txtPaso_Enter);
            // 
            // lblTDmax
            // 
            this.lblTDmax.AutoSize = true;
            this.lblTDmax.Location = new System.Drawing.Point(288, 39);
            this.lblTDmax.Name = "lblTDmax";
            this.lblTDmax.Size = new System.Drawing.Size(0, 17);
            this.lblTDmax.TabIndex = 12;
            // 
            // gridVariables
            // 
            this.gridVariables.AllowUserToAddRows = false;
            this.gridVariables.AllowUserToDeleteRows = false;
            this.gridVariables.AllowUserToResizeRows = false;
            this.gridVariables.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridVariables.Location = new System.Drawing.Point(4, 147);
            this.gridVariables.Margin = new System.Windows.Forms.Padding(4);
            this.gridVariables.MultiSelect = false;
            this.gridVariables.Name = "gridVariables";
            this.gridVariables.RowHeadersWidth = 220;
            this.gridVariables.RowTemplate.Height = 25;
            this.gridVariables.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.gridVariables.Size = new System.Drawing.Size(729, 182);
            this.gridVariables.TabIndex = 1;
            // 
            // updTasks
            // 
            this.updTasks.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.updTasks.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.updTasks.Location = new System.Drawing.Point(13, 343);
            this.updTasks.Margin = new System.Windows.Forms.Padding(4);
            this.updTasks.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.updTasks.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.updTasks.Name = "updTasks";
            this.updTasks.Size = new System.Drawing.Size(63, 23);
            this.updTasks.TabIndex = 7;
            this.updTasks.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.updTasks.ValueChanged += new System.EventHandler(this.updTasks_ValueChanged);
            // 
            // frmDataWRmodel
            // 
            this.AcceptButton = this.btnAccept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(751, 378);
            this.Controls.Add(this.updTasks);
            this.Controls.Add(this.gridVariables);
            this.Controls.Add(this.lblTDmax);
            this.Controls.Add(this.txtPaso);
            this.Controls.Add(this.txtCiclos);
            this.Controls.Add(this.txtTD);
            this.Controls.Add(this.txtTT);
            this.Controls.Add(this.txtMVC);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.lblTasks);
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
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "WR model parameters";
            ((System.ComponentModel.ISupportInitialize)(this.gridVariables)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.updTasks)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMVC;
        private System.Windows.Forms.Label lblTT;
        private System.Windows.Forms.Label lblTD;
        private System.Windows.Forms.Label lblCiclos;
        private System.Windows.Forms.Label lblTasks;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtMVC;
        private System.Windows.Forms.TextBox txtTT;
        private System.Windows.Forms.TextBox txtTD;
        private System.Windows.Forms.TextBox txtCiclos;
        private System.Windows.Forms.TextBox txtPaso;
        private System.Windows.Forms.Label lblTDmax;
        private System.Windows.Forms.DataGridView gridVariables;
        private System.Windows.Forms.NumericUpDown updTasks;
    }
}