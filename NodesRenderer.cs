using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace SimpleRoutingAnalyzer {
    class NodesRenderer {
        public int NodeSize { get; set; } = 20;
        public Font NodeFont { get; set; } = new Font("consolas", 10);

        private Color backgroundColor = Color.White;
        private Brush backgroundBrush = new SolidBrush(Color.White);
        private Pen backgroundPen = new Pen(new SolidBrush(Color.White), 3);
        private Pen backgroundPenCap = new Pen(new SolidBrush(Color.White), 3) { CustomEndCap = new AdjustableArrowCap(4, 3) };
        private Color foregroundColor = Color.Black;
        private Brush foregroundBrush = new SolidBrush(Color.Black);
        private Pen foregroundPen = new Pen(new SolidBrush(Color.DarkGray), 1);
        private Pen foregroundPenCap = new Pen(new SolidBrush(Color.Black), 3) { CustomEndCap = new AdjustableArrowCap(4, 3) };
        private Pen selectionPen = new Pen(Brushes.LightBlue, 5);

        public Color Background {
            get => backgroundColor;
            set {
                backgroundColor = value;
                backgroundPenCap.Dispose();
                backgroundPen?.Dispose();
                backgroundBrush?.Dispose();
                backgroundBrush = new SolidBrush(value);
                backgroundPen = new Pen(backgroundBrush, 2);
                backgroundPenCap = new Pen(backgroundBrush, 2) { CustomEndCap = new AdjustableArrowCap(4, 3) };
            }
        }
        public Color Foreground {
            get => foregroundColor;
            set {
                foregroundColor = value;
                foregroundPenCap.Dispose();
                foregroundPen?.Dispose();
                foregroundBrush?.Dispose();
                foregroundBrush = new SolidBrush(value);
                foregroundPen = new Pen(backgroundBrush, 2);
                foregroundPenCap = new Pen(backgroundBrush, 2) { CustomEndCap = new AdjustableArrowCap(4, 3) };
            }
        } 

        public void DrawSelection(Graphics gfx, Point pt) {
            gfx.DrawEllipse(selectionPen, pt.X - NodeSize / 2, pt.Y - NodeSize / 2, NodeSize, NodeSize);
        }

        public void DrawDefault(Graphics gfx, Point pt) {
             gfx.FillEllipse(foregroundBrush, pt.X - NodeSize / 2, pt.Y - NodeSize / 2, NodeSize, NodeSize);
        }

        public void DrawDisabled(Graphics gfx, Point pt) {
            gfx.FillEllipse(foregroundBrush, pt.X - NodeSize / 2, pt.Y - NodeSize / 2, NodeSize, NodeSize);

            gfx.FillEllipse(backgroundBrush, pt.X - NodeSize / 2 + 4, pt.Y - NodeSize / 2 + 4, NodeSize - 8, NodeSize - 8);
        }

        public void DrawSource(Graphics gfx, Point pt) {
            gfx.FillEllipse(foregroundBrush, pt.X - NodeSize / 2, pt.Y - NodeSize / 2, NodeSize, NodeSize);
            gfx.DrawLine(backgroundPen, pt.X - NodeSize / 2, pt.Y - NodeSize / 2, pt.X + NodeSize / 2, pt.Y + NodeSize / 2);
            gfx.DrawLine(backgroundPen, pt.X + NodeSize / 2, pt.Y - NodeSize / 2, pt.X - NodeSize / 2, pt.Y + NodeSize / 2);
        }

        public void DrawDestination(Graphics gfx, Point pt) {
            gfx.FillEllipse(foregroundBrush, pt.X - NodeSize / 2, pt.Y - NodeSize / 2, NodeSize, NodeSize);
            gfx.DrawLine(backgroundPen, pt.X - NodeSize / 2, pt.Y, pt.X + NodeSize / 2, pt.Y);
            gfx.DrawLine(backgroundPen, pt.X, pt.Y - NodeSize / 2, pt.X, pt.Y + NodeSize / 2);
        }

        public void DrawLink(Graphics gfx, Point beg, Point end) {
            int dx = end.X - beg.X;
            int dy = end.Y - beg.Y;
            int len = (int)Math.Sqrt(dx * dx + dy * dy);
            
            if (len > 0) beg.X += NodeSize * dx / len / 2;
            if (len > 0) beg.Y += NodeSize * dy / len / 2;
            if (len > 0) end.X -= NodeSize * dx / len / 2;
            if (len > 0) end.Y -= NodeSize * dy / len / 2;
            
            gfx.DrawLine(foregroundPen, beg, end);
        }

        public void DrawPath(Graphics gfx, Point beg, Point end) {
            int dx = end.X - beg.X;
            int dy = end.Y - beg.Y;
            int len = (int)Math.Sqrt(dx * dx + dy * dy);

            if (len > 0) beg.X += NodeSize * dx / len / 2;
            if (len > 0) beg.Y += NodeSize * dy / len / 2;
            if (len > 0) end.X -= NodeSize * dx / len / 2;
            if (len > 0) end.Y -= NodeSize * dy / len / 2;

            gfx.DrawLine(foregroundPenCap, beg, end);
        }

        public void DrawLabel(Graphics gfx, Point pt, string text) {
            var size = gfx.MeasureString(text, NodeFont);
            gfx.FillRectangle(
                backgroundBrush,
                pt.X - size.Width / 2,
                pt.Y + NodeSize / 2,
                size.Width,
                size.Height
            );
            gfx.DrawString(
                text, 
                NodeFont, 
                foregroundBrush,
                pt.X - size.Width / 2,
                pt.Y + NodeSize / 2
            );
        }
    }
}
