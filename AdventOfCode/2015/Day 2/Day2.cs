namespace AdventOfCode.Y2015;

public class Day2 : AdventOfCodeDay
{
    public Day2() : base(2015, 2) { }

    protected override string SolvePart1(string[] input)
    {
        int total = 0;

        foreach(string line in input)
        {
            var dims = line.Split('x');
            var l = int.Parse(dims[0]);
            var w = int.Parse(dims[1]);
            var h = int.Parse(dims[2]);

            var side1 = l * w;
            var side2 = w * h;
            var side3 = h * l;

            var area = 2 * side1 + 2 * side2 + 2 * side3;
            area += Math.Min(Math.Min(side1, side2), side3);
            total += area;
        }

        return total.ToString();
    }

    protected override string SolvePart2(string[] input)
    {
        int total = 0;

        foreach (string line in input)
        {
            var dims = line.Split('x');
            var l = int.Parse(dims[0]);
            var w = int.Parse(dims[1]);
            var h = int.Parse(dims[2]);

            var side1 = l * w;
            var side2 = w * h;
            var side3 = h * l;

            var shortestPerimeter = 2 * Math.Min(Math.Min(l + w, w + h), h + l);
            var ribbon = l * w * h;
            total += shortestPerimeter + ribbon;
        }

        return total.ToString();
    }
}
