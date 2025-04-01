using System;
using System.Collections.Generic;

class Graph
{
    private int vertices;
    private Dictionary<int, List<(int, int)>> adjacencyList;

    public Graph(int vertices)
    {
        this.vertices = vertices;
        adjacencyList = new Dictionary<int, List<(int, int)>>();
        for (int i = 0; i < vertices; i++)
        {
            adjacencyList[i] = new List<(int, int)>();
        }
    }

    public void AddEdge(int source, int destination, int weight)
    {
        adjacencyList[source].Add((destination, weight));
    }

    public void Dijkstra(int start)
    {
        int[] distances = new int[vertices];
        bool[] visited = new bool[vertices];
        for (int i = 0; i < vertices; i++)
        {
            distances[i] = int.MaxValue;
        }
        distances[start] = 0;

        for (int i = 0; i < vertices - 1; i++)
        {
            int minVertex = MinDistance(distances, visited);
            visited[minVertex] = true;

            foreach (var neighbor in adjacencyList[minVertex])
            {
                int v = neighbor.Item1;
                int weight = neighbor.Item2;

                if (!visited[v] && distances[minVertex] != int.MaxValue && distances[minVertex] + weight < distances[v])
                {
                    distances[v] = distances[minVertex] + weight;
                }
            }
        }

        PrintSolution(distances);
    }

    private int MinDistance(int[] distances, bool[] visited)
    {
        int min = int.MaxValue, minIndex = -1;
        for (int v = 0; v < vertices; v++)
        {
            if (!visited[v] && distances[v] <= min)
            {
                min = distances[v];
                minIndex = v;
            }
        }
        return minIndex;
    }

    private void PrintSolution(int[] distances)
    {
        Console.WriteLine("Nodo	Distancia desde origen");
        for (int i = 0; i < vertices; i++)
        {
            Console.WriteLine(i + "\t" + distances[i]);
        }
    }
}

class Program
{
    static void Main()
    {
        Graph g = new Graph(5);
        g.AddEdge(0, 1, 10);
        g.AddEdge(0, 2, 3);
        g.AddEdge(1, 2, 1);
        g.AddEdge(1, 3, 2);
        g.AddEdge(2, 1, 4);
        g.AddEdge(2, 3, 8);
        g.AddEdge(2, 4, 2);
        g.AddEdge(3, 4, 7);
        g.AddEdge(4, 3, 9);

        Console.WriteLine("Ejecutando Dijkstra desde el nodo 0:");
        g.Dijkstra(0);
    }
}
