using AdventOfCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode_2023
{
    public class Day1 : AdventOfCodeDay
    {
        public Day1() : base(2023, 1) { }

        protected override string SolvePart1(string[] input)
        {
            static bool isDigit(char c)
            {
                return int.TryParse(c.ToString(), out _);
            }

            int total = 0;
            foreach (string line in input)
            {
                char? first = line.FirstOrDefault(_ => isDigit(_));
                char? last = line.LastOrDefault(_ => isDigit(_));

                if (!first.HasValue || !last.HasValue)
                {
                    continue;
                }

                total += int.Parse($"{first}{last}");
            }

            return total.ToString();
        }

        protected override string SolvePart2(string[] input)
        {
            Dictionary<string, string> numbers = new()
            {
                {"1", "1"},
                {"one", "1"},
                {"2", "2"},
                {"two", "2"},
                {"3", "3"},
                {"three", "3"},
                {"4", "4"},
                {"four", "4"},
                {"5", "5"},
                {"five", "5"},
                {"6", "6"},
                {"six", "6"},
                {"7", "7"},
                {"seven", "7"},
                {"8", "8"},
                {"eight", "8"},
                {"9", "9"},
                {"nine", "9"}
            };

            int total = 0;

            foreach (string line in input)
            {
                int lowest = int.MaxValue;
                string? lowestKey = null;
                int highest = -1;
                string? highestKey = null;

                foreach (string key in numbers.Keys)
                {
                    int firstInd = line.IndexOf(key);

                    if (firstInd != -1 && firstInd < lowest)
                    {
                        lowest = firstInd;
                        lowestKey = key;
                    }

                    int lastInd = line.LastIndexOf(key);

                    if (lastInd != -1 && lastInd > highest)
                    {
                        highest = lastInd;
                        highestKey = key;
                    }
                }
                if (lowestKey != null && highestKey != null)
                {
                    total += int.Parse($"{numbers[lowestKey]}{numbers[highestKey]}");
                }
                else
                {
                    throw new Exception();
                }
            }

            return total.ToString();
        }
    }
}
