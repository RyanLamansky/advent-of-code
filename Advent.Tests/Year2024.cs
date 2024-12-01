namespace Advent;

[TestClass]
public class Year2024Tests
{
    [TestMethod]
    public void Day01() => new Year2024.Day01.Answer().Test(11, 3714264, 31, 18805872);
}