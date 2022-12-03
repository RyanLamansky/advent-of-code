namespace Advent;

public interface IPuzzle
{
    long Part1(IEnumerable<string> input);

    long Part2(IEnumerable<string> input);

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

    public sealed long RunSamplePart1() => Part1(EnumerateLinesFromSampleStream());

    public sealed long RunSamplePart2() => Part2(EnumerateLinesFromSampleStream());

    public sealed long RunInputPart1() => Part1(EnumerateLinesFromInputStream());

    public sealed long RunInputPart2() => Part2(EnumerateLinesFromInputStream());
}
