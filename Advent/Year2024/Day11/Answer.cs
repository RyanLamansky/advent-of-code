namespace Advent.Year2024.Day11;

using Parsed = string[];
using Result = int;

public sealed class Answer : IPuzzle<Parsed, Result>
{
    public Parsed Parse(IEnumerable<string> input)
        => input.First().Split(' ');

    public Result Part1(Parsed parsed)
    {
        var list = parsed.ToList();

        for (var blink = 1; blink <= 25; blink++)
        {
            var replacementList = new List<string>();

            foreach (var item in list)
            {
                if (item == "0")
                {
                    replacementList.Add("1");
                    continue;
                }

                if (item.Length % 2 == 0)
                {
                    replacementList.Add(item[..(item.Length / 2)]);
                    replacementList.Add(long.Parse(item[(item.Length / 2)..]).ToString());
                    continue;
                }

                replacementList.Add((long.Parse(item) * 2024).ToString());
            }

            list = replacementList;
        }

        return list.Count;
    }

    public Result Part2(Parsed parsed)
    {
        // The algorithm used for part 1 is too inefficient for part 2...
        return 0;
    }
}
