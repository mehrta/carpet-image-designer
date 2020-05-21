using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Collections;

namespace CrossPlanGenerator
{
    public class ImageAnalyzer
    {
        // Subclasses
        public class CPoint // We don't use "System.Drawing.Point" Struct for efficiency issues 
        {
            public int X;
            public int Y;
        }

        public class Region
        {
            // Fields
            private static uint _idCounter;

            // Properties
            public uint ID { set; get; }
            public int ColorID { set; get; }
            public Rectangle Boundaries;
            public Point GeometricSum;
            public List<CPoint> Pixels { set; get; }

            // Methods
            public Region()
            {
                Pixels = new List<CPoint>();
                this.ID = (++_idCounter);
            }

            public Region(int colorID, Rectangle boundaries)
            {
                Pixels = new List<CPoint>();
                this.ID = (++_idCounter);
                this.ColorID = colorID;
                this.Boundaries = boundaries;
            }

            public static void ResetIdCounter()
            {
                _idCounter = 0;
            }

            public void Merge(Region r, Pixel[,] pixels)
            {
                for (int i = 0; i < r.Pixels.Count; i++) {
                    this.Pixels.Add(r.Pixels[i]);
                    pixels[r.Pixels[i].X, r.Pixels[i].Y].Region = this;
                }
                this.Boundaries = Rectangle.Union(this.Boundaries, r.Boundaries);
                this.GeometricSum.X += r.GeometricSum.X;
                this.GeometricSum.Y += r.GeometricSum.Y;
            }
        }

        public class Pixel
        {
            public Region Region { set; get; }
            public Color Color { set; get; }
        }

        public class GridSettings
        {
            public bool ShowMainGrid { set; get; }
            public bool ShowSubGrid { set; get; }
            public int MainGridSize { set; get; }
            public int SubGridCorrectiveValue { set; get; }
            public Color MainGridColor1 { set; get; }
            public Color MainGridColor2 { set; get; }
        }

        // Fields
        private Region _highlightedRegion;
        private int _colorIdCounter;
        private bool _continueAnalyze;

        // Properties
        public Bitmap Input { set; get; }
        public Bitmap Output { private set; get; }
        public List<Region> Regions { private set; get; }
        public Pixel[,] Pixels { private set; get; }
        public GridSettings Grid { set; get; }
        public Dictionary<int, int> UsedColors { private set; get; } // Dictionary<Color, ColorID>
        public int OutputImageZoom { set; get; }
        public bool AnalyzeCanceled { set; get; }
        public int BorderSize { set; get; }
        public Exception AnalyzeException { set; get; }
        public bool HighlightRegions { set; get; }
        public Region HighlightedRegion
        {
            set
            {
                if (_highlightedRegion == value)
                    return;

                // Restore the color of previously highlighted region
                if (_highlightedRegion != null) {
                    CPoint p;

                    p = _highlightedRegion.Pixels[0];
                    FillRegion(_highlightedRegion, this.Input.GetPixel(p.X, p.Y));
                }

                // Highlight region "value"
                if (value != null) {
                    CPoint p;
                    Color hColor;

                    p = value.Pixels[0];

                    // Compute highlight color
                    int k = 40;
                    hColor = this.Input.GetPixel(p.X, p.Y);

                    if (hColor.R > 100 && hColor.G > 100 && hColor.B > 100)
                        k = k * -1;
                    hColor = Color.FromArgb(Math.Min(255, hColor.R + k), Math.Min(255, hColor.G + k), Math.Min(255, hColor.B + k));

                    // Fill Region "value" with color "hColor"
                    FillRegion(value, hColor);
                }

                //
                _highlightedRegion = value;
            }

            get
            {
                return _highlightedRegion;
            }
        }

        // Methods
        public ImageAnalyzer()
        {
            this.Regions = new List<Region>();
            this.UsedColors = new Dictionary<int, int>(256);

            this.Grid = new GridSettings();
            this.Grid.ShowMainGrid = true;
            this.Grid.ShowSubGrid = true;
            this.Grid.MainGridSize = 10;
            this.Grid.SubGridCorrectiveValue = -25;
            this.Grid.MainGridColor1 = Color.Black;
            this.Grid.MainGridColor2 = Color.Blue;
            this.AnalyzeException = null;

            this.OutputImageZoom = 8;
            this.BorderSize = 60;
            _continueAnalyze = true;
            _highlightedRegion = null;
        }

