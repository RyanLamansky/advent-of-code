namespace Advent;

public interface IPuzzle
{
    int Part1(IEnumerable<string> input);

    int Part2(IEnumerable<string> input);

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

    public sealed int RunSamplePart1() => Part1(EnumerateLinesFromSampleStream());

    public sealed int RunSamplePart2() => Part2(EnumerateLinesFromSampleStream());

    public sealed int RunInputPart1() => Part1(EnumerateLinesFromInputStream());

    public sealed int RunInputPart2() => Part2(EnumerateLinesFromInputStream());
}
