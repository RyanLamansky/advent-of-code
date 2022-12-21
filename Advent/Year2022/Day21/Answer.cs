namespace Advent.Year2022.Day21;

public sealed class Answer : IPuzzle64
{
    private sealed class Operation
    {
        public string Left, Right;
        public char Op;

        public Operation(string raw)
        {
            var split = raw.Split(' ');
            Left = split[0];
            Op = split[1][0];
            Right = split[2];
        }

        public override string ToString() => $"{Left} {Op} {Right}";
    }

    public long Part1(IEnumerable<string> input)
    {
        var monkeys = input.Select(line =>
        {
            var name = line[..4];
            var action = line[6..];

            return (name, action);
        })
            .ToDictionary(kv => kv.name, kv => kv.action);

        var solved = monkeys.Where(kv => kv.Value.Length <= 4).ToDictionary(kv => kv.Key, kv => long.Parse(kv.Value));
        var unsolved = monkeys.Where(kv => !solved.ContainsKey(kv.Key)).ToDictionary(kv => kv.Key, kv => new Operation(kv.Value));

        while (!solved.ContainsKey("root"))
        {
            var newlySolved = new HashSet<string>();
            foreach (var kv in unsolved)
            {
                var v = kv.Value;

                if (!solved.TryGetValue(v.Left, out var s1) || !solved.TryGetValue(v.Right, out var s2))
                    continue;

                var answer = v.Op switch
                {
                    '+' => s1 + s2,
                    '-' => s1 - s2,
                    '*' => s1 * s2,
                    '/' => s1 / s2,
                    _ => throw new Exception(),
                };

                solved.Add(kv.Key, answer);
                newlySolved.Add(kv.Key);
            }

            if (newlySolved.Count == 0)
                throw new Exception();

            foreach (var name in newlySolved)
                unsolved.Remove(name);
        }

        return solved["root"];
    }

    public long Part2(IEnumerable<string> input)
    {
        var monkeys = input.Select(line =>
        {
            var name = line[..4];
            var action = line[6..];

            return (name, action);
        })
            .ToDictionary(kv => kv.name, kv => kv.action);

        var solved = monkeys.Where(kv => kv.Value.Length <= 4).ToDictionary(kv => kv.Key, kv => long.Parse(kv.Value));
        var unsolved = monkeys.Where(kv => !solved.ContainsKey(kv.Key)).ToDictionary(kv => kv.Key, kv => new Operation(kv.Value));
        solved.Remove("humn");
        unsolved["root"] = new Operation($"{unsolved["root"].Left} = {unsolved["root"].Right}");

        while (!solved.ContainsKey("humn"))
        {
            var newlySolved = new HashSet<string>();
            foreach (var kv in unsolved)
            {
                var v = kv.Value;

                if (!solved.TryGetValue(v.Left, out var s1) || !solved.TryGetValue(v.Right, out var s2))
                    continue;

                if (v.Op == '=')
                    continue;

                var answer = v.Op switch
                {
                    '+' => s1 + s2,
                    '-' => s1 - s2,
                    '*' => s1 * s2,
                    '/' => s1 / s2,
                    _ => throw new Exception(),
                };

                solved.Add(kv.Key, answer);
                newlySolved.Add(kv.Key);
            }

            if (newlySolved.Count == 0)
                break;

            foreach (var name in newlySolved)
                unsolved.Remove(name);
        }

        var halfSolved = new Dictionary<string, Operation>();
        foreach (var kv in unsolved)
        {
            var part = kv.Value;

            var v = kv.Value;
            if (solved.TryGetValue(v.Left, out var s1))
                part = new Operation($"{s1} {v.Op} {v.Right}");
            if (solved.TryGetValue(v.Right, out var s2))
                part = new Operation($"{v.Left} {v.Op} {s2}");

            halfSolved.Add(kv.Key, part);
        }

        string monkey;
        if (long.TryParse(halfSolved["root"].Left, out var solveFor))
        {
            monkey = halfSolved["root"].Right;
        }
        else
        {
            solveFor = long.Parse(halfSolved["root"].Right);
            monkey = halfSolved["root"].Left;
        }

        do
        {
            var operation = halfSolved[monkey];

            long value;

            if (char.IsDigit(operation.Left[0]))
            {
                value = long.Parse(operation.Left);
                monkey = operation.Right;
                solveFor = operation.Op switch
                {
                    '+' => checked(solveFor - value),
                    '-' => checked(value - solveFor),
                    '*' => checked(solveFor / value),
                    '/' => checked(value / solveFor),
                    _ => throw new Exception(),
                };
            }
            else
            {
                value = long.Parse(operation.Right);
                monkey = operation.Left;
                solveFor = operation.Op switch
                {
                    '+' => checked(solveFor - value),
                    '-' => checked(solveFor + value),
                    '*' => checked(solveFor / value),
                    '/' => checked(solveFor * value),
                    _ => throw new Exception(),
                };
            }
        } while (monkey != "humn");

        return solveFor;
    }
}
