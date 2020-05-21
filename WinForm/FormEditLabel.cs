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
    public partial class FormEditLabel : Form
    {
        public Color? LabelColor { set; get; }
        public Font LabelFont { set; get; }
        public string LabelText { set; get; }

        public FormEditLabel()
        {
            InitializeComponent();
        }

        private void FormEditLabel_Load(object sender, EventArgs e)
        {
            txtText.Text = LabelText;

            if (this.Tag.ToString() == "1") { // This.Tag: Number of selected labels
                lnkFont.Text = StaticMethods.GetFontString(LabelFont);
                picColor.BackgroundImage = null;
                picColor.BackColor = (Color)LabelColor;
            }
            else {

                if (LabelFont != null) {
                    lnkFont.Text = StaticMethods.GetFontString(LabelFont);
                }
                else {
                    lnkFont.Text = "انتخاب فونت";
                }

                if (LabelColor != null) {
                    picColor.BackColor = (Color)LabelColor;
                    picColor.BackgroundImage = null;
                }
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if ((this.Tag.ToString() == "1") && (txtText.Text.Trim() == "")) {
                FormMessageBox msg = new FormMessageBox();
                msg.MessageType = FormMessageBox.MessageTypeEnum.Alert;
                msg.Message = "لطفا برای برچسب انتخاب شده یک متن وارد کنید.";
                msg.ShowDialog();
                txtText.Focus();
                return;
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            LabelText = txtText.Text;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void picColor_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                picColor.BackColor = colorDialog.Color;
                picColor.BackgroundImage = null;
                LabelColor = colorDialog.Color;
            }
        }

        private void lnkFont_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            fontDialog.Font = LabelFont;
            if (fontDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                LabelFont = fontDialog.Font;
                lnkFont.Text = StaticMethods.GetFontString(fontDialog.Font);
            }
        }
    }
}
