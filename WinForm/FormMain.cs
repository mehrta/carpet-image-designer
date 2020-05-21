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
    public partial class FormMain : Form
    {
        public enum Tool { SelectLabel, Pan, AddLabel }

        public struct MouseInfo
        {
            public Point PanStartLocation { set; get; }
            public Point OriginalPixelLocation { set; get; }
            public int ColorID { set; get; }
        }

        // Properties
        public string[] ProgramArguments { set; get; }
        public ImageAnalyzer ImageAnalyzerObject { set; get; }
        public LabelManager LabelManagerObject { set; get; }
        public Document DocumentObject { set; get; }

        // Private fields
        private StringBuilder _tmpStrBuilder; // Temporary String Builder
        private Tool _selectedTool;
        private MouseInfo _mouseInfo;
        private float _zoomScale;  // Output image (and labels) zoom scale
        private FormAutomaticLabeling _frmAutoLabeling;
        private FormImageCutter _frmImageCutter;

        public FormMain()
        {
            InitializeComponent();

            // Initialize
            ImageAnalyzerObject = new ImageAnalyzer();
            LabelManagerObject = new LabelManager();
            DocumentObject = new Document();

            LabelManagerObject.Container = picOutput;
            LabelManagerObject.SelectedLabelsChanged += new EventHandler(LabelManagerObject_SelectedLabelsChanged);
            LabelManagerObject.LabelPropertiesChanged += new EventHandler(LabelManagerObject_LabelPropertiesChanged);

            DocumentObject.DocumentChanged += new EventHandler(DocumentObject_DocumentChanged);
            DocumentObject.DocumentSaved += new EventHandler(DocumentObject_DocumentSaved);

            _tmpStrBuilder = new StringBuilder(120);
            _selectedTool = Tool.Pan;
            _zoomScale = 1f;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            // Check the lock of program
            //if (!StaticMethods.SoftwareShouldRun())
            //    Application.Exit();

            // Load settings
            SettingsManager sm = new SettingsManager(ImageAnalyzerObject, LabelManagerObject);
            sm.LoadSettings();

            // Initialize Controls
            menu_OutputImage_ShowMainGrid.Checked = ImageAnalyzerObject.Grid.ShowMainGrid;
            menu_OutputImage_ShowSubGrid.Checked = ImageAnalyzerObject.Grid.ShowSubGrid;
            menu_OutputImage_HighlightRegion.Checked = ImageAnalyzerObject.HighlightRegions;

            lblStatusLeft.Text = "";
            toolStripCmbZoom.SelectedIndex = 11;

            // Check main() arguments
            if (this.ProgramArguments.Length > 0) {
                LoadFile(this.ProgramArguments[0]);
                openFileDialog.FileName = this.ProgramArguments[0];
            }
        }

        private void LoadFile(string fileName)
        {
            const string MESSAGE_IMAGE_LOAD_FAILED = "هنگام بارگذاری فایل خطایی رخ داده است، لطفا فایل انتخاب شده را بررسی کنید.";
            const string MESSAGE_FILE_LOADED = "تصویر بارگذاری شد.";

            Bitmap img = null;
            bool imageLoaded = true;

            // Load file
            try {
                DocumentObject.LoadFromFile(fileName);
                img = (Bitmap)DocumentObject.InputImage;
            }
            catch (Exception exp) {
                imageLoaded = false;
                UpdateUI(false, false);
                StaticMethods.ShowErrorMessage(MESSAGE_IMAGE_LOAD_FAILED, exp);
            }

            if (imageLoaded) {
                ImageAnalyzerObject.Rest();
                ImageAnalyzerObject.Input = new Bitmap(img);
                ImageAnalyzerObject.OutputImageZoom = 8;
                LabelManagerObject.DeleteAllLabels(false);

                picInput.Image = img;
                picOutput.Image = null;

                // Update UI
                UpdateUI(true, false);
                tabControlMain.SelectedIndex = 0;
                lblStatusRight.Text = MESSAGE_FILE_LOADED;

                // if A ".cpg" file is opened, we should add labels and process it automatically
                if (DocumentObject.IsCpgDocument) {
                    ImageAnalyzerObject.OutputImageZoom = DocumentObject.OutputImageZoom;

                    // Start Analyzing
                    menu_Program_StartAnalyze_Click(null, null);

                    // Add Labels
                    LabelManagerObject.AddLabels(DocumentObject.Labels.ToArray());
                }

                //
                UpdateOutputImageMenuUI();
            }

        }

        void LabelManagerObject_LabelPropertiesChanged(object sender, EventArgs e)
        {
            menu_Program_SaveDocument.Enabled = true;
            DocumentObject.IsDocumentChanged = true;
        }

        void DocumentObject_DocumentSaved(object sender, EventArgs e)
        {
            menu_Program_SaveDocument.Enabled = false;
            lblStatusRight.Text = "پرونده ذخیره شد.";
        }

        void DocumentObject_DocumentChanged(object sender, EventArgs e)
        {
            menu_Program_SaveDocument.Enabled = true;
        }

        private void LabelManagerObject_SelectedLabelsChanged(object sender, EventArgs e)
        {
            if (LabelManagerObject.SelectedLabels.Count > 0) {
                lblStatusRight.Text = LabelManagerObject.SelectedLabels.Count.ToString() +
                    " برچسب انتخاب شده است.";
                menu_Labeling_DeleteSelectedLabels.Enabled = true;
                menu_Labeling_LargerFont.Enabled = true;
                menu_Labeling_SmallerFont.Enabled = true;
                menu_Labeling_EditSelectedLabels.Enabled = true;
            }
            else {
                lblStatusRight.Text = "آماده";
                menu_Labeling_DeleteSelectedLabels.Enabled = false;
                menu_Labeling_LargerFont.Enabled = false;
                menu_Labeling_SmallerFont.Enabled = false;
                menu_Labeling_EditSelectedLabels.Enabled = false;
            }
        }

        public void UpdateOutputImageMenuUI()
        {
            menu_OutputImage_ShowSubGrid.Checked = ImageAnalyzerObject.Grid.ShowSubGrid;
            menu_OutputImage_ShowMainGrid.Checked = ImageAnalyzerObject.Grid.ShowMainGrid;

            // 'Check' clicked menu item
            menu_OutputImage_Size_6.Checked =
            menu_OutputImage_Size_7.Checked =
            menu_OutputImage_Size_8.Checked =
            menu_OutputImage_Size_9.Checked =
            menu_OutputImage_Size_10.Checked =
            menu_OutputImage_Size_15.Checked =
            menu_OutputImage_Size_20.Checked = false;

            switch (ImageAnalyzerObject.OutputImageZoom) {
                case 6:
                    menu_OutputImage_Size_6.Checked = true;
                    break;
                case 7:
                    menu_OutputImage_Size_7.Checked = true;
                    break;
                case 8:
                    menu_OutputImage_Size_8.Checked = true;
                    break;
                case 9:
                    menu_OutputImage_Size_9.Checked = true;
                    break;
                case 10:
                    menu_OutputImage_Size_10.Checked = true;
                    break;
                case 15:
                    menu_OutputImage_Size_15.Checked = true;
                    break;
                case 20:
                    menu_OutputImage_Size_20.Checked = true;
                    break;
            }

            lblOutputImageZoom.Text = ImageAnalyzerObject.OutputImageZoom.ToString() + " برابر";
        }

        private void UpdateUI(bool inputImageLoaded, bool outputImageLoaded)
        {
            const string PROGRAM_TITLE = "تولید نقشه خانه بندی";

            if (inputImageLoaded) {
                menu_Program_StartAnalyze.Enabled = true;
                menu_Program_SaveDocument.Enabled = true;
                picInput.Dock = DockStyle.None;
                picInput.SizeMode = PictureBoxSizeMode.AutoSize;
                lblInputImageWidth.Text = picInput.Image.Width.ToString() + " پیکسل";
                lblInputImageHeight.Text = picInput.Image.Height.ToString() + " پیکسل";
                txtInputImageFileName.Text = openFileDialog.FileName;
                if (DocumentObject.FileName == "")
                    this.Text = System.IO.Path.GetFileName(openFileDialog.FileName) + " - " + PROGRAM_TITLE;
                else
                    this.Text = System.IO.Path.GetFileName(DocumentObject.FileName) + " - " + PROGRAM_TITLE;
            }
            else {
                menu_Program_StartAnalyze.Enabled = false;
                menu_Program_SaveDocument.Enabled = false;
                picInput.Visible = false;
                lblInputImageWidth.Text = "-";
                lblInputImageHeight.Text = "-";
                lblNumberOfRegions.Text = "-";
            }

            if (outputImageLoaded) {

                menu_Program_SaveOutput.Enabled = true;
                menu_Labeling_SelectAllLabels.Enabled = true;
                menu_OutputImage_CutTool.Enabled = true;
                menu_Labeling_AutomaticLabeling.Enabled = true;
                toolStripPan.Enabled = true;
                picOutput.Enabled = true;
                picOutput.Dock = DockStyle.None;
                picOutput.SizeMode = PictureBoxSizeMode.AutoSize;
                toolStripMoveLable_Click(null, null);

                // Update Tab "Properties"
                lblOutputImageZoom.Text = ImageAnalyzerObject.OutputImageZoom.ToString();
                lblOutputImageWidth.Text = ImageAnalyzerObject.Output.Width.ToString();
                lblOutputImageHeight.Text = ImageAnalyzerObject.Output.Height.ToString();
                lblNumberOfDifferentColors.Text = ImageAnalyzerObject.UsedColors.Count.ToString();
                lblNumberOfRegions.Text = ImageAnalyzerObject.Regions.Count.ToString();
            }
            else {
                menu_Program_SaveOutput.Enabled = false;
                menu_Labeling_SelectAllLabels.Enabled = false;
                menu_Labeling_EditSelectedLabels.Enabled = false;
                menu_Labeling_DeleteSelectedLabels.Enabled = false;
                menu_Labeling_AutomaticLabeling.Enabled = false;
                menu_Labeling_SmallerFont.Enabled = false;
                menu_Labeling_LargerFont.Enabled = false;
                menu_OutputImage_CutTool.Enabled = false;
                toolStripAddLable.Enabled = false;
                toolStripMoveLable.Enabled = false;
                toolStripPan.Enabled = false;
                toolStripMoveLable_Click(null, null);

                picOutput.Enabled = false;
                picOutput.Image = null;

                lblOutputImageWidth.Text = "-";
                lblOutputImageHeight.Text = "-";
                lblNumberOfDifferentColors.Text = "-";
            }
        }

        private void menu_Program_StartAnalyze_Click(object sender, EventArgs e)
        {

            // Update UI
            lblStatusRight.Text = "در حال پردازش تصویر اصلی";
            lnkStopAnalyze.Visible = true;
            menu_Program_StartAnalyze.Enabled = false;
            menu_Program_Open.Enabled = false;
            menu_Program_SaveOutput.Enabled = false;
            menu_Program_SaveDocument.Enabled = false;
            picOutput.Image = null;

            // Disable PictureBoxes
            picInput.Enabled = false;
            picOutput.Enabled = false;

            // Run Background Worker 
            backgroundWorker.RunWorkerAsync();
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            //System.Threading.Thread.CurrentThread.Priority = System.Threading.ThreadPriority.Highest;
            // Start analyzing input image (in a worker thread)
            ImageAnalyzerObject.Analyze();
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            const string IMAGE_PROCCESSED_SUCCESSFULLY = "پردازش تصویر با موفقیت به اتمام رسید.";
            const string IMAGE_PROCCESSING_CANCELED = "عملیات پردازش تصویر لغو شد.";
            const string IMAGE_PROCCESSING_FAILED = "هنگام پردازش تصویر خطایی رخ داد.";

            if (ImageAnalyzerObject.AnalyzeCanceled) {
                // Analyzing input image has canceled
                LabelManagerObject.DeleteAllLabels();
                lblStatusRight.Text = IMAGE_PROCCESSING_CANCELED;
                UpdateUI(true, false);
            }
            else if (ImageAnalyzerObject.AnalyzeException != null) {
                // An error has occured while analyzing input image
                UpdateUI(true, false);
                LabelManagerObject.DeleteAllLabels();

                lblStatusRight.Text = IMAGE_PROCCESSING_FAILED;
                StaticMethods.ShowImageProccessingFailedErrorMessage(ImageAnalyzerObject.AnalyzeException);
            }
            else {
                // IMAGE PROCCESSED SUCCESSFULLY
                picOutput.Image = ImageAnalyzerObject.Output;
                lblStatusRight.Text = IMAGE_PROCCESSED_SUCCESSFULLY;
                tabControlMain.SelectedIndex = 1;
                toolStripCmbZoom.SelectedIndex = 11;

                UpdateUI(true, true);
            }

            menu_Program_StartAnalyze.Enabled = true;
            menu_Program_Open.Enabled = true;
            menu_Program_SaveDocument.Enabled = true;
            lnkStopAnalyze.Visible = false;
        }

        private void menu_Program_SaveOutput_Click(object sender, EventArgs e)
        {
            if (saveOutputFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                System.Drawing.Imaging.ImageFormat defaultImageFormat;

                // Default image format
                if (saveOutputFileDialog.FilterIndex == 1)
                    defaultImageFormat = System.Drawing.Imaging.ImageFormat.Bmp;
                else if (saveOutputFileDialog.FilterIndex == 2)
                    defaultImageFormat = System.Drawing.Imaging.ImageFormat.Jpeg;
                else if (saveOutputFileDialog.FilterIndex == 3)
                    defaultImageFormat = System.Drawing.Imaging.ImageFormat.Png;
                else
                    defaultImageFormat = System.Drawing.Imaging.ImageFormat.Tiff;


                // Save Output Image
                Control.ControlCollection labels;
                labels = (menu_Labeling_ShowLabels.Enabled ? LabelManagerObject.Container.Controls : null);
                try {
                    ImageAnalyzerObject.SaveOutputImage(saveOutputFileDialog.FileName, labels, defaultImageFormat);
                }
                catch (Exception exp) {
                    StaticMethods.ShowErrorMessage("هنگام ذخیره سازی تصویر خطایی رخ داد، لطفا دوباره سعی کنید.", exp);
                }
            }
        }

        private void lnkStopAnalyze_Click(object sender, EventArgs e)
        {
            ImageAnalyzerObject.StopAnalyze();
        }

        private void menu_OutputImage_Size_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem c = sender as ToolStripMenuItem;
            string tag;

            tag = c.Tag.ToString();

            if (tag == "other") {
                // Show FormOutputImageSettings
                menu_OutputImag_Settings_Click(null, null);
            }
            else {
                ImageAnalyzerObject.OutputImageZoom = int.Parse(tag);

                // 'Check' clicked menu item
                UpdateOutputImageMenuUI();

                // Redraw output image
                if (ImageAnalyzerObject.Output != null) {
                    ImageAnalyzerObject.RedrawOutput();
                    picOutput.Image = ImageAnalyzerObject.Output;
                    lblOutputImageZoom.Text = tag + "  برابر";
                }
            }
        }

        private void menu_Program_StartAnalyze_EnabledChanged(object sender, EventArgs e)
        {
            toolStripStartAnalyze.Enabled = menu_Program_StartAnalyze.Enabled;
        }

        private void menu_OutputImage_ShowMainGrid_Click(object sender, EventArgs e)
        {
            ImageAnalyzerObject.Grid.ShowMainGrid = menu_OutputImage_ShowMainGrid.Checked;

            if (ImageAnalyzerObject.Output != null) {
                ImageAnalyzerObject.RedrawOutput();
                picOutput.Image = ImageAnalyzerObject.Output;
                DocumentObject.IsDocumentChanged = true;
            }
        }

        private void menu_OutputImage_ShowSubGrid_Click(object sender, EventArgs e)
        {
            ImageAnalyzerObject.Grid.ShowSubGrid = menu_OutputImage_ShowSubGrid.Checked;

            if (ImageAnalyzerObject.Output != null) {
                ImageAnalyzerObject.RedrawOutput();
                picOutput.Image = ImageAnalyzerObject.Output;
                DocumentObject.IsDocumentChanged = true;
            }
        }

        private void picOutput_MouseMove(object sender, MouseEventArgs e)
        {
            Point p;

            p = ImageAnalyzerObject.GetOriginalPixelLocation(
                (int)(e.X / _zoomScale), (int)(e.Y / _zoomScale));

            // If mouse does not leave the current cell, we should do nothing.
            if (p.X == _mouseInfo.OriginalPixelLocation.X && p.Y == _mouseInfo.OriginalPixelLocation.Y)
                return;
            else {
                _mouseInfo.OriginalPixelLocation = p;
            }

            if (p.X == -1) {
                // Mouse is in border area of output image
                lblStatusLeft.Text = "";
            }
            else {
                if (ImageAnalyzerObject.HighlightRegions) {
                    ImageAnalyzer.Region previouslyHighlightedRegion = ImageAnalyzerObject.HighlightedRegion;
                    ImageAnalyzer.Region r = ImageAnalyzerObject.Pixels[p.X, p.Y].Region;

                    ImageAnalyzerObject.HighlightedRegion = r;

                    // Invalidate picOutput
                    picOutput.Invalidate(ImageAnalyzerObject.GetRegionBoundariesOnOutputImage(r.Boundaries, _zoomScale));
                    if (previouslyHighlightedRegion != null)
                        picOutput.Invalidate(ImageAnalyzerObject.GetRegionBoundariesOnOutputImage(previouslyHighlightedRegion.Boundaries, _zoomScale));

                }
                #region Update Left Status Bar
                _tmpStrBuilder.Remove(0, _tmpStrBuilder.Length);//_tmpStrBuilder.Clear();
                _tmpStrBuilder.Append("XY={");
                _tmpStrBuilder.Append(p.X + 1);
                _tmpStrBuilder.Append(", ");
                _tmpStrBuilder.Append(ImageAnalyzerObject.Input.Height - p.Y);

                _tmpStrBuilder.Append("}  RGBA={");
                _tmpStrBuilder.Append(ImageAnalyzerObject.Pixels[p.X, p.Y].Color.R);
                _tmpStrBuilder.Append(", ");
                _tmpStrBuilder.Append(ImageAnalyzerObject.Pixels[p.X, p.Y].Color.G);
                _tmpStrBuilder.Append(", ");
                _tmpStrBuilder.Append(ImageAnalyzerObject.Pixels[p.X, p.Y].Color.B);
                _tmpStrBuilder.Append(", ");
                _tmpStrBuilder.Append(ImageAnalyzerObject.Pixels[p.X, p.Y].Color.A);

                _tmpStrBuilder.Append("}  ColorID={");
                _mouseInfo.ColorID = ImageAnalyzerObject.UsedColors[ImageAnalyzerObject.Pixels[p.X, p.Y].Color.ToArgb()];
                _tmpStrBuilder.Append(_mouseInfo.ColorID);
                _tmpStrBuilder.Append("}");

                lblStatusLeft.Text = _tmpStrBuilder.ToString();
                #endregion
            }


            #region Pan Image
            if ((e.Button == System.Windows.Forms.MouseButtons.Left) && (_selectedTool == Tool.Pan)) {
                int deltaX = _mouseInfo.PanStartLocation.X - e.X;
                int deltaY = _mouseInfo.PanStartLocation.Y - e.Y;

                tabOutputImage.AutoScrollPosition = new Point(
                    deltaX - tabOutputImage.AutoScrollPosition.X,
                    deltaY - tabOutputImage.AutoScrollPosition.Y);
            }
            #endregion
        }

        private void picOutput_Click(object sender, EventArgs e)
        {
            MouseEventArgs arg = (MouseEventArgs)e;

            if (arg.Button == System.Windows.Forms.MouseButtons.Left) {
                if (_selectedTool == Tool.AddLabel) {
                    // Add a new label
                    string s;
                    Color labelBackgroundColor;

                    if (_mouseInfo.OriginalPixelLocation.X != -1) {
                        s = _mouseInfo.ColorID.ToString();
                        labelBackgroundColor =
                            ImageAnalyzerObject.Pixels[_mouseInfo.OriginalPixelLocation.X,
                            _mouseInfo.OriginalPixelLocation.Y].Color;
                    }
                    else {
                        s = "?";
                        labelBackgroundColor = Color.White;
                    }
                    LabelManagerObject.AddLabel(arg.X, arg.Y, s, labelBackgroundColor);
                }
                else if (_selectedTool == Tool.SelectLabel) {
                    toolStripMoveLable_Click(null, null);
                    LabelManagerObject.DeselectAllLabels();
                }
            }
            else {
                toolStripMoveLable_Click(null, null);
                LabelManagerObject.DeselectAllLabels();
            }

        }

        private void menu_OutputImag_Settings_Click(object sender, EventArgs e)
        {
            FormOutputImageSettings frm = new FormOutputImageSettings();
            frm.MainForm = this;
            frm.ShowDialog(this);
        }

        private void menu_OutputImage_Refresh_Click(object sender, EventArgs e)
        {
            picOutput.Refresh();
        }

        private void toolStripAddLable_Click(object sender, EventArgs e)
        {
            if (_selectedTool != Tool.AddLabel) {
                _selectedTool = Tool.AddLabel;
                toolStripAddLable.Image = imageList.Images[3];
                toolStripMoveLable.Image = imageList.Images[0];
                toolStripPan.Image = imageList.Images[4];
                picOutput.Cursor = Cursors.Cross;

                LabelManagerObject.LockLabels = false;
            }
        }

        private void toolStripMoveLable_Click(object sender, EventArgs e)
        {
            if (_selectedTool != Tool.SelectLabel) {
                _selectedTool = Tool.SelectLabel;
                toolStripMoveLable.Image = imageList.Images[1];
                toolStripAddLable.Image = imageList.Images[2];
                toolStripPan.Image = imageList.Images[4];
                picOutput.Cursor = Cursors.Default;

                LabelManagerObject.LockLabels = false;
            }
        }

        private void toolStripPan_Click(object sender, EventArgs e)
        {
            if (_selectedTool != Tool.Pan) {
                _selectedTool = Tool.Pan;
                toolStripMoveLable.Image = imageList.Images[0];
                toolStripAddLable.Image = imageList.Images[2];
                toolStripPan.Image = imageList.Images[5];
                picOutput.Cursor = Cursors.NoMove2D;

                LabelManagerObject.LockLabels = true;
            }

        }

        private void tabControlMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((tabControlMain.SelectedTab == tabOutputImage) &&
                 (picOutput.Image != null)) {
                toolStripAddLable.Enabled = true;
                toolStripMoveLable.Enabled = true;
                toolStripPan.Enabled = true;
            }
            else {
                toolStripAddLable.Enabled = false;
                toolStripMoveLable.Enabled = false;
                toolStripPan.Enabled = false;
            }
        }

        private void tabControlMain_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.ControlKey)
                LabelManagerObject.CtrlKeyIsPressed = true;
            else if (e.KeyCode == Keys.ShiftKey)
                LabelManagerObject.ShiftKeyIsPressed = true;
            else if (e.KeyCode == Keys.V) {
                if (toolStripAddLable.Enabled)
                    toolStripAddLable_Click(null, null);
            }
            else if (e.KeyCode == Keys.S) {
                if (toolStripMoveLable.Enabled)
                    toolStripMoveLable_Click(null, null);
            }
            else if (e.KeyCode == Keys.H) {
                if (toolStripPan.Enabled)
                    toolStripPan_Click(null, null);
            }

            if (LabelManagerObject.SelectedLabels.Count > 0) {
                if (e.KeyCode == Keys.Up)
                    LabelManagerObject.MoveSelectedUp();
                else if (e.KeyCode == Keys.Down)
                    LabelManagerObject.MoveSelectedDown();
                else if (e.KeyCode == Keys.Right)
                    LabelManagerObject.MoveSelectedRight();
                else if (e.KeyCode == Keys.Left)
                    LabelManagerObject.MoveSelectedLeft();
            }
        }

        private void tabControlMain_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey)
                LabelManagerObject.CtrlKeyIsPressed = false;
            else if (e.KeyCode == Keys.ShiftKey)
                LabelManagerObject.ShiftKeyIsPressed = false;
        }

        private void menu_Labeling_ShowLabels_Click(object sender, EventArgs e)
        {
            LabelManagerObject.ShowLabels = menu_Labeling_ShowLabels.Checked;
        }

        private void menu_Labeling_SelectAllLabels_Click(object sender, EventArgs e)
        {
            LabelManagerObject.SelectAllLabels();
        }

        private void menu_Labeling_DeleteSelectedLabels_Click(object sender, EventArgs e)
        {
            LabelManagerObject.DeleteSelectedLabels();
            DocumentObject.IsDocumentChanged = true;
        }

        private void menu_Labeling_Settings_Click(object sender, EventArgs e)
        {
            FormLabelSettings frm = new FormLabelSettings();
            frm.MainForm = this;
            frm.ShowDialog(this);
        }

        private void menu_Labeling_DeleteSelectedLabels_EnabledChanged(object sender, EventArgs e)
        {
            toolStripDeleteSelectedLabels.Enabled = menu_Labeling_DeleteSelectedLabels.Enabled;
        }

        private void menu_Program_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void menu_About_Software_Click(object sender, EventArgs e)
        {
            FormAbout frmAbout = new FormAbout();
            frmAbout.ShowDialog();
        }

        private void menu_Labeling_SmallerFont_Click(object sender, EventArgs e)
        {
            LabelManagerObject.MakeSelectedLabelsSmaller();
            DocumentObject.IsDocumentChanged = true;
        }

        private void menu_Labeling_LargerFont_Click(object sender, EventArgs e)
        {
            LabelManagerObject.MakeSelectedLabelsLarger();
            DocumentObject.IsDocumentChanged = true;
        }

        private void menu_Labeling_EditSelectedLabels_Click(object sender, EventArgs e)
        {
            LabelManagerObject.EditSelectedLabels();
            DocumentObject.IsDocumentChanged = true;
        }

        private void picOutput_MouseDown(object sender, MouseEventArgs e)
        {
            tabOutputImage.Focus();
            if (_selectedTool == Tool.Pan) {
                _mouseInfo.PanStartLocation = e.Location;
            }
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            // show an alert (save document)
            if (DocumentObject.IsDocumentChanged) {
                DialogResult dlgResult = StaticMethods.ShowDocumentNotSavedAlert();
                if (dlgResult == System.Windows.Forms.DialogResult.Yes)
                    menu_Program_SaveDocument_Click(null, null);
                else if (dlgResult == System.Windows.Forms.DialogResult.Cancel) {
                    e.Cancel = true;
                    return;
                }
            }

            // Form is closing, Save settings
            SettingsManager sm = new SettingsManager(ImageAnalyzerObject, LabelManagerObject);
            sm.SaveSettings();
        }

        private void menu_Program_SaveDocument_Click(object sender, EventArgs e)
        {
            const string PROGRAM_TITLE = "تولید نقشه خانه بندی";
            string fileName;

            if (DocumentObject.FileName == "") {
                // Document is saved for first time, show SaveFileDialog
                if (saveDocumentFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    fileName = saveDocumentFileDialog.FileName;
                else
                    return;
            }
            else {
                // Overwrite document file
                fileName = DocumentObject.FileName;
            }

            // -------- Save Document

            // Add labels to document object
            DocumentObject.Labels.Clear();
            foreach (Label l in LabelManagerObject.Container.Controls) {
                DocumentObject.Labels.Add(l);
            }

            DocumentObject.OutputImageZoom = (ushort)ImageAnalyzerObject.OutputImageZoom;
            try {
                DocumentObject.SaveToFile(fileName);
                txtInputImageFileName.Text = fileName;
                this.Text = System.IO.Path.GetFileName(fileName) + " - " + PROGRAM_TITLE;
            }
            catch (Exception exp) {
                StaticMethods.ShowErrorMessage("هنگام ذخیره سازی پرونده خطایی رخ داد، لطفا دوباره سعی کنید.", exp);
            }
        }

        private void menu_Program_Open_Click(object sender, EventArgs e)
        {
            // show an alert (save document)
            if (DocumentObject.IsDocumentChanged) {
                DialogResult dlgResult = StaticMethods.ShowDocumentNotSavedAlert();
                if (dlgResult == System.Windows.Forms.DialogResult.Yes)
                    menu_Program_SaveDocument_Click(null, null);
                else if (dlgResult == System.Windows.Forms.DialogResult.Cancel)
                    return;
            }

            // Show Open Dialog Box
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                LoadFile(openFileDialog.FileName);
            }
        }

        private void menu_Program_Open_EnabledChanged(object sender, EventArgs e)
        {
            toolStripOpen.Enabled = menu_Program_Open.Enabled;
        }

        private void menu_Program_SaveDocument_EnabledChanged(object sender, EventArgs e)
        {
            toolStripSaveDocument.Enabled = menu_Program_SaveDocument.Enabled;
        }

        private void toolStripCmbZoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Update UI
            tabOutputImage.Focus();
            if (toolStripCmbZoom.SelectedIndex == 0) {
                // first item
                menu_OutputImage_Zoom_Out.Enabled = false;
                menu_OutputImage_Zoom_In.Enabled = true;
            }
            else if (toolStripCmbZoom.SelectedIndex == toolStripCmbZoom.Items.Count - 1) {
                // last item
                menu_OutputImage_Zoom_In.Enabled = false;
                menu_OutputImage_Zoom_Out.Enabled = true;
            }
            else {
                // Middle items
                menu_OutputImage_Zoom_In.Enabled = true;
                menu_OutputImage_Zoom_Out.Enabled = true;
            }

            //
            if (ImageAnalyzerObject.Output == null)
                return;

            //
            string cmbZoomText = toolStripCmbZoom.Text.Replace("%", "");

            if (float.TryParse(cmbZoomText, out _zoomScale)) {
                _zoomScale = _zoomScale / 100;

                // Change Zoom
                picOutput.SuspendLayout();
                picOutput.Visible = false;

                picOutput.SizeMode = PictureBoxSizeMode.StretchImage;
                picOutput.Width = (int)(ImageAnalyzerObject.Output.Width * _zoomScale);
                picOutput.Height = (int)(ImageAnalyzerObject.Output.Height * _zoomScale);
                LabelManagerObject.ZoomScale = _zoomScale;

                picOutput.Invalidate(true);
                picOutput.ResumeLayout(true);
                picOutput.Visible = true;
                toolStripCmbZoom.BackColor = Color.White;
            }
            else {
                // An invalid number is entered
                toolStripCmbZoom.BackColor = Color.Red;
            }
        }

        private void menu_OutputImage_Zoom_In_Click(object sender, EventArgs e)
        {
            toolStripCmbZoom.SelectedIndex += 1;
        }

        private void menu_OutputImage_Zoom_Out_Click(object sender, EventArgs e)
        {
            toolStripCmbZoom.SelectedIndex -= 1;
        }

        private void menu_OutputImage_Zoom_In_EnabledChanged(object sender, EventArgs e)
        {
            toolStripZoomIn.Enabled = menu_OutputImage_Zoom_In.Enabled;
        }

        private void menu_OutputImage_Zoom_Out_EnabledChanged(object sender, EventArgs e)
        {
            toolStripZoomOut.Enabled = menu_OutputImage_Zoom_Out.Enabled;
        }

        private void menu_OutputImage_Zoom_ZoomChanged(object sender, EventArgs e)
        {
            int index = int.Parse((sender as ToolStripMenuItem).Tag.ToString());
            toolStripCmbZoom.SelectedIndex = index;
        }

        private void toolStripCmbZoom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                toolStripCmbZoom.SelectedIndex = 11;
        }

        private void menu_OutputImage_HighlightRegion_CheckedChanged(object sender, EventArgs e)
        {
            ImageAnalyzerObject.HighlightRegions = menu_OutputImage_HighlightRegion.Checked;

            if (!menu_OutputImage_HighlightRegion.Checked)
                ImageAnalyzerObject.HighlightedRegion = null;
        }

        private void menu_Labeling_AutomaticLabeling_Click(object sender, EventArgs e)
        {
            if (_frmAutoLabeling == null) {
                _frmAutoLabeling = new FormAutomaticLabeling();
                _frmAutoLabeling.ImageAnalyzerObject = this.ImageAnalyzerObject;
                _frmAutoLabeling.LabelManagerObject = this.LabelManagerObject;
            }

            _frmAutoLabeling.Show();
            _frmAutoLabeling.Focus();
        }

        private void menu_OutputImage_CutTool_Click(object sender, EventArgs e)
        {
            if (_frmImageCutter == null) {
                _frmImageCutter = new FormImageCutter();
                _frmImageCutter.ImageAnalyzerObject = this.ImageAnalyzerObject;
                _frmImageCutter.LabelManagerObject = this.LabelManagerObject;
            }

            if (txtInputImageFileName.Text != "-") {
                System.IO.FileInfo fi = new System.IO.FileInfo(txtInputImageFileName.Text);
                _frmImageCutter.folderBrowserDialog.SelectedPath = System.IO.Path.GetFullPath(fi.DirectoryName);
            }
            _frmImageCutter.Show();
            _frmImageCutter.Focus();
        }

    }
}