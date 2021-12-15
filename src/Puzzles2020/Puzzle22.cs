using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.PuzzleCore;

namespace AdventOfCode2020.Puzzles
{
    public class Puzzle22 : PuzzleBase
    {
        private record Player
        {
            public List<int> Cards { get; init; } = new List<int>();
        }

        private record Game
        {
            public Player Player1 { get; init; }

            public Player Player2 { get; init; }
        }

        private static List<List<string>> SplitArray(string[] lines, string separator)
        {
            var newArray = new List<List<string>>();

            var newChunk = new List<string>();

            foreach (var line in lines)
            {
                if (line == separator)
                {
                    newArray.Add(newChunk);

                    newChunk = new List<string>();
                    continue;
                }

                newChunk.Add(line);
            }

            newArray.Add(newChunk);
            return newArray;
        }

        private static Game GetGame(string[] lines)
        {
            var playerLines = SplitArray(lines, string.Empty);

            var game = new Game
            {
                Player1 = new Player
                {
                    Cards = playerLines[0].Skip(1).Select(int.Parse).ToList()
                },
                Player2 = new Player
                {
                    Cards = playerLines[1].Skip(1).Select(int.Parse).ToList()
                }
            };

            return game;
        }

        private static void PlayGame(Game game)
        {
            var p1 = game.Player1;
            var p2 = game.Player2;

            while (p1.Cards.Count > 0 && p2.Cards.Count > 0)
            {
                var p1Card = p1.Cards[0];
                var p2Card = p2.Cards[0];

                p1.Cards.RemoveAt(0);
                p2.Cards.RemoveAt(0);

                if (p1Card > p2Card)
                {
                    p1.Cards.Add(p1Card);
                    p1.Cards.Add(p2Card);
                }
                else
                {
                    p2.Cards.Add(p2Card);
                    p2.Cards.Add(p1Card);
                }
            }
        }

        private static long GetScore(Game game)
        {
            if (game.Player1.Cards.Any())
            {
                return game.Player1.Cards.Select(x => x).Reverse().Select((value, index) => value * (index + 1)).Sum();
            }
            else
            {
                return game.Player2.Cards.Select(x => x).Reverse().Select((value, index) => value * (index + 1)).Sum();
            }
        }

        public override long Sample1()
        {
            var game = GetGame(Puzzle22Input.Sample);
            PlayGame(game);

            var score = GetScore(game);

            return score;
        }

        public override long Solve1()
        {
            var game = GetGame(Puzzle22Input.Input);
            PlayGame(game);

            var score = GetScore(game);

            return score;
        }

        public override long Solve2()
        {
            var input = Puzzle22Input.Input;

            return default;
        }
    }
}
