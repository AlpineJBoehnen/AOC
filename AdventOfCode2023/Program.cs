using System.Diagnostics;

internal class Program
{
    private static void Main(string[] args)
    {
        string[] input = ReadInput();
        Stopwatch sw = Stopwatch.StartNew();

        string output = Day1_Part1(input);

        sw.Stop();
        Console.WriteLine($"Time taken: {sw.ElapsedMilliseconds}ms");
        WriteStringToFileOnDesktop("output.txt", output);
    }

    private static string Day1_Part1(string[] input)
    {
        var isDigit = (char c) =>
        {
            return int.TryParse(c.ToString(), out _);
        };

        int total = 0;
        foreach(string line in input)
        {
            char? first = line.FirstOrDefault(_ => isDigit(_));
            char? last = line.LastOrDefault(_ => isDigit(_));

            if(!first.HasValue || !last.HasValue)
            {
                continue;
            }

            total += int.Parse($"{first}{last}");
        }

        return total.ToString();
    }

    private static string[] ReadInput()
    {
        Console.WriteLine("Enter the path to the text file:");
        string? filePath;
        while(string.IsNullOrWhiteSpace(filePath = Console.ReadLine()))
        {
            Console.WriteLine("Enter the path to the text file:");
        }

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