namespace Advent.Year2024.Day03;

public sealed class Answer : IPuzzle<object[], long>
{
    public object[] Parse(IEnumerable<string> input)
    {
        static IEnumerable<object> Parse(IEnumerable<string> input)
        {
            bool? nextEnabled = null;
            foreach (var line in input)
            {
                var candidates = line.Split("mul(");

                foreach (var candidate in candidates)
                {
                    if (nextEnabled is bool nextEnabledReal)
                    {
                        yield return nextEnabledReal;
                        nextEnabled = null;
                    }

                    var doIndex = candidate.IndexOf("do()");
                    var dontIndex = candidate.IndexOf("don't()");

                    if (doIndex > 0)
                        nextEnabled = true;
                    if (dontIndex > doIndex)
                        nextEnabled = false;

                    var commaIndex = candidate.IndexOf(',');
                    if (commaIndex < 0)
                        continue;

                    var closeIndex = candidate.IndexOf(')', commaIndex);
                    if (closeIndex < 0)
                        continue;

                    if (!int.TryParse(candidate.AsSpan(0, commaIndex), out var left))
                        continue;
                    if (!int.TryParse(candidate.AsSpan(commaIndex + 1, closeIndex - commaIndex - 1), out var right))
                        continue;

                    yield return left * right;
                }
            }
        }

        return Parse(input).ToArray();
    }

    public long Part1(object[] parsed)
    {
        var total = parsed.OfType<int>().Sum(value => (long)value);
        return total;
    }

    public long Part2(object[] parsed)
    {
        var total = 0L;
        var enabled = true;

        foreach (var value in parsed)
        {
            if (value is bool changeEnabled)
                enabled = changeEnabled;

            if (!enabled)
                continue;

            if (value is int mul)
                total += mul;
        }

        return total;
    }
}
