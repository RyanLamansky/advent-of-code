namespace Advent.Year2022.Day23;

public sealed class Answer : IPuzzle<int>
{
    sealed class Elf
    {
        public int X, Y;

        public Elf(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static IEnumerable<Elf> ReadData(IEnumerable<string> input)
        {
            var y = 0;
            foreach (var line in input)
            {
                y++;

                var x = 0;
                foreach (var c in line)
                {
                    x++;

                    if (c == '#')
                        yield return new(x, y);
                }
            }
        }

        [Flags]
        enum Direction : byte
        {
            None = 0,
            North = 1 << 0,
            NorthEast = 1 << 1,
            NorthWest = 1 << 2,
            South = 1 << 3,
            SouthEast = 1 << 4,
            SouthWest = 1 << 5,
            East = 1 << 6,
            West = 1 << 7,
        }

        public bool LocationMatches((int X, int Y) location) => X == location.X && Y == location.Y;

        public (int X, int Y) ProposeMove(int round, HashSet<(int X, int Y)> positions)
        {
            var x = X;
            var y = Y;

            var occupied =
                (positions.Contains((x, y - 1)) ? Direction.North : 0) |
                (positions.Contains((x + 1, y - 1)) ? Direction.NorthEast : 0) |
                (positions.Contains((x - 1, y - 1)) ? Direction.NorthWest : 0) |
                (positions.Contains((x, y + 1)) ? Direction.South : 0) |
                (positions.Contains((x + 1, y + 1)) ? Direction.SouthEast : 0) |
                (positions.Contains((x - 1, y + 1)) ? Direction.SouthWest : 0) |
                (positions.Contains((x + 1, y)) ? Direction.East : 0) |
                (positions.Contains((x - 1, y)) ? Direction.West : 0)
                ;

            if (occupied == Direction.None)
                return (x, y);

            for (var sequence = 0; sequence < 4; sequence++)
            {
                // "Finally, at the end of the round, the first direction the Elves considered is moved to the end of the list of directions."
                switch ((sequence + round) % 4)
                {
                    default: throw new Exception();
                    case 0:
                        if ((occupied & (Direction.North | Direction.NorthEast | Direction.NorthWest)) == 0)
                            return (x, y - 1);
                        break;
                    case 1:
                        if ((occupied & (Direction.South | Direction.SouthEast | Direction.SouthWest)) == 0)
                            return (x, y + 1);
                        break;
                    case 2:
                        if ((occupied & (Direction.West | Direction.NorthWest | Direction.SouthWest)) == 0)
                            return (x - 1, y);
                        break;
                    case 3:
                        if ((occupied & (Direction.East | Direction.NorthEast | Direction.SouthEast)) == 0)
                            return (x + 1, y);
                        break;
                }
            }

            return (x, y);
        }

        public override string ToString() => $"{X},{Y}";
    }

    public int Part1(IEnumerable<string> input)
    {
        var elves = Elf.ReadData(input).ToArray();
        var positions = elves.Select(elf => (elf.X, elf.Y)).ToHashSet();

        for (var round = 0; round < 10; round++)
        {
            foreach (var (elf, proposal) in elves
                .Select(elf => (Elf: elf, Proposal: elf.ProposeMove(round, positions)))
                .Where(ep => !ep.Elf.LocationMatches(ep.Proposal))
                .GroupBy(ep => ep.Proposal)
                .Where(ep => ep.Count() == 1)
                .Select(group => group.First())
                )
            {
                elf.X = proposal.X;
                elf.Y = proposal.Y;
            }

            positions = elves.Select(elf => (elf.X, elf.Y)).ToHashSet();
        }

        var minX = positions.Min(xy => xy.X);
        var minY = positions.Min(xy => xy.Y);
        var maxX = positions.Max(xy => xy.X);
        var maxY = positions.Max(xy => xy.Y);

        var rangeX = maxX - minX + 1;
        var rangeY = maxY - minY + 1;
        var area = rangeX * rangeY;
        var unoccupiedArea = area - elves.Length;

        return unoccupiedArea;
    }

    public int Part2(IEnumerable<string> input)
    {
        var elves = Elf.ReadData(input).ToArray();
        var positions = elves.Select(elf => (elf.X, elf.Y)).ToHashSet();

        var round = 0;
        bool moved;
        do
        {
            moved = false;
            foreach (var (elf, proposal) in elves
                .Select(elf => (Elf: elf, Proposal: elf.ProposeMove(round, positions)))
                .Where(ep => !ep.Elf.LocationMatches(ep.Proposal))
                .GroupBy(ep => ep.Proposal)
                .Where(ep => ep.Count() == 1)
                .Select(group => group.First())
                )
            {
                elf.X = proposal.X;
                elf.Y = proposal.Y;
                moved = true;
            }

            positions = elves.Select(elf => (elf.X, elf.Y)).ToHashSet();
            round++;
        } while (moved);

        return round;
    }
}
