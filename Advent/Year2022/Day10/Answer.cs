using System.Text;

namespace Advent.Year2022.Day10;

public sealed class Answer : IPuzzleString
{
    public string Part1(IEnumerable<string> input)
    {
        var cycle = 1;
        var x = 1;
        var totalStrength = 0;

        static void CheckCycle(int cycle, int x, ref int totalStrength)
        {
            switch (cycle)
            {
                case 20:
                case 60:
                case 100:
                case 140:
                case 180:
                case 220:
                    totalStrength += x * cycle;
                    break;
            }
        }

        foreach (var line in input)
        {
            if (line[0] == 'a')
            {
                var amount = int.Parse(line.AsSpan(5));
                cycle++;
                CheckCycle(cycle, x, ref totalStrength);
                x += amount;
                cycle++;
            }
            else
            {
                cycle++;
            }

            CheckCycle(cycle, x, ref totalStrength);
        }

        return totalStrength.ToString();
    }

    public string Part2(IEnumerable<string> input)
    {
        var cycle = 1;
        var x = 1;
        var builder = new StringBuilder();

        static void CheckPixel(int cycle, int x, StringBuilder builder)
        {
            cycle = (cycle - 1) % 40;
            if (cycle == 0)
                builder.AppendLine();

            if (Math.Abs(cycle - x) <= 1)
                builder.Append('#');
            else
                builder.Append('.');
        }

        CheckPixel(cycle, x, builder);

        foreach (var line in input)
        {
            if (line[0] == 'a')
            {
                var amount = int.Parse(line.AsSpan(5));
                cycle++;
                CheckPixel(cycle, x, builder);

                x += amount;
                cycle++;
            }
            else
            {
                cycle++;
            }

            CheckPixel(cycle, x, builder);
        }

        return builder.ToString();
    }
}
