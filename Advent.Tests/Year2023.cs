namespace Advent;

[TestClass]
public class Year2023Tests
{
    [TestMethod]
    public void Day01() => new Year2023.Day01.Answer().Test(142, 57346, 281, 57345);

    [TestMethod]
    public void Day02() => new Year2023.Day02.Answer().Test(8, 2449, 2286, 63981);
}