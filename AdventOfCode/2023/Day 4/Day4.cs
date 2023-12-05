namespace AdventOfCode.Y2023;

public class Day4 : AdventOfCodeDay
{
    public Day4() : base(2023, 4) { }

    protected override string SolvePart1(string[] input)
    {
        int total = 0;
        foreach (string line in input)
        {
            int matches = 0;
            var values = line.Split(':')[1];
            var twoSets = values.Split('|');
            var winning = twoSets[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var picked = twoSets[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);

            foreach (var pick in picked)
            {
                if (winning.Contains(pick))
                {
                    matches++;
                }
            }

            if (matches > 0)
            {
                total += (int)Math.Pow(2, matches - 1);
            }
        }

        return total.ToString();
    }

    protected override string SolvePart2(string[] input)
    {
        int total = 0;
        int[] matches = new int[input.Length];

        for (int ii = 0; ii < input.Length; ii++)
        {
            int matching = 0;
            var values = input[ii].Split(':')[1];
            var twoSets = values.Split('|');
            var winning = twoSets[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var picked = twoSets[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);

            foreach (var pick in picked)
            {
                if (winning.Contains(pick))
                {
                    matching++;
                }
            }

            matches[ii] = matching;
        }

        for (int ii = 0; ii < matches.Length; ii++)
        {
            total += GetCardsWon(matches, ii);
        }

        total += matches.Length;

        return total.ToString();
    }

    private static int GetCardsWon(int[] matches, int cardId)
    {
        int total = 0;

        total += matches[cardId];

        for (int ii = 0; ii < matches[cardId]; ii++)
        {
            total += GetCardsWon(matches, cardId + ii + 1);
        }

        return total;
    }
}
