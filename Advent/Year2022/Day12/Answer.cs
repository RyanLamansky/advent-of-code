namespace Advent.Year2022.Day12;

public static class TupleExtensions
{
    public static (byte X, byte Y) Up(this (byte X, byte Y) location) => (location.X, (byte)(location.Y - 1));
    public static (byte X, byte Y) Down(this (byte X, byte Y) location) => (location.X, (byte)(location.Y + 1));
    public static (byte X, byte Y) Left(this (byte X, byte Y) location) => ((byte)(location.X - 1), location.Y);
    public static (byte X, byte Y) Right(this (byte X, byte Y) location) => ((byte)(location.X + 1), location.Y);
}

public sealed class Answer : IPuzzle
{
    private sealed class Map
    {
        private readonly (byte X, byte Y) Start, End;
        private readonly char[,] map;

        public Map(IEnumerable<string> input)
        {
            var lines = input.ToList();

            var map = this.map = new char[lines[0].Length, lines.Count];

            for (byte y = 0; y < lines.Count; y++)
            {
                var line = lines[y];
                for (byte x = 0; x < line.Length; x++)
                {
                    var c = line[x];
                    map[x, y] = c;

                    if (c == 'S')
                        Start = (x, y);
                    if (c == 'E')
                        End = (x, y);
                }
            }
        }

        private char? this[(byte X, byte Y) location]
        {
            get
            {
                var (x, y) = location;

                if (x < 0)
                    return null;
                if (y < 0)
                    return null;
                if (x >= map.GetLength(0))
                    return null;
                if (y >= map.GetLength(1))
                    return null;

                return map[x, y];
            }
        }

        public readonly struct Path
        {
            public readonly (byte X, byte Y) Location;
            public readonly (byte X, byte Y)[] Visited;

            public Path((byte X, byte Y) location, (byte X, byte Y)[] visited)
            {
                Location = location;
                Visited = visited;
            }

            public Path To((byte X, byte Y) location)
            {
                var array = new (byte X, byte Y)[Visited.Length + 1];
                Array.Copy(Visited, array, Visited.Length);
                array[array.Length - 1] = location;

                return new Path(location, array);
            }

            public override string ToString() => $"{Location}; {Visited.Length}";
        }

        public IEnumerable<Path> Paths()
        {
            var queue = new Queue<Path>();
            queue.Enqueue(new Path(Start, new (byte X, byte Y)[] { Start }));

            while (queue.TryDequeue(out var path))
            {
                var location = path.Location;

                if (path.Location == End)
                {
                    yield return path;
                    continue;
                }

                var visited = path.Visited;

                var current = this[location]!.Value;

                var step = location.Up();
                var charAt = this[step];
                if (charAt is not null && (current == 'S' || (charAt == 'E' && current == 'z') || Math.Abs(current - charAt.Value) <= 1) && !visited.Contains(step))
                    queue.Enqueue(path.To(step));

                step = location.Down();
                charAt = this[step];
                if (charAt is not null && (current == 'S' || (charAt == 'E' && current == 'z') || Math.Abs(current - charAt.Value) <= 1) && !visited.Contains(step))
                    queue.Enqueue(path.To(step));

                step = location.Left();
                charAt = this[step];
                if (charAt is not null && (current == 'S' || (charAt == 'E' && current == 'z') || Math.Abs(current - charAt.Value) <= 1) && !visited.Contains(step))
                    queue.Enqueue(path.To(step));

                step = location.Right();
                charAt = this[step];
                if (charAt is not null && (current == 'S' || (charAt == 'E' && current == 'z') || Math.Abs(current - charAt.Value) <= 1) && !visited.Contains(step))
                    queue.Enqueue(path.To(step));
            }
        }
    }

    public int Part1(IEnumerable<string> input)
    {
        var map = new Map(input);

        return 0;
    }

    public int Part2(IEnumerable<string> input)
    {
        var map = new Map(input);

        return 0;
    }
}
