using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Puzzles
{
    public class Puzzle6 : IPuzzle
    {
        private record Group
        {
            public List<char> Answers { get; init; } = new List<char>();

            public int Count { get; set; }
        }

        private static IEnumerable<Group> GetGroups(string[] lines)
        {
            var group = new Group();

            foreach (var line in lines)
            {
                if (line == string.Empty)
                {
                    yield return group;

                    group = new Group();

                    continue;
                }

                group.Answers.AddRange(line.Select(x => x));
                group.Count++;
            }

            yield return group;
        }

        private static string Solve1()
        {
            var groups = GetGroups(Puzzle6Input.Input);
            var allSum = 0;

            foreach (var group in groups)
            {
                var distinctGroup = group.Answers.Distinct().ToList();

                allSum += distinctGroup.Count;
            }

            return allSum.ToString();
        }

        private static string Solve2()
        {
            var groups = GetGroups(Puzzle6Input.Input);
            var allSum = 0;

            foreach (var group in groups)
            {
                var currentAnswers = new List<bool>();
                var groupedAnswers = group.Answers
                    .GroupBy(x => x)
                    .Select( x => new { Answer = x, Count = x.Count() })
                    .Where(x => x.Count == group.Count)
                    .ToArray();

                allSum += groupedAnswers.Count();
            }

            return allSum.ToString();
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
