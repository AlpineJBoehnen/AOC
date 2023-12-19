using System.Collections.Specialized;
using System.Drawing;
using System.Globalization;

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

        int[,] paint = new int[height, width];
        bool[,] upsAndDowns = new bool[height, width];

        foreach (PaintedCell cell in cells)
        {
            paint[cell.Y + offsetY, cell.X + offsetX] = cell.Color;
            upsAndDowns[cell.Y + offsetY, cell.X + offsetX] = cell.IsUpOrDown;
        }

        for (int yy = 0; yy < height; yy++)
        {
            for (int xx = 0; xx < width; xx++)
            {
                Console.Write(paint[yy, xx] == 0 ? '.' : '#');
            }
            Console.Write('\n');
        }

        int total = 0;
        for (int yy = 0; yy < height; yy++)
        {
            for (int xx = 0; xx < width; xx++)
            {
                if (paint[yy, xx] != 0)
                {
                    total++;
                    continue;
                }

                int t = 0;
            }
        }


        return total.ToString();
    }

    protected override string SolvePart2(string[] input)
    {
        return "";
    }

    private struct PaintedCell
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Color { get; set; }
        public bool IsUpOrDown { get; set; }
    }
}