using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRoutingAnalyzer.RoutingAlgorithms {
    class NoCahceDijkstraRouting : IRoutingAlgorithm {
        public static readonly string Name = "No Cache Dijkstra";

        private Graph graph;
        private int[] weights;
        private bool refresh = true;


        private void ResetWeights() {
            for (int i = 0; i < weights.Length; i++) {
                weights[i] = int.MaxValue;
            }
        }
        private void MarkWeights(int s, int w = 0) {
            if (w < weights[s]) {
                weights[s] = w;

                foreach (int node in graph[s]) {
                    MarkWeights(node, w + 1);
                }
            } 
        }

        public NoCahceDijkstraRouting(Graph graph) {
            this.graph = graph;
            weights = new int[graph.Count];
            Refresh();
        }

        public void Refresh() {
            refresh = true;
        }

        public string Metadata(int node) {
            return "";
        }

        public string Metadata() {
            return "";
        }

        public int[] Route(RoutingData data) {
            int s = data.Source;
            int d = data.Destination;

            if (s == d) return new int[0];

            if (refresh) {
                refresh = false;
                ResetWeights();
                MarkWeights(d);
            }

            var res = new List<int>();

            int min = int.MaxValue;
            foreach (var node in graph[s]) {
                int w = weights[node];
                if (w != int.MaxValue) {
                    if (w < min) {
                        res.Clear();
                        res.Add(node);
                        min = w;
                    } else if (w == min) {
                        res.Add(node);
                    }
                }
            }

            return res.ToArray();
        }
    }
}
