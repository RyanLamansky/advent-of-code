namespace Advent;

[TestClass]
public class Year2022Tests
{
    [TestMethod]
    public void Day01() => new Year2022.Day01.Answer().Test(24000, 69501, 45000, 202346);

    [TestMethod]
    public void Day02() => new Year2022.Day02.Answer().Test(15, 12772, 12, 11618);

    [TestMethod]
    public void Day03() => new Year2022.Day03.Answer().Test(157, 8515, 70, 2434);

    [TestMethod]
    public void Day04() => new Year2022.Day04.Answer().Test(2, 485, 4, 857);

    [TestMethod]
    public void Day05() => new Year2022.Day05.Answer().Test("CMZ", "DHBJQJCCW", "MCD", "WJVRLSJJT");

    [TestMethod]
    public void Day06() => new Year2022.Day06.Answer().Test(5, 1578, 23, 2178);

    [TestMethod]
    public void Day07() => new Year2022.Day07.Answer().Test(94853, 82422, 24933642, 10096985);
}