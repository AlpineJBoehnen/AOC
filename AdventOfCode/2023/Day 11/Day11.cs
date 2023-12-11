using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace AdventOfCode.Y2023;

public class Day11 : AdventOfCodeDay
{
    public Day11() : base(2023, 11)
    {
    }

    protected override string SolvePart1(string[] input)
    {
        List<string> expansionList = [];

        foreach (string line in input)
        {
            string temp = "";
            for (int x = 0; x < line.Length; x++)
            {
                bool hasGalaxy = false;
                for (int y = 0; y < input.Length; y++)
                {
                    if (input[y][x] == '#')
                    {
                        hasGalaxy = true;
                    }
                }

                temp += line[x];
                if (!hasGalaxy)
                {
                    temp += line[x];
                }
            }

            expansionList.Add(temp);
            if (!temp.Contains('#'))
            {
                expansionList.Add(temp);
            }
        }

        List<(int x, int y)> galaxies = [];
        for (int y = 0; y < expansionList.Count; y++)
        {
            for (int x = 0; x < expansionList[y].Length; x++)
            {
                if (expansionList[y][x] == '#')
                {
                    galaxies.Add((x, y));
                }
            }
        }

        int total = 0;
        foreach (var a in galaxies)
        {
            foreach (var b in galaxies)
            {
                if (a == b)
                {
                    continue;
                }

                total += Math.Abs(a.x - b.x) + Math.Abs(a.y - b.y);
            }
        }

        return (total / 2).ToString();
    }

    protected override string SolvePart2(string[] input)
    {
        List<string> expansionList = [];

        foreach (string line in input)
        {
            string temp = "";
            for (int x = 0; x < line.Length; x++)
            {
                bool hasGalaxy = false;
                for (int y = 0; y < input.Length; y++)
                {
                    if (input[y][x] == '#')
                    {
                        hasGalaxy = true;
                    }
                }

                if (hasGalaxy)
                {
                    temp += line[x];
                }
                else
                {
                    temp += 'x';
                }
            }

            if(temp.Contains('#'))
            {
                expansionList.Add(temp);
            }
            else
            {
                expansionList.Add(new string('x', temp.Length));
            }
        }

        List<(int x, int y)> galaxies = [];
        for (int y = 0; y < expansionList.Count; y++)
        {
            for (int x = 0; x < expansionList[y].Length; x++)
            {
                if (expansionList[y][x] == '#')
                {
                    galaxies.Add((x, y));
                }
            }
        }

        ulong total = 0;
        foreach (var a in galaxies)
        {
            foreach (var b in galaxies)
            {
                if (a == b)
                {
                    continue;
                }

                int horizontalExpansions = expansionList[a.y][Math.Min(a.x, b.x)..(Math.Max(a.x, b.x) + 1)].Count(c => c == 'x');

                int verticalExpansions = 0;
                foreach (var line in expansionList[Math.Min(a.y, b.y)..(Math.Max(a.y, b.y) + 1)])
                {
                    if (line[a.x] == 'x')
                    {
                        verticalExpansions++;
                    }
                }

                int expansion = 1_000_000;
                ulong add = (ulong)(Math.Abs(a.x - b.x) + Math.Abs(a.y - b.y) + (horizontalExpansions * (expansion - 1)) + (verticalExpansions * (expansion - 1)));
                total += add;
            }
        }

        //string testOutput = "";
        //foreach(string line in expansionList)
        //{
        //    testOutput += line + "\n";
        //}

        ulong half = total / 2;
        return half.ToString();
    }
}