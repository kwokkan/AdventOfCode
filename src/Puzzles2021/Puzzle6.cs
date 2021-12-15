using AdventOfCode.PuzzleCore;

namespace AdventOfCode.Puzzles2021
{
    public class Puzzle6 : PuzzleBase
    {
        public Puzzle6()
            : base(2021, 6)
        {
        }

        private static List<int> InputAs(string[] input)
        {
            return input[0].Split(",").Select(int.Parse).ToList();
        }

        private static void Increment(long[] input)
        {
            var newFishes = input[0];

            for (int i = 1; i <= 8; i++)
            {
                input[i - 1] = input[i];
            }

            input[6] += newFishes;
            input[8] = newFishes;
        }

        private static long Calculate(List<int> input, int days)
        {
            var fishes = new long[9];

            var initial = input.ToLookup(x => x);

            foreach (var current in initial)
            {
                fishes[current.Key] = current.Count();
            }

            for (int i = 0; i < days; i++)
            {
                Increment(fishes);
            }

            return fishes.Sum();
        }

        public override long Sample1()
        {
            var input = InputAs(GetSampleString());

            return Calculate(input, 80);
        }

        public override long Sample2()
        {
            var input = InputAs(GetSampleString());

            return Calculate(input, 256);
        }

        public override long Solve1()
        {
            var input = InputAs(GetInputString());

            return Calculate(input, 80);
        }

        public override long Solve2()
        {
            var input = InputAs(GetInputString());

            return Calculate(input, 256);
        }
    }
}
