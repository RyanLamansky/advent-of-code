namespace Advent.Year2022.Day17;

public sealed class Answer : IPuzzle<long>
{
    private readonly struct Row
    {
        private readonly byte raw;

        public Row(int raw)
        {
            this.raw = (byte)(raw & 0b11111111);
        }

        public override string ToString()
        {
            Span<char> rawResult = stackalloc char[7];
            var remaining = raw;

            for (var i = 0; i < rawResult.Length; i++)
            {
                rawResult[i] = (remaining & 1) == 0 ? '.' : '#';
                remaining >>= 1;
            }

            return new string(rawResult);
        }

        public bool this[int index] => // There's probably a clever algorithm for the min/max branches.
            raw >= 0 && raw <= 6 && (raw & (1 << index)) != 0;

        public static implicit operator int(Row row) => row.raw;

        public static void WriteRows(Span<Row> rows)
        {
            var format = new string('0', (int)Math.Ceiling(Math.Log10(rows.Length)));
            Console.Write(new string(' ', format.Length + 1));
            Console.WriteLine("0123456");

            for (var i = 0; i < rows.Length; i++)
            {
                var index = rows.Length - 1 - i;
                Console.WriteLine($"{index.ToString(format)}|{rows[index]}|");
            }
        }
    }

    public long Part1(IEnumerable<string> input)
    {
        var directions = input.First().AsSpan();
        var directionIndex = 0;

        var rows = Array.Empty<Row>();

        const ushort
            horizontal = 0b0000_0000_0000_1111,
            plus = 0b0000_0010_0111_0010,
            reverseL = 0b0000_0100_0100_0111,
            vertical = 0b0001_0001_0001_0001,
            box = 0b0000_0000_0011_0011;

        static int Height(ushort shape) => 4 - (ushort.LeadingZeroCount(shape) / 4);

        static int Segment(ushort shape, int row) => (shape >> (row * 4)) & 0b1111;

        static void DrawShape(Span<Row> rows, ushort shape, int origin, int height)
        {
            var area = rows.Slice(rows.Length - height, height);
            for (var h = 0; h < height; h++)
                area[h] = new Row(Segment(shape, h) << origin | (int)area[h]);
        }

        static bool CanDrawShape(Span<Row> rows, ushort shape, int origin, int height)
        {
            if (height > rows.Length)
                return false;

            var area = rows.Slice(rows.Length - height, height);

            for (var h = 0; h < height; h++)
            {
                var shiftedSegment = Segment(shape, h) << origin;
                const int tooWideMask = int.MaxValue << 7;
                if ((shiftedSegment & tooWideMask) != 0 || (area[h] & shiftedSegment) != 0)
                    return false;
            }

            return true;
        }

        static int FirstNonBlankRow(Span<Row> rows)
        {
            for (var i = rows.Length - 1; i >= 0; i--)
                if ((int)rows[i] != 0)
                    return i;

            return -1;
        }

        for (var rock = 0; rock < 2022; rock++)
        {
            var shape = (rock % 5) switch
            {
                0 => horizontal,
                1 => plus,
                2 => reverseL,
                3 => vertical,
                4 => box,
                _ => throw new Exception(),
            };

            var height = Height(shape);
            Array.Resize(ref rows, FirstNonBlankRow(rows) + height + 4);

            var bottom = rows.Length - height;
            var origin = 2;
            var window = rows.AsSpan(bottom, height);

            do
            {
                if (directions[directionIndex++ % directions.Length] == '>')
                {
                    if (CanDrawShape(window, shape, origin + 1, height))
                        origin++;
                }
                else
                {
                    if (origin - 1 >= 0)
                        if (CanDrawShape(window, shape, origin - 1, height))
                            origin--;
                }

                if (bottom - 1 < 0 || !CanDrawShape(rows.AsSpan(bottom - 1, height), shape, origin, height))
                    break;

                window = rows.AsSpan(--bottom, height);
            } while (bottom >= 0);

            DrawShape(window, shape, origin, height); // Draw at the final resting place.
        }

        var result = FirstNonBlankRow(rows) + 1;

        return result;
    }

    public long Part2(IEnumerable<string> input)
    {
        return 0;
    }
}
