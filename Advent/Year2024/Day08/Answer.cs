namespace Advent.Year2024.Day08;

using System.Collections.Frozen;
using Parsed = (Grid<char> grid, System.Collections.Frozen.FrozenDictionary<char, (int X, int Y)[]> locationsByAntenna);
using Result = int;
using XY = (int X, int Y);

public sealed class Answer : IPuzzle<Parsed, Result>
{
    public Parsed Parse(IEnumerable<string> input)
    {
        var grid = new Grid<char>(input, c => c);

        var locationsByAntenna = grid
            .Where(xyv => xyv.Value != '.')
            .GroupBy(xyv => xyv.Value)
            .ToFrozenDictionary(group => group.Key, group => group.Select(xyv => (xyv.X, xyv.Y)).ToArray());

        return (grid, locationsByAntenna);
    }

    static XY Add(XY a, XY b) => (a.X + b.X, a.Y + b.Y);

    static XY Subtract(XY a, XY b) => (a.X - b.X, a.Y - b.Y);

    static XY Multiply(XY value, int multiplier) => (value.X * multiplier, value.Y * multiplier);

    public Result Part1(Parsed parsed)
    {
        var (grid, locationsByAntenna) = parsed;
        var antinodes = new HashSet<XY>();

        foreach (var kv in locationsByAntenna)
        {
            foreach (var first in kv.Value)
            {
                foreach (var second in locationsByAntenna[kv.Key])
                {
                    if (first == second)
                        continue;

                    var difference = Subtract(first, second);
                    var candidate = Add(first, difference);
                    if (grid.IsValidLocation(candidate))
                        antinodes.Add(candidate);

                    candidate = Subtract(second, difference);
                    if (grid.IsValidLocation(candidate))
                        antinodes.Add(candidate);
                }
            }
        }

        return antinodes.Count;
    }

    public Result Part2(Parsed parsed)
    {
        var (grid, locationsByAntenna) = parsed;
        var antinodes = new HashSet<XY>();

        foreach (var kv in locationsByAntenna)
        {
            foreach (var first in kv.Value)
            {
                antinodes.Add(first);

                foreach (var second in locationsByAntenna[kv.Key])
                {
                    if (first == second)
                        continue;

                    var difference = Subtract(first, second);

                    XY candidate, sliding = first;
                    while (grid.IsValidLocation(candidate = Add(sliding, difference)))
                    {
                        antinodes.Add(candidate);
                        sliding = Add(sliding, difference);
                    }

                    sliding = second;
                    while (grid.IsValidLocation(candidate = Subtract(sliding, difference)))
                    {
                        antinodes.Add(candidate);
                        sliding = Add(sliding, Multiply(difference, -1));
                    }
                }
            }
        }

        return antinodes.Count;
    }
}
