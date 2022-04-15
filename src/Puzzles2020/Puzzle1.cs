using AdventOfCode.PuzzleCore;

namespace AdventOfCode.Puzzles2020;

public class Puzzle1 : PuzzleBase
{
    public Puzzle1()
        : base(2020, 1)
    {
    }

    private static long SampleInternal()
    {
        return Solve1Internal(Puzzle1Input.Sample);
    }

    private static long Solve1Internal(int[]? data = null)
    {
        var input = data ?? Puzzle1Input.Input;

        for (int outer = 0; outer < input.Length; outer++)
        {
            for (int inner = input.Length - 1; inner >= 0; inner--)
            {
                if (inner == outer)
                {
                    continue;
                }

                if (input[inner] + input[outer] == 2020)
                {
                    return (input[inner] * input[outer]);
                }
            }
        }

        return default;
    }

    private static long Solve2Internal()
    {
        var input = Puzzle1Input.Input;

        for (int i = 0; i < input.Length; i++)
        {
            for (int j = 0; j < input.Length; j++)
            {
                for (int k = 0; k < input.Length; k++)
                {
                    if (i == j && j == k)
                    {
                        continue;
                    }

                    if ((input[i] + input[j] + input[k]) == 2020)
                    {
                        return input[i] * input[j] * input[k];
                    }
                }
            }
        }

        return default;
    }

    public override long Sample1()
    {
        return SampleInternal();
    }

    public override long Solve1()
    {
        return Solve1Internal();
    }

    public override long Solve2()
    {
        return Solve2Internal();
    }
}
