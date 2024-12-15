namespace Advent.Year2024.Day15;

using Parsed = (Grid<char> Map, char[] Moves);
using Result = int;
using XY = (int X, int Y);

public sealed class Answer : IPuzzle<Parsed, Result>
{
    public Parsed Parse(IEnumerable<string> input)
    {
        var blankLineIndex = input
            .Select((line, index) => (line, index))
            .Where(li => li.line.Length == 0)
            .Select(li => li.index)
            .First();

        var map = new Grid<char>(input.Take(blankLineIndex), c => c);
        var chars = input.Skip(blankLineIndex).SelectMany(line => line).ToArray();

        return (map, chars);
    }

    public Result Part1(Parsed parsed)
    {
        var (map, moves) = parsed;
        var botLocation = map.Find('@');

        foreach (var move in moves)
        {
            XY delta;
            switch (move)
            {
                case '^':
                    delta = (0, -1);
                    break;
                case '>':
                    delta = (1, 0);
                    break;
                case 'v':
                    delta = (0, 1);
                    break;
                case '<':
                    delta = (-1, 0);
                    break;
                default:
                    continue;
            }

            static bool TryMove(Grid<char> map, XY start, XY delta)
            {
                var destination = (start.X + delta.X, start.Y + delta.Y);
                switch (map[destination])
                {
                    case '.':
                        map[destination] = map[start];
                        map[start] = '.';
                        return true;
                    case '#':
                        return false;
                }

                if (!TryMove(map, destination, delta))
                    return false;

                map[destination] = map[start];
                map[start] = '.';
                return true;
            }

            if (TryMove(map, botLocation, delta))
                botLocation = (botLocation.X + delta.X, botLocation.Y + delta.Y);
        }

        return map.Where(xyc => xyc.Value == 'O').Sum(xyc => 100 * xyc.Y + xyc.X);
    }

    public Result Part2(Parsed parsed)
    {
        return 0;
    }
}
