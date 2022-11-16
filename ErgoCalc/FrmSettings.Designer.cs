namespace ErgoCalc
{
    partial class FrmSettings
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAccept = new System.Windows.Forms.Button();
            this.tabSettings = new System.Windows.Forms.TabControl();
            this.tabPlot = new System.Windows.Forms.TabPage();
            this.grpFont = new System.Windows.Forms.GroupBox();
            this.lblFontName = new System.Windows.Forms.Label();
            this.pctFontColor = new System.Windows.Forms.PictureBox();
            this.lblFontColor = new System.Windows.Forms.Label();
            this.lblFontStyle = new System.Windows.Forms.Label();
            this.lblFontSize = new System.Windows.Forms.Label();
            this.btnDlgFont = new System.Windows.Forms.Button();
            this.lblDlgFont = new System.Windows.Forms.Label();
            this.tabGUI = new System.Windows.Forms.TabPage();
            this.txtDataFormat = new System.Windows.Forms.TextBox();
            this.lblDataFormat = new System.Windows.Forms.Label();
            this.grpCulture = new System.Windows.Forms.GroupBox();
            this.cboAllCultures = new System.Windows.Forms.ComboBox();
            this.radUserCulture = new System.Windows.Forms.RadioButton();
            this.radInvariantCulture = new System.Windows.Forms.RadioButton();
            this.radCurrentCulture = new System.Windows.Forms.RadioButton();
            this.chkDlgPath = new System.Windows.Forms.CheckBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.tabSettings.SuspendLayout();
            this.tabPlot.SuspendLayout();
            this.grpFont.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctFontColor)).BeginInit();
            this.tabGUI.SuspendLayout();
            this.grpCulture.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(255, 279);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 30);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // btnAccept
            // 
            this.btnAccept.Location = new System.Drawing.Point(361, 279);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(100, 30);
            this.btnAccept.TabIndex = 6;
            this.btnAccept.Text = "&Accept";
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.Accept_Click);
            // 
            // tabSettings
            // 
            this.tabSettings.Controls.Add(this.tabPlot);
            this.tabSettings.Controls.Add(this.tabGUI);
            this.tabSettings.Location = new System.Drawing.Point(12, 14);
            this.tabSettings.Name = "tabSettings";
            this.tabSettings.SelectedIndex = 0;
            this.tabSettings.Size = new System.Drawing.Size(449, 259);
            this.tabSettings.TabIndex = 11;
            // 
            // tabPlot
            // 
            this.tabPlot.Controls.Add(this.grpFont);
            this.tabPlot.Controls.Add(this.btnDlgFont);
            this.tabPlot.Controls.Add(this.lblDlgFont);
            this.tabPlot.Location = new System.Drawing.Point(4, 26);
            this.tabPlot.Name = "tabPlot";
            this.tabPlot.Padding = new System.Windows.Forms.Padding(3);
            this.tabPlot.Size = new System.Drawing.Size(441, 229);
            this.tabPlot.TabIndex = 0;
            this.tabPlot.Text = "Text and font";
            this.tabPlot.UseVisualStyleBackColor = true;
            // 
            // grpFont
            // 
            this.grpFont.Controls.Add(this.lblFontName);
            this.grpFont.Controls.Add(this.pctFontColor);
            this.grpFont.Controls.Add(this.lblFontColor);
            this.grpFont.Controls.Add(this.lblFontStyle);
            this.grpFont.Controls.Add(this.lblFontSize);
            this.grpFont.Location = new System.Drawing.Point(48, 57);
            this.grpFont.Name = "grpFont";
            this.grpFont.Size = new System.Drawing.Size(310, 150);
            this.grpFont.TabIndex = 7;
            this.grpFont.TabStop = false;
            this.grpFont.Text = "Current font";
            // 
            // lblFontName
            // 
            this.lblFontName.AutoSize = true;
            this.lblFontName.Location = new System.Drawing.Point(28, 25);
            this.lblFontName.Name = "lblFontName";
            this.lblFontName.Size = new System.Drawing.Size(45, 19);
            this.lblFontName.TabIndex = 2;
            this.lblFontName.Text = "Name";
            // 
            // pctFontColor
            // 
            this.pctFontColor.Location = new System.Drawing.Point(191, 112);
            this.pctFontColor.Name = "pctFontColor";
            this.pctFontColor.Size = new System.Drawing.Size(82, 26);
            this.pctFontColor.TabIndex = 6;
            this.pctFontColor.TabStop = false;
            // 
            // lblFontColor
            // 
            this.lblFontColor.AutoSize = true;
            this.lblFontColor.Location = new System.Drawing.Point(28, 112);
            this.lblFontColor.Name = "lblFontColor";
            this.lblFontColor.Size = new System.Drawing.Size(42, 19);
            this.lblFontColor.TabIndex = 5;
            this.lblFontColor.Text = "Color";
            // 
            // lblFontStyle
            // 
            this.lblFontStyle.AutoSize = true;
            this.lblFontStyle.Location = new System.Drawing.Point(28, 54);
            this.lblFontStyle.Name = "lblFontStyle";
            this.lblFontStyle.Size = new System.Drawing.Size(38, 19);
            this.lblFontStyle.TabIndex = 3;
            this.lblFontStyle.Text = "Style";
            // 
            // lblFontSize
            // 
            this.lblFontSize.AutoSize = true;
            this.lblFontSize.Location = new System.Drawing.Point(28, 83);
            this.lblFontSize.Name = "lblFontSize";
            this.lblFontSize.Size = new System.Drawing.Size(32, 19);
            this.lblFontSize.TabIndex = 4;
            this.lblFontSize.Text = "Size";
            // 
            // btnDlgFont
            // 
            this.btnDlgFont.Location = new System.Drawing.Point(173, 19);
            this.btnDlgFont.Name = "btnDlgFont";
            this.btnDlgFont.Size = new System.Drawing.Size(90, 32);
            this.btnDlgFont.TabIndex = 1;
            this.btnDlgFont.Text = "Select font";
            this.btnDlgFont.UseVisualStyleBackColor = true;
            this.btnDlgFont.Click += new System.EventHandler(this.DlgFont);
            // 
            // lblDlgFont
            // 
            this.lblDlgFont.AutoSize = true;
            this.lblDlgFont.Location = new System.Drawing.Point(50, 27);
            this.lblDlgFont.Name = "lblDlgFont";
            this.lblDlgFont.Size = new System.Drawing.Size(73, 19);
            this.lblDlgFont.TabIndex = 0;
            this.lblDlgFont.Text = "Select font";
            // 
            // tabGUI
            // 
            this.tabGUI.Controls.Add(this.txtDataFormat);
            this.tabGUI.Controls.Add(this.lblDataFormat);
            this.tabGUI.Controls.Add(this.grpCulture);
            this.tabGUI.Controls.Add(this.chkDlgPath);
            this.tabGUI.Location = new System.Drawing.Point(4, 24);
            this.tabGUI.Name = "tabGUI";
            this.tabGUI.Padding = new System.Windows.Forms.Padding(3);
            this.tabGUI.Size = new System.Drawing.Size(441, 231);
            this.tabGUI.TabIndex = 1;
            this.tabGUI.Text = "User interface";
            this.tabGUI.UseVisualStyleBackColor = true;
            // 
            // txtDataFormat
            // 
            this.txtDataFormat.Location = new System.Drawing.Point(260, 185);
            this.txtDataFormat.Name = "txtDataFormat";
            this.txtDataFormat.Size = new System.Drawing.Size(74, 25);
            this.txtDataFormat.TabIndex = 3;
            // 
            // lblDataFormat
            // 
            this.lblDataFormat.AutoSize = true;
            this.lblDataFormat.Location = new System.Drawing.Point(19, 188);
            this.lblDataFormat.MaximumSize = new System.Drawing.Size(240, 0);
            this.lblDataFormat.Name = "lblDataFormat";
            this.lblDataFormat.Size = new System.Drawing.Size(201, 19);
            this.lblDataFormat.TabIndex = 2;
            this.lblDataFormat.Text = "Numeric data-formatting string";
            // 
            // grpCulture
            // 
            this.grpCulture.Controls.Add(this.cboAllCultures);
            this.grpCulture.Controls.Add(this.radUserCulture);
            this.grpCulture.Controls.Add(this.radInvariantCulture);
            this.grpCulture.Controls.Add(this.radCurrentCulture);
            this.grpCulture.Location = new System.Drawing.Point(19, 8);
            this.grpCulture.Name = "grpCulture";
            this.grpCulture.Size = new System.Drawing.Size(304, 140);
            this.grpCulture.TabIndex = 1;
            this.grpCulture.TabStop = false;
            this.grpCulture.Text = "UI and data format";
            // 
            // cboAllCultures
            // 
            this.cboAllCultures.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboAllCultures.Enabled = false;
            this.cboAllCultures.FormattingEnabled = true;
            this.cboAllCultures.Location = new System.Drawing.Point(40, 106);
            this.cboAllCultures.Name = "cboAllCultures";
            this.cboAllCultures.Size = new System.Drawing.Size(190, 25);
            this.cboAllCultures.TabIndex = 3;
            this.cboAllCultures.SelectedValueChanged += new System.EventHandler(this.AllCultures_SelectedValueChanged);
            // 
            // radUserCulture
            // 
            this.radUserCulture.AutoSize = true;
            this.radUserCulture.Location = new System.Drawing.Point(18, 80);
            this.radUserCulture.Name = "radUserCulture";
            this.radUserCulture.Size = new System.Drawing.Size(108, 23);
            this.radUserCulture.TabIndex = 2;
            this.radUserCulture.TabStop = true;
            this.radUserCulture.Text = "Select culture";
            this.radUserCulture.UseVisualStyleBackColor = true;
            this.radUserCulture.CheckedChanged += new System.EventHandler(this.UserCulture_CheckedChanged);
            // 
            // radInvariantCulture
            // 
            this.radInvariantCulture.AutoSize = true;
            this.radInvariantCulture.Location = new System.Drawing.Point(18, 51);
            this.radInvariantCulture.Name = "radInvariantCulture";
            this.radInvariantCulture.Size = new System.Drawing.Size(196, 23);
            this.radInvariantCulture.TabIndex = 1;
            this.radInvariantCulture.TabStop = true;
            this.radInvariantCulture.Text = "Invariant culture formatting";
            this.radInvariantCulture.UseVisualStyleBackColor = true;
            this.radInvariantCulture.CheckedChanged += new System.EventHandler(this.InvariantCulture_CheckedChanged);
            // 
            // radCurrentCulture
            // 
            this.radCurrentCulture.AutoSize = true;
            this.radCurrentCulture.Location = new System.Drawing.Point(18, 23);
            this.radCurrentCulture.Name = "radCurrentCulture";
            this.radCurrentCulture.Size = new System.Drawing.Size(189, 23);
            this.radCurrentCulture.TabIndex = 0;
            this.radCurrentCulture.TabStop = true;
            this.radCurrentCulture.Text = "Current culture formatting";
            this.radCurrentCulture.UseVisualStyleBackColor = true;
            this.radCurrentCulture.CheckedChanged += new System.EventHandler(this.CurrentCulture_CheckedChanged);
            // 
            // chkDlgPath
            // 
            this.chkDlgPath.AutoSize = true;
            this.chkDlgPath.Location = new System.Drawing.Point(23, 154);
            this.chkDlgPath.Name = "chkDlgPath";
            this.chkDlgPath.Size = new System.Drawing.Size(290, 23);
            this.chkDlgPath.TabIndex = 0;
            this.chkDlgPath.Text = "Remember open/save dialog previous path";
            this.chkDlgPath.UseVisualStyleBackColor = true;
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(12, 279);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(100, 30);
            this.btnReset.TabIndex = 12;
            this.btnReset.Text = "&Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.Reset_Click);
            // 
            // FrmSettings
            // 
            this.AcceptButton = this.btnAccept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(473, 321);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.tabSettings);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.btnCancel);
            this.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSettings";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.tabSettings.ResumeLayout(false);
            this.tabPlot.ResumeLayout(false);
            this.tabPlot.PerformLayout();
            this.grpFont.ResumeLayout(false);
            this.grpFont.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctFontColor)).EndInit();
            this.tabGUI.ResumeLayout(false);
            this.tabGUI.PerformLayout();
            this.grpCulture.ResumeLayout(false);
            this.grpCulture.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private Button btnCancel;
        private Button btnAccept;
        private TabControl tabSettings;
        private TabPage tabPlot;
        private TabPage tabGUI;
        private GroupBox grpCulture;
        private RadioButton radInvariantCulture;
        private RadioButton radCurrentCulture;
        private CheckBox chkDlgPath;
        private TextBox txtDataFormat;
        private Label lblDataFormat;
        private Button btnReset;
        private ComboBox cboAllCultures;
        private RadioButton radUserCulture;
        private Button btnDlgFont;
        private Label lblDlgFont;
        private Label lblFontName;
        private GroupBox grpFont;
        private PictureBox pctFontColor;
        private Label lblFontColor;
        private Label lblFontStyle;
        private Label lblFontSize;
    }
}