        public void Rest()
        {
            Input = null;
            Output = null;
            Pixels = null;
            Regions.Clear();
            AnalyzeException = null;
            _highlightedRegion = null;
        }

        public void Analyze()
        {
            Region reg; // Temprory object
            Bitmap inputBmp;
            Color currentPixelColor;
            CPoint currentPixel; // Current pixel location (in Input Image coordinate)

            // Initialize
            this.Output = null;
            Pixels = new Pixel[Input.Width, Input.Height];
            currentPixel = new CPoint();
            inputBmp = (Bitmap)Input;
            _colorIdCounter = 0;
            Regions.Clear();
            UsedColors.Clear();
            Region.ResetIdCounter();
            this.AnalyzeCanceled = false;
            this.AnalyzeException = null;

            #region Fast Algorithm
            //try {
            //    // Initialize
            //    Pixels = new Pixel[Input.Width, Input.Height];
            //    currentPixel = new CPoint();
            //    inputBmp = (Bitmap)Input;
            //    _colorIdCounter = 0;
            //    Regions.Clear();
            //    UsedColors.Clear();
            //    Region.ResetIdCounter();
            //    this.AnalyzeCanceled = false;
            //    this.AnalyzeException = null;

            //    for (int y = 0; y < Input.Height; y++) {
            //        for (int x = 0; x < Input.Width; x++) {
            //            Pixels[x, y] = new Pixel();
            //            reg = new Region();

            //            Pixels[x, y].Color = inputBmp.GetPixel(x, y);
            //            reg.ColorID = GetColorID(Pixels[x, y].Color);
            //            Pixels[x, y].Region = reg;
            //        }
            //    }
            //}
            //catch (Exception e) {
            //    AnalyzeException = e;
            //    this.Pixels = null;
            //    this.UsedColors = null;
            //    this.Output = null;
            //    GC.Collect();
            //    return;
            //}
            #endregion

            #region Complete Algorithm

            try {
                #region Scan First Row

                // Scan inputBmp[0, 0]
                reg = new Region();

                currentPixel.X = 0;
                currentPixel.Y = 0;

                Pixels[0, 0] = new Pixel();
                Pixels[0, 0].Region = reg;
                Pixels[0, 0].Color = inputBmp.GetPixel(0, 0);

                reg.ColorID = GetColorID(Pixels[0, 0].Color);
                reg.Boundaries = new Rectangle(0, 0, 1, 1);
                reg.GeometricSum = new Point(0, 0);
                reg.Pixels.Add(currentPixel);
                Regions.Add(reg);

                // Scan Pixel [1...Input.Width-1] of First Row
                // reg = null;
                for (int x = 1; x < Input.Width; x++) {
                    currentPixel = new CPoint();

                    currentPixel.X = x;
                    currentPixel.Y = 0;
                    currentPixelColor = inputBmp.GetPixel(x, 0);

                    if (currentPixelColor == Pixels[x - 1, 0].Color) {
                        // Add this pixel to it's left neighbor's region
                        reg = Pixels[x - 1, 0].Region;
                        reg.Pixels.Add(currentPixel);
                        reg.Boundaries.Width += 1;
                        // reg.GeometricSum.Y = 0;  // No need to set. it has already set to zero 
                    }
                    else {
                        // Create a new Region for currentPixel
                        reg = new Region(GetColorID(currentPixelColor), new Rectangle(x, 0, 1, 1));
                        reg.Pixels.Add(currentPixel);
                        Regions.Add(reg);
                    }

                    reg.GeometricSum.X += currentPixel.X;
                    reg.GeometricSum.Y += currentPixel.Y;
                    Pixels[x, 0] = new Pixel();
                    Pixels[x, 0].Color = currentPixelColor;
                    Pixels[x, 0].Region = reg;
                }
                #endregion

                #region Scan Rest of Image
                // reg = null;
                for (int y = 1; (y < Input.Height) && (_continueAnalyze); y++) {

                    #region Scan First Pixel of Current Row
                    currentPixel = new CPoint();

                    currentPixel.X = 0;
                    currentPixel.Y = y;

                    currentPixelColor = inputBmp.GetPixel(0, y);

                    if (currentPixelColor == Pixels[0, currentPixel.Y - 1].Color) {
                        reg = Pixels[0, currentPixel.Y - 1].Region;
                        reg.Boundaries.Height++;
                    }
                    else if (currentPixelColor == Pixels[1, currentPixel.Y - 1].Color) {
                        reg = Pixels[1, currentPixel.Y - 1].Region;
                        reg.Boundaries = Rectangle.Union(reg.Boundaries, new Rectangle(currentPixel.X, currentPixel.Y, 1, 1));
                    }
                    else {
                        reg = new Region(GetColorID(currentPixelColor), new Rectangle(0, currentPixel.Y, 1, 1));
                        Regions.Add(reg);
                    }

                    //reg.GeometricSum.X += currentPixel.X;
                    reg.GeometricSum.Y += currentPixel.Y;
                    reg.Pixels.Add(currentPixel);
                    Pixels[0, currentPixel.Y] = new Pixel();
                    Pixels[0, currentPixel.Y].Region = reg;
                    Pixels[0, currentPixel.Y].Color = currentPixelColor;
                    #endregion

                    #region Scan Inner Pixels of Current Row
                    for (int x = 1; x < Input.Width - 1; x++) {
                        currentPixel = new CPoint();
                        currentPixel.X = x;
                        currentPixel.Y = y;
                        currentPixelColor = inputBmp.GetPixel(x, y);

                        if (currentPixelColor == Pixels[x - 1, currentPixel.Y].Color) {
                            reg = Pixels[x - 1, currentPixel.Y].Region;
                        }
                        else if (currentPixelColor == Pixels[x - 1, currentPixel.Y - 1].Color) {
                            reg = Pixels[x - 1, currentPixel.Y - 1].Region;
                        }
                        else if (currentPixelColor == Pixels[x, currentPixel.Y - 1].Color) {
                            reg = Pixels[x, currentPixel.Y - 1].Region;
                        }
                        else if (currentPixelColor == Pixels[x + 1, currentPixel.Y - 1].Color) {
                            reg = Pixels[x + 1, currentPixel.Y - 1].Region;
                        }
                        else {
                            // Create a new region
                            reg = new Region();
                            reg.ColorID = GetColorID(currentPixelColor);
                            reg.Boundaries = new Rectangle(currentPixel.X, currentPixel.Y, 1, 1);
                            Regions.Add(reg);
                        }

                        reg.GeometricSum.X += currentPixel.X;
                        reg.GeometricSum.Y += currentPixel.Y;
                        reg.Boundaries = Rectangle.Union(reg.Boundaries, new Rectangle(currentPixel.X, currentPixel.Y, 1, 1));
                        reg.Pixels.Add(currentPixel);

                        Pixels[currentPixel.X, currentPixel.Y] = new Pixel();
                        Pixels[currentPixel.X, currentPixel.Y].Region = reg;
                        Pixels[currentPixel.X, currentPixel.Y].Color = currentPixelColor;

                        // Merge Regions with same color
                        if ((currentPixelColor == Pixels[x - 1, currentPixel.Y - 1].Color) &&
                            (currentPixelColor == Pixels[x + 1, currentPixel.Y - 1].Color) &&
                            (Pixels[x - 1, currentPixel.Y - 1].Region.ID != Pixels[x + 1, currentPixel.Y - 1].Region.ID)) {

                            Regions.Remove(Pixels[x + 1, currentPixel.Y - 1].Region);
                            Pixels[x - 1, currentPixel.Y - 1].Region.Merge(Pixels[x + 1, currentPixel.Y - 1].Region, Pixels);
                        }

                        if ((currentPixelColor == Pixels[x - 1, currentPixel.Y].Color) &&
                            (currentPixelColor == Pixels[x + 1, currentPixel.Y - 1].Color) &&
                            (Pixels[x - 1, currentPixel.Y].Region.ID != Pixels[x + 1, currentPixel.Y - 1].Region.ID)) {

                            Regions.Remove(Pixels[x + 1, currentPixel.Y - 1].Region);
                            Pixels[x - 1, currentPixel.Y].Region.Merge(Pixels[x + 1, currentPixel.Y - 1].Region, Pixels);
                        }
                    }
                    #endregion

                    #region Scan Last Pixel of Current Row
                    currentPixel = new CPoint();
                    currentPixel.X = Input.Width - 1;
                    currentPixel.Y = y;
                    currentPixelColor = inputBmp.GetPixel(currentPixel.X, currentPixel.Y);

                    if (currentPixelColor == Pixels[currentPixel.X - 1, currentPixel.Y].Color) {
                        reg = Pixels[currentPixel.X - 1, currentPixel.Y].Region;
                    }
                    else if (currentPixelColor == Pixels[currentPixel.X - 1, currentPixel.Y - 1].Color) {
                        reg = Pixels[currentPixel.X - 1, currentPixel.Y - 1].Region;
                    }
                    else if (currentPixelColor == Pixels[currentPixel.X, currentPixel.Y - 1].Color) {
                        reg = Pixels[currentPixel.X, currentPixel.Y - 1].Region;
                    }
                    else {
                        reg = new Region(GetColorID(currentPixelColor), new Rectangle(currentPixel.X, currentPixel.Y, 1, 1));
                        Regions.Add(reg);
                    }

                    reg.GeometricSum.X += currentPixel.X;
                    reg.GeometricSum.Y += currentPixel.Y;
                    reg.Boundaries = Rectangle.Union(reg.Boundaries, new Rectangle(currentPixel.X, currentPixel.Y, 1, 1));
                    reg.Pixels.Add(currentPixel);

                    Pixels[currentPixel.X, currentPixel.Y] = new Pixel();
                    Pixels[currentPixel.X, currentPixel.Y].Region = reg;
                    Pixels[currentPixel.X, currentPixel.Y].Color = currentPixelColor;
                    #endregion
                }
                #endregion
            }
            catch (Exception e) {
                AnalyzeException = e;
                this.Pixels = null;
                this.UsedColors = null;
                this.Output = null;
                GC.Collect();
                return;
            }
            #endregion

            // Produce Output Image
            if (_continueAnalyze) {
                RedrawOutput();
            }
            else {
                // Operations canceled, Reset object
                _continueAnalyze = true;
                this.AnalyzeCanceled = true;
            }
        }

