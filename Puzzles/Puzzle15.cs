using System.Collections.Generic;

namespace AdventOfCode2020.Puzzles
{
    public class Puzzle15 : PuzzleBase
    {
        private static int GetNthNumber(int[] input, int nth)
        {
            var usedNumbers = new List<int>(nth);
            usedNumbers.AddRange(input);

            var used = new Dictionary<int, List<int>>();

            var counter = 0;
            var prevNumber = 0;

            foreach (var item in input)
            {
                used.Add(item, new List<int>
                {
                    counter
                });

                prevNumber = item;

                counter++;
            }

            while (counter < nth)
            {
                if (used.ContainsKey(prevNumber) && used[prevNumber].Count > 1)
                {
                    var prevList = used[prevNumber];

                    var prevIndex = prevList[^2];

                    var newNum = counter - prevIndex - 1;

                    if (used.ContainsKey(newNum))
                    {
                        used[newNum].Add(counter);
                    }
                    else
                    {
                        used.Add(newNum, new List<int>
                        {
                            counter
                        });
                    }

                    prevNumber = newNum;
                }
                else
                {
                    used[0].Add(counter);

                    prevNumber = 0;
                }

                counter++;
            }

            return prevNumber;
        }

        public override long Sample()
        {
            var number = GetNthNumber(Puzzle15Input.Sample, 2020);

            return number;
        }

        public override long Solve1()
        {
            var number = GetNthNumber(Puzzle15Input.Input, 2020);

            return number;
        }

        public override long Solve2()
        {
            var number = GetNthNumber(Puzzle15Input.Input, 30000000);

            return number;
        }
    }
}
