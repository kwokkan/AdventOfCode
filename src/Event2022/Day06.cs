using AdventOfCode.PuzzleCore;
using Xunit;

namespace AdventOfCode.Event2022;

public class Day06 : PuzzleBase
{
    public Day06()
        : base(2022, 6)
    {
    }

    private static string InputAs(string[] input)
    {
        return input[0];
    }

    private static int Calculate(string input, int count)
    {
        var index = 0;

        for (; index < input.Length - count; index++)
        {
            if (input.Skip(index).Take(count).Distinct().Count() == count)
            {
                break;
            }
        }

        return index + count;
    }

    [Fact]
    public override long Sample1()
    {
        var input = InputAs(GetSampleString());

        var actual = Calculate(input, 4);

        Assert.Equal(7, actual);
        return actual;
    }

    [Fact]
    public override long Sample2()
    {
        var input = InputAs(GetSampleString());

        var actual = Calculate(input, 14);

        Assert.Equal(19, actual);
        return actual;
    }

    [Fact]
    public override long Solve1()
    {
        var input = InputAs(GetInputString());

        var actual = Calculate(input, 4);

        Assert.Equal(1647, actual);
        return actual;
    }

    [Fact]
    public override long Solve2()
    {
        var input = InputAs(GetInputString());

        var actual = Calculate(input, 14);

        Assert.Equal(2447, actual);
        return actual;
    }
}
