//using System.Collections.Specialized;
//using System.Diagnostics.CodeAnalysis;
//using System.Globalization;

//namespace AdventOfCode.Y2023;

//public class Day17 : AdventOfCodeDay
//{
//    /*
//function reconstruct_path(cameFrom, current)
//    total_path := {current}
//    while current in cameFrom.Keys:
//        current := cameFrom[current]
//        total_path.prepend(current)
//    return total_path

//// A* finds a path from start to goal.
//// h is the heuristic function. h(n) estimates the cost to reach goal from node n.
//function A_Star(start, goal, h)
//    // The set of discovered nodes that may need to be (re-)expanded.
//    // Initially, only the start node is known.
//    // This is usually implemented as a min-heap or priority queue rather than a hash-set.
//    openSet := {start}

//    // For node n, cameFrom[n] is the node immediately preceding it on the cheapest path from the start
//    // to n currently known.
//    cameFrom := an empty map

//    // For node n, gScore[n] is the cost of the cheapest path from start to n currently known.
//    gScore := map with default value of Infinity
//    gScore[start] := 0

//    // For node n, fScore[n] := gScore[n] + h(n). fScore[n] represents our current best guess as to
//    // how cheap a path could be from start to finish if it goes through n.
//    fScore := map with default value of Infinity
//    fScore[start] := h(start)

//    while openSet is not empty
//        // This operation can occur in O(Log(N)) time if openSet is a min-heap or a priority queue
//        current := the node in openSet having the lowest fScore[] value
//        if current = goal
//            return reconstruct_path(cameFrom, current)

//        openSet.Remove(current)
//        for each neighbor of current
//            // d(current,neighbor) is the weight of the edge from current to neighbor
//            // tentative_gScore is the distance from start to the neighbor through current
//            tentative_gScore := gScore[current] + d(current, neighbor)
//            if tentative_gScore < gScore[neighbor]
//                // This path to neighbor is better than any previous one. Record it!
//                cameFrom[neighbor] := current
//                gScore[neighbor] := tentative_gScore
//                fScore[neighbor] := tentative_gScore + h(neighbor)
//                if neighbor not in openSet
//                    openSet.add(neighbor)

//    // Open set is empty but goal was never reached
//    return failure
//     */

//    public Day17() : base(2023, 17)
//    {
//    }

//    protected override string SolvePart1(string[] input)
//    {
//        return "";
//    }

//    protected override string SolvePart2(string[] input)
//    {
//        return "";
//    }

//    private static (Node start, Node end) GetGraph(string[] input)
//    {
//        Dictionary<(ushort x, ushort y), Node> nodes = new();
//        Node? start = null;
//        Node? end = null;
//        for (ushort yy = 0; yy < input.Length; yy++)
//        {
//            for (ushort xx = 0; xx < input[0].Length; xx++)
//            {
//                Node node = new(xx, yy, int.Parse(input[yy][xx].ToString()));
//                nodes.Add((xx, yy), node);

//                if ((xx, yy) == (0, 0))
//                {
//                    start = node;
//                }
//                else if ((xx, yy) == (input[0].Length - 1, input.Length - 1))
//                {
//                    end = node;
//                }
//            }
//        }

//        foreach (var node in nodes)
//        {
//            if (node.Key.x > 0) 
//            {
//                node.Value.Neighbors.Enqueue(nodes[(node.X - 1, node.Y)], node.Value);
//            }
//            if (node.Key.x < input[0].Length - 1)
//            {
//                node.Value.Neighbors.Enqueue(nodes[(node.Key.x + 1, node.Key.y)], 1);
//            }
//            if (node.Key.y > 0)
//            {
//                node.Value.Neighbors.Enqueue(nodes[(node.Key.x, node.Key.y - 1)], 1);
//            }
//            if (node.Key.y < input.Length - 1)
//            {
//                node.Value.Neighbors.Enqueue(nodes[(node.Key.x, node.Key.y + 1)], 1);
//            }
//        }

//        if (start == null || end == null)
//        {
//            throw new Exception("Start or end node not found");
//        }

//        return (start.Value, end.Value);
//    }

//    private static int AStar(Node start, Node end, Func<Node, int[][]> heuristic)
//    {
//        PriorityQueue<Node, int> openSet = new();
//        openSet.Enqueue(start, 0);

//        List<Node> cameFrom = [];

//    }

//    private struct Node
//    {
//        public ushort X { get; set; }
//        public ushort Y { get; set; }
//        public int Value { get; set; }
//        public int GScore { get; set; } = int.MaxValue;
//        public PriorityQueue<Node, int> Neighbors { get; set; } = new();

//        public Node(ushort x, ushort y, int value)
//        {
//            X = x;
//            Y = y;
//            Value = value;
//        }

//        public override int GetHashCode()
//        {
//            return X << 16 | Y;
//        }

//        public override bool Equals([NotNullWhen(true)] object? obj)
//        {
//            if (obj is Node node)
//            {
//                return (node.X, node.Y) == (X, Y);
//            }

//            return false;
//        }
//    }
//}