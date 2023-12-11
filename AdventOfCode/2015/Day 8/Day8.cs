using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2015;

public partial class Day8 : AdventOfCodeDay
{
    public Day8() : base(2015, 8) { }

    protected override string SolvePart1(string[] input)
    {
        int total = 0;
        foreach (string line in input)
        {
            string copy = line;
            copy = copy.Replace("\\\\", "a");
            copy = copy.Replace("\\\"", "a");
            copy = copy.Replace("\"", "");
            foreach (Match match in asciiRegex().Matches(copy))
            {
                copy = copy.Replace(match.Value, "a");
            }
            total += line.Length - copy.Length;
        }
        return total.ToString();
    }

    protected override string SolvePart2(string[] input)
    {
        int total = 0;
        foreach (string line in input)
        {
            string copy = line;
            copy = copy.Replace("\\\\", "\\\\\\\\");    // \\ -> \\\\
            copy = copy.Replace("\\\"", "\\\\\"");      // \" -> \\\"
            copy = copy.Replace("\"", "\\\"");          // " -> \"
            copy = $"\"{copy}\"";
            foreach (Match match in asciiRegex().Matches(copy))
            {
                copy = copy.Replace(match.Value, "\\xxx");
            }
            total += copy.Length - line.Length;
        }
        return total.ToString();
    }

    [GeneratedRegex(@"\\x[0-f]{2}")]
    private static partial Regex asciiRegex();
}
