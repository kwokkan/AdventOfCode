using Xunit;

namespace AdventOfCode2020.Puzzles.Tests
{
    public abstract class TestBase<TPuzzle>
        where TPuzzle : IPuzzle, new()
    {
        protected readonly IPuzzle Puzzle;

        private readonly string _expectedAnswer1;
        private readonly string _expectedAnswer2;

        private readonly Solution _solution;

        public TestBase(string expectedAnswer1, string expectedAnswer2)
        {
            Puzzle = new TPuzzle();

            _expectedAnswer1 = expectedAnswer1;
            _expectedAnswer2 = expectedAnswer2;

            _solution = Puzzle.Solve();
        }

        [Fact]
        public void Solution1()
        {
            Assert.NotNull(_solution.Part1);
            Assert.NotEmpty(_solution.Part1);
            Assert.Equal(_expectedAnswer1, _solution.Part1);
        }

        [Fact]
        public void Solution2()
        {
            Assert.NotNull(_solution.Part2);
            Assert.NotEmpty(_solution.Part2);
            Assert.Equal(_expectedAnswer2, _solution.Part2);
        }
    }
}
