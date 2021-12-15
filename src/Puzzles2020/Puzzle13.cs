using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.PuzzleCore;

namespace AdventOfCode.Puzzles2020
{
    public class Puzzle13 : PuzzleBase
    {
        private record TimeTable
        {
            public int Time { get; init; }
            public string[] BusIds { get; init; }
        }

        private static TimeTable GetTimeTable(string[] lines)
        {
            var time = int.Parse(lines[0]);
            var ids = lines[1]
                .Split(',')
                .ToArray();

            return new TimeTable
            {
                Time = time,
                BusIds = ids,
            };
        }

        private static long GetEarliest(TimeTable timeTable)
        {
            var busIds = timeTable.BusIds.Where(x => x != "x").Select(int.Parse).ToArray();

            var max = busIds.Max();

            var mods = busIds.Select(x => new
            {
                max = Enumerable.Range(timeTable.Time, x).SkipWhile(r => r % x != 0).First(),
                id = x
            })
                .OrderBy(x => x.max)
                .ToArray();

            var first = mods.First();
            return first.id * (first.max - timeTable.Time);
        }

        private static long GetEarliestOffset(TimeTable timeTable)
        {
            var busIdOffsets = timeTable.BusIds.Select((x, y) => new
            {
                offset = y,
                id = x,
                intValue = int.TryParse(x, out var iv) ? (int?)iv : null
            }).ToArray();

            var validBuses = busIdOffsets
                .Where(x => x.intValue.HasValue)
                .OrderByDescending(x => x.intValue!.Value)
                .ToArray();

            var maxBus = validBuses.First();
            var maxOffset = busIdOffsets.Length;

            var validBusesWithoutMax = validBuses.Skip(1).ToArray();

            var nextMaxBus = validBusesWithoutMax.First();

            var loopIncrement = maxBus.intValue!.Value;

            var currentTime = loopIncrement * 1L;

            while (true)
            {
                var isValid = true;

                for (int i = 0; i < validBusesWithoutMax.Length; i++)
                {
                    var bus = validBusesWithoutMax[i];

                    var newCurrentTime = (currentTime - maxBus.offset) + bus.offset;

                    var mod = newCurrentTime % bus.intValue!.Value;

                    if (mod != 0)
                    {
                        isValid = false;
                        break;
                    }
                }

                if (isValid)
                {
                    return currentTime - maxBus.offset;
                }

                currentTime += loopIncrement;
            }
        }

        public override long Sample1()
        {
            var timeTable = GetTimeTable(Puzzle13Input.Sample);
            var earliest = GetEarliest(timeTable);

            return earliest;
        }

        public override long Solve1()
        {
            var timeTable = GetTimeTable(Puzzle13Input.Input);
            var earliest = GetEarliest(timeTable);

            return earliest;
        }

        public override long Solve2()
        {
            var timetable = GetTimeTable(Puzzle13Input.Sample);
            var earliest = GetEarliestOffset(timetable);

            return earliest;
        }
    }
}
