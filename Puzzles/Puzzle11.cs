using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Puzzles
{
    public class Puzzle11 : PuzzleBase, IPuzzle
    {
        private readonly static IReadOnlyList<(int x, int y)> _matrix = new List<(int x, int y)>
        {
            (-1, -1),
            ( 0, -1),
            ( 1, -1),
            (-1,  0),
            ( 1,  0),
            (-1,  1),
            ( 0,  1),
            ( 1,  1),
        }.AsReadOnly();

        private static char[][] GetPaddedSeats(string[] input)
        {
            var seats = input.Select(x => x.Select(y => y).ToList()).ToList();

            foreach (var seat in seats)
            {
                seat.Insert(0, '.');
                seat.Add('.');
            }

            seats.Insert(0, Enumerable.Repeat('.', seats[0].Count).ToList());
            seats.Add(Enumerable.Repeat('.', seats[0].Count).ToList());

            var paddedSeats = seats.Select(x => x.Select(y => y).ToArray()).ToArray();

            return paddedSeats;
        }

        private static bool CheckSeats(char[][] input1, char[][] input2)
        {
            if (input1.Length != input2.Length)
            {
                return false;
            }

            for (int x = 0; x < input1.Length; x++)
            {
                for (int y = 0; y < input1[x].Length; y++)
                {
                    if (input1[x][y] != input2[x][y])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private static void GetKingSurroundings(char[][] seats, int x, int y, ref int empty, ref int floor, ref int taken)
        {
            for (int i = x - 1; i <= x + 1; i++)
            {
                for (int j = y - 1; j <= y + 1; j++)
                {
                    if (i == x && j == y)
                    {
                        continue;
                    }

                    switch (seats[i][j])
                    {
                        case 'L':
                            empty++;
                            break;
                        case '.':
                            floor++;
                            break;
                        case '#':
                            taken++;
                            break;
                    }
                }
            }
        }

        private static void GetQueenSurroundings(char[][] seats, int x, int y, ref int empty, ref int floor, ref int taken)
        {
            var yLength = seats.Length - 1;
            var xLength = seats[0].Length - 1;

            foreach (var matrix in _matrix)
            {
                var currX = x;
                var currY = y;

                while (currX > 0 && currX < xLength && currY > 0 && currY < yLength)
                {
                    currX += matrix.x;
                    currY += matrix.y;

                    var currSeat = seats[currX][currY];

                    switch (currSeat)
                    {
                        case 'L':
                            empty++;
                            currX = -1;
                            currY = -1;
                            break;
                        case '.':
                            floor++;
                            break;
                        case '#':
                            taken++;
                            currX = -1;
                            currY = -1;
                            break;
                    }
                }
            }
        }

        private static (int empty, int floor, int taken) GetSurroundings(char[][] seats, int x, int y, bool onlyAdjaent)
        {
            var empty = 0;
            var floor = 0;
            var taken = 0;

            if (onlyAdjaent)
            {
                GetKingSurroundings(seats, x, y, ref empty, ref floor, ref taken);
            }
            else
            {
                GetQueenSurroundings(seats, x, y, ref empty, ref floor, ref taken);
            }

            return (empty, floor, taken);
        }

        private static char[][] FlipSeats(char[][] seats, int takenLimit, bool onlyAdjacent)
        {
            var newSeats = seats.Select(x => x.ToArray()).ToArray();

            var seatsToFlip = new List<(int x, int y)>();

            for (int x = 1; x < seats.Length - 1; x++)
            {
                for (int y = 1; y < seats[x].Length - 1; y++)
                {
                    var surroundings = GetSurroundings(seats, x, y, onlyAdjacent);

                    switch (seats[x][y])
                    {
                        case 'L':
                            if (surroundings.taken == 0)
                            {
                                seatsToFlip.Add((x, y));
                            }
                            break;
                        case '#':
                            if (surroundings.taken >= takenLimit)
                            {
                                seatsToFlip.Add((x, y));
                            }
                            break;
                    }
                }
            }

            foreach (var toFlip in seatsToFlip)
            {
                if (newSeats[toFlip.x][toFlip.y] == 'L')
                {
                    newSeats[toFlip.x][toFlip.y] = '#';
                }
                else
                {
                    newSeats[toFlip.x][toFlip.y] = 'L';
                }
            }

            return newSeats;
        }

        private static long GetChangeCount(char[][] seats, int takenLimit, bool onlyAdjacent)
        {
            var same = false;
            var prevSeats = seats;

            while (!same)
            {
                var flippedSeats = FlipSeats(prevSeats, takenLimit, onlyAdjacent);

                same = CheckSeats(prevSeats, flippedSeats);

                prevSeats = flippedSeats;
            }

            var count = 0L;

            count = prevSeats.Sum(x => x.Count(y => y == '#'));

            return count;
        }

        private static IEnumerable<(int Jolt, int Count)> GetBatchCount(int[] input)
        {
            var count = 0;
            var prev = 0;
            foreach (var item in input)
            {
                if (item == prev)
                {
                    count++;
                }
                else
                {
                    if (prev != 0)
                    {
                        yield return (prev, count);
                    }

                    prev = item;
                    count = 1;
                }
            }

            yield return (prev, count);
        }

        public override long Sample()
        {
            var seats = GetPaddedSeats(Puzzle11Input.Sample);
            var flipCount = GetChangeCount(seats, 4, true);

            return flipCount;
        }

        public override long Solve1()
        {
            var seats = GetPaddedSeats(Puzzle11Input.Input);
            var flipCount = GetChangeCount(seats, 4, true);

            return flipCount;
        }

        public override long Solve2()
        {
            var seats = GetPaddedSeats(Puzzle11Input.Input);
            var flipCount = GetChangeCount(seats, 5, false);

            return flipCount;
        }

        Solution IPuzzle.Solve()
        {
            return new Solution
            (
                Sample(),
                Solve1(),
                Solve2()
            );
        }
    }
}
