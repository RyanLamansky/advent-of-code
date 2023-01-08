namespace Advent.Year2022.Day20;

public sealed class Answer : IPuzzle<long>
{
    sealed class Number
    {
        public readonly long Value;

        public Number(long value) => Value = value;

        public override string ToString() => Value.ToString();

        public static implicit operator long(Number value) => value.Value;
    }

    static void Mix(Number[] original, Number[] mixed)
    {
        var endIndex = original.Length - 1;
        foreach (var amount in original)
        {
            var oldIndex = Array.IndexOf(mixed, amount);
            var newIndex = (oldIndex + amount) % endIndex;

            if (newIndex <= 0 && oldIndex + amount != 0)
                newIndex += endIndex;

            var item = mixed[oldIndex];

            if (newIndex > oldIndex)
                Array.Copy(mixed, oldIndex + 1, mixed, oldIndex, newIndex - oldIndex);
            else
                Array.Copy(mixed, newIndex, mixed, newIndex + 1, oldIndex - newIndex);

            mixed[newIndex] = item;
        }
    }

    public long Part1(IEnumerable<string> input)
    {
        var original = input.Select(raw => new Number(int.Parse(raw))).ToArray();
        var mixed = original.ToArray();

        Mix(original, mixed);

        var zeroIndex = Array.IndexOf(mixed, mixed.First(x => x == 0));

        var a = mixed[(1000 + zeroIndex) % mixed.Length];
        var b = mixed[(2000 + zeroIndex) % mixed.Length];
        var c = mixed[(3000 + zeroIndex) % mixed.Length];
        var sum = a + b + c;

        return sum;
    }

    public long Part2(IEnumerable<string> input)
    {
        var original = input.Select(raw => new Number(int.Parse(raw) * 811589153L)).ToArray();
        var mixed = original.ToArray();

        for (var i = 0; i < 10; i++)
            Mix(original, mixed);

        var zeroIndex = Array.IndexOf(mixed, mixed.First(x => x == 0));

        var a = mixed[(1000 + zeroIndex) % mixed.Length];
        var b = mixed[(2000 + zeroIndex) % mixed.Length];
        var c = mixed[(3000 + zeroIndex) % mixed.Length];
        var sum = a + b + c;

        return sum;
    }
}
