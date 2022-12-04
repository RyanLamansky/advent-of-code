namespace Advent.Year2021.Day04;

public sealed class Answer : IPuzzle
{
    private sealed class Board
    {
        private readonly int[] data;

        public Board(IEnumerator<string> lines)
        {
            data = new int[25];

            for (var i = 0; i < 5; i++)
            {
                var parts = lines.Current.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                for (var part = 0; part < parts.Length; part++)
                {
                    data[i * 5 + part] = int.Parse(parts[part]);
                }

                lines.MoveNext();
            }
        }

        public int this[int x, int y] => data[x * 5 + y];

        public int WinningScore(HashSet<int> usedNumbers)
        {
            var winner = false;

            for (var x = 0; x < 5 && !winner; x++)
            {
                for (var y = 0; y < 5; y++)
                {
                    if (!usedNumbers.Contains(this[x,y]))
                        break;

                    if (y == 4)
                        winner = true;
                }
            }

            if (!winner)
            {
                for (var y = 0; y < 5 && !winner; y++)
                {
                    for (var x = 0; x < 5; x++)
                    {
                        if (!usedNumbers.Contains(this[x, y]))
                            break;

                        if (x == 4)
                            winner = true;
                    }
                }
            }

            if (!winner)
                return -1;

            return this.data.Where(v => !usedNumbers.Contains(v)).Sum();
        }
    }

    public int Part1(IEnumerable<string> input)
    {
        using var lines = input.GetEnumerator();
        lines.MoveNext();

        var numbers = lines.Current.Split(',').Select(int.Parse).ToArray();

        var boards = new List<Board>();
        lines.MoveNext();

        while (lines.MoveNext())
            boards.Add(new Board(lines));

        var usedNumbers = new HashSet<int>();

        do
        {
            var number = numbers[usedNumbers.Count];
            usedNumbers.Add(number);

            if (usedNumbers.Count < 5)
                continue;

            var winningScore = boards.Select(o => o.WinningScore(usedNumbers)).Max();
            if (winningScore >= 0)
                return winningScore * number;
        } while (true);
    }

    public int Part2(IEnumerable<string> input)
    {
        using var lines = input.GetEnumerator();
        lines.MoveNext();

        var numbers = lines.Current.Split(',').Select(int.Parse).ToArray();

        var boards = new HashSet<Board>();
        lines.MoveNext();

        while (lines.MoveNext())
            boards.Add(new Board(lines));

        var usedNumbers = new HashSet<int>();

        do
        {
            var number = numbers[usedNumbers.Count];
            usedNumbers.Add(number);

            if (usedNumbers.Count < 5)
                continue;

            var winners = boards.Where(board => board.WinningScore(usedNumbers) >= 0).ToArray();

            for (var i = 0; i < winners.Length; i++)
            {
                if (boards.Count == 1)
                    return boards.First().WinningScore(usedNumbers) * number;

                boards.Remove(winners[i]);
            }
        } while (true);
    }
}
