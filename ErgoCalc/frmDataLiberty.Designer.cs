
namespace ErgoCalc
{
    partial class FrmDataLiberty
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
            this.groupIndex = new System.Windows.Forms.GroupBox();
            this.radCarr = new System.Windows.Forms.RadioButton();
            this.radPush = new System.Windows.Forms.RadioButton();
            this.radPull = new System.Windows.Forms.RadioButton();
            this.radLow = new System.Windows.Forms.RadioButton();
            this.radLift = new System.Windows.Forms.RadioButton();
            this.lblSubtasks = new System.Windows.Forms.Label();
            this.updTasks = new System.Windows.Forms.NumericUpDown();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.btnExample = new System.Windows.Forms.Button();
            this.gridVariables = new System.Windows.Forms.DataGridView();
            this.groupIndex.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.updTasks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridVariables)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(468, 367);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(133, 34);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "&Accept";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.Accept_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(628, 367);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(133, 34);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // groupIndex
            // 
            this.groupIndex.Controls.Add(this.radCarr);
            this.groupIndex.Controls.Add(this.radPush);
            this.groupIndex.Controls.Add(this.radPull);
            this.groupIndex.Controls.Add(this.radLow);
            this.groupIndex.Controls.Add(this.radLift);
            this.groupIndex.Enabled = false;
            this.groupIndex.Location = new System.Drawing.Point(282, 274);
            this.groupIndex.Name = "groupIndex";
            this.groupIndex.Size = new System.Drawing.Size(475, 61);
            this.groupIndex.TabIndex = 9;
            this.groupIndex.TabStop = false;
            this.groupIndex.Text = "Index type";
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
            this.lblSubtasks.Location = new System.Drawing.Point(16, 287);
            this.lblSubtasks.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSubtasks.Name = "lblSubtasks";
            this.lblSubtasks.Size = new System.Drawing.Size(115, 17);
            this.lblSubtasks.TabIndex = 8;
            this.lblSubtasks.Text = "Number of cases";
            // 
            // updTasks
            // 
            this.updTasks.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.updTasks.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.updTasks.Location = new System.Drawing.Point(148, 285);
            this.updTasks.Margin = new System.Windows.Forms.Padding(4);
            this.updTasks.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.updTasks.Name = "updTasks";
            this.updTasks.Size = new System.Drawing.Size(63, 23);
            this.updTasks.TabIndex = 7;
            this.updTasks.ValueChanged += new System.EventHandler(this.Tasks_ValueChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Name = "columnHeader1";
            this.columnHeader1.Text = "Task A";
            this.columnHeader1.Width = 189;
            // 
            // btnExample
            // 
            this.btnExample.Location = new System.Drawing.Point(16, 367);
            this.btnExample.Name = "btnExample";
            this.btnExample.Size = new System.Drawing.Size(133, 34);
            this.btnExample.TabIndex = 9;
            this.btnExample.Text = "Example";
            this.btnExample.UseVisualStyleBackColor = true;
            this.btnExample.Click += new System.EventHandler(this.Example_Click);
            // 
            // gridVariables
            // 
            this.gridVariables.AllowUserToAddRows = false;
            this.gridVariables.AllowUserToDeleteRows = false;
            this.gridVariables.AllowUserToResizeRows = false;
            this.gridVariables.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridVariables.Location = new System.Drawing.Point(13, 13);
            this.gridVariables.Margin = new System.Windows.Forms.Padding(4);
            this.gridVariables.MultiSelect = false;
            this.gridVariables.Name = "gridVariables";
            this.gridVariables.RowHeadersWidth = 220;
            this.gridVariables.RowTemplate.Height = 25;
            this.gridVariables.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.gridVariables.Size = new System.Drawing.Size(748, 245);
            this.gridVariables.TabIndex = 10;
            // 
            // FrmDataLiberty
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(774, 415);
            this.Controls.Add(this.gridVariables);
            this.Controls.Add(this.groupIndex);
            this.Controls.Add(this.btnExample);
            this.Controls.Add(this.lblSubtasks);
            this.Controls.Add(this.updTasks);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmDataLiberty";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "LM-MMH data";
            this.groupIndex.ResumeLayout(false);
            this.groupIndex.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.updTasks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridVariables)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupIndex;
        private System.Windows.Forms.RadioButton radPull;
        private System.Windows.Forms.RadioButton radLow;
        private System.Windows.Forms.RadioButton radLift;
        private System.Windows.Forms.Label lblSubtasks;
        private System.Windows.Forms.NumericUpDown updTasks;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.RadioButton radCarr;
        private System.Windows.Forms.RadioButton radPush;
        private System.Windows.Forms.Button btnExample;
        private System.Windows.Forms.DataGridView gridVariables;
    }
}