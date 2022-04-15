namespace AdventOfCode.PuzzleCore
{
    public abstract class PuzzleBase
    {
        private readonly int _year;
        private readonly int _day;

        private static string[] GetFile(int day, string type, bool appendBlankLine = false)
        {
            var path = $"inputs/{day}-{type}.txt";

            var file = new FileInfo(path);

            if (!file.Exists)
            {
                throw new FileNotFoundException($"{type} for day {day} cannot be found.");
            }

            using var reader = file.OpenText();
            var lines = new List<string>();
            string? line = null;

            while ((line = reader.ReadLine()) != null)
            {
                lines.Add(line);
            }

            if (appendBlankLine)
            {
                lines.Add(string.Empty);
            }

            return lines.ToArray();
        }

        public PuzzleBase()
        {
        }

        public PuzzleBase(int year, int day)
        {
            _year = year;
            _day = day;
        }

        public virtual long ExpectedSample1 => throw new NotImplementedException($"{nameof(ExpectedSample1)} not implemented.");
        public virtual long ExpectedSample2 => throw new NotImplementedException($"{nameof(ExpectedSample2)} not implemented.");
        public virtual long ExpectedAnswer1 => throw new NotImplementedException($"{nameof(ExpectedAnswer1)} not implemented.");
        public virtual long ExpectedAnswer2 => throw new NotImplementedException($"{nameof(ExpectedAnswer2)} not implemented.");

        protected string[] GetSampleString(bool appendBlankLine = false)
        {
            return GetFile(_day, "sample", appendBlankLine: appendBlankLine);
        }

        protected string[] GetInputString(bool appendBlankLine = false)
        {
            return GetFile(_day, "input", appendBlankLine: appendBlankLine);
        }

        public virtual long Sample1()
        {
            throw new NotImplementedException($"{nameof(Sample1)} not implemented.");
        }

        public virtual long Sample2()
        {
            throw new NotImplementedException($"{nameof(Sample2)} not implemented.");
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
