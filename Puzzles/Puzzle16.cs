using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.PuzzleCore;

namespace AdventOfCode2020.Puzzles
{
    public class Puzzle16 : PuzzleBase
    {
        private record Range
        {
            public int Min { get; init; }

            public int Max { get; init; }

            public Range(int min, int max) => (Min, Max) = (min, max);
        }

        private record Property
        {
            public string Field { get; init; }

            public Range First { get; init; }

            public Range Second { get; init; }
        }

        private record Ticket
        {
            public List<Property> Properties { get; init; } = new List<Property>();

            public List<int> YourTicket { get; init; } = new List<int>();

            public List<List<int>> NearbyTickets { get; init; } = new List<List<int>>();
        }

        private static bool InRanges(int value, params Range[] ranges)
        {
            foreach (var range in ranges)
            {
                if (range.Min <= value && value <= range.Max)
                {
                    return true;
                }
            }

            return false;
        }

        private static List<List<string>> SplitArray(string[] lines, string separator)
        {
            var newArray = new List<List<string>>();

            var newChunk = new List<string>();

            foreach (var line in lines)
            {
                if (line == separator)
                {
                    newArray.Add(newChunk);

                    newChunk = new List<string>();
                    continue;
                }

                newChunk.Add(line);
            }

            newArray.Add(newChunk);
            return newArray;
        }

        private static Ticket GetTicket(string[] lines)
        {
            var ticket = new Ticket();

            var parts = SplitArray(lines, string.Empty);

            foreach (var part in parts[0])
            {
                var lineParts = part.Split(": ");
                var className = lineParts[0];
                var rangeParts = lineParts[1].Split(" or ");

                var lowerRangeParts = rangeParts[0].Split("-");
                var upperRangeParts = rangeParts[1].Split("-");

                ticket.Properties.Add(new Property
                {
                    Field = className,
                    First = new Range(int.Parse(lowerRangeParts[0]), int.Parse(lowerRangeParts[1])),
                    Second = new Range(int.Parse(upperRangeParts[0]), int.Parse(upperRangeParts[1]))
                });
            }

            ticket.YourTicket.AddRange(parts[1][1].Split(",").Select(int.Parse).ToList());

            foreach (var part in parts[2].Skip(1))
            {
                ticket.NearbyTickets.Add(part.Split(",").Select(int.Parse).ToList());
            }

            return ticket;
        }

        private static long DetermineInvalid(Ticket ticket)
        {
            var sum = 0L;

            var allRanges = new List<Range>();

            foreach (var prop in ticket.Properties)
            {
                allRanges.Add(prop.First);
                allRanges.Add(prop.Second);
            }

            foreach (var nearby in ticket.NearbyTickets)
            {
                var currentSum = nearby.Where(x => allRanges.All(range => !InRanges(x, range))).Sum();

                sum += currentSum;
            }

            return sum;
        }

        public override long Sample()
        {
            var ticket = GetTicket(Puzzle16Input.Sample);
            var invalid = DetermineInvalid(ticket);

            return invalid;
        }

        public override long Solve1()
        {
            var ticket = GetTicket(Puzzle16Input.Input);
            var invalid = DetermineInvalid(ticket);

            return invalid;
        }

        public override long Solve2()
        {
            var input = Puzzle16Input.Input;

            return default;
        }
    }
}
