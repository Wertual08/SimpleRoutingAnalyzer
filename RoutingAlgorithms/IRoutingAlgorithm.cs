using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRoutingAnalyzer.RoutingAlgorithms
{
    interface IRoutingAlgorithm
    {
        int[] Route(RoutingData data);
        string Metadata(int node);
        string Metadata();
    }
}
