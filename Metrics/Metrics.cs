using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRoutingAnalyzer.Metrics {
    abstract class Metrics : IMetrics {
        private HashSet<int> routeStack = new HashSet<int>();
        private (int, int, int)?[] routeCache;

        protected abstract int[] Route(int s, int d, int p);
        protected void InitHops(int count) {
            routeCache = new (int, int, int)?[count];
        }
        private (int, int, int) GetPossibleHopsHelper(int s, int d, int p) {
            if (routeCache[s] != null) {
                return ((int, int, int))routeCache[s];
            }

            if (s == d) {
                routeCache[s] = (1, 0, 0);
                return (1, 0, 0);
            }

            var nodes = Route(s, d, p);

            if (nodes.Length < 1) {
                routeCache[s] = (0, 0, 1);
                return (0, 0, 1);
            }

            routeStack.Add(s);
            int valid = 0;
            int length = 0;
            int invalid = 0;

            foreach (var node in nodes) {
                if (routeStack.Contains(node)) {
                    invalid++;
                } else {
                    (int lvalid, int llength, int linvalid) = GetPossibleHopsHelper(node, d, s);
                    valid += lvalid;
                    length += llength + lvalid;
                    invalid += linvalid;
                }
            }

            routeStack.Remove(s);

            routeCache[s] = (valid, length, invalid);
            return (valid, length, invalid);
        }
        protected (int, int, int) GetPossibleHops(int s, int d) {
            routeStack.Clear();
            for (int i = 0; i < routeCache.Length; i++) {
                routeCache[i] = null;
            }
            return GetPossibleHopsHelper(s, d, -1);
        }


        public abstract int IterationsTotal { get; }
        public abstract int Iteration { get; }

        public abstract void Iterate();
        public abstract Dictionary<string, string> State();
    }
}
