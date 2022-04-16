using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRoutingAnalyzer.RoutingAlgorithms
{
    class HotPotatoRouting : IRoutingAlgorithm
    {
        public static readonly string Name = "Hot Potato";

        private Graph Graph;
        private int Width;
        private int Height;

        public HotPotatoRouting(Graph graph)
        {
            Graph = graph;
            Width = int.Parse(graph["width"]);
            Height = int.Parse(graph["height"]);
        }

        public void Refresh() { }

        public int[] Route(RoutingData data)
        {
            int p = data.Previous;
            int s = data.Source;
            int d = data.Destination;

            if (s == d) return new int[] { d };

            int sx = s % Width;
            int sy = s / Width;

            int dx = d % Width;
            int dy = d / Width;

            var dests = new List<int>();

            if (dx - sx > 0 && sx < Width - 1) dests.Add(sy * Width + sx + 1);
            if (dx - sx < 0 && sx > 0) dests.Add(sy * Width + sx - 1);
            if (dy - sy > 0 && sy < Height - 1) dests.Add((sy + 1) * Width + sx);
            if (dy - sy < 0 && sy > 0) dests.Add((sy - 1) * Width + sx);

            dests.RemoveAll((int node) => !Graph.Enabled[node]);

            if (dests.Count == 0)
            {
                if (dx - sx <= 0 && sx < Width - 1) dests.Add(sy * Width + sx + 1);
                if (dx - sx >= 0 && sx > 0) dests.Add(sy * Width + sx - 1);
                if (dy - sy <= 0 && sy < Height - 1) dests.Add((sy + 1) * Width + sx);
                if (dy - sy >= 0 && sy > 0) dests.Add((sy - 1) * Width + sx);
            }

            if (dests.Count > 1)
            {
                dests.RemoveAll((int node) => node == p);
            }

            return dests.ToArray();
        }

        public string Metadata(int node)
        {
            return null;
        }
        public string Metadata()
        {
            return null;
        }
    }
}
