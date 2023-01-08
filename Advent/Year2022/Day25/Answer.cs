using System.Text;

namespace Advent.Year2022.Day25;

public sealed class Answer : IPuzzleString
{
    public string Part1(IEnumerable<string> input)
    {
        var multipliers = new long[20];
        multipliers[0] = 1;

        for (var i = 1; i < multipliers.Length; i++)
            multipliers[i] = multipliers[i - 1] * 5;

        long FromSnafu(string snafu)
        {
            var value = 0L;
            for (var lengthRemaining = snafu.Length - 1; lengthRemaining >= 0; lengthRemaining--)
            {
                var digit = snafu[snafu.Length - lengthRemaining - 1];
                var multiplier = multipliers[lengthRemaining];
                var converted = digit switch
                {
                    '2' => 2,
                    '1' => 1,
                    '0' => 0,
                    '-' => -1,
                    '=' => -2,
                    _ => throw new Exception()
                };
                value += multiplier * converted;
            }
            return value;
        }

        static string ToSnafu(long value)
        {
            var result = new StringBuilder();

            do
            {
                var part = value % 5;

                switch (part)
                {
                    case 0:
                        result.Append('0');
                        break;
                    case 1:
                        result.Append('1');
                        break;
                    case 2:
                        result.Append('2');
                        break;
                    case 3:
                        value += 2;
                        result.Append('=');
                        break;
                    case 4:
                        value += 1;
                        result.Append('-');
                        break;
                }
            } while ((value /= 5) > 0);

            return new string(result.ToString().Reverse().ToArray());
        }

        return ToSnafu(input.Select(FromSnafu).Sum());
    }

    public string Part2(IEnumerable<string> input)
    {
        return "0";
    }
}
