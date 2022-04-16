using SimpleRoutingAnalyzer.RoutingAlgorithms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleRoutingAnalyzer {
    class GraphPanel : Panel {
        private NodesRenderer Renderer = new NodesRenderer();

        private int OffsetX = 64;
        private int OffsetY = 64;
        private float GraphScale = 1.0f;
        private Point TranslatePt(Point pt) {
            return new Point(pt.X + OffsetX, pt.Y + OffsetY);
        }
        private Point ScalePt(Point pt) {
            return new Point((int)(pt.X / GraphScale), (int)(pt.Y / GraphScale));
        }

        private Graph GraphStorage = null;
        public Graph Graph {
            get => GraphStorage;
            set {
                SelectedNodes.Clear();
                Source = -1;
                Destination = -1;
                GraphStorage = value;
                Distances = null;
                Refresh();
            }
        }
        private IRoutingAlgorithm AlgorithmStorage = null;
        public IRoutingAlgorithm Algorithm {
            get => AlgorithmStorage;
            set {
                AlgorithmStorage = value;
                Refresh();
            }
        }
        public int Source { get; set; } = -1;
        public int Destination { get; set; } = -1;
        public bool ShowLabels { get; set; }
        public bool ShowDistances {
            get => Distances != null;
            set {
                if (value && Graph != null && SelectedNodes.Count > 0) {
                    Distances = new List<int[]>(SelectedNodes.Count);

                    foreach (var sel in SelectedNodes) {
                        var dists = new int[Graph.Count];
                        //Metrics.InitWeights(dists);
                        //Metrics.MarkWeights(Graph, dists, sel);
                        Distances.Add(dists);
                    }
                } else Distances = null;
                Refresh();
            }
        }
        private List<int[]> Distances = null;
        private string[] MetadataStorage = null;
        public string[] Metadata {
            get => MetadataStorage;
            set {
                MetadataStorage = value;
                Refresh();
            }
        }


        private void GetPossibleHops(HashSet<(int, int)> hops, int s, int d, int p = -1) {
            if (Algorithm != null && s != -1 && d != -1) {
                var data = new RoutingData();
                data.Previous = p;
                data.Source = s;
                data.Destination = d;

                var nodes = Algorithm.Route(data);
                foreach (var node in nodes) {
                    var hop = (s, node);
                    if (!hops.Contains(hop)) {
                        hops.Add(hop);
                        GetPossibleHops(hops, node, d, s);
                    }
                }
            }
        }

        public GraphPanel() {
            DoubleBuffered = true;
        }

        protected override void OnPaint(PaintEventArgs e) {
            e.Graphics.ScaleTransform(GraphScale, GraphScale);
            var hops = new HashSet<(int, int)>();
            GetPossibleHops(hops, Source, Destination);


            if (Graph != null) {
                for (int s = 0; s < Graph.Count; s++) {
                    for (int d = 0; d < Graph.Count; d++) {
                        if (Graph[s, d] == Graph.None || hops.Contains((s, d)) || hops.Contains((d, s))) {
                            continue;
                        }

                        Point beg = TranslatePt(Graph.Points[s]);
                        Point end = TranslatePt(Graph.Points[d]);

                        Renderer.DrawLink(e.Graphics, beg, end);
                    }
                }

                foreach (var path in hops) {
                    int s = path.Item1;
                    int d = path.Item2;

                    var color = Color.Cyan;
                    using (var big_arrow = new AdjustableArrowCap(4, 3))
                    using (var pen = new Pen(color, 2)) {
                        Point beg = TranslatePt(Graph.Points[s]);
                        Point end = TranslatePt(Graph.Points[d]);

                        Renderer.DrawPath(e.Graphics, beg, end);
                    }
                }

                for (int s = 0; s < Graph.Count; s++) {
                    Point pt = TranslatePt(Graph.Points[s]);

                    string name = s.ToString();
                    if (Metadata != null) {
                        name = $"{s}: {Metadata[s]}";
                    } else if (Distances != null) {
                        name = "";
                        for (int i = 0; i < Distances.Count; i++) {
                            name += Distances[i][s];
                            if (i < Distances.Count - 1) name += ", ";
                        }
                    }
                    Renderer.DrawLabel(e.Graphics, pt, name);

                    if (SelectedNodes.Contains(s)) {
                        Renderer.DrawSelection(e.Graphics, pt);
                    }
                    if (s == Source) {
                        Renderer.DrawSource(e.Graphics, pt);
                    } else if (s == Destination) {
                        Renderer.DrawDestination(e.Graphics, pt);
                    } else if (!Graph.Enabled[s]) {
                        Renderer.DrawDisabled(e.Graphics, pt);
                    } else {
                        Renderer.DrawDefault(e.Graphics, pt);
                    }
                }
            }

            if (DragAction == DragActionType.Select) {
                using (var brush = new SolidBrush(Color.FromArgb(64, Color.SkyBlue))) {
                    e.Graphics.FillRectangle(brush, DragRegion);
                }
                e.Graphics.DrawRectangle(Pens.LightBlue, DragRegion);
            }

            base.OnPaint(e);
        }

        private enum DragActionType {
            None,
            DragScreen,
            DragNode,
            Select,
        }
        private DragActionType DragAction;
        private HashSet<int> SelectedNodes = new HashSet<int>();
        private Point FirstMouse, LastMouse;
        private Rectangle DragRegion {
            get {
                int lx = FirstMouse.X;
                int dy = FirstMouse.Y;
                int rx = LastMouse.X;
                int uy = LastMouse.Y;
                if (lx > rx) {
                    int t = lx;
                    lx = rx;
                    rx = t;
                }
                if (dy > uy) {
                    int t = dy;
                    dy = uy;
                    uy = t;
                }
                return Rectangle.FromLTRB(lx, dy, rx, uy);
            }
        }


        protected override void OnMouseWheel(MouseEventArgs e) {
            if (e.Delta > 0) {
                GraphScale *= 1.1f;
            }
            if (e.Delta < 0) {
                GraphScale /= 1.1f;
            }
            Refresh();
            base.OnMouseWheel(e);
        }
        protected override void OnMouseDown(MouseEventArgs e) {
            if (Graph != null) {
                int sel = -1;
                int rad = Renderer.NodeSize * Renderer.NodeSize / 4;
                for (int i = 0; i < Graph.Points.Length; i++) {
                    var node = Graph.Points[i];
                    int dx = ScalePt(e.Location).X - (OffsetX + node.X);
                    int dy = ScalePt(e.Location).Y - (OffsetY + node.Y);
                    if (dx * dx + dy * dy <= rad) {
                        sel = i;
                        break;
                    }
                }

                if (!ModifierKeys.HasFlag(Keys.Control) && sel < 0) SelectedNodes.Clear();
                if (!ModifierKeys.HasFlag(Keys.Control) && !SelectedNodes.Contains(sel)) SelectedNodes.Clear();
                if (ModifierKeys.HasFlag(Keys.Control) && SelectedNodes.Contains(sel)) SelectedNodes.Remove(sel);
                else if (sel >= 0) SelectedNodes.Add(sel);

                if (e.Button.HasFlag(MouseButtons.Middle)) {
                    if (SelectedNodes.Count > 0) {
                        DragAction = DragActionType.DragNode;
                    }
                } else if (e.Button.HasFlag(MouseButtons.Left)) {
                    DragAction = DragActionType.Select;
                } else {
                    DragAction = DragActionType.DragScreen;
                }

                FirstMouse = ScalePt(e.Location);
                LastMouse = ScalePt(e.Location);
                Refresh();
            }
            base.OnMouseDown(e);
        }
        protected override void OnMouseMove(MouseEventArgs e) {
            int dx = ScalePt(e.Location).X - LastMouse.X;
            int dy = ScalePt(e.Location).Y - LastMouse.Y;
            LastMouse = ScalePt(e.Location);

            switch (DragAction) {
                case DragActionType.DragScreen:
                    OffsetX += dx;
                    OffsetY += dy;
                    break;
                case DragActionType.DragNode:
                    foreach (int i in SelectedNodes) {
                        Graph.Points[i].X += dx;
                        Graph.Points[i].Y += dy;
                    }
                    break;
                case DragActionType.Select:
                    for (int i = 0; i < Graph.Count; i++) {
                        int x = Graph.Points[i].X + OffsetX;
                        int y = Graph.Points[i].Y + OffsetY;
                        if (DragRegion.Contains(x, y))
                            SelectedNodes.Add(i);
                        else if (!ModifierKeys.HasFlag(Keys.Control))
                            SelectedNodes.Remove(i);
                    }
                    break;
            }
            if (DragAction != DragActionType.None) Refresh();
            base.OnMouseMove(e);
        }
        protected override void OnMouseUp(MouseEventArgs e) {
            DragAction = DragActionType.None;
            LastMouse = ScalePt(e.Location);
            Refresh();
            OnSelectionChanged(EventArgs.Empty);
            base.OnMouseUp(e);
        }
        protected override void OnDoubleClick(EventArgs e) {
            ToggleSelected();
            base.OnDoubleClick(e);
        }

        protected virtual void OnSelectionChanged(EventArgs e) {
            SelectionChanged?.Invoke(this, e);
        }

        public int[] Selected => SelectedNodes.ToArray();
        public event EventHandler SelectionChanged;

        public void ToggleSelected() {
            foreach (int i in SelectedNodes) {
                Graph.Enabled[i] = !Graph.Enabled[i];
            }
            Algorithm.Refresh();
            Refresh();
        }
        public void SourceSelected() {
            if (SelectedNodes.Count > 0) {
                Source = SelectedNodes.First();
            } else {
                Source = -1;
            }
            Refresh();
        }
        public void DestinationSelected() {
            if (SelectedNodes.Count > 0) {
                Destination = SelectedNodes.First();
            } else {
                Destination = -1;
            }
            Refresh();
        }


        public Bitmap Export() {
            var controlBitmap = new Bitmap(ClientSize.Width, ClientSize.Height);
            DrawToBitmap(controlBitmap, new Rectangle(new Point(), ClientSize));


            int lx = Graph.Points.Min(pt => (int)(TranslatePt(pt).X * GraphScale));
            int rx = Graph.Points.Max(pt => (int)(TranslatePt(pt).X * GraphScale));
            int dy = Graph.Points.Min(pt => (int)(TranslatePt(pt).Y * GraphScale));
            int uy = Graph.Points.Max(pt => (int)(TranslatePt(pt).Y * GraphScale));

            lx = Math.Max(0, lx - (int)(32 * GraphScale));
            dy = Math.Max(0, dy - (int)(32 * GraphScale));
            rx = Math.Min(ClientSize.Width, rx + (int)(32 * GraphScale));
            uy = Math.Min(ClientSize.Height, uy + (int)(32 * GraphScale));
            var bounds = new Rectangle(lx, dy, rx - lx, uy - dy);

            return controlBitmap.Clone(bounds, PixelFormat.Format32bppArgb);
        }
    }
}
