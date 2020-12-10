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

    public abstract class TestBase<TPuzzle, TPuzzleBase>
        where TPuzzle : IPuzzle, new()
        where TPuzzleBase : PuzzleBase, new()
    {
        protected readonly TPuzzleBase Puzzle;

        private readonly long? _expectedSample;
        private readonly long _expectedAnswer1;
        private readonly long _expectedAnswer2;

        public TestBase(long expectedAnswer1, long expectedAnswer2)
        {
            Puzzle = new TPuzzleBase();

            _expectedAnswer1 = expectedAnswer1;
            _expectedAnswer2 = expectedAnswer2;
        }

        public TestBase(int expectedSample, long expectedAnswer1, long expectedAnswer2)
            : this(expectedAnswer1, expectedAnswer2)
        {
            _expectedSample = expectedSample;
        }

        [Fact]
        public void Sample()
        {
            var answer = Puzzle.Sample();

            if (_expectedSample.HasValue)
            {
                Assert.Equal(_expectedSample, answer);
            }
        }

        [Fact]
        public void Solution1()
        {
            var answer = Puzzle.Solve1();

            Assert.Equal(_expectedAnswer1, answer);
        }

        [Fact]
        public void Solution2()
        {
            var answer = Puzzle.Solve2();

            Assert.Equal(_expectedAnswer2, answer);
        }
    }
}
