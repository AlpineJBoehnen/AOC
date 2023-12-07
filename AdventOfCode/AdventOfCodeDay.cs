using System.Diagnostics;

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
        _year = year;

        string dayFolder = $"Day {day}";
        _fullInputPath = Path.Combine(dayFolder, "input.txt");
        _exampleInputPath = Path.Combine(dayFolder, "example.txt");
    }

    public void Run(bool useExampleInput = false)
    {
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
        string resultPart1 = SolvePart1(ReadPart1Input(useExampleInput));
        sw.Stop();
        Console.WriteLine($"Part 1: {resultPart1} ({sw.ElapsedMilliseconds} ms)");

        // Part 2
        sw.Restart();
        string resultPart2 = SolvePart2(ReadPart2Input(useExampleInput));
        sw.Stop();
        Console.WriteLine($"Part 2: {resultPart2} ({sw.ElapsedMilliseconds} ms)");
    }

    protected abstract string SolvePart1(string[] input);
    protected abstract string SolvePart2(string[] input);

    private string[] ReadPart1Input(bool useExampleInput)
    {
        string root = Path.Combine(Environment.CurrentDirectory, $"..\\..\\..\\{_year}", $"Day {_day}");
        if (useExampleInput)
        {
            return File.ReadAllLines(Path.Combine(root, "example.txt"));
        }
        try
        {
            return File.ReadAllLines(Path.Combine(root, "input.txt"));
        }
        catch
        {
            return File.ReadAllLines(Path.Combine(root, "part1.txt"));
        }
    }

    private string[] ReadPart2Input(bool useExampleInput)
    {
        string root = Path.Combine(Environment.CurrentDirectory, $"..\\..\\..\\{_year}", $"Day {_day}");
        if (useExampleInput)
        {
            return File.ReadAllLines(Path.Combine(root, "example.txt"));
        }
        try
        {
            return File.ReadAllLines(Path.Combine(root, "input.txt"));
        }
        catch
        {
            return File.ReadAllLines(Path.Combine(root, "part2.txt"));
        }
    }
}
