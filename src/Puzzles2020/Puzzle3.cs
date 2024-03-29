﻿using System.Collections.Generic;
using System.Linq;
using AdventOfCode.PuzzleCore;

namespace AdventOfCode.Puzzles2020;

public class Puzzle3 : PuzzleBase
{
    public Puzzle3()
        : base(2020, 3)
    {
    }

    private static int GetCount(int right, int down)
    {
        var lines = Puzzle3Input.Input;

        var count = 0;
        var currentPosition = right;

        for (int i = down; i < lines.Length; i += down)
        {
            var line = lines[i];

            if (line[currentPosition] == '#')
            {
                count++;
            }

            currentPosition += right;

            currentPosition %= line.Length;
        }

        return count;
    }

    public override long Solve1()
    {
        var count = GetCount(3, 1);

        return count;
    }

    public override long Solve2()
    {
        var counts = new List<long>
        {
            GetCount(1, 1),
            GetCount(3, 1),
            GetCount(5, 1),
            GetCount(7, 1),
            GetCount(1, 2),
        };

        return counts.Aggregate((x, y) => x * y);
    }
}
