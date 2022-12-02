namespace Advent2022.Day01;

internal static class Answer
{
    public static void Solve()
    {
        static IEnumerable<int[]> Input()
        {
            using var input = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("Advent2022.Day01.input.txt");
            using var reader = new StreamReader(input!);

            var lines = new List<int>();

            string? line;
            while ((line = reader.ReadLine()) is not null)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    lines.Add(int.Parse(line));
                    continue;
                }

                if (lines.Count != 0)
                {
                    yield return lines.ToArray();
                    lines.Clear();
                }
            }

            if (lines.Count != 0)
                yield return lines.ToArray();
        }

        static int[][] GetLines()
        {
            var groups = new List<int[]>();
            foreach (var group in Input())
            {
                Console.WriteLine(group.Length);
                groups.Add(group);
            }
            return groups.ToArray();
        }

        var lines = GetLines();

        var maxes = lines.Select(o => o.Sum()).ToArray();

        var maxofmaxes = maxes.Max();

        Console.WriteLine($"Part 1: {maxofmaxes}");

        var threeBiggestMaxes = maxes.OrderByDescending(o => o).Take(3).ToArray();

        var topThreeSum = threeBiggestMaxes.Sum();

        Console.WriteLine($"Part 2: {topThreeSum}");
    }
}
