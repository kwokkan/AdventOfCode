using AdventOfCode.PuzzleCore;

namespace AdventOfCode.Puzzles2021
{
    public class Puzzle10 : PuzzleBase
    {
        private static readonly Dictionary<char, char> Mappings = new Dictionary<char, char>
        {
            { '(', ')' },
            { '[', ']' },
            { '{', '}' },
            { '<', '>' },
        };

        private static readonly Dictionary<char, int> Points = new Dictionary<char, int>
        {
            { ')', 3 },
            { ']', 57 },
            { '}', 1197 },
            { '>', 25137 },
        };

        private static readonly char[] Points2 = new char[]
        {
            ')',
            ']',
            '}',
            '>',
        };

        public Puzzle10()
            : base(2021, 10)
        {
        }

        private static bool IsOpening(char input)
        {
            return !Points.ContainsKey(input);
        }

        private static (char[] Openings, char? Corrupt) ParseLine(string input)
        {
            var openings = new Stack<char>();
            char? corrupt = null;

            foreach (var current in input)
            {
                if (IsOpening(current))
                {
                    openings.Push(current);
                }
                else
                {
                    var lastOpening = openings.Peek();
                    var mappedClosing = Mappings[lastOpening];

                    if (mappedClosing == current)
                    {
                        openings.Pop();
                    }
                    else
                    {
                        corrupt = current;
                        break;
                    }
                }
            }

            return (openings.ToArray(), corrupt);
        }

        private static long GetIncompletePoints(char[] input)
        {
            var total = 0L;

            foreach (var current in input)
            {
                total *= 5;
                total += (Array.IndexOf(Points2, Mappings[current]) + 1);
            }

            return total;
        }

        private static long Calculate1(string[] input)
        {
            var corrupted = new List<char>();

            foreach (var current in input)
            {
                var parsed = ParseLine(current);

                if (parsed.Corrupt.HasValue)
                {
                    corrupted.Add(parsed.Corrupt.Value);
                }
            }

            return corrupted.Select(x => Points[x]).Sum();
        }

        private static long Calculate2(string[] input)
        {
            var incomplete = new List<long>();

            foreach (var current in input)
            {
                var parsed = ParseLine(current);

                if (!parsed.Corrupt.HasValue)
                {
                    incomplete.Add(GetIncompletePoints(parsed.Openings));
                }
            }

            incomplete.Sort();

            return incomplete[(incomplete.Count / 2)];
        }

        public override long Sample1()
        {
            var input = GetSampleString();

            return Calculate1(input);
        }

        public override long Sample2()
        {
            var input = GetSampleString();

            return Calculate2(input);
        }

        public override long Solve1()
        {
            var input = GetInputString();

            return Calculate1(input);
        }

        public override long Solve2()
        {
            var input = GetInputString();

            return Calculate2(input);
        }
    }
}
