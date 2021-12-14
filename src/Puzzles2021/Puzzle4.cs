using AdventOfCode.PuzzleCore;

namespace AdventOfCode.Puzzles2021
{
    public class Puzzle4 : PuzzleBase
    {
        private record Report
        {
            public int[] Numbers { get; init; }

            public int[][][] Boards { get; init; }

            public Report(int[] numbers, int[][][] boards)
            {
                Numbers = numbers;
                Boards = boards;
            }
        }

        private const int GridSize = 5;

        public Puzzle4()
            : base(2021, 4)
        {
        }

        private static Report InputAs(string[] input)
        {
            var numbers = input[0].Split(",").Select(int.Parse).ToArray();
            var boards = new List<int[][]>();

            var board = new List<int[]>();
            for (int i = 2; i < input.Length; i++)
            {
                var current = input[i];

                if (string.IsNullOrWhiteSpace(current))
                {
                    boards.Add(board.ToArray());

                    board = new List<int[]>();

                    continue;
                }

                var rows = current.Split(" ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                board.Add(rows.ToArray());
            }

            return new Report(numbers.ToArray(), boards.ToArray());
        }


        private static bool HasWon(List<int> numbers, int[][] board)
        {
            if (numbers.Count < GridSize)
            {
                return false;
            }

            // rows
            for (int i = 0; i < GridSize; i++)
            {
                var correct = 0;

                for (int j = 0; j < GridSize; j++)
                {
                    correct += numbers.Contains(board[i][j]) ? 1 : 0;
                }

                if (correct == GridSize)
                {
                    return true;
                }
            }

            // columns
            for (int i = 0; i < GridSize; i++)
            {
                var correct = 0;

                for (int j = 0; j < GridSize; j++)
                {
                    correct += numbers.Contains(board[j][i]) ? 1 : 0;
                }

                if (correct == GridSize)
                {
                    return true;
                }
            }

            return false;
        }

        private static int GetUnmarkedSum(List<int> numbers, int[][] board)
        {
            return board.SelectMany(x => x).Where(x => !numbers.Contains(x)).Sum();
        }

        private static long GetScore1(Report report)
        {
            var pickedNumbers = new List<int>();

            foreach (var number in report.Numbers)
            {
                pickedNumbers.Add(number);

                foreach (var board in report.Boards)
                {
                    if (HasWon(pickedNumbers, board))
                    {
                        var unmarked = GetUnmarkedSum(pickedNumbers, board);

                        return unmarked * number;
                    }
                }
            }

            return default;
        }

        private static long GetScore2(Report report)
        {
            var pickedNumbers = new List<int>();
            var remainingBoards = report.Boards.Select(x => x).ToList();
            int[][]? lastBoardToWin = null;
            int lastWinNumber = -1;

            foreach (var number in report.Numbers)
            {
                pickedNumbers.Add(number);

                for (int i = 0; i < remainingBoards.Count; i++)
                {
                    var board = remainingBoards[i];

                    if (HasWon(pickedNumbers, board))
                    {
                        lastBoardToWin = board;
                        lastWinNumber = number;

                        remainingBoards.RemoveAt(i);
                        i--;
                    }
                }

                if (!remainingBoards.Any())
                {
                    break;
                }
            }

            var unmarked = GetUnmarkedSum(pickedNumbers, lastBoardToWin!);

            return unmarked * lastWinNumber;
        }

        private static long Calculate1(string[] input)
        {
            var report = InputAs(input);

            return GetScore1(report);
        }

        private static long Calculate2(string[] input)
        {
            var report = InputAs(input);

            return GetScore2(report);
        }

        public override long Sample1()
        {
            var input = GetSampleString(appendBlankLine: true);

            return Calculate1(input);
        }

        public override long Sample2()
        {
            var input = GetSampleString(appendBlankLine: true);

            return Calculate2(input);
        }

        public override long Solve1()
        {
            var input = GetInputString(appendBlankLine: true);

            return Calculate1(input);
        }

        public override long Solve2()
        {
            var input = GetInputString(appendBlankLine: true);

            return Calculate2(input);
        }
    }
}
