namespace AdventOfCode2020.Puzzles
{
    public record Solution
    {
        public int Sample { get; init; }

        public string Part1 { get; init; }

        public string Part2 { get; init; }

        public Solution(string part1, string part2) => (Part1, Part2) = (part1, part2);

        public Solution(int sample, string part1, string part2) => (Sample, Part1, Part2) = (sample, part1, part2);
    }
}
