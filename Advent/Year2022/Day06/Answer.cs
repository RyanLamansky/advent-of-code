using System.Drawing;

namespace Advent.Year2022.Day06;

public sealed class Answer : IPuzzle
{
    private static bool IsDistinct(ReadOnlySpan<char> chars)
    {
        for (var i = 0; i < chars.Length; i++)
        {
            var c = chars[i];

            for (var j = 1; j < chars.Length - 1; j++)
            {
                if (c == chars[(i + j) % chars.Length])
                    return false;
            }
        }

        return true;
    }

    private static int Solve(IEnumerable<string> input, int size)
    {
        var line = input.First().AsSpan();

        for (var i = size; i < line.Length; i++)
        {
            if (IsDistinct(line.Slice(i - size, size)))
                return i;
        }

        throw new Exception();
    }

    public int Part1(IEnumerable<string> input) => Solve(input, 4);

    public int Part2(IEnumerable<string> input) =>  Solve(input, 14);
}
