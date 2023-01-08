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

        public int BasinSize(int x, int y)
        {
            var bottom = this[x, y];
            var toCheck = new Dictionary<(int X, int Y), byte> { { (x, y), bottom } };
            var done = new HashSet<(int X, int Y)> { (x, y) };
            var size = 1;

            do
            {
                var kv = toCheck.First();
                var target = kv.Key;
                var minimum = kv.Value;


                var candidate = (target.X, Y: target.Y - 1);
                var value = this[candidate.X, candidate.Y];
                if (value > minimum && value < 10)
                {
                    size++;
                    if (value < 9)
                        toCheck[candidate] = value;
                }

                candidate = (target.X, target.Y + 1);
                value = this[candidate.X, candidate.Y];
                if (value > minimum && value < 10)
                {
                    size++;
                    if (value < 9)
                        toCheck[candidate] = value;
                }

                candidate = (target.X - 1, target.Y);
                value = this[candidate.X, candidate.Y];
                if (value > minimum && value < 10)
                {
                    size++;
                    if (value < 9)
                        toCheck[candidate] = value;
                }

                candidate = (target.X + 1, target.Y);
                value = this[candidate.X, candidate.Y];
                if (value > minimum && value < 10)
                {
                    size++;
                    if (value < 9)
                        toCheck[candidate] = value;
                }

                done.Add(target);
                toCheck.Remove(kv.Key);
            } while (toCheck.Count > 0);

            return size;
        }
    }

    public int Part1(IEnumerable<string> input)
    {
        var map = new HeightMap(input);

        return map.Where(xy => map.IsLowPointOfLocation(xy.X, xy.Y)).Sum(xy => map[xy.X, xy.Y] + 1);
    }

    public int Part2(IEnumerable<string> input) => 0;
}
