using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Puzzles
{
    public class Puzzle2 : IPuzzle
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

        private static string Solve1()
        {
            var policies = GetPolicies(Puzzle2Input.Input);
            var count = 0;

            foreach (var policy in policies)
            {
                var charCount = policy.Password.Count(x => x == policy.Char);

                if (charCount >= policy.Min && charCount <= policy.Max)
                {
                    count++;
                }
            }

            return count.ToString();
        }

        private static string Solve2()
        {
            var policies = GetPolicies(Puzzle2Input.Input);
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

            return count.ToString();
        }

        Solution IPuzzle.Solve()
        {
            return new Solution
            (
                Solve1(),
                Solve2()
            );
        }
    }
}
