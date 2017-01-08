using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntryPoint
{
    class FloydWarshall
    {
        int indexer = 0;
        Dictionary<int, GraphNode> IndexedGraphNodes = new Dictionary<int, GraphNode>();
        Dictionary<Vector2, GraphNode> GraphNodeByPosition = new Dictionary<Vector2, GraphNode>();


        int[,] DistanceMatrix;
        int[,] NextVertexMatrix;
        public void CalculateDistances()
        {
            DistanceMatrix = new int[IndexedGraphNodes.Count, IndexedGraphNodes.Count];
            NextVertexMatrix = new int[IndexedGraphNodes.Count, IndexedGraphNodes.Count];

            for (int x = 0; x < DistanceMatrix.GetLength(0); x++)
            {
                for (int y = 0; y < DistanceMatrix.GetLength(1); y++)
                {
                    DistanceMatrix[x, y] = int.MaxValue;
                }
            }
            int lol = 3;
            for (int i = 0; i < DistanceMatrix.GetLength(0); i++)
            {
                DistanceMatrix[i, i] = 0;
            }

            int lol2 = 3;
            for (int index = 0; index < IndexedGraphNodes.Count; index++)
            {
                foreach(var i in IndexedGraphNodes[index].GetAllDirectConnections())
                {
                    DistanceMatrix[index, i.index] = 1;
                    NextVertexMatrix[index, i.index] = i.index;
                }
            }

            int lol1 = 3;
            for (int k = 0; k < DistanceMatrix.GetLength(0); k++)
            {
                for (int i = 0; i < DistanceMatrix.GetLength(0); i++)
                {
                    for (int j = 0; j < DistanceMatrix.GetLength(0); j++)
                    {
                        if (DistanceMatrix[i, j] > DistanceMatrix[i, k] + DistanceMatrix[k, j])
                        {
                            if (!(DistanceMatrix[i, k] + DistanceMatrix[k, j] < DistanceMatrix[k, j] || (DistanceMatrix[i, k] + DistanceMatrix[k, j] < DistanceMatrix[i, k])))
                            {
                                DistanceMatrix[i, j] = DistanceMatrix[i, k] + DistanceMatrix[k, j];
                                NextVertexMatrix[i, j] = NextVertexMatrix[i, k];
                            }
                        }
                    }
                }
            }
            int haha = 3;
        }

        public List<Vector2> GetShortestPath(Vector2 start, Vector2 end)
        {
            List<Vector2> toreturn = new List<Vector2>();

            var startindex = GraphNodeByPosition[start].index;
            var endIndex = GraphNodeByPosition[end].index;

            toreturn.Add(start);
            int CurrentIndex = startindex;
            while(CurrentIndex != endIndex)
            {
                CurrentIndex = NextVertexMatrix[CurrentIndex, endIndex];
                toreturn.Add(IndexedGraphNodes[CurrentIndex].position);
            }
            throw new NotImplementedException();
        }

        public void AddConnection(Vector2 first, Vector2 second)
        {
            if (!GraphNodeByPosition.ContainsKey(first))
            {
                RegisterNode(first);
            }
            if (!GraphNodeByPosition.ContainsKey(second))
            {
                RegisterNode(second);
            }
            GraphNodeByPosition[first].AddConnection(GraphNodeByPosition[second]);
            GraphNodeByPosition[second].AddConnection(GraphNodeByPosition[first]);
        }
        private void RegisterNode(Vector2 position)
        {
            var node = new GraphNode(position, indexer);
            IndexedGraphNodes.Add(indexer, node);
            GraphNodeByPosition.Add(position, node);
            indexer++;
        }

        class GraphNode
        {
            public int index { get; private set; }
            public GraphNode(Vector2 position, int index)
            {
                this.position = position;
                this.index = index;
            }
            public void AddConnection(GraphNode node)
            {
                if (!DirectConnections.Contains(node))
                {
                    DirectConnections.Add(node);
                }
            }

            public List<GraphNode> GetAllDirectConnections()
            {
                return DirectConnections;
            }

            public Vector2 position { get; private set; }
            List<GraphNode> DirectConnections = new List<GraphNode>();
        }
    }
}
