using System.Diagnostics;

internal class Program
{
    private static void Main(string[] args)
    {
        string[] input = ReadInput();
        Stopwatch sw = Stopwatch.StartNew();

        Day1_Part1(input);
    }

    private static void Day1_Part1(string[] input)
    {
        var isDigit = (char c) =>
        {
            return int.TryParse
        };
        int total = 0;
        foreach(string line in input)
        {
            
        }
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