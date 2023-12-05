namespace Advent.Year2023.Day05;

public sealed class Answer : IPuzzle<uint>
{
    static (uint[] Seeds, (string Mapping, (uint Destination, uint Source, uint Length)[] Ranges)[] Mappings) Parse(IEnumerable<string> input)
    {
        using var enumerator = input.GetEnumerator();
        enumerator.MoveNext();

        var seeds = enumerator.Current["seeds: ".Length..].Split(' ').Select(uint.Parse).ToArray();
        var mappings = new List<(string Mapping, (uint Destination, uint Source, uint Length)[] Ranges)>();

        enumerator.MoveNext();

        while (enumerator.MoveNext())
        {
            var mapping = enumerator.Current;
            var ranges = new List<(uint Destination, uint Source, uint Length)>();

            string current;
            while (enumerator.MoveNext() && (current = enumerator.Current).Length != 0)
            {
                var values = current.Split(' ').Select(uint.Parse).ToArray();
                ranges.Add((values[0], values[1], values[2]));
            }

            mappings.Add((mapping, ranges.ToArray()));
        }

        return (seeds, mappings.ToArray());
    }

    public uint Part1(IEnumerable<string> input)
    {
        var (seeds, mappings) = Parse(input);

        var minimum = uint.MaxValue;

        foreach (var seed in seeds)
        {
            var value = seed;
            foreach (var (mapping, ranges) in mappings)
            {
                foreach (var (destination, source, length) in ranges)
                {
                    if (value >= source && value < source + length)
                    {
                        var adjustment = value - source;
                        value = destination + adjustment;
                        break;
                    }
                }
            }

            minimum = Math.Min(value, minimum);
        }

        return minimum;
    }

    public uint Part2(IEnumerable<string> input)
    {
        return 0;
    }
}
