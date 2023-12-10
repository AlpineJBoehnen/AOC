namespace AdventOfCode.Y2023;

public class Day8 : AdventOfCodeDay
{
    public Day8() : base(2023, 8)
    {
    }

    protected override string SolvePart1(string[] input)
    {
        bool[] lefts = input[0].Select(c => c == 'L').ToArray();

        Dictionary<string, (string, string)> nodes = [];

        for (int ii = 2; ii < input.Length; ii++)
        {
            string[] nodeLeftRight = input[ii].Split(new char[] { ' ', '=', ',', '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
            nodes[nodeLeftRight[0]] = (nodeLeftRight[1], nodeLeftRight[2]);
        }

        string currentNode = "AAA";
        int steps = 0;
        while(currentNode != "ZZZ")
        {
            int leftIndex = steps % lefts.Length;
            steps++;
            (string left, string right) = nodes[currentNode];
            if (lefts[leftIndex])
            {
                currentNode = left;
            }
            else
            {
                currentNode = right;
            }
        }
        return steps.ToString();
    }

    protected override string SolvePart2(string[] input)
    {
        bool[] lefts = input[0].Select(c => c == 'L').ToArray();

        Dictionary<string, (string, string)> nodes = [];
        //List<string> Starts = [];
        List<string> Paths = [];
        List<uint> Lengths = [];

        for (int ii = 2; ii < input.Length; ii++)
        {
            string[] nodeLeftRight = input[ii].Split(new char[] { ' ', '=', ',', '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
            nodes[nodeLeftRight[0]] = (nodeLeftRight[1], nodeLeftRight[2]);

            if (nodeLeftRight[0].EndsWith('A'))
            {
                //Starts.Add(nodeLeftRight[0]);
                Paths.Add(nodeLeftRight[0]);
                Lengths.Add(0);
            }
        }

        int steps = 0;
        int mils = 1;
        while (Lengths.Any(_ => _ == 0))
        {
            int leftIndex = steps % lefts.Length;
            steps++;
            if(steps % 1000000 == 0)
            {
                Console.WriteLine($"{mils++} million");
            }
            for (int ii = 0; ii < Paths.Count; ii++)
            {
                if (Lengths[ii] != 0)
                {
                    continue;
                }

                (string left, string right) = nodes[Paths[ii]];
                if (lefts[leftIndex])
                {
                    Paths[ii] = left;
                }
                else
                {
                    Paths[ii] = right;
                }

                if (Paths[ii].EndsWith('Z'))
                {
                    Lengths[ii] = (uint)steps;
                }
            }
        }

        var res = LCM(Lengths.ToArray());
        return res.ToString();
    }

    static ulong LCM(uint[] numbers)
    {
        ulong lcm = numbers[0];

        for (int ii = 1; ii < numbers.Length; ii++)
        {
            lcm = (lcm * numbers[ii]) / GCF(lcm, numbers[ii]);
        }

        return lcm;
    }

    static ulong GCF(ulong a, ulong b)
    {
        while (b != 0)
        {
            ulong temp = b;
            b = a % b;
            a = temp;
        }

        return a;
    }
}