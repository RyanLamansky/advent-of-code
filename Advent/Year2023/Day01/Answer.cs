namespace Advent.Year2023.Day01;

public sealed class Answer : IPuzzle<int>
{
    public int Part1(IEnumerable<string> input)
    {
        var total = 0;
        foreach (var line in input)
        {
            var digits = line.Where(char.IsDigit).Select(c => c - '0').ToArray();
            total += digits.First() * 10 + digits.Last();
        }

        return total;
    }

    public int Part2(IEnumerable<string> input)
    {
        var total = 0;
        foreach (var line in input)
        {
            var digit0 = new (int Index, int Value)[] {
                (line.IndexOf('0'), 0),
                (line.IndexOf('1'), 1),
                (line.IndexOf('2'), 2),
                (line.IndexOf('3'), 3),
                (line.IndexOf('4'), 4),
                (line.IndexOf('5'), 5),
                (line.IndexOf('6'), 6),
                (line.IndexOf('7'), 7),
                (line.IndexOf('8'), 8),
                (line.IndexOf('9'), 9),
                (line.IndexOf("one"), 1),
                (line.IndexOf("two"), 2),
                (line.IndexOf("three"), 3),
                (line.IndexOf("four"), 4),
                (line.IndexOf("five"), 5),
                (line.IndexOf("six"), 6),
                (line.IndexOf("seven"), 7),
                (line.IndexOf("eight"), 8),
                (line.IndexOf("nine"), 9),
            }.Where(match => match.Index >= 0).OrderBy(match => match.Index).Select(match => match.Value).First();

            var digit1 = new (int Index, int Value)[] {
                (line.LastIndexOf('0'), 0),
                (line.LastIndexOf('1'), 1),
                (line.LastIndexOf('2'), 2),
                (line.LastIndexOf('3'), 3),
                (line.LastIndexOf('4'), 4),
                (line.LastIndexOf('5'), 5),
                (line.LastIndexOf('6'), 6),
                (line.LastIndexOf('7'), 7),
                (line.LastIndexOf('8'), 8),
                (line.LastIndexOf('9'), 9),
                (line.LastIndexOf("one"), 1),
                (line.LastIndexOf("two"), 2),
                (line.LastIndexOf("three"), 3),
                (line.LastIndexOf("four"), 4),
                (line.LastIndexOf("five"), 5),
                (line.LastIndexOf("six"), 6),
                (line.LastIndexOf("seven"), 7),
                (line.LastIndexOf("eight"), 8),
                (line.LastIndexOf("nine"), 9),
            }.Where(match => match.Index >= 0).OrderByDescending(match => match.Index).Select(match => match.Value).First();

            total += digit0 * 10 + digit1;
        }

        return total;
    }
}
