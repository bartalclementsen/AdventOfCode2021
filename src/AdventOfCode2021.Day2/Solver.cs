using System;
using System.Collections.Generic;

namespace AdventOfCode2021.Day2
{
    internal class Solver
    {

        public string SolveDayStar1(string input)
        {
            List<Command> commands = input.SplitAndConvertByNewLine((s) => Command.FromString(s));

            int horizontalDistance = 0;
            int verticalDistance = 0;

            foreach (var command in commands)
            {
                switch (command.Direction)
                {
                    case Direction.forward:
                        horizontalDistance += command.Units;
                        break;
                    case Direction.down:
                        verticalDistance += command.Units;
                        break;
                    case Direction.up:
                        verticalDistance -= command.Units;
                        break;
                }
            }

            return (horizontalDistance * verticalDistance).ToString();
        }

        public string SolveDayStar2(string input)
        {
            List<Command> commands = input.SplitAndConvertByNewLine((s) => Command.FromString(s));

            int horizontalDistance = 0;
            int verticalDistance = 0;
            int aim = 0;

            foreach (var command in commands)
            {
                switch (command.Direction)
                {
                    case Direction.forward:
                        horizontalDistance += command.Units;
                        verticalDistance += aim * command.Units;
                        break;
                    case Direction.down:
                        aim += command.Units;
                        break;
                    case Direction.up:
                        aim -= command.Units;
                        break;
                }
            }

            return (horizontalDistance * verticalDistance).ToString();
        }

        /* ----------------------------------------------------------------------------  */
        /*                               INTERNAL CLASSES                                */
        /* ----------------------------------------------------------------------------  */
        private enum Direction
        {
            forward,
            down,
            up
        }

        private class Command
        {
            public Direction Direction { get; }

            public int Units { get; }

            public Command(Direction direction, int units)
            {
                Direction = direction;
                Units = units;
            }

            public static Command FromString(string input)
            {
                var inputs = input.Split(" ");

                return new Command(
                    Enum.Parse<Direction>(inputs[0]),
                    int.Parse(inputs[1])
                );
            }
        }
    }
}
