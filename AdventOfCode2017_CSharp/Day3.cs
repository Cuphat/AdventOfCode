using System;
using System.Collections.Generic;

namespace AdventOfCode2017_CSharp
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

            public IEnumerable<Coordinate> GetSurroundingCoordinates()
            {
                var xCoords = new[] {X - 1, X, X + 1};
                var yCoords = new[] {Y - 1, Y, Y + 1};
                foreach (var x in xCoords)
                {
                    foreach (var y in yCoords)
                    {
                        if (x == X && y == Y)
                            continue;
                        yield return new Coordinate(x, y);
                    }
                }
            }

            public override bool Equals(object obj)
            {
                return obj is Coordinate coordinate && this == coordinate;
            }
            public override int GetHashCode()
            {
                return X.GetHashCode() ^ Y.GetHashCode();
            }
            public static bool operator ==(Coordinate a, Coordinate b)
            {
                return a?.X == b?.X && a?.Y == b?.Y;
            }
            public static bool operator !=(Coordinate a, Coordinate b)
            {
                return !(a == b);
            }
        }

        private static (Direction Direction, int MaxLevel)
            UpdateDirectionAndLevel(Coordinate currentLocation, Direction direction, int maxLevel)
        {
            // Get the current level we are on.
            var currentLevel = Math.Abs(currentLocation.GetRelevantXorY(direction));

            // If we haven't hit the peak, just return now.
            if (currentLevel < maxLevel) return (direction, maxLevel);

            // Special case: East can move 1 past max. If we are currently 1 past max, increase max and change.
            // Otherwise, if we're at max, don't change directions and just return.
            switch (direction)
            {
                case Direction.East when currentLevel > maxLevel:
                    maxLevel++;
                    break;
                case Direction.East:
                    return (direction, maxLevel);
            }

            // Increment direction.
            direction++;
            if (!Enum.IsDefined(typeof(Direction), direction))
                direction = Direction.East;

            return (direction, maxLevel);
        }

        private static void Day3(int input)
        {
            // Part 1
            {
                var currentLocation = new Coordinate(0, 0);
                var map = new Dictionary<int, Coordinate> {{1, currentLocation}};
                var direction = Direction.East;
                var maxLevel = 0;

                // Make us our spiral.
                for (var i = 2; i <= input; i++)
                {
                    // Move us and add this number to the map.
                    currentLocation = new Coordinate(currentLocation, direction);
                    map.Add(i, currentLocation);

                    // Update our direction and level.
                    (direction, maxLevel) = UpdateDirectionAndLevel(currentLocation, direction, maxLevel);
                }

                var inputCoordinate = map[input];
                var distance = Math.Abs(inputCoordinate.X) + Math.Abs(inputCoordinate.Y);
                Console.WriteLine($"From {input} back to center takes {distance} moves.");
            }

            // Part 2
            {
                var currentLocation = new Coordinate(0, 0);
                var map = new Dictionary<Coordinate, int> { { currentLocation, 1 } };
                var direction = Direction.East;
                var maxLevel = 0;
                var lastNumber = 1;

                // Make us our spiral.
                while (lastNumber < input)
                {
                    // Move us to the next location.
                    currentLocation = new Coordinate(currentLocation, direction);

                    // Calculate the number that should be here.
                    var sum = 0;
                    foreach (var coordinate in currentLocation.GetSurroundingCoordinates())
                    {
                        if (!map.ContainsKey(coordinate))
                            continue;
                        sum += map[coordinate];
                    }
                    lastNumber = sum;

                    // Add this number to the map.
                    map.Add(currentLocation, sum);

                    // Update our direction and level.
                    (direction, maxLevel) = UpdateDirectionAndLevel(currentLocation, direction, maxLevel);
                }

                Console.WriteLine($"The first number higher than {input} in the spiral is {lastNumber}.");
            }
        }
    }
}
