namespace Advent.Year2021.Day08;

public sealed class Answer : IPuzzle
{
    public int Part1(IEnumerable<string> input) => input
        .SelectMany(line => line.Split(" | ")[1].Split(' '))
        .Select(value => value.Length)
        .Where(length => length is 2 or 3 or 4 or 7)
        .Count();

    public int Part2(IEnumerable<string> input) => input
        .Select(line =>
        {
            var components = line
                .Split(" | ");

            var combos = components[0]
                .Split(' ')
                .ToHashSet();

            var one = combos.Single(combo => combo.Length == 2);
            combos.Remove(one);

            var four = combos.Single(combo => combo.Length == 4);
            combos.Remove(four);

            var seven = combos.Single(combo => combo.Length == 3);
            combos.Remove(seven);

            var eight = combos.Single(combo => combo.Length == 7);
            combos.Remove(eight);

            var three = combos.Single(combo => combo.Length == 5 && combo.Intersect(seven).Count() == 3);
            combos.Remove(three);

            var five = combos.Single(combo => combo.Length == 5 && combo.Intersect(four).Count() == 3);
            combos.Remove(five);

            var two = combos.Single(combo => combo.Length == 5);
            combos.Remove(two);

            var nine = combos.Single(combo => combo.Intersect(three).Count() == 5);
            combos.Remove(nine);

            var six = combos.Single(combo => combo.Intersect(five).Count() == 5);
            combos.Remove(six);

            var zero = combos.Single();

            var rawValue = components[1].Split(' ');

            var candidates = new[] { zero, one, two, three, four, five, six, seven, eight, nine, };

            var digit0 = Array.IndexOf(candidates, candidates.Single(candidate => candidate.Length == rawValue[0].Length && candidate.Except(rawValue[0]).Count() == 0));
            var digit1 = Array.IndexOf(candidates, candidates.Single(candidate => candidate.Length == rawValue[1].Length && candidate.Except(rawValue[1]).Count() == 0));
            var digit2 = Array.IndexOf(candidates, candidates.Single(candidate => candidate.Length == rawValue[2].Length && candidate.Except(rawValue[2]).Count() == 0));
            var digit3 = Array.IndexOf(candidates, candidates.Single(candidate => candidate.Length == rawValue[3].Length && candidate.Except(rawValue[3]).Count() == 0));

            var number = digit0 * 1000 + digit1 * 100 + digit2 * 10 + digit3;

            return number;
        })
        .Sum();
}
