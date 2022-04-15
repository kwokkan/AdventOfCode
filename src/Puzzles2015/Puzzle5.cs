using AdventOfCode.PuzzleCore;

namespace AdventOfCode.Puzzles2015;

public class Puzzle5 : PuzzleBase
{
    private static readonly string[] _blacklist = new[]
    {
        "ab",
        "cd",
        "pq",
        "xy",
    };

    private static readonly char[] _vowels = new char[]
    {
        'a',
        'e',
        'i',
        'o',
        'u',
    };

    public Puzzle5()
        : base(2015, 5)
    {
    }

    private static bool Matches(string input)
    {
        if (_blacklist.Any(b => input.Contains(b)))
        {
            return false;
        }

        if (input.Count(v => _vowels.Any(i => i == v)) < 3)
        {
            return false;
        }

        var first = input[0];

        for (int i = 1; i < input.Length; i++)
        {
            var current = input[i];

            if (current == first)
            {
                return true;
            }

            first = current;
        }

        return false;
    }

    private long Calculate1(string[] input)
    {
        var count = 0;

        foreach (var line in input)
        {
            if (Matches(line))
            {
                count++;
            }
        }

        return count;
    }

    public override long ExpectedSample1 => 2;
    public override long Sample1()
    {
        var input = GetSampleString();

        return Calculate1(input);
    }

    public override long ExpectedAnswer1 => 255;
    public override long Solve1()
    {
        var input = GetInputString();

        return Calculate1(input);
    }
}
