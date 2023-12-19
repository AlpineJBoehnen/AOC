using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text;

namespace AdventOfCode.Y2023;

public class Day16 : AdventOfCodeDay
{
    public Day16() : base(2023, 16)
    {
    }

    protected override string SolvePart1(string[] input)
    {
        bool[][] energyMap = new bool[input.Length][];

        for (int i = 0; i < input.Length; i++)
        {
            energyMap[i] = new bool[input[i].Length];
        }

        int energy = CountEnergy(input.Select(x => x.ToCharArray()).ToArray(), new Beam(0, 0, _right));

        return energy.ToString();
    }

    protected override string SolvePart2(string[] input)
    {
        int maxEnergized = 0;

        // top
        for (int x = 0; x < input[0].Length; x++)
        {
            int energy = CountEnergy(input.Select(x => x.ToCharArray()).ToArray(), new Beam(x, 0, _down));

            if (energy > maxEnergized)
            {
                maxEnergized = energy;
            }
        }

        // bottom
        for (int x = 0; x < input[0].Length; x++)
        {
            int energy = CountEnergy(input.Select(x => x.ToCharArray()).ToArray(), new Beam(x, input.Length - 1, _up));

            if (energy > maxEnergized)
            {
                maxEnergized = energy;
            }
        }

        // left
        for (int y = 0; y < input.Length; y++)
        {
            int energy = CountEnergy(input.Select(x => x.ToCharArray()).ToArray(), new Beam(0, y, _right));

            if (energy > maxEnergized)
            {
                maxEnergized = energy;
            }
        }

        // right
        for (int y = 0; y < input.Length; y++)
        {
            int energy = CountEnergy(input.Select(x => x.ToCharArray()).ToArray(), new Beam(input[0].Length - 1, y, _left));

            if (energy > maxEnergized)
            {
                maxEnergized = energy;
            }
        }

        return maxEnergized.ToString();
    }

    private const byte _up = 0b00001000;
    private const byte _down = 0b00000100;
    private const byte _left = 0b00000010;
    private const byte _right = 0b00000001;
    private readonly static HashSet<Beam> _b = [];
    private static Beam? GetBeam(char[][] map, Beam b, List<Beam>? visited = null)
    {
        // base case
        if (b.X < 0 || b.Y < 0 || b.X == map[0].Length || b.Y == map.Length)
        {
            return null;
        }

        // already processed
        if (_b.TryGetValue(b, out Beam value))
        {
            return value;
        }

        // loop detection
        if (visited == null)
        {
            visited = [];
        }
        else if (visited.Contains(b))
        {
            int bInd = visited.IndexOf(b);
            b.IsLoop = true;
            b.Children.UnionWith(visited[(bInd+1)..]);
            return b;
        }

        visited.Add(b);
        Beam? c1 = null;
        Beam? c2 = null;

        if (b.IsMovingUp)
        {
            switch (map[b.Y][b.X])
            {
                case '.':
                case '|':
                    c1 = GetBeam(map, b.Up(), new(visited));
                    break;
                case '/':
                    c1 = GetBeam(map, b.Right(), new(visited));
                    break;
                case '\\':
                    c1 = GetBeam(map, b.Left(), new(visited));
                    break;
                case '-':
                    c2 = GetBeam(map, b.Right(), new(visited));
                    c1 = GetBeam(map, b.Left(), new(visited));
                    break;
            }
        }
        else if (b.IsMovingDown)
        {
            switch (map[b.Y][b.X])
            {
                case '.':
                case '|':
                    c1 = GetBeam(map, b.Down(), new(visited));
                    break;
                case '/':
                    c1 = GetBeam(map, b.Left(), new(visited));
                    break;
                case '\\':
                    c1 = GetBeam(map, b.Right(), new(visited));
                    break;
                case '-':
                    c1 = GetBeam(map, b.Left(), new(visited));
                    c2 = GetBeam(map, b.Right(), new(visited));
                    break;
            }
        }
        else if (b.IsMovingLeft)
        {
            switch (map[b.Y][b.X])
            {
                case '.':
                case '-':
                    c1 = GetBeam(map, b.Left(), new(visited));
                    break;
                case '/':
                    c1 = GetBeam(map, b.Down(), new(visited));
                    break;
                case '\\':
                    c1 = GetBeam(map, b.Up(), new(visited));
                    break;
                case '|':
                    c1 = GetBeam(map, b.Down(), new(visited));
                    c2 = GetBeam(map, b.Up(), new(visited));
                    break;
            }
        }
        else // Right
        {
            switch (map[b.Y][b.X])
            {
                case '.':
                case '-':
                    c1 = GetBeam(map, b.Right(), new(visited));
                    break;
                case '/':
                    c1 = GetBeam(map, b.Up(), new(visited));
                    break;
                case '\\':
                    c1 = GetBeam(map, b.Down(), new(visited));
                    break;
                case '|':
                    c1 = GetBeam(map, b.Up(), new(visited));
                    c2 = GetBeam(map, b.Down(), new(visited));
                    break;
            }
        }

        if(c1.HasValue)
        {
            b.Children.Add(c1.Value);
        }
        if(c2.HasValue)
        {
            b.Children.Add(c2.Value);
        }

        _b.Add(b);
        return b;
    }

