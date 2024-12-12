namespace Advent.Year2024.Day10;

using System.Collections.Frozen;
using Parsed = System.Collections.Frozen.FrozenSet<(int X, int Y)>[];
using Result = int;

public sealed class Answer : IPuzzle<Parsed, Result>
{
    public Parsed Parse(IEnumerable<string> input)
        => [.. new Grid<int>(input, c => c - '0')
            .GroupBy(xyv => xyv.Value, xyv => (xyv.X, xyv.Y))
            .OrderBy(group => group.Key)
            .Select(group => group.ToFrozenSet())];

    public Result Part1(Parsed parsed)
    {
        var trails = parsed[0].Select(xy => new HashSet<(int x, int y)> { xy }).ToArray();

        for (var step = 1; step <= 9; step++)
        {
            var next = parsed[step];

            foreach (var trail in trails)
            {
                foreach (var location in trail.ToArray())
                {
                    trail.Remove(location);

                    var (x, y) = location;

                    if (next.Contains((x + 1, y)))
                        trail.Add((x + 1, y));

                    if (next.Contains((x, y + 1)))
                        trail.Add((x, y + 1));
                    
                    if (next.Contains((x - 1, y)))
                        trail.Add((x - 1, y));

                    if (next.Contains((x, y - 1)))
                        trail.Add((x, y - 1));
                }
            }
        }

        return trails.Sum(trail => trail.Count);
    }

    public Result Part2(Parsed parsed)
    {
        static int Trace(int step, int x, int y, Parsed parsed)
        {
            var next = parsed[step];
            var total = 0;

            if (next.Contains((x + 1, y)))
                total += step == 9 ? 1 : Trace(step + 1, x + 1, y, parsed);
            if (next.Contains((x, y + 1)))
                total += step == 9 ? 1 : Trace(step + 1, x, y + 1, parsed);
            if (next.Contains((x - 1, y)))
                total += step == 9 ? 1 : Trace(step + 1, x - 1, y, parsed       );
            if (next.Contains((x, y - 1)))
                total += step == 9 ? 1 : Trace(step + 1, x, y - 1, parsed);

            return total;
        }

        return parsed[0].Sum(xy => Trace(1, xy.X, xy.Y, parsed));
    }
}
