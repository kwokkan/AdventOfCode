using AdventOfCode.PuzzleCore;
using Xunit;

namespace AdventOfCode.Event2022;

public class Day03 : PuzzleBase
{
    internal record Bag
    {
        internal char[] C1 { get; set; } = default!;
        internal char[] C2 { get; set; } = default!;
    }

    public Day03()
        : base(2022, 3)
    {
    }

    private static Bag[] InputAs(string[] input)
    {
        var bags = new List<Bag>();

        foreach (var line in input)
        {
            var c1 = line.Take(line.Length / 2).Select(x => x).ToArray();
            var c2 = line.Skip(line.Length / 2).Select(x => x).ToArray();

            bags.Add(new Bag
            {
                C1 = c1,
                C2 = c2,
            });
        }

        return bags.ToArray();
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

    private static long Calculate1(Bag[] bags)
    {
        var total = 0;

        foreach (var bag in bags)
        {
            foreach (var item in bag.C1)
            {
                if (bag.C2.Contains(item))
                {
                    total += GetPriority(item);

                    break;
                }
            }
        }

        return total;
    }

    private static int GetTotal2(Bag[] bags)
    {
        var total = 0;

        var groups = new List<char[]>();
        var groupNum = 0;

        foreach (var bag in bags)
        {
            groups.Add(bag.C1.Concat(bag.C2).ToArray());
            groupNum++;

            if (groupNum == 3)
            {
                foreach (var item in groups[0])
                {
                    if (groups[1].Contains(item) && groups[2].Contains(item))
                    {
                        total += GetPriority(item);

                        groups.Clear();
                        groupNum = 0;

                        break;
                    }
                }
            }
        }

        return total;
    }

    [Fact]
    public override long Sample1()
    {
        var input = InputAs(GetSampleString());

        var actual = Calculate1(input);

        Assert.Equal(157, actual);
        return actual;
    }

    [Fact]
    public override long Sample2()
    {
        var input = InputAs(GetSampleString());

        var actual = GetTotal2(input);

        Assert.Equal(70, actual);
        return actual;
    }

    [Fact]
    public override long Solve1()
    {
        var input = InputAs(GetInputString());

        var actual = Calculate1(input);

        Assert.Equal(7716, actual);
        return actual;
    }

    [Fact]
    public override long Solve2()
    {
        var input = InputAs(GetInputString());

        var actual = GetTotal2(input);

        Assert.Equal(2973, actual);
        return actual;
    }
}
