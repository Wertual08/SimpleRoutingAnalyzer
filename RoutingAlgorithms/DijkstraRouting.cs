﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRoutingAnalyzer.RoutingAlgorithms {
    class DijkstraRouting : IRoutingAlgorithm {
        public static readonly string Name = "Dijkstra";

        private Graph graph;
        private int[,] weights;


        public void MarkWeights(int s, int d, int w = 0) {
            if (w < weights[s, d]) {
                weights[s, d] = w;

                foreach (int node in graph[s]) {
                    MarkWeights(node, d, w + 1);
                }
            } 
        }
        public DijkstraRouting(Graph graph) {
            this.graph = graph;
            weights = new int[graph.Count, graph.Count];
            Refresh();
        }

        public void Refresh() {
            for (int s = 0; s < graph.Count; s++) {
                for (int d = 0; d < graph.Count; d++) {
                    weights[s, d] = int.MaxValue;
                }
            }

            for (int i = 0; i < graph.Count; i++) {
                MarkWeights(i, i);
            }
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

            var nodes = graph[s];
            var res = new List<int>();

            int min = int.MaxValue;
            foreach (var node in nodes) {
                int w = weights[node, d];
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