        public void StopAnalyze()
        {
            _continueAnalyze = false;
        }

        public void RedrawOutput()
        {
            int outputWidth, outputHeight;
            Bitmap outputBmp;
            Graphics g;

            outputWidth = (Input.Width * this.OutputImageZoom) + (2 * this.BorderSize);
            outputHeight = (Input.Height * this.OutputImageZoom) + (2 * this.BorderSize);
            outputBmp = new Bitmap(outputWidth, outputHeight);
            g = Graphics.FromImage(outputBmp);

            // Draw white border around output image
            Rectangle r1 = new Rectangle(0, 0, outputBmp.Width, BorderSize);
            Rectangle r2 = new Rectangle(outputBmp.Width - BorderSize, BorderSize, BorderSize, outputBmp.Height - BorderSize);
            Rectangle r3 = new Rectangle(0, outputBmp.Height - BorderSize, outputBmp.Width - BorderSize, BorderSize);
            Rectangle r4 = new Rectangle(0, BorderSize, BorderSize, outputBmp.Height - BorderSize - BorderSize);
            g.FillRectangles(new SolidBrush(Color.White), new Rectangle[] { r1, r2, r3, r4 });

            // Draw output image
            for (int y = 0; (y < Input.Height) && (_continueAnalyze); y++) {
                for (int x = 0; x < Input.Width; x++) {
                    g.FillRectangle(new SolidBrush(Pixels[x, y].Color),
                        x * OutputImageZoom + this.BorderSize,
                        y * OutputImageZoom + this.BorderSize,
                        OutputImageZoom,
                        OutputImageZoom);
                }
            }


            Pen gridPen = new Pen(Color.Black);
            #region Draw Sub Grid
            Point HorP1 = new Point();
            Point HorP2 = new Point();
            Point VerP1 = new Point();
            Point VerP2 = new Point();

            if (Grid.ShowSubGrid && _continueAnalyze) {

                for (int y = 0; y < Input.Height - 1; y++) {

                    HorP1.X = this.BorderSize;
                    HorP1.Y = this.BorderSize + (OutputImageZoom * (y + 1)) - 1;
                    HorP2.X = this.BorderSize + OutputImageZoom - 1;
                    HorP2.Y = HorP1.Y;

                    VerP1.X = this.BorderSize + OutputImageZoom - 1;
                    VerP1.Y = this.BorderSize + (y * OutputImageZoom);
                    VerP2.X = VerP1.X;
                    VerP2.Y = HorP2.Y;

                    for (int x = 0; x < Input.Width - 1; x++) {

                        // Draw Horizontal Line Segment
                        gridPen.Color = GetGridColor(Pixels[x, y].Color, Pixels[x, y + 1].Color);
                        g.DrawLine(gridPen, HorP1, HorP2);

                        // Draw Vertical Line Segment
                        gridPen.Color = GetGridColor(Pixels[x, y].Color, Pixels[x + 1, y].Color);
                        g.DrawLine(gridPen, VerP1, VerP2);

                        // Update Horizontal Line Location
                        HorP1.X += OutputImageZoom;
                        HorP2.X += OutputImageZoom;

                        VerP1.X += OutputImageZoom;
                        VerP2.X += OutputImageZoom;
                    }
                }

                // Draw vertical line segments of last row
                int lastY = Input.Height - 1;
                VerP1.X = this.BorderSize + OutputImageZoom - 1;
                VerP1.Y = this.BorderSize + (lastY * OutputImageZoom);
                VerP2.X = VerP1.X;
                VerP2.Y = VerP1.Y + OutputImageZoom - 1;
                for (int x = 0; x < Input.Width - 1; x++) {
                    gridPen.Color = GetGridColor(Pixels[x, lastY].Color, Pixels[x + 1, lastY].Color);
                    g.DrawLine(gridPen, VerP1, VerP2);
                    VerP1.X += OutputImageZoom;
                    VerP2.X += OutputImageZoom;
                }

                // Draw horizontal line segments of last column
                int lastX = Input.Width - 1;
                HorP1.X = this.BorderSize + (lastX * OutputImageZoom) - 1;
                HorP1.Y = this.BorderSize + OutputImageZoom - 1;
                HorP2.X = HorP1.X + OutputImageZoom - 1;
                HorP2.Y = HorP1.Y;
                for (int y = 0; y < Input.Height - 1; y++) {
                    gridPen.Color = GetGridColor(Pixels[lastX, y].Color, Pixels[lastX, y + 1].Color);
                    g.DrawLine(gridPen, HorP1, HorP2);
                    HorP1.Y += OutputImageZoom;
                    HorP2.Y += OutputImageZoom;

                }

                // Draw First Horizontal Line
                gridPen.Color = Color.Gray;
                HorP1.X = this.BorderSize;
                HorP1.Y = this.BorderSize;
                HorP2.X = outputBmp.Width - this.BorderSize - 1;
                HorP2.Y = this.BorderSize;
                g.DrawLine(gridPen, HorP1, HorP2);

                // Draw Last Vertical Line
                VerP1.X = outputBmp.Width - this.BorderSize - 1;
                VerP1.Y = this.BorderSize;
                VerP2.X = VerP1.X;
                VerP2.Y = outputBmp.Height - this.BorderSize - 1;
                g.DrawLine(gridPen, VerP1, VerP2);

                // ---------- Old Algorithm
                //// Horizontal Lines
                //for (int y = 0; y <= Input.Height; y++) {
                //    int lineY = y * OutputImageZoom + this.BorderSize;
                //    g.DrawLine(gridPen, this.BorderSize, lineY, outputBmp.Width - this.BorderSize, lineY);
                //}
                //// Vertical Lines
                //for (int x = 0; x <= Input.Width; x++) {
                //    int lineX = x * OutputImageZoom + this.BorderSize;
                //    g.DrawLine(gridPen, lineX, this.BorderSize, lineX, outputBmp.Height - this.BorderSize);
                //}
            }
            #endregion

            #region Show Main Grid
            if (Grid.ShowMainGrid && _continueAnalyze) {
                int lineCounter = 0;

                // Horizontal Lines
                gridPen.Color = this.Grid.MainGridColor1;
                for (int y = Input.Height; y >= 0; y -= Grid.MainGridSize) {
                    int lineY = y * OutputImageZoom + this.BorderSize - 1;
                    g.DrawLine(gridPen, this.BorderSize, lineY, outputBmp.Width - this.BorderSize - 1, lineY);

                    lineCounter++;
                    if (lineCounter == 5) {
                        gridPen.Color = this.Grid.MainGridColor2;
                        lineCounter = 0;
                    }
                    else {
                        gridPen.Color = this.Grid.MainGridColor1;
                    }
                }

                // Vertical Lines
                lineCounter = 0;
                gridPen.Color = this.Grid.MainGridColor1;
                for (int x = 0; x <= Input.Width; x += Grid.MainGridSize) {
                    int lineX = x * OutputImageZoom + this.BorderSize - 1;
                    g.DrawLine(gridPen, lineX, this.BorderSize, lineX, outputBmp.Height - this.BorderSize - 1);

                    lineCounter++;
                    if (lineCounter == 5) {
                        gridPen.Color = this.Grid.MainGridColor2;
                        lineCounter = 0;
                    }
                    else {
                        gridPen.Color = this.Grid.MainGridColor1;
                    }
                }
            }
            #endregion

            #region Draw Grid Numbers
            Font numbersFont = new Font("B Sara", 21, FontStyle.Bold);
            SolidBrush numbersBrush = new SolidBrush(Color.Blue);
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            int labelX, lableWidth;
            for (int x = Grid.MainGridSize; x <= Input.Width; x += Grid.MainGridSize) {
                lableWidth = (int)g.MeasureString(x.ToString(), numbersFont).Width;
                labelX = (this.BorderSize + x * this.OutputImageZoom) - (lableWidth / 2);
                g.DrawString(x.ToString(), numbersFont, numbersBrush, labelX, outputBmp.Height - BorderSize + 4);
            }

            Rectangle rectLabel = new Rectangle();
            int yLabelText = Grid.MainGridSize;
            for (int y = Input.Height - Grid.MainGridSize; y >= 0; y -= Grid.MainGridSize, yLabelText += Grid.MainGridSize) {
                rectLabel.Size = g.MeasureString(yLabelText.ToString(), numbersFont).ToSize();
                rectLabel.X = this.BorderSize - rectLabel.Width - 2;
                rectLabel.Y = y * OutputImageZoom + this.BorderSize - (rectLabel.Height / 2);
                g.DrawString(yLabelText.ToString(), numbersFont, numbersBrush, rectLabel.Location);
            }

            #endregion

            if (_continueAnalyze) {
                this.Output = outputBmp;
            }
            else {
                this.Output = null;

                // Reset Object for next use
                _continueAnalyze = true;
                this.AnalyzeCanceled = true;
            }
        }

