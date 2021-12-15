using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.PuzzleCore;

namespace AdventOfCode.Puzzles2020
{
    public class Puzzle12 : PuzzleBase
    {
        private record Point
        {
            public int X { get; set; }
            public int Y { get; set; }
        }

        private record Instruction
        {
            public char Action { get; init; }
            public int Value { get; init; }
        }

        private static IEnumerable<Instruction> GetInstructions(string[] lines)
        {
            foreach (var line in lines)
            {
                var action = line[0];
                var value = int.Parse(line[1..]);

                yield return new Instruction
                {
                    Action = action,
                    Value = value,
                };
            }
        }

        private static void MoveDirection(char action, int value, Point point)
        {
            switch (action)
            {
                case 'N':
                    point.Y += value;
                    break;
                case 'E':
                    point.X += value;
                    break;
                case 'S':
                    point.Y -= value;
                    break;
                case 'W':
                    point.X -= value;
                    break;
            }
        }

        private static (int x, int y) RunInstructions(Instruction[] instructions)
        {
            var directions = new char[]
            {
                'N',
                'E',
                'S',
                'W',
            };

            var point = new Point();

            var currDirection = 'E';
            var currDirectionIndex = (instructions.Length * directions.Length) + 1;

            foreach (var instruction in instructions)
            {
                var newDirection = instruction.Action;
                var value = instruction.Value;

                switch (instruction.Action)
                {
                    case 'F':
                        newDirection = currDirection;

                        goto default;
                    case 'L':
                        {
                            var div = value / 90;
                            currDirectionIndex -= div;

                            var mod = currDirectionIndex % directions.Length;
                            currDirection = directions[mod];
                            newDirection = currDirection;
                        }

                        break;
                    case 'R':
                        {
                            var div = value / 90;
                            currDirectionIndex += div;

                            var mod = currDirectionIndex % directions.Length;
                            currDirection = directions[mod];
                            newDirection = currDirection;
                        }

                        break;
                    default:
                        MoveDirection(newDirection, value, point);
                        break;
                }
            }

            return (point.X, point.Y);
        }

        private static (int x, int y) RunInstructionsWithWaypoint(Instruction[] instructions)
        {
            var ship = new Point();
            var waypoint = new Point
            {
                X = 10,
                Y = 1,
            };

            foreach (var instruction in instructions)
            {
                var newDirection = instruction.Action;
                var value = instruction.Value;

                switch (instruction.Action)
                {
                    case 'F':
                        ship.X += waypoint.X * value;
                        ship.Y += waypoint.Y * value;

                        break;
                    case 'L':
                        {
                            var div = value / 90;

                            var oldX = waypoint.X;
                            var oldY = waypoint.Y;

                            switch (div)
                            {
                                case 1:
                                    waypoint.X = oldY * -1;
                                    waypoint.Y = oldX;
                                    break;
                                case 2:
                                    waypoint.X *= -1;
                                    waypoint.Y *= -1;
                                    break;
                                case 3:
                                    waypoint.X = oldY;
                                    waypoint.Y = oldX * -1;
                                    break;
                            }
                        }

                        break;
                    case 'R':
                        {
                            var div = value / 90;

                            var oldX = waypoint.X;
                            var oldY = waypoint.Y;

                            switch (div)
                            {
                                case 1:
                                    waypoint.X = oldY;
                                    waypoint.Y = oldX * -1;
                                    break;
                                case 2:
                                    waypoint.X *= -1;
                                    waypoint.Y *= -1;
                                    break;
                                case 3:
                                    waypoint.X = oldY * -1;
                                    waypoint.Y = oldX;
                                    break;
                            }
                        }

                        break;
                    default:
                        MoveDirection(newDirection, value, waypoint);
                        break;
                }
            }

            return (ship.X, ship.Y);
        }

        public override long Sample1()
        {
            var instructions = GetInstructions(Puzzle12Input.Sample).ToArray();
            var destination = RunInstructions(instructions);

            return Math.Abs(destination.x) + Math.Abs(destination.y);
        }

        public override long Solve1()
        {
            var instructions = GetInstructions(Puzzle12Input.Input).ToArray();
            var destination = RunInstructions(instructions);

            return Math.Abs(destination.x) + Math.Abs(destination.y);
        }

        public override long Solve2()
        {
            var instructions = GetInstructions(Puzzle12Input.Input).ToArray();
            var destination = RunInstructionsWithWaypoint(instructions);

            return Math.Abs(destination.x) + Math.Abs(destination.y);
        }
    }
}
