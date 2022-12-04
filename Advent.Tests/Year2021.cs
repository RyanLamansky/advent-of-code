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
}