        public Bitmap[,] CutOutputImage(int cutWidth, int cutHeight, string footerText, Color footerColor, Font footerFont, int borderSize, ICollection labels)
        {
            int segmentsColumns, segmentsRows, segmentWidth, segmentHeight, copyWidth, copyHeight, palletCols, palletRows;
            Bitmap[,] segments;
            Graphics g;
            Font numbersFont = new Font("B Sara", 21, FontStyle.Bold);
            SolidBrush numbersBrush = new SolidBrush(Color.Blue);
            Size footerSize = new Size();
            Size palletCellSize = new Size();
            Image outputImage;

            outputImage = this.Output;

            // Draw Labels On the Output Image
            if (labels.Count > 0) {
                outputImage = new Bitmap(this.Output);
                DrawLabelsOnImage(outputImage, labels);
            }

            // Initialize
            segmentWidth = cutWidth * OutputImageZoom;
            segmentHeight = cutHeight * OutputImageZoom;

            palletCellSize.Width = 2 * OutputImageZoom;
            palletCellSize.Height = palletCellSize.Width;

            palletCols = segmentWidth / palletCellSize.Width;
            palletRows = (int)Math.Ceiling((float)UsedColors.Count / palletCols);

            footerSize = Graphics.FromImage(Output).MeasureString(footerText, footerFont).ToSize();

            segmentsColumns = (int)Math.Ceiling((double)Input.Width / cutWidth);
            segmentsRows = (int)Math.Ceiling((double)Input.Height / cutHeight);


            // Create an array of image segments
            segments = new Bitmap[segmentsColumns, segmentsRows];

            for (int y = 0; y < segmentsRows; y++)
                for (int x = 0; x < segmentsColumns; x++) {
                    // Create a bitmap
                    segments[x, y] = new Bitmap(
                        segmentWidth + 2 * borderSize,
                        2 * borderSize + segmentHeight + (palletRows * palletCellSize.Height) + footerSize.Height + 30);
                    g = Graphics.FromImage(segments[x, y]);

                    #region Copy output image to segments
                    // Fill image with white color
                    g.Clear(Color.White);

                    // Calculate width and height of copy
                    copyWidth = Math.Min(segmentWidth, (Input.Width * OutputImageZoom) - (x * segmentWidth));
                    copyHeight = Math.Min(segmentHeight, (Input.Height * OutputImageZoom) - (y * segmentHeight));

                    // Copy segment pixels from Output image to segments[x,y] 
                    g.DrawImage(
                        outputImage,
                        borderSize,
                        borderSize,
                        new Rectangle(this.BorderSize + segmentWidth * x,
                        this.BorderSize + segmentHeight * y,
                        copyWidth,
                        copyHeight),
                        GraphicsUnit.Pixel);

                    #endregion

                    #region Draw Grid Numbers
                    g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

                    int segmentLeft, segmentTop, firstNumber;
                    Size lSize;
                    Point lLoc = new Point();
                    segmentLeft = cutWidth * x;
                    segmentTop = cutHeight * y;

                    // Draw Horizontal Labels

                    firstNumber = segmentLeft - (segmentLeft % Grid.MainGridSize) + Grid.MainGridSize;

                    for (int numX = firstNumber; (numX <= segmentLeft + cutWidth) && (numX <= Input.Width); numX += Grid.MainGridSize) {
                        lSize = g.MeasureString(numX.ToString(), numbersFont).ToSize();
                        lLoc.X = borderSize + (numX - segmentLeft) * OutputImageZoom - (lSize.Width / 2);
                        lLoc.Y = borderSize + copyHeight + 5;
                        g.DrawString(numX.ToString(), numbersFont, numbersBrush, lLoc.X, lLoc.Y);
                    }

                    // Draw Vertical Labels
                    int reverseSegmentTop = this.Input.Height - segmentTop;
                    firstNumber = reverseSegmentTop - (reverseSegmentTop % Grid.MainGridSize);
                    lLoc.Y = borderSize + ((reverseSegmentTop - firstNumber) * OutputImageZoom);
                    for (int numY = firstNumber; (numY > reverseSegmentTop - cutHeight && (numY > 0)); numY -= Grid.MainGridSize) {
                        lSize = g.MeasureString(numY.ToString(), numbersFont).ToSize();
                        lLoc.X = borderSize - lSize.Width - 3;
                        g.DrawString(numY.ToString(), numbersFont, numbersBrush, lLoc.X, (lLoc.Y - lSize.Height / 2));
                        lLoc.Y += (Grid.MainGridSize * OutputImageZoom);
                    }

                    #endregion

                    #region Draw Color Pallet
                    GraphicTable gt = new GraphicTable(g);

                    gt.BorderColor = Color.Gray;
                    gt.Rows = palletRows;
                    gt.Columns = palletCols;
                    gt.CellSize = palletCellSize.Width;
                    gt.Location.X = borderSize;
                    gt.Location.Y = 2 * borderSize + segmentHeight + 10;

                    // Draw Lines of Tabel
                    gt.Draw();

                    // Fill cells
                    int cellRowIndex = 0, cellColIndex = 0;

                    foreach (int key in UsedColors.Keys) {
                        gt.SetCellColor(cellRowIndex, cellColIndex, Color.FromArgb(key));
                        cellColIndex++;
                        if (cellColIndex == palletCols) {
                            cellColIndex = 0;
                            cellRowIndex++;
                        }
                    }
                    #endregion

                    #region Draw Footer
                    if (footerText != "") {
                        g.DrawString(
                            footerText,
                            footerFont,
                            new SolidBrush(footerColor),
                            borderSize + (segmentWidth - footerSize.Width) / 2,
                            gt.Bottom + 10);
                    }
                    #endregion
                }

            return segments;
        }

