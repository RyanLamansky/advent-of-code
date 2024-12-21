namespace Advent;

public interface IPuzzle<T> : IPuzzle
{
    T Part1(IEnumerable<string> input);

    T Part2(IEnumerable<string> input);

    public sealed T RunSamplePart1() => Part1(new Input($"{GetType().Namespace!.Replace('.', '/')}/sample.txt").ReadLines());

    public sealed T RunSamplePart2() => Part2(new Input(
        $"{GetType().Namespace!.Replace('.', '/')}/sample2.txt",
        $"{GetType().Namespace!.Replace('.', '/')}/sample.txt").ReadLines()
        );

    public sealed T RunInputPart1() => Part1(new Input($"{GetType().Namespace!.Replace('.', '/')}/input.txt").ReadLines());

    public sealed T RunInputPart2() => Part2(new Input($"{GetType().Namespace!.Replace('.', '/')}/input.txt").ReadLines());

    IEnumerable<object?> IPuzzle.Solve()
    {
        yield return RunSamplePart1();
        yield return RunInputPart1();
        yield return RunSamplePart2();
        yield return RunInputPart2();
    }
}

public interface IPuzzle<TParsed, TResult> : IPuzzle<TResult>
{
    TParsed Parse(IEnumerable<string> input);

    TResult Part1(TParsed parsed);

    TResult Part2(TParsed parsed);

    TResult IPuzzle<TResult>.Part1(IEnumerable<string> input) => Part1(Parse(input));

    TResult IPuzzle<TResult>.Part2(IEnumerable<string> input) => Part2(Parse(input));
}
