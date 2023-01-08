namespace Advent.Year2022.Day19;

public sealed class Answer : IPuzzle<int>
{
    private readonly struct Blueprint
    {
        public readonly byte
            OreRobotOreCost,
            ClayRobotOreCost,
            ObsidianRobotOreCost,
            ObsidianRobotClayCost,
            GeodeRobotOreCost,
            GeodeRobotObsidianCost;

        public Blueprint(string line)
        {
            var startIndex = line.IndexOf("costs ") + "costs ".Length;
            OreRobotOreCost = byte.Parse(line.AsSpan(startIndex, line.IndexOf(' ', startIndex + 1) - startIndex));

            startIndex = line.IndexOf("costs ", startIndex + 3) + "costs ".Length;
            ClayRobotOreCost = byte.Parse(line.AsSpan(startIndex, line.IndexOf(' ', startIndex + 1) - startIndex));

            startIndex = line.IndexOf("costs ", startIndex + 3) + "costs ".Length;
            ObsidianRobotOreCost = byte.Parse(line.AsSpan(startIndex, line.IndexOf(' ', startIndex + 1) - startIndex));

            startIndex = line.IndexOf("and ", startIndex + 3) + "and ".Length;
            ObsidianRobotClayCost = byte.Parse(line.AsSpan(startIndex, line.IndexOf(' ', startIndex + 1) - startIndex));

            startIndex = line.IndexOf("costs ", startIndex + 3) + "costs ".Length;
            GeodeRobotOreCost = byte.Parse(line.AsSpan(startIndex, line.IndexOf(' ', startIndex + 1) - startIndex));

            startIndex = line.IndexOf("and ", startIndex + 3) + "and ".Length;
            GeodeRobotObsidianCost = byte.Parse(line.AsSpan(startIndex, line.IndexOf(' ', startIndex + 1) - startIndex));
        }

        public static Blueprint[] ReadInput(IEnumerable<string> input) => input.Select(line => new Blueprint(line)).ToArray();

        public override string ToString() =>
            $"{OreRobotOreCost}, {ClayRobotOreCost}, {ObsidianRobotOreCost}, {ObsidianRobotClayCost}, {GeodeRobotOreCost}, {GeodeRobotObsidianCost}";
    }

    public int Part1(IEnumerable<string> input)
    {
        var bluePrints = Blueprint.ReadInput(input);

        return 0;
    }

    public int Part2(IEnumerable<string> input)
    {
        return 0;
    }
}
