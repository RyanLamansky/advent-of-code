namespace Advent.Year2024.Day06;

using Parsed = (Grid<char> Map, (int x, int y) Start);

public sealed class Answer : IPuzzle<Parsed, int>
{
    public Parsed Parse(IEnumerable<string> input)
    {
        var grid = new Grid<char>(input, c => c);

        var start = grid.Find('^');

        return (grid, start);
    }

    enum Direction
    {
        Up,
        Right,
        Down,
        Left,
    }

    public int Part1(Parsed parsed)
    {
        var (grid, start) = parsed;

        var visited = new HashSet<(int x, int y)>();

        var position = (start.x, start.y);
        var direction = Direction.Up;

        while (grid.GetOrDefault(position, ' ') != ' ')
        {
            visited.Add(position);

            var (x, y) = position;

            switch (direction)
            {
                case Direction.Up:
                    if (grid.GetOrDefault(x, y - 1, ' ') == '#')
                        direction = Direction.Right;
                    else
                    {
                        position = (x, y - 1);
                        grid.TrySet(position, '^');
                    }
                    break;
                case Direction.Right:
                    if (grid.GetOrDefault(x + 1, y, ' ') == '#')
                        direction = Direction.Down;
                    else
                    {
                        position = (x + 1, y);
                        grid.TrySet(position, '>');
                    }
                    break;
                case Direction.Down:
                    if (grid.GetOrDefault(x, y + 1, ' ') == '#')
                        direction = Direction.Left;
                    else
                    {
                        position = (x, y + 1);
                        grid.TrySet(position, 'v');
                    }
                    break;
                case Direction.Left:
                    if (grid.GetOrDefault(x - 1, y, ' ') == '#')
                        direction = Direction.Up;
                    else
                    {
                        position = (x - 1, y);
                        grid.TrySet(position, '<');
                    }
                    break;
            }
        }

        return visited.Count;
    }

    public int Part2(Parsed parsed) => parsed.Map.AsParallel().Count(gridValue =>
    {
        var blocked = (gridValue.X, gridValue.Y);

        var grid = parsed.Map.Clone();
        if (grid[blocked] == '#')
            return false;

        grid[blocked] = '#';
        var visited = new HashSet<(int x, int y, Direction direction)>();
        var position = (parsed.Start.x, parsed.Start.y);
        var direction = Direction.Up;

        while (grid.GetOrDefault(position, ' ') != ' ')
        {
            if (!visited.Add((position.x, position.y, direction)))
                return true;

            var (x, y) = position;

            switch (direction)
            {
                case Direction.Up:
                    if (grid.GetOrDefault(x, y - 1, ' ') == '#')
                        direction = Direction.Right;
                    else
                    {
                        position = (x, y - 1);
                        grid.TrySet(position, '^');
                    }
                    break;
                case Direction.Right:
                    if (grid.GetOrDefault(x + 1, y, ' ') == '#')
                        direction = Direction.Down;
                    else
                    {
                        position = (x + 1, y);
                        grid.TrySet(position, '>');
                    }
                    break;
                case Direction.Down:
                    if (grid.GetOrDefault(x, y + 1, ' ') == '#')
                        direction = Direction.Left;
                    else
                    {
                        position = (x, y + 1);
                        grid.TrySet(position, 'v');
                    }
                    break;
                case Direction.Left:
                    if (grid.GetOrDefault(x - 1, y, ' ') == '#')
                        direction = Direction.Up;
                    else
                    {
                        position = (x - 1, y);
                        grid.TrySet(position, '<');
                    }
                    break;
            }
        }

        return false;
    });
}
