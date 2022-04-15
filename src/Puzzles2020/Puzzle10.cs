using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.PuzzleCore;

namespace AdventOfCode.Puzzles2020;

public class Puzzle10 : PuzzleBase
{
    public Puzzle10()
        : base(2020, 10)
    {
    }

    private static (int, int, int) GetJolts(int[] input)
    {
        var sorted = input.OrderBy(x => x).ToList();

        var jolt1 = 0;
        var jolt2 = 0;
        var jolt3 = 1;

        switch (sorted[0])
        {
            case 1:
                jolt1++;
                break;
            case 3:
                jolt3++;
                break;
        }

        for (int i = 1; i < sorted.Count; i++)
        {
            var curr = sorted[i];
            var prev = sorted[i - 1];

            var diff = curr - prev;

            switch (diff)
            {
                case 1:
                    jolt1++;
                    break;
                case 3:
                    jolt3++;
                    break;
            }
        }

        return (jolt1, jolt2, jolt3);
    }

    private static int[] GetDiffs(int[] input)
    {
        var sorted = input.OrderBy(x => x).ToList();
        var diffs = new List<int>(sorted.Count);

        for (int i = 0; i < sorted.Count - 1; i++)
        {
            diffs.Add(sorted[i + 1] - sorted[i]);
        }

        return diffs.ToArray();
    }

    private static IEnumerable<(int Jolt, int Count)> GetBatchCount(int[] input)
    {
        var count = 0;
        var prev = 0;
        foreach (var item in input)
        {
            if (item == prev)
            {
                count++;
            }
            else
            {
                if (prev != 0)
                {
                    yield return (prev, count);
                }

                prev = item;
                count = 1;
            }
        }

        yield return (prev, count);
    }

    public override long Sample1()
    {
        var jolts = GetJolts(Puzzle10Input.Sample);

        return jolts.Item1 * jolts.Item3;
    }

    public override long Solve1()
    {
        var jolts = GetJolts(Puzzle10Input.Input);

        return jolts.Item1 * jolts.Item3;
    }

    public override long Solve2()
    {
        var sorted = Puzzle10Input.Input.OrderBy(x => x).ToList();

        sorted.Add(0);
        sorted.Add(sorted.Max() + 3);
        sorted.Sort();

        var diffs = GetDiffs(sorted.ToArray());

        var batches = GetBatchCount(diffs).ToArray();

        var d = 1L;

        foreach (var batch in batches)
        {
            var multiplier = 1;

            if (batch.Jolt == 1)
            {
                var count = batch.Count;


                if (count == 2)
                {
                    multiplier = 2;
                }
                else if (count == 3)
                {
                    multiplier = 4;
                }
                else if (count == 4)
                {
                    multiplier = 7;
                }
                else if (count == 5)
                {
                    multiplier = 11;
                }
                else
                {
                    var div = (int)Math.Floor(count / 4f);
                    var mod = count % 4;

                    if (div > 0)
                    {
                        var pow = (int)Math.Pow(6, div);

                        var curPow = div - 1;

                        while (curPow > 0)
                        {
                            pow *= (int)Math.Pow(6, curPow);
                            curPow--;
                        }

                        multiplier = pow;
                    }

                    switch (mod)
                    {
                        case 3:
                            multiplier *= 3;
                            break;
                        case 2:
                            multiplier *= 2;
                            break;
                    }
                }
            }

            d *= multiplier;
        }

        return d;
    }
}
