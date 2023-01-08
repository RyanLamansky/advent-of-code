using System.Buffers;

namespace Advent.Year2022.Day15;

public sealed class Answer : IPuzzle<long>
{
    private readonly struct XY
    {
        public readonly int X, Y;

        public XY(ReadOnlySpan<char> rawX, ReadOnlySpan<char> rawY)
        {
            X = int.Parse(rawX);
            Y = int.Parse(rawY);
        }

        public override string ToString() => $"{X},{Y}";
    }

    private readonly struct Coverage
    {
        public readonly int Index, Length;

        public Coverage(int index, int length)
        {
            Index = index;
            Length = length;
        }

        public override string ToString() => $"{Index},{Length}";

        public int End => Index + Length - 1;
    }

    private readonly struct Sensor
    {
        public readonly XY Location, ClosestBeacon;

        public Sensor(string line)
        {
            var index = line.IndexOf('=') + 1;
            var end = line.IndexOf(',', index);
            var rawX = line.AsSpan(index, end - index);

            index = line.IndexOf('=', end) + 1;
            end = line.IndexOf(':', index);
            var rawY = line.AsSpan(index, end - index);

            Location = new XY(rawX, rawY);

            index = line.IndexOf('=', end) + 1;
            end = line.IndexOf(',', index);
            rawX = line.AsSpan(index, end - index);

            index = line.IndexOf('=', end) + 1;
            rawY = line.AsSpan(index);

            ClosestBeacon = new XY(rawX, rawY);
        }

        public Coverage BeaconExclusionCoverage(int y)
        {
            var distanceX = Math.Abs(Location.X - ClosestBeacon.X);
            var distanceY = Math.Abs(Location.Y - ClosestBeacon.Y);

            var widthAtOrigin = (distanceX + distanceY) * 2 + 1;
            var yDistanceFromOrigin = Math.Abs(Location.Y - y);
            var length = Math.Max(0, widthAtOrigin - yDistanceFromOrigin * 2);
            var index = Location.X - (length / 2);

            return new Coverage(index, length);
        }

        public override string ToString() => $"{Location}: {ClosestBeacon}";
    }

    public long Part1(IEnumerable<string> input)
    {
        var sensors = input.Select(line => new Sensor(line)).ToArray();

        var targetY = sensors[0].Location.X == 2 ? 10 : 2000000;

        var result = sensors
            .Select(sensor => sensor.BeaconExclusionCoverage(targetY))
            .SelectMany(coverage => Enumerable.Range(coverage.Index, coverage.Length))
            .Except(sensors.Where(sensor => sensor.ClosestBeacon.Y == targetY).Select(sensor => sensor.ClosestBeacon.X))
            .Distinct()
            .Count();

        return result;
    }

    public long Part2(IEnumerable<string> input)
    {
        var sensors = input.Select(line => new Sensor(line)).ToArray();
        var max = sensors[0].Location.X == 2 ? 20 : 4000000;

        // A proper pathing algorithm would be much faster but this is fast enough for the Advent of Code.
        var results = Enumerable.Range(0, max).AsParallel().Select(y =>
        {
            var coverages = ArrayPool<Coverage>.Shared.Rent(sensors.Length);
            try
            {
                for (var s = 0; s < sensors.Length; s++)
                {
                    var rawCoverage = sensors[s].BeaconExclusionCoverage(y);
                    if (rawCoverage.Length == 0)
                        coverages[s] = new Coverage(int.MaxValue - 1, 0);
                    else
                        coverages[s] = rawCoverage;
                }

                Array.Sort(coverages, (left, right) => left.Index.CompareTo(right.Index));
                var end = coverages[0].End;
                for (var c = 0; c < sensors.Length && end <= max; c++)
                {
                    var coverage = coverages[c];
                    if (coverage.Index > end + 1)
                        return (end + 1) * 4000000L + y;

                    end = Math.Max(end, coverage.End);
                }

                return -1L;
            }
            finally
            {
                ArrayPool<Coverage>.Shared.Return(coverages);
            }
        });

        return results.First(result => result != -1);
    }
}
