using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace live_and_learn
{
    class WeightedGraph
    {
        private Dictionary<int, int>[] childNodes;
        public int Size { get; private set; }
        public WeightedGraph(Dictionary<int, int>[] childNodes)
        {
            this.childNodes = childNodes;
        }

        public int[] findShortestPaths()
        {
            List<bool> visited = InitializeVisited(childNodes.Length);
            int[] distances = InitializeDistances();
            while (visited.Contains(false))
            {
                int chosenVertex = PickMinimumDistance(visited, distances);
                if (chosenVertex == Int32.MaxValue)
                {
                    return distances;
                }
                visited[chosenVertex] = true;
                Dictionary<int, int> chosenDict = childNodes[chosenVertex];
                foreach (var element in chosenDict)
                {
                    int forcomparison = element.Value + distances[chosenVertex];
                    if (distances[element.Key] > forcomparison)
                    {
                        distances[element.Key] = forcomparison;
                    }
                }
            }
            return distances;
        }

        private int[] InitializeDistances()
        {
            int[] distances = new int[childNodes.Length];
            for (int i = 0; i < distances.Length; i++)
            {
                if (i == 0)
                {
                    distances[i] = 0;
                }
                else
                {
                    distances[i] = Int32.MaxValue;
                }
            }
            return distances;
        }

        private List<bool> InitializeVisited(int count)
        {
            List<bool> visited = new List<bool>();
            for (int i = 0; i < count; i++)
            {
                visited.Add(false);
            }
            return visited;
        }

        private int PickMinimumDistance(List<bool> visited, int[] distances)
        {
            int forComparison = Int32.MaxValue;
            int forReturn = Int32.MaxValue;
            for (int i = 0; i < distances.Length; i++)
            {
                if (distances[i] < forComparison && visited[i] == false)
                {
                    forComparison = distances[i];
                    forReturn = i;
                }
            }
            return forReturn;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            WeightedGraph myGraph = new WeightedGraph(new Dictionary<int, int>[] {
                new Dictionary<int, int>() { { 1, 1}, { 5, 7}, { 6, 6} }, //0
                new Dictionary<int, int>() {}, //1
                new Dictionary<int, int>() { { 0, 2 }, { 3, 3 } }, //2
                new Dictionary<int, int>() { { 5, 1 } }, //3
                new Dictionary<int, int>() {}, //4
                new Dictionary<int, int>() { { 4, 3 } }, //5
                new Dictionary<int, int>() { { 4, 8 }, { 9, 5 } }, //6
                new Dictionary<int, int>() { { 6, 3 } }, //7
                new Dictionary<int, int>() { { 7, 2 } }, //8
                new Dictionary<int, int>() { { 10, 2 }, { 11, 4 }, { 12, 3} }, //9
                new Dictionary<int, int>() {}, //10
                new Dictionary<int, int>() { { 12, 2 } }, //11
                new Dictionary<int, int>() {} //12
            });
            int[] mas = myGraph.findShortestPaths();
            foreach (int element in mas)
                Console.WriteLine(element);
        }
    }
}
