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
            btnCancel = new Button();
            btnAccept = new Button();
            tabSettings = new TabControl();
            tabPlot = new TabPage();
            pictureBox1 = new PictureBox();
            trackZoomFactor = new TrackBar();
            updZoomFactor = new NumericUpDown();
            lblZoomFactor = new Label();
            pctBackColor = new PictureBox();
            lblBackColor = new Label();
            chkWordWrap = new CheckBox();
            lblFont = new Label();
            pctFontColor = new PictureBox();
            lblFontColor = new Label();
            btnDlgFont = new Button();
            lblFontStyle = new Label();
            tabGUI = new TabPage();
            txtDataFormat = new TextBox();
            lblDataFormat = new Label();
            grpCulture = new GroupBox();
            cboAllCultures = new ComboBox();
            radUserCulture = new RadioButton();
            radInvariantCulture = new RadioButton();
            radCurrentCulture = new RadioButton();
            chkDlgPath = new CheckBox();
            chkWindowPos = new CheckBox();
            btnReset = new Button();
            tabSettings.SuspendLayout();
            tabPlot.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackZoomFactor).BeginInit();
            ((System.ComponentModel.ISupportInitialize)updZoomFactor).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pctBackColor).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pctFontColor).BeginInit();
            tabGUI.SuspendLayout();
            grpCulture.SuspendLayout();
            SuspendLayout();
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(256, 320);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(100, 30);
            btnCancel.TabIndex = 5;
            btnCancel.Text = "&Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += Cancel_Click;
            // 
            // btnAccept
            // 
            btnAccept.Location = new Point(362, 320);
            btnAccept.Name = "btnAccept";
            btnAccept.Size = new Size(100, 30);
            btnAccept.TabIndex = 6;
            btnAccept.Text = "&Accept";
            btnAccept.UseVisualStyleBackColor = true;
            btnAccept.Click += Accept_Click;
            // 
            // tabSettings
            // 
            tabSettings.Controls.Add(tabPlot);
            tabSettings.Controls.Add(tabGUI);
            tabSettings.Location = new Point(12, 14);
            tabSettings.Name = "tabSettings";
            tabSettings.SelectedIndex = 0;
            tabSettings.Size = new Size(449, 299);
            tabSettings.TabIndex = 11;
            // 
            // tabPlot
            // 
            tabPlot.Controls.Add(pictureBox1);
            tabPlot.Controls.Add(trackZoomFactor);
            tabPlot.Controls.Add(updZoomFactor);
            tabPlot.Controls.Add(lblZoomFactor);
            tabPlot.Controls.Add(pctBackColor);
            tabPlot.Controls.Add(lblBackColor);
            tabPlot.Controls.Add(chkWordWrap);
            tabPlot.Controls.Add(lblFont);
            tabPlot.Controls.Add(pctFontColor);
            tabPlot.Controls.Add(lblFontColor);
            tabPlot.Controls.Add(btnDlgFont);
            tabPlot.Controls.Add(lblFontStyle);
            tabPlot.Location = new Point(4, 26);
            tabPlot.Name = "tabPlot";
            tabPlot.Padding = new Padding(3);
            tabPlot.Size = new Size(441, 269);
            tabPlot.TabIndex = 0;
            tabPlot.Text = "Text and font";
            tabPlot.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.LightGray;
            pictureBox1.Location = new Point(42, 106);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(350, 1);
            pictureBox1.TabIndex = 13;
            pictureBox1.TabStop = false;
            // 
            // trackZoomFactor
            // 
            trackZoomFactor.BackColor = Color.White;
            trackZoomFactor.Location = new Point(38, 201);
            trackZoomFactor.Maximum = 500;
            trackZoomFactor.Name = "trackZoomFactor";
            trackZoomFactor.Size = new Size(363, 45);
            trackZoomFactor.SmallChange = 10;
            trackZoomFactor.TabIndex = 12;
            trackZoomFactor.TickFrequency = 10;
            trackZoomFactor.ValueChanged += trackZoomFactor_ValueChanged;
            // 
            // updZoomFactor
            // 
            updZoomFactor.Location = new Point(152, 170);
            updZoomFactor.Maximum = new decimal(new int[] { 500, 0, 0, 0 });
            updZoomFactor.Name = "updZoomFactor";
            updZoomFactor.Size = new Size(55, 25);
            updZoomFactor.TabIndex = 11;
            updZoomFactor.ValueChanged += updZoomFactor_ValueChanged;
            // 
            // lblZoomFactor
            // 
            lblZoomFactor.AutoSize = true;
            lblZoomFactor.Location = new Point(42, 172);
            lblZoomFactor.Name = "lblZoomFactor";
            lblZoomFactor.Size = new Size(84, 19);
            lblZoomFactor.TabIndex = 10;
            lblZoomFactor.Text = "Zoom factor";
            // 
            // pctBackColor
            // 
            pctBackColor.BorderStyle = BorderStyle.FixedSingle;
            pctBackColor.Location = new Point(115, 138);
            pctBackColor.Name = "pctBackColor";
            pctBackColor.Size = new Size(26, 26);
            pctBackColor.TabIndex = 9;
            pctBackColor.TabStop = false;
            pctBackColor.Click += pctBackColor_Click;
            // 
            // lblBackColor
            // 
            lblBackColor.AutoSize = true;
            lblBackColor.Location = new Point(42, 143);
            lblBackColor.Name = "lblBackColor";
            lblBackColor.Size = new Size(71, 19);
            lblBackColor.TabIndex = 8;
            lblBackColor.Text = "Back color";
            // 
            // chkWordWrap
            // 
            chkWordWrap.AutoSize = true;
            chkWordWrap.CheckAlign = ContentAlignment.MiddleRight;
            chkWordWrap.Location = new Point(42, 113);
            chkWordWrap.Name = "chkWordWrap";
            chkWordWrap.Size = new Size(95, 23);
            chkWordWrap.TabIndex = 7;
            chkWordWrap.Text = "Word wrap";
            chkWordWrap.UseVisualStyleBackColor = true;
            // 
            // lblFont
            // 
            lblFont.AutoSize = true;
            lblFont.Location = new Point(42, 19);
            lblFont.Name = "lblFont";
            lblFont.Size = new Size(37, 19);
            lblFont.TabIndex = 2;
            lblFont.Text = "Font";
            // 
            // pctFontColor
            // 
            pctFontColor.BorderStyle = BorderStyle.FixedSingle;
            pctFontColor.Location = new Point(87, 72);
            pctFontColor.Name = "pctFontColor";
            pctFontColor.Size = new Size(26, 26);
            pctFontColor.TabIndex = 6;
            pctFontColor.TabStop = false;
            pctFontColor.Click += pctFontColor_Click;
            // 
            // lblFontColor
            // 
            lblFontColor.AutoSize = true;
            lblFontColor.Location = new Point(42, 77);
            lblFontColor.Name = "lblFontColor";
            lblFontColor.Size = new Size(42, 19);
            lblFontColor.TabIndex = 5;
            lblFontColor.Text = "Color";
            // 
            // btnDlgFont
            // 
            btnDlgFont.Location = new Point(89, 16);
            btnDlgFont.Name = "btnDlgFont";
            btnDlgFont.Size = new Size(90, 26);
            btnDlgFont.TabIndex = 1;
            btnDlgFont.Text = "Select font";
            btnDlgFont.UseVisualStyleBackColor = true;
            btnDlgFont.Click += DlgFont;
            // 
            // lblFontStyle
            // 
            lblFontStyle.AutoSize = true;
            lblFontStyle.Location = new Point(42, 48);
            lblFontStyle.Name = "lblFontStyle";
            lblFontStyle.Size = new Size(38, 19);
            lblFontStyle.TabIndex = 3;
            lblFontStyle.Text = "Style";
            // 
            // tabGUI
            // 
            tabGUI.Controls.Add(txtDataFormat);
            tabGUI.Controls.Add(lblDataFormat);
            tabGUI.Controls.Add(grpCulture);
            tabGUI.Controls.Add(chkDlgPath);
            tabGUI.Controls.Add(chkWindowPos);
            tabGUI.Location = new Point(4, 26);
            tabGUI.Name = "tabGUI";
            tabGUI.Padding = new Padding(3);
            tabGUI.Size = new Size(441, 269);
            tabGUI.TabIndex = 1;
            tabGUI.Text = "User interface";
            tabGUI.UseVisualStyleBackColor = true;
            // 
            // txtDataFormat
            // 
            txtDataFormat.Location = new Point(260, 233);
            txtDataFormat.Name = "txtDataFormat";
            txtDataFormat.Size = new Size(74, 25);
            txtDataFormat.TabIndex = 3;
            txtDataFormat.TextChanged += this.txtDataFormat_TextChanged;
            // 
            // lblDataFormat
            // 
            lblDataFormat.AutoSize = true;
            lblDataFormat.Location = new Point(19, 236);
            lblDataFormat.MaximumSize = new Size(240, 0);
            lblDataFormat.Name = "lblDataFormat";
            lblDataFormat.Size = new Size(201, 19);
            lblDataFormat.TabIndex = 2;
            lblDataFormat.Text = "Numeric data-formatting string";
            lblDataFormat.Click += this.lblDataFormat_Click;
            // 
            // grpCulture
            // 
            grpCulture.Controls.Add(cboAllCultures);
            grpCulture.Controls.Add(radUserCulture);
            grpCulture.Controls.Add(radInvariantCulture);
            grpCulture.Controls.Add(radCurrentCulture);
            grpCulture.Location = new Point(19, 8);
            grpCulture.Name = "grpCulture";
            grpCulture.Size = new Size(304, 150);
            grpCulture.TabIndex = 1;
            grpCulture.TabStop = false;
            grpCulture.Text = "UI and data format";
            // 
            // cboAllCultures
            // 
            cboAllCultures.AutoCompleteMode = AutoCompleteMode.Suggest;
            cboAllCultures.Enabled = false;
            cboAllCultures.FormattingEnabled = true;
            cboAllCultures.Location = new Point(40, 113);
            cboAllCultures.Name = "cboAllCultures";
            cboAllCultures.Size = new Size(190, 25);
            cboAllCultures.TabIndex = 3;
            cboAllCultures.SelectedValueChanged += AllCultures_SelectedValueChanged;
            // 
            // radUserCulture
            // 
            radUserCulture.AutoSize = true;
            radUserCulture.Location = new Point(18, 87);
            radUserCulture.Name = "radUserCulture";
            radUserCulture.Size = new Size(108, 23);
            radUserCulture.TabIndex = 2;
            radUserCulture.TabStop = true;
            radUserCulture.Text = "Select culture";
            radUserCulture.UseVisualStyleBackColor = true;
            radUserCulture.CheckedChanged += UserCulture_CheckedChanged;
            // 
            // radInvariantCulture
            // 
            radInvariantCulture.AutoSize = true;
            radInvariantCulture.Location = new Point(18, 55);
            radInvariantCulture.Name = "radInvariantCulture";
            radInvariantCulture.Size = new Size(196, 23);
            radInvariantCulture.TabIndex = 1;
            radInvariantCulture.TabStop = true;
            radInvariantCulture.Text = "Invariant culture formatting";
            radInvariantCulture.UseVisualStyleBackColor = true;
            radInvariantCulture.CheckedChanged += InvariantCulture_CheckedChanged;
            // 
            // radCurrentCulture
            // 
            radCurrentCulture.AutoSize = true;
            radCurrentCulture.Location = new Point(18, 24);
            radCurrentCulture.Name = "radCurrentCulture";
            radCurrentCulture.Size = new Size(189, 23);
            radCurrentCulture.TabIndex = 0;
            radCurrentCulture.TabStop = true;
            radCurrentCulture.Text = "Current culture formatting";
            radCurrentCulture.UseVisualStyleBackColor = true;
            radCurrentCulture.CheckedChanged += CurrentCulture_CheckedChanged;
            // 
            // chkDlgPath
            // 
            chkDlgPath.AutoSize = true;
            chkDlgPath.Location = new Point(23, 202);
            chkDlgPath.Name = "chkDlgPath";
            chkDlgPath.Size = new Size(290, 23);
            chkDlgPath.TabIndex = 0;
            chkDlgPath.Text = "Remember open/save dialog previous path";
            chkDlgPath.UseVisualStyleBackColor = true;
            chkDlgPath.CheckedChanged += chkDlgPath_CheckedChanged;
            // 
            // chkWindowPos
            // 
            chkWindowPos.AutoSize = true;
            chkWindowPos.Location = new Point(23, 168);
            chkWindowPos.MaximumSize = new Size(415, 0);
            chkWindowPos.Name = "chkWindowPos";
            chkWindowPos.Size = new Size(319, 23);
            chkWindowPos.TabIndex = 0;
            chkWindowPos.Text = "Remember window size and position on startup";
            chkWindowPos.UseVisualStyleBackColor = true;
            // 
            // btnReset
            // 
            btnReset.Location = new Point(13, 320);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(100, 30);
            btnReset.TabIndex = 12;
            btnReset.Text = "&Reset";
            btnReset.UseVisualStyleBackColor = true;
            btnReset.Click += Reset_Click;
            // 
            // FrmSettings
            // 
            AcceptButton = btnAccept;
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(474, 361);
            Controls.Add(btnReset);
            Controls.Add(tabSettings);
            Controls.Add(btnAccept);
            Controls.Add(btnCancel);
            Font = new Font("Segoe UI", 10F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmSettings";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Settings";
            tabSettings.ResumeLayout(false);
            tabPlot.ResumeLayout(false);
            tabPlot.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackZoomFactor).EndInit();
            ((System.ComponentModel.ISupportInitialize)updZoomFactor).EndInit();
            ((System.ComponentModel.ISupportInitialize)pctBackColor).EndInit();
            ((System.ComponentModel.ISupportInitialize)pctFontColor).EndInit();
            tabGUI.ResumeLayout(false);
            tabGUI.PerformLayout();
            grpCulture.ResumeLayout(false);
            grpCulture.PerformLayout();
            ResumeLayout(false);
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
        private CheckBox chkWindowPos;
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