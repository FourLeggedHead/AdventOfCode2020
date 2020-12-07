using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace FourLeggedHead.Tools
{
    public static class GraphSolver
    {

        #region BFS Algorithm

        /// <summary>
        /// Explore all levels of the graph until the desired vertex is found
        /// </summary>
        /// <param name="graph"> Graph to search within </param>
        /// <param name="origin"> Starting vertex in the graph for the search </param>
        /// <param name="target"> Vertex to look for </param>
        /// <returns></returns>
        public static HashSet<IVertex> BreadthFirstSearch<T>(IGraph<T> graph, IVertex origin, IVertex target) where T : struct
        {
            if (!graph.Contains(origin)) return null;
            if (target != null && !graph.Contains(target)) return null;

            var visited = new HashSet<IVertex>();

            var queue = new Queue<IVertex>();
            queue.Enqueue(origin);

            while (queue.Any())
            {
                var vertex = queue.Dequeue();

                if (visited.Contains(vertex)) continue;

                visited.Add(vertex);

                if (target != null && vertex == target) return visited;

                var neighbors = graph.GetNeighbors(vertex);
                if (neighbors != null)
                {
                    foreach (var neighbor in neighbors)
                    {
                        if (!visited.Contains(neighbor))
                        {
                            queue.Enqueue(neighbor);
                            neighbor.Parent = vertex;
                        }
                    }
                }
            }

            return visited;
        }
        
        /// <summary>
        /// Explore the entire graph, level by level
        /// </summary>
        /// <param name="graph"> Graph to explore </param>
        /// <param name="origin"> Starting vertex in the graph for its exploration </param>
        /// <returns></returns>
        public static HashSet<IVertex> BreadthFirstExploration<T>(IGraph<T> graph, IVertex origin) where T : struct
        {
            return BreadthFirstSearch(graph, origin, null);
        }

        #endregion

        #region DFS Algortihm

        /// <summary>
        /// Explore the graph depth first, recursively
        /// </summary>
        /// <param name="graph"> Graph to explore </param>
        /// <param name="origin"> Starting vertex in the graph for its exploration </param>
        /// <returns></returns>
        public static HashSet<IVertex> DepthFirstSearchRecursive<T>(IGraph<T> graph, IVertex origin) where T : struct
        {
            var visited = new HashSet<IVertex>();
            DepthFirstTraverse<T>(graph, visited, origin);
            return visited;
        }

        private static void DepthFirstTraverse<T>(IGraph<T> graph, HashSet<IVertex> visited, IVertex vertex) where T : struct
        {
            if (!graph.Contains(vertex)) return;

            visited.Add(vertex);

            var neighbors = graph.GetNeighbors(vertex);
            if (neighbors != null)
            {
                foreach (var neighbor in neighbors)
                {
                    if (!visited.Contains(neighbor))
                    {
                        DepthFirstTraverse(graph, visited, neighbor);
                    }
                }
            }
        }

        /// <summary>
        /// Explore entire branches of the graph until the desired vertex is found
        /// </summary>
        /// <param name="graph"> Graph to search within </param>
        /// <param name="origin"> Starting vertex in the graph for the search </param>
        /// <param name="target"> Vertex to look for </param>
        /// <returns></returns>
        private static HashSet<IVertex> DepthFirstSearchIterative<T>(IGraph<T> graph, IVertex origin, IVertex target) where T : struct
        {
            if (!graph.Contains(origin)) return null;
            if (target != null && !graph.Contains(target)) return null;

            var visited = new HashSet<IVertex>();

            var stack = new Stack<IVertex>();
            stack.Push(origin);

            while (stack.Any())
            {
                var vertex = stack.Pop();

                if (visited.Contains(vertex)) continue;

                visited.Add(vertex);

                if (target != null && vertex == target) return visited;

                var neighbors = graph.GetNeighbors(vertex);
                if (neighbors != null)
                {
                    foreach (var neighbor in neighbors)
                    {
                        if (!visited.Contains(neighbor))
                        {
                            stack.Push(neighbor);
                            neighbor.Parent = vertex;
                        }
                    }
                }
            }

            return visited;
        }

        /// <summary>
        /// Explore the graph depth first, iteratively
        /// </summary>
        /// <param name="graph"> Graph to explore </param>
        /// <param name="origin"> Starting vertex in the graph for its exploration </param>
        /// <returns></returns>
        public static HashSet<IVertex> DepthFirstExploration<T>(IGraph<T> graph, IVertex origin) where T : struct
        {
            return DepthFirstSearchIterative(graph, origin, null);
        }

        #endregion

        #region Dijkstra Algorithm

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="graph"></param>
        /// <param name="origin"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static Dictionary<IVertex,T> GetShortestPathDijkstra<T>(IGraph<T> graph, IVertex origin, IVertex target)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            if (!graph.Contains(origin)) return null;
            if (target != null && !graph.Contains(target)) return null;

            var list = new List<IVertex>(graph);

            var vertices = new Dictionary<IVertex, T>();
            foreach (var vertex in list)
            {
                vertex.Parent = null;
                vertices.Add(vertex, graph.MaxDistance());
            }

            vertices[origin] = graph.DistanceBetweenVertices(origin, origin);

            while (list.Any())
            {
                var remainingVertices = vertices.Where(v => list.Contains(v.Key)).ToDictionary(v => v.Key, v => v.Value);
                var minDistance = remainingVertices.Values.Min();
                var vertex = remainingVertices.First(v => v.Value.Equals(minDistance)).Key;
                
                list.Remove(vertex);

                if (target != null && vertex == target) return vertices;

                var neighbors = graph.GetNeighbors(vertex);
                if (neighbors != null)
                {
                    foreach (var neighbor in neighbors)
                    {
                        if (list.Contains(neighbor))
                        {
                            var distance = graph.AddDistance(vertices[vertex],
                                                (T)graph.DistanceBetweenNeighbors(vertex, neighbor));

                            if (distance.CompareTo(vertices[neighbor]) < 0)
                            {
                                neighbor.Parent = vertex;
                                vertices[neighbor] = distance;
                            } 
                        }
                    } 
                }
            }

            return vertices;
        }

        #endregion
    }
}
