namespace ErgoCalc
{
    partial class FrmDataCLM
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
            this.updTasks = new System.Windows.Forms.NumericUpDown();
            this.gridVariables = new System.Windows.Forms.DataGridView();
            this.lblTasks = new System.Windows.Forms.Label();
            this.btnAccept = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnExample = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.updTasks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridVariables)).BeginInit();
            this.SuspendLayout();
            // 
            // updTasks
            // 
            this.updTasks.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.updTasks.Location = new System.Drawing.Point(152, 389);
            this.updTasks.Margin = new System.Windows.Forms.Padding(4);
            this.updTasks.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.updTasks.Name = "updTasks";
            this.updTasks.Size = new System.Drawing.Size(63, 23);
            this.updTasks.TabIndex = 0;
            this.updTasks.ValueChanged += new System.EventHandler(this.Tasks_ValueChanged);
            // 
            // gridVariables
            // 
            this.gridVariables.AllowUserToAddRows = false;
            this.gridVariables.AllowUserToDeleteRows = false;
            this.gridVariables.AllowUserToResizeRows = false;
            this.gridVariables.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridVariables.Location = new System.Drawing.Point(16, 15);
            this.gridVariables.Margin = new System.Windows.Forms.Padding(4);
            this.gridVariables.MultiSelect = false;
            this.gridVariables.Name = "gridVariables";
            this.gridVariables.RowHeadersWidth = 180;
            this.gridVariables.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.gridVariables.Size = new System.Drawing.Size(709, 366);
            this.gridVariables.TabIndex = 1;
            // 
            // lblTasks
            // 
            this.lblTasks.AutoSize = true;
            this.lblTasks.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblTasks.Location = new System.Drawing.Point(16, 391);
            this.lblTasks.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTasks.Name = "lblTasks";
            this.lblTasks.Size = new System.Drawing.Size(111, 17);
            this.lblTasks.TabIndex = 2;
            this.lblTasks.Text = "Number of tasks";
            // 
            // btnOK
            // 
            this.btnAccept.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnAccept.Location = new System.Drawing.Point(436, 438);
            this.btnAccept.Margin = new System.Windows.Forms.Padding(4);
            this.btnAccept.Name = "btnOK";
            this.btnAccept.Size = new System.Drawing.Size(133, 34);
            this.btnAccept.TabIndex = 3;
            this.btnAccept.Text = "&Accept";
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.Accept_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(592, 438);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(133, 34);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // btnExample
            // 
            this.btnExample.Location = new System.Drawing.Point(16, 438);
            this.btnExample.Name = "btnExample";
            this.btnExample.Size = new System.Drawing.Size(133, 34);
            this.btnExample.TabIndex = 5;
            this.btnExample.Text = "&Example";
            this.btnExample.UseVisualStyleBackColor = true;
            this.btnExample.Click += new System.EventHandler(this.Example_Click);
            // 
            // frmDataCLM
            // 
            this.AcceptButton = this.btnAccept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(743, 487);
            this.Controls.Add(this.btnExample);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.lblTasks);
            this.Controls.Add(this.gridVariables);
            this.Controls.Add(this.updTasks);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDataCLM";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "CLM model data";
            ((System.ComponentModel.ISupportInitialize)(this.updTasks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridVariables)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown updTasks;
        private System.Windows.Forms.DataGridView gridVariables;
        private System.Windows.Forms.Label lblTasks;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnExample;
    }
}