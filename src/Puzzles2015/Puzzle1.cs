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
        var current = 0;
        var position = 0;

        foreach (var dir in input)
        {
            position++;

            switch (dir)
            {
                case '(':
                    current++;
                    break;
                case ')':
                    current--;
                    break;
                default:
                    break;
            }

            if (current == -1)
            {
                return position;
            }
        }

        return position;
    }

    public override long ExpectedSample1 => -3;
    public override long Sample1()
    {
        var input = GetSampleString()[0];

        return Calculate1(input);
    }

    public override long ExpectedSample2 => 1;
    public override long Sample2()
    {
        var input = GetSampleString()[0];

        return Calculate2(input);
    }

    public override long ExpectedAnswer1 => 280;
    public override long Solve1()
    {
        var input = GetInputString()[0];

        return Calculate1(input);
    }

    public override long ExpectedAnswer2 => 1797;
    public override long Solve2()
    {
        var input = GetInputString()[0];

        return Calculate2(input);
    }
}
