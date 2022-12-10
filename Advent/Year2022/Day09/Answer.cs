namespace Advent.Year2022.Day09;

public sealed class Answer : IPuzzle
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

    public int Part2(IEnumerable<string> input)
    {
        return 0;
    }
}
