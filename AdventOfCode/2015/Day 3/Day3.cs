namespace AdventOfCode.Y2015;

public class Day3 : AdventOfCodeDay
{
    public Day3() : base(2015, 3) { }

    protected override string SolvePart1(string[] input)
    {
        HashSet<string> visitedCoords = new();

        (int, int) coord = (int.MaxValue / 2, int.MaxValue / 2);

        // initial location
        visitedCoords.Add($"{coord.Item1},{coord.Item2}");

        foreach (char c in input[0])
        {
            switch (c)
            {
                case '^':
                    coord.Item2++;
                    break;
                case 'v':
                    coord.Item2--;
                    break;
                case '<':
                    coord.Item1--;
                    break;
                case '>':
                    coord.Item1++;
                    break;
            }

            visitedCoords.Add($"{coord.Item1},{coord.Item2}");
        }

        return visitedCoords.Count.ToString();
    }

    protected override string SolvePart2(string[] input)
    {
        HashSet<string> visitedCoords = new();

        (int, int) santaCoord = (int.MaxValue / 2, int.MaxValue / 2);
        (int, int) roboSantaCoord = (int.MaxValue / 2, int.MaxValue / 2);

        // initial location
        visitedCoords.Add($"{santaCoord.Item1},{santaCoord.Item2}");

        bool santasTurn = false;
        foreach (char c in input[0])
        {
            santasTurn = !santasTurn;
            switch (c)
            {
                case '^':
                    if (santasTurn)
                    {
                        santaCoord.Item2++;
                    }
                    else
                    {
                        roboSantaCoord.Item2++;
                    }
                    break;
                case 'v':
                    if (santasTurn)
                    {
                        santaCoord.Item2--;
                    }
                    else
                    {
                        roboSantaCoord.Item2--;
                    }
                    break;
                case '<':
                    if (santasTurn)
                    {
                        santaCoord.Item1--;
                    }
                    else
                    {
                        roboSantaCoord.Item1--;
                    }
                    break;
                case '>':
                    if (santasTurn)
                    {
                        santaCoord.Item1++;
                    }
                    else
                    {
                        roboSantaCoord.Item1++;
                    }
                    break;
            }

            if (santasTurn)
            {
                visitedCoords.Add($"{santaCoord.Item1},{santaCoord.Item2}");
            }
            else
            {
                visitedCoords.Add($"{roboSantaCoord.Item1},{roboSantaCoord.Item2}");
            }
        }

        return (visitedCoords.Count).ToString();
    }
}
