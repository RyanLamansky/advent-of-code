namespace Advent.Year2021.Day12;

public sealed class Answer : IPuzzle<int>
{
    private static Dictionary<string, string[]> ParseConnections(IEnumerable<string> input)
    {
        var connections = new Dictionary<string, List<string>>();
        foreach (var raw in input)
        {
            var parts = raw.Split('-');
            var left = parts[0];
            var right = parts[1];

            if (!connections.TryGetValue(left, out var links))
                connections[left] = links = [];

            links.Add(right);

            if (!connections.TryGetValue(right, out links))
                connections[right] = links = [];

            links.Add(left);
        }

        return connections.ToDictionary(kv => kv.Key, kv => kv.Value.ToArray());
    }

    public int Part1(IEnumerable<string> input)
    {
        var connections = ParseConnections(input);

        static int Explore(string current, Dictionary<string, string[]> connections, HashSet<string> visited)
        {
            if (char.IsLower(current[0]))
                visited.Add(current);

            var count = 0;

            foreach (var connection in connections[current])
            {
                if (connection == "end")
                {
                    count++;
                    continue;
                }

                if (!visited.Contains(connection))
                    count += Explore(connection, connections, new HashSet<string>(visited));
            }

            return count;
        }

        return Explore("start", connections, []);
    }

    public int Part2(IEnumerable<string> input)
    {
        var connections = ParseConnections(input);

        static int Explore(string current, Dictionary<string, string[]> connections, HashSet<string> visited, bool twice)
        {
            if (char.IsLower(current[0]))
                visited.Add(current);

            var count = 0;

            foreach (var connection in connections[current])
            {
                if (connection == "end")
                {
                    count++;
                    continue;
                }

                if (connection == "start")
                    continue;

                if (!visited.Contains(connection))
                    count += Explore(connection, connections, new HashSet<string>(visited), twice);
                else if (!twice)
                    count += Explore(connection, connections, new HashSet<string>(visited), true);
            }

            return count;
        }

        return Explore("start", connections, [], false);
    }
}
