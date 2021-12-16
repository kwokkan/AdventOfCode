using AdventOfCode.PuzzleCore;

namespace AdventOfCode.Puzzles2021
{
    public class Puzzle14 : PuzzleBase
    {
        private record Manual
        {
            public string Template { get; init; }

            public Dictionary<string, char> Mappings { get; init; } = new Dictionary<string, char>();

            public Manual(string template)
            {
                Template = template;
            }
        }

        public Puzzle14()
            : base(2021, 14)
        {
        }

        private static Manual InputAs(string[] input)
        {
            var output = new Manual(input[0]);

            for (int i = 2; i < input.Length; i++)
            {
                var curernt = input[i];
                var parts = curernt.Split("->", StringSplitOptions.TrimEntries);

                output.Mappings.Add(parts[0], parts[1].ToCharArray()[0]);
            }

            return output;
        }

        private static void Increase(Manual manual, ref List<char> input)
        {
            var inputCount = input.Count - 1;
            var insertions = new Queue<char>(inputCount);

            for (int i = 0; i < inputCount; i++)
            {
                var current = $"{input[i]}{input[i + 1]}";
                insertions.Enqueue(manual.Mappings[current]);
            }

            var output = new List<char>(input.Count * 2);

            for (int i = 0; i < inputCount; i++)
            {
                output.Add(input[i]);
                output.Add(insertions.Dequeue());
            }
            output.Add(input[inputCount]);

            input = output;
        }

        private static (long Min, long Max) GetMinMax(List<char> input)
        {
            var grouped = input.ToLookup(x => x).Select(x => new { x.Key, Count = x.LongCount() }).ToArray();

            return (grouped.Min(x => x.Count), grouped.Max(x => x.Count));
        }

        private static long Calculate(Manual input, int iterations)
        {
            var output = input.Template.Select(x => x).ToList();

            for (int i = 0; i < iterations; i++)
            {
                Increase(input, ref output);
            }

            var minMax = GetMinMax(output);

            return minMax.Max - minMax.Min;
        }

        public override long Sample1()
        {
            var input = InputAs(GetSampleString());

            return Calculate(input, 10);
        }

        //TODO: Optimise
        /*public override long Sample2()
        {
            var input = InputAs(GetSampleString());

            return Calculate(input, 40);
        }*/

        public override long Solve1()
        {
            var input = InputAs(GetInputString());

            return Calculate(input, 10);
        }

        /*public override long Solve2()
        {
            var input = InputAs(GetInputString());

            return Calculate(input, 40);
        }*/
    }
}
