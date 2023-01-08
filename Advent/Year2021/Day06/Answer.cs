namespace Advent.Year2021.Day06;

public sealed class Answer : IPuzzle<long>
{
    static long Calculate(IEnumerable<string> input, int days)
    {
        var countdowns = new long[9];
        foreach (var age in input.First().Split(',').Select(int.Parse))
            countdowns[age]++;

        for (var day = 1; day <= days; day++)
        {
            var growth = countdowns[0];
            Array.Copy(countdowns, 1, countdowns, 0, 8);
            countdowns[6] += growth;
            countdowns[8] = growth;
        }

        return countdowns.Sum();
    }

    public long Part1(IEnumerable<string> input) => Calculate(input, 80);

    public long Part2(IEnumerable<string> input) => Calculate(input, 256);
}
