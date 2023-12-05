namespace AdventOfCode.Y2015;

public class Day1 : AdventOfCodeDay
{
    public Day1() : base(2015, 1) { }

    protected override string SolvePart1(string[] input)
    {
        return (input[0].Count(_ => _ == '(') - input[0].Count(_ => _ == ')')).ToString();
    }

    protected override string SolvePart2(string[] input)
    {
        int floor = 0;
        for(int i = 0; i < input[0].Length; i++)
        {
            if (input[0][i] == '(')
            {
                floor++;
            }
            else
            {
                floor--;
            } 

            if(floor == -1)
            {
                return (i + 1).ToString();
            }
        }

        throw new Exception("Santa never reaches the basement!");
    }
}
