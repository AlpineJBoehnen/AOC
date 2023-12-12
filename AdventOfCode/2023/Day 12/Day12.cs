using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.Arm;

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

    // 23560973677 too low

    protected override string SolvePart2(string[] input)
    {
        ulong total = 0;
        foreach (string line in input)
        {
            var parts = line.Split(' ');
            string lne = string.Join('?', parts[0], parts[0], parts[0], parts[0], parts[0]);
            string dmgGrps = string.Join(',', parts[1], parts[1], parts[1], parts[1], parts[1]);
            ulong inc = CountArrangementsDp(lne.ToCharArray(), dmgGrps.Split(',').Select(_ => int.Parse(_)).ToArray(), 0, 0, 0);
            Debug.WriteLine($"{line} - {inc}");
            total += inc;
            _dp.Clear();
        }

        return total.ToString();
    }

    private static int CountArrangements(string line, int[] damagedGroups)
    {
        int total = 0;
        var combos = GenerateCombinations(line.ToCharArray(), 0);
        foreach (string arrangement in combos)
        {
            int[] groups = arrangement.Split('.', StringSplitOptions.RemoveEmptyEntries).Select(_ => _.Length).ToArray();
            if (groups.Length == damagedGroups.Length)
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

    private static Dictionary<(int, int, int), ulong> _dp = [];

    private static ulong CountArrangementsDp(char[] line, int[] damagedGroups, int lineIndex, int groupIndex, int groupLength)
    {
        if (_dp.TryGetValue((lineIndex, groupIndex, groupLength), out ulong cached))
        {
            return cached;
        }

        if(lineIndex == line.Length)
        {
            if (groupIndex == damagedGroups.Length && groupLength == 0)
            {
                return 1;
            }
            else if(groupIndex == damagedGroups.Length - 1 && damagedGroups[groupIndex] == groupLength)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        ulong total = 0;

        if (line[lineIndex] == '.' || line[lineIndex] == '?')
        {
            if(groupLength == 0)
            {
                total += CountArrangementsDp(line, damagedGroups, lineIndex + 1, groupIndex, 0);
            }
            else if (groupLength>0 && groupIndex < damagedGroups.Length && damagedGroups[groupIndex] == groupLength)
            {
                total += CountArrangementsDp(line, damagedGroups, lineIndex + 1, groupIndex + 1, 0);
            }
        }

        if (line[lineIndex] == '#' || line[lineIndex] == '?')
        {
            total += CountArrangementsDp(line, damagedGroups, lineIndex + 1, groupIndex, groupLength + 1);
        }

        return _dp[(lineIndex, groupIndex, groupLength)] = total;
    }
}