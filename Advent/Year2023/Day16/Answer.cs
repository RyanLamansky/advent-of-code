namespace Advent.Year2023.Day16;

public sealed class Answer : IPuzzle<int>
{
    static Dictionary<(int X, int Y), char> Parse(IEnumerable<string> input)
    {
        var grid = new Dictionary<(int X, int Y), char>();

        var y = -1;

        foreach (var line in input)
        {
            y++;

            for (var x = 0; x < line.Length; x++)
                grid.Add((x, y), line[x]);
        }

        return grid;
    }

    static int TraceEnergy(
        Dictionary<(int X, int Y), char> grid,
        HashSet<(int X, int Y)> newUp,
        HashSet<(int X, int Y)> newRight,
        HashSet<(int X, int Y)> newDown,
        HashSet<(int X, int Y)> newLeft)
    {
        HashSet<(int X, int Y)>
            up = [],
            right = [],
            down = [],
            left = [];

        while (newUp.Count + newRight.Count + newDown.Count + newLeft.Count > 0)
        {
            var set = newUp.ToHashSet();
            newUp.Clear();

            foreach (var (x, y) in set)
            {
                if (!grid.TryGetValue((x, y), out var c) || !up.Add((x, y)))
                    continue;

                switch (c)
                {
                    case '.':
                    case '|':
                        newUp.Add((x, y - 1));
                        break;
                    case '-':
                        newLeft.Add((x - 1, y));
                        newRight.Add((x + 1, y));
                        break;
                    case '\\':
                        newLeft.Add((x - 1, y));
                        break;
                    case '/':
                        newRight.Add((x + 1, y));
                        break;
                }
            }

            set = [.. newRight];
            newRight.Clear();

            foreach (var (x, y) in set)
            {
                if (!grid.TryGetValue((x, y), out var c) || !right.Add((x, y)))
                    continue;

                switch (c)
                {
                    case '.':
                    case '-':
                        newRight.Add((x + 1, y));
                        break;
                    case '\\':
                        newDown.Add((x, y + 1));
                        break;
                    case '/':
                        newUp.Add((x, y - 1));
                        break;
                    case '|':
                        newUp.Add((x, y - 1));
                        newDown.Add((x, y + 1));
                        break;
                }
            }

            set = [.. newDown];
            newDown.Clear();

            foreach (var (x, y) in set)
            {
                if (!grid.TryGetValue((x, y), out var c) || !down.Add((x, y)))
                    continue;

                switch (c)
                {
                    case '.':
                    case '|':
                        newDown.Add((x, y + 1));
                        break;
                    case '-':
                        newLeft.Add((x - 1, y));
                        newRight.Add((x + 1, y));
                        break;
                    case '\\':
                        newRight.Add((x + 1, y));
                        break;
                    case '/':
                        newLeft.Add((x - 1, y));
                        break;
                }
            }

            set = [.. newLeft];
            newLeft.Clear();

            foreach (var (x, y) in set)
            {
                if (!grid.TryGetValue((x, y), out var c) || !left.Add((x, y)))
                    continue;

                switch (c)
                {
                    case '.':
                    case '-':
                        newLeft.Add((x - 1, y));
                        break;
                    case '\\':
                        newUp.Add((x, y - 1));
                        break;
                    case '/':
                        newDown.Add((x, y + 1));
                        break;
                    case '|':
                        newUp.Add((x, y - 1));
                        newDown.Add((x, y + 1));
                        break;
                }
            }
        }

        return up.Union(right).Union(down).Union(left).Count();
    }

    public int Part1(IEnumerable<string> input) => TraceEnergy(Parse(input), [], [(0, 0)], [], []);

    public int Part2(IEnumerable<string> input)
    {
        var grid = Parse(input);

        var maxX = grid.Keys.Max(xy => xy.X);
        var maxY = grid.Keys.Max(xy => xy.Y);

        var top = Enumerable.Range(0, maxX).AsParallel().Select(x => TraceEnergy(grid, [], [], [(x, 0)], []));
        var right = Enumerable.Range(0, maxY).AsParallel().Select(y => TraceEnergy(grid, [], [], [], [(maxX, y)]));
        var bottom = Enumerable.Range(0, maxX).AsParallel().Select(x => TraceEnergy(grid, [(x, maxY)], [], [], []));
        var left = Enumerable.Range(0, maxX).AsParallel().Select(y => TraceEnergy(grid, [], [(0, y)], [], []));

        return top.Concat(right).Concat(bottom).Concat(left).Max();
    }
}
