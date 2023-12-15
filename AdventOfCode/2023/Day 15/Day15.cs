using System.Collections.Specialized;
using System.Globalization;

namespace AdventOfCode.Y2023;

public class Day15 : AdventOfCodeDay
{
    public Day15() : base(2023, 15)
    {
    }

    protected override string SolvePart1(string[] input)
    {
        ulong total = 0;
        foreach (string step in input[0].Split(','))
        {
            ulong hash = 0;
            foreach (char c in step)
            {
                hash += c;
                hash *= 17;
                hash %= 256;
            }

            total += hash;
        }

        return total.ToString();
    }

    protected override string SolvePart2(string[] input)
    {
        OrderedDictionary[] boxes = new OrderedDictionary[256];

        for (int i = 0; i < boxes.Length; i++)
        {
            boxes[i] = new OrderedDictionary();
        }

        foreach (string step in input[0].Split(','))
        {
            string label;
            if (step.Contains('-'))
            {
                label = step.TrimEnd('-');
            }
            else
            {
                label = step.Split('=')[0];
            }

            ulong hash = 0;

            foreach (char c in label)
            {
                hash += c;
                hash *= 17;
                hash %= 256;
            }

            if (step.Contains('-'))
            {
                if (boxes[hash].Contains(label))
                {
                    boxes[hash].Remove(label);
                }
            }
            else
            {
                int focalLength = int.Parse(step.Split('=')[1]);
                if (boxes[hash].Contains(label))
                {
                    boxes[hash][label] = focalLength;
                }
                else
                {
                    boxes[hash].Add(label, focalLength);
                }
            }

        }

        ulong total = 0;
        for (int i = 0; i < boxes.Length; i++)
        {
            for(int j = 0; j < boxes[i].Count; j++)
            {
                total += (ulong)((i + 1) * (j + 1) * (int)boxes[i][j]!);
            }
        }

        return total.ToString();
    }
}