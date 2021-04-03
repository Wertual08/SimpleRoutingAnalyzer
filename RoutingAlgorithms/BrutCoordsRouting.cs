using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRoutingAnalyzer.RoutingAlgorithms
{
    class BrutCoordsRouting : IRoutingAlgorithm
    {
        public static readonly string Name = "Brut-Coords";

        private int[,] Coordinates;

        private static int[] Solve(int src, int dst, int count, int[] generators)
        {
            int sd = dst - src;

            int n_bounds = (int)Math.Sqrt(count);
            int g_bounds = 2 * n_bounds;

            int n = -n_bounds;
            int[] vector = new int[generators.Length];
            for (int i = 0; i < vector.Length; i++)
                vector[i] = -g_bounds;

            int min_sum = int.MaxValue;
            int[] result = null;


            do
            {
                int eq = 0;
                eq += n * count;
                for (int i = 0; i < vector.Length; i++)
                    eq += vector[i] * generators[i];
                //eq = Math.Abs(eq);

                int sum = vector.Sum((int v) => Math.Abs(v));

                if (eq == sd)
                {
                    if (sum < min_sum)
                    {
                        result = new int[vector.Length + 1];
                        result[0] = n;
                        vector.CopyTo(result, 1);
                        min_sum = sum;
                    }
                }

                vector[0]++;
                for (int i = 0; i < vector.Length - 1; i++)
                {
                    if (vector[i] > g_bounds)
                    {
                        vector[i + 1]++;
                        vector[i] = -g_bounds;
                    }
                }
                if (vector[vector.Length - 1] > g_bounds)
                {
                    vector[vector.Length - 1] = -g_bounds;
                    n++;
                }
            }
            while (n <= n_bounds);

            return result;
        }

        public BrutCoordsRouting(int count, int[] generators)
        {
            Coordinates = new int[count, generators.Length + 1];
            for (int i = 0; i < count; i++)
            {
                var coords = Solve(0, i, count, generators);
                if (coords == null)
                    throw new Exception("Unable to find coordinates.");

                for (int j = 0; j < coords.Length; j++)
                {
                    Coordinates[i, j] = coords[j];
                }
            }
        }

        public int[] Route(RoutingData data)
        {
            return null;
        }

        public string Metadata(int node)
        {
            var builder = new StringBuilder();
            builder.Append("{ ");

            for (int i = 0; i < Coordinates.GetLength(1); i++)
            {
                builder.Append(/*Coordinates[(node + 12) % 64, i] - */Coordinates[node, i]);
                if (i < Coordinates.GetLength(1) - 1) builder.Append(", ");
            }
            builder.Append(" }");

            return builder.ToString();
        }

        public string Metadata()
        {
            return "";
        }
    }
}
