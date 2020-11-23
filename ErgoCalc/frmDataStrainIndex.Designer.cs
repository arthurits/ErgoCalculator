namespace ErgoCalc
{
    partial class frmDataStrainIndex
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
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupIndex = new System.Windows.Forms.GroupBox();
            this.radCUSI = new System.Windows.Forms.RadioButton();
            this.radCOSI = new System.Windows.Forms.RadioButton();
            this.radRSI = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.updTasks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridVariables)).BeginInit();
            this.groupIndex.SuspendLayout();
            this.SuspendLayout();
            // 
            // updTasks
            // 
            this.updTasks.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.updTasks.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.updTasks.Location = new System.Drawing.Point(152, 405);
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
            this.updTasks.TabIndex = 1;
            this.updTasks.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.updTasks.ValueChanged += new System.EventHandler(this.updTasks_ValueChanged);
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
            this.gridVariables.RowHeadersWidth = 220;
            this.gridVariables.RowTemplate.Height = 25;
            this.gridVariables.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.gridVariables.Size = new System.Drawing.Size(745, 374);
            this.gridVariables.TabIndex = 0;
            // 
            // lblTasks
            // 
            this.lblTasks.AutoSize = true;
            this.lblTasks.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTasks.Location = new System.Drawing.Point(16, 409);
            this.lblTasks.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTasks.Name = "lblTasks";
            this.lblTasks.Size = new System.Drawing.Size(111, 17);
            this.lblTasks.TabIndex = 2;
            this.lblTasks.Text = "Number of tasks";
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(464, 462);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(133, 34);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "&Accept";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(620, 462);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(133, 34);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupIndex
            // 
            this.groupIndex.Controls.Add(this.radCUSI);
            this.groupIndex.Controls.Add(this.radCOSI);
            this.groupIndex.Controls.Add(this.radRSI);
            this.groupIndex.Location = new System.Drawing.Point(19, 435);
            this.groupIndex.Name = "groupIndex";
            this.groupIndex.Size = new System.Drawing.Size(328, 61);
            this.groupIndex.TabIndex = 6;
            this.groupIndex.TabStop = false;
            this.groupIndex.Text = "Index type";
            // 
            // radCUSI
            // 
            this.radCUSI.AutoSize = true;
            this.radCUSI.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.radCUSI.Location = new System.Drawing.Point(215, 30);
            this.radCUSI.Name = "radCUSI";
            this.radCUSI.Size = new System.Drawing.Size(57, 21);
            this.radCUSI.TabIndex = 2;
            this.radCUSI.TabStop = true;
            this.radCUSI.Text = "CUSI";
            this.radCUSI.UseVisualStyleBackColor = true;
            this.radCUSI.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // radCOSI
            // 
            this.radCOSI.AutoSize = true;
            this.radCOSI.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.radCOSI.Location = new System.Drawing.Point(120, 30);
            this.radCOSI.Name = "radCOSI";
            this.radCOSI.Size = new System.Drawing.Size(58, 21);
            this.radCOSI.TabIndex = 1;
            this.radCOSI.TabStop = true;
            this.radCOSI.Text = "COSI";
            this.radCOSI.UseVisualStyleBackColor = true;
            this.radCOSI.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // radRSI
            // 
            this.radRSI.AutoSize = true;
            this.radRSI.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.radRSI.Checked = true;
            this.radRSI.Location = new System.Drawing.Point(38, 30);
            this.radRSI.Name = "radRSI";
            this.radRSI.Size = new System.Drawing.Size(48, 21);
            this.radRSI.TabIndex = 0;
            this.radRSI.TabStop = true;
            this.radRSI.Text = "RSI";
            this.radRSI.UseVisualStyleBackColor = true;
            this.radRSI.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // frmDataStrainIndex
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(774, 511);
            this.Controls.Add(this.groupIndex);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblTasks);
            this.Controls.Add(this.gridVariables);
            this.Controls.Add(this.updTasks);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDataStrainIndex";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Strain index data";
            ((System.ComponentModel.ISupportInitialize)(this.updTasks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridVariables)).EndInit();
            this.groupIndex.ResumeLayout(false);
            this.groupIndex.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown updTasks;
        private System.Windows.Forms.DataGridView gridVariables;
        private System.Windows.Forms.Label lblTasks;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupIndex;
        private System.Windows.Forms.RadioButton radCUSI;
        private System.Windows.Forms.RadioButton radCOSI;
        private System.Windows.Forms.RadioButton radRSI;
    }
}