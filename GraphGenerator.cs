using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRoutingAnalyzer
{
    static class GraphGenerator
    {
        public static Graph Mesh(int w, int h)
        {
            var graph = new Graph(w * h);

            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    int s = y * w + x;

                    graph.Points[s] = new Point(x * 64, y * 64);

                    if (y > 0) graph[s, (y - 1) * w + x] = 1;
                    if (x > 0) graph[s, y * w + x - 1] = 1;
                    if (y < h - 1) graph[s, (y + 1) * w + x] = 1;
                    if (x < w - 1) graph[s, y * w + x + 1] = 1;
                }
            }

            graph["width"] = w.ToString();
            graph["height"] = h.ToString();

            return graph;
        }

        public static Graph Circulant(int n, int[] gens)
        {
            var graph = new Graph(n);

            foreach (int g in gens)
            {
                for (int s = 0; s < n; s++)
                {
                    int d = (s + g) % n;
                    graph[s, d] = 1;
                    graph[d, s] = 1;
                }
            }

            double r = n * 64 / (2.0 * Math.PI);
            for (int s = 0; s < n; s++)
            {
                double a = (double)s / n * 2.0 * Math.PI;
                int x = (int)(r * Math.Cos(a));
                int y = (int)(r * Math.Sin(a));
                graph.Points[s] = new Point(x, y);
            }

            graph["count"] = gens.Length.ToString();
            for (int i = 0; i < gens.Length; i++)
                graph[$"g{i}"] = gens[i].ToString();

            return graph;
        }
    }
}
