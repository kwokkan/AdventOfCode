using System.Collections.Generic;
using System.Linq;
using AdventOfCode.PuzzleCore;

namespace AdventOfCode2020.Puzzles
{
    public class Puzzle2 : PuzzleBase
    {
        private record PasswordPolicy
        {
            public int Min { get; init; }
            public int Max { get; init; }
            public char Char { get; init; }
            public string Password { get; init; }
        }

        private static IEnumerable<PasswordPolicy> GetPolicies(string[] lines)
        {
            foreach (var line in lines)
            {
                var data = line.Split(' ');
                var minMax = data[0].Split('-');

                yield return new PasswordPolicy
                {
                    Min = int.Parse(minMax[0]),
                    Max = int.Parse(minMax[1]),
                    Char = data[1][0],
                    Password = data[2],
                };
            }
        }

        private static long Solve1Internal()
        {
            var policies = GetPolicies(Puzzle2Input.Input).ToList();
            var count = 0;

            foreach (var policy in policies)
            {
                var charCount = policy.Password.Count(x => x == policy.Char);

                if (charCount >= policy.Min && charCount <= policy.Max)
                {
                    count++;
                }
            }

            return count;
        }

        private static long Solve2Internal()
        {
            var policies = GetPolicies(Puzzle2Input.Input).ToList();
            var count = 0;

            foreach (var policy in policies)
            {
                var minChar = policy.Password[policy.Min - 1] == policy.Char ? 1 : 0;
                var maxChar = policy.Password[policy.Max - 1] == policy.Char ? 1 : 0;

                if (minChar + maxChar == 1)
                {
                    count++;
                }
            }

            return count;
        }

        public override long Solve1()
        {
            return Solve1Internal();
        }

        public override long Solve2()
        {
            return Solve2Internal();
        }
    }
}
