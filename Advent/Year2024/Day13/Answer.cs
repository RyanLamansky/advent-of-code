namespace Advent.Year2024.Day13;

using Parsed = ((int X, int Y) A, (int X, int Y) B, (int X, int Y) Prize)[];
using Result = int;
using XY = (int X, int Y);

public sealed class Answer : IPuzzle<Parsed, Result>
{
    public Parsed Parse(IEnumerable<string> input)
    {
        var result = new List<(XY A, XY B, XY Prize)>();

        using var lines = input.GetEnumerator();
        lines.MoveNext();

        do
        {
            XY a = (int.Parse(lines.Current.AsSpan(12, 2)), int.Parse(lines.Current.AsSpan(18, 2)));
            lines.MoveNext();
            XY b = (int.Parse(lines.Current.AsSpan(12, 2)), int.Parse(lines.Current.AsSpan(18, 2)));
            lines.MoveNext();

            var line = lines.Current;
            var eqIndex = line.IndexOf('=');
            var commaIndex = line.IndexOf(',', eqIndex);
            var x = int.Parse(line.AsSpan(eqIndex + 1, commaIndex - eqIndex - 1));

            eqIndex = line.IndexOf('=', commaIndex);
            var y = int.Parse(line.AsSpan(eqIndex + 1));

            result.Add((a, b, (x, y)));
        } while (lines.MoveNext() && lines.Current.Length == 0 && lines.MoveNext());

        return [.. result];
    }

    public Result Part1(Parsed parsed)
    {
        return 0;
    }

    public Result Part2(Parsed parsed)
    {
        return 0;
    }
}
