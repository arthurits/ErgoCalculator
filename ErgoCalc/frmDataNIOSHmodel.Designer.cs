namespace ErgoCalc
{
    partial class frmDataNIOSHmodel
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
            this.chkComposite = new System.Windows.Forms.CheckBox();
            this.lblConstanteLC = new System.Windows.Forms.Label();
            this.txtConstanteLC = new System.Windows.Forms.TextBox();
            this.listViewTasks = new System.Windows.Forms.ListView();
            this.tabDataNIOSH = new System.Windows.Forms.TablessTabControl();
            this.tabSubtasks = new System.Windows.Forms.TabPage();
            this.tabTasks = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.updTasks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridVariables)).BeginInit();
            this.tabDataNIOSH.SuspendLayout();
            this.SuspendLayout();
            // 
            // updTasks
            // 
            this.updTasks.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
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
            this.updTasks.Size = new System.Drawing.Size(63, 22);
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
            this.gridVariables.RowHeadersWidth = 180;
            this.gridVariables.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.gridVariables.Size = new System.Drawing.Size(745, 374);
            this.gridVariables.TabIndex = 0;
            // 
            // lblTasks
            // 
            this.lblTasks.AutoSize = true;
            this.lblTasks.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblTasks.Location = new System.Drawing.Point(16, 409);
            this.lblTasks.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTasks.Name = "lblTasks";
            this.lblTasks.Size = new System.Drawing.Size(104, 16);
            this.lblTasks.TabIndex = 2;
            this.lblTasks.Text = "Number of tasks";
            this.lblTasks.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            // chkComposite
            // 
            this.chkComposite.AutoSize = true;
            this.chkComposite.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkComposite.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.chkComposite.Location = new System.Drawing.Point(16, 434);
            this.chkComposite.Margin = new System.Windows.Forms.Padding(4);
            this.chkComposite.Name = "chkComposite";
            this.chkComposite.Size = new System.Drawing.Size(167, 21);
            this.chkComposite.TabIndex = 2;
            this.chkComposite.Text = "Composite lifting index";
            this.chkComposite.UseVisualStyleBackColor = true;
            // 
            // lblConstanteLC
            // 
            this.lblConstanteLC.AutoSize = true;
            this.lblConstanteLC.Location = new System.Drawing.Point(16, 464);
            this.lblConstanteLC.Name = "lblConstanteLC";
            this.lblConstanteLC.Size = new System.Drawing.Size(152, 17);
            this.lblConstanteLC.TabIndex = 6;
            this.lblConstanteLC.Text = "Load constant (LC, kg)";
            // 
            // txtConstanteLC
            // 
            this.txtConstanteLC.Location = new System.Drawing.Point(191, 462);
            this.txtConstanteLC.Name = "txtConstanteLC";
            this.txtConstanteLC.Size = new System.Drawing.Size(33, 23);
            this.txtConstanteLC.TabIndex = 3;
            // 
            // listViewTasks
            // 
            this.listViewTasks.HideSelection = false;
            this.listViewTasks.Location = new System.Drawing.Point(464, 335);
            this.listViewTasks.Name = "listViewTasks";
            this.listViewTasks.Size = new System.Drawing.Size(222, 120);
            this.listViewTasks.TabIndex = 7;
            this.listViewTasks.UseCompatibleStateImageBehavior = false;
            this.listViewTasks.Visible = false;
            // 
            // tabDataNIOSH
            // 
            this.tabDataNIOSH.AllowArrowKeys = true;
            this.tabDataNIOSH.AllowTabKey = true;
            this.tabDataNIOSH.Controls.Add(this.tabSubtasks);
            this.tabDataNIOSH.Controls.Add(this.tabTasks);
            this.tabDataNIOSH.Location = new System.Drawing.Point(300, 288);
            this.tabDataNIOSH.Name = "tabDataNIOSH";
            this.tabDataNIOSH.SelectedIndex = 0;
            this.tabDataNIOSH.Size = new System.Drawing.Size(158, 208);
            this.tabDataNIOSH.TabIndex = 8;
            this.tabDataNIOSH.Visible = false;
            // 
            // tabSubtasks
            // 
            this.tabSubtasks.BackColor = System.Drawing.SystemColors.Control;
            this.tabSubtasks.Location = new System.Drawing.Point(4, 25);
            this.tabSubtasks.Name = "tabSubtasks";
            this.tabSubtasks.Padding = new System.Windows.Forms.Padding(3);
            this.tabSubtasks.Size = new System.Drawing.Size(150, 179);
            this.tabSubtasks.TabIndex = 0;
            this.tabSubtasks.Text = "Subtasks";
            // 
            // tabTasks
            // 
            this.tabTasks.BackColor = System.Drawing.SystemColors.Control;
            this.tabTasks.Location = new System.Drawing.Point(4, 25);
            this.tabTasks.Name = "tabTasks";
            this.tabTasks.Padding = new System.Windows.Forms.Padding(3);
            this.tabTasks.Size = new System.Drawing.Size(150, 179);
            this.tabTasks.TabIndex = 1;
            this.tabTasks.Text = "Tasks";
            // 
            // frmDataNIOSHmodel
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(774, 511);
            this.Controls.Add(this.tabDataNIOSH);
            this.Controls.Add(this.listViewTasks);
            this.Controls.Add(this.txtConstanteLC);
            this.Controls.Add(this.lblConstanteLC);
            this.Controls.Add(this.chkComposite);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblTasks);
            this.Controls.Add(this.gridVariables);
            this.Controls.Add(this.updTasks);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDataNIOSHmodel";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "NIOSH model data";
            ((System.ComponentModel.ISupportInitialize)(this.updTasks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridVariables)).EndInit();
            this.tabDataNIOSH.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown updTasks;
        private System.Windows.Forms.DataGridView gridVariables;
        private System.Windows.Forms.Label lblTasks;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox chkComposite;
        private System.Windows.Forms.Label lblConstanteLC;
        private System.Windows.Forms.TextBox txtConstanteLC;
        private System.Windows.Forms.ListView listViewTasks;
        private System.Windows.Forms.TablessTabControl tabDataNIOSH;
        private System.Windows.Forms.TabPage tabSubtasks;
        private System.Windows.Forms.TabPage tabTasks;
    }
}