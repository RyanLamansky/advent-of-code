using System.Collections;

namespace Advent.Year2022.Day09;

public sealed class Answer : IPuzzle<int>
{
    static void Move(char direction, ref (int X, int Y) item)
    {
        switch (direction)
        {
            case 'U':
                item = (item.X, item.Y - 1);
                return;
            case 'D':
                item = (item.X, item.Y + 1);
                return;
            case 'L':
                item = (item.X - 1, item.Y);
                return;
            case 'R':
                item = (item.X + 1, item.Y);
                return;
        }
    }

    static void Snap(char direction, (int X, int Y) head, ref (int X, int Y) tail)
    {
        switch (direction)
        {
            case 'U':
                tail = (head.X, head.Y + 1);
                return;
            case 'D':
                tail = (head.X, head.Y - 1);
                return;
            case 'L':
                tail = (head.X + 1, head.Y);
                return;
            case 'R':
                tail = (head.X - 1, head.Y);
                return;
        }
    }

    public int Part1(IEnumerable<string> input)
    {
        (int X, int Y)
            head = (0, 0),
            tail = (0, 0);

        var tailVisited = new HashSet<(int X, int Y)> { tail };

        foreach (var line in input)
        {
            var direction = line[0];

            for (var amount = int.Parse(line.AsSpan(2)); amount > 0; amount--)
            {
                Move(direction, ref head);
                var xDistance = Math.Abs(head.X - tail.X);
                var yDistance = Math.Abs(head.Y - tail.Y);
                var maxDistance = Math.Max(xDistance, yDistance);
                var totalDistance = xDistance + yDistance;

                switch (maxDistance)
                {
                    case 0:
                    case 1:
                        break;
                    case 2:
                        if (maxDistance == totalDistance)
                            Move(direction, ref tail);
                        else
                            Snap(direction, head, ref tail);

                        tailVisited.Add(tail);
                        break;
                    default:
                        throw new Exception();
                }
            }
        }

        return tailVisited.Count;
    }

    private sealed class Knot : IEnumerable<Knot>
    {
        public readonly Knot? Next;
        public int X, Y;
        public readonly HashSet<(int X, int Y)> Visited = new() { (0, 0) };

        public Knot(int remainingLength)
        {
            if (remainingLength > 0)
                Next = new Knot(remainingLength - 1);
        }

        public void Move(char direction)
        {
            switch (direction)
            {
                case 'U':
                    Y--;
                    break;
                case 'D':
                    Y++;
                    break;
                case 'L':
                    X--;
                    break;
                case 'R':
                    X++;
                    break;
                case '↗':
                    X++;
                    Y--;
                    break;
                case '↘':
                    X++;
                    Y++;
                    break;
                case '↙':
                    X--;
                    Y++;
                    break;
                case '↖':
                    X--;
                    Y--;
                    break;
            }

            Visited.Add((X, Y));

            if (Next is null)
                return; // End of the rope

            if (Math.Abs(X - Next.X) <= 1 && Math.Abs(Y - Next.Y) <= 1)
                return; // Next segment is still touching after the move.

            if (X == Next.X || Y == Next.Y)
            {
                // Still on the same axis.
                if (X == Next.X)
                {
                    if (Y > Next.Y)
                        Next.Move('D');
                    else
                        Next.Move('U');
                }
                else
                {
                    if (X > Next.X)
                        Next.Move('R');
                    else
                        Next.Move('L');
                }
                return;
            }

            if (X > Next.X)
            {
                if (Y > Next.Y)
                    Next.Move('↘');
                else
                    Next.Move('↗');
            }
            else
            {
                if (Y > Next.Y)
                    Next.Move('↙');
                else
                    Next.Move('↖');
            }
        }

        public IEnumerator<Knot> GetEnumerator()
        {
            var next = this;
            do
            {
                yield return next;
            } while ((next = next!.Next) is not null);
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public override string ToString() => $"{X},{Y}";
    }

    public int Part2(IEnumerable<string> input)
    {
        var rope = new Knot(9);

        foreach (var line in input)
        {
            var direction = line[0];

            for (var amount = int.Parse(line.AsSpan(2)); amount > 0; amount--)
                rope.Move(direction);
        }

        return rope.Last().Visited.Count;
    }
}
