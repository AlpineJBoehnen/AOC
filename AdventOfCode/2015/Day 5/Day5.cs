using System.Security.Cryptography;
using System.Text;

namespace AdventOfCode.Y2015;

public class Day5 : AdventOfCodeDay
{
    public Day5() : base(2015, 5) { }

    protected override string SolvePart1(string[] input)
    {
        // trimmed blank line at end of input
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
        // trimmed blank line at end of input
        int total = 0;

        foreach (string line in input)
        {
            int upTo = 0;
            if (line.Length % 2 == 1) //odd
            {
                upTo = line.Length - 1;
            }
            else
            {
                upTo = line.Length;
            }

            HashSet<string> seenPairs = new();
            bool letterBetweenPairs = false;
            bool pair = false;

            for (int ii = 1; ii < line.Length - 1; ii ++)
            {
                if (line[ii - 1] == line[ii + 1])
                {
                    letterBetweenPairs = true;
                }
            }

            char prevChar = ' ';
            for (int ii = 0; ii < upTo; ii += 2)
            {
                char nextChar = line[ii + 1];

                if (prevChar == line[ii] && line[ii] == nextChar)
                {
                    if (seenPairs.Contains($"{prevChar}{line[ii]}"))
                    {
                        pair = true;
                    }
                    else
                    {
                        seenPairs.Add($"{prevChar}{line[ii]}");
                    }
                }
                else
                {
                    if (seenPairs.Contains($"{prevChar}{line[ii]}"))
                    {
                        pair = true;
                    }
                    else
                    {
                        seenPairs.Add($"{prevChar}{line[ii]}");
                    }

                    if (seenPairs.Contains($"{line[ii]}{nextChar}"))
                    {
                        pair = true;
                    }
                    else
                    {
                        seenPairs.Add($"{line[ii]}{nextChar}");
                    }
                }

                prevChar = nextChar;
            }

            //last char if odd length
            if (line.Length % 2 == 1)
            {
                if (line[^1] == line[^0])
                {
                    if (seenPairs.Contains($"{line[^2]}{line[^1]}"))
                    {
                        pair = true;
                    }
                }
            }

            if (pair && letterBetweenPairs)
            {
                total++;
            }
        }

        return total.ToString();
    }
}
