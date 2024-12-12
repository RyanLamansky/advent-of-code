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

    public Result Part2(Parsed parsed)
    {
        // Need to track perimeter differently than part 1.
        // A Dictionary<XY, char> where the values can be | - or + might work...
        return 0;
    }
}
