using System.Text;

namespace Advent.Year2022.Day05;

public sealed class Answer : IPuzzleString
{
    private static string Solve(IEnumerable<string> lines, bool takeSingles)
    {
        using var enumerator = lines.GetEnumerator();
        enumerator.MoveNext();

        var line = enumerator.Current;
        var rawStacks = new List<char>[(line.Length + 1) / 4];
        for (var i = 0; i < rawStacks.Length; i++)
            rawStacks[i] = new List<char>();

        do
        {
            for (var i = 1; i < line.Length; i += 4)
            {
                if (line[i] != ' ')
                    rawStacks[(i - 1) / 4].Add(line[i]);

            }
        } while (enumerator.MoveNext() && !char.IsDigit((line = enumerator.Current)[1]));

        enumerator.MoveNext();

        var stacks = new Stack<char>[rawStacks.Length];

        for (var i = 0; i < rawStacks.Length; i++)
        {
            var rawStack = rawStacks[i];
            rawStack.Reverse();

            var stack = stacks[i] = new Stack<char>();

            foreach (var item in rawStack)
                stack.Push(item);
        }

        while (enumerator.MoveNext())
        {
            var parts = enumerator.Current.Split(' ');
            var amount = int.Parse(parts[1]);
            var source = stacks[int.Parse(parts[3]) - 1];
            var destination = stacks[int.Parse(parts[5]) - 1];

            if (takeSingles)
            {
                while (--amount >= 0)
                    destination.Push(source.Pop());
            }
            else
            {
                var temp = new char[amount];
                while (--amount >= 0)
                    temp[amount] = source.Pop();

                for (var i = 0; i < temp.Length; i++)
                    destination.Push(temp[i]);
            }
        }

        var tops = new StringBuilder();
        foreach (var stack in stacks)
            tops.Append(stack.Peek());

        return tops.ToString();
    }

    public string Part1(IEnumerable<string> input) => Solve(input, true);

    public string Part2(IEnumerable<string> input) => Solve(input, false);
}
