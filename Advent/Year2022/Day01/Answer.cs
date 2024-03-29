﻿namespace Advent.Year2022.Day01;

public sealed class Answer : IPuzzle<int>
{
    private static IEnumerable<int> TotalOfGroups(IEnumerable<string> input)
    {
        var currentTotal = -1;
        foreach (var item in input)
        {
            if (item.Length != 0)
            {
                currentTotal += int.Parse(item);
                continue;
            }

            yield return currentTotal;
            currentTotal = 0;
        }

        if (currentTotal != -1)
            yield return currentTotal;
    }

    public int Part1(IEnumerable<string> input) => TotalOfGroups(input).Max();

    public int Part2(IEnumerable<string> input) => TotalOfGroups(input).OrderByDescending(o => o).Take(3).Sum();
}
