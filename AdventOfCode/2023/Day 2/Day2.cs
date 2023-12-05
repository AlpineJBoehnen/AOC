namespace AdventOfCode.Y2023;

public class Day2 : AdventOfCodeDay
{
    public Day2() : base(2023, 2) { }

    protected override string SolvePart1(string[] input)
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

    protected override string SolvePart2(string[] input)
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
