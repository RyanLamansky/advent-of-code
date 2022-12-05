using System.Diagnostics.CodeAnalysis;

namespace Advent.Year2021.Day05;

public sealed class Answer : IPuzzle
{
    private readonly struct Point : IEquatable<Point>
    {
        public readonly int X, Y;

        public Point(string raw)
        {
            var split = raw.Split(',');
            X = int.Parse(split[0]);
            Y = int.Parse(split[1]);
        }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        // struct comes with a "good enough" Equals/GetHashCode, but an optimized implementation is much faster.
        public bool Equals(Point other) => X == other.X && Y == other.Y;
        public override bool Equals([NotNullWhen(true)] object? obj) => obj is Point p && Equals(p);
        public override int GetHashCode() => (X << 16) ^ Y;

        public override string ToString() => $"{X},{Y}";

        public IEnumerable<Point> WalkTo(Point other)
        {
            var Y = this.Y;
            var X = this.X;

            if (X == other.X)
                return Enumerable
                    .Range(Math.Min(Y, other.Y), Math.Abs(Y - other.Y) + 1)
                    .Select(y => new Point(X, y));
            if (Y == other.Y)
                return Enumerable
                    .Range(Math.Min(X, other.X), Math.Abs(X - other.X) + 1)
                    .Select(x => new Point(x, Y));

            static IEnumerable<Point> WalkDiagnonally(Point start, Point end)
            {
                var stepX = start.X - end.X < 0 ? 1 : -1;
                var stepY = start.Y - end.Y < 0 ? 1 : -1;
                var stepCount = Math.Abs(start.X - end.X) + 1;

                for (var i = 0; i < stepCount; i++)
                    yield return new Point(start.X + i * stepX, start.Y + i * stepY);
            }

            return WalkDiagnonally(this, other);
        }
    }

    private readonly struct Pair
    {
        public readonly Point Start, End;

        public Pair(string line)
        {
            var split = line.Split(" -> ");
            Start = new Point(split[0]);
            End = new Point(split[1]);
        }

        public override string ToString() => $"{Start} -> {End}";

        public bool NotAngled => Start.X == End.X || Start.Y == End.Y;
    }

    public int Part1(IEnumerable<string> input) => input
        .Select(line => new Pair(line))
        .Where(pair => pair.NotAngled)
        .SelectMany(pair => pair.Start.WalkTo(pair.End))
        .GroupBy(point => point)
        .Select(group => (group.Key, Count: group.Count()))
        .Where(kv => kv.Count >= 2)
        .Count();

    public int Part2(IEnumerable<string> input) => input
        .Select(line => new Pair(line))
        .SelectMany(pair => pair.Start.WalkTo(pair.End))
        .GroupBy(point => point)
        .Select(group => (group.Key, Count: group.Count()))
        .Where(kv => kv.Count >= 2)
        .Count();
}
