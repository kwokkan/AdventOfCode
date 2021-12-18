using AdventOfCode.PuzzleCore;

namespace AdventOfCode.Puzzles2021
{
    public class Puzzle15 : PuzzleBase
    {
        public Puzzle15()
            : base(2021, 15)
        {
        }

        private static int[][] InputAs(string[] input)
        {
            var output = new List<int[]>(input.Length);

            foreach (var current in input)
            {
                var row = current.Select(x => int.Parse(x.ToString())).ToList();

                output.Add(row.ToArray());
            }

            return output.ToArray();
        }

        private static void Traverse(int[][] input, int fromX, int fromY, long parentCount, int parentPath, int minSteps, ref long lowestCount)
        {
            var currentCount = parentCount + input[fromY][fromX];
            var currentPath = parentPath + 1;

            if (currentCount > lowestCount)
            {
                return;
            }

            for (int y = fromY + 1; y < input.Length; y++)
            {
                Traverse(input, fromX, y, currentCount, currentPath, minSteps, ref lowestCount);
            }

            for (int x = fromX + 1; x < input[fromY].Length; x++)
            {
                Traverse(input, x, fromY, currentCount, currentPath, minSteps, ref lowestCount);
            }

            if (input.Length - 1 == fromY && input[fromY].Length - 1 == fromX && currentPath >= minSteps)
            {
                if (currentCount < lowestCount)
                {
                    lowestCount = currentCount;
                }
            }
        }

        private static long Calculate1(int[][] input)
        {
            var lowestCount = 0L - input[0][0];

            // get from top left to bottom left to bottom right
            for (int y = 0; y < input.Length; y++)
            {
                lowestCount += input[y][0];
            }
            for (int x = 1; x < input[^1].Length; x++)
            {
                lowestCount += input[x][^1];
            }

            var currentX = 0;
            var currentY = 0;

            Traverse(input, currentX, currentY, 0L - input[0][0], 0, input.Length + input[0].Length - 1, ref lowestCount);

            return lowestCount;
        }

        public override long Sample1()
        {
            var input = InputAs(GetSampleString());

            return Calculate1(input);
        }

        //TODO: Optimise
        /*public override long Solve1()
        {
            var input = InputAs(GetInputString());

            return Calculate1(input);
        }*/
    }
}
