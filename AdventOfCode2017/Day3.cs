using System;
using System.Collections.Generic;

namespace AdventOfCode2017
{
    internal static partial class Program
    {
        private static void Day3()
        {
            const int input = 325489;
            Day3(input);
        }

        private enum Direction
        {
            East,
            North,
            West,
            South
        }

        private class Coordinate
        {
            public int X { get; }
            public int Y { get; }

            public Coordinate(int x, int y)
            {
                X = x;
                Y = y;
            }

            public Coordinate(Coordinate coordinate, Direction direction)
            {
                X = coordinate.X;
                Y = coordinate.Y;
                switch (direction)
                {
                    case Direction.East:
                        X += 1;
                        break;
                    case Direction.North:
                        Y += 1;
                        break;
                    case Direction.West:
                        X -= 1;
                        break;
                    case Direction.South:
                        Y -= 1;
                        break;
                    default:
                        throw new IndexOutOfRangeException("Direction is not a valid value.");
                }
            }

            public int GetRelevantXorY(Direction direction)
            {
                switch (direction)
                {
                    case Direction.East:
                    case Direction.West:
                        return X;
                    case Direction.North:
                    case Direction.South:
                        return Y;
                    default:
                        throw new IndexOutOfRangeException("Direction is not a valid value.");
                }
            }
        }

        private static void Day3(int input)
        {
            var currentLocation = new Coordinate(0, 0);
            var map = new Dictionary<int, Coordinate> { { 1, currentLocation } };
            var direction = Direction.East;
            var maxLevel = 0;

            // Make us our spiral.
            for (var i = 2; i <= input; i++)
            {
                // Move us and add this number to the map.
                currentLocation = new Coordinate(currentLocation, direction);
                map.Add(i, currentLocation);

                // Get the current level we are on.
                var currentLevel = Math.Abs(currentLocation.GetRelevantXorY(direction));

                // If we've hit the peak, change direction.
                if (currentLevel >= maxLevel)
                {
                    // Special case: East can move 1 past max. If we are currently 1 past max, increase max and change.
                    if (direction == Direction.East && currentLevel > maxLevel)
                    {
                        maxLevel++;
                        direction = Direction.North;
                        continue;
                    }

                    // If we reach this, we're at exactly the max and still moving East.
                    if (direction == Direction.East)
                        continue;

                    // Increment direction.
                    direction++;
                    if (!Enum.IsDefined(typeof(Direction), direction))
                        direction = Direction.East;
                }
            }

            var inputCoordinate = map[input];
            var distance = Math.Abs(inputCoordinate.X) + Math.Abs(inputCoordinate.Y);
            Console.WriteLine($"From {input} back to center takes {distance} moves.");
        }
    }
}
