using AdventOfCode.PuzzleCore;
using static AdventOfCode.Puzzles2021.Puzzle16Utils;

namespace AdventOfCode.Puzzles2021
{
    public class Puzzle16 : PuzzleBase
    {
        public Puzzle16()
            : base(2021, 16)
        {
        }

        private static long Calculate1(string input)
        {
            var output = ParsePacket(input);

            return output.VersionSum;
        }

        private static long Calculate2(string input)
        {
            var output = ParsePacket(input);

            return output.ValueOf;
        }

        public override long Sample1()
        {
            var input = GetSampleString()[0];

            return Calculate1(input);
        }

        public override long Sample2()
        {
            var input = GetSampleString()[0];

            return Calculate2(input);
        }

        public override long Solve1()
        {
            var input = GetInputString()[0];

            return Calculate1(input);
        }

        public override long Solve2()
        {
            var input = GetInputString()[0];

            return Calculate2(input);
        }
    }
}
