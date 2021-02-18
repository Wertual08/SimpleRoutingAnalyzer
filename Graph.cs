using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SimpleRoutingAnalyzer
{
    class Graph
    {
        public static readonly int None = 0;

        public int[] AdjencyMatrix { get; set; } = null;
        public Point[] Points { get; set; } = null;
        public bool[] Enabled { get; set; } = null;
        public int Count { get => Points?.Length ?? 0; }

        public Graph() { }
        public Graph(int size)
        {
            AdjencyMatrix = new int[size * size];
            Points = new Point[size];
            Enabled = new bool[size];
            for (int i = 0; i < Enabled.Length; i++)
                Enabled[i] = true;
        }

        public int this[int s, int d] 
        { 
            get => AdjencyMatrix[s * Count + d]; 
            set => AdjencyMatrix[s * Count + d] = value; 
        }
        public int[] this[int s]
        {
            get
            {
                var result = new List<int>(Count);
                for (int d = 0; d < Count; d++)
                    if (AdjencyMatrix[s * Count + d] != None && Enabled[d])
                        result.Add(d);
                return result.ToArray();
            }
        }
        public int[] GetStraightNodes(int s)
        {
            var result = new List<int>(Count);
            for (int d = 0; d < Count; d++)
                if (AdjencyMatrix[s * Count + d] != None)
                    result.Add(d);
            return result.ToArray();
        }

        public int[] Connected(int s)
        {
            var result = new List<int>(Count);
            for (int d = 0; d < Count; d++)
                if (AdjencyMatrix[s * Count + d] != None)
                    result.Add(d);
            return result.ToArray();
        }
        public int Available(int s)
        {
            int res = 0;
            for (int d = 0; d < Count; d++)
                if (AdjencyMatrix[s * Count + d] != None && Enabled[d])
                    res++;
            return res;
        }

        public static Graph Parse(string str)
        {
            var lines = str.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            if (lines.Length < 1) return null;

            var line = lines[0].Split(new char[] { ' ', ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
            if (line.Length != lines.Length) return null;

            var graph = new Graph(line.Length);

            for (int y = 0; y < graph.Count; y++)
            {
                line = lines[y].Split(new char[] { ' ', ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
                if (line.Length != graph.Count) return null;

                for (int x = 0; x < line.Length; x++)
                {
                    int val;
                    if (int.TryParse(line[x], out val)) graph[x, y] = val;
                    else return null;
                }
            }
            return graph;
        }
        public override string ToString()
        {
            var str = new StringBuilder(Count * Count * 4);
            for (int y = 0; y < Count; y++)
            {
                for (int x = 0; x < Count; x++)
                {
                    str.Append(AdjencyMatrix[y * Count + x]);
                    str.Append(" ");
                }
                str.Append(Environment.NewLine);
            }
            return str.ToString();
        }

        public void ParsePoints(string str)
        {
            var lines = str.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            
            for (int y = 0; y < lines.Length; y++)
            {
                var line = lines[y].Split(new char[] { ' ', ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
                if (line.Length != 2) return;
                
                int px, py;
                if (int.TryParse(line[0], out px) &&
                    int.TryParse(line[1], out py)) Points[y] = new Point(px, py);
                else return;
            }
        }
        public string PointsToString()
        {
            string str = "";

            for (int y = 0; y < (Points?.Length ?? 0); y++)
                str += Points[y].X + " " + Points[y].Y + Environment.NewLine;

            return str;
        }
    }
}
