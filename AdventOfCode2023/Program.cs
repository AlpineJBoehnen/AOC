using AdventOfCode2023;
using System.Diagnostics;

internal class Program
{
    private static void Main(string[] args)
    {
        string[] input = ReadInput(@"C:\Users\Justin Boehnen\Desktop\input.txt");
        Stopwatch sw = Stopwatch.StartNew();

        string output = Day5.Part1(input);

        sw.Stop();
        Console.WriteLine($"Time taken: {sw.ElapsedMilliseconds}ms");
        Console.WriteLine($"Output: {output}");
        WriteStringToFileOnDesktop("output.txt", output);
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