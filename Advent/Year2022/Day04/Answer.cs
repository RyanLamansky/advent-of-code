namespace Advent.Year2022.Day04;

public sealed class Answer : IPuzzle
{
    private readonly struct StartEnd
    {
        public readonly int Start, End;

        public StartEnd(string raw)
        {
            var parts = raw.Split('-');
            this.Start = int.Parse(parts[0]);
            this.End = int.Parse(parts[1]);
        }

        public int Length => this.End - this.Start + 1;

        public override string ToString() => $"{Start}-{End}";
    }

    private readonly struct Pair
    {
        private readonly StartEnd First, Second;

        public Pair(string line)
        {
            var parts = line.Split(',');
            this.First = new StartEnd(parts[0]);
            this.Second = new StartEnd(parts[1]);
        }

        public override string ToString() => $"{First},{Second}";

        public bool HasOverlap => Enumerable.Range(First.Start, First.Length).Intersect(Enumerable.Range(Second.Start, Second.Length)).Any();
        public bool FirstContainsSecond => Second.Start >= First.Start && Second.End <= First.End;
        public bool SecondContainsFirst => First.Start >= Second.Start && First.End <= Second.End;
        public bool OneContainsOther => FirstContainsSecond || SecondContainsFirst;
    }

    public int Part1(IEnumerable<string> input) => input.Where(line => new Pair(line).OneContainsOther).Count();

    public int Part2(IEnumerable<string> input) => input.Where(line => new Pair(line).HasOverlap).Count();
}
