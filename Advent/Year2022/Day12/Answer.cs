namespace Advent.Year2022.Day12;

public sealed class Answer : IPuzzle
{
    private static char[,] ReadMap(IEnumerable<string> input, out (byte X, byte Y) start, out (byte X, byte Y) end)
    {
        start = default;
        end = default;
        var lines = input.ToArray();

        var map = new char[lines[0].Length, lines.Length];

        for (byte y = 0; y < lines.Length; y++)
        {
            var line = lines[y];
            for (byte x = 0; x < line.Length; x++)
            {
                var c = line[x];
                map[x, y] = c;

                if (c == 'S')
                    start = (x, y);
                else if (c == 'E')
                    end = (x, y);
            }
        }

        map[start.X, start.Y] = 'a';
        map[end.X, end.Y] = 'z';

        return map;
    }

    static int? LowestSteps(char[,] map, (byte X, byte Y) start, (byte X, byte Y) end)
    {
        var maxX = map.GetLength(0) - 1;
        var maxY = map.GetLength(1) - 1;
        var best = new int[maxX + 1, maxY + 1];

        for (var x = 0; x <= maxX; x++)
            for (var y = 0; y <= maxY; y++)
                best[x, y] = int.MaxValue;

        best[start.X, start.Y] = 0;

        var currentQueue = new HashSet<(byte X, byte Y)> { start };
        var nextQueue = new HashSet<(byte X, byte Y)>();
        var step = -1;

        void TryEnqueue(char height, int x, int y)
        {
            if (x < 0 || x > maxX)
                return;
            if (y < 0 || y > maxY)
                return;

            if (best[x, y] <= step + 1)
                return;
            if (map[x, y] > height + 1)
                return;

            best[x, y] = step + 1;
            nextQueue.Add(((byte)x, (byte)y));
        }

        while (true)
        {
            step++;

            foreach (var test in currentQueue)
            {
                if (test == end)
                    return step;

                var (x, y) = test;
                var height = map[x, y];
                TryEnqueue(height, x + 1, y);
                TryEnqueue(height, x, y + 1);
                TryEnqueue(height, x - 1, y);
                TryEnqueue(height, x, y - 1);
            }

            if (nextQueue.Count == 0)
                return null; // Not reachable from this starting point. Only happens in Part 2.

            (nextQueue, currentQueue) = (currentQueue, nextQueue);
            nextQueue.Clear();
        }
    }

    public int Part1(IEnumerable<string> input)
    {
        var map = ReadMap(input, out var start, out var end);

        return LowestSteps(map, start, end).GetValueOrDefault();
    }

    public int Part2(IEnumerable<string> input)
    {
        var map = ReadMap(input, out var start, out var end);

        var results = new List<int>();

        for (byte x = 0; x < map.GetLength(0); x++)
        {
            for (byte y = 0; y < map.GetLength(1); y++)
            {
                if (map[x, y] == 'a')
                {
                    // This can be sped up by sharing the best grid with subsequent runs for much earlier bailouts.
                    var steps = LowestSteps(map, (x, y), end);
                    if (steps is not null)
                        results.Add(steps.Value);
                }
            }
        }

        return results.Min();
    }
}
