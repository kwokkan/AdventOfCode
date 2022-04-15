using System.Text.RegularExpressions;
using AdventOfCode.PuzzleCore;

namespace AdventOfCode.Puzzles2015;

public class Puzzle14 : PuzzleBase
{
    private static readonly Regex _regex = new Regex("^.*\\s(\\d+)\\s.*\\s(\\d+)\\s.*\\s(\\d+)\\s.*$");

    public Puzzle14()
        : base(2015, 14)
    {
    }

    private static long GetDistance(int speed, int steps, int rest, int duration)
    {
        var abs = Math.Abs(duration / (steps + rest));

        var remain = duration % (steps + rest);

        var distance = speed * abs * steps;

        distance += Math.Min(remain, steps) * speed;

        return distance;
    }

    private static (int Speed, int Steps, int RestFor) GetReindeer(string input)
    {
        var matches = _regex.Matches(input);

        return (
            int.Parse(matches[0].Groups[1].Value),
            int.Parse(matches[0].Groups[2].Value),
            int.Parse(matches[0].Groups[3].Value)
        );
    }

    private static long Calculate1(string[] input, int duration)
    {
        var distances = new List<long>();

        foreach (var line in input)
        {
            var reindeer = GetReindeer(line);

            distances.Add(GetDistance(reindeer.Speed, reindeer.Steps, reindeer.RestFor, duration));
        }

        return distances.Max();
    }

    public override long Sample1()
    {
        var input = GetSampleString();

        return Calculate1(input, 1000);
    }

    public override long Solve1()
    {
        var input = GetInputString();

        return Calculate1(input, 2503);
    }
}
