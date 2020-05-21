using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace CrossPlanGenerator
{
    public class LabelManager
    {
        // Types
        public class LabelTag
        {
            public int Left { set; get; }
            public int Top { set; get; }
            public float FontSize { set; get; }
            public bool Selected { set; get; }
        }

        // Private fields
        private Point _dragStartLocation;
        private bool _dragging;
        private bool _lockLabels;
        private bool _showLabels;
        private bool _labelMoved;
        private float _zoomScale;

        // Events
        public event EventHandler SelectedLabelsChanged; // Number Of Selected Labels Changed
        public event EventHandler LabelPropertiesChanged;

        // Properties
        public ArrayList SelectedLabels { set; get; }
        public Control Container { set; get; }
        public bool CtrlKeyIsPressed { set; get; }
        public bool ShiftKeyIsPressed { set; get; }
        public bool InvertLabelColorOfDarkRegions { set; get; }
        public Color DefaultLabelForeColor { set; get; }
        public Color DefaultLabelForeColorDark { set; get; }
        public Color DarkBackgroundColorUpperBound { set; get; }
        public Font DefaultLabelFont { set; get; }
        public bool LockLabels
        {
            set
            {
                if (value == _lockLabels)
                    return;

                _lockLabels = value;

                if (value) {
                    foreach (Label l in Container.Controls)
                        l.Cursor = Cursors.Default;
                }
                else {
                    foreach (Label l in Container.Controls)
                        l.Cursor = Cursors.SizeAll;
                }
            }

            get
            {
                return _lockLabels;
            }
        }
        public bool ShowLabels
        {
            set
            {
                _showLabels = value;

                DeselectAllLabels();
                foreach (Label l in Container.Controls) {
                    l.Visible = value;
                }
            }

            get
            {
                return _showLabels;
            }
        }
        public float ZoomScale
        {
            set
            {
                _zoomScale = value;

                LabelTag tag;

                foreach (Label l in Container.Controls) {
                    tag = (LabelTag)l.Tag;
                    //l.SuspendLayout();
                    Font newFont = new Font(l.Font.FontFamily.Name, tag.FontSize * value, l.Font.Style);
                    l.Font = newFont;
                    l.Left = (int)(tag.Left * value);
                    l.Top = (int)(tag.Top * value);
                    //l.Invalidate();
                    //l.ResumeLayout();
                }
            }

            get
            {
                return _zoomScale;
            }
        }

        // Methods
        public LabelManager()
        {
            DefaultLabelForeColor = Color.Black;
            DefaultLabelForeColorDark = Color.WhiteSmoke;
            DarkBackgroundColorUpperBound = Color.FromArgb(50, 50, 50);
            DefaultLabelFont = new Font("B Sara", 15, FontStyle.Bold);
            this.SelectedLabels = new ArrayList(300);
            this.InvertLabelColorOfDarkRegions = true;
            _showLabels = true;
            _zoomScale = 1f;
        }

        public Label AddLabel(int x, int y, string text, Color labelBackgroundColor)
        {
            Label newLabel = new Label();
            LabelTag tag = new LabelTag();

            // Deselect All Labels
            DeselectAllLabels();

            // Add this label to the container
            this.Container.Controls.Add(newLabel);

            // Set Properties
            newLabel.Visible = _showLabels;
            newLabel.Font = this.DefaultLabelFont;
            newLabel.ForeColor = CalculateLabelForeColor(labelBackgroundColor);
            newLabel.AutoSize = true;
            newLabel.Text = text;
            newLabel.Left = x - (newLabel.Width / 2);
            newLabel.Top = y - (newLabel.Height / 2);
            newLabel.Padding = new Padding(0);
            newLabel.Cursor = Cursors.SizeAll;

            // Set Tag of the label
            tag.Left = (int)(newLabel.Left / _zoomScale);
            tag.Top = (int)(newLabel.Top / _zoomScale);
            tag.FontSize = newLabel.Font.SizeInPoints / _zoomScale;
            tag.Selected = false; // false: Unselected , true: Selected

            newLabel.Tag = tag;

            // Set Event Handlers
            newLabel.MouseDown += new MouseEventHandler(Label_MouseDown);
            newLabel.MouseUp += new MouseEventHandler(Label_MouseUp);
            newLabel.MouseMove += new MouseEventHandler(Label_MouseMove);
            newLabel.DoubleClick += new EventHandler(Label_DoubleClick);

            // Select The Label
            SelectLabel(newLabel);

            // Fire event
            LabelPropertiesChanged(null, null);

            return newLabel;
        }

        public void AddLabels(Label[] labels)
        {
            foreach (Label l in labels) {
                // Set Tag of the label
                LabelTag tag = new LabelTag();
                tag.Left = l.Left;
                tag.Top = l.Top;
                tag.FontSize = l.Font.SizeInPoints;
                tag.Selected = false; // false: Unselected , true: Selected

                // Set Properties
                l.Tag = tag;
                l.Cursor = Cursors.SizeAll;
                l.Visible = _showLabels;
                l.BackColor = Color.Transparent;

                // Set Event Handlers
                l.MouseDown += new MouseEventHandler(Label_MouseDown);
                l.MouseUp += new MouseEventHandler(Label_MouseUp);
                l.MouseMove += new MouseEventHandler(Label_MouseMove);
                l.DoubleClick += new EventHandler(Label_DoubleClick);
            }

            // Add labels to the container
            this.Container.Controls.AddRange(labels);
        }

        public Color CalculateLabelForeColor(Color labelBackgroundColor)
        {
            if ((labelBackgroundColor.R <= DarkBackgroundColorUpperBound.R &&
                labelBackgroundColor.G <= DarkBackgroundColorUpperBound.G &&
                labelBackgroundColor.B <= DarkBackgroundColorUpperBound.B)) {

                // Label is placed on a dark region
                if (this.InvertLabelColorOfDarkRegions) {
                    return StaticMethods.ColorFromHsv(
                        ((int)labelBackgroundColor.GetHue() + 180) % 360,
                        1.0f,
                        1.0f);
                }
                else {
                    return this.DefaultLabelForeColorDark;
                }
            }
            else {
                // Label is placed on a bright (not dark) region
                return this.DefaultLabelForeColor;
            }
        }

        public void DeleteAllLabels(bool fireLabelPropertiesChangedEvent = true)
        {
            if (Container.Controls.Count > 0) {
                Container.Controls.Clear();
                SelectedLabels.Clear();

                // Fire event
                if (fireLabelPropertiesChangedEvent)
                    LabelPropertiesChanged(null, null);
            }
        }

        public void SelectAllLabels()
        {
            SelectedLabels.Clear();

            // Performance boost
            Container.SuspendLayout();
            if (Container.Controls.Count > 200)
                Container.Visible = false;


            foreach (Control c in Container.Controls) {
                (c.Tag as LabelTag).Selected = true;
                SelectedLabels.Add(c);
                (c as Label).BorderStyle = BorderStyle.FixedSingle;
            }

            Container.SuspendLayout();
            Container.Visible = true;

            // Fire event "SelectedLabelsChanged"
            SelectedLabelsChanged(null, null);
        }

        public void SelectLabel(Label l)
        {
            (l.Tag as LabelTag).Selected = true;
            SelectedLabels.Add(l);
            l.BorderStyle = BorderStyle.FixedSingle;

            // Fire event "SelectedLabelsChanged"
            SelectedLabelsChanged(null, null);
        }

        public void DeselectLabel(Label l)
        {
            (l.Tag as LabelTag).Selected = false;
            SelectedLabels.Remove(l);
            l.BorderStyle = BorderStyle.None;

            // Fire event "SelectedLabelsChanged"
            SelectedLabelsChanged(null, null);
        }

        public void DeleteLabels(List<Label> labels)
        {
            this.Container.SuspendLayout();
            this.Container.Visible = false;
            foreach (Label l in labels) {
                Container.Controls.Remove(l);
            }
            this.Container.Visible = true;

            DeselectAllLabels();

            this.Container.ResumeLayout();
        }

        public void DeleteSelectedLabels()
        {
            if (SelectedLabels.Count > 0) {

                foreach (Label l in SelectedLabels)
                    Container.Controls.Remove(l);
                SelectedLabels.Clear();

                // Fire event
                SelectedLabelsChanged(null, null);

                // Fire event
                LabelPropertiesChanged(null, null);
            }
        }

        public void DeselectAllLabels()
        {
            // Performance boost
            if (SelectedLabels.Count > 200)
                Container.Visible = false;

            foreach (Label l in SelectedLabels) {
                (l.Tag as LabelTag).Selected = false;
                l.BorderStyle = BorderStyle.None;
            }

            Container.Visible = true;
            SelectedLabels.Clear();
            _dragging = false;

            // Fire event "SelectedLabelsChanged"
            SelectedLabelsChanged(null, null);
        }

        public void MoveSelectedUp()
        {
            if (SelectedLabels.Count > 0) {
                foreach (Label l in SelectedLabels)
                    l.Top--;
                LabelPropertiesChanged(null, null);
            }
        }

        public void MoveSelectedDown()
        {
            if (SelectedLabels.Count > 0) {
                foreach (Label l in SelectedLabels)
                    l.Top++;
                LabelPropertiesChanged(null, null);
            }
        }

        public void MoveSelectedRight()
        {
            if (SelectedLabels.Count > 0) {
                foreach (Label l in SelectedLabels)
                    l.Left++;
                LabelPropertiesChanged(null, null);
            }
        }

        public void MoveSelectedLeft()
        {
            if (SelectedLabels.Count > 0) {
                foreach (Label l in SelectedLabels)
                    l.Left--;
                LabelPropertiesChanged(null, null);
            }
        }

        public void MakeSelectedLabelsLarger()
        {
            if (SelectedLabels.Count > 0) {
                foreach (Label l in SelectedLabels)
                    if (l.Font.SizeInPoints < 150) {
                        Font newFont = new Font(l.Font.FontFamily, l.Font.SizeInPoints + 2, l.Font.Style);
                        l.Font = newFont;
                    }

                LabelPropertiesChanged(null, null);
            }
        }

        public void MakeSelectedLabelsSmaller()
        {
            if (SelectedLabels.Count > 0) {
                foreach (Label l in SelectedLabels)
                    if (l.Font.SizeInPoints >= 8) {
                        Font newFont = new Font(l.Font.FontFamily, l.Font.SizeInPoints - 2, l.Font.Style);
                        l.Font = newFont;
                    }

                LabelPropertiesChanged(null, null);
            }
        }

        public void EditSelectedLabels()
        {
            FormEditLabel frmEdit = new FormEditLabel();

            frmEdit.Tag = SelectedLabels.Count;
            if (SelectedLabels.Count == 0)
                return;
            else if (SelectedLabels.Count == 1) {
                Label l = SelectedLabels[0] as Label;
                frmEdit.Tag = "1";
                frmEdit.LabelText = l.Text;

                Font labelFont = new Font(l.Font.FontFamily.Name, (l.Tag as LabelTag).FontSize, l.Font.Style);
                frmEdit.LabelFont = labelFont;
                frmEdit.LabelColor = l.ForeColor;

                if (frmEdit.ShowDialog() == DialogResult.OK) {
                    l.Text = frmEdit.LabelText;
                    l.Font = new Font(frmEdit.LabelFont.FontFamily.Name, frmEdit.LabelFont.SizeInPoints * _zoomScale, frmEdit.LabelFont.Style);
                    l.ForeColor = (Color)frmEdit.LabelColor;
                    (l.Tag as LabelTag).FontSize = frmEdit.LabelFont.SizeInPoints;

                    LabelPropertiesChanged(null, null);
                }
            }
            else {
                bool textAreTheSame = true;
                bool fontAreTheSame = true;
                bool colorAreTheSame = true;

                // Check properties of the labels for equality
                for (int i = 1; i < SelectedLabels.Count; i++) {
                    if ((SelectedLabels[i] as Label).Text != (SelectedLabels[i - 1] as Label).Text)
                        textAreTheSame = false;

                    if (((SelectedLabels[i] as Label).Font.FontFamily.Name != (SelectedLabels[i - 1] as Label).Font.FontFamily.Name) ||
                        ((SelectedLabels[i] as Label).Font.Style != (SelectedLabels[i - 1] as Label).Font.Style) ||
                        (((SelectedLabels[i] as Label).Tag as LabelTag).FontSize != ((SelectedLabels[i - 1] as Label).Tag as LabelTag).FontSize)
                        )
                        fontAreTheSame = false;

                    if ((SelectedLabels[i] as Label).ForeColor != (SelectedLabels[i - 1] as Label).ForeColor)
                        colorAreTheSame = false;
                }

                // Set Label
                if (textAreTheSame)
                    frmEdit.LabelText = (SelectedLabels[0] as Label).Text;

                // Set Font
                if (fontAreTheSame) {
                    Label l = SelectedLabels[0] as Label;
                    frmEdit.LabelFont = new Font(l.Font.FontFamily.Name, (l.Tag as LabelTag).FontSize, l.Font.Style);
                }

                // Set Color
                if (colorAreTheSame)
                    frmEdit.LabelColor = (SelectedLabels[0] as Label).ForeColor;

                // Show Dialog
                if (frmEdit.ShowDialog() == DialogResult.OK) {
                    if (frmEdit.LabelText != "")
                        foreach (Label l in SelectedLabels)
                            l.Text = frmEdit.LabelText;

                    if (frmEdit.LabelFont != null)
                        foreach (Label l in SelectedLabels) {
                            l.Font = new Font(frmEdit.LabelFont.FontFamily.Name, frmEdit.LabelFont.SizeInPoints * _zoomScale, frmEdit.LabelFont.Style);
                            (l.Tag as LabelTag).FontSize = frmEdit.LabelFont.SizeInPoints;
                        }

                    if (frmEdit.LabelColor != null)
                        foreach (Label l in SelectedLabels)
                            l.ForeColor = (Color)frmEdit.LabelColor;

                    LabelPropertiesChanged(null, null);
                }
            }
        }

        public void SelectAllLabelsWithSameColor(Color c)
        {
            DeselectAllLabels();

            foreach (Label l in Container.Controls)
                if (l.ForeColor == c)
                    SelectLabel(l);
        }

        void Label_MouseDown(object sender, MouseEventArgs e)
        {
            Label l = (Label)sender;

            if (!this.LockLabels)
                if ((l.Tag as LabelTag).Selected == false) {
                    if (!CtrlKeyIsPressed)
                        // First, We should deselect all labels
                        DeselectAllLabels();
                    SelectLabel(l);
                }
                else {
                    if (CtrlKeyIsPressed) {
                        // Deselect Clicked Label
                        DeselectLabel(l);
                    }
                    else {
                        if (!this.LockLabels) {
                            _dragging = true;
                            _dragStartLocation = e.Location;
                        }
                    }
                }
        }

        void Label_MouseUp(object sender, MouseEventArgs e)
        {
            if (_labelMoved)
                LabelPropertiesChanged(null, null);

            _dragging = false;
            _labelMoved = false;
        }

        void Label_MouseMove(object sender, MouseEventArgs e)
        {
            if (_dragging) {
                int MouseDeltaX = e.X - _dragStartLocation.X;

                // Move Selected Labels
                foreach (Label l in SelectedLabels) {
                    l.Left = l.Left + MouseDeltaX;
                    l.Top = l.Top + (e.Y - _dragStartLocation.Y);

                    (l.Tag as LabelTag).Left = (int)(l.Left / _zoomScale);
                    (l.Tag as LabelTag).Top = (int)(l.Top / _zoomScale);
                }

                _labelMoved = true;
            }
        }

        void Label_DoubleClick(object sender, EventArgs e)
        {
            if (ShiftKeyIsPressed) {
                // Select All labels with same color
                SelectAllLabelsWithSameColor((sender as Label).ForeColor);
            }
            else {
                EditSelectedLabels();
            }
        }
    }
}
