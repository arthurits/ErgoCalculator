
namespace ErgoCalc
{
    partial class frmDataLiberty
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
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tabDataStrain = new System.Windows.Forms.TabControl();
            this.tabSubtasks = new System.Windows.Forms.TabPage();
            this.groupIndex = new System.Windows.Forms.GroupBox();
            this.radPull = new System.Windows.Forms.RadioButton();
            this.radLow = new System.Windows.Forms.RadioButton();
            this.radLift = new System.Windows.Forms.RadioButton();
            this.lblSubtasks = new System.Windows.Forms.Label();
            this.updSubtasks = new System.Windows.Forms.NumericUpDown();
            this.gridVariables = new System.Windows.Forms.DataGridView();
            this.tabTasks = new System.Windows.Forms.TabPage();
            this.listViewTasks = new System.Windows.Forms.ListViewEx();
            this.lblTasks = new System.Windows.Forms.Label();
            this.updTasks = new System.Windows.Forms.NumericUpDown();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.tabDummy = new System.Windows.Forms.TabControl();
            this.radPush = new System.Windows.Forms.RadioButton();
            this.radCarr = new System.Windows.Forms.RadioButton();
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
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(468, 464);
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
            this.tabDataStrain.Location = new System.Drawing.Point(16, 12);
            this.tabDataStrain.Name = "tabDataStrain";
            this.tabDataStrain.SelectedIndex = 0;
            this.tabDataStrain.Size = new System.Drawing.Size(745, 445);
            this.tabDataStrain.TabIndex = 7;
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
            this.tabSubtasks.Size = new System.Drawing.Size(737, 416);
            this.tabSubtasks.TabIndex = 0;
            this.tabSubtasks.Text = "Subtasks data";
            // 
            // groupIndex
            // 
            this.groupIndex.Controls.Add(this.radCarr);
            this.groupIndex.Controls.Add(this.radPush);
            this.groupIndex.Controls.Add(this.radPull);
            this.groupIndex.Controls.Add(this.radLow);
            this.groupIndex.Controls.Add(this.radLift);
            this.groupIndex.Location = new System.Drawing.Point(6, 316);
            this.groupIndex.Name = "groupIndex";
            this.groupIndex.Size = new System.Drawing.Size(475, 61);
            this.groupIndex.TabIndex = 9;
            this.groupIndex.TabStop = false;
            this.groupIndex.Text = "Index type";
            // 
            // radPull
            // 
            this.radPull.AutoSize = true;
            this.radPull.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.radPull.Location = new System.Drawing.Point(290, 28);
            this.radPull.Name = "radPull";
            this.radPull.Size = new System.Drawing.Size(68, 21);
            this.radPull.TabIndex = 2;
            this.radPull.Text = "Pulling";
            this.radPull.UseVisualStyleBackColor = true;
            this.radPull.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // radLow
            // 
            this.radLow.AutoSize = true;
            this.radLow.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.radLow.Location = new System.Drawing.Point(189, 28);
            this.radLow.Name = "radLow";
            this.radLow.Size = new System.Drawing.Size(83, 21);
            this.radLow.TabIndex = 1;
            this.radLow.Text = "Lowering";
            this.radLow.UseVisualStyleBackColor = true;
            this.radLow.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // radLift
            // 
            this.radLift.AutoSize = true;
            this.radLift.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.radLift.Location = new System.Drawing.Point(107, 28);
            this.radLift.Name = "radLift";
            this.radLift.Size = new System.Drawing.Size(64, 21);
            this.radLift.TabIndex = 0;
            this.radLift.Text = "Lifting";
            this.radLift.UseVisualStyleBackColor = true;
            this.radLift.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // lblSubtasks
            // 
            this.lblSubtasks.AutoSize = true;
            this.lblSubtasks.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblSubtasks.Location = new System.Drawing.Point(3, 389);
            this.lblSubtasks.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSubtasks.Name = "lblSubtasks";
            this.lblSubtasks.Size = new System.Drawing.Size(115, 17);
            this.lblSubtasks.TabIndex = 8;
            this.lblSubtasks.Text = "Number of cases";
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
            this.updSubtasks.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.updSubtasks.Name = "updSubtasks";
            this.updSubtasks.Size = new System.Drawing.Size(63, 23);
            this.updSubtasks.TabIndex = 7;
            this.updSubtasks.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.updSubtasks.ValueChanged += new System.EventHandler(this.updSubtasks_ValueChanged);
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
            this.gridVariables.Size = new System.Drawing.Size(729, 207);
            this.gridVariables.TabIndex = 1;
            // 
            // tabTasks
            // 
            this.tabTasks.BackColor = System.Drawing.SystemColors.Control;
            this.tabTasks.Controls.Add(this.listViewTasks);
            this.tabTasks.Controls.Add(this.lblTasks);
            this.tabTasks.Controls.Add(this.updTasks);
            this.tabTasks.Location = new System.Drawing.Point(4, 25);
            this.tabTasks.Name = "tabTasks";
            this.tabTasks.Padding = new System.Windows.Forms.Padding(3);
            this.tabTasks.Size = new System.Drawing.Size(737, 416);
            this.tabTasks.TabIndex = 1;
            this.tabTasks.Text = "Tasks";
            // 
            // listViewTasks
            // 
            this.listViewTasks.AllowDrop = true;
            this.listViewTasks.FullRowSelect = true;
            this.listViewTasks.HideSelection = false;
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
            // 
            // columnHeader1
            // 
            this.columnHeader1.Name = "columnHeader1";
            this.columnHeader1.Text = "Task A";
            this.columnHeader1.Width = 189;
            // 
            // tabDummy
            // 
            this.tabDummy.Location = new System.Drawing.Point(373, 422);
            this.tabDummy.Name = "tabDummy";
            this.tabDummy.SelectedIndex = 0;
            this.tabDummy.Size = new System.Drawing.Size(88, 77);
            this.tabDummy.TabIndex = 8;
            this.tabDummy.Visible = false;
            // 
            // radPush
            // 
            this.radPush.AutoSize = true;
            this.radPush.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.radPush.Location = new System.Drawing.Point(376, 29);
            this.radPush.Name = "radPush";
            this.radPush.Size = new System.Drawing.Size(77, 21);
            this.radPush.TabIndex = 3;
            this.radPush.Text = "Pushing";
            this.radPush.UseVisualStyleBackColor = true;
            this.radPush.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // radCarr
            // 
            this.radCarr.AutoSize = true;
            this.radCarr.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.radCarr.Checked = true;
            this.radCarr.Location = new System.Drawing.Point(10, 28);
            this.radCarr.Name = "radCarr";
            this.radCarr.Size = new System.Drawing.Size(79, 21);
            this.radCarr.TabIndex = 4;
            this.radCarr.TabStop = true;
            this.radCarr.Text = "Carrying";
            this.radCarr.UseVisualStyleBackColor = true;
            this.radCarr.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // frmDataLiberty
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(774, 511);
            this.Controls.Add(this.tabDummy);
            this.Controls.Add(this.tabDataStrain);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDataLiberty";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "LM-MMH data";
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

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TabControl tabDataStrain;
        //private System.Windows.Forms.TablessTabControl tabDataStrain;
        private System.Windows.Forms.TabPage tabSubtasks;
        private System.Windows.Forms.TabPage tabTasks;
        private System.Windows.Forms.GroupBox groupIndex;
        private System.Windows.Forms.RadioButton radPull;
        private System.Windows.Forms.RadioButton radLow;
        private System.Windows.Forms.RadioButton radLift;
        private System.Windows.Forms.Label lblSubtasks;
        private System.Windows.Forms.NumericUpDown updSubtasks;
        private System.Windows.Forms.DataGridView gridVariables;
        private System.Windows.Forms.Label lblTasks;
        private System.Windows.Forms.NumericUpDown updTasks;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ListViewEx listViewTasks;
        private System.Windows.Forms.TabControl tabDummy;
        private System.Windows.Forms.RadioButton radCarr;
        private System.Windows.Forms.RadioButton radPush;
    }
}