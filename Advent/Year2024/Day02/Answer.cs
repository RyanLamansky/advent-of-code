namespace Advent.Year2024.Day02;

public sealed class Answer : IPuzzle<int>
{
    private static int[][] Parse(IEnumerable<string> input)
        => input.Select(line => line.Split(' ').Select(value => int.Parse(value)).ToArray()).ToArray();

    public int Part1(IEnumerable<string> input)
    {
        var reports = Parse(input);

        var isSafe = reports.Select(report =>
        {
            var minUp = int.MaxValue;
            var maxUp = 0;
            var minDown = int.MaxValue;
            var maxDown = 0;

            var previous = report[0];

            foreach (var level in report.AsSpan(1))
            {
                var difference = level - previous;
                if (difference == 0)
                    return false;

                if (difference > 0)
                {
                    minUp = Math.Min(minUp, difference);
                    maxUp = Math.Max(maxUp, difference);
                }
                else
                {
                    difference *= -1;
                    minDown = Math.Min(minDown, difference);
                    maxDown = Math.Max(maxDown, difference);
                }
                previous = level;
            }

            return (maxDown == 0 && minUp >= 1 && maxUp <= 3) || (maxUp == 0 && minDown >= 1 && maxDown <= 3);
        }).ToArray();

        return isSafe.Count(v => v);
    }

    public int Part2(IEnumerable<string> input)
    {
        var reports = Parse(input);

        var isSafe = reports.Select(fullReportRaw =>
        {
            for (var i = 0; i < fullReportRaw.Length; i++)
            {
                var report = new List<int>(fullReportRaw);
                report.RemoveAt(i);

                var minUp = int.MaxValue;
                var maxUp = 0;
                var minDown = int.MaxValue;
                var maxDown = 0;

                var previous = report[0];

                foreach (var level in report[1..])
                {
                    var difference = level - previous;
                    if (difference == 0)
                    {
                        maxDown = int.MaxValue;
                        break;
                    }

                    if (difference > 0)
                    {
                        minUp = Math.Min(minUp, difference);
                        maxUp = Math.Max(maxUp, difference);
                    }
                    else
                    {
                        difference *= -1;
                        minDown = Math.Min(minDown, difference);
                        maxDown = Math.Max(maxDown, difference);
                    }
                    previous = level;
                }

                if ((maxDown == 0 && minUp >= 1 && maxUp <= 3) || (maxUp == 0 && minDown >= 1 && maxDown <= 3))
                    return true;
            }

            return false;
        }).ToArray();

        return isSafe.Count(v => v);
    }
}
