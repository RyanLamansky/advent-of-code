namespace Advent.Year2023.Day15;

public sealed class Answer : IPuzzle<int>
{
    static int Hash(string value)
    {
        var current = 0;

        foreach (var c in value)
            current = ((current + c) * 17) % 256;

        return current;
    }

    public int Part1(IEnumerable<string> input) => input.First().Split(',').Select(Hash).Sum();

    public int Part2(IEnumerable<string> input)
    {
        var boxes = Enumerable
            .Range(0, 256)
            .Select(_ => new List<(string Label, int Value)>())
            .ToArray();

        var labelEnd = new char[] { '=', '-' };

        foreach (var step in input.First().Split(','))
        {
            var labelEndIndex = step.IndexOfAny(labelEnd);
            var label = step[..labelEndIndex];
            var box = boxes[Hash(label)];
            var existing = box.FindIndex(si => si.Label == label);

            switch (step[labelEndIndex])
            {
                case '=':
                    var value = int.Parse(step[(labelEndIndex + 1)..]);
                    if (existing >=0)
                    {
                        box[existing] = (label, value);
                        break;
                    }

                    box.Add((label, value));
                    break;
                case '-':
                    if (existing >= 0)
                        box.RemoveAt(existing);
                    break;
            }
        }

        var overallPower = 0;

        for (var boxIndex = 0; boxIndex < boxes.Length; boxIndex++)
        {
            var box = boxes[boxIndex];
            var boxPower = 0;
            for (var labelIndex = 0; labelIndex < box.Count; labelIndex++)
            {
                var lensPower = (boxIndex + 1) * box[labelIndex].Value * (labelIndex + 1);
                boxPower += lensPower;
            }

            overallPower += boxPower;
        }

        return overallPower;
    }
}
