using System.Collections.Specialized;
using System.Drawing;
using System.Globalization;
using System.Runtime.ExceptionServices;

namespace AdventOfCode.Y2023;

public class Day18 : AdventOfCodeDay
{
    public Day18() : base(2023, 18)
    {
    }

    protected override string SolvePart1(string[] input)
    {
        List<PaintedCell> cells = new();
        int x = 0;
        int y = 0;
        foreach (string line in input)
        {
            var parts = line.Split(' ');
            string dir = parts[0];
            int count = int.Parse(parts[1]);
            int color = Convert.ToInt32(parts[2][2..^1], 16);

            for (int i = 0; i < count; i++)
            {
                switch (dir)
                {
                    case "U":
                        cells.Add(new PaintedCell { X = x, Y = --y, Color = color, IsUpOrDown = true });
                        break;
                    case "D":
                        cells.Add(new PaintedCell { X = x, Y = ++y, Color = color, IsUpOrDown = true });
                        break;
                    case "L":
                        cells.Add(new PaintedCell { X = --x, Y = y, Color = color, IsUpOrDown = false });
                        break;
                    case "R":
                        cells.Add(new PaintedCell { X = ++x, Y = y, Color = color, IsUpOrDown = false });
                        break;
                }
            }
        }

        int width = (cells.Max(_ => _.X) - cells.Min(_ => _.X)) + 1;
        int height = (cells.Max(_ => _.Y) - cells.Min(_ => _.Y)) + 1;
        int offsetX = -cells.Min(_ => _.X);
        int offsetY = -cells.Min(_ => _.Y);

        int[][] paint = new int[height][];
        bool[][] upsAndDowns = new bool[height][];

        foreach (PaintedCell cell in cells)
        {
            if (paint[cell.Y + offsetY] == null)
            {
                paint[cell.Y + offsetY] = new int[width];
                upsAndDowns[cell.Y + offsetY] = new bool[width];
            }

            paint[cell.Y + offsetY][cell.X + offsetX] = cell.Color;
            upsAndDowns[cell.Y + offsetY][cell.X + offsetX] = cell.IsUpOrDown;
        }

        (int Y, int X) firstInside = (0, 0);
        for (int xx = 0; xx < width; xx++)
        {
            if (paint[0][xx] != 0)
            {
                firstInside = (1, xx + 1);
                break;
            }
        }

        FloodFill(ref paint, firstInside.X, firstInside.Y);
        int total = 0;
        for (int yy = 0; yy < height; yy++)
        {
            for (int xx = 0; xx < width; xx++)
            {
                if (paint[yy][xx] != 0)
                {
                    total++;
                }
            }
        }

        return total.ToString();
    }

    protected override string SolvePart2(string[] input)
    {
        //19838269529535
        //19838269529535
        int n = 1;
        (long X, long Y) current = (0,0);

        List<long> X = [0];
        List<long> Y = [0];
        //List<long> X = [0,4,4,0];
        //List<long> Y = [0,0,4,4];
        //List<long> X = [0,5,5,0];
        //List<long> Y = [0,0,5,5];
        //n = 4;
        foreach (string line in input)
        {
            //var parts = line.Split(' ');
            //int count = Convert.ToInt32(parts[2][2..7], 16);
            //switch (parts[2][7])
            //{
            //    case '0': // up
            //        current.Y -= count;
            //        break;
            //    case '1': // down
            //        current.Y += count;
            //        break;
            //    case '2': // left
            //        current.X -= count;
            //        break;
            //    case '3': // right
            //        current.X += count;
            //        break;
            //    default:
            //        throw new Exception("Unknown direction");
            //};

            var parts = line.Split(' ');
            int count = int.Parse(parts[1]);
            switch (parts[0][0])
            {
                case 'U': // up
                    current.Y -= count;
                    break;
                case 'D': // down
                    current.Y += count;
                    break;
                case 'L': // left
                    current.X -= count;
                    break;
                case 'R': // right
                    current.X += count;
                    break;
                default:
                    throw new Exception("Unknown direction");
            };

            X.Add(current.X);
            Y.Add(current.Y);
            n++;
        }

        return ShoelaceArea(X.ToArray(), Y.ToArray(), n).ToString();
    }

    private struct PaintedCell
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Color { get; set; }
        public bool IsUpOrDown { get; set; }
    }

    private static void FloodFill(ref int[][] map, int x, int y)
    {
        Stack<(int x, int y)> stack = new();
        stack.Push((x, y));

        while (stack.Count != 0)
        {
            var c = stack.Pop();
            if (c.x >= 0 && c.x < map[0].Length && c.y >= 0 && c.y < map.Length && map[c.y][c.x] == 0)
            {
                map[c.y][c.x] = -1;
                stack.Push((c.x - 1, c.y));
                stack.Push((c.x + 1, c.y));
                stack.Push((c.x, c.y - 1));
                stack.Push((c.x, c.y + 1));
            }
        }
    }

    private long ShoelaceArea(long[] X, long[] Y, int n)
    {
        long area = 0;

        int j = n - 1;

        for (int i = 0; i < n; i++)
        {
            area += (X[j] + X[i]) * (Y[j] - Y[i]);
            j = i;
        }

        return Math.Abs(area / 2);
    }
}