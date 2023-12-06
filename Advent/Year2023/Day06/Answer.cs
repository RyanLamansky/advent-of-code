namespace Advent.Year2023.Day06;

public sealed class Answer : IPuzzle<int>
{
    public int Part1(IEnumerable<string> input)
    {
        var lines = input.ToArray();
        var times = lines[0]["Time:      ".Length..]
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToArray();
        var distances = lines[1]["Distance:  ".Length..]
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToArray();

        var multiplied = 1;
        foreach (var (time, record) in times.Select((time, index) => (time, distances[index])))
        {
            var speeds = Enumerable
                .Range(0, time)
                .Select((speed, remaining) => speed * (time - remaining))
                .Where(distance => distance > record)
                .Count();

            multiplied *= speeds;
        }

        return multiplied;
    }

    public int Part2(IEnumerable<string> input)
    {
        var lines = input.ToArray();
        var time = int.Parse(lines[0]["Time:      ".Length..].Replace(" ", ""));
        var record = long.Parse(lines[1]["Distance:  ".Length..].Replace(" ", ""));

        return Enumerable
            .Range(0, time)
            .AsParallel()
            .Select((speed, remaining) => (long)speed * (time - remaining))
            .Where(distance => distance > record)
            .Count();
    }
}
