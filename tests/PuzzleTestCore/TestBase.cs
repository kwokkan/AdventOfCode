using AdventOfCode.PuzzleCore;
using Xunit;

namespace AdventOfCode.PuzzleTestCore
{
    public abstract class TestBase<TPuzzleBase>
        where TPuzzleBase : PuzzleBase, new()
    {
        protected readonly TPuzzleBase Puzzle;

        private readonly long? _expectedSample1;
        private readonly long? _expectedSample2;
        private readonly long _expectedAnswer1;
        private readonly long _expectedAnswer2;

        public TestBase(long expectedAnswer1, long expectedAnswer2)
        {
            Puzzle = new TPuzzleBase();

            _expectedAnswer1 = expectedAnswer1;
            _expectedAnswer2 = expectedAnswer2;
        }

        public TestBase(long? expectedSample1, long? expectedSample2, long expectedAnswer1, long expectedAnswer2)
            : this(expectedAnswer1, expectedAnswer2)
        {
            _expectedSample1 = expectedSample1;
            _expectedSample2 = expectedSample2;
        }

        private static void Run(Action action)
        {
            try
            {
                action();
            }
            catch (NotImplementedException ex)
            {
                throw new SkipException(ex.Message, ex);
            }
        }

        [Fact]
        public void Sample1()
        {
            Run(() =>
            {
                var answer = Puzzle.Sample1();

                if (_expectedSample1.HasValue)
                {
                    Assert.Equal(_expectedSample1, answer);
                }
            });
        }

        [Fact]
        public void Sample2()
        {
            Run(() =>
            {
                var answer = Puzzle.Sample2();

                if (_expectedSample2.HasValue)
                {
                    Assert.Equal(_expectedSample2, answer);
                }
            });
        }

        [Fact]
        public void Solution1()
        {
            Run(() =>
            {
                var answer = Puzzle.Solve1();

                Assert.Equal(_expectedAnswer1, answer);
                Assert.NotEqual(default, _expectedAnswer1);
            });
        }

        [Fact]
        public void Solution2()
        {
            Run(() =>
            {
                var answer = Puzzle.Solve2();

                Assert.Equal(_expectedAnswer2, answer);
                Assert.NotEqual(default, _expectedAnswer2);
            });
        }
    }
}
