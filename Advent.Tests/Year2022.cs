namespace Advent;

[TestClass]
public class Year2022Tests
{
    private static void TestPuzzle(
        IPuzzle puzzle,
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

    [TestMethod]
    public void Day01() => TestPuzzle(new Year2022.Day01.Answer(), 24000, 69501, 45000, 202346);

    [TestMethod]
    public void Day02() => TestPuzzle(new Year2022.Day02.Answer(), 15, 12772, 12, 11618);

    [TestMethod]
    public void Day03() => TestPuzzle(new Year2022.Day03.Answer(), 157, 8515, 70, 2434);
}