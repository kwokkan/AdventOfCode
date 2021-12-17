using AdventOfCode.PuzzleCore;

namespace AdventOfCode.Puzzles2021
{
    public class Puzzle17 : PuzzleBase
    {
        private record Point
        {
            public int X { get; set; }

            public int Y { get; set; }
        }

        private readonly record struct Range(Point Min, Point Max);

        public Puzzle17()
                : base(2021, 17)
        {
        }

        private static Range InputAs(string[] input)
        {
            var parts = input[0]
                .Replace("target area: x=", string.Empty)
                .Replace(", y=", " ")
                .Replace("..", " ")
                .Split(" ")
                .Select(int.Parse)
                .ToArray();

            var min = new Point
            {
                X = Math.Min(parts[0], parts[1]),
                Y = Math.Min(parts[2], parts[3]),
            };

            var max = new Point
            {
                X = Math.Max(parts[0], parts[1]),
                Y = Math.Max(parts[2], parts[3]),
            };

            return new Range
            {
                Min = min,
                Max = max,
            };
        }

        private static Point Step(Point current, Point velocity)
        {
            var newX = current.X + velocity.X;
            var newY = current.Y + velocity.Y;

            if (velocity.X > 0)
            {
                velocity.X--;
            }
            else if (velocity.X < 0)
            {
                velocity.X++;
            }

            velocity.Y--;

            return new Point
            {
                X = newX,
                Y = newY,
            };
        }

        private static int CheckValid(Point current, Range target)
        {
            if (
                   (current.X <= target.Max.X && current.Y > target.Max.Y)
                || (current.X < target.Min.X && current.Y >= target.Min.Y))
            {
                return -1;
            }

            if (
                   current.X >= target.Min.X
                && current.X <= target.Max.X
                && current.Y <= target.Max.Y
                && current.Y >= target.Min.Y
            )
            {
                return 0;
            }

            if (current.X > target.Max.X || current.Y < target.Min.Y)
            {
                return 1;
            }

            return -2;
        }

        private static long Calculate1(Range target)
        {
            var finalMaxY = int.MinValue;

            var xLength = target.Min.X;
            var yLength = Math.Abs(target.Min.Y);

            for (int x = 1; x < xLength; x++)
            {
                for (int y = 0; y < yLength; y++)
                {
                    var newPoint = new Point();
                    var velocity = new Point
                    {
                        X = x,
                        Y = y,
                    };
                    var currentMaxY = int.MinValue;

                    while (true)
                    {
                        newPoint = Step(newPoint, velocity);

                        if (newPoint.Y > currentMaxY)
                        {
                            currentMaxY = newPoint.Y;
                        }

                        var result = CheckValid(newPoint, target);

                        if (result == -1)
                        {
                            continue;
                        }
                        else if (result == 0)
                        {
                            if (currentMaxY > finalMaxY)
                            {
                                finalMaxY = currentMaxY;
                            }

                            break;
                        }
                        else if (result == 1)
                        {
                            break;
                        }
                        else
                        {
                            throw new InvalidOperationException("Not valid");
                        }
                    }
                }
            }

            return finalMaxY;
        }

        private static long Calculate2(Range target)
        {
            var targetCount = 0L;

            var xLength = target.Max.X;
            var yLength = Math.Abs(target.Min.Y);

            for (int x = 1; x <= xLength; x++)
            {
                for (int y = target.Min.Y; y <= yLength; y++)
                {
                    var newPoint = new Point();
                    var velocity = new Point
                    {
                        X = x,
                        Y = y,
                    };

                    while (true)
                    {
                        newPoint = Step(newPoint, velocity);

                        var result = CheckValid(newPoint, target);

                        if (result == -1)
                        {
                            continue;
                        }
                        else if (result == 0)
                        {
                            targetCount++;

                            break;
                        }
                        else if (result == 1)
                        {
                            break;
                        }
                        else
                        {
                            throw new InvalidOperationException("Not valid");
                        }
                    }
                }
            }

            return targetCount;
        }

        public override long Sample1()
        {
            var input = InputAs(GetSampleString());

            return Calculate1(input);
        }

        public override long Sample2()
        {
            var input = InputAs(GetSampleString());

            return Calculate2(input);
        }

        public override long Solve1()
        {
            var input = InputAs(GetInputString());

            return Calculate1(input);
        }

        public override long Solve2()
        {
            var input = InputAs(GetInputString());

            return Calculate2(input);
        }
    }
}
