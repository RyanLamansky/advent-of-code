namespace Advent.Year2023.Day11;

public sealed class Answer : IPuzzle<long>
{
    static (int X, int Y)[] Parse(IEnumerable<string> input) => input
        .SelectMany((line, y) => line.Select((c, x) => (X: x, Y: y, C: c)))
        .Where(xyc => xyc.C == '#')
        .Select(xyc => (xyc.X, xyc.Y))
        .ToArray();

    public long Part1(IEnumerable<string> input)
    {
        var parsed = Parse(input);

        var xs = parsed.Select(xy => xy.X).Distinct().OrderBy(x => x).ToArray();
        var ys = parsed.Select(xy => xy.Y).Distinct().OrderBy(y => y).ToArray();
        var xMin = xs.Min();
        var xMax = xs.Max();
        var xGaps = Enumerable.Range(xMin, xMax - xMin).Except(xs).ToArray();
        var yMin = ys.Min();
        var yMax = ys.Max();
        var yGaps = Enumerable.Range(yMin, yMax - yMin).Except(ys).ToArray();

        var combinations = Enumerable
            .Range(0, parsed.Length)
            .SelectMany(i =>
                Enumerable
                    .Range(i + 1, parsed.Length - i - 1)
                    .Select(j => (parsed[i], parsed[j])
                )
            );

        var totalLength = 0;

        foreach (var ((startX, startY), (endX, endY)) in combinations)
        {
            var xDistance = Math.Abs(endX - startX);
            var yDistance = Math.Abs(endY - startY);
            var xGap = xGaps.Count(x => x > Math.Min(startX, endX) && x < Math.Max(startX, endX));
            var yGap = yGaps.Count(y => y > Math.Min(startY, endY) && y < Math.Max(startY, endY));

            var length = xDistance + xGap + yDistance + yGap;
            totalLength += length;
        }

        return totalLength;
    }

    public long Part2(IEnumerable<string> input)
    {
        var parsed = Parse(input);

        var xs = parsed.Select(xy => xy.X).Distinct().OrderBy(x => x).ToArray();
        var ys = parsed.Select(xy => xy.Y).Distinct().OrderBy(y => y).ToArray();
        var xMin = xs.Min();
        var xMax = xs.Max();
        var xGaps = Enumerable.Range(xMin, xMax - xMin).Except(xs).ToArray();
        var yMin = ys.Min();
        var yMax = ys.Max();
        var yGaps = Enumerable.Range(yMin, yMax - yMin).Except(ys).ToArray();

        var combinations = Enumerable
            .Range(0, parsed.Length)
            .SelectMany(i =>
                Enumerable
                    .Range(i + 1, parsed.Length - i - 1)
                    .Select(j => (parsed[i], parsed[j])
                )
            );

        var totalLength = 0L;

        foreach (var ((startX, startY), (endX, endY)) in combinations)
        {
            var xDistance = Math.Abs(endX - startX);
            var yDistance = Math.Abs(endY - startY);
            var xGap = xGaps.Count(x => x > Math.Min(startX, endX) && x < Math.Max(startX, endX)) * (1_000_000 - 1);
            var yGap = yGaps.Count(y => y > Math.Min(startY, endY) && y < Math.Max(startY, endY)) * (1_000_000 - 1);

            var length = xDistance + xGap + yDistance + yGap;
            totalLength += length;
        }

        return totalLength;
    }
}
