using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRoutingAnalyzer.RoutingAlgorithms
{
    class Config
    {
        public static string AlgorithmToString(IRoutingAlgorithm algorithm)
        {
            if (algorithm is DijkstraRouting) return DijkstraRouting.Name;
            if (algorithm is GreedyPromotionRouting) return GreedyPromotionRouting.Name;
            if (algorithm is AdvancedGreedyPromotionRouting) return AdvancedGreedyPromotionRouting.Name;
            if (algorithm is RestrictedGreedyPromotionRouting) return RestrictedGreedyPromotionRouting.Name;
            if (algorithm is RestrictedGreedyPromotion2Routing) return RestrictedGreedyPromotion2Routing.Name;
            else return "None";
        }
        public static IRoutingAlgorithm StringToAlgorithm(string algorithm, Graph graph)
        {
            if (graph == null) return null;
            if (algorithm == DijkstraRouting.Name) return new DijkstraRouting(graph);
            if (algorithm == GreedyPromotionRouting.Name) return new GreedyPromotionRouting(graph);
            if (algorithm == AdvancedGreedyPromotionRouting.Name) return new AdvancedGreedyPromotionRouting(graph);
            if (algorithm == RestrictedGreedyPromotionRouting.Name) return new RestrictedGreedyPromotionRouting(graph);
            if (algorithm == RestrictedGreedyPromotion2Routing.Name) return new RestrictedGreedyPromotion2Routing(graph);
            else return null;
        }

        public Graph Graph { get; set; }
        public string Algorithm { get; set; }
        public string[] Configs { get; set; }
        public string[] Seeds { get; set; }
        public string Name { get; set; }
    }
}
