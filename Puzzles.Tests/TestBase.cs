using Xunit;

namespace AdventOfCode2020.Puzzles.Tests
{
    public abstract class TestBase<TPuzzle>
        where TPuzzle : IPuzzle, new()
    {
        protected readonly IPuzzle Puzzle;

        private readonly long? _expectedSample;
        private readonly long _expectedAnswer1;
        private readonly long _expectedAnswer2;

        public TestBase(long expectedAnswer1, long expectedAnswer2)
        {
            Puzzle = new TPuzzle();

            _expectedAnswer1 = expectedAnswer1;
            _expectedAnswer2 = expectedAnswer2;
        }

        public TestBase(int expectedSample, long expectedAnswer1, long expectedAnswer2)
            : this(expectedAnswer1, expectedAnswer2)
        {
            _expectedSample = expectedSample;
        }

        [Fact]
        public void Solution()
        {
            var solution = Puzzle.Solve();

            if (_expectedSample.HasValue)
            {
                Assert.Equal(_expectedSample, solution.Sample);
            }

            Assert.Equal(_expectedAnswer1, solution.Part1);
            Assert.NotEqual(default, solution.Part1);

            Assert.Equal(_expectedAnswer2, solution.Part2);
            Assert.NotEqual(default, solution.Part2);
        }
    }
}
