namespace Advent.Year2024.Day09;

using Parsed = int?[];
using Result = long;

public sealed class Answer : IPuzzle<Parsed, Result>
{
    public Parsed Parse(IEnumerable<string> input)
    {
        var result = new List<int?>();
        var fileId = -1;

        using (var c = input.First().GetEnumerator())
        {
            while (c.MoveNext())
            {
                fileId++;
                var count = c.Current - '0';

                for (var i = 0; i < count; i++)
                    result.Add(fileId);

                if (!c.MoveNext())
                    break;

                count = c.Current - '0';

                for (var i = 0; i < count; i++)
                    result.Add(null);
            }
        }

        return [.. result];
    }

    public Result Part1(Parsed parsed)
    {
        var taken = 0;

        for (var i = 0; i < parsed.Length - taken; i++)
        {
            if (parsed[i] is not null)
                continue;

            while ((parsed[i] = parsed[^(++taken)]) is null) ;

            parsed[^taken] = null;
        }

        var checksum = 0L;
        var skipped = 0;
        for (var i = 0; i < parsed.Length; i++)
        {
            var value = parsed[i];
            if (value is null)
            {
                skipped++;
                continue;
            }

            checksum += value.GetValueOrDefault() * (i - skipped);
        }

        return checksum;
    }

    public Result Part2(Parsed parsed)
    {
        return 0;
    }
}
