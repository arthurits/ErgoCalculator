namespace ErgoCalc
{
    partial class FrmDataNIOSH
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
            this.updSubTasks = new System.Windows.Forms.NumericUpDown();
            this.lblSubTasks = new System.Windows.Forms.Label();
            this.btnAccept = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblConstanteLC = new System.Windows.Forms.Label();
            this.txtConstanteLC = new System.Windows.Forms.TextBox();
            this.listViewTasks = new System.Windows.Forms.ListViewEx();
            this.tabData = new System.Windows.Forms.TabControl();
            this.tabSubtasks = new System.Windows.Forms.TabPage();
            this.gridVariables = new System.Windows.Forms.DataGridView();
            this.tabTasks = new System.Windows.Forms.TabPage();
            this.lblTasks = new System.Windows.Forms.Label();
            this.updTasks = new System.Windows.Forms.NumericUpDown();
            this.tabDummy = new System.Windows.Forms.TabControl();
            this.grpIndex = new System.Windows.Forms.GroupBox();
            this.radVLI = new System.Windows.Forms.RadioButton();
            this.radSLI = new System.Windows.Forms.RadioButton();
            this.radCLI = new System.Windows.Forms.RadioButton();
            this.radLI = new System.Windows.Forms.RadioButton();
            this.btnExample = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.updSubTasks)).BeginInit();
            this.tabData.SuspendLayout();
            this.tabSubtasks.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridVariables)).BeginInit();
            this.tabTasks.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.updTasks)).BeginInit();
            this.grpIndex.SuspendLayout();
            this.SuspendLayout();
            // 
            // updSubTasks
            // 
            this.updSubTasks.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.updSubTasks.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.updSubTasks.Location = new System.Drawing.Point(140, 288);
            this.updSubTasks.Margin = new System.Windows.Forms.Padding(4);
            this.updSubTasks.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.updSubTasks.Name = "updSubTasks";
            this.updSubTasks.Size = new System.Drawing.Size(63, 22);
            this.updSubTasks.TabIndex = 1;
            this.updSubTasks.ValueChanged += new System.EventHandler(this.SubTasks_ValueChanged);
            // 
            // lblSubTasks
            // 
            this.lblSubTasks.AutoSize = true;
            this.lblSubTasks.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblSubTasks.Location = new System.Drawing.Point(4, 292);
            this.lblSubTasks.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSubTasks.Name = "lblSubTasks";
            this.lblSubTasks.Size = new System.Drawing.Size(104, 16);
            this.lblSubTasks.TabIndex = 2;
            this.lblSubTasks.Text = "Number of tasks";
            this.lblSubTasks.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnOK
            // 
            this.btnAccept.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnAccept.Location = new System.Drawing.Point(464, 462);
            this.btnAccept.Margin = new System.Windows.Forms.Padding(4);
            this.btnAccept.Name = "btnOK";
            this.btnAccept.Size = new System.Drawing.Size(133, 34);
            this.btnAccept.TabIndex = 4;
            this.btnAccept.Text = "&Accept";
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.Accept_Click);
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
            this.btnCancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // lblConstanteLC
            // 
            this.lblConstanteLC.AutoSize = true;
            this.lblConstanteLC.Enabled = false;
            this.lblConstanteLC.Location = new System.Drawing.Point(16, 471);
            this.lblConstanteLC.Name = "lblConstanteLC";
            this.lblConstanteLC.Size = new System.Drawing.Size(152, 17);
            this.lblConstanteLC.TabIndex = 6;
            this.lblConstanteLC.Text = "Load constant (LC, kg)";
            // 
            // txtConstanteLC
            // 
            this.txtConstanteLC.Enabled = false;
            this.txtConstanteLC.Location = new System.Drawing.Point(174, 469);
            this.txtConstanteLC.Name = "txtConstanteLC";
            this.txtConstanteLC.Size = new System.Drawing.Size(33, 23);
            this.txtConstanteLC.TabIndex = 3;
            // 
            // listViewTasks
            // 
            this.listViewTasks.AllowDrop = true;
            this.listViewTasks.DummyName = "Dummy";
            this.listViewTasks.FullRowSelect = true;
            this.listViewTasks.Location = new System.Drawing.Point(0, 34);
            this.listViewTasks.Name = "listViewTasks";
            this.listViewTasks.Size = new System.Drawing.Size(737, 294);
            this.listViewTasks.TabIndex = 7;
            this.listViewTasks.UseCompatibleStateImageBehavior = false;
            // 
            // tabData
            // 
            this.tabData.Controls.Add(this.tabSubtasks);
            this.tabData.Controls.Add(this.tabTasks);
            this.tabData.Location = new System.Drawing.Point(16, 15);
            this.tabData.Name = "tabData";
            this.tabData.SelectedIndex = 0;
            this.tabData.Size = new System.Drawing.Size(745, 357);
            this.tabData.TabIndex = 8;
            this.tabData.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabData_Selected);
            // 
            // tabSubtasks
            // 
            this.tabSubtasks.BackColor = System.Drawing.SystemColors.Control;
            this.tabSubtasks.Controls.Add(this.gridVariables);
            this.tabSubtasks.Controls.Add(this.lblSubTasks);
            this.tabSubtasks.Controls.Add(this.updSubTasks);
            this.tabSubtasks.Location = new System.Drawing.Point(4, 25);
            this.tabSubtasks.Name = "tabSubtasks";
            this.tabSubtasks.Padding = new System.Windows.Forms.Padding(3);
            this.tabSubtasks.Size = new System.Drawing.Size(737, 328);
            this.tabSubtasks.TabIndex = 0;
            this.tabSubtasks.Text = "Subtasks";
            // 
            // gridVariables
            // 
            this.gridVariables.AllowUserToAddRows = false;
            this.gridVariables.AllowUserToDeleteRows = false;
            this.gridVariables.AllowUserToResizeRows = false;
            this.gridVariables.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridVariables.Location = new System.Drawing.Point(0, 0);
            this.gridVariables.Margin = new System.Windows.Forms.Padding(4);
            this.gridVariables.MultiSelect = false;
            this.gridVariables.Name = "gridVariables";
            this.gridVariables.RowHeadersWidth = 180;
            this.gridVariables.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.gridVariables.Size = new System.Drawing.Size(737, 264);
            this.gridVariables.TabIndex = 1;
            // 
            // tabTasks
            // 
            this.tabTasks.BackColor = System.Drawing.SystemColors.Control;
            this.tabTasks.Controls.Add(this.lblTasks);
            this.tabTasks.Controls.Add(this.updTasks);
            this.tabTasks.Controls.Add(this.listViewTasks);
            this.tabTasks.Location = new System.Drawing.Point(4, 24);
            this.tabTasks.Name = "tabTasks";
            this.tabTasks.Padding = new System.Windows.Forms.Padding(3);
            this.tabTasks.Size = new System.Drawing.Size(737, 329);
            this.tabTasks.TabIndex = 1;
            this.tabTasks.Text = "Tasks";
            // 
            // lblTasks
            // 
            this.lblTasks.AutoSize = true;
            this.lblTasks.Location = new System.Drawing.Point(12, 7);
            this.lblTasks.Name = "lblTasks";
            this.lblTasks.Size = new System.Drawing.Size(111, 17);
            this.lblTasks.TabIndex = 9;
            this.lblTasks.Text = "Number of tasks";
            // 
            // updTasks
            // 
            this.updTasks.Location = new System.Drawing.Point(129, 7);
            this.updTasks.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.updTasks.Name = "updTasks";
            this.updTasks.Size = new System.Drawing.Size(47, 23);
            this.updTasks.TabIndex = 8;
            this.updTasks.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.updTasks.ValueChanged += new System.EventHandler(this.Tasks_ValueChanged);
            // 
            // tabDummy
            // 
            this.tabDummy.Location = new System.Drawing.Point(669, 380);
            this.tabDummy.Name = "tabDummy";
            this.tabDummy.SelectedIndex = 0;
            this.tabDummy.Size = new System.Drawing.Size(84, 75);
            this.tabDummy.TabIndex = 9;
            this.tabDummy.Visible = false;
            // 
            // grpIndex
            // 
            this.grpIndex.Controls.Add(this.radVLI);
            this.grpIndex.Controls.Add(this.radSLI);
            this.grpIndex.Controls.Add(this.radCLI);
            this.grpIndex.Controls.Add(this.radLI);
            this.grpIndex.Location = new System.Drawing.Point(16, 378);
            this.grpIndex.Name = "grpIndex";
            this.grpIndex.Size = new System.Drawing.Size(314, 65);
            this.grpIndex.TabIndex = 10;
            this.grpIndex.TabStop = false;
            this.grpIndex.Text = "Index type";
            // 
            // radVLI
            // 
            this.radVLI.AutoSize = true;
            this.radVLI.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.radVLI.Enabled = false;
            this.radVLI.Location = new System.Drawing.Point(242, 30);
            this.radVLI.Name = "radVLI";
            this.radVLI.Size = new System.Drawing.Size(46, 21);
            this.radVLI.TabIndex = 3;
            this.radVLI.TabStop = true;
            this.radVLI.Tag = "3";
            this.radVLI.Text = "VLI";
            this.radVLI.UseVisualStyleBackColor = true;
            this.radVLI.CheckedChanged += new System.EventHandler(this.rad_CheckedChanged);
            // 
            // radSLI
            // 
            this.radSLI.AutoSize = true;
            this.radSLI.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.radSLI.Enabled = false;
            this.radSLI.Location = new System.Drawing.Point(161, 30);
            this.radSLI.Name = "radSLI";
            this.radSLI.Size = new System.Drawing.Size(46, 21);
            this.radSLI.TabIndex = 2;
            this.radSLI.TabStop = true;
            this.radSLI.Tag = "2";
            this.radSLI.Text = "SLI";
            this.radSLI.UseVisualStyleBackColor = true;
            this.radSLI.CheckedChanged += new System.EventHandler(this.rad_CheckedChanged);
            // 
            // radCLI
            // 
            this.radCLI.AutoSize = true;
            this.radCLI.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.radCLI.Enabled = false;
            this.radCLI.Location = new System.Drawing.Point(80, 30);
            this.radCLI.Name = "radCLI";
            this.radCLI.Size = new System.Drawing.Size(46, 21);
            this.radCLI.TabIndex = 1;
            this.radCLI.TabStop = true;
            this.radCLI.Tag = "1";
            this.radCLI.Text = "CLI";
            this.radCLI.UseVisualStyleBackColor = true;
            this.radCLI.CheckedChanged += new System.EventHandler(this.rad_CheckedChanged);
            // 
            // radLI
            // 
            this.radLI.AutoSize = true;
            this.radLI.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.radLI.Checked = true;
            this.radLI.Location = new System.Drawing.Point(8, 30);
            this.radLI.Name = "radLI";
            this.radLI.Size = new System.Drawing.Size(37, 21);
            this.radLI.TabIndex = 0;
            this.radLI.TabStop = true;
            this.radLI.Tag = "0";
            this.radLI.Text = "LI";
            this.radLI.UseVisualStyleBackColor = true;
            this.radLI.CheckedChanged += new System.EventHandler(this.rad_CheckedChanged);
            // 
            // btnExample
            // 
            this.btnExample.Location = new System.Drawing.Point(306, 462);
            this.btnExample.Name = "btnExample";
            this.btnExample.Size = new System.Drawing.Size(133, 34);
            this.btnExample.TabIndex = 11;
            this.btnExample.Text = "Example";
            this.btnExample.UseVisualStyleBackColor = true;
            this.btnExample.Click += new System.EventHandler(this.Example_Click);
            // 
            // FrmDataNIOSH
            // 
            this.AcceptButton = this.btnAccept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(774, 511);
            this.Controls.Add(this.btnExample);
            this.Controls.Add(this.grpIndex);
            this.Controls.Add(this.tabDummy);
            this.Controls.Add(this.tabData);
            this.Controls.Add(this.txtConstanteLC);
            this.Controls.Add(this.lblConstanteLC);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAccept);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmDataNIOSH";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "NIOSH model data";
            ((System.ComponentModel.ISupportInitialize)(this.updSubTasks)).EndInit();
            this.tabData.ResumeLayout(false);
            this.tabSubtasks.ResumeLayout(false);
            this.tabSubtasks.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridVariables)).EndInit();
            this.tabTasks.ResumeLayout(false);
            this.tabTasks.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.updTasks)).EndInit();
            this.grpIndex.ResumeLayout(false);
            this.grpIndex.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown updSubTasks;
        private System.Windows.Forms.Label lblSubTasks;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblConstanteLC;
        private System.Windows.Forms.TextBox txtConstanteLC;
        private System.Windows.Forms.ListViewEx listViewTasks;
        private System.Windows.Forms.TabControl tabData;
        private System.Windows.Forms.TabPage tabSubtasks;
        private System.Windows.Forms.TabPage tabTasks;
        private System.Windows.Forms.DataGridView gridVariables;
        private System.Windows.Forms.TabControl tabDummy;
        private System.Windows.Forms.Label lblTasks;
        private System.Windows.Forms.NumericUpDown updTasks;
        private System.Windows.Forms.GroupBox grpIndex;
        private System.Windows.Forms.RadioButton radVLI;
        private System.Windows.Forms.RadioButton radSLI;
        private System.Windows.Forms.RadioButton radCLI;
        private System.Windows.Forms.RadioButton radLI;
        private System.Windows.Forms.Button btnExample;
    }
}