        public Color GetGridColor(Color color1, Color color2)
        {
            int r, g, b;

            r = ((color1.R + color2.R) / 2) + Grid.SubGridCorrectiveValue;
            g = ((color1.G + color2.G) / 2) + Grid.SubGridCorrectiveValue;
            b = ((color1.B + color2.B) / 2) + Grid.SubGridCorrectiveValue;

            // R
            if (r < 0)
                r = 0;
            else if (r > 255)
                r = 255;

            // G
            if (g < 0)
                g = 0;
            else if (g > 255)
                g = 255;

            // B
            if (b < 0)
                b = 0;
            else if (b > 255)
                b = 255;

            return Color.FromArgb(r, g, b);
        }

        public Point GetOriginalPixelLocation(int outputImageLocationX, int outputImageLocationY)
        {
            Point p = new Point();

            int x = outputImageLocationX - this.BorderSize;
            int y = outputImageLocationY - this.BorderSize;

            if ((x < 0) || (y < 0) ||
                (outputImageLocationX >= Output.Width - this.BorderSize) ||
                (outputImageLocationY >= Output.Height - this.BorderSize)) {
                // Mouse is on the border of image
                p.X = -1;
                p.Y = -1;
            }
            else {
                p.X = x / OutputImageZoom;
                p.Y = y / OutputImageZoom;
            }

            return p;
        }

