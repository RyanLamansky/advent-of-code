namespace Advent.Year2023.Day03;

public sealed class Answer : IPuzzle<int>
{
    static int Digits(int number) => number switch
    {
        < 10 => 1,
        < 100 => 2,
        < 1000 => 3,
        _ => throw new Exception()
    };

    static (((int X, int Y) Origin, int Value)[] Numbers, Dictionary<(int X, int Y), char> Symbols) Parse(IEnumerable<string> input)
    {
        var numbers = new List<((int X, int Y) Origin, int Value)>();
        var symbols = new Dictionary<(int X, int Y), char>();
        var currentNumber = 0;
        var y = -1;

        foreach (var line in input)
        {
            y++;
            var x = -1;

            void FinishCurrentNumber()
            {
                if (currentNumber != 0)
                {
                    numbers.Add(((x - Digits(currentNumber), y), currentNumber));
                    currentNumber = 0;
                }
            }

            foreach (var c in line)
            {
                x++;
                if (char.IsAsciiDigit(c))
                {
                    currentNumber = currentNumber * 10 + (c - '0');
                    continue;
                }

                FinishCurrentNumber();

                if (c == '.')
                    continue;

                symbols.Add((x, y), c);
            }

            x++;
            FinishCurrentNumber();
        }

        return (numbers.ToArray(), symbols);
    }

    public int Part1(IEnumerable<string> input)
    {
        var (numbers, symbols) = Parse(input);
        var sum = 0;

        foreach (var ((x, y), value) in numbers)
        {
            var digits = Digits(value);

            if (
                symbols.ContainsKey((x - 1, y))
                || symbols.ContainsKey((x + digits, y))
                )
            {
                sum += value;
                continue;
            }

            for (var searchY = y - 1; searchY <= y + 1; searchY += 2)
            {
                for (var searchX = x - 1; searchX <= x + digits; searchX++)
                {
                    if (symbols.ContainsKey((searchX, searchY)))
                    {
                        sum += value;
                        goto Found;
                    }
                }
            }

        Found:
            continue;
        }

        return sum;
    }

    public int Part2(IEnumerable<string> input)
    {
        var (numbers, symbols) = Parse(input);
        var candidates = symbols
            .Where(kv => kv.Value == '*')
            .ToDictionary(kv => kv.Key, kv => new List<int>());

        foreach (var ((x, y), value) in numbers)
        {
            var digits = Digits(value);

            if (
                candidates.TryGetValue((x - 1, y), out var list)
                || candidates.TryGetValue((x + digits, y), out list)
                )
            {
                list.Add(value);
                continue;
            }

            for (var searchY = y - 1; searchY <= y + 1; searchY += 2)
            {
                for (var searchX = x - 1; searchX <= x + digits; searchX++)
                {
                    if (candidates.TryGetValue((searchX, searchY), out list))
                    {
                        list.Add(value);
                        goto Found;
                    }
                }
            }

        Found:
            continue;
        }

        return candidates.Values.Where(v => v.Count == 2).Sum(v => v[0] * v[1]);
    }
}
