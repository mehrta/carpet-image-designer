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
    public partial class FormLabelSettings : Form
    {
        public FormMain MainForm { set; get; }

        public FormLabelSettings()
        {
            InitializeComponent();
        }

        private void FormLabelSettings_Load(object sender, EventArgs e)
        {
            fontDialog.Font = MainForm.LabelManagerObject.DefaultLabelFont;
            lnkFont.Text = StaticMethods.GetFontString(MainForm.LabelManagerObject.DefaultLabelFont);
            picColor.BackColor = MainForm.LabelManagerObject.DefaultLabelForeColor;
            picDarkColor.BackColor = MainForm.LabelManagerObject.DefaultLabelForeColorDark;

            picDarkBackgroundColorUpperBound.BackColor = MainForm.LabelManagerObject.DarkBackgroundColorUpperBound;
            trackR.Value = MainForm.LabelManagerObject.DarkBackgroundColorUpperBound.R;
            trackG.Value = MainForm.LabelManagerObject.DarkBackgroundColorUpperBound.G;
            trackB.Value = MainForm.LabelManagerObject.DarkBackgroundColorUpperBound.B;

            radInvertLabelColors.Checked = MainForm.LabelManagerObject.InvertLabelColorOfDarkRegions;
            radDarkBackgroundColorUpperBound.Checked = !radInvertLabelColors.Checked;
        }

        private void lnkFont_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            fontDialog.Font = MainForm.LabelManagerObject.DefaultLabelFont;
            if (fontDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                lnkFont.Text = StaticMethods.GetFontString(fontDialog.Font);
        }

        private void picColor_Click(object sender, EventArgs e)
        {
            colorDialog.Color = picColor.BackColor;
            if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                picColor.BackColor = colorDialog.Color;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            MainForm.LabelManagerObject.DefaultLabelFont = fontDialog.Font;
            MainForm.LabelManagerObject.DefaultLabelForeColor = picColor.BackColor;
            MainForm.LabelManagerObject.DefaultLabelForeColorDark = picDarkColor.BackColor;
            MainForm.LabelManagerObject.DarkBackgroundColorUpperBound = picDarkBackgroundColorUpperBound.BackColor;

            if (radInvertLabelColors.Checked)
                MainForm.LabelManagerObject.InvertLabelColorOfDarkRegions = true;
            else
                MainForm.LabelManagerObject.InvertLabelColorOfDarkRegions = false;

            this.Close();
        }

        private void picDarkColor_Click(object sender, EventArgs e)
        {
            colorDialog.Color = picDarkColor.BackColor;
            if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                picDarkColor.BackColor = colorDialog.Color;
            }
        }

        private void TrackRGB_ValueChanged(object sender, EventArgs e)
        {
            lblR.Text = trackR.Value.ToString();
            lblG.Text = trackG.Value.ToString();
            lblB.Text = trackB.Value.ToString();

            picDarkBackgroundColorUpperBound.BackColor = Color.FromArgb(trackR.Value, trackG.Value, trackB.Value);
        }

        private void radDarkBackgroundColorUpperBound_CheckedChanged(object sender, EventArgs e)
        {
            if (radDarkBackgroundColorUpperBound.Checked)
                picDarkColor.Enabled = true;
            else
                picDarkColor.Enabled = false;
        }
    }
}
