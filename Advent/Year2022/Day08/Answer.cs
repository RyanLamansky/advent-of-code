namespace Advent.Year2022.Day08;

public sealed class Answer : IPuzzle
{
    private readonly struct Grid
    {
        private readonly byte[,] data;

        public byte this[int x, int y] => data[x, y];

        public int Length => data.GetLength(0); // Same width and height in sample and input.

        public Grid(IEnumerable<string> input)
        {
            var rows = new List<byte[]>();

            foreach (var row in input)
                rows.Add(row.Select(c => (byte)(c - '0')).ToArray());

            var data = this.data = new byte[rows[0].Length, rows.Count];

            for (var y = 0; y < rows.Count; y++)
            {
                var row = rows[y];

                for (var x = 0; x < row.Length; x++)
                    data[x, y] = row[x];
            }
        }

        public bool IsVisible(int x, int y)
        {
            if (x == 0)
                return true;
            if (y == 0)
                return true;
            if (x == Length - 1)
                return true;
            if (y == Length - 1)
                return true;

            var initial = this[x, y];

            var current = initial;
            for (var left = x - 1; ; left--)
            {
                var here = this[left, y];

                if (here >= current)
                    break;

                if (left == 0)
                    return true;
            }

            current = initial;
            for (var right = x + 1; ; right++)
            {
                var here = this[right, y];

                if (here >= current)
                    break;

                if (right == Length - 1)
                    return true;
            }

            current = initial;
            for (var top = y - 1; ; top--)
            {
                var here = this[x, top];

                if (here >= current)
                    break;

                if (top == 0)
                    return true;
            }

            current = initial;
            for (var bottom = y + 1; ; bottom++)
            {
                var here = this[x, bottom];

                if (here >= current)
                    break;

                if (bottom == Length - 1)
                    return true;
            }

            return false;
        }

        public int ScenicScore(int x, int y)
        {
            if (x == 0)
                return 0;
            if (y == 0)
                return 0;
            if (x == Length - 1)
                return 0;
            if (y == Length - 1)
                return 0;

            var total = 1;
            int direction;
            var initial = this[x, y];

            direction = 1;
            for (var step = x - 1; step > 0; step--)
            {
                var here = this[step, y];

                if (here >= initial)
                    break;

                direction++;
            }
            total *= direction;

            direction = 1;
            for (var step = x + 1; step < Length - 1; step++)
            {
                var here = this[step, y];

                if (here >= initial)
                    break;

                direction++;
            }
            total *= direction;

            direction = 1;
            for (var step = y - 1; step > 0; step--)
            {
                var here = this[x, step];

                if (here >= initial)
                    break;

                direction++;
            }
            total *= direction;

            direction = 1;
            for (var step = y + 1; step < Length - 1; step++)
            {
                var here = this[x, step];

                if (here >= initial)
                    break;

                direction++;
            }
            total *= direction;

            return total;
        }
    }

    public int Part1(IEnumerable<string> input)
    {
        var grid = new Grid(input);

        var count = 0;

        for (var y = 0; y < grid.Length; y++)
        {
            for (var x = 0; x < grid.Length; x++)
            {
                if (grid.IsVisible(x, y))
                    count++;
            }
        }

        return count;
    }

    public int Part2(IEnumerable<string> input)
    {
        var grid = new Grid(input);
        var highScore = 0;

        for (var y = 0; y < grid.Length; y++)
        {
            for (var x = 0; x < grid.Length; x++)
            {
                highScore = Math.Max(highScore, grid.ScenicScore(x, y));
            }
        }

        return highScore;
    }
}
