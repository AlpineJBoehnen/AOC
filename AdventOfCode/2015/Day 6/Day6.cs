using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2015;

public partial class Day6 : AdventOfCodeDay
{
    public Day6() : base(2015, 6) { }

    protected override string SolvePart1(string[] input)
    {
        bool[,] matrix = new bool[1000, 1000];
        foreach(string line in input)
        {
            int[] numbers = NumberRegex().Matches(line).Select(_ => int.Parse(_.Value)).ToArray();

            if(line.StartsWith("turn on"))
            {
                TurnOn((numbers[0], numbers[1]), (numbers[2], numbers[3]), ref matrix);
            }
            else if (line.StartsWith("turn off"))
            {
                TurnOff((numbers[0], numbers[1]), (numbers[2], numbers[3]), ref matrix);
            }
            else // toggle
            {
                Toggle((numbers[0], numbers[1]), (numbers[2], numbers[3]), ref matrix);
            }
        }

        return Count(true, ref matrix).ToString();
    }

    protected override string SolvePart2(string[] input)
    {
        int[,] matrix = new int[1000, 1000];
        foreach (string line in input)
        {
            int[] numbers = NumberRegex().Matches(line).Select(_ => int.Parse(_.Value)).ToArray();

            if (line.StartsWith("turn on"))
            {
                Increase(1, (numbers[0], numbers[1]), (numbers[2], numbers[3]), ref matrix);
            }
            else if (line.StartsWith("turn off"))
            {
                Decrease(1, (numbers[0], numbers[1]), (numbers[2], numbers[3]), ref matrix);
            }
            else // toggle
            {
                Increase(2, (numbers[0], numbers[1]), (numbers[2], numbers[3]), ref matrix);
            }
        }

        return Brightness(true, ref matrix).ToString();
    }

    private static void TurnOn((int, int) a, (int, int) b, ref bool[,] matrix)
    {
        for(int xx = a.Item1; xx <= b.Item1; xx++)
        { 
            for (int yy = a.Item2; yy <= b.Item2; yy++)
            {
                matrix[xx, yy] = true;
            }
        }
    }

    private static void TurnOff((int, int) a, (int, int) b, ref bool[,] matrix)
    {
        for (int xx = a.Item1; xx <= b.Item1; xx++)
        {
            for (int yy = a.Item2; yy <= b.Item2; yy++)
            {
                matrix[xx, yy] = false;
            }
        }
    }

    private static void Toggle((int, int) a, (int, int) b, ref bool[,] matrix)
    {
        for (int xx = a.Item1; xx <= b.Item1; xx++)
        {
            for (int yy = a.Item2; yy <= b.Item2; yy++)
            {
                matrix[xx, yy] = !matrix[xx, yy];
            }
        }
    }

    private static int Count(bool condition, ref bool[,] matrix)
    {
        int count = 0;
        for (int xx = 0; xx < 1000; xx++)
        {
            for (int yy = 0; yy < 1000; yy++)
            {
                if(matrix[xx, yy] == condition)
                {
                    count++;
                }
            }
        }

        return count;
    }

    private static void Increase(int amount, (int, int) a, (int, int) b, ref int[,] matrix)
    {
        for (int xx = a.Item1; xx <= b.Item1; xx++)
        {
            for (int yy = a.Item2; yy <= b.Item2; yy++)
            {
                matrix[xx, yy] += amount;
            }
        }
    }

    private static void Decrease(int amount, (int, int) a, (int, int) b, ref int[,] matrix)
    {
        for (int xx = a.Item1; xx <= b.Item1; xx++)
        {
            for (int yy = a.Item2; yy <= b.Item2; yy++)
            {
                matrix[xx, yy] -= Math.Min(amount, matrix[xx, yy]);
            }
        }
    }

    private static int Brightness(bool condition, ref int[,] matrix)
    {
        int count = 0;
        for (int xx = 0; xx < 1000; xx++)
        {
            for (int yy = 0; yy < 1000; yy++)
            {
                count += matrix[xx, yy];
            }
        }

        return count;
    }

    [GeneratedRegex(@"\d+")]
    private static partial Regex NumberRegex();
}
