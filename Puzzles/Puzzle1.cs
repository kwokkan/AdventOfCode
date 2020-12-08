namespace AdventOfCode2020.Puzzles
{
    public class Puzzle1 : IPuzzle
    {
        private static int Sample()
        {
            return int.Parse(Solve1(Puzzle1Input.Sample));
        }

        private static string Solve1(int[]? data = null)
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
                        return (input[inner] * input[outer]).ToString();
                    }
                }
            }

            return string.Empty;
        }

        private static string Solve2()
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
                            return (input[i] * input[j] * input[k]).ToString();
                        }
                    }
                }
            }

            return string.Empty;
        }

        Solution IPuzzle.Solve()
        {
            return new Solution
            (
                Sample(),
                Solve1(),
                Solve2()
            );
        }
    }
}
