namespace Advent.Year2024.Day01;

public sealed class Answer : IPuzzle<int>
{
    static (int L, int R)[] ReadPairs(IEnumerable<string> input) =>
        input.Select(line =>
        {
            var split = line.Split("  ");
            return (L: int.Parse(split[0]), R: int.Parse(split[1]));
        }).ToArray();

    public int Part1(IEnumerable<string> input)
    {
        var pairs = ReadPairs(input);

        var totalDistance = pairs
            .Select(lr => lr.L)
            .OrderBy(i => i)
            .Zip(pairs.Select(lr => lr.R).OrderBy(i => i))
            .Sum(fs => Math.Abs(fs.First - fs.Second));

        return totalDistance;
    }

    public int Part2(IEnumerable<string> input)
    {
        var pairs = ReadPairs(input);

        var counts = pairs
            .Select(lr => lr.R)
            .GroupBy(i => i)
            .ToDictionary(group => group.Key, group => group.Count());

        return pairs.Select(lr => lr.L).Sum(i => i * counts.GetValueOrDefault(i));
    }
}
