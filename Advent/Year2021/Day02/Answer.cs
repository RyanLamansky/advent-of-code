namespace Advent.Year2021.Day02;

public sealed class Answer : IPuzzle
{
    public int Part1(IEnumerable<string> input)
    {
        var horizontal = 0;
        var depth = 0;

        foreach (var command in input)
        {
            var parts = command.Split(' ');
            var amount = int.Parse(parts[1]);

            switch (parts[0])
            {
                case "forward": horizontal += amount; break;
                case "down": depth += amount; break;
                case "up": depth -= amount; break;
            }
        }

        return horizontal * depth;
    }

    public int Part2(IEnumerable<string> input)
    {
        var horizontal = 0;
        var depth = 0;
        var aim = 0;

        foreach (var command in input)
        {
            var parts = command.Split(' ');
            var amount = int.Parse(parts[1]);

            switch (parts[0])
            {
                case "forward":
                    horizontal += amount;
                    depth += amount * aim;
                    break;
                case "down": aim += amount; break;
                case "up": aim -= amount; break;
            }
        }

        return horizontal * depth;
    }
}
