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
    public partial class FormAutomaticLabeling : Form
    {
        public ImageAnalyzer ImageAnalyzerObject { set; get; }
        public LabelManager LabelManagerObject { set; get; }
        private List<Label> _addedLabels;

        public FormAutomaticLabeling()
        {
            InitializeComponent();

            //
            _addedLabels = new List<Label>(100);
        }

        private void btnAutomaticLabeling_Click(object sender, EventArgs e)
        {
            const string MESSAGE_NO_OUTPUT = "ابتدا باید تصویر ورودی پردازش شود.";

            // Check Output Image
            if (ImageAnalyzerObject.Output == null) {
                StaticMethods.ShowMessage(MESSAGE_NO_OUTPUT);
                return;
            }

            //
            if (chkFirstDeleteAllLabels.Checked) {
                LabelManagerObject.DeleteAllLabels();
            }
            else {
                LabelManagerObject.DeleteLabels(_addedLabels);
            }

            //LabelManagerObject.Container.Visible = false;
            _addedLabels.Clear();
            AddLabelsAutomatically();
            //LabelManagerObject.Container.Visible = true;

            // Update UI
            System.Media.SystemSounds.Asterisk.Play();
            lblStatus.Text = _addedLabels.Count.ToString() + " بر چسب اضافه شد.";

            if (_addedLabels.Count > 0)
                btnDeleteAddedLabels.Enabled = true;
        }

        private void AddLabelsAutomatically()
        {
            Label l;
            Point rHotSpot;

            LabelManagerObject.Container.SuspendLayout();
            foreach (ImageAnalyzer.Region r in ImageAnalyzerObject.Regions) {

                // Check the size of region
                float regionPixelsArea;
                float regionBoundariesArea;
                regionPixelsArea = r.Pixels.Count;
                regionBoundariesArea = r.Boundaries.Width * r.Boundaries.Height;
                if ((r.Boundaries.Width < numRegionMinHeight.Value) ||
                    (r.Boundaries.Height < numRegionMinHeight.Value) ||
                    (((regionPixelsArea / regionBoundariesArea) * 100) < (float)numRegionMinArea.Value))
                    continue;


                // Add a label on the current region
                l = new Label();
                Size lSize;

                rHotSpot = CalculateHotSpot(r);
                l.Text = r.ColorID.ToString();
                l.AutoSize = true;
                l.Font = LabelManagerObject.DefaultLabelFont;
                lSize = TextRenderer.MeasureText(l.Text, LabelManagerObject.DefaultLabelFont);
                l.Left =
                    (rHotSpot.X * ImageAnalyzerObject.OutputImageZoom) + (ImageAnalyzerObject.OutputImageZoom / 2) +
                    ImageAnalyzerObject.BorderSize - (lSize.Width / 2);
                l.Top =
                    (rHotSpot.Y * ImageAnalyzerObject.OutputImageZoom) + (ImageAnalyzerObject.OutputImageZoom / 2) +
                    ImageAnalyzerObject.BorderSize - (lSize.Height / 2);
                l.ForeColor =
                    LabelManagerObject.CalculateLabelForeColor(ImageAnalyzerObject.Pixels[rHotSpot.X, rHotSpot.Y].Color);

                _addedLabels.Add(l);
            }

            // Add labels to the container (picOutput)
            LabelManagerObject.AddLabels(_addedLabels.ToArray());
            LabelManagerObject.Container.ResumeLayout();

        }

        private void btnDeleteAddedLabels_Click(object sender, EventArgs e)
        {
            LabelManagerObject.DeleteLabels(_addedLabels);
            _addedLabels.Clear();
            lblStatus.Text = "برچسب های اضافه شده حذف شدند.";
            btnDeleteAddedLabels.Enabled = false;
        }

        private void numRegionMin_ValueChanged(object sender, EventArgs e)
        {
            lblMinBoundariesArea.Text = ((int)numRegionMinWidth.Value * (int)numRegionMinHeight.Value).ToString();
        }

        private Point CalculateHotSpot(ImageAnalyzer.Region r)
        {

            Point geoAverage = new Point(); // Geometric Average
            Point hotSpot;
            Point left, right, top, bottom;
            int turn = 0;

            // Check geometric average
            geoAverage.X = r.GeometricSum.X / r.Pixels.Count;
            geoAverage.Y = r.GeometricSum.Y / r.Pixels.Count;
            if (ImageAnalyzerObject.Pixels[geoAverage.X, geoAverage.Y].Region.ColorID == r.ColorID)
                return geoAverage;

            int s = 0;
            foreach (ImageAnalyzer.CPoint p in r.Pixels) {
                s += p.X;
            }

            // Check left, top, right and bottom point of geoAverage
            bool notFound = true;
            left = top = right = bottom = hotSpot = geoAverage;

            while (notFound) {
                switch (turn) {
                    case 0: // Left
                        if (left.X > r.Boundaries.X) {
                            left.X -= 1;
                            if (ImageAnalyzerObject.Pixels[left.X, left.Y].Region.ColorID == r.ColorID) {
                                hotSpot = left;
                                notFound = false; // HotSpot found
                            }
                        }
                        break;

                    case 1: // Top
                        if (top.Y > r.Boundaries.Y) {
                            top.Y -= 1;
                            if (ImageAnalyzerObject.Pixels[top.X, top.Y].Region.ColorID == r.ColorID) {
                                hotSpot = top;
                                notFound = false; // HotSpot found
                            }
                        }
                        break;

                    case 2: // Right
                        if (right.X < r.Boundaries.Right - 1) {
                            right.X += 1;
                            if (ImageAnalyzerObject.Pixels[right.X, right.Y].Region.ColorID == r.ColorID) {
                                hotSpot = right;
                                notFound = false; // HotSpot found
                            }
                        }
                        break;

                    case 3: // Bottom
                        if (bottom.Y < r.Boundaries.Bottom - 1) {
                            bottom.Y += 1;
                            if (ImageAnalyzerObject.Pixels[bottom.X, bottom.Y].Region.ColorID == r.ColorID) {
                                hotSpot = bottom;
                                notFound = false; // HotSpot found
                            }
                        }
                        break;
                }

                // Change direction
                turn++;
                if (turn == 4)
                    turn = 0;
            }

            return hotSpot;
        }

        private void FormAutomaticLabeling_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
    }
}
