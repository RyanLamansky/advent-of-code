using System.Text;

namespace Advent.Year2021.Day13;

public sealed class Answer : IPuzzle<string>
{
    private static (HashSet<(int X, int Y)>, (int X, int Y)[]) ParseInput(IEnumerable<string> input)
    {
        using var enumerator = input.GetEnumerator();

        var dots = new HashSet<(int, int)>();
        string line;
        while (enumerator.MoveNext() && (line = enumerator.Current).Length != 0)
        {
            var parts = line.Split(',');
            dots.Add((int.Parse(parts[0]), int.Parse(parts[1])));
        }

        var folds = new List<(int, int)>();

        while (enumerator.MoveNext() && (line = enumerator.Current).Length != 0)
        {
            int x, y;
            var value = line.AsSpan(13);
            switch (line[11])
            {
                case 'x':
                    x = int.Parse(value);
                    y = 0;
                    break;
                case 'y':
                    x = 0;
                    y = int.Parse(value);
                    break;
                default:
                    throw new Exception();
            }

            folds.Add((x, y));
        }

        return (dots, folds.ToArray());
    }

    public string Part1(IEnumerable<string> input)
    {
        var (dots, folds) = ParseInput(input);

        var (X, Y) = folds[0];
        var fold = Math.Max(X, Y);
        var folder = (Func<(int X, int Y), (int X, int Y)>)(X != 0
            ? dot => (dot.X < fold ? dot.X : dot.X - ((dot.X - fold) * 2), dot.Y)
            : dot => (dot.X, dot.Y < fold ? dot.Y : dot.Y - ((dot.Y - fold) * 2)));

        dots = dots.Select(folder).ToHashSet();

        return dots.Count.ToString();
    }

    public string Part2(IEnumerable<string> input)
    {
        var (dots, folds) = ParseInput(input);

        foreach (var (X, Y) in folds)
        {
            var fold = Math.Max(X, Y);
            var folder = (Func<(int X, int Y), (int X, int Y)>)(X != 0
                ? dot => (dot.X < fold ? dot.X : dot.X - ((dot.X - fold) * 2), dot.Y)
                : dot => (dot.X, dot.Y < fold ? dot.Y : dot.Y - ((dot.Y - fold) * 2)));

            dots = dots.Select(folder).ToHashSet();
        }

        var maxX = dots.Max(dot => dot.X);
        var maxY = dots.Max(dot => dot.Y);

        var result = new StringBuilder();
        for (var y = 0; y <= maxY; y++)
        {
            for (var x = 0; x <= maxX; x++)
                result.Append(dots.Contains((x, y)) ? '#' : '.');

            result.AppendLine();
        }

        return result.ToString().TrimEnd();
    }
}
