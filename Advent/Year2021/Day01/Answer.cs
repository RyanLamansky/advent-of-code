namespace Advent.Year2021.Day01;

public sealed class Answer : IPuzzle
{
    public int Part1(IEnumerable<string> input)
    {
        using var enumerator = input.Select(int.Parse).GetEnumerator();
        enumerator.MoveNext();
        var previous = enumerator.Current;

        var incremented = 0;
        while (enumerator.MoveNext())
        {
            if (previous < (previous = enumerator.Current))
                incremented++;
        }

        return incremented;
    }

    public int Part2(IEnumerable<string> input)
    {
        var previous = new int[3];

        using var enumerator = input.Select(int.Parse).GetEnumerator();

        enumerator.MoveNext();
        previous[0] = enumerator.Current;
        enumerator.MoveNext();
        previous[1] = enumerator.Current;
        enumerator.MoveNext();
        previous[2] = enumerator.Current;

        var index = 2;
        var incremented = 0;
        while (enumerator.MoveNext())
        {
            var oldSum = previous.Sum();
            previous[++index % previous.Length] = enumerator.Current;
            var newSum = previous.Sum();

            if (newSum > oldSum)
                incremented++;
        }

        return incremented;
    }
}
