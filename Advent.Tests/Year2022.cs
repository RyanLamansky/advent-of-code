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
}