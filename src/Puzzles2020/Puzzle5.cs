using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.PuzzleCore;

namespace AdventOfCode.Puzzles2020;

public class Puzzle5 : PuzzleBase
{
    public Puzzle5()
        : base(2020, 5)
    {
    }

    private static int GetBinary(params bool[] pos)
    {
        var allNumbers = Enumerable.Range(0, (int)Math.Pow(2, pos.Length)).ToList();

        for (int i = 0; i < pos.Length; i++)
        {
            var mid = allNumbers.Count / 2;

            if (pos[i])
            {
                allNumbers.RemoveRange(mid, mid);
            }
            else
            {
                allNumbers.RemoveRange(0, mid);
            }
        }

        return allNumbers[0];
    }

    public override long Solve1()
    {
        var seats = Puzzle5Input.Input;
        var maxSeat = 0;

        foreach (var seat in seats)
        {
            var currentRow = GetBinary(seat.Take(7).Select(x => x == 'F').ToArray());
            var currentColumn = GetBinary(seat.Skip(7).Take(3).Select(x => x == 'L').ToArray());

            var currentSeat = (currentRow * 8) + currentColumn;

            if (currentSeat > maxSeat)
            {
                maxSeat = currentSeat;
            }
        }

        return maxSeat;
    }

    public override long Solve2()
    {
        var seats = Puzzle5Input.Input;
        var allSeats = new List<int>();

        foreach (var seat in seats)
        {
            var currentRow = GetBinary(seat.Take(7).Select(x => x == 'F').ToArray());
            var currentColumn = GetBinary(seat.Skip(7).Take(3).Select(x => x == 'L').ToArray());

            var currentSeat = (currentRow * 8) + currentColumn;

            allSeats.Add(currentSeat);
        }

        var maxFront = (0 * 8) + 7;
        var minBack = (127 * 8) + 0;

        allSeats.RemoveAll(x => x <= maxFront || x >= minBack);
        allSeats.Sort();

        var foundSeat = 0;
        for (int i = maxFront; i < minBack; i++)
        {
            if (!allSeats.Contains(i) && allSeats.Contains(i - 1) && allSeats.Contains(i + 1))
            {
                foundSeat = i;
                break;
            }
        }

        return foundSeat;
    }
}
