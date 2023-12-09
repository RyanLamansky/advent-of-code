namespace Advent.Year2023.Day09;

public sealed class Answer : IPuzzle<int>
{
    public int Part1(IEnumerable<string> input) => input.Select(line =>
    {
        var allDifferences = new List<int[]>();
        var differences = line.Split(' ').Select(int.Parse).ToArray();
        while (differences.Any(difference => difference != 0))
        {
            allDifferences.Add(differences);
            differences = differences.Skip(1).Select((value, i) => value - differences[i]).ToArray();
        }

        var next = 0;
        foreach (var last in allDifferences.Select(set => set.Last()).Reverse())
            next += last;

        return next;
    }).Sum();

    public int Part2(IEnumerable<string> input) => input.Select(line =>
    {
        var allDifferences = new List<int[]>();
        var differences = line.Split(' ').Select(int.Parse).ToArray();
        while (differences.Any(difference => difference != 0))
        {
            allDifferences.Add(differences);
            differences = differences.Take(differences.Length - 1).Select((value, i) => value - differences[i + 1]).ToArray();
        }

        var previous = 0;
        foreach (var first in allDifferences.Select(set => set.First()).Reverse())
            previous += first;

        return previous;
    }).Sum();
}
