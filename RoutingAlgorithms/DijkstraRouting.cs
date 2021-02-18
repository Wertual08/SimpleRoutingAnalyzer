using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRoutingAnalyzer.RoutingAlgorithms
{
    class DijkstraRouting : IRoutingAlgorithm
    {
        public static readonly string Name = "Dijkstra";

        private Graph Graph;
        private int[,] Distances;


        public static void MarkWeights(Graph graph, int[,] weights, int s, int d, int w = 0)
        {
            if (w < weights[s, d]) weights[s, d] = w;
            else return;

            foreach (int node in graph[s])
                MarkWeights(graph, weights, node, d, w + 1);
        }
        public DijkstraRouting(Graph graph)
        {
            Graph = graph;
            Distances = new int[graph.Count, graph.Count];
            for (int s = 0; s < graph.Count; s++)
                for (int d = 0; d < graph.Count; d++)
                    Distances[s, d] = int.MaxValue;

            for (int i = 0; i < graph.Count; i++)
                MarkWeights(graph, Distances, i, i);
        }

        public string Metadata(int node)
        {
            return "";
        }

        public string Metadata()
        {
            return "";
        }

        public int[] Route(RoutingData data)
        {
            int s = data.Source;
            int d = data.Destination;

            var nodes = Graph.Connected(s);

            var res = new List<int>();

            int min = int.MaxValue;
            foreach (var node in nodes)
            {
                int w = Distances[node, d];
                if (w < min)
                {
                    res.Clear();
                    res.Add(node);
                    min = w;
                }
                if (w == min)
                {
                    res.Add(node);
                }
            }

            return res.ToArray();
        }
    }
}
