namespace Advent;

internal static class PuzzleExtensions
{
    public static void Test<T>(
        this IPuzzle<T> puzzle,
        T samplePart1,
        T inputPart1,
        T samplePart2,
        T inputPart2,
        [System.Runtime.CompilerServices.CallerMemberName] string? callerName = null)
    {
        Assert.AreEqual(samplePart1, puzzle.RunSamplePart1(), $"{callerName}.SamplePart1 failed.");
        Assert.AreEqual(inputPart1, puzzle.RunInputPart1(), $"{callerName}.InputPart1 failed.");
        Assert.AreEqual(samplePart2, puzzle.RunSamplePart2(), $"{callerName}.SamplePart2 failed.");
        Assert.AreEqual(inputPart2, puzzle.RunInputPart2(), $"{callerName}.InputPart2 failed.");
    }
}
