using AdventOfCode.PuzzleCore;
using Xunit;

namespace AdventOfCode.Event2022;

public class Day04 : PuzzleBase
{
    internal record Range
    {
        public int Min { get; set; }

        public int Max { get; set; }
    }

    internal record Pair
    {
        public Range P1 { get; set; } = default!;

        public Range P2 { get; set; } = default!;
    }

    public Day04()
        : base(2022, 4)
    {
    }

    private static Pair[] InputAs(string[] input)
    {
        var pairs = new List<Pair>();

        foreach (var line in input)
        {
            var parts = line.Split(",");

            var p1Parts = parts[0].Split("-");
            var p2Parts = parts[1].Split("-");

            pairs.Add(new Pair
            {
                P1 = new Range
                {
                    Min = int.Parse(p1Parts[0]),
                    Max = int.Parse(p1Parts[1]),
                },
                P2 = new Range
                {
                    Min = int.Parse(p2Parts[0]),
                    Max = int.Parse(p2Parts[1]),
                },
            });
        }

        return pairs.ToArray();
    }

    private static int GetPriority(char priority)
    {
        if (priority >= 'a' && priority <= 'z')
        {
            return ((int)priority - 96);
        }
        else
        {
            return ((int)priority - 64 + 26);
        }
    }

    private static long Calculate1(Pair[] pairs)
    {
        var total = 0;

        foreach (var pair in pairs)
        {
            var p1in2 = pair.P1.Min >= pair.P2.Min && pair.P1.Max <= pair.P2.Max;
            var p2in1 = pair.P2.Min >= pair.P1.Min && pair.P2.Max <= pair.P1.Max;

            if (p1in2 || p2in1)
            {
                total++;
            }
        }

        return total;
    }

    private static int Calculate2(Pair[] pairs)
    {
        var total = 0;

        foreach (var pair in pairs)
        {
            var p1in2 = (pair.P1.Min >= pair.P2.Min && pair.P1.Min <= pair.P2.Max)
                || (pair.P1.Max >= pair.P2.Min && pair.P1.Max <= pair.P2.Max);
            var p2in1 = (pair.P2.Min >= pair.P1.Min && pair.P2.Min <= pair.P1.Max)
                || (pair.P2.Max >= pair.P1.Min && pair.P2.Max <= pair.P1.Max);

            if (p1in2 || p2in1)
            {
                total++;
            }
        }

        return total;
    }

    [Fact]
    public override long Sample1()
    {
        var input = InputAs(GetSampleString());

        var actual = Calculate1(input);

        Assert.Equal(2, actual);
        return actual;
    }

    [Fact]
    public override long Sample2()
    {
        var input = InputAs(GetSampleString());

        var actual = Calculate2(input);

        Assert.Equal(4, actual);
        return actual;
    }

    [Fact]
    public override long Solve1()
    {
        var input = InputAs(GetInputString());

        var actual = Calculate1(input);

        Assert.Equal(453, actual);
        return actual;
    }

    [Fact]
    public override long Solve2()
    {
        var input = InputAs(GetInputString());

        var actual = Calculate2(input);

        Assert.Equal(919, actual);
        return actual;
    }
}
