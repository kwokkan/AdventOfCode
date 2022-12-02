using AdventOfCode.PuzzleCore;
using Xunit;

namespace AdventOfCode.Event2022;

public class Day02 : PuzzleBase
{
    internal record Turn
    {
        internal int P1 { get; set; }
        internal int P2 { get; set; }
    }

    private static string[] P1Map = new[] { "", "A", "B", "C" };
    private static string[] P2Map = new[] { "", "X", "Y", "Z" };

    public Day02()
        : base(2022, 2)
    {
    }

    private static Turn[] InputAs(string[] input)
    {
        var turns = new List<Turn>();

        foreach (var line in input)
        {
            var parts = line.Split(" ");

            turns.Add(new Turn
            {
                P1 = Array.IndexOf(P1Map, parts[0]),
                P2 = Array.IndexOf(P2Map, parts[1]),
            });
        }

        return turns.ToArray();
    }

    private static int GetTotal1(Turn[] turns)
    {
        var total = 0;

        foreach (var turn in turns)
        {
            if (turn.P1 == turn.P2)
            {
                total += turn.P2 + 3;
            }
            else
            {
                total += turn.P2;

                switch (turn.P2)
                {
                    case 1:
                        if (turn.P1 == 3)
                        {
                            total += 6;
                        }
                        break;
                    case 2:
                        if (turn.P1 == 1)
                        {
                            total += 6;
                        }
                        break;
                    case 3:
                        if (turn.P1 == 2)
                        {
                            total += 6;
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        return total;
    }

    private static int GetTotal2(Turn[] turns)
    {
        var total = 0;

        foreach (var turn in turns)
        {
            if (turn.P2 == 2)
            {
                total += turn.P1 + 3;
            }
            else if (turn.P2 == 1)
            {
                var score = turn.P1;
                score--;

                if (score == 0)
                {
                    score = 3;
                }

                total += score;
            }
            else
            {
                var score = turn.P1;
                score++;

                if (score == 4)
                {
                    score = 1;
                }

                total += score + 6;
            }
        }

        return total;
    }

    [Fact]
    public override long Sample1()
    {
        var input = InputAs(GetSampleString());

        var actual = GetTotal1(input);

        Assert.Equal(15, actual);
        return actual;
    }

    [Fact]
    public override long Sample2()
    {
        var input = InputAs(GetSampleString());

        var actual = GetTotal2(input);

        Assert.Equal(12, actual);
        return actual;
    }

    [Fact]
    public override long Solve1()
    {
        var input = InputAs(GetInputString());

        var actual = GetTotal1(input);

        Assert.Equal(11386, actual);
        return actual;
    }

    [Fact]
    public override long Solve2()
    {
        var input = InputAs(GetInputString());

        var actual = GetTotal2(input);

        Assert.Equal(13600, actual);
        return actual;
    }
}
