using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Puzzles
{
    public class Puzzle1 : IPuzzle
    {
        private static string Solve1()
        {
            var allNumbers = new List<int>();

            var input = Puzzle1Input.Input;

            for (int outer = 0; outer < input.Length; outer++)
            {
                for (int inner = input.Length - 1; inner >= 0; inner--)
                {
                    if (input[inner] == input[outer])
                    {
                        continue;
                    }

                    if (input[inner] + input[outer] == 2020)
                    {
                        allNumbers.Add(input[inner] * input[outer]);

                        if (allNumbers.Count == 2)
                        {
                            break;
                        }
                    }
                }
            }

            return allNumbers.Aggregate((x, y) => x * y).ToString();
        }

        private static string Solve2()
        {
            throw new NotImplementedException();
        }

        Solution IPuzzle.Solve()
        {
            return new Solution
            {
                Part1 = Solve1(),
                Part2 = Solve2(),
            };
        }
    }
}
