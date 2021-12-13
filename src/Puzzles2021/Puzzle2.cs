using AdventOfCode.PuzzleCore;

namespace AdventOfCode.Puzzles2021
{
    public class Puzzle2 : PuzzleBase
    {
        private record Instruction
        {
            public string Command { get; init; }

            public int Value { get; init; }

            public Instruction(string command, int value)
            {
                Command = command;
                Value = value;
            }
        }

        public Puzzle2()
            : base(2021, 2)
        {
        }

        private static Instruction[] InputAs(string[] input)
        {
            var output = new List<Instruction>();

            foreach (var current in input)
            {
                var parts = current.Split(" ");

                output.Add(new Instruction(parts[0], int.Parse(parts[1])));
            }

            return output.ToArray();
        }

        private static long Calculate1(Instruction[] input)
        {
            var x = 0;
            var y = 0;

            foreach (var current in input)
            {
                switch (current.Command)
                {
                    case "forward":
                        x += current.Value;
                        break;
                    case "down":
                        y += current.Value;
                        break;
                    case "up":
                        y -= current.Value;
                        break;
                    default:
                        break;
                }
            }

            return x * y;
        }

        private static long Calculate2(Instruction[] input)
        {
            var x = 0;
            var y = 0;
            var z = 0;

            foreach (var current in input)
            {
                switch (current.Command)
                {
                    case "forward":
                        x += current.Value;
                        z += y * current.Value;
                        break;
                    case "down":
                        y += current.Value;
                        break;
                    case "up":
                        y -= current.Value;
                        break;
                    default:
                        break;
                }
            }

            return x * z;
        }

        public override long Sample()
        {
            var input = InputAs(GetSampleString());

            return Calculate1(input);
        }

        public override long Solve1()
        {
            var input = InputAs(GetInputString());

            return Calculate1(input);
        }

        public override long Solve2()
        {
            var input = InputAs(GetInputString());

            return Calculate2(input);
        }
    }
}
