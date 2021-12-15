using System;
using AdventOfCode.Puzzles2020;

namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
            var solution = GetSolution();

            Console.WriteLine("Advent of code 2020");
            Console.WriteLine(solution);
        }

        private static (long Sample, long Solution1, long Solution2) GetSolution()
        {
            var puzzle = new Puzzle12();

            return (puzzle.Sample1(), puzzle.Solve1(), puzzle.Solve2());
        }
    }
}
