namespace Advent.Year2021.Day11;

public sealed class Answer : IPuzzle<int>
{
    public int Part1(IEnumerable<string> input)
    {
        var grid = new Grid<int>(input, c => c - '0');
        var flashed = new HashSet<(int X, int Y)>();

        var totalFlashes = 0;

        for (var i = 0; i < 100; i++)
        {
            foreach (var (x, y, value) in grid)
                grid[x, y] = value + 1;

            do
            {
                flashed.Clear();

                foreach (var (x, y, value) in grid)
                {
                    if (value < 10)
                        continue;

                    totalFlashes++;

                    flashed.Add((x, y));
                    grid[x, y] = 0;

                    static int Energize(int v) => v = v == 0 ? 0 : v + 1;

                    grid.TryModify(x - 1, y - 1, Energize);
                    grid.TryModify(x, y - 1, Energize);
                    grid.TryModify(x + 1, y - 1, Energize);

                    grid.TryModify(x - 1, y, Energize);
                    grid.TryModify(x + 1, y, Energize);

                    grid.TryModify(x - 1, y + 1, Energize);
                    grid.TryModify(x, y + 1, Energize);
                    grid.TryModify(x + 1, y + 1, Energize);
                }
            } while (flashed.Count != 0);
        }

        return totalFlashes;
    }

    public int Part2(IEnumerable<string> input)
    {
        var grid = new Grid<int>(input, c => c - '0');
        var flashed = new HashSet<(int X, int Y)>();
        var step = 0;
        do
        {
            step++;

            foreach (var (x, y, value) in grid)
                grid[x, y] = value + 1;

            do
            {
                flashed.Clear();

                foreach (var (x, y, value) in grid)
                {
                    if (value < 10)
                        continue;

                    flashed.Add((x, y));
                    grid[x, y] = 0;

                    static int Energize(int v) => v = v == 0 ? 0 : v + 1;

                    grid.TryModify(x - 1, y - 1, Energize);
                    grid.TryModify(x, y - 1, Energize);
                    grid.TryModify(x + 1, y - 1, Energize);

                    grid.TryModify(x - 1, y, Energize);
                    grid.TryModify(x + 1, y, Energize);

                    grid.TryModify(x - 1, y + 1, Energize);
                    grid.TryModify(x, y + 1, Energize);
                    grid.TryModify(x + 1, y + 1, Energize);
                }
            } while (flashed.Count != 0);
        } while (!grid.All(tuple => tuple.Value == 0));

        return step;
    }
}
