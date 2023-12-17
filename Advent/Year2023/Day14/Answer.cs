namespace Advent.Year2023.Day14;

public sealed class Answer : IPuzzle<int>
{
    static (HashSet<(int X, int Y)> RoundRocks, HashSet<(int X, int Y)> CubeRocks) Parse(IEnumerable<string> input)
    {
        HashSet<(int X, int Y)> roundRocks = [], cubeRocks = [];

        var y = -1;
        foreach (var line in input)
        {
            y++;

            for (var x = 0; x < line.Length; x++)
            {
                switch (line[x])
                {
                    case 'O': roundRocks.Add((x, y)); break;
                    case '#': cubeRocks.Add((x, y)); break;

                }
            }
        }

        return (roundRocks, cubeRocks);
    }

    public int Part1(IEnumerable<string> input)
    {
        var (roundRocks, cubeRocks) = Parse(input);

        var maxY = roundRocks.Union(cubeRocks).Max(xy => xy.Y);

        bool movedSomething;
        do
        {
            movedSomething = false;
            foreach (var (x,y) in roundRocks.OrderBy(xy => xy.Y).ToArray())
            {
                var rolledY = y - 1;

                if (rolledY < 0)
                    continue;

                if (cubeRocks.Contains((x, rolledY)))
                    continue;

                if (!roundRocks.Add((x, rolledY)))
                    continue;

                roundRocks.Remove((x, y));
                movedSomething = true;
            }
        } while (movedSomething);

        return roundRocks.Sum(xy => maxY + 1 - xy.Y);
    }

    public int Part2(IEnumerable<string> input)
    {
        return 0;
    }
}
