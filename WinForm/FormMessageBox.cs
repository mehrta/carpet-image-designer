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
    public partial class FormMessageBox : Form
    {
        public enum MessageTypeEnum { Alert, Error, YesNo };

        public MessageTypeEnum MessageType { set; get; }

        public string Message { set; get; }

        public FormMessageBox()
        {
            InitializeComponent();

            this.MessageType = MessageTypeEnum.Alert;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormMessageBox_Load(object sender, EventArgs e)
        {
            txtMessage.Text = this.Message;

            // Play Sound
            if (MessageType == MessageTypeEnum.Alert) {
                picImage.Image = imageList.Images[0];
                System.Media.SystemSounds.Asterisk.Play();
            }
            else if (MessageType == MessageTypeEnum.Error) {
                picImage.Image = imageList.Images[1];
                System.Media.SystemSounds.Hand.Play();

                btnYes.Visible = btnNo.Visible = false;
            }
            else { // MessageType == YesNo
                btnYes.Visible = btnNo.Visible = true;
                btnOk.Visible = false;

                btnNo.Left -= 45;
                btnYes.Left += 45;

                picImage.Image = imageList.Images[0];
                System.Media.SystemSounds.Asterisk.Play();
            }
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.Close();
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.No;
            this.Close();
        }
    }
}
