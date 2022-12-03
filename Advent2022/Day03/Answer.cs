namespace Advent2022.Day03;

internal static class Answer
{
    static int Value(char input) => input switch
    {
        >= 'a' and <= 'z' => input - 'a' + 1,
        >= 'A' and <= 'Z' => input - 'A' + 27,
        _ => throw new Exception(),
    };

    public static void Solve()
    {
        var totalValueOfCommon = 0;

        foreach (var line in EmbeddedResource.EnumerateLines("Advent2022.Day03.input.txt"))
        {
            var compartment1 = line[..(line.Length / 2)];
            var compartment2 = line[compartment1.Length..];
            var commonToBoth = new string(compartment1.Intersect(compartment2).ToArray());
            var valueOfCommon = commonToBoth.Sum(Value);
            totalValueOfCommon += valueOfCommon;
        }

        Console.WriteLine($"Part 1: {totalValueOfCommon}");

        totalValueOfCommon = 0;
        foreach (var group in EmbeddedResource.EnumerateLines("Advent2022.Day03.input.txt").Chunk(3))
        {
            var commonToAll = group[0].Intersect(group[1]).Intersect(group[2]);
            totalValueOfCommon += commonToAll.Sum(Value);
        }

        Console.WriteLine($"Part 2: {totalValueOfCommon}");
    }
}
