namespace Advent.Year2023.Day10;

public sealed class Answer : IPuzzle<int>
{
    static (Dictionary<(int X, int Y), char> Map, (int X, int Y) Start) Parse(IEnumerable<string> input)
    {
        var map = new Dictionary<(int X, int Y), char>();
        var start = (X: -1, Y: -1);
        var y = -1;
        foreach (var line in input)
        {
            y++;
            var x = -1;
            foreach (var c in line)
            {
                x++;

                switch (c)
                {
                    case '.':
                        continue;
                    case 'S':
                        start = (x, y);
                        break;
                }

                map.Add((x, y), c switch
                {
                    '|' => '│',
                    '-' => '─',
                    'L' => '└',
                    'J' => '┘',
                    '7' => '┐',
                    'F' => '┌',
                    'S' => 'S',
                    _ => throw new Exception()
                });
            }
        }

        return (map, start);
    }

    public int Part1(IEnumerable<string> input)
    {
        var (map, start) = Parse(input);

        var visited = new HashSet<(int X, int Y)> { start };
        var next = new HashSet<(int X, int Y)> { start };
        var current = new HashSet<(int X, int Y)>();
        var steps = -1;

        do
        {
            steps++;

            (current, next) = (next, current);
            next.Clear();

            foreach (var (x, y) in current)
            {
                var self = map[(x, y)];
                visited.Add((x, y));

                static void Up(Dictionary<(int X, int Y), char> map, int x, int y, HashSet<(int X, int Y)> next)
                {
                    if (map.TryGetValue((x, y - 1), out var target) && target is '┐' or '┌' or '│')
                        next.Add((x, y - 1));
                }

                static void Right(Dictionary<(int X, int Y), char> map, int x, int y, HashSet<(int X, int Y)> next)
                {
                    if (map.TryGetValue((x + 1, y), out var target) && target is '┐' or '┘' or '─')
                        next.Add((x + 1, y));
                }

                static void Down(Dictionary<(int X, int Y), char> map, int x, int y, HashSet<(int X, int Y)> next)
                {
                    if (map.TryGetValue((x, y + 1), out var target) && target is '└' or '┘' or '│')
                        next.Add((x, y + 1));
                }

                static void Left(Dictionary<(int X, int Y), char> map, int x, int y, HashSet<(int X, int Y)> next)
                {
                    if (map.TryGetValue((x - 1, y), out var target) && target is '└' or '┌' or '─')
                        next.Add((x - 1, y));
                }

                switch (self)
                {
                    case '│':
                        Up(map, x, y, next);
                        Down(map, x, y, next);
                        break;
                    case '─':
                        Right(map, x, y, next);
                        Left(map, x, y, next);
                        break;
                    case '└':
                        Up(map, x, y, next);
                        Right(map, x, y, next);
                        break;
                    case '┘':
                        Up(map, x, y, next);
                        Left(map, x, y, next);
                        break;
                    case '┐':
                        Down(map, x, y, next);
                        Left(map, x, y, next);
                        break;
                    case '┌':
                        Right(map, x, y, next);
                        Down(map, x, y, next);
                        break;
                    case 'S':
                        Up(map, x, y, next);
                        Right(map, x, y, next);
                        Down(map, x, y, next);
                        Left(map, x, y, next);
                        break;
                }
            }

            next.ExceptWith(visited);
        } while (next.Count > 0);

        return steps;
    }

    public int Part2(IEnumerable<string> input)
    {
        return 0;
    }
}
