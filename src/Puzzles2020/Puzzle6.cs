using System.Collections.Generic;
using System.Linq;
using AdventOfCode.PuzzleCore;

namespace AdventOfCode2020.Puzzles
{
    public class Puzzle6 : PuzzleBase
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

        public override long Solve1()
        {
            var groups = GetGroups(Puzzle6Input.Input).ToList();
            var allSum = 0;

            foreach (var group in groups)
            {
                var distinctGroup = group.Answers.Distinct().Count();

                allSum += distinctGroup;
            }

            return allSum;
        }

        public override long Solve2()
        {
            var groups = GetGroups(Puzzle6Input.Input).ToList();
            var allSum = 0;

            foreach (var group in groups)
            {
                var groupedAnswers = group.Answers
                    .GroupBy(x => x)
                    .Select(x => new { Answer = x, Count = x.Count() })
                    .Count(x => x.Count == group.Count);

                allSum += groupedAnswers;
            }

            return allSum;
        }
    }
}
