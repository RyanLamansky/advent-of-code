namespace Advent.Year2023.Day04;

public sealed class Answer : IPuzzle<int>
{
    static IEnumerable<(int[] numbers, int[] winners)> Parse(IEnumerable<string> input)
        => input.Select(line =>
        {
            var parts = line[(line.IndexOf(':') + 2)..].Split('|', StringSplitOptions.TrimEntries);
            var numbers = parts[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            var winners = parts[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

            return (numbers, winners);
        });
    
    public int Part1(IEnumerable<string> input)
    {
        var total = 0;
        foreach (var (numbers, winners) in Parse(input))
        {
            var matches = winners.Intersect(numbers).Count();
            if (matches == 0)
                continue;

            var score = 1;
            for (var i = 1; i < matches; i++)
                score *= 2;

            total += score;
        }

        return total;
    }

    public int Part2(IEnumerable<string> input)
    {
        var copies = new Dictionary<int, int>();
        var cardNumber = 0;
        foreach (var (numbers, winners) in Parse(input))
        {
            cardNumber++;
            copies.TryGetValue(cardNumber, out var boost);
            var matches = winners.Intersect(numbers).Count();

            while (matches > 0)
            {
                copies.TryGetValue(cardNumber + matches, out var card);
                copies[cardNumber + matches] = card + 1 + boost;
                matches--;
            }
        }

        return copies.Values.Sum() + cardNumber;
    }
}
