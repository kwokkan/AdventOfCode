using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Puzzles
{
    public class Puzzle9 : PuzzleBase
    {
        private static bool HasSum(IList<long> values, long sum)
        {
            for (int i = 0; i < values.Count; i++)
            {
                for (int j = 0; j < values.Count; j++)
                {
                    if (i == j)
                    {
                        continue;
                    }

                    if (values[i] + values[j] == sum)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private static long GetEarliest(long[] values, int preamble)
        {
            var earliest = 0L;

            for (int i = preamble; i < values.Length; i++)
            {
                var currentValue = values[i];

                var prevValues = new List<long>();

                for (int j = i - 1; j >= i - preamble; j--)
                {
                    prevValues.Add(values[j]);
                }

                if (!HasSum(prevValues, currentValue))
                {
                    earliest = currentValue;
                    break;
                }
            }

            return earliest;
        }

        public override long Sample()
        {
            return GetEarliest(Puzzle9Input.Sample, 5);
        }

        public override long Solve1()
        {
            var earliest = GetEarliest(Puzzle9Input.Input, 25);

            return earliest;
        }

        public override long Solve2()
        {
            var earliest = GetEarliest(Puzzle9Input.Input, 25);

            var values = Puzzle9Input.Input;

            var counted = new List<long>(values.Length);

            var sum = 0L;

            for (int i = 0; i < values.Length; i++)
            {
                counted.Clear();

                var found = false;

                for (int j = i; j < values.Length; j++)
                {
                    counted.Add(values[j]);

                    var currentSum = counted.Sum();

                    if (currentSum == earliest && counted.Count > 2)
                    {
                        found = true;
                        break;
                    }
                    if (currentSum > earliest)
                    {
                        break;
                    }
                }

                if (found)
                {
                    sum = counted.Min() + counted.Max();
                    break;
                }
            }

            return sum;
        }
    }
}
