namespace Advent.Year2023.Day12;

public sealed class Answer : IPuzzle<int>
{
    static IEnumerable<(string Statuses, int[] GroupSizes)> Parse(IEnumerable<string> input) => input.Select(line =>
    {
        var parts = line.Split(' ');
            
        return (parts[0], parts[1].Split(',').Select(int.Parse).ToArray());
    });

    public int Part1(IEnumerable<string> input)
    {
        var arrangements = 0;
        foreach (var (statuses, groupSizes) in Parse(input))
        {
            var shifts = statuses
                .Select((c, i) => (c, i))
                .Where((ci => ci.c == '?'))
                .Select(ci => ci.i)
                .ToArray();

            var binary = statuses
                .Select((c, i) => (c, i))
                .Where((ci => ci.c == '#'))
                .Select(ci => ci.i)
                .Aggregate(0, (result, i) => result |= (1 << i));

            var popCount = groupSizes.Sum() - statuses.Count(c => c == '#');

            var min = 0;
            for (var i = 0; i < popCount; i++)
                min = (min << 1) | 1;

            for (var i = min; int.PopCount(i) <= shifts.Length; i++)
            {
                if (int.PopCount(i) != popCount)
                    continue;

                var test = i;
                var shifted = 0;
                foreach (var shift in shifts)
                {
                    shifted |= (test & 0b1) << shift;
                    test >>= 1;
                }

                var applied = shifted | binary;

                var counted = applied;
                var nope = false;

                foreach (var size in groupSizes)
                {
                    while (counted != 0 && (counted & 0b1) == 0)
                        counted >>= 1;

                    var localSize = 0;

                    while (counted != 0 && (counted & 0b1) == 1)
                    {
                        counted >>= 1;
                        localSize++;
                    }

                    if (localSize != size)
                    {
                        nope = true;
                        break;
                    }
                }

                if (!nope)
                    arrangements++;
            }
        }

        return arrangements;
    }

    public int Part2(IEnumerable<string> input)
    {
        return 0;
    }
}
