using AdventOfCode.PuzzleCore;

namespace AdventOfCode.Puzzles2015;

public class Puzzle1 : PuzzleBase
{
    public Puzzle1()
        : base(2015, 1)
    {
    }

    private static long Calculate1(string input)
    {
        return input.Count(x => x == '(') - input.Count(x => x == ')');
    }

    private static long Calculate2(string input)
    {
        var currrent = 0;
        var position = 0;

        foreach (var dir in input)
        {
            position++;

            switch (dir)
            {
                case '(':
                    currrent++;
                    break;
                case ')':
                    currrent--;
                    break;
                default:
                    break;
            }

            if (currrent == -1)
            {
                return position;
            }
        }

        return position;
    }

    public override long Sample1()
    {
        var input = GetSampleString()[0];

        return Calculate1(input);
    }

    public override long Sample2()
    {
        var input = GetSampleString()[0];

        return Calculate2(input);
    }

    public override long Solve1()
    {
        var input = GetInputString()[0];

        return Calculate1(input);
    }

    public override long Solve2()
    {
        var input = GetInputString()[0];

        return Calculate2(input);
    }
}
