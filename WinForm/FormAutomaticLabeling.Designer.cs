namespace CrossPlanGenerator
{
    partial class FormAutomaticLabeling
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAutomaticLabeling));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkFirstDeleteAllLabels = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.lblMinBoundariesArea = new System.Windows.Forms.Label();
            this.numRegionMinWidth = new System.Windows.Forms.NumericUpDown();
            this.numRegionMinArea = new System.Windows.Forms.NumericUpDown();
            this.numRegionMinHeight = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnAutomaticLabeling = new System.Windows.Forms.Button();
            this.btnDeleteAddedLabels = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRegionMinWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRegionMinArea)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRegionMinHeight)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkFirstDeleteAllLabels);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.lblMinBoundariesArea);
            this.groupBox1.Controls.Add(this.numRegionMinWidth);
            this.groupBox1.Controls.Add(this.numRegionMinArea);
            this.groupBox1.Controls.Add(this.numRegionMinHeight);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(306, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.groupBox1.Size = new System.Drawing.Size(416, 246);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "پارامتر ها";
            // 
            // chkFirstDeleteAllLabels
            // 
            this.chkFirstDeleteAllLabels.AutoSize = true;
            this.chkFirstDeleteAllLabels.Location = new System.Drawing.Point(189, 218);
            this.chkFirstDeleteAllLabels.Name = "chkFirstDeleteAllLabels";
            this.chkFirstDeleteAllLabels.Size = new System.Drawing.Size(208, 17);
            this.chkFirstDeleteAllLabels.TabIndex = 3;
            this.chkFirstDeleteAllLabels.Text = "ابتدا همه برچسب های موجود حذف شوند";
            this.chkFirstDeleteAllLabels.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.Green;
            this.label10.Location = new System.Drawing.Point(6, 88);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(93, 13);
            this.label10.TabIndex = 2;
            this.label10.Tag = "";
            this.label10.Text = "پیکسل مربع باشد)";
            // 
            // lblMinBoundariesArea
            // 
            this.lblMinBoundariesArea.ForeColor = System.Drawing.Color.Green;
            this.lblMinBoundariesArea.Location = new System.Drawing.Point(105, 89);
            this.lblMinBoundariesArea.Name = "lblMinBoundariesArea";
            this.lblMinBoundariesArea.Size = new System.Drawing.Size(60, 13);
            this.lblMinBoundariesArea.TabIndex = 2;
            this.lblMinBoundariesArea.Tag = "";
            this.lblMinBoundariesArea.Text = "36";
            this.lblMinBoundariesArea.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // numRegionMinWidth
            // 
            this.numRegionMinWidth.Location = new System.Drawing.Point(138, 30);
            this.numRegionMinWidth.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.numRegionMinWidth.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numRegionMinWidth.Name = "numRegionMinWidth";
            this.numRegionMinWidth.Size = new System.Drawing.Size(65, 21);
            this.numRegionMinWidth.TabIndex = 1;
            this.numRegionMinWidth.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.numRegionMinWidth.ValueChanged += new System.EventHandler(this.numRegionMin_ValueChanged);
            // 
            // numRegionMinArea
            // 
            this.numRegionMinArea.Location = new System.Drawing.Point(193, 141);
            this.numRegionMinArea.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numRegionMinArea.Name = "numRegionMinArea";
            this.numRegionMinArea.Size = new System.Drawing.Size(41, 21);
            this.numRegionMinArea.TabIndex = 1;
            this.numRegionMinArea.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // numRegionMinHeight
            // 
            this.numRegionMinHeight.Location = new System.Drawing.Point(138, 58);
            this.numRegionMinHeight.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.numRegionMinHeight.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numRegionMinHeight.Name = "numRegionMinHeight";
            this.numRegionMinHeight.Size = new System.Drawing.Size(65, 21);
            this.numRegionMinHeight.TabIndex = 1;
            this.numRegionMinHeight.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.numRegionMinHeight.ValueChanged += new System.EventHandler(this.numRegionMin_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(114, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "پیکسل صرف نظر شود.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "پیکسل صرف نظر شود.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(215, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(195, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "1. از برچسب گذاری نواحی با طول کمتر از";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(5, 144);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(186, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "مساحت مستطیل محاط کننده آن باشد.";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(235, 143);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(163, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "مساحت ناحیه رنگی آن حداقل   %";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(232, 117);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(178, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "3. تنها نواحی شماره گذاری شوند که:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Green;
            this.label8.Location = new System.Drawing.Point(166, 88);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(232, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "(یعنی مساحت مستطیل محاط کننده ناحیه حداقل";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(209, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(201, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "2. از برچسب گذاری نواحی با عرض کمتر از";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.pictureBox1);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.groupBox2.Size = new System.Drawing.Size(288, 246);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "مثال";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.ForeColor = System.Drawing.Color.BlueViolet;
            this.label14.Location = new System.Drawing.Point(227, 100);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(57, 65);
            this.label14.TabIndex = 1;
            this.label14.Text = "ناحیه\r\nرنگی\r\nبه مساحت\r\n14 پیکسل\r\nمربع";
            this.label14.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.Color.Green;
            this.label12.Location = new System.Drawing.Point(5, 100);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(57, 52);
            this.label12.TabIndex = 1;
            this.label12.Text = "عرض\r\nمستطیل\r\nمحاط کننده\r\n(5 پیکسل)";
            this.label12.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ForeColor = System.Drawing.Color.Red;
            this.label13.Location = new System.Drawing.Point(58, 20);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(155, 26);
            this.label13.TabIndex = 1;
            this.label13.Text = "مستطیل محاط کننده ناحیه رنگی\r\n(به مساحت 25 پیکسل مربع)";
            this.label13.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.Green;
            this.label11.Location = new System.Drawing.Point(74, 203);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(124, 26);
            this.label11.TabIndex = 1;
            this.label11.Text = "طول مستطیل محاط کننده\r\n(5 پیکسل)";
            this.label11.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(62, 51);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(163, 149);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // btnAutomaticLabeling
            // 
            this.btnAutomaticLabeling.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnAutomaticLabeling.Location = new System.Drawing.Point(551, 281);
            this.btnAutomaticLabeling.Name = "btnAutomaticLabeling";
            this.btnAutomaticLabeling.Size = new System.Drawing.Size(171, 27);
            this.btnAutomaticLabeling.TabIndex = 1;
            this.btnAutomaticLabeling.Text = "برچسب گذاری اتوماتیک";
            this.btnAutomaticLabeling.UseVisualStyleBackColor = true;
            this.btnAutomaticLabeling.Click += new System.EventHandler(this.btnAutomaticLabeling_Click);
            // 
            // btnDeleteAddedLabels
            // 
            this.btnDeleteAddedLabels.Enabled = false;
            this.btnDeleteAddedLabels.ForeColor = System.Drawing.Color.Red;
            this.btnDeleteAddedLabels.Location = new System.Drawing.Point(374, 281);
            this.btnDeleteAddedLabels.Name = "btnDeleteAddedLabels";
            this.btnDeleteAddedLabels.Size = new System.Drawing.Size(171, 27);
            this.btnDeleteAddedLabels.TabIndex = 1;
            this.btnDeleteAddedLabels.Text = "حذف برچسب های اضافه شده";
            this.btnDeleteAddedLabels.UseVisualStyleBackColor = true;
            this.btnDeleteAddedLabels.Click += new System.EventHandler(this.btnDeleteAddedLabels_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lblStatus.Location = new System.Drawing.Point(12, 285);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblStatus.Size = new System.Drawing.Size(294, 23);
            this.lblStatus.TabIndex = 2;
            // 
            // FormAutomaticLabeling
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(735, 320);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnDeleteAddedLabels);
            this.Controls.Add(this.btnAutomaticLabeling);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormAutomaticLabeling";
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = " برچسب گذاری اتوماتیک";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormAutomaticLabeling_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRegionMinWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRegionMinArea)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRegionMinHeight)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnAutomaticLabeling;
        private System.Windows.Forms.Button btnDeleteAddedLabels;
        private System.Windows.Forms.NumericUpDown numRegionMinHeight;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numRegionMinWidth;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblMinBoundariesArea;
        private System.Windows.Forms.NumericUpDown numRegionMinArea;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.CheckBox chkFirstDeleteAllLabels;
        private System.Windows.Forms.Label lblStatus;
    }
}