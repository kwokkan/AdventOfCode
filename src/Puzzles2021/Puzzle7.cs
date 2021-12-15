using AdventOfCode.PuzzleCore;

namespace AdventOfCode.Puzzles2021
{
    public class Puzzle7 : PuzzleBase
    {
        public Puzzle7()
            : base(2021, 7)
        {
        }

        private static List<int> InputAs(string[] input)
        {
            return input[0].Split(",").Select(int.Parse).ToList();
        }

        private static int GetGenerated(int count)
        {
            var total = 0;

            for (int i = 1; i <= count; i++)
            {
                total += i;
            }

            return total;
        }

        private static long Calculate(List<int> input)
        {
            var minFuel = long.MaxValue;

            for (int moveTo = 0; moveTo < input.Count; moveTo++)
            {
                var currentFuel = 0;

                for (int moveFrom = 0; moveFrom < input.Count; moveFrom++)
                {
                    var currentMoveFrom = input[moveFrom];

                    if (moveTo < currentMoveFrom)
                    {
                        currentFuel += currentMoveFrom - moveTo;
                    }
                    else
                    {
                        currentFuel += moveTo - currentMoveFrom;
                    }
                }

                if (currentFuel < minFuel)
                {
                    minFuel = currentFuel;
                }
            }

            return minFuel;
        }

        private static long Calculate2(List<int> input)
        {
            var minFuel = long.MaxValue;

            for (int moveTo = 0; moveTo < input.Count; moveTo++)
            {
                var currentFuel = 0;

                for (int moveFrom = 0; moveFrom < input.Count; moveFrom++)
                {
                    var currentMoveFrom = input[moveFrom];

                    if (moveTo < currentMoveFrom)
                    {
                        currentFuel += GetGenerated(currentMoveFrom - moveTo);
                    }
                    else
                    {
                        currentFuel += GetGenerated(moveTo - currentMoveFrom);
                    }

                    if (currentFuel > minFuel)
                    {
                        break;
                    }
                }

                if (currentFuel < minFuel)
                {
                    minFuel = currentFuel;
                }
            }

            return minFuel;
        }

        public override long Sample1()
        {
            var input = InputAs(GetSampleString());

            return Calculate(input);
        }

        public override long Sample2()
        {
            var input = InputAs(GetSampleString());

            return Calculate2(input);
        }

        public override long Solve1()
        {
            var input = InputAs(GetInputString());

            return Calculate(input);
        }

        public override long Solve2()
        {
            var input = InputAs(GetInputString());

            return Calculate2(input);
        }
    }
}
