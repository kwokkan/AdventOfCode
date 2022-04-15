using AdventOfCode.PuzzleCore;

namespace AdventOfCode.Puzzles2015;

public class Puzzle3 : PuzzleBase
{
    private record struct Point(int X, int Y);

    public Puzzle3()
        : base(2015, 3)
    {
    }

    private static void Populate(string input, Dictionary<Point, int> points)
    {
        var curX = 0;
        var curY = 0;

        foreach (var direction in input)
        {
            switch (direction)
            {
                case '^':
                    curY++;
                    break;
                case '>':
                    curX++;
                    break;
                case 'v':
                    curY--;
                    break;
                case '<':
                    curX--;
                    break;
                default:
                    break;
            }

            var curPoint = new Point(curX, curY);

            if (points.ContainsKey(curPoint))
            {
                points[curPoint]++;
            }
            else
            {
                points[curPoint] = 1;
            }
        }
    }

    private long Calculate1(string input)
    {
        var points = new Dictionary<Point, int>
        {
            { new Point(0, 0), 1 }
        };

        Populate(input, points);

        return points.Count;
    }

    private static long Calculate2(string input)
    {
        var points = new Dictionary<Point, int>
        {
            { new Point(0, 0), 2 }
        };

        Populate(string.Join("", input.Where((x, i) => i % 2 == 0)), points);
        Populate(string.Join("", input.Where((x, i) => i % 2 == 1)), points);

        return points.Count;
    }

    public override long ExpectedSample1 => 2;
    public override long Sample1()
    {
        var input = GetSampleString()[0];

        return Calculate1(input);
    }

    public override long ExpectedSample2 => 11;
    public override long Sample2()
    {
        var input = GetSampleString()[0];

        return Calculate2(input);
    }

    public override long ExpectedAnswer1 => 2565;
    public override long Solve1()
    {
        var input = GetInputString()[0];

        return Calculate1(input);
    }

    public override long ExpectedAnswer2 => 2639;
    public override long Solve2()
    {
        var input = GetInputString()[0];

        return Calculate2(input);
    }
}
