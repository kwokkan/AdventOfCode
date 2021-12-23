using AdventOfCode.PuzzleCore;

namespace AdventOfCode.Puzzles2021
{
    public class Puzzle8 : PuzzleBase
    {
        private record Signal(string[] Patterns, string[] Values);

        private record KeyValue
        {
            public string Signal { get; set; }

            public int Value { get; set; }

            public KeyValue(string signal, int value)
            {
                (Signal, Value) = (signal, value);
            }
        }

        public Puzzle8()
            : base(2021, 8)
        {
        }

        private static Signal[] InputAs(string[] input)
        {
            var output = new List<Signal>(input.Length);

            foreach (var current in input)
            {
                var parts = current.Split(" | ");

                output.Add(new Signal(parts[0].Split(" "), parts[1].Split(" ")));
            }

            return output.ToArray();
        }

        private static KeyValue[] CreateBasicMappings(string[] input)
        {
            var output = new List<KeyValue>(input.Length);

            for (int i = 0; i < input.Length; i++)
            {
                var current = input[i];
                var currentValue = -1;

                switch (current.Length)
                {
                    case 2:
                        currentValue = 1;
                        break;
                    case 3:
                        currentValue = 7;
                        break;
                    case 4:
                        currentValue = 4;
                        break;
                    case 7:
                        currentValue = 8;
                        break;
                }

                output.Add(new KeyValue(new string(current.OrderBy(c => c).ToArray()), currentValue));
            }

            return output.ToArray();
        }

        private static void PopulateAdvancedMappings(KeyValue[] input)
        {
            var digit7 = input.First(x => x.Value == 7);

            foreach (var item in input.OrderByDescending(x => x.Signal.Length).ToList())
            {
                if (item.Value != -1)
                {
                    continue;
                }

                // 0, 6, 9
                if (item.Signal.Length == 6)
                {
                    var digit4 = input.First(x => x.Value == 4);

                    if (digit4.Signal.All(key => item.Signal.Contains(key)))
                    {
                        item.Value = 9;
                    }
                    else if (digit7.Signal.All(key => item.Signal.Contains(key)))
                    {
                        item.Value = 0;
                    }
                    else
                    {
                        item.Value = 6;
                    }
                }
                else if (item.Signal.Length == 5)
                {
                    var digit6 = input.First(x => x.Value == 6);

                    if (digit7.Signal.All(key => item.Signal.Contains(key)))
                    {
                        item.Value = 3;
                    }
                    else if (item.Signal.All(key => digit6.Signal.Contains(key)))
                    {
                        item.Value = 5;
                    }
                    else
                    {
                        item.Value = 2;
                    }
                }
            }
        }

        private static int DecodeValue(Signal input)
        {
            var mappings = CreateBasicMappings(input.Patterns);

            PopulateAdvancedMappings(mappings);

            var numbers = input.Values
                .Select(value => mappings.First(map => map.Signal == new string(value.OrderBy(v => v).ToArray())))
                .Select(x => x.Value).ToList();

            return int.Parse(string.Join(string.Empty, numbers));
        }

        private static long Calculate1(Signal[] input)
        {
            var uniqueLengths = new[]
            {
                2,
                4,
                3,
                7,
            };

            var count = 0;

            count = input.SelectMany(x => x.Values).Count(x => uniqueLengths.Contains(x.Length));

            return count;
        }

        private static long Calculate2(Signal[] input)
        {
            var count = 0;

            foreach (var current in input)
            {
                count += DecodeValue(current);
            }

            return count;
        }

        public override long Sample1()
        {
            var input = InputAs(GetSampleString());

            return Calculate1(input);
        }

        public override long Sample2()
        {
            var input = InputAs(GetSampleString());

            return Calculate2(input);
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
