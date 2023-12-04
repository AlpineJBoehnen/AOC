using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    internal class Day2 : ISolution
    {
        public static string Part1(string[] input)
        {
            int[] limits = [12, 13, 14]; //R G B
            int total = 0;

            foreach (string line in input)
            {
                int[] colorCounts = new int[3]; //R G B
                var parts = line.Split(',', ':', ';');
                int roundId = int.Parse(parts[0].Replace("Game ", ""));

                foreach (string part in parts.Skip(1))
                {
                    int count = int.Parse(part.Split(' ', StringSplitOptions.RemoveEmptyEntries)[0]);
                    string colorName = part.Split(' ', StringSplitOptions.RemoveEmptyEntries)[1];
                    int colorIndex = colorName == "red" ? 0 : colorName == "green" ? 1 : 2;

                    if (count > colorCounts[colorIndex])
                    {
                        colorCounts[colorIndex] = count;
                    }
                }

                if (colorCounts[0] <= limits[0] && colorCounts[1] <= limits[1] && colorCounts[2] <= limits[2])
                {
                    total += roundId;
                }
            }

            return total.ToString();
        }

        public static string Part2(string[] input)
        {
            int total = 0;

            foreach (string line in input)
            {
                int[] colorCounts = new int[3]; //R G B
                var parts = line.Split(',', ':', ';');
                int roundId = int.Parse(parts[0].Replace("Game ", ""));

                foreach (string part in parts.Skip(1))
                {
                    int count = int.Parse(part.Split(' ', StringSplitOptions.RemoveEmptyEntries)[0]);
                    string colorName = part.Split(' ', StringSplitOptions.RemoveEmptyEntries)[1];
                    int colorIndex = colorName == "red" ? 0 : colorName == "green" ? 1 : 2;

                    if (count > colorCounts[colorIndex])
                    {
                        colorCounts[colorIndex] = count;
                    }
                }

                total += (colorCounts[0] * colorCounts[1] * colorCounts[2]);
            }

            return total.ToString();
        }
    }
}
