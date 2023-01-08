using System.Collections;

namespace Advent.Year2021.Day09;

public sealed class Answer : IPuzzle<int>
{
    private readonly struct HeightMap : IEnumerable<(int X, int Y)>
    {
        private readonly byte[,] map;

        public byte this[int x, int y]
        {
            get
            {
                if (x < 0 || y < 0 || x >= map.GetLength(0) || y >= map.GetLength(1))
                    return 10;

                return map[x, y];
            }
        }

        public HeightMap(IEnumerable<string> lines)
        {
            var parsedLines = lines.Select(line => line.Select(c => (byte)(c - '0')).ToArray()).ToArray();

            var map = this.map = new byte[parsedLines[0].Length, parsedLines.Length];

            for (var y = 0; y < parsedLines.Length; y++)
            {
                var line = parsedLines[y];
                for (var x = 0; x < line.Length; x++)
                    map[x, y] = line[x];
            }
        }

        public IEnumerator<(int, int)> GetEnumerator()
        {
            var map = this.map;
            for (var y = 0; y < map.GetLength(1); y++)
            {
                for (var x = 0; x < map.GetLength(0); x++)
                    yield return (x, y);
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public bool IsLowPointOfLocation(int x, int y)
        {
            var value = this[x, y];

            return value < this[x, y - 1]
                 && value < this[x - 1, y] && value < this[x + 1, y]
                 && value < this[x, y + 1];
        }
    }

    public int Part1(IEnumerable<string> input)
    {
        var map = new HeightMap(input);

        return map.Where(xy => map.IsLowPointOfLocation(xy.X, xy.Y)).Sum(xy => map[xy.X, xy.Y] + 1);
    }

    public int Part2(IEnumerable<string> input)
    {
        var map = new HeightMap(input);

        var used = new HashSet<(int X, int Y)>();
        
        var queue = new HashSet<(int X, int Y)>();
        var nextQueue = new HashSet<(int X, int Y)>();
        var sizes = new List<int>();

        foreach (var (x, y) in map)
        {
            if (!used.Add((x, y)) || map[x, y] >= 9)
                continue;

            var current = 1;
            queue.Add((x, y));

            do
            {
                void TryEnqueue(int x, int y)
                {
                    if (map[x, y] < 9 && used.Add((x, y)))
                    {
                        current++;
                        nextQueue.Add((x, y));
                    }
                }

                foreach (var (cx, cy) in queue)
                {
                    TryEnqueue(cx - 1, cy);
                    TryEnqueue(cx + 1, cy);
                    TryEnqueue(cx, cy - 1);
                    TryEnqueue(cx, cy + 1);
                };

                (nextQueue, queue) = (queue, nextQueue);
                nextQueue.Clear();
            } while (queue.Count != 0);

            sizes.Add(current);
        }

        return sizes.OrderDescending().Take(3).Aggregate(1, (current, value) => current * value);
    }
}