        public void SaveOutputImage(string fileName, System.Windows.Forms.Control.ControlCollection labels, System.Drawing.Imaging.ImageFormat defaultImageFormat)
        {
            // Detect file extension
            string fileExtension;
            System.Drawing.Imaging.ImageFormat imgFormat;

            fileExtension = System.IO.Path.GetExtension(fileName).ToLower();

            if (fileExtension == "bmp")
                imgFormat = System.Drawing.Imaging.ImageFormat.Bmp;
            else if (fileExtension == "jpg" || fileExtension == "jpeg")
                imgFormat = System.Drawing.Imaging.ImageFormat.Jpeg;
            else if (fileExtension == "png")
                imgFormat = System.Drawing.Imaging.ImageFormat.Png;
            else if (fileExtension == "tiff")
                imgFormat = System.Drawing.Imaging.ImageFormat.Tiff;
            else {
                imgFormat = defaultImageFormat;

            }

            // Save Image
            Image image = this.Output;

            if (labels != null) {
                // Draw Labels on image
                image = new Bitmap(this.Output);
                DrawLabelsOnImage(image, labels);
            }

            image.Save(fileName, imgFormat);
            image.Dispose();
        }

        public static void DrawLabelsOnImage(Image i, ICollection labels)
        {
            if (labels.Count > 0) {
                Graphics g;

                g = Graphics.FromImage(i);
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

                SolidBrush labelBrush = new SolidBrush(Color.Black);

                foreach (System.Windows.Forms.Label l in labels) {
                    labelBrush.Color = l.ForeColor;

                    Font labelFont;
                    if (l.Font.SizeInPoints != (l.Tag as LabelManager.LabelTag).FontSize)
                        labelFont = new Font(l.Font.FontFamily.Name, (l.Tag as LabelManager.LabelTag).FontSize, l.Font.Style);
                    else
                        labelFont = l.Font;

                    g.DrawString(l.Text, labelFont, labelBrush, (l.Tag as LabelManager.LabelTag).Left, (l.Tag as LabelManager.LabelTag).Top);
                }
            }
        }

