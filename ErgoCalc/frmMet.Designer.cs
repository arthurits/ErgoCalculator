namespace ErgoCalc
{
    partial class frmMet
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
            this.tabMetabolicRate = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lblOcupacion = new System.Windows.Forms.Label();
            this.cboCategorias = new System.Windows.Forms.ComboBox();
            this.cboOcupaciones = new System.Windows.Forms.ComboBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lblClaseExp = new System.Windows.Forms.Label();
            this.cboClase = new System.Windows.Forms.ComboBox();
            this.lblCategoria = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.lblTasks = new System.Windows.Forms.Label();
            this.updTasks = new System.Windows.Forms.NumericUpDown();
            this.gridLevel2A = new System.Windows.Forms.DataGridView();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.tabMetabolicRate.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.updTasks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLevel2A)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAccept
            // 
            this.btnAccept.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnAccept.Location = new System.Drawing.Point(258, 415);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(82, 27);
            this.btnAccept.TabIndex = 0;
            this.btnAccept.Text = "&Accept";
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(346, 415);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(82, 27);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // tabMetabolicRate
            // 
            this.tabMetabolicRate.Controls.Add(this.tabPage1);
            this.tabMetabolicRate.Controls.Add(this.tabPage2);
            this.tabMetabolicRate.Controls.Add(this.tabPage3);
            this.tabMetabolicRate.Controls.Add(this.tabPage4);
            this.tabMetabolicRate.Controls.Add(this.tabPage5);
            this.tabMetabolicRate.Location = new System.Drawing.Point(12, 69);
            this.tabMetabolicRate.Name = "tabMetabolicRate";
            this.tabMetabolicRate.SelectedIndex = 0;
            this.tabMetabolicRate.Size = new System.Drawing.Size(416, 329);
            this.tabMetabolicRate.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lblOcupacion);
            this.tabPage1.Controls.Add(this.cboCategorias);
            this.tabPage1.Controls.Add(this.cboOcupaciones);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(408, 301);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Level 1.A";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lblOcupacion
            // 
            this.lblOcupacion.AutoSize = true;
            this.lblOcupacion.Location = new System.Drawing.Point(54, 41);
            this.lblOcupacion.Name = "lblOcupacion";
            this.lblOcupacion.Size = new System.Drawing.Size(107, 15);
            this.lblOcupacion.TabIndex = 2;
            this.lblOcupacion.Text = "Select occupation:";
            // 
            // cboCategorias
            // 
            this.cboCategorias.FormattingEnabled = true;
            this.cboCategorias.Location = new System.Drawing.Point(54, 128);
            this.cboCategorias.Name = "cboCategorias";
            this.cboCategorias.Size = new System.Drawing.Size(198, 23);
            this.cboCategorias.TabIndex = 1;
            // 
            // cboOcupaciones
            // 
            this.cboOcupaciones.FormattingEnabled = true;
            this.cboOcupaciones.Location = new System.Drawing.Point(54, 76);
            this.cboOcupaciones.Name = "cboOcupaciones";
            this.cboOcupaciones.Size = new System.Drawing.Size(198, 23);
            this.cboOcupaciones.TabIndex = 0;
            this.cboOcupaciones.SelectedValueChanged += new System.EventHandler(this.cboOcupaciones_SelectedValueChanged);
            this.cboOcupaciones.Leave += new System.EventHandler(this.cboOcupaciones_Leave);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.lblClaseExp);
            this.tabPage2.Controls.Add(this.cboClase);
            this.tabPage2.Controls.Add(this.lblCategoria);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(408, 301);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Level 1.B";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // lblClaseExp
            // 
            this.lblClaseExp.Location = new System.Drawing.Point(34, 117);
            this.lblClaseExp.Name = "lblClaseExp";
            this.lblClaseExp.Size = new System.Drawing.Size(354, 163);
            this.lblClaseExp.TabIndex = 2;
            this.lblClaseExp.Text = "lblClaseExp";
            // 
            // cboClase
            // 
            this.cboClase.FormattingEnabled = true;
            this.cboClase.Location = new System.Drawing.Point(57, 76);
            this.cboClase.Name = "cboClase";
            this.cboClase.Size = new System.Drawing.Size(197, 23);
            this.cboClase.TabIndex = 1;
            this.cboClase.SelectedValueChanged += new System.EventHandler(this.cboClase_SelectedValueChanged);
            this.cboClase.Leave += new System.EventHandler(this.cboClase_Leave);
            // 
            // lblCategoria
            // 
            this.lblCategoria.AutoSize = true;
            this.lblCategoria.Location = new System.Drawing.Point(57, 41);
            this.lblCategoria.Name = "lblCategoria";
            this.lblCategoria.Size = new System.Drawing.Size(93, 15);
            this.lblCategoria.TabIndex = 0;
            this.lblCategoria.Text = "Select category:";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.lblTasks);
            this.tabPage3.Controls.Add(this.updTasks);
            this.tabPage3.Controls.Add(this.gridLevel2A);
            this.tabPage3.Location = new System.Drawing.Point(4, 24);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(408, 301);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Level 2.A";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // lblTasks
            // 
            this.lblTasks.AutoSize = true;
            this.lblTasks.Location = new System.Drawing.Point(16, 266);
            this.lblTasks.Name = "lblTasks";
            this.lblTasks.Size = new System.Drawing.Size(96, 15);
            this.lblTasks.TabIndex = 2;
            this.lblTasks.Text = "Number of tasks";
            // 
            // updTasks
            // 
            this.updTasks.Location = new System.Drawing.Point(118, 263);
            this.updTasks.Name = "updTasks";
            this.updTasks.Size = new System.Drawing.Size(37, 21);
            this.updTasks.TabIndex = 1;
            this.updTasks.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.updTasks.ValueChanged += new System.EventHandler(this.updTasks_ValueChanged);
            // 
            // gridLevel2A
            // 
            this.gridLevel2A.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridLevel2A.Location = new System.Drawing.Point(19, 40);
            this.gridLevel2A.Name = "gridLevel2A";
            this.gridLevel2A.Size = new System.Drawing.Size(367, 217);
            this.gridLevel2A.TabIndex = 0;
            // 
            // tabPage4
            // 
            this.tabPage4.Location = new System.Drawing.Point(4, 24);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(408, 301);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Level 2.B";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tabPage5
            // 
            this.tabPage5.Location = new System.Drawing.Point(4, 24);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(408, 301);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Level 3";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // frmMet
            // 
            this.AcceptButton = this.btnAccept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(440, 454);
            this.Controls.Add(this.tabMetabolicRate);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAccept);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Metabolic rate";
            this.tabMetabolicRate.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.updTasks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLevel2A)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TabControl tabMetabolicRate;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ComboBox cboOcupaciones;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.ComboBox cboCategorias;
        private System.Windows.Forms.Label lblOcupacion;
        private System.Windows.Forms.Label lblClaseExp;
        private System.Windows.Forms.ComboBox cboClase;
        private System.Windows.Forms.Label lblCategoria;
        private System.Windows.Forms.DataGridView gridLevel2A;
        private System.Windows.Forms.Label lblTasks;
        private System.Windows.Forms.NumericUpDown updTasks;
    }
}