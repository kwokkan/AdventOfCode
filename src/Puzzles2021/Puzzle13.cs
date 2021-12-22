using System.Text;
using AdventOfCode.PuzzleCore;

namespace AdventOfCode.Puzzles2021
{
    public class Puzzle13 : PuzzleBase
    {
        private record Point(int X, int Y);

        private record Instructions(
            Point[] Grid,
            Point[] Steps
        );

        public Puzzle13()
            : base(2021, 13)
        {
        }

        private static Instructions InputAs(string[] input)
        {
            var grid = new List<Point>(input.Length);
            var steps = new List<Point>();

            var i = 0;
            for (; i < input.Length; i++)
            {
                var current = input[i];

                if (string.IsNullOrWhiteSpace(current))
                {
                    i++;
                    break;
                }

                var parts = current.Split(",").Select(int.Parse).ToArray();
                grid.Add(new Point(parts[0], parts[1]));
            }

            var prefix = "fold along ";
            for (; i < input.Length; i++)
            {
                var current = input[i].AsSpan(prefix.Length);

                if (current[0] == 'x')
                {
                    steps.Add(new Point(int.Parse(current.Slice(2)), -1));
                }
                else
                {
                    steps.Add(new Point(-1, int.Parse(current.Slice(2))));
                }
            }

            return new Instructions(grid.ToArray(), steps.ToArray());
        }

        private static string Format(int[][] input)
        {
            var sb = new StringBuilder();

            foreach (var current in input)
            {
                sb.AppendJoin(string.Empty, current);
                sb.AppendLine();
            }

            return sb.ToString();
        }

        private static int[][] CreateGrid(int x, int y)
        {
            var grid = Enumerable.Range(0, y).Select(_ => new int[x]).ToArray();

            return grid;
        }

        private static void PopulateGrid(Point[] input, int[][] grid)
        {
            foreach (var current in input)
            {
                grid[current.Y][current.X]++;
            }
        }

        private static int[][] FoldX(int[][] input, int from)
        {
            var output = CreateGrid(from, input.Length);

            for (int y = 0; y < output.Length; y++)
            {
                for (int x = 0; x < output[y].Length; x++)
                {
                    output[y][x] = input[y][x];

                    if (output[y][x] == 0)
                    {
                        output[y][x] += input[y][^(x + 1)];
                    }
                }
            }

            return output;
        }

        private static int[][] FoldY(int[][] input, int from)
        {
            var output = CreateGrid(input[0].Length, from);

            for (int y = 0; y < output.Length; y++)
            {
                for (int x = 0; x < output[y].Length; x++)
                {
                    output[y][x] = input[y][x];

                    if (output[y][x] == 0)
                    {
                        output[y][x] += input[^(y + 1)][x];
                    }
                }
            }

            return output;
        }

        private static long GetDots(int[][] input)
        {
            var count = 0;

            foreach (var current in input)
            {
                count += current.Count(x => x > 0);
            }

            return count;
        }

        private static int[][] FoldGrid(Instructions input, int steps)
        {
            var maxX = input.Grid.Max(x => x.X) + 1;
            var maxY = input.Grid.Max(x => x.Y) + 1;

            var grid = CreateGrid(maxX, maxY);
            PopulateGrid(input.Grid, grid);

            for (int i = 0; i < steps; i++)
            {
                var current = input.Steps[i];

                if (current.X != -1)
                {
                    grid = FoldX(grid, current.X);
                }
                else
                {
                    grid = FoldY(grid, current.Y);
                }
            }

            return grid;
        }

        private static long Calculate1(Instructions input, int steps)
        {
            var grid = FoldGrid(input, steps);

            return GetDots(grid);
        }

        private static long Calculate2(Instructions input, int steps)
        {
            var grid = FoldGrid(input, steps);

            var str = Format(grid);

            return GetDots(grid);
        }

        public override long Sample1()
        {
            var input = InputAs(GetSampleString());

            return Calculate1(input, 1);
        }

        public override long Sample2()
        {
            var input = InputAs(GetSampleString());

            return Calculate2(input, input.Steps.Length);
        }

        public override long Solve1()
        {
            var input = InputAs(GetInputString());

            return Calculate1(input, 1);
        }

        public override long Solve2()
        {
            var input = InputAs(GetInputString());

            return Calculate2(input, input.Steps.Length);
        }
    }
}
