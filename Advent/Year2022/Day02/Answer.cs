namespace Advent.Year2022.Day02;

internal static class Answer
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

    public static void Solve()
    {
        static IEnumerable<int> XyzIsRps()
        {
            using var input = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("Advent.Year2022.Day02.input.txt");
            using var reader = new StreamReader(input!);

            string? line;
            while ((line = reader.ReadLine()) is not null)
            {
                var opponent = line[0] switch
                {
                    'A' => Choice.Rock,
                    'B' => Choice.Paper,
                    'C' => Choice.Scissors,
                };

                var you = line[2] switch
                {
                    'X' => Choice.Rock,
                    'Y' => Choice.Paper,
                    'Z' => Choice.Scissors,
                };

                var outcome = opponent == you ? Outcome.Draw : opponent switch
                {
                    Choice.Rock => you switch
                    {
                        Choice.Paper => Outcome.Win,
                        Choice.Scissors => Outcome.Loss,
                    },
                    Choice.Paper => you switch
                    {
                        Choice.Rock => Outcome.Loss,
                        Choice.Scissors => Outcome.Win,
                    },
                    Choice.Scissors => you switch
                    {
                        Choice.Rock => Outcome.Win,
                        Choice.Paper => Outcome.Loss,
                    }
                };

                yield return (int)outcome + (int)you;
            }
        }

        var total = XyzIsRps().Sum();

        Console.WriteLine($"Part 1: {total}");

        static IEnumerable<int> XyzIsLdw()
        {
            using var input = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("Advent.Year2022.Day02.input.txt");
            using var reader = new StreamReader(input!);

            string? line;
            while ((line = reader.ReadLine()) is not null)
            {
                var opponent = line[0] switch
                {
                    'A' => Choice.Rock,
                    'B' => Choice.Paper,
                    'C' => Choice.Scissors,
                };

                var outcome = line[2] switch
                {
                    'X' => Outcome.Loss,
                    'Y' => Outcome.Draw,
                    'Z' => Outcome.Win,
                };

                var you = outcome switch
                {
                    Outcome.Draw => opponent,
                    Outcome.Win => opponent switch
                    {
                        Choice.Rock => Choice.Paper,
                        Choice.Paper => Choice.Scissors,
                        Choice.Scissors => Choice.Rock,
                    },
                    Outcome.Loss => opponent switch
                    {
                        Choice.Rock => Choice.Scissors,
                        Choice.Paper => Choice.Rock,
                        Choice.Scissors => Choice.Paper,
                    },
                };

                yield return (int)outcome + (int)you;
            }
        }

        Console.WriteLine($"Part 2: {XyzIsLdw().Sum()}");

    }
}
