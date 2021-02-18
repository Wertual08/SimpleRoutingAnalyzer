using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRoutingAnalyzer.RoutingAlgorithms
{
    class AdvancedGreedyPromotionRouting : IRoutingAlgorithm
    {
        public static readonly string Name = "Advanced Greedy Promotion";

        private GreedyPromotionData Data;
        private Graph Graph;

        public AdvancedGreedyPromotionRouting(Graph graph)
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

            res.Add(dests[0]);
            int max_dot = cur_del.Dot(Data[dests[0]].Sub(Data[s]));
            for (int i = 1; i < dests.Length; i++)
            {
                int cur_dot = cur_del.Dot(Data[dests[i]].Sub(Data[s]));
                if (cur_dot > max_dot)
                {
                    res.Clear();
                    res.Add(dests[i]);
                    max_dot = cur_dot;
                }
                else if (cur_dot == max_dot)
                    res.Add(dests[i]);
            }

            // MODIFICATION //
            if (res.Count == 0) res.Add(p);
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
