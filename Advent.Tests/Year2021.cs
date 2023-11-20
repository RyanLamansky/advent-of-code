namespace Advent;

[TestClass]
public class Year2021Tests
{
    [TestMethod]
    public void Day01() => new Year2021.Day01.Answer().Test(7, 1527, 5, 1575);

    [TestMethod]
    public void Day02() => new Year2021.Day02.Answer().Test(150, 1488669, 900, 1176514794);

    [TestMethod]
    public void Day03() => new Year2021.Day03.Answer().Test(198, 3242606, 230, 4856080);

    [TestMethod]
    public void Day04() => new Year2021.Day04.Answer().Test(4512, 2745, 1924, 6594);

    [TestMethod]
    public void Day05() => new Year2021.Day05.Answer().Test(5, 5124, 12, 19771);

    [TestMethod]
    public void Day06() => new Year2021.Day06.Answer().Test(5934, 346063, 26984457539, 1572358335990);

    [TestMethod]
    public void Day07() => new Year2021.Day07.Answer().Test(37, 353800, 168, 98119739);

    [TestMethod]
    public void Day08() => new Year2021.Day08.Answer().Test(26, 392, 61229, 1004688);

    [TestMethod]
    public void Day09() => new Year2021.Day09.Answer().Test(15, 580, 1134, 856716);

    [TestMethod]
    public void Day10() => new Year2021.Day10.Answer().Test(26397, 388713, 288957, 3539961434);

    [TestMethod]
    public void Day11() => new Year2021.Day11.Answer().Test(1656, 1757, 195, 422);

    [TestMethod]
    public void Day12() => new Year2021.Day12.Answer().Test(10, 4495, 36, 131254);
}