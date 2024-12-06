namespace Advent;

[TestClass]
public class Year2024Tests
{
    [TestMethod]
    public void Day01() => new Year2024.Day01.Answer().Test(11, 3714264, 31, 18805872);

    [TestMethod]
    public void Day02() => new Year2024.Day02.Answer().Test(2, 411, 4, 465);

    [TestMethod]
    public void Day03() => new Year2024.Day03.Answer().Test(161, 173731097, 48, 93729253);

    [TestMethod]
    public void Day04() => new Year2024.Day04.Answer().Test(18, 2534, 9, 1866);

    [TestMethod]
    public void Day05() => new Year2024.Day05.Answer().Test(143, 5129, 123, 4077);

    [TestMethod]
    public void Day06() => new Year2024.Day06.Answer().Test(41, 4826, 6, 1721);
}