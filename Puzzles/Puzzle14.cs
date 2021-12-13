using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.PuzzleCore;

namespace AdventOfCode2020.Puzzles
{
    public class Puzzle14 : PuzzleBase
    {
        private record Program
        {
            public string Mask { get; init; }
            public List<(int Address, long Value)> Mems { get; init; } = new List<(int Address, long Value)>();
        }

        private static long[] GetMaskValues(string bits, char mask)
        {
            var maskLengthIndex = bits.Length - 1;

            return bits.Select((x, y) =>
                x == mask ? (long)Math.Pow(2, maskLengthIndex - y) : 0
            ).ToArray();
        }

        private static IEnumerable<Program> GetPrograms(string[] lines)
        {
            Program program = null;

            foreach (var line in lines)
            {
                if (line.StartsWith("mask = "))
                {
                    if (program != null)
                    {
                        yield return program;
                    }

                    program = new Program
                    {
                        Mask = line[7..],
                    };
                }
                else if (line.StartsWith("mem["))
                {
                    var values = line.Split(" = ");
                    var mem = int.Parse(values[0][4..^1]);
                    var value = long.Parse(values[1]);

                    program!.Mems.Add((mem, value));
                }
            }

            yield return program!;
        }

        private static long ApplyMasks(Program[] programs)
        {
            var memValues = new Dictionary<int, long>();

            foreach (var program in programs)
            {
                var posMask = GetMaskValues(program.Mask, '1');
                var negMask = GetMaskValues(program.Mask, '0');

                foreach (var mem in program.Mems)
                {
                    var newValue = mem.Value;

                    foreach (var pm in posMask)
                    {
                        newValue |= pm;
                    }

                    foreach (var nm in negMask)
                    {
                        if ((newValue & nm) == nm)
                        {
                            newValue -= nm;
                        }
                    }

                    memValues[mem.Address] = newValue;
                }
            }

            return memValues.Values.Sum();
        }

        public override long Sample()
        {
            var programs = GetPrograms(Puzzle14Input.Sample).ToArray();
            var sum = ApplyMasks(programs);

            return sum;
        }

        public override long Solve1()
        {
            var programs = GetPrograms(Puzzle14Input.Input).ToArray();
            var sum = ApplyMasks(programs);

            return sum;
        }

        public override long Solve2()
        {
            var input = Puzzle14Input.Input;

            return default;
        }
    }
}
