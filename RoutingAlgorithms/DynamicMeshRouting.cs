using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRoutingAnalyzer.RoutingAlgorithms {
    class DynamicMeshRouting : IRoutingAlgorithm {
        public static readonly string Name = "Dynamic Mesh";

        private Graph Graph;
        private int Width;
        private int Height;

        private List<int> RegularOutputs(int s, int d) {
            var result = new List<int>(2);
            int sx = s % Width;
            int sy = s / Width;
            int dx = d % Width;
            int dy = d / Width;

            if (dy > sy) {
                result.Add(sy + Width);
            }
            if (dx < sx) {
                result.Add(sx - 1);
            }
            if (dy < sy) {
                result.Add(sy - Width);
            }
            if (dx > sx) {
                result.Add(sx + 1);
            }
            return result;
        }
        private List<int> Perpendicular(int s, int d) {
            var result = new List<int>(2);

            int sx = s % Width;
            int sy = s / Width;
            int dx = d % Width;
            int dy = d / Width;

            if (sx == dx) {
                if (sx > 0) {
                    result.Add(sx - 1);
                }
                if (sx < Width - 1) {
                    result.Add(sx + 1);
                }
            }

            if (sy == dy) {
                if (sy > 0) {
                    result.Add(sx - Width);
                }
                if (sy < Height - 1) {
                    result.Add(sx + Width);
                }
            }

            return result;
        }

        public DynamicMeshRouting(Graph graph) {
            Graph = graph;
            Width = int.Parse(graph["width"]);
            Height = int.Parse(graph["height"]);
        }

        public void Refresh() { }

        public int[] Route(RoutingData data) {
            var dests = RegularOutputs(data.Source, data.Destination);

            var result = new List<int>(2);
            if (dests.Count == 1) {
                int dest = dests[0];

                if (Graph.Enabled[dest]) {
                    result.Add(dest);
                } else {
                    var perpendiculars = Perpendicular(data.Source, dest);
                    foreach (int p in perpendiculars) {
                        if (Graph.Enabled[p]) {
                            result.Add(p);
                        }
                    }
                }
            }
            else if (dests.Count == 2) {
                foreach (int dest in dests) {
                    if (Graph.Enabled[dest]) {
                        result.Add(dest);
                    }
                }
            }

            return null;
        }

        public string Metadata(int node) {
            return "";
        }

        public string Metadata() {
            return "";
        }
    }
}
