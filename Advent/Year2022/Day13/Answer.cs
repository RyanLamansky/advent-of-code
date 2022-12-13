namespace Advent.Year2022.Day13;

public sealed class Answer : IPuzzle
{
    private sealed class Packet : IComparable<Packet>
    {
        private readonly object[] data;
        private readonly string raw;

        public Packet(string raw)
        {
            this.raw = raw;
            var lists = new Stack<List<object>>();

            for (var i = 0; i < raw.Length; i++)
            {
                var c = raw[i];

                if (c == '[')
                {
                    lists.Push(new List<object>());
                    continue;
                }

                if (c == ']')
                {
                    if (lists.Count > 1)
                    {
                        var popped = lists.Pop();
                        lists.Peek().Add(popped.ToArray());
                        continue;
                    }

                    data = lists.Pop().ToArray();
                    return;
                }

                if (char.IsAsciiDigit(c))
                {
                    var endOfInt = raw.IndexOfAny(new char[] { ',', ']' }, i);
                    var value = int.Parse(raw.AsSpan(i, endOfInt - i));
                    lists.Peek().Add(value);

                    i = endOfInt - 1;
                }
            }

            throw new Exception();
        }

        public static IEnumerable<(Packet a, Packet b)> ReadPairs(IEnumerable<string> input)
        {
            using var enumerator = input.GetEnumerator();

            while (enumerator.MoveNext())
            {
                var a = new Packet(enumerator.Current);
                enumerator.MoveNext();
                var b = new Packet(enumerator.Current);
                enumerator.MoveNext();

                yield return (a, b);
            }

            yield break;
        }

        public override string ToString() => raw;

        private static bool? IsLessThan(object[] leftData, object[] rightData)
        {
            if (leftData.Length == 0)
            {
                if (rightData.Length == 0)
                    return null;

                return true;
            }

            for (var i = 0; i < leftData.Length; i++)
            {
                if (i == rightData.Length)
                    return false;

                var result = IsLessThan(leftData[i], rightData[i]);
                if (result is not null)
                    return result;
            }

            if (leftData.Length == rightData.Length)
                return null;

            return true;
        }

        private static bool? IsLessThan(object left, object right)
        {
            if (left is int l)
            {
                if (right is int r)
                {
                    if (l < r)
                        return true;
                    if (l > r)
                        return false;

                    return null;
                }

                return IsLessThan(new object[] { left }, (object[])right);
            }

            var leftData = (object[])left;

            if (right is not object[] rightData)
                rightData = new object[] { right };

            return IsLessThan(leftData, rightData);
        }

        public bool IsLessThan(Packet right) => IsLessThan(data, right.data) ?? throw new Exception();

        public int CompareTo(Packet? other)
        {
            if (other is null) throw new Exception();

            var isLessThan = this.IsLessThan(other);
            if (isLessThan)
                return -1;
            else
                return 1;
        }
    }

    public int Part1(IEnumerable<string> input)
    {
        var packetPairs = Packet.ReadPairs(input).ToArray();
        var results = packetPairs.Select(pair => pair.a.IsLessThan(pair.b)).ToArray();

        var inOrder = results.Select((r, i) => (r, i)).Where(t => t.r).Select(t => t.i + 1).ToArray();

        return inOrder.Sum();
    }

    public int Part2(IEnumerable<string> input)
    {
        var divider1 = new Packet("[[2]]");
        var divider2 = new Packet("[[6]]");

        var packets = input.Where(raw => raw.Length != 0).Select(raw => new Packet(raw)).Append(divider1).Append(divider2).ToArray();
        var sorted = packets.Order().ToArray();

        var d1i = Array.IndexOf(sorted, divider1);
        var d2i = Array.IndexOf(sorted, divider2);

        return (d1i + 1) * (d2i + 1);
    }
}
