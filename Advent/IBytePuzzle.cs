namespace Advent;

public interface IBytePuzzle
{
    public delegate bool TryNextByte(out byte value);

    long Part1(TryNextByte tryNextByte);

    long Part2(TryNextByte tryNextByte);

    private TryNextByte GetByteTryerForEmbeddedResource(string name)
    {
        var stream = this.GetType().Assembly.GetManifestResourceStream(name)
            ?? throw new Exception($"{name} not found.");

        return (out byte value) =>
        {
            var result = stream.ReadByte();
            if (result < 0)
            {
                value = default;
                return false;
            }

            value = (byte)result;
            return true;
        };
    }

    private TryNextByte EnumerateLinesFromSampleStream()
        => GetByteTryerForEmbeddedResource($"{this.GetType().Namespace}.sample.txt");

    private TryNextByte EnumerateLinesFromInputStream()
        => GetByteTryerForEmbeddedResource($"{this.GetType().Namespace}.input.txt");

    public sealed long RunSamplePart1() => Part1(EnumerateLinesFromSampleStream());

    public sealed long RunSamplePart2() => Part2(EnumerateLinesFromSampleStream());

    public sealed long RunInputPart1() => Part1(EnumerateLinesFromInputStream());

    public sealed long RunInputPart2() => Part2(EnumerateLinesFromInputStream());
}
