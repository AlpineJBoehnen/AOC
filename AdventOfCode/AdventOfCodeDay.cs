using System.Diagnostics;
using System.Reflection;

namespace AdventOfCode;

public abstract class AdventOfCodeDay
{
    private string _fullInputPath;
    private string _exampleInputPath;
    private int _year;
    private int _day;

    protected AdventOfCodeDay(int year, int day)
    {
        _day = day;

        string dayFolder = $"Day {day}";
        _fullInputPath = Path.Combine(dayFolder, "input.txt");
        _exampleInputPath = Path.Combine(dayFolder, "example.txt");
    }

    public void Run(bool useExampleInput = false)
    {
        string[] input = useExampleInput ? ReadInput(_exampleInputPath) : ReadInput(_fullInputPath);

        if (useExampleInput)
        {
            Console.WriteLine($"Advent of Code {_year} (Day {_day}) (Example Input)\n");
        }
        else
        {
            Console.WriteLine($"Advent of Code {_year} (Day {_day})\n");
        }

        // Part 1
        Stopwatch sw = Stopwatch.StartNew();
        string resultPart1 = SolvePart1(input);
        sw.Stop();
        Console.WriteLine($"Part 1: {resultPart1} ({sw.ElapsedMilliseconds} ms)");

        // Part 2
        sw.Restart();
        string resultPart2 = SolvePart2(input);
        sw.Stop();
        Console.WriteLine($"Part 2: {resultPart2} ({sw.ElapsedMilliseconds} ms)");
    }

    protected abstract string SolvePart1(string[] input);
    protected abstract string SolvePart2(string[] input);

    private static string[] ReadInput(string filePath)
    {
        // Construct the full path to the file relative to the assembly location
        string fullPath = Path.Combine(Environment.CurrentDirectory, $"..\\..\\..\\2023", filePath);

        return File.ReadAllLines(fullPath);
    }
}
