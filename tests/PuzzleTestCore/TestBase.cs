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

        private static void Run(Func<(long? Expected, long Actual)> action)
        {
            try
            {
                var result = action();

                Assert.Equal(result.Expected, result.Actual);
            }
            catch (NotImplementedException ex)
            {
                throw new SkipException(ex.Message, ex);
            }
        }

        [SkippableFact]
        public void Sample1()
        {
            Run(() =>
            {
                return (_expectedSample1, Puzzle.Sample1());
            });
        }

        [SkippableFact]
        public void Sample2()
        {
            Run(() =>
            {
                return (_expectedSample2, Puzzle.Sample2());
            });
        }

        [SkippableFact]
        public void Solution1()
        {
            Run(() =>
            {
                return (_expectedAnswer1, Puzzle.Solve1());
            });
        }

        [SkippableFact]
        public void Solution2()
        {
            Run(() =>
            {
                return (_expectedAnswer2, Puzzle.Solve2());
            });
        }
    }
}
