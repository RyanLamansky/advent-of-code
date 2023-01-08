namespace Advent.Year2022.Day16;

public sealed class Answer : IPuzzle<int>
{
    private sealed class Valve
    {
        public readonly byte FlowRate;
        public readonly Valve[] LeadsTo;

        public Valve(string name, byte flowRate, int leadsToCount)
        {
            Name = name;
            FlowRate = flowRate;
            LeadsTo = new Valve[leadsToCount];
        }

        public string Name { get; }

        public override string ToString() => $"{Name} flow {FlowRate}, to {string.Join(", ", LeadsTo.Select(t => t.Name))}";

        public static Valve[] Parse(IEnumerable<string> input)
        {
            var rawValves = input.Select(line =>
            {
                var name = line.Substring("Valve ".Length, 2);
                var flowRateStart = "Valve AA has flow rate=".Length;
                var flowRateEnd = line.IndexOf(';', flowRateStart);
                var flowRate = byte.Parse(line.AsSpan(flowRateStart, flowRateEnd - flowRateStart));

                var leadsToTextLength = (line.IndexOf(',', flowRateEnd) == -1 ? "; tunnel leads to valve " : "; tunnels lead to valves ").Length;

                var leadsTo = line[(flowRateEnd + leadsToTextLength)..].Split(", ");

                return (Name: name, FlowRate: flowRate, LeadsTo: leadsTo);
            }).ToArray();

            var valvesByName = rawValves.ToDictionary(raw => raw.Name, raw => new Valve(raw.Name, raw.FlowRate, raw.LeadsTo.Length));
            foreach (var (name, _, leadsTo) in rawValves)
            {
                var valve = valvesByName[name];

                for (var i = 0; i < leadsTo.Length; i++)
                    valve.LeadsTo[i] = valvesByName[leadsTo[i]];
            }

            return valvesByName
                .Values
                .OrderBy(valve => Array.FindIndex(rawValves, raw => raw.Name == valve.Name))
                .ToArray();
        }
    }

    public int Part1(IEnumerable<string> input)
    {
        var valves = Valve.Parse(input);

        return 0;
    }

    public int Part2(IEnumerable<string> input)
    {
        var valves = Valve.Parse(input);

        return 0;
    }
}
