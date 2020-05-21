namespace CrossPlanGenerator
{
    partial class FormLabelSettings
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
            if (disposing && (components != null)) {
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.picColor = new System.Windows.Forms.PictureBox();
            this.lnkFont = new System.Windows.Forms.LinkLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.picDarkColor = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.fontDialog = new System.Windows.Forms.FontDialog();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radInvertLabelColors = new System.Windows.Forms.RadioButton();
            this.radDarkBackgroundColorUpperBound = new System.Windows.Forms.RadioButton();
            this.picDarkBackgroundColorUpperBound = new System.Windows.Forms.PictureBox();
            this.lblB = new System.Windows.Forms.Label();
            this.lblG = new System.Windows.Forms.Label();
            this.lblR = new System.Windows.Forms.Label();
            this.trackB = new System.Windows.Forms.TrackBar();
            this.trackG = new System.Windows.Forms.TrackBar();
            this.trackR = new System.Windows.Forms.TrackBar();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDarkColor)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picDarkBackgroundColorUpperBound)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackG)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackR)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.picColor);
            this.groupBox1.Controls.Add(this.lnkFont);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.ForeColor = System.Drawing.Color.Blue;
            this.groupBox1.Location = new System.Drawing.Point(12, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.groupBox1.Size = new System.Drawing.Size(379, 98);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "برچسب گذاری";
            // 
            // picColor
            // 
            this.picColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picColor.BackColor = System.Drawing.Color.Blue;
            this.picColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picColor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picColor.Location = new System.Drawing.Point(255, 61);
            this.picColor.Name = "picColor";
            this.picColor.Size = new System.Drawing.Size(25, 25);
            this.picColor.TabIndex = 8;
            this.picColor.TabStop = false;
            this.picColor.Click += new System.EventHandler(this.picColor_Click);
            // 
            // lnkFont
            // 
            this.lnkFont.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkFont.Location = new System.Drawing.Point(7, 28);
            this.lnkFont.Name = "lnkFont";
            this.lnkFont.Size = new System.Drawing.Size(276, 13);
            this.lnkFont.TabIndex = 1;
            this.lnkFont.TabStop = true;
            this.lnkFont.Text = "linkLabel1";
            this.lnkFont.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkFont_LinkClicked);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(294, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "رنگ پیش فرض:";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(289, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "فونت پیش فرض:";
            // 
            // picDarkColor
            // 
            this.picDarkColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picDarkColor.BackColor = System.Drawing.Color.WhiteSmoke;
            this.picDarkColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picDarkColor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picDarkColor.Enabled = false;
            this.picDarkColor.Location = new System.Drawing.Point(51, 246);
            this.picDarkColor.Name = "picDarkColor";
            this.picDarkColor.Size = new System.Drawing.Size(25, 25);
            this.picDarkColor.TabIndex = 8;
            this.picDarkColor.TabStop = false;
            this.picDarkColor.Click += new System.EventHandler(this.picDarkColor_Click);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label4.Location = new System.Drawing.Point(16, 162);
            this.label4.Name = "label4";
            this.label4.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label4.Size = new System.Drawing.Size(334, 46);
            this.label4.TabIndex = 0;
            this.label4.Text = "رنگی تیره محسوب می شود که مولفه های RGB آن هر سه کوچکتر و یا مساوی مولفه های RGB " +
    "رنگ تعریف شده در بالا باشد (رنگی که از رنگ تعریف شده در بالا تیره تر باشد).";
            // 
            // btnOk
            // 
            this.btnOk.ForeColor = System.Drawing.Color.Blue;
            this.btnOk.Location = new System.Drawing.Point(316, 409);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 27);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "تایید";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnCancel.Location = new System.Drawing.Point(235, 409);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 27);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "لغو";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radInvertLabelColors);
            this.groupBox2.Controls.Add(this.radDarkBackgroundColorUpperBound);
            this.groupBox2.Controls.Add(this.picDarkBackgroundColorUpperBound);
            this.groupBox2.Controls.Add(this.lblB);
            this.groupBox2.Controls.Add(this.lblG);
            this.groupBox2.Controls.Add(this.lblR);
            this.groupBox2.Controls.Add(this.trackB);
            this.groupBox2.Controls.Add(this.trackG);
            this.groupBox2.Controls.Add(this.trackR);
            this.groupBox2.Controls.Add(this.picDarkColor);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.ForeColor = System.Drawing.Color.Blue;
            this.groupBox2.Location = new System.Drawing.Point(12, 111);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.groupBox2.Size = new System.Drawing.Size(379, 284);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "رنگ برچسب برای نواحی تیره";
            // 
            // radInvertLabelColors
            // 
            this.radInvertLabelColors.AutoSize = true;
            this.radInvertLabelColors.Checked = true;
            this.radInvertLabelColors.ForeColor = System.Drawing.Color.Black;
            this.radInvertLabelColors.Location = new System.Drawing.Point(172, 226);
            this.radInvertLabelColors.Name = "radInvertLabelColors";
            this.radInvertLabelColors.Size = new System.Drawing.Size(198, 17);
            this.radInvertLabelColors.TabIndex = 12;
            this.radInvertLabelColors.TabStop = true;
            this.radInvertLabelColors.Text = "رنگ برچسب نواحی تیره معکوس شود";
            this.radInvertLabelColors.UseVisualStyleBackColor = true;
            // 
            // radDarkBackgroundColorUpperBound
            // 
            this.radDarkBackgroundColorUpperBound.AutoSize = true;
            this.radDarkBackgroundColorUpperBound.ForeColor = System.Drawing.Color.Black;
            this.radDarkBackgroundColorUpperBound.Location = new System.Drawing.Point(82, 249);
            this.radDarkBackgroundColorUpperBound.Name = "radDarkBackgroundColorUpperBound";
            this.radDarkBackgroundColorUpperBound.Size = new System.Drawing.Size(288, 17);
            this.radDarkBackgroundColorUpperBound.TabIndex = 12;
            this.radDarkBackgroundColorUpperBound.Text = "برای نواحی تیره از این رنگ برای برچسب ها استفاده شود";
            this.radDarkBackgroundColorUpperBound.UseVisualStyleBackColor = true;
            this.radDarkBackgroundColorUpperBound.CheckedChanged += new System.EventHandler(this.radDarkBackgroundColorUpperBound_CheckedChanged);
            // 
            // picDarkBackgroundColorUpperBound
            // 
            this.picDarkBackgroundColorUpperBound.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picDarkBackgroundColorUpperBound.Location = new System.Drawing.Point(16, 56);
            this.picDarkBackgroundColorUpperBound.Name = "picDarkBackgroundColorUpperBound";
            this.picDarkBackgroundColorUpperBound.Size = new System.Drawing.Size(72, 81);
            this.picDarkBackgroundColorUpperBound.TabIndex = 11;
            this.picDarkBackgroundColorUpperBound.TabStop = false;
            // 
            // lblB
            // 
            this.lblB.AutoSize = true;
            this.lblB.Location = new System.Drawing.Point(104, 124);
            this.lblB.Name = "lblB";
            this.lblB.Size = new System.Drawing.Size(25, 13);
            this.lblB.TabIndex = 10;
            this.lblB.Text = "255";
            // 
            // lblG
            // 
            this.lblG.AutoSize = true;
            this.lblG.ForeColor = System.Drawing.Color.Green;
            this.lblG.Location = new System.Drawing.Point(104, 90);
            this.lblG.Name = "lblG";
            this.lblG.Size = new System.Drawing.Size(25, 13);
            this.lblG.TabIndex = 10;
            this.lblG.Text = "255";
            // 
            // lblR
            // 
            this.lblR.AutoSize = true;
            this.lblR.ForeColor = System.Drawing.Color.Red;
            this.lblR.Location = new System.Drawing.Point(104, 56);
            this.lblR.Name = "lblR";
            this.lblR.Size = new System.Drawing.Size(25, 13);
            this.lblR.TabIndex = 10;
            this.lblR.Text = "255";
            // 
            // trackB
            // 
            this.trackB.AutoSize = false;
            this.trackB.Location = new System.Drawing.Point(134, 120);
            this.trackB.Maximum = 255;
            this.trackB.Name = "trackB";
            this.trackB.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.trackB.Size = new System.Drawing.Size(164, 26);
            this.trackB.TabIndex = 9;
            this.trackB.TickFrequency = 10;
            this.trackB.Value = 255;
            this.trackB.ValueChanged += new System.EventHandler(this.TrackRGB_ValueChanged);
            // 
            // trackG
            // 
            this.trackG.AutoSize = false;
            this.trackG.Location = new System.Drawing.Point(134, 86);
            this.trackG.Maximum = 255;
            this.trackG.Name = "trackG";
            this.trackG.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.trackG.Size = new System.Drawing.Size(164, 26);
            this.trackG.TabIndex = 9;
            this.trackG.TickFrequency = 10;
            this.trackG.Value = 255;
            this.trackG.ValueChanged += new System.EventHandler(this.TrackRGB_ValueChanged);
            // 
            // trackR
            // 
            this.trackR.AutoSize = false;
            this.trackR.Location = new System.Drawing.Point(134, 52);
            this.trackR.Maximum = 255;
            this.trackR.Name = "trackR";
            this.trackR.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.trackR.Size = new System.Drawing.Size(164, 26);
            this.trackR.TabIndex = 9;
            this.trackR.TickFrequency = 10;
            this.trackR.Value = 255;
            this.trackR.ValueChanged += new System.EventHandler(this.TrackRGB_ValueChanged);
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Blue;
            this.label8.Location = new System.Drawing.Point(304, 124);
            this.label8.Name = "label8";
            this.label8.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label8.Size = new System.Drawing.Size(46, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "مولفه B:";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Green;
            this.label7.Location = new System.Drawing.Point(304, 90);
            this.label7.Name = "label7";
            this.label7.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label7.Size = new System.Drawing.Size(47, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "مولفه G:";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(304, 56);
            this.label6.Name = "label6";
            this.label6.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label6.Size = new System.Drawing.Size(47, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "مولفه R:";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(268, 27);
            this.label5.Name = "label5";
            this.label5.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label5.Size = new System.Drawing.Size(105, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "تعریف رنگ ناحیه تیره:";
            // 
            // FormLabelSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(404, 445);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormLabelSettings";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = " تنظیمات برچسب";
            this.Load += new System.EventHandler(this.FormLabelSettings_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDarkColor)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picDarkBackgroundColorUpperBound)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackR)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.FontDialog fontDialog;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.LinkLabel lnkFont;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox picColor;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.PictureBox picDarkColor;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TrackBar trackB;
        private System.Windows.Forms.TrackBar trackG;
        private System.Windows.Forms.TrackBar trackR;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox picDarkBackgroundColorUpperBound;
        private System.Windows.Forms.Label lblB;
        private System.Windows.Forms.Label lblG;
        private System.Windows.Forms.Label lblR;
        private System.Windows.Forms.RadioButton radDarkBackgroundColorUpperBound;
        private System.Windows.Forms.RadioButton radInvertLabelColors;
    }
}