using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace AdventOfCode.Y2023;

public class Day12 : AdventOfCodeDay
{
    public Day12() : base(2023, 12)
    {
    }

    /*
?###???????? 3,2,1
.###.##.#...
.###.##..#..
.###.##...#.
.###.##....#
.###..##.#..
.###..##..#.
.###..##...#
.###...##.#.
.###...##..#
.###....##.#
     */

    protected override string SolvePart1(string[] input)
    {
        long total = 0;
        foreach (string line in input)
        {
            var parts = line.Split(' ');
            var dmgGrps = parts[1].Split(',').Select(_ => int.Parse(_)).ToArray();
            total += CountArrangements(parts[0], dmgGrps);
        }

        return total.ToString();
    }

    protected override string SolvePart2(string[] input)
    {
        return "";
    }

    private static int CountArrangements(string line, int[] damagedGroups)
    {
        int total = 0;
        var combos = GenerateCombinations(line.ToCharArray(), 0);
        foreach (string arrangement in combos)
        {
            int[] groups = arrangement.Split('.', StringSplitOptions.RemoveEmptyEntries).Select(_ => _.Length).ToArray();
            if(groups.Length == damagedGroups.Length)
            {
                bool valid = true;
                for (int i = 0; i < groups.Length; i++)
                {
                    if (groups[i] != damagedGroups[i])
                    {
                        valid = false;
                        break;
                    }
                }

                if (valid)
                {
                    total++;
                }
            }
        }

        return total;
    }

    private static string[] GenerateCombinations(char[] line, int index)
    {
        List<string> combinations = new();
        if (index == line.Length)
        {
            combinations.Add(new string(line));
            return combinations.ToArray();
        }

        if (line[index] == '?')
        {
            line[index] = '.';
            combinations.AddRange(GenerateCombinations(line, index + 1));
            line[index] = '#';
            combinations.AddRange(GenerateCombinations(line, index + 1));
            line[index] = '?';
        }
        else
        {
            combinations.AddRange(GenerateCombinations(line, index + 1));
        }

        return combinations.ToArray();
    }
}