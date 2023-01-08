namespace Advent.Year2021.Day03;

public sealed class Answer : IPuzzle<int>
{
    public int Part1(IEnumerable<string> input)
    {
        var ones = Array.Empty<int>();
        var count = 0;

        foreach (var value in input)
        {
            count++;

            if (ones == Array.Empty<int>())
                ones = new int[value.Length];

            for (var i = 0; i < ones.Length; i++)
            {
                if (value[i] == '1')
                    ones[i]++;
            }
        }

        var min = count / 2;

        var gammaRate = 0;
        foreach (var total in ones)
        {
            if (total > min)
                gammaRate = gammaRate << 1 | 1;
            else
                gammaRate <<= 1;
        }

        var epsilonRate = gammaRate ^ int.MaxValue >> (32 - ones.Length - 1);

        return gammaRate * epsilonRate;
    }

    public int Part2(IEnumerable<string> input)
    {
        var all = input.ToArray();

        var candidates = all.ToArray();
        var index = 0;
        while (candidates.Length != 1)
        {
            var ones = candidates.Count(o => o[index] == '1');
            var winner = ones >= candidates.Length - ones ? '1' : '0';

            candidates = candidates.Where(o => o[index] == winner).ToArray();

            index++;
        }

        var oxygen = Convert.ToInt32(candidates[0], 2);

        candidates = all.ToArray();
        index = 0;
        while (candidates.Length != 1)
        {
            var ones = candidates.Count(o => o[index] == '1');
            var winner = ones >= candidates.Length - ones ? '0' : '1';

            candidates = candidates.Where(o => o[index] == winner).ToArray();

            index++;
        }

        var scrubber = Convert.ToInt32(candidates[0], 2);

        return oxygen * scrubber;
    }
}
