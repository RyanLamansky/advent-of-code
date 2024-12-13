namespace Advent.Year2024.Day12;

using Parsed = Grid<char>;
using Result = int;
using XY = (int X, int Y);

public sealed class Answer : IPuzzle<Parsed, Result>
{
    public Parsed Parse(IEnumerable<string> input)
        => new(input, c => c);

    public Result Part1(Parsed parsed)
    {
        var plots = parsed.Select(xyv => (xyv.X, xyv.Y)).ToHashSet();

        var total = 0;

        while (plots.Count != 0)
        {
            var area = 1;
            var perimeter = 0;

            var plot = plots.First();
            plots.Remove(plot);

            var (x, y) = plot;

            var plant = parsed[plot];
            var checkNext = new Queue<XY>();
            checkNext.Enqueue((x + 1, y));
            checkNext.Enqueue((x, y + 1));
            checkNext.Enqueue((x - 1, y));
            checkNext.Enqueue((x, y - 1));

            while (checkNext.TryDequeue(out var next))
            {
                var neighboringPlant = parsed.GetOrDefault(next, ' ');
                if (neighboringPlant != plant)
                {
                    perimeter++;
                    continue;
                }

                if (!plots.Remove(next))
                    continue;

                area++;

                (x, y) = next;
                checkNext.Enqueue((x + 1, y));
                checkNext.Enqueue((x, y + 1));
                checkNext.Enqueue((x - 1, y));
                checkNext.Enqueue((x, y - 1));
            }

            total += area * perimeter;
        }

        return total;
    }

    [Flags]
    enum Side
    {
        None = 0,
        Top = 1,
        Right = 2,
        Bottom = 4,
        Left = 8,
    }

    public Result Part2(Parsed parsed)
    {
        var plots = parsed.Select(xyv => (xyv.X, xyv.Y)).ToHashSet();
        ReadOnlySpan<int> directions = [1, -1];
        var total = 0;

        while (plots.Count != 0)
        {
            var area = 1;
            var perimeter = new Dictionary<XY, Side>();

            var plot = plots.First();
            plots.Remove(plot);

            var (x, y) = plot;

            var plant = parsed[plot];
            var checkNext = new Queue<(XY, Side)>();
            checkNext.Enqueue(((x + 1, y), Side.Left));
            checkNext.Enqueue(((x, y + 1), Side.Top));
            checkNext.Enqueue(((x - 1, y), Side.Right));
            checkNext.Enqueue(((x, y - 1), Side.Bottom));

            while (checkNext.TryDequeue(out var nextCandidate))
            {
                var (next, fence) = nextCandidate;

                var neighboringPlant = parsed.GetOrDefault(next, ' ');
                if (neighboringPlant != plant)
                {
                    perimeter[next] = perimeter.GetValueOrDefault(next) | fence;
                    continue;
                }

                if (!plots.Remove(next))
                    continue;

                area++;

                (x, y) = next;
                checkNext.Enqueue(((x + 1, y), Side.Left));
                checkNext.Enqueue(((x, y + 1), Side.Top));
                checkNext.Enqueue(((x - 1, y), Side.Right));
                checkNext.Enqueue(((x, y - 1), Side.Bottom));
            }

            var fences = 0;

            while (perimeter.Count != 0)
            {
                var (fenceOrigin, fenceSide) = perimeter.First();

                if ((fenceSide & Side.Bottom) != 0)
                {
                    fenceSide &= ~Side.Bottom;
                    fences++;

                    (x, y) = fenceOrigin;

                    foreach (var delta in directions)
                    {
                        for (var offset = delta; perimeter.TryGetValue((x + offset, y), out var segment) && (segment & Side.Bottom) != 0; offset += delta)
                        {
                            segment &= ~Side.Bottom;
                            if (segment == Side.None)
                                perimeter.Remove((x + offset, y));
                            else
                                perimeter[(x + offset, y)] = segment;
                        }
                    }
                }

                if ((fenceSide & Side.Top) != 0)
                {
                    fenceSide &= ~Side.Top;
                    fences++;

                    (x, y) = fenceOrigin;

                    foreach (var delta in directions)
                    {
                        for (var offset = delta; perimeter.TryGetValue((x + offset, y), out var segment) && (segment & Side.Top) != 0; offset += delta)
                        {
                            segment &= ~Side.Top;
                            if (segment == Side.None)
                                perimeter.Remove((x + offset, y));
                            else
                                perimeter[(x + offset, y)] = segment;
                        }
                    }
                }

                if ((fenceSide & Side.Left) != 0)
                {
                    fenceSide &= ~Side.Left;
                    fences++;

                    (x, y) = fenceOrigin;

                    foreach (var delta in directions)
                    {
                        for (var offset = delta; perimeter.TryGetValue((x, y + offset), out var segment) && (segment & Side.Left) != 0; offset += delta)
                        {
                            segment &= ~Side.Left;
                            if (segment == Side.None)
                                perimeter.Remove((x, y + offset));
                            else
                                perimeter[(x, y + offset)] = segment;
                        }
                    }
                }

                if ((fenceSide & Side.Right) != 0)
                {
                    fenceSide &= ~Side.Right;
                    fences++;

                    (x, y) = fenceOrigin;

                    foreach (var delta in directions)
                    {
                        for (var offset = delta; perimeter.TryGetValue((x, y + offset), out var segment) && (segment & Side.Right) != 0; offset += delta)
                        {
                            segment &= ~Side.Right;
                            if (segment == Side.None)
                                perimeter.Remove((x, y + offset));
                            else
                                perimeter[(x, y + offset)] = segment;
                        }
                    }
                }

                perimeter.Remove(fenceOrigin);
            }

            total += area * fences;
        }

        return total;
    }
}
