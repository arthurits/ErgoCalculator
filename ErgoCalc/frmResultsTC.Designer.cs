
namespace ErgoCalc
{
    partial class FrmResultsTC
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
            splitContainer2 = new SplitContainer();
            tableLayoutPanel1 = new TableLayoutPanel();
            formsPlot2 = new ScottPlot.FormsPlotCulture();
            pictureBox1 = new PictureBox();
            formsPlot1 = new ScottPlot.FormsPlotCulture();
            pictureBox2 = new PictureBox();
            rtbShowResult = new RichTextBox();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // splitContainer2
            // 
            splitContainer2.Dock = DockStyle.Fill;
            splitContainer2.Location = new Point(0, 0);
            splitContainer2.Name = "splitContainer2";
            splitContainer2.Orientation = Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            splitContainer2.Panel1.Controls.Add(tableLayoutPanel1);
            // 
            // splitContainer2.Panel2
            // 
            splitContainer2.Panel2.Controls.Add(rtbShowResult);
            splitContainer2.Size = new Size(892, 440);
            splitContainer2.SplitterDistance = 199;
            splitContainer2.TabIndex = 3;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 4;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 141F));
            tableLayoutPanel1.Controls.Add(formsPlot2, 0, 0);
            tableLayoutPanel1.Controls.Add(pictureBox1, 0, 0);
            tableLayoutPanel1.Controls.Add(formsPlot1, 0, 0);
            tableLayoutPanel1.Controls.Add(pictureBox2, 3, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(892, 199);
            tableLayoutPanel1.TabIndex = 2;
            // 
            // formsPlot2
            // 
            formsPlot2.Dock = DockStyle.Fill;
            formsPlot2.Location = new Point(429, 3);
            formsPlot2.Margin = new Padding(4, 3, 4, 3);
            formsPlot2.Name = "formsPlot2";
            formsPlot2.Size = new Size(317, 193);
            formsPlot2.TabIndex = 5;
            formsPlot2.TabStop = false;
            // 
            // pictureBox1
            // 
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.Location = new Point(328, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(94, 193);
            pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox1.TabIndex = 4;
            pictureBox1.TabStop = false;
            // 
            // formsPlot1
            // 
            formsPlot1.Dock = DockStyle.Fill;
            formsPlot1.Location = new Point(4, 3);
            formsPlot1.Margin = new Padding(4, 3, 4, 3);
            formsPlot1.Name = "formsPlot1";
            formsPlot1.Size = new Size(317, 193);
            formsPlot1.TabIndex = 0;
            formsPlot1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.Dock = DockStyle.Fill;
            pictureBox2.Location = new Point(753, 3);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(136, 193);
            pictureBox2.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox2.TabIndex = 6;
            pictureBox2.TabStop = false;
            // 
            // rtbShowResult
            // 
            rtbShowResult.AcceptsTab = true;
            rtbShowResult.BackColor = SystemColors.Window;
            rtbShowResult.Dock = DockStyle.Fill;
            rtbShowResult.Font = new Font("Lucida Sans", 9F);
            rtbShowResult.Location = new Point(0, 0);
            rtbShowResult.Name = "rtbShowResult";
            rtbShowResult.ReadOnly = true;
            rtbShowResult.ShowSelectionMargin = true;
            rtbShowResult.Size = new Size(892, 237);
            rtbShowResult.TabIndex = 0;
            rtbShowResult.Text = "";
            rtbShowResult.WordWrap = false;
            // 
            // FrmResultsTC
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(892, 440);
            Controls.Add(splitContainer2);
            Font = new Font("Microsoft Sans Serif", 9F);
            Name = "FrmResultsTC";
            Text = "Thermal comfort results";
            Activated += FrmResultsTC_Activated;
            Shown += FrmResultsTC_Shown;
            splitContainer2.Panel1.ResumeLayout(false);
            splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
            splitContainer2.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splitContainer2;
        private TableLayoutPanel tableLayoutPanel1;
        private ScottPlot.FormsPlotCulture formsPlot2;
        private PictureBox pictureBox1;
        private ScottPlot.FormsPlotCulture formsPlot1;
        private PictureBox pictureBox2;
        private RichTextBox rtbShowResult;
    }
}