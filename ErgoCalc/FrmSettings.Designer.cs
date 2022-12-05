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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.trackZoomFactor = new System.Windows.Forms.TrackBar();
            this.updZoomFactor = new System.Windows.Forms.NumericUpDown();
            this.lblZoomFactor = new System.Windows.Forms.Label();
            this.pctBackColor = new System.Windows.Forms.PictureBox();
            this.lblBackColor = new System.Windows.Forms.Label();
            this.chkWordWrap = new System.Windows.Forms.CheckBox();
            this.lblFont = new System.Windows.Forms.Label();
            this.pctFontColor = new System.Windows.Forms.PictureBox();
            this.lblFontColor = new System.Windows.Forms.Label();
            this.btnDlgFont = new System.Windows.Forms.Button();
            this.lblFontStyle = new System.Windows.Forms.Label();
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
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackZoomFactor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.updZoomFactor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctBackColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctFontColor)).BeginInit();
            this.tabGUI.SuspendLayout();
            this.grpCulture.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(256, 299);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 30);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // btnAccept
            // 
            this.btnAccept.Location = new System.Drawing.Point(362, 299);
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
            this.tabSettings.Size = new System.Drawing.Size(449, 279);
            this.tabSettings.TabIndex = 11;
            // 
            // tabPlot
            // 
            this.tabPlot.Controls.Add(this.pictureBox1);
            this.tabPlot.Controls.Add(this.trackZoomFactor);
            this.tabPlot.Controls.Add(this.updZoomFactor);
            this.tabPlot.Controls.Add(this.lblZoomFactor);
            this.tabPlot.Controls.Add(this.pctBackColor);
            this.tabPlot.Controls.Add(this.lblBackColor);
            this.tabPlot.Controls.Add(this.chkWordWrap);
            this.tabPlot.Controls.Add(this.lblFont);
            this.tabPlot.Controls.Add(this.pctFontColor);
            this.tabPlot.Controls.Add(this.lblFontColor);
            this.tabPlot.Controls.Add(this.btnDlgFont);
            this.tabPlot.Controls.Add(this.lblFontStyle);
            this.tabPlot.Location = new System.Drawing.Point(4, 26);
            this.tabPlot.Name = "tabPlot";
            this.tabPlot.Padding = new System.Windows.Forms.Padding(3);
            this.tabPlot.Size = new System.Drawing.Size(441, 249);
            this.tabPlot.TabIndex = 0;
            this.tabPlot.Text = "Text and font";
            this.tabPlot.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.LightGray;
            this.pictureBox1.Location = new System.Drawing.Point(42, 106);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(350, 1);
            this.pictureBox1.TabIndex = 13;
            this.pictureBox1.TabStop = false;
            // 
            // trackZoomFactor
            // 
            this.trackZoomFactor.BackColor = System.Drawing.Color.White;
            this.trackZoomFactor.Location = new System.Drawing.Point(38, 201);
            this.trackZoomFactor.Maximum = 500;
            this.trackZoomFactor.Name = "trackZoomFactor";
            this.trackZoomFactor.Size = new System.Drawing.Size(363, 45);
            this.trackZoomFactor.SmallChange = 10;
            this.trackZoomFactor.TabIndex = 12;
            this.trackZoomFactor.TickFrequency = 10;
            this.trackZoomFactor.ValueChanged += new System.EventHandler(this.trackZoomFactor_ValueChanged);
            // 
            // updZoomFactor
            // 
            this.updZoomFactor.Location = new System.Drawing.Point(152, 170);
            this.updZoomFactor.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.updZoomFactor.Name = "updZoomFactor";
            this.updZoomFactor.Size = new System.Drawing.Size(55, 25);
            this.updZoomFactor.TabIndex = 11;
            this.updZoomFactor.ValueChanged += new System.EventHandler(this.updZoomFactor_ValueChanged);
            // 
            // lblZoomFactor
            // 
            this.lblZoomFactor.AutoSize = true;
            this.lblZoomFactor.Location = new System.Drawing.Point(42, 172);
            this.lblZoomFactor.Name = "lblZoomFactor";
            this.lblZoomFactor.Size = new System.Drawing.Size(84, 19);
            this.lblZoomFactor.TabIndex = 10;
            this.lblZoomFactor.Text = "Zoom factor";
            // 
            // pctBackColor
            // 
            this.pctBackColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pctBackColor.Location = new System.Drawing.Point(115, 138);
            this.pctBackColor.Name = "pctBackColor";
            this.pctBackColor.Size = new System.Drawing.Size(26, 26);
            this.pctBackColor.TabIndex = 9;
            this.pctBackColor.TabStop = false;
            this.pctBackColor.Click += new System.EventHandler(this.pctBackColor_Click);
            // 
            // lblBackColor
            // 
            this.lblBackColor.AutoSize = true;
            this.lblBackColor.Location = new System.Drawing.Point(42, 143);
            this.lblBackColor.Name = "lblBackColor";
            this.lblBackColor.Size = new System.Drawing.Size(71, 19);
            this.lblBackColor.TabIndex = 8;
            this.lblBackColor.Text = "Back color";
            // 
            // chkWordWrap
            // 
            this.chkWordWrap.AutoSize = true;
            this.chkWordWrap.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkWordWrap.Location = new System.Drawing.Point(42, 113);
            this.chkWordWrap.Name = "chkWordWrap";
            this.chkWordWrap.Size = new System.Drawing.Size(95, 23);
            this.chkWordWrap.TabIndex = 7;
            this.chkWordWrap.Text = "Word wrap";
            this.chkWordWrap.UseVisualStyleBackColor = true;
            // 
            // lblFont
            // 
            this.lblFont.AutoSize = true;
            this.lblFont.Location = new System.Drawing.Point(42, 19);
            this.lblFont.Name = "lblFont";
            this.lblFont.Size = new System.Drawing.Size(37, 19);
            this.lblFont.TabIndex = 2;
            this.lblFont.Text = "Font";
            // 
            // pctFontColor
            // 
            this.pctFontColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pctFontColor.Location = new System.Drawing.Point(87, 72);
            this.pctFontColor.Name = "pctFontColor";
            this.pctFontColor.Size = new System.Drawing.Size(26, 26);
            this.pctFontColor.TabIndex = 6;
            this.pctFontColor.TabStop = false;
            this.pctFontColor.Click += new System.EventHandler(this.pctFontColor_Click);
            // 
            // lblFontColor
            // 
            this.lblFontColor.AutoSize = true;
            this.lblFontColor.Location = new System.Drawing.Point(42, 77);
            this.lblFontColor.Name = "lblFontColor";
            this.lblFontColor.Size = new System.Drawing.Size(42, 19);
            this.lblFontColor.TabIndex = 5;
            this.lblFontColor.Text = "Color";
            // 
            // btnDlgFont
            // 
            this.btnDlgFont.Location = new System.Drawing.Point(89, 16);
            this.btnDlgFont.Name = "btnDlgFont";
            this.btnDlgFont.Size = new System.Drawing.Size(90, 26);
            this.btnDlgFont.TabIndex = 1;
            this.btnDlgFont.Text = "Select font";
            this.btnDlgFont.UseVisualStyleBackColor = true;
            this.btnDlgFont.Click += new System.EventHandler(this.DlgFont);
            // 
            // lblFontStyle
            // 
            this.lblFontStyle.AutoSize = true;
            this.lblFontStyle.Location = new System.Drawing.Point(42, 48);
            this.lblFontStyle.Name = "lblFontStyle";
            this.lblFontStyle.Size = new System.Drawing.Size(38, 19);
            this.lblFontStyle.TabIndex = 3;
            this.lblFontStyle.Text = "Style";
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
            this.tabGUI.Size = new System.Drawing.Size(441, 251);
            this.tabGUI.TabIndex = 1;
            this.tabGUI.Text = "User interface";
            this.tabGUI.UseVisualStyleBackColor = true;
            // 
            // txtDataFormat
            // 
            this.txtDataFormat.Location = new System.Drawing.Point(260, 197);
            this.txtDataFormat.Name = "txtDataFormat";
            this.txtDataFormat.Size = new System.Drawing.Size(74, 25);
            this.txtDataFormat.TabIndex = 3;
            // 
            // lblDataFormat
            // 
            this.lblDataFormat.AutoSize = true;
            this.lblDataFormat.Location = new System.Drawing.Point(19, 200);
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
            this.grpCulture.Size = new System.Drawing.Size(304, 150);
            this.grpCulture.TabIndex = 1;
            this.grpCulture.TabStop = false;
            this.grpCulture.Text = "UI and data format";
            // 
            // cboAllCultures
            // 
            this.cboAllCultures.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboAllCultures.Enabled = false;
            this.cboAllCultures.FormattingEnabled = true;
            this.cboAllCultures.Location = new System.Drawing.Point(40, 113);
            this.cboAllCultures.Name = "cboAllCultures";
            this.cboAllCultures.Size = new System.Drawing.Size(190, 25);
            this.cboAllCultures.TabIndex = 3;
            this.cboAllCultures.SelectedValueChanged += new System.EventHandler(this.AllCultures_SelectedValueChanged);
            // 
            // radUserCulture
            // 
            this.radUserCulture.AutoSize = true;
            this.radUserCulture.Location = new System.Drawing.Point(18, 87);
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
            this.radInvariantCulture.Location = new System.Drawing.Point(18, 55);
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
            this.radCurrentCulture.Location = new System.Drawing.Point(18, 24);
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
            this.chkDlgPath.Location = new System.Drawing.Point(23, 166);
            this.chkDlgPath.Name = "chkDlgPath";
            this.chkDlgPath.Size = new System.Drawing.Size(290, 23);
            this.chkDlgPath.TabIndex = 0;
            this.chkDlgPath.Text = "Remember open/save dialog previous path";
            this.chkDlgPath.UseVisualStyleBackColor = true;
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(13, 299);
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
            this.ClientSize = new System.Drawing.Size(474, 341);
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
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackZoomFactor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.updZoomFactor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctBackColor)).EndInit();
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
        private Label lblFont;
        private PictureBox pctFontColor;
        private Label lblFontColor;
        private Label lblFontStyle;
        private PictureBox pctBackColor;
        private Label lblBackColor;
        private CheckBox chkWordWrap;
        private PictureBox pictureBox1;
        private TrackBar trackZoomFactor;
        private NumericUpDown updZoomFactor;
        private Label lblZoomFactor;
    }
}