namespace CrossPlanGenerator
{
    partial class FormImageCutter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormImageCutter));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.numBorderSize = new System.Windows.Forms.NumericUpDown();
            this.numCutHeight = new System.Windows.Forms.NumericUpDown();
            this.lblSegments = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.numCutWidth = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnCut = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtFooter = new System.Windows.Forms.TextBox();
            this.picFooterColor = new System.Windows.Forms.PictureBox();
            this.lnkFont = new System.Windows.Forms.LinkLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbFileType = new System.Windows.Forms.ComboBox();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.btnSelectPath = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.fontDialog = new System.Windows.Forms.FontDialog();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numBorderSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCutHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCutWidth)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picFooterColor)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.numBorderSize);
            this.groupBox2.Controls.Add(this.numCutHeight);
            this.groupBox2.Controls.Add(this.lblSegments);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.numCutWidth);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.ForeColor = System.Drawing.Color.Blue;
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.groupBox2.Size = new System.Drawing.Size(509, 134);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "تنظیمات برش";
            // 
            // numBorderSize
            // 
            this.numBorderSize.Location = new System.Drawing.Point(202, 92);
            this.numBorderSize.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numBorderSize.Name = "numBorderSize";
            this.numBorderSize.Size = new System.Drawing.Size(59, 21);
            this.numBorderSize.TabIndex = 1;
            this.numBorderSize.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // numCutHeight
            // 
            this.numCutHeight.Location = new System.Drawing.Point(202, 27);
            this.numCutHeight.Maximum = new decimal(new int[] {
            50000,
            0,
            0,
            0});
            this.numCutHeight.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numCutHeight.Name = "numCutHeight";
            this.numCutHeight.Size = new System.Drawing.Size(59, 21);
            this.numCutHeight.TabIndex = 1;
            this.numCutHeight.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numCutHeight.ValueChanged += new System.EventHandler(this.numCutSize_ValueChanged);
            // 
            // lblSegments
            // 
            this.lblSegments.ForeColor = System.Drawing.Color.Green;
            this.lblSegments.Location = new System.Drawing.Point(6, 29);
            this.lblSegments.Name = "lblSegments";
            this.lblSegments.Size = new System.Drawing.Size(47, 13);
            this.lblSegments.TabIndex = 2;
            this.lblSegments.Text = "-";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.Color.Green;
            this.label12.Location = new System.Drawing.Point(125, 62);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(378, 13);
            this.label12.TabIndex = 2;
            this.label12.Text = "توجه:   طول و عرض برش را با توجه به ابعاد تصویر اولیه(پردازش نشده) انتخاب کنید.";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Green;
            this.label5.Location = new System.Drawing.Point(59, 29);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "تعداد قطعه ها:";
            // 
            // numCutWidth
            // 
            this.numCutWidth.Location = new System.Drawing.Point(390, 27);
            this.numCutWidth.Maximum = new decimal(new int[] {
            50000,
            0,
            0,
            0});
            this.numCutWidth.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numCutWidth.Name = "numCutWidth";
            this.numCutWidth.Size = new System.Drawing.Size(59, 21);
            this.numCutWidth.TabIndex = 1;
            this.numCutWidth.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numCutWidth.ValueChanged += new System.EventHandler(this.numCutSize_ValueChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(159, 29);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(40, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "پیکسل";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(346, 29);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(40, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "پیکسل";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(158, 94);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "پیکسل";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(280, 94);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(223, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "اندازه حاشیه سفید رنگ اطراف هر قطعه تصویر:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(449, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "طول برش:\r\n";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(263, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "عرض برش:\r\n";
            // 
            // btnCut
            // 
            this.btnCut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCut.ForeColor = System.Drawing.Color.Blue;
            this.btnCut.Location = new System.Drawing.Point(436, 386);
            this.btnCut.Name = "btnCut";
            this.btnCut.Size = new System.Drawing.Size(85, 24);
            this.btnCut.TabIndex = 1;
            this.btnCut.Text = "برش";
            this.btnCut.UseVisualStyleBackColor = true;
            this.btnCut.Click += new System.EventHandler(this.btnCut_Click);
            // 
            // btnClose
            // 
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.ForeColor = System.Drawing.Color.Maroon;
            this.btnClose.Location = new System.Drawing.Point(345, 386);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(85, 24);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "بستن پنجره";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtFooter);
            this.groupBox3.Controls.Add(this.picFooterColor);
            this.groupBox3.Controls.Add(this.lnkFont);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.ForeColor = System.Drawing.Color.Blue;
            this.groupBox3.Location = new System.Drawing.Point(12, 152);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.groupBox3.Size = new System.Drawing.Size(509, 100);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "پانویس";
            // 
            // txtFooter
            // 
            this.txtFooter.Location = new System.Drawing.Point(9, 64);
            this.txtFooter.Name = "txtFooter";
            this.txtFooter.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtFooter.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtFooter.Size = new System.Drawing.Size(459, 21);
            this.txtFooter.TabIndex = 3;
            // 
            // picFooterColor
            // 
            this.picFooterColor.BackColor = System.Drawing.Color.Black;
            this.picFooterColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picFooterColor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picFooterColor.Location = new System.Drawing.Point(9, 22);
            this.picFooterColor.Name = "picFooterColor";
            this.picFooterColor.Size = new System.Drawing.Size(25, 25);
            this.picFooterColor.TabIndex = 2;
            this.picFooterColor.TabStop = false;
            this.picFooterColor.Click += new System.EventHandler(this.picFooterColor_Click);
            // 
            // lnkFont
            // 
            this.lnkFont.Location = new System.Drawing.Point(107, 27);
            this.lnkFont.Name = "lnkFont";
            this.lnkFont.Size = new System.Drawing.Size(326, 13);
            this.lnkFont.TabIndex = 1;
            this.lnkFont.TabStop = true;
            this.lnkFont.Text = "linkLabel1";
            this.lnkFont.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkFont_LinkClicked);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(40, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "رنگ متن:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(474, 67);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "متن:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(437, 27);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(34, 13);
            this.label10.TabIndex = 0;
            this.label10.Text = "فونت:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbFileType);
            this.groupBox1.Controls.Add(this.txtPath);
            this.groupBox1.Controls.Add(this.btnSelectPath);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.ForeColor = System.Drawing.Color.Blue;
            this.groupBox1.Location = new System.Drawing.Point(12, 260);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.groupBox1.Size = new System.Drawing.Size(509, 105);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ذخیره فایل ها";
            // 
            // cmbFileType
            // 
            this.cmbFileType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFileType.FormattingEnabled = true;
            this.cmbFileType.Items.AddRange(new object[] {
            "PNG",
            "BMP",
            "JPG",
            "TIFF"});
            this.cmbFileType.Location = new System.Drawing.Point(324, 68);
            this.cmbFileType.Name = "cmbFileType";
            this.cmbFileType.Size = new System.Drawing.Size(54, 21);
            this.cmbFileType.TabIndex = 6;
            // 
            // txtPath
            // 
            this.txtPath.BackColor = System.Drawing.Color.White;
            this.txtPath.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtPath.Location = new System.Drawing.Point(9, 31);
            this.txtPath.Name = "txtPath";
            this.txtPath.ReadOnly = true;
            this.txtPath.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtPath.Size = new System.Drawing.Size(369, 21);
            this.txtPath.TabIndex = 5;
            // 
            // btnSelectPath
            // 
            this.btnSelectPath.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectPath.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnSelectPath.Location = new System.Drawing.Point(390, 29);
            this.btnSelectPath.Name = "btnSelectPath";
            this.btnSelectPath.Size = new System.Drawing.Size(113, 24);
            this.btnSelectPath.TabIndex = 4;
            this.btnSelectPath.Text = "انتخاب پوشه";
            this.btnSelectPath.UseVisualStyleBackColor = true;
            this.btnSelectPath.Click += new System.EventHandler(this.btnSelectPath_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(384, 71);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(119, 13);
            this.label11.TabIndex = 0;
            this.label11.Text = "فرمت ذخیره سازی فایل:";
            // 
            // fontDialog
            // 
            this.fontDialog.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            // 
            // FormImageCutter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(533, 419);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnCut);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormImageCutter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ابزار برش دهنده تصویر";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormImageCutter_FormClosing);
            this.Load += new System.EventHandler(this.FormImageCutter_Load);
            this.VisibleChanged += new System.EventHandler(this.FormImageCutter_VisibleChanged);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numBorderSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCutHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCutWidth)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picFooterColor)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnCut;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numCutWidth;
        private System.Windows.Forms.NumericUpDown numCutHeight;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblSegments;
        private System.Windows.Forms.NumericUpDown numBorderSize;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.LinkLabel lnkFont;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.PictureBox picFooterColor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtFooter;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Button btnSelectPath;
        public System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.FontDialog fontDialog;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cmbFileType;
        private System.Windows.Forms.Label label12;
    }
}