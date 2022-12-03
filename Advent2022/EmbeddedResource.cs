using System.Reflection;

namespace Advent2022;

internal static class EmbeddedResource
{
    public static IEnumerable<string> EnumerateLines(string name)
    {
        using var input = Assembly.GetExecutingAssembly().GetManifestResourceStream(name) ?? throw new Exception();
        using var reader = new StreamReader(input);

        string? line;
        while ((line = reader.ReadLine()) is not null)
            yield return line;
    }
}
