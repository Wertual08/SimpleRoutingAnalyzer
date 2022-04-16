using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRoutingAnalyzer.RoutingAlgorithms {
    class CirculantGreedyPromotionRouting : IRoutingAlgorithm {
        public static readonly string Name = "Circulant Greedy Promotion";

        private readonly int[,] Coordinates;
        private readonly Graph Graph;
        private readonly List<int> Generators;

        private static int[] Solve(int src, int dst, int count, List<int> generators) {
            int sd = dst - src;

            int n_bounds = (int)Math.Sqrt(count);
            int g_bounds = 2 * n_bounds;

            int n = -n_bounds;
            int[] vector = new int[generators.Count];
            for (int i = 0; i < vector.Length; i++)
                vector[i] = -g_bounds;

            int min_sum = int.MaxValue;
            int[] result = null;


            do {
                int eq = 0;
                eq += n * count;
                for (int i = 0; i < vector.Length; i++) {
                    eq += vector[i] * generators[i];
                }

                int sum = vector.Sum((int v) => Math.Abs(v));

                if (eq == sd) {
                    if (sum < min_sum) {
                        result = new int[vector.Length + 1];
                        result[0] = n;
                        vector.CopyTo(result, 1);
                        min_sum = sum;
                    }
                }

                vector[0]++;
                for (int i = 0; i < vector.Length - 1; i++) {
                    if (vector[i] > g_bounds) {
                        vector[i + 1]++;
                        vector[i] = -g_bounds;
                    }
                }
                if (vector[vector.Length - 1] > g_bounds) {
                    vector[vector.Length - 1] = -g_bounds;
                    n++;
                }
            }
            while (n <= n_bounds);

            return result;
        }

        public CirculantGreedyPromotionRouting(Graph graph) {
            Graph = graph;

            Generators = new List<int>(int.Parse(graph["count"]));
            for (int i = 0; i < Generators.Capacity; i++) {
                Generators.Add(int.Parse(graph[$"g{i}"]));
            } 

            Coordinates = new int[graph.Count, Generators.Count + 1];
            for (int i = 0; i < graph.Count; i++) {
                var coords = Solve(0, i, graph.Count, Generators);
                if (coords == null) {
                    throw new Exception("Unable to find coordinates.");
                }

                for (int j = 0; j < coords.Length; j++) {
                    Coordinates[i, j] = coords[j];
                }
            }
        }

        public void Refresh() { }

        public string Metadata(int node) {
            var builder = new StringBuilder();
            builder.Append("{ ");

            for (int i = 0; i < Coordinates.GetLength(1); i++) {
                builder.Append(Coordinates[node, i]);
                if (i < Coordinates.GetLength(1) - 1) builder.Append(", ");
            }
            builder.Append(" }");

            return builder.ToString();
        }

        public string Metadata() {
            return "";
        }

        public int[] Route(RoutingData data) {
            int p = data.Previous;
            int s = data.Source;
            int d = data.Destination;

            if (s == d) {
                return new int[] { d };
            }


            var result = new List<int>(2);

            int index = d - s;
            if (index < 0) {
                index += Graph.Count;
            }
            for (int i = 1; i < Coordinates.GetLength(1); i++) {
                int steps = Coordinates[index, i];
                int next = -1;
                if (steps > 0) {
                    next = (s + Generators[i - 1]) % Graph.Count;
                }
                if (steps < 0) {
                    next = s - Generators[i - 1];
                    if (next < 0) {
                        next += Graph.Count;
                    }
                }
                if (next >= 0 && Graph.Enabled[next] && next != p) {
                    result.Add(next);
                }
            }

            if (result.Count == 0) {
                for (int i = 1; i < Coordinates.GetLength(1); i++) {
                    int steps = Coordinates[index, i];
                    int next = -1;
                    int ind = i % Generators.Count;
                    if (steps > 0) {
                        next = (s + Generators[ind]) % Graph.Count;
                    }
                    if (steps < 0) {
                        next = s - Generators[ind];
                        if (next < 0) {
                            next += Graph.Count;
                        }
                    }
                    if (next >= 0 && Graph.Enabled[next]) {
                        result.Add(next);
                    }
                }
            }

            return result.ToArray();
        }
    }
}
