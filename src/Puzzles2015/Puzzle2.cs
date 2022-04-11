using AdventOfCode.PuzzleCore;

namespace AdventOfCode.Puzzles2015;

public class Puzzle2 : PuzzleBase
{
    public Puzzle2()
        : base(2015, 2)
    {
    }

    private static (int l, int w, int h) ParseLine(string line)
    {
        var parts = line.Split('x');

        return (int.Parse(parts[0]), int.Parse(parts[1]), int.Parse(parts[2]));
    }

    private long Calculate1(string[] input)
    {
        var total = 0;

        foreach (var line in input)
        {
            var area = ParseLine(line);

            var x = area.l * area.w;
            var y = area.w * area.h;
            var z = area.h * area.l;

            var current = x + y + z;

            current *= 2;

            current += Math.Min(Math.Min(x, y), z);

            total += current;
        }

        return total;
    }

    private static long Calculate2(string[] input)
    {
        var total = 0;

        foreach (var line in input)
        {
            var area = ParseLine(line);

            var min1 = Math.Min(area.l, Math.Min(area.w, area.h));
            var min2 = new[] { area.l, area.w, area.h }.OrderBy(x => x).Skip(1).First();

            var current = (min1 * 2) + (min2 * 2);

            current += area.l * area.w * area.h;

            total += current;
        }

        return total;
    }

    public override long Sample1()
    {
        var input = GetSampleString();

        return Calculate1(input);
    }

    public override long Sample2()
    {
        var input = GetSampleString();

        return Calculate2(input);
    }

    public override long Solve1()
    {
        var input = GetInputString();

        return Calculate1(input);
    }

    public override long Solve2()
    {
        var input = GetInputString();

        return Calculate2(input);
    }
}
