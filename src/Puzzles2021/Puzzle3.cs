using AdventOfCode.PuzzleCore;

namespace AdventOfCode.Puzzles2021
{
    public class Puzzle3 : PuzzleBase
    {
        private record Report
        {
            public int Pos { get; set; }

            public int Neg { get; set; }
        }

        public Puzzle3()
            : base(2021, 3)
        {
        }

        private static long GetConsumption(Report[] input)
        {
            var posStr = "";
            var negStr = "";

            foreach (var current in input)
            {
                if (current.Pos > current.Neg)
                {
                    posStr += "1";
                    negStr += "0";
                }
                else
                {
                    posStr += "0";
                    negStr += "1";
                }
            }

            return Convert.ToInt32(posStr, 2) * Convert.ToInt32(negStr, 2);
        }

        private static Report[] GetReports(string[] input)
        {
            var width = input[0].Length;

            var reports = Enumerable.Range(0, width).Select(x => new Report()).ToArray();

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < input.Length; j++)
                {
                    var current = input[j][i];

                    if (current == '1')
                    {
                        reports[i].Pos++;
                    }
                    else
                    {
                        reports[i].Neg++;
                    }
                }
            }

            return reports;
        }

        private static long GetMost(Report[] reports, string[] input)
        {
            var ratings = input;
            var latestReports = reports;

            for (int i = 0; i < latestReports.Length; i++)
            {
                var currentReport = latestReports[i];

                if (currentReport.Pos > currentReport.Neg || currentReport.Pos == currentReport.Neg)
                {
                    ratings = ratings.Where(x => x[i] == '1').ToArray();
                }
                else
                {
                    ratings = ratings.Where(x => x[i] == '0').ToArray();
                }

                if (ratings.Length == 1)
                {
                    break;
                }

                latestReports = GetReports(ratings);
            }

            return Convert.ToInt32(ratings[0], 2);
        }

        private static long GetLeast(Report[] reports, string[] input)
        {
            var ratings = input;
            var latestReports = reports;

            for (int i = 0; i < latestReports.Length; i++)
            {
                var currentReport = latestReports[i];

                if (currentReport.Pos > currentReport.Neg || currentReport.Pos == currentReport.Neg)
                {
                    ratings = ratings.Where(x => x[i] == '0').ToArray();
                }
                else
                {
                    ratings = ratings.Where(x => x[i] == '1').ToArray();
                }

                if (ratings.Length == 1)
                {
                    break;
                }

                latestReports = GetReports(ratings);
            }

            return Convert.ToInt32(ratings[0], 2);
        }

        private static long Calculate1(string[] input)
        {
            var reports = GetReports(input);

            return GetConsumption(reports);
        }

        private static long Calculate2(string[] input)
        {
            var reports = GetReports(input);

            var oxygen = GetMost(reports, input);
            var co2 = GetLeast(reports, input);

            return oxygen * co2;
        }

        public override long Sample1()
        {
            var input = GetSampleString();

            return Calculate1(input);
        }

        public override long Sample2()
        {
            var input = GetSampleString();

            return Calculate2(input);
        }

        public override long Solve1()
        {
            var input = GetInputString();

            return Calculate1(input);
        }

        public override long Solve2()
        {
            var input = GetInputString();

            return Calculate2(input);
        }
    }
}
