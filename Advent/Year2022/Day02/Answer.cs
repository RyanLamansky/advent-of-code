namespace Advent.Year2022.Day02;

public sealed class Answer : IPuzzle
{
    enum Choice
    {
        Rock = 1,
        Paper = 2,
        Scissors = 3,
    }

    enum Outcome
    {
        Loss = 0,
        Draw = 3,
        Win = 6,
    }

    static IEnumerable<int> XyzIsRps(IEnumerable<string> input)
    {
        foreach (var line in input)
        {
            var opponent = line[0] switch
            {
                'A' => Choice.Rock,
                'B' => Choice.Paper,
                'C' => Choice.Scissors,
                _ => throw new Exception()
            };

            var you = line[2] switch
            {
                'X' => Choice.Rock,
                'Y' => Choice.Paper,
                'Z' => Choice.Scissors,
                _ => throw new Exception()
            };

            var outcome = opponent == you ? Outcome.Draw : opponent switch
            {
                Choice.Rock => you switch
                {
                    Choice.Paper => Outcome.Win,
                    Choice.Scissors => Outcome.Loss,
                    _ => throw new Exception()
                },
                Choice.Paper => you switch
                {
                    Choice.Rock => Outcome.Loss,
                    Choice.Scissors => Outcome.Win,
                    _ => throw new Exception()
                },
                Choice.Scissors => you switch
                {
                    Choice.Rock => Outcome.Win,
                    Choice.Paper => Outcome.Loss,
                    _ => throw new Exception()
                },
                _ => throw new Exception()
            };

            yield return (int)outcome + (int)you;
        }
    }

    static IEnumerable<int> XyzIsLdw(IEnumerable<string> input)
    {
        foreach (var line in input)
        {
            var opponent = line[0] switch
            {
                'A' => Choice.Rock,
                'B' => Choice.Paper,
                'C' => Choice.Scissors,
                _ => throw new Exception()
            };

            var outcome = line[2] switch
            {
                'X' => Outcome.Loss,
                'Y' => Outcome.Draw,
                'Z' => Outcome.Win,
                _ => throw new Exception()
            };

            var you = outcome switch
            {
                Outcome.Draw => opponent,
                Outcome.Win => opponent switch
                {
                    Choice.Rock => Choice.Paper,
                    Choice.Paper => Choice.Scissors,
                    Choice.Scissors => Choice.Rock,
                    _ => throw new Exception()
                },
                Outcome.Loss => opponent switch
                {
                    Choice.Rock => Choice.Scissors,
                    Choice.Paper => Choice.Rock,
                    Choice.Scissors => Choice.Paper,
                    _ => throw new Exception()
                },
                _ => throw new Exception()
            };

            yield return (int)outcome + (int)you;
        }
    }

    public int Part1(IEnumerable<string> input) => XyzIsRps(input).Sum();

    public int Part2(IEnumerable<string> input) => XyzIsLdw(input).Sum();
}
