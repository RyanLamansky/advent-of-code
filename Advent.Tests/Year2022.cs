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

    [TestMethod]
    public void Day08() => new Year2022.Day08.Answer().Test(21, 1713, 8, 268464);

    [TestMethod]
    public void Day09() => new Year2022.Day09.Answer().Test(13, 6486, 1, 2678);

    [TestMethod]
    public void Day10() => new Year2022.Day10.Answer().Test("13140", "16020", """

        ##..##..##..##..##..##..##..##..##..##..
        ###...###...###...###...###...###...###.
        ####....####....####....####....####....
        #####.....#####.....#####.....#####.....
        ######......######......######......####
        #######.......#######.......#######.....
        .
        """, """

        ####..##..####.#..#.####..##..#....###..
        #....#..#....#.#..#....#.#..#.#....#..#.
        ###..#......#..#..#...#..#..#.#....#..#.
        #....#.....#...#..#..#...####.#....###..
        #....#..#.#....#..#.#....#..#.#....#.#..
        ####..##..####..##..####.#..#.####.#..#.
        .
        """);

    [TestMethod]
    public void Day11() => new Year2022.Day11.Answer().Test(10605, 54054, 2713310158, 14314925001);

    [TestMethod]
    public void Day12() => new Year2022.Day12.Answer().Test(31, 484, 29, 478);

    [TestMethod]
    public void Day13() => new Year2022.Day13.Answer().Test(13, 6568, 140, 19493);

    [TestMethod]
    public void Day14() => new Year2022.Day14.Answer().Test(24, 964, 93, 32041);

    [TestMethod]
    public void Day15() => new Year2022.Day15.Answer().Test(26, 5511201, 56000011, 11318723411840);

    [TestMethod]
    public void Day16() => new Year2022.Day16.Answer().Test(0, 0, 0, 0);

    [TestMethod]
    public void Day17() => new Year2022.Day17.Answer().Test(3068L, 3184, 0, 0);

    [TestMethod]
    public void Day18() => new Year2022.Day18.Answer().Test(64, 4474, 58, 2518);

    [TestMethod]
    public void Day19() => new Year2022.Day19.Answer().Test(0, 0, 0, 0);

    [TestMethod]
    public void Day20() => new Year2022.Day20.Answer().Test(3, 5498, 1623178306, 3390007892081);

    [TestMethod]
    public void Day21() => new Year2022.Day21.Answer().Test(152, 276156919469632, 301, 3441198826073);

    [TestMethod]
    public void Day22() => new Year2022.Day22.Answer().Test(6032, 56372, 0, 0);

    [TestMethod]
    public void Day23() => new Year2022.Day23.Answer().Test(110, 3970, 20, 923);

    [TestMethod]
    public void Day24() => new Year2022.Day24.Answer().Test(18, 332, 54, 942);

    [TestMethod]
    public void Day25() => new Year2022.Day25.Answer().Test("2=-1=0", "122-0==-=211==-2-200", "0", "0");

}