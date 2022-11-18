namespace ErgoCalc
{
    partial class FrmDataStrainIndex
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
            this.btnAccept = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tabDataStrain = new System.Windows.Forms.TabControl();
            this.tabSubtasks = new System.Windows.Forms.TabPage();
            this.groupIndex = new System.Windows.Forms.GroupBox();
            this.radCUSI = new System.Windows.Forms.RadioButton();
            this.radCOSI = new System.Windows.Forms.RadioButton();
            this.radRSI = new System.Windows.Forms.RadioButton();
            this.lblSubtasks = new System.Windows.Forms.Label();
            this.updSubtasks = new System.Windows.Forms.NumericUpDown();
            this.gridVariables = new System.Windows.Forms.DataGridView();
            this.tabTasks = new System.Windows.Forms.TabPage();
            this.listViewTasks = new System.Windows.Forms.ListViewEx();
            this.lblTasks = new System.Windows.Forms.Label();
            this.updTasks = new System.Windows.Forms.NumericUpDown();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.tabDummy = new System.Windows.Forms.TabControl();
            this.btnExample = new System.Windows.Forms.Button();
            this.tabDataStrain.SuspendLayout();
            this.tabSubtasks.SuspendLayout();
            this.groupIndex.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.updSubtasks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridVariables)).BeginInit();
            this.tabTasks.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.updTasks)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnAccept.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnAccept.Location = new System.Drawing.Point(468, 464);
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
            this.btnCancel.Location = new System.Drawing.Point(624, 464);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(133, 34);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // tabDataStrain
            // 
            this.tabDataStrain.Controls.Add(this.tabSubtasks);
            this.tabDataStrain.Controls.Add(this.tabTasks);
            this.tabDataStrain.Location = new System.Drawing.Point(16, 3);
            this.tabDataStrain.Name = "tabDataStrain";
            this.tabDataStrain.SelectedIndex = 0;
            this.tabDataStrain.Size = new System.Drawing.Size(745, 454);
            this.tabDataStrain.TabIndex = 7;
            this.tabDataStrain.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabDataStrain_Selected);
            // 
            // tabSubtasks
            // 
            this.tabSubtasks.BackColor = System.Drawing.SystemColors.Control;
            this.tabSubtasks.Controls.Add(this.groupIndex);
            this.tabSubtasks.Controls.Add(this.lblSubtasks);
            this.tabSubtasks.Controls.Add(this.updSubtasks);
            this.tabSubtasks.Controls.Add(this.gridVariables);
            this.tabSubtasks.Location = new System.Drawing.Point(4, 25);
            this.tabSubtasks.Name = "tabSubtasks";
            this.tabSubtasks.Padding = new System.Windows.Forms.Padding(3);
            this.tabSubtasks.Size = new System.Drawing.Size(737, 425);
            this.tabSubtasks.TabIndex = 0;
            this.tabSubtasks.Text = "Subtasks data";
            // 
            // groupIndex
            // 
            this.groupIndex.Controls.Add(this.radCUSI);
            this.groupIndex.Controls.Add(this.radCOSI);
            this.groupIndex.Controls.Add(this.radRSI);
            this.groupIndex.Location = new System.Drawing.Point(6, 316);
            this.groupIndex.Name = "groupIndex";
            this.groupIndex.Size = new System.Drawing.Size(278, 61);
            this.groupIndex.TabIndex = 9;
            this.groupIndex.TabStop = false;
            this.groupIndex.Text = "Index type";
            // 
            // radCUSI
            // 
            this.radCUSI.AutoSize = true;
            this.radCUSI.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.radCUSI.Enabled = false;
            this.radCUSI.Location = new System.Drawing.Point(196, 28);
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
            this.radCOSI.Enabled = false;
            this.radCOSI.Location = new System.Drawing.Point(101, 28);
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
            this.radRSI.Location = new System.Drawing.Point(19, 28);
            this.radRSI.Name = "radRSI";
            this.radRSI.Size = new System.Drawing.Size(48, 21);
            this.radRSI.TabIndex = 0;
            this.radRSI.TabStop = true;
            this.radRSI.Text = "RSI";
            this.radRSI.UseVisualStyleBackColor = true;
            this.radRSI.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // lblSubtasks
            // 
            this.lblSubtasks.AutoSize = true;
            this.lblSubtasks.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblSubtasks.Location = new System.Drawing.Point(3, 389);
            this.lblSubtasks.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSubtasks.Name = "lblSubtasks";
            this.lblSubtasks.Size = new System.Drawing.Size(111, 17);
            this.lblSubtasks.TabIndex = 8;
            this.lblSubtasks.Text = "Number of tasks";
            // 
            // updSubtasks
            // 
            this.updSubtasks.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.updSubtasks.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.updSubtasks.Location = new System.Drawing.Point(139, 389);
            this.updSubtasks.Margin = new System.Windows.Forms.Padding(4);
            this.updSubtasks.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.updSubtasks.Name = "updSubtasks";
            this.updSubtasks.Size = new System.Drawing.Size(63, 23);
            this.updSubtasks.TabIndex = 7;
            this.updSubtasks.ValueChanged += new System.EventHandler(this.Subtasks_ValueChanged);
            // 
            // gridVariables
            // 
            this.gridVariables.AllowUserToAddRows = false;
            this.gridVariables.AllowUserToDeleteRows = false;
            this.gridVariables.AllowUserToResizeRows = false;
            this.gridVariables.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridVariables.Location = new System.Drawing.Point(4, 4);
            this.gridVariables.Margin = new System.Windows.Forms.Padding(4);
            this.gridVariables.MultiSelect = false;
            this.gridVariables.Name = "gridVariables";
            this.gridVariables.RowHeadersWidth = 220;
            this.gridVariables.RowTemplate.Height = 25;
            this.gridVariables.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.gridVariables.Size = new System.Drawing.Size(745, 293);
            this.gridVariables.TabIndex = 1;
            // 
            // tabTasks
            // 
            this.tabTasks.BackColor = System.Drawing.SystemColors.Control;
            this.tabTasks.Controls.Add(this.listViewTasks);
            this.tabTasks.Controls.Add(this.lblTasks);
            this.tabTasks.Controls.Add(this.updTasks);
            this.tabTasks.Location = new System.Drawing.Point(4, 24);
            this.tabTasks.Name = "tabTasks";
            this.tabTasks.Padding = new System.Windows.Forms.Padding(3);
            this.tabTasks.Size = new System.Drawing.Size(737, 426);
            this.tabTasks.TabIndex = 1;
            this.tabTasks.Text = "Tasks";
            // 
            // listViewTasks
            // 
            this.listViewTasks.AllowDrop = true;
            this.listViewTasks.DummyName = "Dummy";
            this.listViewTasks.FullRowSelect = true;
            this.listViewTasks.Location = new System.Drawing.Point(6, 42);
            this.listViewTasks.Name = "listViewTasks";
            this.listViewTasks.Size = new System.Drawing.Size(725, 377);
            this.listViewTasks.TabIndex = 4;
            this.listViewTasks.UseCompatibleStateImageBehavior = false;
            // 
            // lblTasks
            // 
            this.lblTasks.AutoSize = true;
            this.lblTasks.Location = new System.Drawing.Point(18, 15);
            this.lblTasks.Name = "lblTasks";
            this.lblTasks.Size = new System.Drawing.Size(111, 17);
            this.lblTasks.TabIndex = 2;
            this.lblTasks.Text = "Number of tasks";
            // 
            // updTasks
            // 
            this.updTasks.Location = new System.Drawing.Point(148, 13);
            this.updTasks.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.updTasks.Name = "updTasks";
            this.updTasks.Size = new System.Drawing.Size(66, 23);
            this.updTasks.TabIndex = 1;
            this.updTasks.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.updTasks.ValueChanged += new System.EventHandler(this.Tasks_ValueChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Name = "columnHeader1";
            this.columnHeader1.Text = "Task A";
            this.columnHeader1.Width = 189;
            // 
            // tabDummy
            // 
            this.tabDummy.Location = new System.Drawing.Point(277, 464);
            this.tabDummy.Name = "tabDummy";
            this.tabDummy.SelectedIndex = 0;
            this.tabDummy.Size = new System.Drawing.Size(88, 77);
            this.tabDummy.TabIndex = 8;
            this.tabDummy.Visible = false;
            // 
            // btnExample
            // 
            this.btnExample.Location = new System.Drawing.Point(16, 464);
            this.btnExample.Name = "btnExample";
            this.btnExample.Size = new System.Drawing.Size(133, 34);
            this.btnExample.TabIndex = 9;
            this.btnExample.Text = "Example";
            this.btnExample.UseVisualStyleBackColor = true;
            this.btnExample.Click += new System.EventHandler(this.Example_Click);
            // 
            // FrmDataStrainIndex
            // 
            this.AcceptButton = this.btnAccept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(774, 511);
            this.Controls.Add(this.btnExample);
            this.Controls.Add(this.tabDataStrain);
            this.Controls.Add(this.tabDummy);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAccept);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmDataStrainIndex";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Strain index data";
            this.tabDataStrain.ResumeLayout(false);
            this.tabSubtasks.ResumeLayout(false);
            this.tabSubtasks.PerformLayout();
            this.groupIndex.ResumeLayout(false);
            this.groupIndex.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.updSubtasks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridVariables)).EndInit();
            this.tabTasks.ResumeLayout(false);
            this.tabTasks.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.updTasks)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TabControl tabDataStrain;
        //private System.Windows.Forms.TablessTabControl tabDataStrain;
        private System.Windows.Forms.TabPage tabSubtasks;
        private System.Windows.Forms.TabPage tabTasks;
        private System.Windows.Forms.GroupBox groupIndex;
        private System.Windows.Forms.RadioButton radCUSI;
        private System.Windows.Forms.RadioButton radCOSI;
        private System.Windows.Forms.RadioButton radRSI;
        private System.Windows.Forms.Label lblSubtasks;
        private System.Windows.Forms.NumericUpDown updSubtasks;
        private System.Windows.Forms.DataGridView gridVariables;
        private System.Windows.Forms.Label lblTasks;
        private System.Windows.Forms.NumericUpDown updTasks;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ListViewEx listViewTasks;
        private System.Windows.Forms.TabControl tabDummy;
        private System.Windows.Forms.Button btnExample;
    }
}