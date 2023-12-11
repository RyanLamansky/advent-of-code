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
    public void Day05() => new Year2023.Day05.Answer().Test<uint>(35, 579439039, 46, 7873084);

    [TestMethod]
    public void Day06() => new Year2023.Day06.Answer().Test(288, 2449062, 71503, 33149631);

    [TestMethod]
    public void Day07() => new Year2023.Day07.Answer().Test(6440, 250058342, 5905, 250506580);

    [TestMethod]
    public void Day08() => new Year2023.Day08.Answer().Test(6, 12643, 0, 0);

    [TestMethod]
    public void Day09() => new Year2023.Day09.Answer().Test(114, 2008960228, 2, 1097);

    [TestMethod]
    public void Day10() => new Year2023.Day10.Answer().Test(8, 6860, 0, 0);
}