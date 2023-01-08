namespace Advent.Year2021.Day07;

public sealed class Answer : IPuzzle<int>
{
    public int Part1(IEnumerable<string> input)
    {
        var starts = input.First().Split(',').Select(int.Parse).ToArray();
        var min = starts.Min();
        var max = starts.Max();

        return Enumerable
            .Range(min, max - min + 1)
            .Select(target => starts.Sum(start => Math.Abs(start - target)))
            .Min();
    }

    public int Part2(IEnumerable<string> input)
    {
        var starts = input.First().Split(',').Select(int.Parse).ToArray();
        var min = starts.Min();
        var max = starts.Max();
        var costsByDistance = new int[max + 1];
        for (var i = 1; i < costsByDistance.Length; i++)
            costsByDistance[i] = Enumerable.Range(1, i).Sum();

        return Enumerable
            .Range(min, max - min + 1)
            .Select(target => starts.Sum(start => costsByDistance[Math.Abs(start - target)]))
            .Min();
    }
}
