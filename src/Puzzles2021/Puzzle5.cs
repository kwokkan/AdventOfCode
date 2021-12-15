using AdventOfCode.PuzzleCore;

namespace AdventOfCode.Puzzles2021
{
    public class Puzzle5 : PuzzleBase
    {
        private record Point
        {
            public int X { get; init; }

            public int Y { get; init; }
        }

        public Puzzle5()
            : base(2021, 5)
        {
        }

        private static Tuple<Point, Point>[] InputAs(string[] input)
        {
            var output = new List<Tuple<Point, Point>>();

            foreach (var current in input)
            {
                var parts = current.Split("->", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToArray();
                var froms = parts[0].Split(",").Select(int.Parse).ToArray();
                var tos = parts[1].Split(",").Select(int.Parse).ToArray();

                output.Add(Tuple.Create(
                    new Point
                    {
                        X = froms[0],
                        Y = froms[1],
                    },
                    new Point
                    {
                        X = tos[0],
                        Y = tos[1],
                    }
                ));
            }

            return output.ToArray();
        }

        private static int GetMaxX(Tuple<Point, Point>[] input)
        {
            return Math.Max(input.Max(x => x.Item1.X), input.Max(x => x.Item2.X));
        }

        private static int GetMaxY(Tuple<Point, Point>[] input)
        {
            return Math.Max(input.Max(x => x.Item1.Y), input.Max(x => x.Item2.Y));
        }

        private static Tuple<Point, Point>[] GetValidXY(Tuple<Point, Point>[] input)
        {
            return input.Where(x => x.Item1.X == x.Item2.X || x.Item1.Y == x.Item2.Y).ToArray();
        }

        private static Tuple<Point, Point>[] GetValidDiagonal(Tuple<Point, Point>[] input)
        {
            return input.Where(x => x.Item1.X != x.Item2.X && x.Item1.Y != x.Item2.Y).ToArray();
        }

        private static void PlotXYGrid(Tuple<Point, Point>[] input, int[][] grid)
        {
            foreach (var current in input)
            {
                if (current.Item1.X == current.Item2.X)
                {
                    var minY = Math.Min(current.Item1.Y, current.Item2.Y);
                    var maxY = Math.Max(current.Item1.Y, current.Item2.Y);

                    for (int i = minY; i <= maxY; i++)
                    {
                        grid[i][current.Item1.X]++;
                    }
                }
                else
                {
                    var minX = Math.Min(current.Item1.X, current.Item2.X);
                    var maxX = Math.Max(current.Item1.X, current.Item2.X);

                    for (int i = minX; i <= maxX; i++)
                    {
                        grid[current.Item1.Y][i]++;
                    }
                }
            }
        }

        private static void PlotDiagonalGrid(Tuple<Point, Point>[] input, int[][] grid)
        {
            foreach (var current in input)
            {
                // always start plotting from the left, so there are fewer if statements
                var leftMost = current.Item1.X < current.Item2.X ? current.Item1 : current.Item2;
                var rightMost = current.Item1.X < current.Item2.X ? current.Item2 : current.Item1;

                if (leftMost.Y < rightMost.Y)
                {
                    // top left to bottom right
                    var yGap = rightMost.Y - leftMost.Y;

                    for (int i = 0; i <= yGap; i++)
                    {
                        grid[leftMost.Y + i][leftMost.X + i]++;
                    }
                }
                else
                {
                    // bottom left to top right
                    var yGap = leftMost.Y - rightMost.Y;

                    for (int i = 0; i <= yGap; i++)
                    {
                        grid[leftMost.Y - i][leftMost.X + i]++;
                    }
                }
            }
        }

        private static int GetOverlapping(int[][] input)
        {
            return input.SelectMany(x => x).Count(x => x > 1);
        }

        private long Calculate(Tuple<Point, Point>[] input, bool includeDiagonal)
        {
            var maxX = GetMaxX(input) + 1;
            var maxY = GetMaxY(input) + 1;

            var grid = new int[maxY][];

            Enumerable.Range(0, maxY).ToList().ForEach(x =>
            {
                grid[x] = new int[maxX];
            });

            var validXY = GetValidXY(input);

            PlotXYGrid(validXY, grid);

            if (includeDiagonal)
            {
                var validDiagonal = GetValidDiagonal(input);
                PlotDiagonalGrid(validDiagonal, grid);
            }

            return GetOverlapping(grid);
        }

        public override long Sample1()
        {
            var input = InputAs(GetSampleString());

            return Calculate(input, false);
        }

        public override long Sample2()
        {
            var input = InputAs(GetSampleString());

            return Calculate(input, true);
        }

        public override long Solve1()
        {
            var input = InputAs(GetInputString());

            return Calculate(input, false);
        }

        public override long Solve2()
        {
            var input = InputAs(GetInputString());

            return Calculate(input, true);
        }
    }
}
