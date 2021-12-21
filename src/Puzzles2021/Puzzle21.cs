using AdventOfCode.PuzzleCore;

namespace AdventOfCode.Puzzles2021
{
    public class Puzzle21 : PuzzleBase
    {
        private class Player
        {
            private int _startingPos;
            private int _dieIndex;
            private readonly int _dieLength;

            public Player(int startingPos, int dieIndex, int dieLength)
            {
                _startingPos = startingPos;
                _dieIndex = dieIndex;
                _dieLength = dieLength;
            }

            public int Rolled { get; private set; }

            public int Points { get; private set; }

            public int Roll()
            {
                var moveCount = 0;
                for (int i = 0; i < 3; i++)
                {
                    var currentMove = _dieIndex + i;
                    ClampTo(_dieLength, ref currentMove);
                    moveCount += currentMove;
                }

                var pointsToAdd = _startingPos + moveCount;
                ClampTo(10, ref pointsToAdd);

                Points += pointsToAdd;

                _startingPos = pointsToAdd;

                _dieIndex += 6;
                ClampTo(_dieLength, ref _dieIndex);

                Rolled += 3;

                return Points;
            }

            private static void ClampTo(int max, ref int value)
            {
                while (value > max)
                {
                    value -= max;
                }
            }
        }

        public Puzzle21()
            : base(2021, 21)
        {
        }

        private static Player[] InputAs(string[] input)
        {
            var p1 = new Player(int.Parse(input[0].AsSpan(input[0].LastIndexOf(' '))), 1, 100);
            var p2 = new Player(int.Parse(input[1].AsSpan(input[1].LastIndexOf(' '))), 4, 100);

            return new Player[]
            {
                p1,
                p2,
            };
        }

        private static long Calculate1(Player[] input)
        {
            var currentPlayer = 0;

            while (true)
            {
                var newScore = input[currentPlayer].Roll();

                if (newScore >= 1000)
                {
                    if (currentPlayer == 0)
                    {
                        return (input[0].Rolled + input[1].Rolled) * input[1].Points;
                    }
                    else
                    {
                        return (input[1].Rolled + input[0].Rolled) * input[0].Points;
                    }
                }

                currentPlayer = (currentPlayer + 1) % 2;
            }
        }

        public override long Sample1()
        {
            var input = InputAs(GetSampleString());

            return Calculate1(input);
        }

        public override long Solve1()
        {
            var input = InputAs(GetInputString());

            return Calculate1(input);
        }
    }
}