        public void FillRegion(Region r, Color fillColor)
        {
            Graphics g = Graphics.FromImage(Output);
            Brush b = new SolidBrush(fillColor);
            Rectangle[] cells = new Rectangle[r.Pixels.Count];
            int i = 0;

            if (this.Grid.ShowMainGrid || this.Grid.ShowSubGrid) {
                foreach (CPoint p in r.Pixels) {
                    cells[i] = GetCellSurface_GridON(p.X, p.Y);
                    i++;
                }
                g.FillRectangles(b, cells);
                //g.DrawRectangle(Pens.Blue, GetRegionBoundariesOnOutputImage(r.Boundaries));
            }
            else {
                foreach (CPoint p in r.Pixels) {
                    cells[i] = GetCellSurface_GridOff(p.X, p.Y);
                    i++;
                }
                g.FillRectangles(b, cells);
            }
        }

        public Rectangle GetRegionBoundariesOnOutputImage(Rectangle regionBoundariesOnInputImage, float zoomScale = 1.0f)
        {
            return new Rectangle(
                (int)((this.BorderSize + (OutputImageZoom * regionBoundariesOnInputImage.X)) * zoomScale),
                (int)((this.BorderSize + (OutputImageZoom * regionBoundariesOnInputImage.Y)) * zoomScale),
                (int)Math.Ceiling((regionBoundariesOnInputImage.Width * OutputImageZoom) * zoomScale),
                (int)Math.Ceiling((regionBoundariesOnInputImage.Height * OutputImageZoom) * zoomScale));
        }

        private Rectangle GetCellSurface_GridOff(int cellX, int cellY)
        {
            Rectangle rect = new Rectangle();

            rect.X = BorderSize + (cellX * OutputImageZoom);
            rect.Y = BorderSize + (cellY * OutputImageZoom);
            rect.Width = OutputImageZoom;
            rect.Height = OutputImageZoom;

            return rect;
        }

        private Rectangle GetCellSurface_GridON(int cellX, int cellY)
        {
            Rectangle rect = new Rectangle();

            rect.X = BorderSize + (cellX * OutputImageZoom);
            rect.Y = BorderSize + (cellY * OutputImageZoom);
            rect.Width = OutputImageZoom - 1;
            rect.Height = OutputImageZoom - 1;

            return rect;
        }

        private int GetColorID(Color color)
        {
            int colorID;
            bool keyFound;

            keyFound = UsedColors.TryGetValue(color.ToArgb(), out colorID);

            if (!keyFound) {
                colorID = (++_colorIdCounter);
                UsedColors.Add(color.ToArgb(), colorID);
            }

            return colorID;
        }

        public int segmentsColumns { get; set; }
    }

}
