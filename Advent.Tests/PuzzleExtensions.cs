namespace Advent;

internal static class PuzzleExtensions
{
    public static void Test(
        this IPuzzle puzzle,
        int samplePart1,
        int inputPart1,
        int samplePart2,
        int inputPart2,
        [System.Runtime.CompilerServices.CallerMemberName] string? callerName = null)
    {
        Assert.AreEqual(samplePart1, puzzle.RunSamplePart1(), $"{callerName}.SamplePart1 failed.");
        Assert.AreEqual(inputPart1, puzzle.RunInputPart1(), $"{callerName}.InputPart1 failed.");
        Assert.AreEqual(samplePart2, puzzle.RunSamplePart2(), $"{callerName}.SamplePart2 failed.");
        Assert.AreEqual(inputPart2, puzzle.RunInputPart2(), $"{callerName}.InputPart2 failed.");
    }

    public static void Test(
        this IPuzzle64 puzzle,
        long samplePart1,
        long inputPart1,
        long samplePart2,
        long inputPart2,
        [System.Runtime.CompilerServices.CallerMemberName] string? callerName = null)
    {
        Assert.AreEqual(samplePart1, puzzle.RunSamplePart1(), $"{callerName}.SamplePart1 failed.");
        Assert.AreEqual(inputPart1, puzzle.RunInputPart1(), $"{callerName}.InputPart1 failed.");
        Assert.AreEqual(samplePart2, puzzle.RunSamplePart2(), $"{callerName}.SamplePart2 failed.");
        Assert.AreEqual(inputPart2, puzzle.RunInputPart2(), $"{callerName}.InputPart2 failed.");
    }
}
