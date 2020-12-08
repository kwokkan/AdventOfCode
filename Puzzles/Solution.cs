namespace AdventOfCode2020.Puzzles
{
    public record Solution
    {
        public long Sample { get; init; }

        public long Part1 { get; init; }

        public long Part2 { get; init; }

        public Solution(long part1, long part2) => (Part1, Part2) = (part1, part2);

        public Solution(long sample, long part1, long part2) => (Sample, Part1, Part2) = (sample, part1, part2);
    }
}
