namespace Advent;

[TestClass]
public class Year2023Tests
{
    [TestMethod]
    public void Day01() => new Year2023.Day01.Answer().Test(142, 57346, 281, 57345);

    [TestMethod]
    public void Day02() => new Year2023.Day02.Answer().Test(8, 2449, 2286, 63981);

    [TestMethod]
    public void Day03() => new Year2023.Day03.Answer().Test(4361, 530849, 467835, 84900879);

    [TestMethod]
    public void Day04() => new Year2023.Day04.Answer().Test(13, 26426, 30, 6227972);

    [TestMethod]
    public void Day05() => new Year2023.Day05.Answer().Test<uint>(35, 579439039, 0, 0);
}