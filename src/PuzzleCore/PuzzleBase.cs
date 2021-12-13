namespace AdventOfCode.PuzzleCore
{
    public abstract class PuzzleBase
    {
        private readonly int _year;
        private readonly int _day;

        private static string[] GetFile(int day, string type)
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

        protected string[] GetSampleString()
        {
            return GetFile(_day, "sample");
        }

        protected string[] GetInputString()
        {
            return GetFile(_day, "input");
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
