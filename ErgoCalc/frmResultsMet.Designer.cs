﻿namespace ErgoCalc
{
    partial class FrmResultsMet
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
            this.rtbShowResult = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // rtbShowResult
            // 
            this.rtbShowResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbShowResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.rtbShowResult.Location = new System.Drawing.Point(0, 0);
            this.rtbShowResult.Name = "rtbShowResult";
            this.rtbShowResult.Size = new System.Drawing.Size(609, 440);
            this.rtbShowResult.TabIndex = 0;
            this.rtbShowResult.Text = "";
            // 
            // FrmResultsMet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(609, 440);
            this.Controls.Add(this.rtbShowResult);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Name = "FrmResultsMet";
            this.Text = "Metabolic rate results";
            this.Shown += new System.EventHandler(this.frmMetResult_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbShowResult;
    }
}