using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRoutingAnalyzer.Metrics {
    interface IMetrics {
        int IterationsTotal { get; }
        int Iteration { get; }

        Dictionary<string, string> State();

        void Iterate();
    }
}
