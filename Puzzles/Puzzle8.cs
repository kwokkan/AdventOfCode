﻿using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Puzzles
{
    public class Puzzle8 : IPuzzle
    {
        private record Operation
        {
            public string Op { get; set; }
            public int Value { get; init; }

            public Operation(string op, int value) => (Op, Value) = (op, value);
        }

        private static IEnumerable<Operation> GetOperations(string[] lines)
        {
            foreach (var line in lines)
            {
                var parts = line.Split(' ');
                yield return new Operation(parts[0], int.Parse(parts[1]));
            }
        }

        private static string Solve1()
        {
            var operations = GetOperations(Puzzle8Input.Input).ToList();

            var usedIndexes = new HashSet<int>();
            var sum = 0;

            for (int i = 0; i < operations.Count; i++)
            {
                if (usedIndexes.Contains(i))
                {
                    break;
                }

                usedIndexes.Add(i);

                var currentOp = operations[i];

                switch (currentOp.Op)
                {
                    case "acc":
                        sum += currentOp.Value;
                        break;
                    case "jmp":
                        i += (currentOp.Value - 1);
                        break;
                    case "nop":
                        break;
                    default:
                        break;
                }
            }

            return sum.ToString();
        }

        private static string Solve2()
        {
            var operationCount = GetOperations(Puzzle8Input.Input).Count();

            var lastIndex = operationCount - 1;
            var sum = 0;

            for (int replacerIndex = 0; replacerIndex < operationCount; replacerIndex++)
            {
                if (sum != 0)
                {
                    break;
                }

                var usedIndexes = new HashSet<int>();
                var currentSum = 0;

                var replacedOperations = GetOperations(Puzzle8Input.Input).ToList();
                if (replacedOperations[replacerIndex].Op == "nop")
                {
                    replacedOperations[replacerIndex].Op = "jmp";
                }
                else if (replacedOperations[replacerIndex].Op == "jmp")
                {
                    replacedOperations[replacerIndex].Op = "nop";
                }
                else
                {
                    continue;
                }

                for (int i = 0; i < replacedOperations.Count; i++)
                {
                    if (usedIndexes.Contains(i))
                    {
                        break;
                    }

                    if (i == lastIndex)
                    {
                        sum = currentSum;
                        break;
                    }

                    usedIndexes.Add(i);

                    var currentOp = replacedOperations[i];

                    switch (currentOp.Op)
                    {
                        case "acc":
                            currentSum += currentOp.Value;
                            break;
                        case "jmp":
                            i += (currentOp.Value - 1);
                            break;
                        case "nop":
                            break;
                        default:
                            break;
                    }
                }
            }

            return sum.ToString();
        }

        Solution IPuzzle.Solve()
        {
            return new Solution
            (
                Solve1(),
                Solve2()
            );
        }
    }
}