using System;
using AdventOfCode2020.Puzzles;

namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
            IPuzzle puzzle= new Puzzle4();
            var solution = puzzle.Solve();

            Console.WriteLine("Advent of code 2020");
            Console.WriteLine(solution);
        }
    }
}
