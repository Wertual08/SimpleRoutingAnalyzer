using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRoutingAnalyzer.RoutingAlgorithms
{
    class OFTRouting : IRoutingAlgorithm
    {
        public static readonly string Name = "OFT";

        private Graph Graph;
        private int Width;
        private int Height;

        public OFTRouting(Graph graph)
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

            int t = (sy + 1) * Width + sx;
            int l = sy * Width + sx - 1;
            int b = (sy - 1) * Width + sx;
            int r = sy * Width + sx + 1;

            if (dx - sx < 0 && sx > 1 && Graph.Enabled[l] && l != p) 
                dests.Add(l);
            else if (dy - sy < 0 && sy > 1 && Graph.Enabled[b] && b != p) 
                dests.Add(b);
            else if (dx - sx < 0 && Graph.Enabled[l] && l != p) 
                dests.Add(l);
            else if (dy - sy < 0 && Graph.Enabled[b] && b != p)
                dests.Add(b);
            else if (dx - sx < 0 && sy == 0 && Graph.Enabled[t]) 
                dests.Add(t);
            else if (dy - sy < 0 && sx == 0 && Graph.Enabled[r]) 
                dests.Add(r);
            // ------------------ //
            else if (dx - sx > 1 && Graph.Enabled[r])
                dests.Add(r);
            else if (dy - sy > 1 && Graph.Enabled[t])
                dests.Add(t);
            else if (dx - sx > 0 && Graph.Enabled[r])
                dests.Add(r);
            else if (dy - sy > 0 && Graph.Enabled[t])
                dests.Add(t);
            else if (dx - sx > 0 && sy == 0 && Graph.Enabled[t])
                dests.Add(t);
            else if (dy - sy > 0 && sx == 0 && Graph.Enabled[r])
                dests.Add(r);
            else if (dx - sx == 0 && sx > 0)
                dests.Add(l);
            else if (dy - sy == 0 && sy > 0)
                dests.Add(b);
                
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
