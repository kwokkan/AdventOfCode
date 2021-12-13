namespace AdventOfCode.PuzzleCore
{
    public abstract class PuzzleBase
    {
        public virtual long Sample()
        {
            throw new NotImplementedException($"{nameof(Sample)} not implemented.");
        }

        public virtual long Solve1()
        {
            throw new NotImplementedException($"{nameof(Solve1)} not implemented.");
        }

        public virtual long Solve2()
        {
            throw new NotImplementedException($"{nameof(Solve2)} not implemented.");
        }
    }
}
