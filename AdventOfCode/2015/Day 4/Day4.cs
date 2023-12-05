using System.Security.Cryptography;
using System.Text;

namespace AdventOfCode.Y2015;

public class Day4 : AdventOfCodeDay
{
    public Day4() : base(2015, 4) { }

    protected override string SolvePart1(string[] input)
    {
        string key = input[0];
        string hash = "";

        int number = 0;
        while (!hash.StartsWith("00000"))
        {
            hash = Convert.ToHexString(MD5.HashData(Encoding.UTF8.GetBytes($"{key}{number++}")));
        }

        return (number - 1).ToString();
    }

    protected override string SolvePart2(string[] input)
    {
        string key = input[0];
        string hash = "";

        int number = 0;
        while (!hash.StartsWith("000000"))
        {
            hash = Convert.ToHexString(MD5.HashData(Encoding.UTF8.GetBytes($"{key}{number++}")));
        }

        return (number - 1).ToString();
    }
}
