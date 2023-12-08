namespace Advent.Year2023.Day08;

public sealed class Answer : IPuzzle<int>
{
    static (string Directions, Dictionary<string, (string Left, string Right)> Nodes) Parse(IEnumerable<string> input)
    {
        using var enumerator = input.GetEnumerator();
        enumerator.MoveNext();
        var directions = enumerator.Current;

        enumerator.MoveNext();

        var nodes = new Dictionary<string, (string left, string right)>();

        while (enumerator.MoveNext())
        {
            var current = enumerator.Current;
            nodes.Add(current[..3], (current[7..10], current[12..15]));
        }

        return (directions, nodes); 
    }

    static IEnumerable<T> Loop<T>(IEnumerable<T> source)
    {
        while (true)
            foreach (var item in source)
                yield return item;
    }

    public int Part1(IEnumerable<string> input)
    {
        var (directions, nodes) = Parse(input);

        var destination = "AAA";
        var steps = 0;
        foreach (var direction in Loop(directions))
        {
            steps++;

            var (left, right) = nodes[destination];
            destination = direction == 'L' ? left : right;

            if (destination == "ZZZ")
                return steps;
        }

        throw new Exception();
    }

    public int Part2(IEnumerable<string> input)
    {
        return 0;
    }
}
