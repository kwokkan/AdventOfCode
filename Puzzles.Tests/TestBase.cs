using Xunit;

namespace AdventOfCode2020.Puzzles.Tests
{
    public abstract class TestBase<TPuzzle>
        where TPuzzle : IPuzzle, new()
    {
        protected readonly IPuzzle Puzzle;

        private readonly string _expectedAnswer1;
        private readonly string _expectedAnswer2;

        public TestBase(string expectedAnswer1, string expectedAnswer2)
        {
            Puzzle = new TPuzzle();

            _expectedAnswer1 = expectedAnswer1;
            _expectedAnswer2 = expectedAnswer2;
        }

        [Fact]
        public void TestSolution()
        {
            var solution = Puzzle.Solve();

            Assert.Equal(_expectedAnswer1, solution.Part1);
            Assert.Equal(_expectedAnswer2, solution.Part2);
        }
    }
}
