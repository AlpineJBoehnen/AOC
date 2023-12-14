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
        Dictionary<int, List<int>> scores = [];
        char[][] last = input.Select(_ => _.ToCharArray()).ToArray();
        for (int i = 0; i < 500; i++)
        {
            ShiftEast(ShiftSouth(ShiftWest(ShiftNorth(last))));

            int score = 0;
            for (int yy = 0; yy < last.Length; yy++)
            {
                for (int xx = 0; xx < last[0].Length; xx++)
                {
                    if (last[yy][xx] == 'O')
                    {
                        score += last.Length - yy;
                    }
                }
            }

            if(!scores.ContainsKey(score))
            {
                scores[score] = new List<int>();
            }
            else
            {
                scores[score].Add(i);
            }

            Console.WriteLine($"{i}: {score} [{string.Join(", ", scores[score])}]");
        }

        return "";
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