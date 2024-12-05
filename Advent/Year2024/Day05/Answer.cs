using System.Collections.Frozen;

namespace Advent.Year2024.Day05;

public readonly record struct PageOrderingRule(byte Before, byte After);

public readonly record struct Data(PageOrderingRule[] Rules, byte[][] Updates);

public sealed class Answer : IPuzzle<Data, int>
{
    public Data Parse(IEnumerable<string> input)
    {
        using var enumerator = input.GetEnumerator();

        var rules = new List<PageOrderingRule>();

        while (enumerator.MoveNext())
        {
            var line = enumerator.Current;
            if (line.Length == 0)
                break;

            rules.Add(new(byte.Parse(line.AsSpan(0, 2)), byte.Parse(line.AsSpan(3, 2))));
        }

        var updates = new List<byte[]>();

        while (enumerator.MoveNext())
        {
            updates.Add(enumerator.Current.Split(',').Select(v => byte.Parse(v)).ToArray());
        }

        return new([.. rules], [.. updates]);
    }

    public int Part1(Data parsed)
    {
        var aftersByBefore = parsed.Rules
            .GroupBy(rule => rule.Before, rule => rule.After)
            .ToFrozenDictionary(group => group.Key, group => group.ToFrozenSet());

        var total = 0;

        foreach (var update in parsed.Updates)
        {
            for (var i = 0; i < update.Length; i++)
            {
                var value = update[i];

                if (!aftersByBefore.TryGetValue(value, out var afters))
                    continue;

                for (var afterIndex = 0; afterIndex < i; afterIndex++)
                {
                    if (afters.Contains(update[afterIndex]))
                        goto NextUpdate;
                }
            }
            total += update[update.Length / 2];
        NextUpdate:;
        }

        return total;
    }

    public int Part2(Data parsed)
    {
        var aftersByBefore = parsed.Rules
            .GroupBy(rule => rule.Before, rule => rule.After)
            .ToFrozenDictionary(group => group.Key, group => group.ToFrozenSet());

        var total = 0;

        foreach (var update in parsed.Updates)
        {
            for (var i = 0; i < update.Length; i++)
            {
                var value = update[i];

                if (!aftersByBefore.TryGetValue(value, out var afters))
                    continue;

                for (var afterIndex = 0; afterIndex < i; afterIndex++)
                {
                    if (afters.Contains(update[afterIndex]))
                        goto NeedsReorder;
                }
            }

            continue;

        NeedsReorder:;
            Array.Sort(update, (a, b) =>
            {
                foreach (var rule in parsed.Rules)
                {
                    if (rule.Before == a && rule.After == b)
                        return -1;
                    if (rule.After == a && rule.Before == b)
                        return 1;
                }

                return 0;
            });
            total += update[update.Length / 2];
        }

        return total;
    }
}