    private static int CountEnergy(char[][] map, Beam b)
    {
        var beam = GetBeam(map, b) ?? throw new Exception("Bad start");
        var distinctCoords = beam.Path().Select(x => (x.X, x.Y)).Distinct().ToHashSet();
        var count = distinctCoords.Count();
        return count;
    }

    private struct Beam
    {
        public int X { get; set; }

        public int Y { get; set; }

        public byte Vec { get; set; }
        public HashSet<Beam> Children { get; set; } = [];
        public bool IsLoop { get; set; } = false;
        public Beam(int x, int y, byte directionVector)
        {
            X = x;
            Y = y;
            Vec = directionVector;
        }

        public readonly HashSet<Beam> Path()
        {
            HashSet<Beam> path = [this];
            if (IsLoop)
            {
                path.UnionWith(Children);
            }
            else
            {
                foreach(var child in Children)
                {
                    path.UnionWith(child.Path());
                }
            }

            return path;
        }

        public Beam Up()
        {
            return new Beam(X, Y - 1, _up);
        }

        public Beam Down()
        {
            return new Beam(X, Y + 1, _down);
        }

        public Beam Left()
        {
            return new Beam(X - 1, Y, _left);
        }

        public Beam Right()
        {
            return new Beam(X + 1, Y, _right);
        }

        public readonly bool IsMovingUp => (Vec & _up) != 0;

        public readonly bool IsMovingDown => (Vec & _down) != 0;

        public readonly bool IsMovingLeft => (Vec & _left) != 0;

        public readonly bool IsMovingRight => (Vec & _right) != 0;

        public override readonly string ToString()
        {
            string s = $"({X}, {Y})";
            if (IsMovingUp)
            {
                s += " U";
            }
            else if (IsMovingDown)
            {
                s += " D";
            }
            else if (IsMovingLeft)
            {
                s += " L";
            }
            else
            {
                s += " R";
            }

            s += $" [{Children.Count}]";

            return s;
        }

        int _yMask = 0b0000_0000_0000_0000_0011_1111_1111_1111;

        // XXXX XXXX XXXX XXYY YYYY YYYY YYYY VVVV
        public override int GetHashCode()
        {
            int hash = X << 18 ^ (Y & _yMask) << 4 ^ Vec;
            return hash;
        }

        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            if (obj is not Beam b)
            {
                return false;
            }

            if(X != b.X)
            {
                return false;
            }

            if(Y != b.Y)
            {
                return false;
            }

            if(Vec != b.Vec)
            {
                return false;
            }

            return true;
        }
    }
}