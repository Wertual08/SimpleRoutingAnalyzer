using SimpleRoutingAnalyzer.RoutingAlgorithms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SimpleRoutingAnalyzer.Metrics {
    class StressMetrics : Metrics {
        private readonly Graph graph;
        private readonly IRoutingAlgorithm algorithm;
        private readonly double[,] lengths;
        private readonly Random random;
        private int toDisable = -1;

        protected override int[] Route(int s, int d, int p) {
            return algorithm.Route(new RoutingData {
                Source = s,
                Destination = d,
                Previous = p,
            });
        }

        public StressMetrics(Graph graph, IRoutingAlgorithm algorithm, int seed) {
            this.graph = graph;
            this.algorithm = algorithm;
            lengths = new double[graph.Count, graph.Count];
            random = new Random(seed);

            InitHops(graph.Count);

            for (int i = 0; i < graph.Enabled.Length; i++) {
                graph.Enabled[i] = true;
            }
            for (int s = 0; s < graph.Count; s++) {
                for (int d = 0; d < graph.Count; d++) {
                    lengths[s, d] = 0;
                }
            }
            algorithm.Refresh();
        }

        public int Target { get; private set; } = -1;
        public double ValidRoutesTotal { get; private set; }
        public double ValidRoutes { get; private set; }
        public double DeadEndRoutes { get; private set; }
        public double AccumulatedDelta { get; set; } = 0;
        public double AverageRouteLength { get; set; }

        public override int IterationsTotal => graph.Count + 1;
        private int iteration = -1;
        public override int Iteration { get => iteration; }
        public override Dictionary<string, string> State() { 
            return new Dictionary<string, string> {
                { "Disabled", $"{Target:000}" },
                { "Valid routes total", $"{ValidRoutesTotal:f8}" },
                { "Valid routes", $"{ValidRoutes:f8}" },
                { "Dead-end routes", $"{DeadEndRoutes:f8}" },
                { "Accumulated delta", $"{AccumulatedDelta:f8}" },
                { "Average route length", $"{AverageRouteLength:f8}" },
            };
        }

        public override void Iterate() {
            double valid = 0;
            double length = 0;
            double invalid = 0;
            double delta = 0;
            double accumulatedCount = 0;
            double totalLength = 0;

            for (int s = 0; s < graph.Count; s++) {
                for (int d = 0; d < graph.Count; d++) {
                    if (s == d) {
                        continue;
                    }

                    (int lvalid, int llength, int linvalid) = GetPossibleHops(s, d);

                    valid += lvalid;
                    length += llength;
                    invalid += linvalid;

                    if (lvalid > 0) {
                        double avgLength = (double)llength / lvalid;

                        accumulatedCount++;
                        delta += avgLength - lengths[s, d];
                        totalLength += avgLength;
                        
                        lengths[s, d] = avgLength;
                    }
                }
            }

            ValidRoutesTotal = valid;
            ValidRoutes = valid / (valid + invalid);
            DeadEndRoutes = invalid / (valid + invalid);
            if (accumulatedCount > 0) {
                AccumulatedDelta += delta / accumulatedCount;
                AverageRouteLength = totalLength / accumulatedCount;
            } else {
                AverageRouteLength = -1;
            }

            Target = toDisable;
            if (++iteration < IterationsTotal - 1) {
                int disable = random.Next(0, graph.Count - Iteration);
                toDisable = 0;
                while (!graph.Enabled[toDisable] || disable > 0) {
                    if (graph.Enabled[toDisable]) {
                        disable--;
                    }
                    toDisable++;
                }
                graph.Enabled[toDisable] = false;
                algorithm.Refresh();
            }
        }
    }
}
