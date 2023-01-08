namespace Advent.Year2022.Day11;

public sealed class Answer : IPuzzle<long>
{
    private sealed class Monkey
    {
        public readonly Queue<int> Items;
        public readonly Func<long, long> Operation;
        public readonly int Test, IfTrue, IfFalse;
        public int Inspections;

        public Monkey(IEnumerator<string> lines)
        {
            lines.MoveNext(); // Monkey number is implied.

            this.Items = new Queue<int>(lines.Current["  Starting items: ".Length..].Split(", ").Select(int.Parse));
            lines.MoveNext();

            var rawOperation = lines.Current["  Operation: new = old ".Length..];

            Func<long, long, long> op = rawOperation[0] switch
            {
                '*' => (left, right) => checked(left * right),
                '+' => (left, right) => checked(left + right),
                _ => throw new Exception(),
            };
            Func<long, long> operand;
            if (rawOperation[2..] == "old")
                operand = old => old;
            else
            {
                var constant = int.Parse(rawOperation[2..]);
                operand = _ => constant;
            }
            this.Operation = old => op(old, operand(old));
            lines.MoveNext();

            this.Test = int.Parse(lines.Current["  Test: divisible by ".Length..]);
            lines.MoveNext();

            this.IfTrue = int.Parse(lines.Current["    If true: throw to monkey ".Length..]);
            lines.MoveNext();

            this.IfFalse = int.Parse(lines.Current["    If false: throw to monkey ".Length..]);
            lines.MoveNext();
        }

        public static IEnumerable<Monkey> Parse(IEnumerable<string> input)
        {
            using var lines = input.GetEnumerator();

            while (lines.MoveNext())
                yield return new Monkey(lines);
        }

        public void RunTurn(Monkey[] monkeys)
        {
            while (Items.TryDequeue(out var item))
            {
                Inspections++;

                var operated = Operation(item);
                var divided = checked((int)(operated / 3));
                if (divided % Test == 0)
                    monkeys[IfTrue].Items.Enqueue(divided);
                else
                    monkeys[IfFalse].Items.Enqueue(divided);
            }
        }

        public void RunTurn2(Monkey[] monkeys, int modulus)
        {
            while (Items.TryDequeue(out var item))
            {
                Inspections++;

                var operated = checked((int)(Operation(item) % modulus));
                if (operated % Test == 0)
                    monkeys[IfTrue].Items.Enqueue(operated);
                else
                    monkeys[IfFalse].Items.Enqueue(operated);
            }
        }

        public override string ToString() => $"{Inspections}; {string.Join(", ", Items)}";
    }

    public long Part1(IEnumerable<string> input)
    {
        var monkeys = Monkey.Parse(input).ToArray();

        for (var round = 1; round <= 20; round++)
            foreach (var monkey in monkeys)
                monkey.RunTurn(monkeys);

        var monkeyBusiness = monkeys
            .OrderByDescending(monkey => monkey.Inspections)
            .Take(2)
            .Aggregate(1, (total, monkey) => total *= monkey.Inspections);

        return monkeyBusiness;
    }

    public long Part2(IEnumerable<string> input)
    {
        var monkeys = Monkey.Parse(input).ToArray();
        var modulus = monkeys.Aggregate(1, (accumulation, monkey) => accumulation *= monkey.Test);

        for (var round = 1; round <= 10000; round++)
            foreach (var monkey in monkeys)
                monkey.RunTurn2(monkeys, modulus);

        var monkeyBusiness = monkeys
            .OrderByDescending(monkey => monkey.Inspections)
            .Take(2)
            .Aggregate(1L, (total, monkey) => total *= monkey.Inspections);

        return monkeyBusiness;
    }
}
