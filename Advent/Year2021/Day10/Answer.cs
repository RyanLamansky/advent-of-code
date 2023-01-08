namespace Advent.Year2021.Day10;

public sealed class Answer : IPuzzle<long>
{
    public long Part1(IEnumerable<string> input)
    {
        var errorScore = 0;
        foreach (var line in input)
        {
            var stack = new Stack<char>();
            foreach (var c in line)
            {
                switch (c)
                {
                    case '(':
                    case '[':
                    case '{':
                    case '<':
                        stack.Push(c);
                        continue;
                }

                static int ValueOf(char c) => c switch
                {
                    ')' => 3,
                    ']' => 57,
                    '}' => 1197,
                    '>' => 25137,
                    _ => throw new Exception()
                };

                stack.TryPop(out char popped);
                switch (popped)
                {
                    case '(' when c == ')':
                    case '[' when c == ']':
                    case '{' when c == '}':
                    case '<' when c == '>':
                        continue;
                }

                errorScore += ValueOf(c);
            }
        }

        return errorScore;
    }

    public long Part2(IEnumerable<string> input)
    {
        var scores = new List<long>();
        foreach (var line in input)
        {
            var stack = new Stack<char>();
            foreach (var c in line)
            {
                switch (c)
                {
                    case '(':
                    case '[':
                    case '{':
                    case '<':
                        stack.Push(c);
                        continue;
                }

                stack.TryPop(out char popped);
                switch (popped)
                {
                    case '(' when c == ')':
                    case '[' when c == ']':
                    case '{' when c == '}':
                    case '<' when c == '>':
                        continue;
                }

                stack.Clear();
                break;
            }

            if (stack.Count == 0)
                continue;

            var score = 0L;

            while (stack.TryPop(out char popped)) checked
            {
                score = score * 5 + popped switch
                {
                    '(' => 1,
                    '[' => 2,
                    '{' => 3,
                    '<' => 4,
                    _ => throw new Exception()
                };
            }

            scores.Add(score);
        }

        scores.Sort();

        return scores[scores.Count / 2];
    }
}
