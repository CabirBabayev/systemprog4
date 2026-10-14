﻿using System.Text.RegularExpressions;
FileStatistics ProcessFile(string filePath)
{
    string text = File.ReadAllText(filePath);
    FileStatistics statistics = new FileStatistics
    {
        Characters = text.Length,
        Words = Regex.Matches(text, @"\b\w+\b").Count,
        Lines = File.ReadAllLines(filePath).Length,
        Errors = Regex.Matches(text, @"\berror\b", RegexOptions.IgnoreCase).Count
    };
    return statistics;
}
string[] files = Directory.GetFiles("Files", "*.txt");
FileStatistics[] results = new FileStatistics[files.Length];
Parallel.ForEach(
    Enumerable.Range(0, files.Length),i =>
    {
        results[i] = ProcessFile(files[i]);
        Console.WriteLine(Path.GetFileName(files[i]));
        Console.WriteLine($"Characters: {results[i].Characters}");
        Console.WriteLine($"Words: {results[i].Words}");
        Console.WriteLine($"Lines: {results[i].Lines}");
        Console.WriteLine($"Errors: {results[i].Errors}");
    });
int totalCharacters = 0;
int totalWords = 0;
int totalLines = 0;
int totalErrors = 0;
foreach (FileStatistics statistics in results)
{
    totalCharacters += statistics.Characters;
    totalWords += statistics.Words;
    totalLines += statistics.Lines;
    totalErrors += statistics.Errors;
}
Console.WriteLine("===== TOTAL =====");
Console.WriteLine($"Files: {files.Length}");
Console.WriteLine($"Characters: {totalCharacters}");
Console.WriteLine($"Words: {totalWords}");
Console.WriteLine($"Lines: {totalLines}");
Console.WriteLine($"Errors: {totalErrors}");
class FileStatistics
{
    public int Characters { get; set; }
    public int Words { get; set; }
    public int Lines { get; set; }
    public int Errors { get; set; }
}