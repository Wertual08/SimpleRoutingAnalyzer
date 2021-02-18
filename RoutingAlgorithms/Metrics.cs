using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SimpleRoutingAnalyzer.RoutingAlgorithms
{
    class MetricsArguments
    {
        public string[] Configs { get; set; }
        public int Seed { get; set; }
        public MessageQueue LogQueue { get; set; }
    }
    class MetricsState
    {
        public int DisabledNode { get; set; } = -1;
        public double AlgorithmAVGPath { get; set; } = -1;
        public double OptimalAVGPath { get; set; } = -1;
        public double AlgorithmAVGDelta { get; set; } = -1;
        public double OptimalAVGDelta { get; set; } = -1;
        public double TotalPaths { get; set; } = -1;
        public double AlgorithmUnreachable { get; set; } = -1;
        public double OptimalUnreachable { get; set; } = -1;
        public string Error { get; set; } = null;
    }
    class MetricsResult
    {
        public int Seed { get; set; } = 0;
    }
    static class Metrics
    {
        public static int GetPathLength(IRoutingAlgorithm algorithm, int s, int d, int cap, int p = -1, int l = 0)
        {
            if (s == d) return l;
            if (l == cap) return -1;

            var data = new RoutingData();
            data.Previous = p;
            data.Source = s;
            data.Destination = d;

            var dests = algorithm.Route(data);
            //int min = int.MaxValue;
            //for (int i = 0; i < dests.Length; i++)
            //{
            //    int path_length = GetPathLength(algorithm, dests[i], d, cap, s, l + 1);
            //    if (path_length >= 0 && min > dests.Length)
            //        min = path_length;
            //}
            //if (min == int.MaxValue) min = -1;
            //return min;

            if (dests.Length < 1) return -1;
            else return GetPathLength(algorithm, dests[0], d, cap, s, l + 1);
        }
        public static void InitWeights(int[] weights)
        {
            for (int w = 0; w < weights.Length; w++)
                weights[w] = int.MaxValue;
        }
        public static void MarkWeights(Graph graph, int[] weights, int s, int w = 0)
        {
            if (w < weights[s]) weights[s] = w;
            else return;

            foreach (int node in graph[s])
                MarkWeights(graph, weights, node, w + 1);
        }
        public static void MarkStraightWeights(Graph graph, int[] weights, int s, int w = 0)
        {
            if (w < weights[s]) weights[s] = w;
            else return;

            foreach (int node in graph.GetStraightNodes(s))
                MarkStraightWeights(graph, weights, node, w + 1);
        }

        public static void Simple(object sender, DoWorkEventArgs e)
        {
            var worker = sender as BackgroundWorker;
            var args = e.Argument as MetricsArguments;

            for (int i = 0; i < args.Configs.Length; i++)
            {
                int base_progress = i * 100 / args.Configs.Length;
                try
                {
                    var path = args.Configs[i];
                    var config = JsonSerializer.Deserialize<Config>(File.ReadAllText(path));
                    var graph = config.Graph;
                    var algorithm = Config.StringToAlgorithm(config.Algorithm, graph);

                    double algorithm_paths = 0;
                    double algorithm_length = 0;
                    int algorithm_unreachable = 0;
                    for (int s = 0; s < graph.Count; s++)
                    {
                        for (int d = 0; d < graph.Count; d++)
                        {
                            if (!graph.Enabled[s]) continue;
                            if (!graph.Enabled[d]) continue;
                            if (s == d) continue;

                            int len = GetPathLength(algorithm, s, d, graph.Count * 4);
                            if (len < 0)
                            {
                                algorithm_unreachable++;
                            }
                            else
                            {
                                algorithm_length += len;
                                algorithm_paths++;
                            }
                        }

                        worker.ReportProgress(
                            base_progress +
                            s * 50 / args.Configs.Length / graph.Count);
                    }

                    double optimal_paths = 0;
                    double optimal_length = 0;
                    int optimal_unreachable = 0;
                    int[] weights = new int[graph.Count];
                    for (int s = 0; s < graph.Count; s++)
                    {
                        if (!graph.Enabled[s]) continue;

                        InitWeights(weights);
                        MarkWeights(graph, weights, s, 0);

                        for (int k = 0; k < weights.Length; k++)
                        {
                            if (k == s) continue;
                            if (!graph.Enabled[k]) continue;

                            int w = weights[k];
                            if (w < int.MaxValue)
                            {
                                optimal_length += w;
                                optimal_paths++;
                            }
                            else
                            {
                                optimal_unreachable++;
                            }
                        }

                        worker.ReportProgress(
                            base_progress +
                            50 / args.Configs.Length +
                            s * 50 / args.Configs.Length / graph.Count);
                    }

                    var state = new MetricsState();
                    state.AlgorithmAVGPath = algorithm_length / algorithm_paths;
                    state.AlgorithmUnreachable = algorithm_unreachable;
                    state.OptimalAVGPath = optimal_length / optimal_paths;
                    state.OptimalUnreachable = optimal_unreachable;

                    worker.ReportProgress(
                        base_progress +
                        100 / args.Configs.Length, state);
                }
                catch (Exception ex)
                {
                    var state = new MetricsState();
                    state.Error = ex.ToString();
                    worker.ReportProgress(
                        base_progress +
                        100 / args.Configs.Length, state);
                }
            }

            e.Result = new MetricsResult();
        }
        public static void StressTest(object sender, DoWorkEventArgs e)
        {
            var worker = sender as BackgroundWorker;
            var args = e.Argument as MetricsArguments;
            var generator = new Random(args.Seed);

            for (int i = 0; i < args.Configs.Length; i++)
            {
                int base_progress = i * 100 / args.Configs.Length;
                int last_disabled = -1;
                try
                {
                    var path = args.Configs[i];
                    var config = JsonSerializer.Deserialize<Config>(File.ReadAllText(path));
                    var graph = config.Graph;
                    var algorithm = Config.StringToAlgorithm(config.Algorithm, graph);

                    int[] weights = new int[graph.Count];
                    int[] straight_weights = new int[graph.Count];
                    for (int t = 0; t < graph.Count; t++)
                    {
                        double total_paths = 0;
                        double algorithm_paths = 0;
                        double algorithm_length = 0;
                        double algorithm_delta = 0;
                        double algorithm_unreachable = 0;
                        double optimal_paths = 0;
                        double optimal_length = 0;
                        double optimal_delta = 0;
                        double optimal_unreachable = 0;

                        for (int s = 0; s < graph.Count; s++)
                        {
                            if (!graph.Enabled[s]) continue;

                            InitWeights(weights);
                            InitWeights(straight_weights);
                            MarkWeights(graph, weights, s, 0);
                            MarkStraightWeights(graph, straight_weights, s, 0);

                            for (int d = 0; d < graph.Count; d++)
                            {
                                if (!graph.Enabled[d]) continue;
                                if (s == d) continue;
                                total_paths++;

                                int s_len = straight_weights[d];
                                int o_len = weights[d];
                                int a_len = GetPathLength(algorithm, s, d, graph.Count);
                                if (o_len == int.MaxValue)
                                    optimal_unreachable++;
                                else
                                {
                                    optimal_length += o_len;
                                    optimal_delta += o_len - s_len;
                                    optimal_paths++;
                                }
                                if (a_len < 0)
                                    algorithm_unreachable++;
                                else
                                {
                                    algorithm_length += a_len;
                                    algorithm_delta += a_len - s_len;
                                    algorithm_paths++;
                                }
                            }

                            worker.ReportProgress(
                                base_progress +
                                t * 100 / args.Configs.Length / graph.Count
                                );
                        }

                        var state = new MetricsState();
                        state.DisabledNode = last_disabled;
                        state.AlgorithmAVGPath = algorithm_length / algorithm_paths;
                        state.AlgorithmUnreachable = algorithm_unreachable / total_paths;
                        state.AlgorithmAVGDelta = algorithm_delta / algorithm_paths;
                        state.TotalPaths = total_paths;
                        state.OptimalAVGPath = optimal_length / optimal_paths;
                        state.OptimalUnreachable = optimal_unreachable / total_paths;
                        state.OptimalAVGDelta = optimal_delta / optimal_paths;

                        worker.ReportProgress(
                            base_progress +
                            t * 100 / args.Configs.Length / graph.Count, 
                            state);

                        int ind = generator.Next(graph.Count - t);
                        for (int g = 0; g < graph.Count; g++)
                        {
                            if (graph.Enabled[g])
                            {
                                if (ind == 0)
                                {
                                    graph.Enabled[g] = false;
                                    last_disabled = g;
                                    break;
                                }
                                else ind--;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    var state = new MetricsState();
                    state.Error = ex.ToString();
                    worker.ReportProgress(
                        base_progress +
                        100 / args.Configs.Length, state);
                }
            }

            var result = new MetricsResult();
            result.Seed = args.Seed;
            e.Result = result;
        }
        public static void StressTestMonteKarlo(object sender, DoWorkEventArgs e)
        {
            var worker = sender as BackgroundWorker;
            var args = e.Argument as MetricsArguments;
            var generator = new Random(args.Seed);
            var logger = args.LogQueue;

            for (int i = 0; i < args.Configs.Length; i++)
            {
                int base_progress = i * 100 / args.Configs.Length;
                int last_disabled = -1;
                try
                {
                    var path = args.Configs[i];
                    var config = JsonSerializer.Deserialize<Config>(File.ReadAllText(path));
                    var graph = config.Graph;
                    var algorithm = Config.StringToAlgorithm(config.Algorithm, graph);

                    var pairs = new List<Tuple<int, int>>();
                    for (int p = 0; p < 3; p++)
                    {
                        int s = generator.Next(graph.Count - pairs.Count);
                        for (int k = 0; k < s; k++)
                            if (pairs.Find((Tuple<int, int> sd) => sd.Item1 == k || sd.Item2 == k) != null)
                                s++;
                        int d = generator.Next(graph.Count - pairs.Count);
                        for (int k = 0; k < d; k++)
                            if (pairs.Find((Tuple<int, int> sd) => sd.Item1 == k || sd.Item2 == k) != null)
                                d++;
                        pairs.Add(new Tuple<int, int>(s, d));
                        pairs.Add(new Tuple<int, int>(d, s));

                        logger.Put($"Selected pair: {s} <-> {d}");
                    }

                    int[][] straight_weights = new int[pairs.Count][];
                    for (int p = 0; p < pairs.Count; p++)
                    {
                        straight_weights[p] = new int[graph.Count];
                        int s = pairs[p].Item1;
                        InitWeights(straight_weights[p]);
                        MarkStraightWeights(graph, straight_weights[p], s);
                    }

                    int[] weights = new int[graph.Count];
                    for (int t = 0; t < graph.Count - pairs.Count; t++)
                    {
                        double total_paths = 0;
                        double algorithm_paths = 0;
                        double algorithm_length = 0;
                        double algorithm_delta = 0;
                        double algorithm_unreachable = 0;
                        double optimal_paths = 0;
                        double optimal_length = 0;
                        double optimal_delta = 0;
                        double optimal_unreachable = 0;

                        int w = 0;
                        foreach (var sd in pairs)
                        {
                            int s = sd.Item1;
                            int d = sd.Item2;

                            InitWeights(weights);
                            MarkWeights(graph, weights, s);

                            total_paths++;

                            int s_len = straight_weights[w][d];
                            int o_len = weights[d];
                            int a_len = GetPathLength(algorithm, s, d, graph.Count);
                            if (o_len == int.MaxValue)
                                optimal_unreachable++;
                            else
                            {
                                optimal_length += o_len;
                                optimal_delta += o_len - s_len;
                                optimal_paths++;
                            }
                            if (a_len < 0)
                                algorithm_unreachable++;
                            else
                            {
                                algorithm_length += a_len;
                                algorithm_delta += a_len - s_len;
                                algorithm_paths++;
                            }
                            w++;
                        }

                        var state = new MetricsState();
                        state.DisabledNode = last_disabled;
                        state.AlgorithmAVGPath = algorithm_length / algorithm_paths;
                        state.AlgorithmUnreachable = algorithm_unreachable / total_paths;
                        state.AlgorithmAVGDelta = algorithm_delta / algorithm_paths;
                        state.TotalPaths = total_paths;
                        state.OptimalAVGPath = optimal_length / optimal_paths;
                        state.OptimalUnreachable = optimal_unreachable / total_paths;
                        state.OptimalAVGDelta = optimal_delta / optimal_paths;

                        worker.ReportProgress(
                            base_progress +
                            t * 100 / args.Configs.Length / graph.Count, state);

                        int ind = generator.Next(graph.Count - t - pairs.Count);
                        for (int g = 0; g < graph.Count; g++)
                        {
                            if (graph.Enabled[g] && pairs.Find((Tuple<int, int> sd) => sd.Item1 == g || sd.Item2 == g) == null)
                            {
                                if (ind == 0)
                                {
                                    graph.Enabled[g] = false;
                                    last_disabled = g;
                                    break;
                                }
                                else ind--;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    var state = new MetricsState();
                    state.Error = ex.ToString();
                    worker.ReportProgress(
                        base_progress +
                        100 / args.Configs.Length, state);
                }
            }

            var result = new MetricsResult();
            result.Seed = args.Seed;
            e.Result = result;
        }
    }
}
