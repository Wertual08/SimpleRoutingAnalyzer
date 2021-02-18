using SimpleRoutingAnalyzer.RoutingAlgorithms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleRoutingAnalyzer
{
    class GraphPanel : Panel
    {
        private static readonly int NodeSize = 32;
        private static readonly Font NodeFont = new Font("consolas", 16);
        private void DrawNode(Graphics gfx, int size, Color border, Color fill, Point pt, int ind)
        {
            using (var brush = new SolidBrush(border))
                gfx.FillEllipse(brush, pt.X - size / 2, pt.Y - size / 2, size, size);
            using (var brush = new SolidBrush(fill))
                gfx.FillEllipse(brush, pt.X - size / 2 + 2, pt.Y - size / 2 + 2, size - 4, size - 4);
            
            string name = ind.ToString();
            if (Distances != null)
            {
                name = "";
                for (int i = 0; i < Distances.Count; i++)
                {
                    name += Distances[i][ind];
                    if (i < Distances.Count - 1) name += ", ";
                }
            }

            var ind_size = gfx.MeasureString(name, NodeFont);
            if (name.Length > 2) gfx.FillRectangle(Brushes.DarkBlue,
                pt.X - ind_size.Width / 2,
                pt.Y - ind_size.Height / 2,
                ind_size.Width,
                ind_size.Height);
            gfx.DrawString(name, NodeFont, Brushes.DarkGray,
                pt.X - ind_size.Width / 2,
                pt.Y - ind_size.Height / 2);
        }

        private int OffsetX = 64;
        private int OffsetY = 64;
        private float Scale = 1.0f;
        private Point TranslatePt(Point pt)
        {
            return new Point(pt.X + OffsetX, pt.Y + OffsetY);
        }
        private Point ScalePt(Point pt)
        {
            return new Point((int)(pt.X / Scale), (int)(pt.Y / Scale));
        }

        private Graph GraphStorage = null;
        public Graph Graph
        {
            get => GraphStorage;
            set
            {
                SelectedNodes.Clear();
                Source = -1;
                Destination = -1;
                GraphStorage = value;
                Distances = null;
                Refresh();
            }
        }
        private IRoutingAlgorithm AlgorithmStorage = null;
        public IRoutingAlgorithm Algorithm
        {
            get => AlgorithmStorage;
            set
            {
                AlgorithmStorage = value;
                Refresh();
            }
        }
        public int Source { get; set; } = -1;
        public int Destination { get; set; } = -1;
        public bool ShowDistances
        {
            get => Distances != null;
            set
            {
                if (value && Graph != null && SelectedNodes.Count > 0)
                {
                    Distances = new List<int[]>(SelectedNodes.Count);

                    foreach (var sel in SelectedNodes)
                    {
                        var dists = new int[Graph.Count];
                        Metrics.InitWeights(dists);
                        Metrics.MarkWeights(Graph, dists, sel);
                        Distances.Add(dists);
                    }
                }
                else Distances = null;
                Refresh();
            }
        }
        private List<int[]> Distances = null;

        private void GetPossibleHops(HashSet<Tuple<int, int>> hops, int p, int s, int d)
        {
            if (Algorithm != null && s != -1 && d != -1)
            {
                var data = new RoutingData();
                data.Previous = p;
                data.Source = s;
                data.Destination = d;

                var nodes = Algorithm.Route(data);
                foreach (var node in nodes)
                {
                    var hop = new Tuple<int, int>(s, node);
                    if (!hops.Contains(hop))
                    {
                        hops.Add(hop);
                        GetPossibleHops(hops, s, node, d);
                    }
                }
            }
        }

        public GraphPanel()
        {
            DoubleBuffered = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.ScaleTransform(Scale, Scale);
            var hops = new HashSet<Tuple<int, int>>();
            GetPossibleHops(hops, -1, Source, Destination);
            

            if (Graph != null)
            {
                for (int s = 0; s < Graph.Count; s++)
                {
                    for (int d = 0; d < Graph.Count; d++)
                    {
                        if (Graph[s, d] == Graph.None) continue;

                        var color = Color.Black;
                        using (var big_arrow = new AdjustableArrowCap(4, 3))
                        using (var pen = new Pen(color, 2))
                        {
                            Point beg = TranslatePt(Graph.Points[s]);
                            Point end = TranslatePt(Graph.Points[d]);

                            int dx = end.X - beg.X;
                            int dy = end.Y - beg.Y;
                            int len = (int)Math.Sqrt(dx * dx + dy * dy);

                            //beg.X += dx / 2;
                            //beg.Y += dy / 2;
                            if (len > 0) beg.X += NodeSize * dx / len / 2;
                            if (len > 0) beg.Y += NodeSize * dy / len / 2;
                            if (len > 0) end.X -= NodeSize * dx / len / 2;
                            if (len > 0) end.Y -= NodeSize * dy / len / 2;

                            pen.CustomEndCap = big_arrow;
                            e.Graphics.DrawLine(pen, beg, end);
                        }
                    }
                }
                foreach (var path in hops)
                {
                    int s = path.Item1;
                    int d = path.Item2;

                    var color = Color.Cyan;
                    using (var big_arrow = new AdjustableArrowCap(4, 3))
                    using (var pen = new Pen(color, 2))
                    {
                        Point beg = TranslatePt(Graph.Points[s]);
                        Point end = TranslatePt(Graph.Points[d]);

                        int dx = end.X - beg.X;
                        int dy = end.Y - beg.Y;
                        int len = (int)Math.Sqrt(dx * dx + dy * dy);

                        if (len > 0) beg.X += NodeSize * dx / len / 2;
                        if (len > 0) beg.Y += NodeSize * dy / len / 2;
                        if (len > 0) end.X -= NodeSize * dx / len / 2;
                        if (len > 0) end.Y -= NodeSize * dy / len / 2;

                        pen.CustomEndCap = big_arrow;
                        e.Graphics.DrawLine(pen, beg, end);
                    }
                }

                for (int s = 0; s < Graph.Count; s++)
                {
                    Color fill = Color.DarkBlue;
                    if (s == Source) fill = Color.Red;
                    if (s == Destination) fill = Color.Green;
                    if (!Graph.Enabled[s]) fill = Color.Gray;
                    Color border = Color.Black;
                    if (SelectedNodes.Contains(s)) border = Color.LightBlue;

                    Point pt = TranslatePt(Graph.Points[s]);

                    DrawNode(e.Graphics, NodeSize, border, fill, pt, s);
                }
            }

            if (DragAction == DragActionType.Select)
            {
                using (var brush = new SolidBrush(Color.FromArgb(64, Color.SkyBlue)))
                    e.Graphics.FillRectangle(brush, DragRegion);
                e.Graphics.DrawRectangle(Pens.LightBlue, DragRegion);
            }

            base.OnPaint(e);
        }

        private enum DragActionType
        {
            None, 
            DragScreen,
            DragNode,
            Select,
        }
        private DragActionType DragAction;
        private HashSet<int> SelectedNodes = new HashSet<int>();
        private Point FirstMouse, LastMouse;
        private Rectangle DragRegion
        {
            get
            {
                int lx = FirstMouse.X;
                int dy = FirstMouse.Y;
                int rx = LastMouse.X;
                int uy = LastMouse.Y;
                if (lx > rx)
                {
                    int t = lx;
                    lx = rx;
                    rx = t;
                }
                if (dy > uy)
                {
                    int t = dy;
                    dy = uy;
                    uy = t;
                }
                return Rectangle.FromLTRB(lx, dy, rx, uy);
            }
        }


        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (e.Delta > 0) Scale *= 2;
            if (e.Delta < 0) Scale /= 2;
            Refresh();
            base.OnMouseWheel(e);
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (Graph != null)
            {
                int sel = -1;
                int rad = NodeSize * NodeSize / 4;
                for (int i = 0; i < Graph.Points.Length; i++)
                {
                    var node = Graph.Points[i];
                    int dx = ScalePt(e.Location).X - (OffsetX + node.X);
                    int dy = ScalePt(e.Location).Y - (OffsetY + node.Y);
                    if (dx * dx + dy * dy <= rad)
                    {
                        sel = i;
                        break;
                    }
                }

                if (!ModifierKeys.HasFlag(Keys.Control) && sel < 0) SelectedNodes.Clear();
                if (!ModifierKeys.HasFlag(Keys.Control) && !SelectedNodes.Contains(sel)) SelectedNodes.Clear();
                if (ModifierKeys.HasFlag(Keys.Control) && SelectedNodes.Contains(sel)) SelectedNodes.Remove(sel);
                else if (sel >= 0) SelectedNodes.Add(sel);

                if (SelectedNodes.Count > 0)
                {
                    if (e.Button.HasFlag(MouseButtons.Middle))
                        DragAction = DragActionType.DragNode;
                }
                else
                {
                    if (e.Button.HasFlag(MouseButtons.Left))
                        DragAction = DragActionType.Select;
                    else DragAction = DragActionType.DragScreen;
                }

                FirstMouse = ScalePt(e.Location);
                LastMouse = ScalePt(e.Location);
                Refresh();
            }
            base.OnMouseDown(e);
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            int dx = ScalePt(e.Location).X - LastMouse.X;
            int dy = ScalePt(e.Location).Y - LastMouse.Y;
            LastMouse = ScalePt(e.Location);

            switch (DragAction)
            {
                case DragActionType.DragScreen:
                    OffsetX += dx;
                    OffsetY += dy;
                    break;
                case DragActionType.DragNode:
                    foreach (int i in SelectedNodes)
                    { 
                        Graph.Points[i].X += dx;
                        Graph.Points[i].Y += dy;
                    }
                    break;
                case DragActionType.Select:
                    for (int i = 0; i < Graph.Count; i++)
                    {
                        int x = Graph.Points[i].X + OffsetX;
                        int y = Graph.Points[i].Y + OffsetY;
                        if (DragRegion.Contains(x, y))
                            SelectedNodes.Add(i);
                        else SelectedNodes.Remove(i);
                    }
                    break;
            }
            if (DragAction != DragActionType.None) Refresh();
            base.OnMouseMove(e);
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            DragAction = DragActionType.None;
            LastMouse = ScalePt(e.Location);
            Refresh();
            OnSelectionChanged(EventArgs.Empty);
            base.OnMouseUp(e);
        }
        protected override void OnDoubleClick(EventArgs e)
        {
            ToggleSelected();
            base.OnDoubleClick(e);
        }

        protected virtual void OnSelectionChanged(EventArgs e)
        {
            SelectionChanged?.Invoke(this, e);
        }

        public int[] Selected => SelectedNodes.ToArray(); 
        public event EventHandler SelectionChanged;

        public void ToggleSelected()
        {
            foreach (int i in SelectedNodes)
                Graph.Enabled[i] = !Graph.Enabled[i];
            Refresh();
        }
        public void SourceSelected()
        {
            if (SelectedNodes.Count > 0)
                Source = SelectedNodes.First();
            else Source = -1;
            Refresh();
        }
        public void DestinationSelected()
        {
            if (SelectedNodes.Count > 0)
                Destination = SelectedNodes.First();
            else Destination = -1;
            Refresh();
        }
    }
}
