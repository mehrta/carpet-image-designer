using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CrossPlanGenerator
{
    public partial class FormImageCutter : Form
    {
        public ImageAnalyzer ImageAnalyzerObject { set; get; }
        public LabelManager  LabelManagerObject { set; get; }

        public FormImageCutter()
        {
            InitializeComponent();
        }

        private void btnCut_Click(object sender, EventArgs e)
        {
            const string MESSAGE_SAVE_ERROR = "هنگام ذخیره سازی قطه تصویر های تولید شده، خطایی رخ داد.";
            const string MESSAGE_NO_OUTPUT = "ابتدا باید تصویر ورودی پردازش شود.";
            string fileName;
            Image[,] segments;

            // 
            if (ImageAnalyzerObject.Output == null) {
                StaticMethods.ShowMessage(MESSAGE_NO_OUTPUT);
                return;
            }

            // Cut Output Image
            segments = ImageAnalyzerObject.CutOutputImage(
                (int)numCutWidth.Value,
                (int)numCutHeight.Value,
                txtFooter.Text,
                picFooterColor.BackColor,
                fontDialog.Font,
                (int)numBorderSize.Value,
                LabelManagerObject.Container.Controls);

            // Set image segments file format
            System.Drawing.Imaging.ImageFormat imgFormat;
            if (cmbFileType.SelectedIndex == 0)
                imgFormat = System.Drawing.Imaging.ImageFormat.Png;
            else if (cmbFileType.SelectedIndex == 1)
                imgFormat = System.Drawing.Imaging.ImageFormat.Bmp;
            else if (cmbFileType.SelectedIndex == 2)
                imgFormat = System.Drawing.Imaging.ImageFormat.Jpeg;
            else
                imgFormat = System.Drawing.Imaging.ImageFormat.Tiff;

            // Save images
            try {
                for (int x = 0; x < segments.GetLength(0); x++)
                    for (int y = 0; y < segments.GetLength(1); y++) {
                        fileName = folderBrowserDialog.SelectedPath + "\\" + x.ToString() + "-" + y.ToString() + "." + imgFormat.ToString();

                        segments[x, y].Save(fileName, imgFormat);
                    }
            }
            catch (Exception exp){
                StaticMethods.ShowErrorMessage(MESSAGE_SAVE_ERROR, exp);
                return;
            }

            StaticMethods.ShowMessage(segments.Length.ToString() + " قطعه تصویر ذخیره شدند.");
        }

        private void numCutSize_ValueChanged(object sender, EventArgs e)
        {
            int w, h;

            w = (int)Math.Ceiling((decimal)ImageAnalyzerObject.Input.Width / numCutWidth.Value);
            h = (int)Math.Ceiling((decimal)ImageAnalyzerObject.Input.Height / numCutHeight.Value);

            lblSegments.Text = (w * h).ToString();
        }

        private void FormImageCutter_Load(object sender, EventArgs e)
        {
            cmbFileType.SelectedIndex = 0;
            txtPath.Text = folderBrowserDialog.SelectedPath;
            lnkFont.Text = StaticMethods.GetFontString(fontDialog.Font);
            numCutSize_ValueChanged(null, null);
        }

        private void btnSelectPath_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                txtPath.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void FormImageCutter_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void picFooterColor_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                picFooterColor.BackColor = colorDialog.Color;
                txtFooter.ForeColor = colorDialog.Color;
            }
        }

        private void lnkFont_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (fontDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                lnkFont.Text = StaticMethods.GetFontString(fontDialog.Font);
        }

        private void FormImageCutter_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible) {
                txtPath.Text = folderBrowserDialog.SelectedPath;
                numCutSize_ValueChanged(null, null);
            }
        }
    }
}
