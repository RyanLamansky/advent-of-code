namespace Advent.Year2022.Day03;

public sealed class Answer : IPuzzle
{
    static int Value(char input) => input switch
    {
        >= 'a' and <= 'z' => input - 'a' + 1,
        >= 'A' and <= 'Z' => input - 'A' + 27,
        _ => throw new Exception(),
    };

    public long Part1(IEnumerable<string> input)
    {
        var totalValueOfCommon = 0;

        foreach (var line in input)
        {
            var compartment1 = line[..(line.Length / 2)];
            var compartment2 = line[compartment1.Length..];
            var commonToBoth = new string(compartment1.Intersect(compartment2).ToArray());
            var valueOfCommon = commonToBoth.Sum(Value);
            totalValueOfCommon += valueOfCommon;
        }

        return totalValueOfCommon;
    }

    public long Part2(IEnumerable<string> input)
    {
        var totalValueOfCommon = 0;

        foreach (var group in input.Chunk(3))
        {
            var commonToAll = group[0].Intersect(group[1]).Intersect(group[2]);
            totalValueOfCommon += commonToAll.Sum(Value);
        }

        return totalValueOfCommon;
    }
}
