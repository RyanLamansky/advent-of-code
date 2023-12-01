namespace Advent;

readonly struct Input
{
    readonly byte[] raw;

    public Input(string name)
    {
        using var stream = GetType().Assembly.GetManifestResourceStream(name)
            ?? throw new Exception($"{name} not found.");

        var raw = this.raw = new byte[stream.Length];

        stream.ReadExactly(raw.AsSpan());
    }

    public Input(string name, string fallbackName)
    {
        var assembly = GetType().Assembly;
        using var stream = assembly.GetManifestResourceStream(name)
            ?? assembly.GetManifestResourceStream(fallbackName)
            ?? throw new Exception($"{name} and {fallbackName} not found.");

        var raw = this.raw = new byte[stream.Length];

        stream.ReadExactly(raw.AsSpan());
    }

    public IEnumerable<string> ReadLines()
    {
        using var memory = new MemoryStream(raw);
        using var reader = new StreamReader(memory);

        while (reader.ReadLine() is string line)
            yield return line;
    }
}
