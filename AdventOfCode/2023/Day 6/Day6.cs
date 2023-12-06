namespace AdventOfCode.Y2023;

public class Day6 : AdventOfCodeDay
{
    public Day6() : base(2023, 6)
    {
    }

    protected override string SolvePart1(string[] input)
    {
        int total = 1;
        int[] times = input[0].Remove(0, 5).Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(_ => int.Parse(_)).ToArray();
        int[] distances = input[1].Remove(0, 9).Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(_ => int.Parse(_)).ToArray();

        for(int ii = 0; ii < times.Length; ii++)
        {
            int won = 0;
            for(int secsHeld = 1; secsHeld < times[ii] - 1; secsHeld++)
            {
                if(secsHeld * (times[ii] - secsHeld) > distances[ii])
                {
                    won++;
                }
            }

            total *= won;
        }

        return total.ToString();
    }

    protected override string SolvePart2(string[] input)
    {
        ulong total = 1;
        ulong[] times = input[0].Remove(0, 5).Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(_ => ulong.Parse(_)).ToArray();
        ulong[] distances = input[1].Remove(0, 9).Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(_ => ulong.Parse(_)).ToArray();

        for (ulong ii = 0; ii < (ulong)times.Length; ii++)
        {
            ulong won = 0;
            for (ulong secsHeld = 1; secsHeld < times[ii] - 1; secsHeld++)
            {
                if (secsHeld * (times[ii] - secsHeld) > distances[ii])
                {
                    won++;
                }
            }

            total *= won;
        }

        return total.ToString();
    }
}