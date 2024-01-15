
namespace ErgoCalc
{
    partial class FrmResultsLiberty
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
            formsPlot1 = new ScottPlot.FormsPlot();
            formsPlot3 = new ScottPlot.FormsPlot();
            formsPlot2 = new ScottPlot.FormsPlot();
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            pictureBox3 = new PictureBox();
            rtbShowResult = new RichTextBox();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
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
            splitContainer2.SplitterDistance = 198;
            splitContainer2.TabIndex = 3;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 6;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33444F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 98F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33444F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 98F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33112F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 98F));
            tableLayoutPanel1.Controls.Add(formsPlot1, 0, 0);
            tableLayoutPanel1.Controls.Add(formsPlot3, 4, 0);
            tableLayoutPanel1.Controls.Add(formsPlot2, 2, 0);
            tableLayoutPanel1.Controls.Add(pictureBox1, 1, 0);
            tableLayoutPanel1.Controls.Add(pictureBox2, 3, 0);
            tableLayoutPanel1.Controls.Add(pictureBox3, 5, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(892, 198);
            tableLayoutPanel1.TabIndex = 2;
            // 
            // formsPlot1
            // 
            formsPlot1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            formsPlot1.BackColor = Color.Transparent;
            formsPlot1.Location = new Point(4, 3);
            formsPlot1.Margin = new Padding(4, 3, 4, 3);
            formsPlot1.Name = "formsPlot1";
            formsPlot1.Size = new Size(191, 192);
            formsPlot1.TabIndex = 0;
            formsPlot1.TabStop = false;
            // 
            // formsPlot3
            // 
            formsPlot3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            formsPlot3.BackColor = Color.Transparent;
            formsPlot3.Location = new Point(598, 3);
            formsPlot3.Margin = new Padding(4, 3, 4, 3);
            formsPlot3.Name = "formsPlot3";
            formsPlot3.Size = new Size(191, 192);
            formsPlot3.TabIndex = 6;
            formsPlot3.TabStop = false;
            // 
            // formsPlot2
            // 
            formsPlot2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            formsPlot2.BackColor = Color.Transparent;
            formsPlot2.Location = new Point(301, 3);
            formsPlot2.Margin = new Padding(4, 3, 4, 3);
            formsPlot2.Name = "formsPlot2";
            formsPlot2.Size = new Size(191, 192);
            formsPlot2.TabIndex = 2;
            formsPlot2.TabStop = false;
            // 
            // pictureBox1
            // 
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.Location = new Point(202, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(92, 192);
            pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox1.TabIndex = 7;
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.Dock = DockStyle.Fill;
            pictureBox2.Location = new Point(499, 3);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(92, 192);
            pictureBox2.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox2.TabIndex = 8;
            pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            pictureBox3.Dock = DockStyle.Fill;
            pictureBox3.Location = new Point(796, 3);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(93, 192);
            pictureBox3.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox3.TabIndex = 9;
            pictureBox3.TabStop = false;
            // 
            // rtbShowResult
            // 
            rtbShowResult.AcceptsTab = true;
            rtbShowResult.BackColor = SystemColors.Window;
            rtbShowResult.Dock = DockStyle.Fill;
            rtbShowResult.Font = new Font("Consolas", 10F);
            rtbShowResult.Location = new Point(0, 0);
            rtbShowResult.Name = "rtbShowResult";
            rtbShowResult.ReadOnly = true;
            rtbShowResult.ShowSelectionMargin = true;
            rtbShowResult.Size = new Size(892, 238);
            rtbShowResult.TabIndex = 0;
            rtbShowResult.Text = "";
            rtbShowResult.WordWrap = false;
            // 
            // FrmResultsLiberty
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(892, 440);
            Controls.Add(splitContainer2);
            Font = new Font("Microsoft Sans Serif", 9F);
            Name = "FrmResultsLiberty";
            Text = "LM-MMH results";
            Activated += FrmResultsLiberty_Activated;
            Shown += FrmResultsLiberty_Shown;
            splitContainer2.Panel1.ResumeLayout(false);
            splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
            splitContainer2.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splitContainer2;
        private TableLayoutPanel tableLayoutPanel1;
        private ScottPlot.FormsPlot formsPlot1;
        private ScottPlot.FormsPlot formsPlot3;
        private ScottPlot.FormsPlot formsPlot2;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private PictureBox pictureBox3;
        private RichTextBox rtbShowResult;
    }
}