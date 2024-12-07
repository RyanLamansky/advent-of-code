namespace Advent.Year2024.Day07;

using Line = (ulong TestValue, ulong[] Values);

public sealed class Answer : IPuzzle<Line[], long>
{
    public Line[] Parse(IEnumerable<string> input)
    {
        var sets = new List<Line>();

        foreach (var line in input)
        {
            var parts = line.Split(": ");

            sets.Add((ulong.Parse(parts[0]), parts[1].Split(' ').Select(ulong.Parse).ToArray()));
        }

        return [.. sets];
    }

    public long Part1(Line[] parsed)
    {
        ulong total = 0;

        foreach (var (testValue, values) in parsed)
        {
            var combinations = (int)Math.Pow(2,values.Length - 1);

            for (var combo = 0; combo < combinations; combo++)
            {
                var localTotal = values[0];

                for (byte i = 1; i < values.Length; i++)
                {
                    var value = values[i];

                    localTotal = (combo & (1 << (i - 1))) != 0 ? localTotal + value : localTotal * value;
                }

                if (localTotal != testValue)
                    continue;

                total += localTotal;
                break;
            }
        }

        return checked((long)total);
    }

    static ulong Concatenate(ulong a, ulong b) => b switch
    {
        < 10 => 10 * a + b,
        < 100 => 100 * a + b,
        < 1000 => 1000 * a + b,
        < 10000 => 10000 * a + b,
        < 100000 => 100000 * a + b,
        < 1000000 => 1000000 * a + b,
        < 10000000 => 10000000 * a + b,
        < 100000000 => 100000000 * a + b,
        _ => 1000000000 * a + b // This has enough cases for the problem.
    };

    public long Part2(Line[] parsed) => parsed.AsParallel().Select(line =>
    {
        var (testValue, values) = line;
        var combinations = (int)Math.Pow(4, values.Length - 1);

        const int add = 0b00, mul = 0b01, concat = 0b10, skip = 0b11;

        for (var combo = 0; combo < combinations; combo++)
        {
            var localTotal = values[0];

            for (byte i = 1; i < values.Length; i++)
            {
                var value = values[i];

                var shiftAmount = (((i - 1) * 2));
                var operation = (combo & (0b11 << shiftAmount)) >> shiftAmount;

                if (operation == skip)
                {
                    localTotal = ulong.MaxValue;
                    break;
                }

#pragma warning disable CS8509 // The switch expression does not handle all possible values of its input type (it is not exhaustive).
                localTotal = operation switch
                {
                    add => localTotal + value,
                    mul => localTotal * value,
                    concat => Concatenate(localTotal, value)
                };
#pragma warning restore
            }

            if (localTotal != testValue)
                continue;

            return checked((long)localTotal);
        }

        return 0;
    }).Sum();
}
