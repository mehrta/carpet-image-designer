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
    public partial class FormOutputImageSettings : Form
    {
        public FormMain MainForm { set; get; }

        // Private fields 
        public FormOutputImageSettings()
        {
            InitializeComponent();
        }

        private void FormOutputImageSettings_Load(object sender, EventArgs e)
        {
            panelSubGrid.Enabled = chkShowSubGrid.Checked = MainForm.ImageAnalyzerObject.Grid.ShowSubGrid;
            panelMainGrid.Enabled = chkShowMainGrid.Checked = MainForm.ImageAnalyzerObject.Grid.ShowMainGrid;
            picMainGridColor1.BackColor = MainForm.ImageAnalyzerObject.Grid.MainGridColor1;
            picMainGridColor2.BackColor = MainForm.ImageAnalyzerObject.Grid.MainGridColor2;
            trackSubGridCorrective.Value = MainForm.ImageAnalyzerObject.Grid.SubGridCorrectiveValue;

            numOutputImageZoom.Value = MainForm.ImageAnalyzerObject.OutputImageZoom;
            numBorderSize.Value = MainForm.ImageAnalyzerObject.BorderSize;
        }

        private void chkShowMainGrid_CheckedChanged(object sender, EventArgs e)
        {
            panelMainGrid.Enabled = chkShowMainGrid.Checked;
        }

        private void lnkMainGridColor1_Click(object sender, EventArgs e)
        {
            colorDialog.Color = picMainGridColor1.BackColor;

            if (colorDialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                picMainGridColor1.BackColor = colorDialog.Color;
        }

        private void lnkMainGridColor2_Click(object sender, EventArgs e)
        {
            colorDialog.Color = picMainGridColor2.BackColor;

            if (colorDialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                picMainGridColor2.BackColor = colorDialog.Color;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            MainForm.ImageAnalyzerObject.Grid.ShowSubGrid = chkShowSubGrid.Checked;
            MainForm.ImageAnalyzerObject.Grid.ShowMainGrid = chkShowMainGrid.Checked;
            MainForm.ImageAnalyzerObject.Grid.MainGridColor1 = picMainGridColor1.BackColor;
            MainForm.ImageAnalyzerObject.Grid.MainGridColor2 = picMainGridColor2.BackColor;
            MainForm.ImageAnalyzerObject.Grid.SubGridCorrectiveValue = trackSubGridCorrective.Value;
            MainForm.ImageAnalyzerObject.OutputImageZoom = (int)numOutputImageZoom.Value;
            MainForm.ImageAnalyzerObject.BorderSize = (int)numBorderSize.Value;

            MainForm.UpdateOutputImageMenuUI();

            if (MainForm.picOutput.Image != null) {
                MainForm.ImageAnalyzerObject.RedrawOutput();
                MainForm.picOutput.Image = MainForm.ImageAnalyzerObject.Output;
            }
            this.Close();
        }

        private void trackSubGridCorrective_ValueChanged(object sender, EventArgs e)
        {
            lblSubGridCorrective.Text = trackSubGridCorrective.Value.ToString();
        }

        private void chkShowSubGrid_CheckedChanged(object sender, EventArgs e)
        {
            panelSubGrid.Enabled = chkShowSubGrid.Checked;
        }

        private void btnSetDefaults_Click(object sender, EventArgs e)
        {
            chkShowMainGrid.Checked = true;
            picMainGridColor1.BackColor = Color.Black;
            picMainGridColor2.BackColor = Color.Blue;
            chkShowSubGrid.Checked = true;
            trackSubGridCorrective.Value = -25;
            numOutputImageZoom.Value = 8;
            numBorderSize.Value = 60;
        }

    }
}
