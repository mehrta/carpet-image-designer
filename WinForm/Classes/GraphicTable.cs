using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace CrossPlanGenerator
{
    class GraphicTable
    {
        Graphics _g;

        public int Rows { set; get; }
        public int Columns { set; get; }
        public int CellSize { set; get; }
        public Point Location;
        public Color BorderColor { set; get; }
        public int Bottom
        {
            get
            {
                return Location.Y + (Rows * CellSize);
            }
        }

        public GraphicTable(Graphics g)
        {
            _g = g;
        }


        public void Draw()
        {
            Pen pen = new Pen(BorderColor);
            Point p1 = new Point();
            Point p2 = new Point();

            // Draw Horizontal Lines
            p1.X = Location.X;
            p1.Y = Location.Y;
            p2.Y = p1.Y;
            p2.X = Location.X + this.Columns * this.CellSize;

            for (int y = 0; y < Rows + 1; y++) {
                _g.DrawLine(pen, p1, p2);
                p1.Y += this.CellSize;
                p2.Y = p1.Y;
            }

            // Draw Vertical Lines
            p1.X = Location.X;
            p1.Y = Location.Y;
            p2.X = p1.X;
            p2.Y = Location.Y + this.Rows * this.CellSize;

            for (int x = 0; x < Columns + 1; x++) {
                _g.DrawLine(pen, p1, p2);
                p1.X += this.CellSize;
                p2.X = p1.X;
            }
        }

        public void SetCellColor(int row, int col, Color clr)
        {
            _g.FillRectangle(
                new SolidBrush(clr),
                this.Location.X + (col * CellSize) + 1,
                this.Location.Y + (row * CellSize) + 1,
                CellSize,
                CellSize);
        }
    }
}
