namespace Advent.Year2024.Day14;

using Parsed = (((int X, int Y) Position, (int X, int Y) Velocity)[] Robots, (int Width, int Height) Area);
using Result = int;
using XY = (int X, int Y);

public sealed class Answer : IPuzzle<Parsed, Result>
{
    public Parsed Parse(IEnumerable<string> input)
    {
        var result = new List<(XY Position, XY Velocity)>();

        foreach (var line in input)
        {
            var eq = 2;
            var space = line.IndexOf(' ');
            var comma = line.IndexOf(',', eq);

            var position = (int.Parse(line.AsSpan(eq, comma - eq)), int.Parse(line.AsSpan(comma + 1, space - comma - 1)));

            eq = space + 3;
            comma = line.IndexOf(',', eq);

            var velocity = (int.Parse(line.AsSpan(eq, comma - eq)), int.Parse(line.AsSpan(comma + 1)));

            result.Add((position, velocity));
        }

        return ([.. result], result.Count == 12 ? (11, 7) : (101, 103));
    }

    public Result Part1(Parsed parsed)
    {
        var (robots, (width, height)) = parsed;
        var locations = new XY[robots.Length];
        const int seconds = 100;

        for (var r = 0; r < robots.Length; r++)
        {
            var ((px, py), (vx, vy)) = robots[r];
            var (nx, ny) = ((px + vx * seconds) % width, (py + vy * seconds) % height);
            if (nx < 0)
                nx = width + nx;
            if (ny < 0)
                ny = height + ny;

            locations[r] = (nx, ny);
        }

        int tl = 0, tr = 0, ll = 0, lr = 0, xDivider = width / 2, yDivider = height / 2;

        foreach (var (x, y) in locations)
        {
            if (x < xDivider)
            {
                if (y < yDivider)
                    tl += 1;
                else if (y > yDivider)
                    ll += 1;
            }
            else if (x > xDivider)
            {
                if (y < yDivider)
                    tr += 1;
                else if (y > yDivider)
                    lr += 1;
            }
        }

        return tl * tr * ll * lr;
    }

    public Result Part2(Parsed parsed)
    {
        var (robots, (width, height)) = parsed;

        if (width < 50)
            return 0; // Sample doesn't draw a tree, at least with the detection below.

        // I don't know if this is the intended way to find a tree, but it works with my data.
        var uniqueLocations = new HashSet<XY>();

        for (var seconds = 1; true; seconds++)
        {
            for (var r = 0; r < robots.Length; r++)
            {
                var ((px, py), (vx, vy)) = robots[r];
                var (nx, ny) = ((px + vx * seconds) % width, (py + vy * seconds) % height);
                if (nx < 0)
                    nx = width + nx;
                if (ny < 0)
                    ny = height + ny;

                if (!uniqueLocations.Add((nx, ny)))
                    break;
            }

            if (uniqueLocations.Count == robots.Length)
                return seconds;

            uniqueLocations.Clear();
        }
    }
}
