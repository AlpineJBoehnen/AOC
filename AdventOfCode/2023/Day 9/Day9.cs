using System.Runtime.CompilerServices;

namespace AdventOfCode.Y2023;

public class Day9 : AdventOfCodeDay
{
    public Day9() : base(2023, 9)
    {
    }

    protected override string SolvePart1(string[] input)
    {
        ulong total = 0;

        foreach (string line in input)
        {
            List<int[]> rows = [];
            rows.Add(line.Split(' ').Select(int.Parse).ToArray());

            while (rows.Last().Any(_ => _ != 0))
            {
                List<int> row = [];
                for (int ii = 0; ii < rows.Last().Length - 1; ii++)
                {
                    row.Add(rows.Last()[ii + 1] - rows.Last()[ii]);
                }

                rows.Add(row.ToArray());
            }

            int current = 0;
            for (int ii = rows.Count - 2; ii >= 0; ii--)
            {
                current += rows[ii].Last();
            }

            total += (ulong)current;
        }

        return total.ToString();
    }

    protected override string SolvePart2(string[] input)
    {
        ulong total = 0;

        foreach (string line in input)
        {
            List<int[]> rows = [];
            rows.Add(line.Split(' ').Select(int.Parse).ToArray());

            while (rows.Last().Any(_ => _ != 0))
            {
                List<int> row = [];
                for (int ii = 0; ii < rows.Last().Length - 1; ii++)
                {
                    row.Add(rows.Last()[ii + 1] - rows.Last()[ii]);
                }

                rows.Add(row.ToArray());
            }

            int current = 0;
            for (int ii = rows.Count - 2; ii >= 0; ii--)
            {
                current = rows[ii].First() - current;
            }

            total += (ulong)current;
        }

        return total.ToString();
    }
}