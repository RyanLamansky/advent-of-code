namespace Advent;

/// <summary>
/// Like <see cref="IPuzzle"/>, but with <see cref="string"/> results instead of <see cref="int"/>.
/// </summary>
public interface IPuzzleString : IPuzzle
{
    int IPuzzle.Part1(IEnumerable<string> input) => throw new NotSupportedException();

    int IPuzzle.Part2(IEnumerable<string> input) => throw new NotSupportedException();

    new string Part1(IEnumerable<string> input);

    new string Part2(IEnumerable<string> input);

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

    new public sealed string RunSamplePart1() => Part1(EnumerateLinesFromSampleStream());

    new public sealed string RunSamplePart2() => Part2(EnumerateLinesFromSampleStream());

    new public sealed string RunInputPart1() => Part1(EnumerateLinesFromInputStream());

    new public sealed string RunInputPart2() => Part2(EnumerateLinesFromInputStream());
}
