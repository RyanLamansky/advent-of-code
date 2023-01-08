namespace Advent.Year2022.Day14;

public sealed class Answer : IPuzzle<int>
{
    private readonly struct XY
    {
        public readonly int X, Y;

        public XY(ReadOnlySpan<char> raw)
        {
            var comma = raw.IndexOf(',');
            var rawX = raw[..comma];
            var rawY = raw[(comma + 1)..];

            X = int.Parse(rawX);
            Y = int.Parse(rawY);
        }

        public override string ToString() => $"{X},{Y}";

        public static IEnumerable<XY[]> ReadLines(IEnumerable<string> input)
        {
            var collected = new List<XY>();

            foreach (var line in input)
            {
                var split = line.Split(" -> ");
                foreach (var raw in split)
                    collected.Add(new XY(raw));

                yield return collected.ToArray();
                collected.Clear();
            }
        }
    }

    private readonly struct Cell : IEquatable<Cell>
    {
        private enum State : byte
        {
            Air = 0,
            Rock,
            Sand
        }

        private readonly State state;

        public Cell(char value) => state = value switch
        {
            '.' => State.Air,
            '#' => State.Rock,
            'o' => State.Sand,
            _ => throw new Exception()
        };

        public override string ToString() => state switch
        {
            State.Air => ".",
            State.Rock => "#",
            State.Sand => "o",
            _ => throw new Exception()
        };

        public char ToChar() => state switch
        {
            State.Air => '.',
            State.Rock => '#',
            State.Sand => 'o',
            _ => throw new Exception()
        };

        public static implicit operator Cell(char value) => new(value);

        public static bool operator ==(Cell cell, char value) => cell.ToChar() == value;

        public static bool operator !=(Cell cell, char value) => cell.ToChar() != value;

        public override bool Equals(object? obj) => obj is Cell cell && Equals(cell);

        public override int GetHashCode() => state.GetHashCode();

        public bool Equals(Cell other) => state == other.state;
    }

    private sealed class Cave
    {
        private readonly Cell[,] cells;
        private readonly int baseX = 0;

        public Cave(IEnumerable<string> input, int margin)
        {
            var rockTraces = XY.ReadLines(input).ToArray();
            var minX = rockTraces.SelectMany(line => line).Min(xy => xy.X);
            var maxX = rockTraces.SelectMany(line => line).Max(xy => xy.X);
            const int minY = 0; // Sand origin is 500,0, which is a lower Y than any input data.
            var maxY = rockTraces.SelectMany(line => line).Max(xy => xy.Y) + 2;
            baseX = minX - margin;

            cells = new Cell[maxX + (margin * 2 + 1) - minX, maxY + 1 - minY];

            foreach (var trace in rockTraces)
            {
                for (var i = 1; i < trace.Length; i++)
                {
                    var start = trace[i - 1];
                    var end = trace[i];

                    if (start.Y == end.Y)
                        for (var x = Math.Min(start.X, end.X); x <= Math.Max(start.X, end.X); x++)
                            this[x, start.Y] = '#';
                    else
                        for (var y = Math.Min(start.Y, end.Y); y <= Math.Max(start.Y, end.Y); y++)
                            this[start.X, y] = '#';
                }
            }
        }

        public void RockBase()
        {
            var y = cells.GetLength(1) - 1;
            for (var x = 0; x < cells.GetLength(0); x++)
                cells[x, y] = '#';
        }

        public Cell this[XY xy]
        {
            get => cells[xy.X - baseX, xy.Y];
            set => cells[xy.X - baseX, xy.Y] = value;
        }
        public Cell this[int x, int y]
        {
            get => cells[x - baseX, y];
            set => cells[x - baseX, y] = value;
        }

        public override string ToString()
        {
            var builder = new System.Text.StringBuilder();

            for (var y = 0; y < cells.GetLength(1); y++)
            {
                for (var x = 0; x < cells.GetLength(0); x++)
                    builder.Append(cells[x, y].ToChar());

                builder.AppendLine();
            }

            return builder.ToString();
        }

        public bool TryDropSand(int x, int y)
        {
            if (this[x, y] == 'o')
                return false;

            for (; y + 1 < cells.GetLength(1); y++)
            {
                var y2 = y + 1;
                if (this[x, y2] == '.')
                    continue;

                if (this[x - 1, y2] == '.')
                {
                    x--;
                    continue;
                }

                if (this[x + 1, y2] == '.')
                {
                    x++;
                    continue;
                }

                this[x, y] = 'o';
                return true;
            }

            return false;
        }
    }

    public int Part1(IEnumerable<string> input)
    {
        var cave = new Cave(input, 1);
        var dropped = 0;
        while (cave.TryDropSand(500, 0))
            dropped++;
        return dropped;
    }

    public int Part2(IEnumerable<string> input)
    {
        var cave = new Cave(input, 501);
        cave.RockBase();
        var dropped = 0;
        while (cave.TryDropSand(500, 0))
            dropped++;
        return dropped;
    }
}
