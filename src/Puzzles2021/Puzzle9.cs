using AdventOfCode.PuzzleCore;

namespace AdventOfCode.Puzzles2021
{
    public class Puzzle9 : PuzzleBase
    {
        public Puzzle9()
            : base(2021, 9)
        {
        }

        private static int[][] InputAs(string[] input)
        {
            var output = new List<int[]>(input.Length + 2);

            output.Add(Enumerable.Range(0, input[0].Length + 2).Select(x => int.MaxValue).ToArray());

            foreach (var current in input)
            {
                var row = current.Select(x => int.Parse(x.ToString())).ToList();
                row.Insert(0, int.MaxValue);
                row.Add(int.MaxValue);

                output.Add(row.ToArray());
            }

            output.Add(Enumerable.Range(0, input[0].Length + 2).Select(x => int.MaxValue).ToArray());

            return output.ToArray();
        }

        private static bool IsLowest(int[][] input, int x, int y)
        {
            var current = input[x][y];

            return current < 9
                && current < input[x][y - 1]
                && current < input[x + 1][y]
                && current < input[x][y + 1]
                && current < input[x - 1][y];
        }

        private static long Calculate(int[][] input)
        {
            var lowPoints = new List<int>();

            for (int x = 1; x < input.Length - 1; x++)
            {
                var currentRow = input[x];

                for (int y = 1; y < currentRow.Length - 1; y++)
                {
                    if (IsLowest(input, x, y))
                    {
                        lowPoints.Add(currentRow[y]);
                    }
                }
            }

            return lowPoints.Select(x => x + 1).Sum();
        }

        public override long Sample1()
        {
            var input = InputAs(GetSampleString());

            return Calculate(input);
        }

        public override long Solve1()
        {
            var input = InputAs(GetInputString());

            return Calculate(input);
        }
    }
}
