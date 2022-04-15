using AdventOfCode.PuzzleCore;
using Xunit;

namespace AdventOfCode.PuzzleTestCore;

public abstract class TestBase<TPuzzleBase>
    where TPuzzleBase : PuzzleBase, new()
{
    protected readonly TPuzzleBase Puzzle;

    private readonly long? _expectedSample1;
    private readonly long? _expectedSample2;
    private readonly long _expectedAnswer1;
    private readonly long _expectedAnswer2;

    public TestBase()
    {
        Puzzle = new TPuzzleBase();
    }

    public TestBase(long expectedAnswer1, long expectedAnswer2)
        : this()
    {
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
            var expected = _expectedSample1;

            try
            {
                expected = Puzzle.ExpectedSample1;
            }
            catch (NotImplementedException)
            {
            }

            return (expected, Puzzle.Sample1());
        });
    }

    [SkippableFact]
    public void Sample2()
    {
        Run(() =>
        {
            var expected = _expectedSample2;

            try
            {
                expected = Puzzle.ExpectedSample2;
            }
            catch (NotImplementedException)
            {
            }

            return (expected, Puzzle.Sample2());
        });
    }

    [SkippableFact]
    public void Solution1()
    {
        Run(() =>
        {
            var expected = _expectedAnswer1;

            try
            {
                expected = Puzzle.ExpectedAnswer1;
            }
            catch (NotImplementedException)
            {
            }

            return (expected, Puzzle.Solve1());
        });
    }

    [SkippableFact]
    public void Solution2()
    {
        Run(() =>
        {
            var expected = _expectedAnswer2;

            try
            {
                expected = Puzzle.ExpectedAnswer2;
            }
            catch (NotImplementedException)
            {
            }

            return (expected, Puzzle.Solve2());
        });
    }
}
