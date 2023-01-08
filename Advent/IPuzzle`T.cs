﻿namespace Advent;

public interface IPuzzle<T> : IPuzzle
{
    T Part1(IEnumerable<string> input);

    T Part2(IEnumerable<string> input);

    public sealed T RunSamplePart1() => Part1(new Input($"{GetType().Namespace}.sample.txt").ReadLines());

    public sealed T RunSamplePart2() => Part2(new Input($"{GetType().Namespace}.sample.txt").ReadLines());

    public sealed T RunInputPart1() => Part1(new Input($"{GetType().Namespace}.input.txt").ReadLines());

    public sealed T RunInputPart2() => Part2(new Input($"{GetType().Namespace}.input.txt").ReadLines());

    IEnumerable<object?> IPuzzle.Solve()
    {
        yield return RunSamplePart1();
        yield return RunSamplePart2();
        yield return RunInputPart1();
        yield return RunInputPart2();
    }
}
