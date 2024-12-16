namespace Advent.Year2024.Day16;

using Parsed = Grid<char>;
using Result = int;
using XY = (int X, int Y);

public sealed class Answer : IPuzzle<Parsed, Result>
{
    public Parsed Parse(IEnumerable<string> input)
        => Grid.From(input);

    enum Direction
    {
        North,
        East,
        South,
        West,
    }

    public Result Part1(Parsed parsed)
    {
        var herd = new Queue<(XY Location, Direction Direction, int Score)>();
        herd.Enqueue(((parsed.Find('S'), Direction.East, 0)));

        var bestScoreByLocationDirection = new Dictionary<(XY Location, Direction Direction), int>()
        {
            { (herd.Peek().Location, Direction.East), 0 }
        };

        while (herd.TryDequeue(out var reindeer))
        {
            void TryMove(XY target, Direction direction, int score)
            {
                var td = (target, direction);

                if (parsed[target] == '#')
                    return;

                if (bestScoreByLocationDirection.TryGetValue((target, direction), out var bestScore) && bestScore <= score)
                    return;

                bestScoreByLocationDirection[td] = score;

                if (parsed[target] == 'E')
                    return;

                herd.Enqueue((target, direction, score));
            }

            var ((x, y), direction, score) = reindeer;

            switch (direction)
            {
                case Direction.East:
                    TryMove((x, y - 1), Direction.North, score + 1001);
                    TryMove((x + 1, y), Direction.East, score + 1);
                    TryMove((x, y + 1), Direction.South, score + 1001);
                    break;
                case Direction.South:
                    TryMove((x + 1, y), Direction.East, score + 1001);
                    TryMove((x, y + 1), Direction.South, score + 1);
                    TryMove((x - 1, y), Direction.West, score + 1001);
                    break;
                case Direction.West:
                    TryMove((x, y + 1), Direction.South, score + 1001);
                    TryMove((x - 1, y), Direction.West, score + 1);
                    TryMove((x, y - 1), Direction.North, score + 1001);
                    break;
                case Direction.North:
                    TryMove((x - 1, y), Direction.West, score + 1001);
                    TryMove((x, y - 1), Direction.North, score + 1);
                    TryMove((x + 1, y), Direction.East, score + 1001);
                    break;
            }
        }

        var end = parsed.Find('E');

        return bestScoreByLocationDirection
            .Where(kv => kv.Key.Location == end)
            .Min(kv => kv.Value)
            ;
    }

    public Result Part2(Parsed parsed)
    {
        return 0;
    }
}
