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
    class HalfFaultMetrics : Metrics {
        private readonly Graph graph;
        private readonly IRoutingAlgorithm algorithm;
        private readonly Random random;

        protected override int[] Route(int s, int d, int p) {
            return algorithm.Route(new RoutingData {
                Source = s,
                Destination = d,
                Previous = p,
            });
        }

        public HalfFaultMetrics(Graph graph, IRoutingAlgorithm algorithm, int seed, int iterations) {
            this.graph = graph;
            this.algorithm = algorithm;
            random = new Random(seed);
            iterationsTotal = iterations;

            InitHops(graph.Count);
        }

        public int Source { get; private set; }
        public int Destination { get; private set; } 
        public int FaultAfter { get; private set; }

        private int iterationsTotal;
        public override int IterationsTotal => iterationsTotal;
        private int iteration = -1;
        public override int Iteration { get => iteration; }
        public override Dictionary<string, string> State() {
            return new Dictionary<string, string> {
                { "Source", $"{Source:000}" },
                { "Destination", $"{Destination:000}" },
                { "Fault after", $"{FaultAfter:000}" },
            };
        }

        public override void Iterate() {
            for (int i = 0; i < graph.Enabled.Length; i++) {
                graph.Enabled[i] = true;
            }

            int source = random.Next(0, graph.Count);
            int destination = random.Next(0, graph.Count - 1);
            int disabled = 0;
            if (destination >= source) {
                destination++;
            }

            bool next;
            do {
                int disable = random.Next(0, graph.Count - disabled);
                int toDisable = 0;
                while (!graph.Enabled[toDisable] || disable > 0) {
                    if (graph.Enabled[toDisable]) {
                        disable--;
                    }
                    toDisable++;
                }
                graph.Enabled[toDisable] = false;
                algorithm.Refresh();

                (int lvalid, int llength, int linvalid) = GetPossibleHops(source, destination);
                next = linvalid < lvalid && lvalid > 0;
                disabled++;
            } while (next);

            Source = source;
            Destination = destination;
            FaultAfter = disabled;
            iteration++;
        }
    }
}
