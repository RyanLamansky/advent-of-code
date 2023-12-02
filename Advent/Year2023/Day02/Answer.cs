namespace Advent.Year2023.Day02;

public sealed class Answer : IPuzzle<int>
{
    private static IEnumerable<(int Game, (int Red, int Green, int Blue)[] Sets)> Parse(IEnumerable<string> input)
        => input
            .Select((game, id) => (id + 1, game
                [(game.IndexOf(':') + 1)..]
                .Split(';')
                .Select(set => set
                    .Split(',', StringSplitOptions.TrimEntries)
                    .Select(type => type.Split(' '))
                    .ToDictionary(pair => pair[1], pair => int.Parse(pair[0]))
                )
                .Select(sets => (
                    Red: sets.GetValueOrDefault("red"),
                    Green: sets.GetValueOrDefault("green"),
                    Blue: sets.GetValueOrDefault("blue")
                ))
                .ToArray()
            ));

    public int Part1(IEnumerable<string> input)
    {
        var total = 0;

        foreach (var (game, sets) in Parse(input))
            if (sets.All(set => set.Red <= 12 && set.Green <= 13 && set.Blue <= 14))
                total += game;

        return total;
    }

    public int Part2(IEnumerable<string> input)
    {
        var total = 0;

        foreach (var (game, sets) in Parse(input))
            total += sets.Max(set => set.Red) * sets.Max(set => set.Green) * sets.Max(set => set.Blue);

        return total;
    }
}
