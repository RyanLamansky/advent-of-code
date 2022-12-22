namespace Advent.Year2022.Day22;

enum Facing
{
    Right,
    Down,
    Left,
    Up,
}

static class Extensions
{
    public static char[,] ToBoard(this string[] lines)
    {
        var width = lines.Max(line => line.Length);
        var result = new char[width, lines.Length];

        for (var y = 0; y < lines.Length; y++)
        {
            var line = lines[y].PadRight(width);
            for (var x = 0; x < line.Length; x++)
                result[x, y] = line[x];
        }

        return result;
    }

    public static IEnumerable<char> EnumerateX(this char[,] board, int y)
    {
        for (var x = 0; x < board.GetLength(0); x++)
            yield return board[x, y];
    }

    public static IEnumerable<char> EnumerateY(this char[,] board, int x)
    {
        for (var y = 0; y < board.GetLength(1); y++)
            yield return board[x, y];
    }

    public static IEnumerable<(int X, int Y, int NewX)> RightMoves(this char[,] board)
    {
        for (var y = 0; y < board.GetLength(1); y++)
        {
            var row = new string(board.EnumerateX(y).ToArray());
            var startChar = row.First(c => c != ' ');
            var startPos = row.IndexOf(startChar);
            var endChar = row.Reverse().First(c => c != ' ');
            var endPos = row.LastIndexOf(endChar);

            for (var x = startPos; x <= endPos; x++)
            {
                var c = row[x];
                if (c == '#')
                    continue;

                if (x == endPos)
                {
                    yield return (x, y, startChar != '#' ? startPos : x);
                    continue;
                }

                yield return (x, y, row[x + 1] != '#' ? x + 1 : x);
            }
        }
    }

    public static IEnumerable<(int X, int Y, int NewX)> LeftMoves(this char[,] board)
    {
        for (var y = 0; y < board.GetLength(1); y++)
        {
            var row = new string(board.EnumerateX(y).ToArray());
            var startChar = row.First(c => c != ' ');
            var startPos = row.IndexOf(startChar);
            var endChar = row.Reverse().First(c => c != ' ');
            var endPos = row.LastIndexOf(endChar);

            for (var x = startPos; x <= endPos; x++)
            {
                var c = row[x];
                if (c == '#')
                    continue;

                if (x == startPos)
                {
                    yield return (x, y, endChar != '#' ? endPos : x);
                    continue;
                }

                yield return (x, y, row[x - 1] != '#' ? x - 1 : x);
            }
        }
    }

    public static IEnumerable<(int X, int Y, int NewY)> DownMoves(this char[,] board)
    {
        for (var x = 0; x < board.GetLength(0); x++)
        {
            var column = new string(board.EnumerateY(x).ToArray());
            var startChar = column.First(c => c != ' ');
            var startPos = column.IndexOf(startChar);
            var endChar = column.Reverse().First(c => c != ' ');
            var endPos = column.LastIndexOf(endChar);

            for (var y = startPos; y <= endPos; y++)
            {
                var c = column[y];
                if (c == '#')
                    continue;

                if (y == endPos)
                {
                    yield return (x, y, startChar != '#' ? startPos : y);
                    continue;
                }

                yield return (x, y, column[y + 1] != '#' ? y + 1 : y);
            }
        }
    }

    public static IEnumerable<(int X, int Y, int NewY)> UpMoves(this char[,] board)
    {
        for (var x = 0; x < board.GetLength(0); x++)
        {
            var column = new string(board.EnumerateY(x).ToArray());
            var startChar = column.First(c => c != ' ');
            var startPos = column.IndexOf(startChar);
            var endChar = column.Reverse().First(c => c != ' ');
            var endPos = column.LastIndexOf(endChar);

            for (var y = startPos; y <= endPos; y++)
            {
                var c = column[y];
                if (c == '#')
                    continue;

                if (y == startPos)
                {
                    yield return (x, y, endChar != '#' ? endPos : y);
                    continue;
                }

                yield return (x, y, column[y - 1] != '#' ? y - 1 : y);
            }
        }
    }

#pragma warning disable CS8509, CS8524
    public static Facing Rotate(this Facing facing, int direction) => facing switch
    {
        Facing.Right => direction switch { -1 => Facing.Up, 1 => Facing.Down },
        Facing.Down => direction switch { -1 => Facing.Right, 1 => Facing.Left },
        Facing.Left => direction switch { -1 => Facing.Down, 1 => Facing.Up },
        Facing.Up => direction switch { -1 => Facing.Left, 1 => Facing.Right },
    };
#pragma warning restore
}

public sealed class Answer : IPuzzle
{
    private static IEnumerable<(int Turn, int Distance)> ReadMovement(IEnumerable<char> commands)
    {
        var amount = 0;
        var direction = 'R';
        foreach (var c in commands)
        {
            if (char.IsAsciiDigit(c))
            {
                amount = amount * 10 + (c - '0');
                continue;
            }

            yield return (direction == 'L' ? -1 : 1, amount);
            direction = c;
            amount = 0;
        }

        yield return (direction == 'L' ? -1 : 1, amount);
    }

    public int Part1(IEnumerable<string> input)
    {
        char[,] board;
        (int Turn, int Distance)[] commands;
        {
            var allLines = input.ToList();

            board = allLines.Take(allLines.Count - 2).ToArray().ToBoard();
            commands = ReadMovement(input.Last()).ToArray();
        }

        var rightMoves = board.RightMoves().ToDictionary(v => (v.X, v.Y), v => v.NewX);
        var leftMoves = board.LeftMoves().ToDictionary(v => (v.X, v.Y), v => v.NewX);
        var downMoves = board.DownMoves().ToDictionary(v => (v.X, v.Y), v => v.NewY);
        var upMoves = board.UpMoves().ToDictionary(v => (v.X, v.Y), v => v.NewY);

        var moveOutcomes = new Dictionary<Facing, Dictionary<(int X, int Y), int>>
        {
            { Facing.Right, rightMoves },
            { Facing.Down, downMoves },
            { Facing.Left, leftMoves },
            { Facing.Up, upMoves },
        };

        var facing = Facing.Up;
        var y = 0;
        var x = new string(board.EnumerateX(0).ToArray()).IndexOf('.');
        var journey = new List<(int X, int Y)> { (x, y) };

        foreach (var command in commands)
        {
            facing = facing.Rotate(command.Turn);

            for (var i = 0; i < command.Distance; i++)
            {
                var n = moveOutcomes[facing][(x, y)];
                switch (facing)
                {
                    case Facing.Left:
                    case Facing.Right:
                        if (x == n)
                            i = command.Distance;
                        else
                        {
                            x = n;
                            journey.Add((x, y));
                        }
                        break;

                    case Facing.Up:
                    case Facing.Down:
                        if (y == n)
                            i = command.Distance;
                        else
                        {
                            y = n;
                            journey.Add((x, y));
                        }
                        break;
                }
            }
        }

        var pw = 1000 * (y + 1) + 4 * (x + 1) + (int)facing;
        return pw;
    }

    public int Part2(IEnumerable<string> input)
    {
        return 0;
    }
}
