using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.Arm;

namespace AdventOfCode.Y2023;

public class Day14 : AdventOfCodeDay
{
    public Day14() : base(2023, 14)
    {
    }

    protected override string SolvePart1(string[] input)
    {
        //List<string> iters = [];

        char[][] last = input.Select(_ => _.ToCharArray()).ToArray();
        for (int i = 0; i < 1000; i++)
        {
            Console.WriteLine(i);
            char[][] map = ShiftEast(ShiftSouth(ShiftWest(ShiftNorth(last))));

            Console.Write($"Map {i}:\n{string.Join('\n', map.Select(_ => new string(_)))}");
            if (string.Join(' ', map.Select(_ => new string(_))) == string.Join(' ', last.Select(_ => new string(_))))
            {
                Console.WriteLine("match!");
                break;
            }
            last = map;
        }
        //string[] map = ShiftNorth(input.Select(_ => _.ToCharArray()).ToArray());

        //foreach (var line in map)
        //{
        //    Console.WriteLine(line);
        //}

        int total = 0;
        //for (int yy = 0; yy < map.Length; yy++)
        //{
        //    for (int xx = 0; xx < map[0].Length; xx++)
        //    {
        //        if (map[yy][xx] == 'O')
        //        {
        //            total += map.Length - yy;
        //        }
        //    }
        //}

        return total.ToString();
    }

    public char[][] ShiftNorth(char[][] map)
    {
        for (int xx = 0; xx < map[0].Length; xx++)
        {
            int changed = 0;
            do
            {
                changed = 0;
                for (int yy = 0; yy < map.Length - 1; yy++)
                {
                    if (map[yy][xx] == '.' && map[yy + 1][xx] == 'O')
                    {
                        map[yy][xx] = 'O';
                        map[yy + 1][xx] = '.';
                        changed++;
                    }
                }
            }
            while (changed > 0);
        }

        return map;
    }

    public char[][] ShiftSouth(char[][] map)
    {
        for (int xx = 0; xx < map[0].Length; xx++)
        {
            int changed = 0;
            do
            {
                changed = 0;
                for (int yy = 0; yy < map.Length - 1; yy++)
                {
                    if (map[yy][xx] == 'O' && map[yy + 1][xx] == '.')
                    {
                        map[yy][xx] = '.';
                        map[yy + 1][xx] = 'O';
                        changed++;
                    }
                }
            }
            while (changed > 0);
        }

        return map;
    }

    public char[][] ShiftWest(char[][] map)
    {
        for (int yy = 0; yy < map.Length; yy++)
        {
            int changed = 0;
            do
            {
                changed = 0;
                for (int xx = 0; xx < map[0].Length - 1; xx++)
                {
                    if (map[yy][xx] == '.' && map[yy][xx + 1] == 'O')
                    {
                        map[yy][xx] = 'O';
                        map[yy][xx + 1] = '.';
                        changed++;
                    }
                }
            }
            while (changed > 0);
        }

        return map;
    }

    public char[][] ShiftEast(char[][] map)
    {
        for (int yy = 0; yy < map.Length; yy++)
        {
            int changed = 0;
            do
            {
                changed = 0;
                for (int xx = 0; xx < map[0].Length - 1; xx++)
                {
                    if (map[yy][xx] == 'O' && map[yy][xx + 1] == '.')
                    {
                        map[yy][xx] = '.';
                        map[yy][xx + 1] = 'O';
                        changed++;
                    }
                }
            }
            while (changed > 0);
        }

        return map;
    }

    protected override string SolvePart2(string[] input)
    {
        return "";
    }
}