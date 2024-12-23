using System;
using System.Collections.Generic;
using System.IO;

public class Scoreboard
{
    private readonly string filePath;
    private List<(string playerName, int score)> scores;

    public Scoreboard(string filePath)
    {
        this.filePath = filePath;
        scores = new List<(string, int)>();
        LoadScores();
    }

    public void AddScore(string playerName, int score)
    {
        scores.Add((playerName, score));
        SaveScores();
    }

    public List<string> GetTopScoresFormatted(int topN = 10)
    {
        scores.Sort((a, b) => b.score.CompareTo(a.score));
        var topScores = scores.GetRange(0, Math.Min(topN, scores.Count));
        List<string> formattedScores = new List<string>();

        foreach (var (playerName, score) in topScores)
        {
            formattedScores.Add($"{playerName} - {score}");
        }

        return formattedScores;
    }

    private void LoadScores()
    {
        if (File.Exists(filePath))
        {
            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                var parts = line.Split(',');
                if (parts.Length == 2 && int.TryParse(parts[1], out int score))
                {
                    scores.Add((parts[0], score));
                }
            }
        }
    }

    private void SaveScores()
    {
        using (var writer = new StreamWriter(filePath))
        {
            foreach (var (playerName, score) in scores)
            {
                writer.WriteLine($"{playerName},{score}");
            }
        }
    }
} 