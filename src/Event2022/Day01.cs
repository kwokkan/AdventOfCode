using AdventOfCode.PuzzleCore;
using Xunit;

namespace AdventOfCode.Event2022;

public class Day01 : PuzzleBase
{
    public Day01()
        : base(2022, 1)
    {
    }

    private static int[] InputAs(string[] input)
    {
        var counts = new List<int>();

        var current = 0;
        foreach (var line in input)
        {
            if (int.TryParse(line, out var value))
            {
                current += value;
            }
            else
            {
                counts.Add(current);
                current = 0;
            }
        }

        return counts.ToArray();
    }

    [Fact]
    public override long Sample1()
    {
        var input = InputAs(GetSampleString(appendBlankLine: true));

        var actual = input.Max(x => x);

        Assert.Equal(24000, actual);
        return actual;
    }

    [Fact]
    public override long Sample2()
    {
        var input = InputAs(GetSampleString(appendBlankLine: true));

        var actual = input.OrderByDescending(x => x).Take(3).Sum();

        Assert.Equal(45000, actual);
        return actual;
    }

    [Fact]
    public override long Solve1()
    {
        var input = InputAs(GetInputString(appendBlankLine: true));

        var actual = input.Max(x => x);

        Assert.Equal(75501, actual);
        return actual;
    }

    [Fact]
    public override long Solve2()
    {
        var input = InputAs(GetInputString(appendBlankLine: true));

        var actual = input.OrderByDescending(x => x).Take(3).Sum();

        Assert.Equal(215594, actual);
        return actual;
    }
}
