using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRoutingAnalyzer.RoutingAlgorithms
{
    class GreedyPromotionData
    {
        public class Vector
        {
            private int[] Data = new int[4] { int.MaxValue, int.MaxValue, int.MaxValue, int.MaxValue };

            public int this[int i] { get => Data[i]; set => Data[i] = value; }

            public int Dot(Vector vec)
            {
                if (vec == null) throw new ArgumentNullException("vec");
                if (Data.Length != vec.Data.Length) throw new Exception("Vectors must be same lengths.");
                int result = 0;
                for (int i = 0; i < Data.Length; i++)
                    result += Data[i] * vec.Data[i];
                return result;
            }
            public Vector Sub(Vector vec)
            {
                if (vec == null) throw new ArgumentNullException("vec");
                if (Data.Length != vec.Data.Length) throw new Exception("Vectors must be same lengths.");

                var res = new Vector();
                for (int i = 0; i < Data.Length; i++)
                    res.Data[i] = Data[i] - vec.Data[i];

                return res;
            }

            public override string ToString()
            {
                return $"{{ {Data[0]}, {Data[1]}, {Data[2]}, {Data[3]} }}";
            }
        }

        private Vector Centers = new Vector();
        private Vector[] Coordinates;
        private bool[] Exceptional;

        private static int Min(int a, int b, int c)
        {
            if (b > c) b = c;
            if (a > b) a = b;
            return a;
        }

        public GreedyPromotionData(Graph graph)
        {
            Exceptional = new bool[graph.Count];
            for (int i = 0; i < graph.Count; i++)
            {
                Exceptional[i] = graph.Available(i) <= 2;
                if (!Exceptional[i])
                {
                    var connected = graph.Connected(i);
                    foreach (var node in connected)
                    {
                        if (graph.Available(node) <= 2)
                        {
                            Exceptional[i] = true;
                            break;
                        }
                    }
                }
            }

            int start = 0, dim;
            Coordinates = new Vector[graph.Count];
            var buffer = new int[graph.Count];
            for (int i = 0; i < buffer.Length; i++)
            {
                Coordinates[i] = new Vector();
                buffer[i] = int.MaxValue;
            }

            buffer[0] = 0;
            for (int d = 0; d < Coordinates.Length; d++)
            {
                for (int i = 0; i < Coordinates.Length; i++)
                {
                    if (buffer[i] == d)
                    {
                        var nodes = graph.Connected(i);
                        foreach (int node in nodes)
                        {
                            if (d + 1 < buffer[node])
                            {
                                buffer[node] = d + 1;
                                start = node;
                            }
                        }
                    }
                }
            }

            dim = 0;
            Centers[dim] = start;
            Coordinates[start][dim] = 0;
            for (int d = 0; d < Coordinates.Length; d++)
            {
                for (int i = 0; i < Coordinates.Length; i++)
                {
                    if (Coordinates[i][dim] == d)
                    {
                        var nodes = graph.Connected(i);
                        foreach (int node in nodes)
                        {
                            int start_diff = Math.Min(
                                Math.Abs(Centers[dim] - start),
                                graph.Count - 1 - Math.Abs(Centers[dim] - start)
                            );
                            int cur_diff = Math.Min(
                                Math.Abs(Centers[dim] - node),
                                graph.Count - 1 - Math.Abs(Centers[dim] - node)
                            );
                            if (d + 1 < Coordinates[node][dim])
                            {
                                Coordinates[node][dim] = d + 1;
                                start = node;
                            }
                            else if (d + 1 == Coordinates[node][dim] && cur_diff > start_diff)
                            {
                                start = node;
                            }
                        }
                    }
                }
            }

            dim = 1;
            Centers[dim] = start;
            Coordinates[start][dim] = 0;
            for (int d = 0; d < Coordinates.Length; d++)
            {
                for (int i = 0; i < Coordinates.Length; i++)
                {
                    if (Coordinates[i][dim] == d)
                    {
                        var nodes = graph.Connected(i);
                        foreach (int node in nodes)
                        {
                            if (d + 1 < Coordinates[node][dim])
                            {
                                Coordinates[node][dim] = d + 1;

                                int start_sum = Coordinates[start][dim - 1] + Coordinates[start][dim];
                                int cur_sum = Coordinates[node][dim - 1] + Coordinates[node][dim];
                                int start_abs = Math.Abs(Coordinates[start][dim - 1] - Coordinates[start][dim]);
                                int cur_abs = Math.Abs(Coordinates[node][dim - 1] - Coordinates[node][dim]);

                                if (start_sum < cur_sum) start = node;
                                else if (start_sum == cur_sum && cur_abs < start_abs) start = node;
                            }
                        }
                    }
                }
            }

            dim = 2;
            Centers[dim] = start;
            Coordinates[start][dim] = 0;
            for (int d = 0; d < Coordinates.Length; d++)
            {
                for (int i = 0; i < Coordinates.Length; i++)
                {
                    if (Coordinates[i][dim] == d)
                    {
                        var nodes = graph.Connected(i);
                        foreach (int node in nodes)
                        {
                            if(d + 1 < Coordinates[node][dim])
                            {
                                Coordinates[node][dim] = d + 1;

                                int start_min = Min(Coordinates[start][dim - 2], Coordinates[start][dim - 1], Coordinates[start][dim]);
                                int cur_min = Min(Coordinates[node][dim - 2], Coordinates[node][dim - 1], Coordinates[node][dim]);
                                int start_sum = Coordinates[start][dim - 2] + Coordinates[start][dim - 1] + Coordinates[start][dim];
                                int cur_sum = Coordinates[node][dim - 2] + Coordinates[node][dim - 1] + Coordinates[node][dim];
                                if (cur_min > start_min) start = node;
                                else if (cur_min == start_min && cur_sum > start_sum) start = node;
                            }
                        }
                    }
                }
            }

            dim = 3;
            Centers[dim] = start;
            Coordinates[start][dim] = 0;
            for (int d = 0; d < Coordinates.Length; d++)
            {
                for (int i = 0; i < Coordinates.Length; i++)
                {
                    if (Coordinates[i][dim] == d)
                    {
                        var nodes = graph.Connected(i);
                        foreach (int node in nodes)
                            if (d + 1 < Coordinates[node][dim])
                                Coordinates[node][dim] = d + 1;
                    }
                }
            }

            string test = "";
            for (int d = 0; d < 4; d++)
            {
                for (int i = 0; i < Coordinates.Length; i++)
                {
                    test += Coordinates[i][d] + " ";
                    if (i % 4 == 3) test += Environment.NewLine;
                }

                test += Environment.NewLine;
            }
            File.WriteAllText("supershit.txt", test);
        }

        public int Count => Coordinates.Length;
        public Vector this[int i] { get => Coordinates[i]; set => Coordinates[i] = value; }
        public bool IsExceptional(int i) { return Exceptional[i]; }

        public string Metadata()
        {
            return Centers.ToString();
        }
    }
}
