using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2021.Day15
{
    internal class Solver
    {
        public string SolveDayStar1(string input)
        {
            var dangerMap = new DangerMap(input);

            var safestPath = dangerMap.GetSafestPath();

            return safestPath.Skip(1).Sum(n => n.Danger).ToString();
        }

        public string SolveDayStar2(string input)
        {
            var dangerMap2 = new DangerMap(input);
            dangerMap2.ExpandMap(5, 5);

            var dangerMap = new DangerMap(dangerMap2.ToString());

            var s = dangerMap.ToString();

            var safestPath = dangerMap.GetSafestPath();

            return safestPath.Skip(1).Sum(n => n.Danger).ToString();
        }

        public class DangerMap
        {
            private Node[,] _nodes;
            private readonly List<Node> _allNodes;

            public DangerMap(string input)
            {
                var lines = input.SplitByNewLine();
                int yMax = lines.Count();
                int xMax = lines.First().Length;

                _nodes = new Node[xMax, yMax];
                _allNodes = new List<Node>();

                for (int y = 0; y < yMax; y++)
                {
                    var line = lines.ElementAt(y);

                    for (int x = 0; x < xMax; x++)
                    {
                        int danger = int.Parse(line[x].ToString());

                        _nodes[x, y] = new Node(x, y, danger);
                        _allNodes.Add(_nodes[x, y]);
                    }
                }

                // Connect Nodes
                for (int y = 0; y < yMax; y++)
                {
                    for (int x = 0; x < xMax; x++)
                    {
                        var node = _nodes[x, y];
                        if (y > 0)
                            node.AddConnectedNode(Direction.N, _nodes[x, y - 1]);

                        if (x + 1 < xMax)
                            node.AddConnectedNode(Direction.E, _nodes[x + 1, y]);

                        if (y + 1 < yMax)
                            node.AddConnectedNode(Direction.S, _nodes[x, y + 1]);

                        if (x > 0)
                            node.AddConnectedNode(Direction.W, _nodes[x - 1, y]);
                    }
                }
            }

            public void ExpandMap(int xFactor, int yFactor)
            {
                var nodes = _nodes;
                _allNodes.Clear();

                int yMax = nodes.GetLength(1);
                int xMax = nodes.GetLength(1);

                int newYMax = yMax * yFactor;
                int newXMax = xMax * xFactor;

                _nodes = new Node[newXMax, newYMax];

                //Expand Right
                for (int y = 0; y < yMax; y++)
                {
                    for (int x = 0; x < xMax; x++)
                    {
                        var node = new Node(nodes[x, y]);
                        _nodes[x, y] = node;
                        _allNodes.Add(_nodes[x, y]);

                        for (int xfactor = 1; xfactor < xFactor; xfactor++)
                        {
                            int newX = (x + (xMax * xfactor));
                            int newY = y;
                            int newDanger = (node.Danger + xfactor);
                            if (newDanger > 9)
                                newDanger = (newDanger % 10) + 1;

                            _nodes[newX, newY] = new Node(newX, newY, newDanger);
                            _allNodes.Add(_nodes[newX, newY]);
                        }
                    }
                }

                // Expand down
                for (int y = 0; y < yMax; y++)
                {
                    for (int x = 0; x < newXMax; x++)
                    {
                        var node = _nodes[x, y];
                        _nodes[x, y] = node;

                        for (int yfactor = 1; yfactor < yFactor; yfactor++)
                        {
                            int newX = x;
                            int newY = (y + (yMax * yfactor));
                            int newDanger = (node.Danger + yfactor);
                            if (newDanger > 9)
                                newDanger = (newDanger % 10) + 1;
                            _nodes[newX, newY] = new Node(newX, newY, newDanger);
                            _allNodes.Add(_nodes[newX, newY]);
                        }
                    }
                }

                // Connect Nodes
                for (int y = 0; y < newYMax; y++)
                {
                    for (int x = 0; x < newXMax; x++)
                    {
                        var node = _nodes[x, y];
                        if (y > 0)
                            node.AddConnectedNode(Direction.N, _nodes[x, y - 1]);

                        if (x + 1 < xMax)
                            node.AddConnectedNode(Direction.E, _nodes[x + 1, y]);

                        if (y + 1 < yMax)
                            node.AddConnectedNode(Direction.S, _nodes[x, y + 1]);

                        if (x > 0)
                            node.AddConnectedNode(Direction.W, _nodes[x - 1, y]);
                    }
                }



            }

            public List<Node> GetSafestPath()
            {
                Node startNode = _nodes[0, 0];
                Node endNode = _nodes[_nodes.GetLength(0) - 1, _nodes.GetLength(1) - 1];


                DijkstraSolver solver = new DijkstraSolver();
                var result = solver.Solve(_allNodes, startNode, endNode);

                return result;
            }

            public override string ToString()
            {
                StringBuilder sb = new StringBuilder();

                for (int y = 0; y < _nodes.GetLength(1); y++)
                {
                    if (y != 0)
                        sb.AppendLine();

                    for (int x = 0; x < _nodes.GetLength(0); x++)
                    {
                        var node = _nodes[x, y];
                        sb.Append(node?.Danger.ToString() ?? "X");
                    }
                }

                return sb.ToString();
            }
        }

        public class DijkstraSolver
        {
            public List<Node> Solve(List<Node> nodes, Node start, Node end)
            {
                Dictionary<Node, NodeInfo<Node>> nodeInfos = nodes.ToDictionary(n => n, n => new NodeInfo<Node>(n));

                foreach (var item in nodeInfos.Values)
                {
                    item.Neighbours = item.Node.GetConnectedNodes().Select(n => nodeInfos[n]).ToList();
                }

                HashSet<NodeInfo<Node>> unvisted = new();
                unvisted.Add(nodeInfos[start]);

                while(unvisted.Any())
                {
                    var currentNodeInfo = unvisted.OrderBy(o => o.ShortestDistance).First();
                    currentNodeInfo.IsVisited = true;
                    unvisted.Remove(currentNodeInfo);

                    foreach (var nodeInfo in currentNodeInfo.Neighbours.Where(n => n.IsVisited == false))
                    {
                        var distance = currentNodeInfo.ShortestDistance + nodeInfo.Node.Danger;

                        if (currentNodeInfo.ShortestDistance == int.MaxValue || nodeInfo.ShortestDistance > distance)
                        {
                            nodeInfo.ShortestDistance = distance;
                            nodeInfo.PreviousNodeInfo = currentNodeInfo;
                        }

                        unvisted.Add(nodeInfo);
                    }
                }

                List<Node> result = new List<Node>();
                var test = nodeInfos.Values.Where(o => o.IsVisited == false).Count();

                NodeInfo<Node>? node = nodeInfos[end];

                while(node != null)
                {
                    result.Insert(0, node.Node);
                    node = node.PreviousNodeInfo;
                }


                return result;
                
            }

            public class NodeInfo<T> : IEquatable<NodeInfo<T>?>
            {
                public int ShortestDistance { get; set; }

                public NodeInfo<T>? PreviousNodeInfo { get; set; }

                public T Node { get; set; }

                public bool IsVisited { get; set; }

                public List<NodeInfo<T>> Neighbours { get; set; } = new List<NodeInfo<T>>();

                public NodeInfo(T node, int shortestDistance = int.MaxValue, NodeInfo<T>? previousNodeInfo = null)
                {
                    Node = node;
                    ShortestDistance = shortestDistance;
                    PreviousNodeInfo = previousNodeInfo;
                }

                public override bool Equals(object? obj)
                {
                    return Equals(obj as NodeInfo<T>);
                }

                public bool Equals(NodeInfo<T>? other)
                {
                    return other != null &&
                           EqualityComparer<T>.Default.Equals(Node, other.Node);
                }

                public override int GetHashCode()
                {
                    return HashCode.Combine(Node);
                }

                public override string ToString()
                {
                    return Node?.ToString() ?? "";
                }
            }
        }

        public enum Direction
        {
            N,
            E,
            S,
            W
        }

        public class Node : IEquatable<Node?>
        {
            public int Y { get; set; }

            public int X { get; set; }

            public int Danger { get; set; }

            public IEnumerable<Node> Nodes => _connectedNodes.Values.ToList();

            private readonly Dictionary<Direction, Node> _connectedNodes = new();
            private Node node;

            public Node(int x, int y, int danger)
            {
                X = x;
                Y = y;
                Danger = danger;
            }

            public Node(Node node)
            {
                X = node.X;
                Y = node.Y;
                Danger = node.Danger;
            }

            public void AddConnectedNode(Direction direction, Node node)
            {
                _connectedNodes.Add(direction, node);
            }

            public IEnumerable<Node> GetConnectedNodes() => _connectedNodes.Values.ToList();

            public Node? GetConnectedNode(Direction direction)
            {
                _connectedNodes.TryGetValue(direction, out var foundNode);
                return foundNode;
            }

            public override bool Equals(object? obj)
            {
                return Equals(obj as Node);
            }

            public bool Equals(Node? other)
            {
                return other != null &&
                       Y == other.Y &&
                       X == other.X;
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(Y, X);
            }

            public override string ToString()
            {
                return $"{Danger} ({X},{Y})";
            }
        }
    }
}
