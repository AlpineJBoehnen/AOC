using AdventOfCode2023;
using System.Diagnostics;

internal class Program
{
    private static void Main(string[] args)
    {
        string[] input = ReadInput(@"C:\Users\unive\Desktop\input.txt");
        Stopwatch sw = Stopwatch.StartNew();

        string output = Day3.Part2(input);

        sw.Stop();
        Console.WriteLine($"Time taken: {sw.ElapsedMilliseconds}ms");
        Console.WriteLine($"Output: {output}");
        WriteStringToFileOnDesktop("output.txt", output);
    }

    private static string Day1_Part1(string[] input)
    {
        var isDigit = (char c) =>
        {
            return int.TryParse(c.ToString(), out _);
        };

        int total = 0;
        foreach (string line in input)
        {
            char? first = line.FirstOrDefault(_ => isDigit(_));
            char? last = line.LastOrDefault(_ => isDigit(_));

            if (!first.HasValue || !last.HasValue)
            {
                continue;
            }

            total += int.Parse($"{first}{last}");
        }

        return total.ToString();
    }

    private static string Day1_Part2(string[] input)
    {
        Dictionary<string, string> numbers = new()
        {
            {"1", "1"},
            {"one", "1"},
            {"2", "2"},
            {"two", "2"},
            {"3", "3"},
            {"three", "3"},
            {"4", "4"},
            {"four", "4"},
            {"5", "5"},
            {"five", "5"},
            {"6", "6"},
            {"six", "6"},
            {"7", "7"},
            {"seven", "7"},
            {"8", "8"},
            {"eight", "8"},
            {"9", "9"},
            {"nine", "9"}
        };

        int total = 0;

        foreach (string line in input)
        {
            int lowest = int.MaxValue;
            string? lowestKey = null;
            int highest = -1;
            string? highestKey = null;

            foreach (string key in numbers.Keys)
            {
                int firstInd = line.IndexOf(key);

                if (firstInd != -1 && firstInd < lowest)
                {
                    lowest = firstInd;
                    lowestKey = key;
                }

                int lastInd = line.LastIndexOf(key);

                if (lastInd != -1 && lastInd > highest)
                {
                    highest = lastInd;
                    highestKey = key;
                }
            }
            if (lowestKey != null && highestKey != null)
            {
                total += int.Parse($"{numbers[lowestKey]}{numbers[highestKey]}");
            }
            else
            {
                throw new Exception();
            }
        }

        return total.ToString();
    }

    private static string Day2_Part1(string[] input)
    {
        int[] limits = [12, 13, 14]; //R G B
        int total = 0;

        foreach(string line in input)
        {
            int[] colorCounts = new int[3]; //R G B
            var parts = line.Split(',', ':', ';');
            int roundId = int.Parse(parts[0].Replace("Game ", ""));

            foreach(string part in parts.Skip(1))
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

    private static string Day2_Part2(string[] input)
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

    private static string[] ReadInput()
    {
        string? filePath;
        while (string.IsNullOrWhiteSpace(filePath = Console.ReadLine()))
        {
            Console.WriteLine("Enter the path to the text file:");
        }

        return File.ReadAllLines(filePath);
    }

    private static string[] ReadInput(string filePath)
    {
        return File.ReadAllLines(filePath);
    }

    static void WriteStringToFileOnDesktop(string fileName, string content)
    {
        try
        {
            // Get the desktop path
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            // Combine the desktop path and the file name
            string filePath = Path.Combine(desktopPath, fileName);

            // Write the content to the file
            File.WriteAllText(filePath, content);

            Console.WriteLine($"File '{fileName}' has been written to the desktop.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}