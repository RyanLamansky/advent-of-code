namespace Advent;

readonly struct Input
{
    static readonly string BasePath;

    static Input()
    {
        var directory = new DirectoryInfo(Environment.CurrentDirectory);
        DirectoryInfo[] matches;
        while ((matches = directory.GetDirectories("Advent")).Length == 0)
            directory = directory.Parent ?? throw new Exception($"Advent directory not found.");

        var advent = matches[0].Parent!;
        BasePath = advent.FullName;
    }

    readonly byte[] raw;

    public Input(string name)
    {
        raw = File.ReadAllBytes(Path.Combine(BasePath, name));
    }

    public Input(string name, string fallbackName)
    {
        var filePath = Path.Combine(BasePath, name);

        if (File.Exists(filePath))
        {
            this.raw = File.ReadAllBytes(filePath);
            return;
        }

        filePath = Path.Combine(BasePath, fallbackName);
        if (File.Exists(filePath))
        {
            this.raw = File.ReadAllBytes(filePath);
            return;
        }

        throw new Exception($"{name} and {fallbackName} not found.");
    }

    public IEnumerable<string> ReadLines()
    {
        using var memory = new MemoryStream(raw);
        using var reader = new StreamReader(memory);

        while (reader.ReadLine() is string line)
            yield return line;
    }
}
