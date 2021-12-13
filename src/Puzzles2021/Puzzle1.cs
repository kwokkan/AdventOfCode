using AdventOfCode.PuzzleCore;

namespace AdventOfCode.Puzzles2021
{
    public class Puzzle1 : PuzzleBase
    {
        public Puzzle1()
            : base(2021, 1)
        {
        }

        private static int[] InputAs(string[] input)
        {
            var output = new List<int>();

            foreach (var current in input)
            {
                output.Add(int.Parse(current));
            }

            return output.ToArray();
        }

        private static int GetRangeValue(int[] input, int from, int window)
        {
            return input.Skip(from).Take(window).Sum();
        }

        private long Calculate(int[] input, int window)
        {
            var prev = GetRangeValue(input, 0, window);
            var count = 0;

            for (int i = 1; i < input.Length - (window - 1); i++)
            {
                var current = GetRangeValue(input, i, window);

                if (current > prev)
                {
                    count++;
                }

                prev = current;
            }

            return count;
        }

        public override long Sample1()
        {
            var input = InputAs(GetSampleString());

            return Calculate(input, 1);
        }

        public override long Sample2()
        {
            var input = InputAs(GetSampleString());

            return Calculate(input, 3);
        }

        public override long Solve1()
        {
            var input = InputAs(GetInputString());

            return Calculate(input, 1);
        }

        public override long Solve2()
        {
            var input = InputAs(GetInputString());

            return Calculate(input, 3);
        }
    }
}
