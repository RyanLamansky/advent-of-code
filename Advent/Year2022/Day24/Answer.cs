using System.Diagnostics.CodeAnalysis;

namespace Advent.Year2022.Day24;

public sealed class Answer : IPuzzle<int>
{
    enum Direction : byte
    {
        None = 0,
        NotAFlake = (byte)'.',
        Up = (byte)'^',
        Right = (byte)'>',
        Down = (byte)'v',
        Left = (byte)'<',
    }

    readonly struct Flake : IEquatable<Flake>
    {
        public readonly byte X, Y;
        public readonly Direction Direction;

        public Flake(byte x, byte y, Direction direction)
        {
            X = x;
            Y = y;
            Direction = direction;
        }

        public override string ToString() => $"{X},{Y} {(char)Direction}";

        public static (Flake[] flakes, int width, int height) ReadInput(IEnumerable<string> input)
        {
            var flakes = new List<Flake>();
            var width = 0;
            var height = 0;
            byte y = 0;
            foreach (var line in input)
            {
                if (line[3] == '#')
                {
                    width = Math.Max(width, line.Length - 2);
                    height = Math.Max(height, y);
                    continue;
                }

                byte x = 0;
                foreach (char c in line[1..^1])
                {
                    var direction = (Direction)c;
                    if (direction != Direction.NotAFlake)
                        flakes.Add(new Flake(x, y, direction));
                    x++;
                }

                y++;
            }

            return (flakes.ToArray(), width, height);
        }

        public override bool Equals([NotNullWhen(true)] object? obj) => obj is Flake flake && Equals(flake);

        public bool Equals(Flake other) => X == other.X && Y == other.Y && Direction == other.Direction;

        public override int GetHashCode() => (X << 16) | (Y << 8) | (byte)Direction;
    }

    sealed class BoardComparer : IEqualityComparer<Flake[]>
    {
        public bool Equals(Flake[]? x, Flake[]? y)
        {
            for (var i = 0; i < x!.Length; i++)
                if (!x[i].Equals(y![i]))
                    return false;

            return true;
        }

        public int GetHashCode([DisallowNull] Flake[] obj)
        {
            var code = new HashCode();
            foreach (var x in obj)
                code.Add(x);

            return code.ToHashCode();
        }
    }

    static bool[][,] CoverageByMinute(IEnumerable<string> input, out byte maxX, out byte maxY)
    {
        var (flakes, width, height) = Flake.ReadInput(input);

        var uniqueCombinations = new HashSet<Flake[]>(new BoardComparer());
        var combinationAtMinuteList = new List<Flake[]>();
        maxX = (byte)(width - 1);
        maxY = (byte)(height - 1);

        var movedFlakes = flakes.ToArray();
        while (uniqueCombinations.Add(movedFlakes))
        {
            combinationAtMinuteList.Add(movedFlakes);

            for (var i = 0; i < flakes.Length; i++)
            {
                var flake = flakes[i];

                flakes[i] = flake.Direction switch
                {
                    Direction.Up => new Flake(flake.X, flake.Y == 0 ? maxY : (byte)(flake.Y - 1), flake.Direction),
                    Direction.Right => new Flake((byte)(flake.X == maxX ? 0 : flake.X + 1), flake.Y, flake.Direction),
                    Direction.Down => new Flake(flake.X, (byte)(flake.Y == maxY ? 0 : flake.Y + 1), flake.Direction),
                    Direction.Left => new Flake((byte)(flake.X == 0 ? maxX : flake.X - 1), flake.Y, flake.Direction),
                    _ => throw new Exception(),
                };
            }

            movedFlakes = flakes.ToArray();
        };

        return combinationAtMinuteList
            .Select(flakes =>
            {
                var grid = new bool[width, height];

                foreach (var flake in flakes)
                    grid[flake.X, flake.Y] = true;

                return grid;
            })
            .ToArray();
    }

    static int MinutesNeeded((byte X, byte Y) start, (byte X, byte Y) end, bool[][,] combinations, byte maxX, byte maxY, int initialMinutes = 0)
    {
        var currentQueue = new HashSet<(byte X, byte Y)>();
        var nextQueue = new HashSet<(byte X, byte Y)>();
        var minutes = initialMinutes;

        while (true)
        {
            minutes++;
            var nextCoverage = combinations[minutes % combinations.Length];

            if ((minutes - initialMinutes) < combinations.Length && !nextCoverage[start.X, start.Y])
                nextQueue.Add(start);

            void TryEnqueue(int x, int y)
            {
                if (x >= 0 && x <= maxX && y >= 0 && y <= maxY && !nextCoverage[x, y])
                    nextQueue.Add(((byte)x, (byte)y));
            }

            foreach (var (x, y) in currentQueue)
            {
                if (x == end.X && y == end.Y)
                    return minutes;

                TryEnqueue(x, y);
                TryEnqueue(x + 1, y);
                TryEnqueue(x, y + 1);
                TryEnqueue(x - 1, y);
                TryEnqueue(x, y - 1);
            }

            (nextQueue, currentQueue) = (currentQueue, nextQueue);
            nextQueue.Clear();
        }
    }

    public int Part1(IEnumerable<string> input)
    {
        var combinations = CoverageByMinute(input, out var maxX, out var maxY);

        return MinutesNeeded((0, 0), (maxX, maxY), combinations, maxX, maxY);
    }

    public int Part2(IEnumerable<string> input)
    {
        var combinations = CoverageByMinute(input, out var maxX, out var maxY);

        var initial = MinutesNeeded((0, 0), (maxX, maxY), combinations, maxX, maxY);
        var returnForSnacks = MinutesNeeded((maxX, maxY), (0, 0), combinations, maxX, maxY, initial);
        var returnWithSnacks = MinutesNeeded((0, 0), (maxX, maxY), combinations, maxX, maxY, returnForSnacks);

        return returnWithSnacks;
    }
}
