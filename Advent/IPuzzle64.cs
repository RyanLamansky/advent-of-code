namespace Advent;

/// <summary>
/// Like <see cref="IPuzzle"/>, but with <see cref="long"/> results instead of <see cref="int"/>.
/// </summary>
public interface IPuzzle64 : IPuzzle
{
    int IPuzzle.Part1(IEnumerable<string> input) => checked((int)Part1(input));

    int IPuzzle.Part2(IEnumerable<string> input) => checked((int)Part2(input));

    new long Part1(IEnumerable<string> input);

    new long Part2(IEnumerable<string> input);

    private IEnumerable<string> EnumerateLinesFromEmbeddedStream(string name)
    {
        using var stream = this.GetType().Assembly.GetManifestResourceStream(name)
            ?? throw new Exception($"{name} not found.");
        using var reader = new StreamReader(stream);

        while (reader.ReadLine() is string line)
            yield return line;
    }

    private IEnumerable<string> EnumerateLinesFromSampleStream()
        => EnumerateLinesFromEmbeddedStream($"{this.GetType().Namespace}.sample.txt");

    private IEnumerable<string> EnumerateLinesFromInputStream()
        => EnumerateLinesFromEmbeddedStream($"{this.GetType().Namespace}.input.txt");

    new public sealed long RunSamplePart1() => Part1(EnumerateLinesFromSampleStream());

    new public sealed long RunSamplePart2() => Part2(EnumerateLinesFromSampleStream());

    new public sealed long RunInputPart1() => Part1(EnumerateLinesFromInputStream());

    new public sealed long RunInputPart2() => Part2(EnumerateLinesFromInputStream());
}
