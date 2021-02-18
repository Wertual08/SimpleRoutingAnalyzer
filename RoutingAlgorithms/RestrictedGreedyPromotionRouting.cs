using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRoutingAnalyzer.RoutingAlgorithms
{
    class RestrictedGreedyPromotionRouting : IRoutingAlgorithm
    {
        public static readonly string Name = "Restricted Greedy Promotion";

        private GreedyPromotionData Data;
        private Graph Graph;

        public RestrictedGreedyPromotionRouting(Graph graph)
        {
            Data = new GreedyPromotionData(graph);
            Graph = graph;
        }

        public int[] Route(RoutingData data)
        {
            int p = data.Previous;
            int s = data.Source;
            int d = data.Destination;

            if (s == d) return new int[0];
            var dests = Graph[s];
            if (dests.Length < 1) return new int[0];
            else if (dests.Length == 1) return new int[] { dests[0] };
            
            // MODIFICATION //
            int j = 0;
            for (int i = 0; i < dests.Length; i++)
                if (dests[i] != p) dests[j++] = dests[i];
            var old = dests;
            dests = new int[j];
            for (int i = 0; i < j; i++)
                dests[i] = old[i];
            // ------------ //

            var res = new List<int>(2);

            var cur_del = Data[d].Sub(Data[s]);

            int max_dot = int.MinValue;
            int total_available = 1;
            foreach (int dest in dests)
            {
                if (dest != d && Graph.Available(dest) <= 2) continue;
                total_available++;
                int cur_dot = cur_del.Dot(Data[dest].Sub(Data[s]));
                if (cur_dot > max_dot)
                {
                    res.Clear();
                    res.Add(dest);
                    max_dot = cur_dot;
                }
                else if (cur_dot == max_dot)
                    res.Add(dest);
            }

            if (total_available <= 2 && p >= 0)
            {
                int cur_dot = cur_del.Dot(Data[p].Sub(Data[s]));
                if (cur_dot > max_dot)
                {
                    res.Clear();
                    res.Add(p);
                    max_dot = cur_dot;
                }
                else if (cur_dot == max_dot)
                    res.Add(p);
            }

            // MODIFICATION //
            if (res.Count == 0 && p >= 0) res.Add(p);
            // ------------ //
            return res.ToArray();
        }

        public string Metadata(int node)
        {
            if (node >= 0 && node < Data.Count)
                return Data[node].ToString();
            else return null;
        }
        public string Metadata()
        {
            return Data.Metadata();
        }
    }
}
