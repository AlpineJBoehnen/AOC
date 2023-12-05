using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2015;

public partial class Day5 : AdventOfCodeDay
{
    public Day5() : base(2015, 5) { }

    protected override string SolvePart1(string[] input)
    {
        int total = 0;

        foreach (string line in input)
        {
            int vowels = 0;
            bool doubleLetter = false;
            bool badString = false;

            char prevChar = ' ';
            for (int ii = 0; ii < line.Length; ii++)
            {
                if (line[ii] == 'b' && prevChar == 'a')
                {
                    badString = true;
                    break;
                }

                if (line[ii] == 'd' && prevChar == 'c')
                {
                    badString = true;
                    break;
                }

                if (line[ii] == 'q' && prevChar == 'p')
                {
                    badString = true;
                    break;
                }

                if (line[ii] == 'y' && prevChar == 'x')
                {
                    badString = true;
                    break;
                }

                if (line[ii] == prevChar)
                {
                    doubleLetter = true;
                }

                if (line[ii] == 'a' || line[ii] == 'e' || line[ii] == 'i' || line[ii] == 'o' || line[ii] == 'u')
                {
                    vowels++;
                }

                prevChar = line[ii];
            }

            if (!badString && vowels >= 3 && doubleLetter)
            {
                total++;
            }
        }

        return total.ToString();
    }

    protected override string SolvePart2(string[] input)
    {
        int total = 0;

        foreach (string line in input)
        {
            bool letterBetweenPairs = false;

            for (int ii = 1; ii < line.Length - 1; ii ++)
            {
                if (line[ii - 1] == line[ii + 1])
                {
                    letterBetweenPairs = true;
                }
            }

            bool pair = NonOverlappingPairRegex().IsMatch(line);

            if (pair && letterBetweenPairs)
            {
                total++;
            }
        }

        return total.ToString();
    }

    [GeneratedRegex(@"(?=(\w\w)\w*\1)")]
    private static partial Regex NonOverlappingPairRegex();
}
