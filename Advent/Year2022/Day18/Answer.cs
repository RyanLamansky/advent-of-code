namespace Advent.Year2022.Day18;

public sealed class Answer : IPuzzle
{
    public int Part1(IEnumerable<string> input)
    {
        var cubes = input.Select(line =>
        {
            var parts = line.Split(',');
            return (X: byte.Parse(parts[0]), Y: byte.Parse(parts[1]), Z: byte.Parse(parts[2]));
        }).ToHashSet();

        var openSides = 0;
        foreach (var (X, Y, Z) in cubes)
        {
            if (!cubes.Contains(((byte)(X - 1), Y, Z))) openSides++;
            if (!cubes.Contains(((byte)(X + 1), Y, Z))) openSides++;
            if (!cubes.Contains((X, (byte)(Y - 1), Z))) openSides++;
            if (!cubes.Contains((X, (byte)(Y+ 1), Z))) openSides++;
            if (!cubes.Contains((X, Y, (byte)(Z - 1)))) openSides++;
            if (!cubes.Contains((X, Y, (byte)(Z + 1)))) openSides++;
        }

        return openSides;
    }

    public int Part2(IEnumerable<string> input)
    {
        var cubes = input.Select(line =>
        {
            var parts = line.Split(',');
            return (X: sbyte.Parse(parts[0]), Y: sbyte.Parse(parts[1]), Z: sbyte.Parse(parts[2]));
        }).ToHashSet();

        sbyte minX = cubes.Min(cube => cube.X);
        sbyte maxX = cubes.Max(cube => cube.X);
        sbyte minY = cubes.Min(cube => cube.Y);
        sbyte maxY = cubes.Max(cube => cube.Y);
        sbyte minZ = cubes.Min(cube => cube.Z);
        sbyte maxZ = cubes.Max(cube => cube.Z);

        static bool HasNeighbor(HashSet<(sbyte X, sbyte Y, sbyte Z)> cubes, (sbyte X, sbyte Y, sbyte Z) cube)
        {
            if (cubes.Contains(((sbyte)(cube.X - 1), cube.Y, cube.Z))) return true;
            if (cubes.Contains(((sbyte)(cube.X + 1), cube.Y, cube.Z))) return true;
            if (cubes.Contains((cube.X, (sbyte)(cube.Y - 1), cube.Z))) return true;
            if (cubes.Contains((cube.X, (sbyte)(cube.Y + 1), cube.Z))) return true;
            if (cubes.Contains((cube.X, cube.Y, (sbyte)(cube.Z - 1)))) return true;
            if (cubes.Contains((cube.X, cube.Y, (sbyte)(cube.Z + 1)))) return true;

            return false;
        }

        var outside = new HashSet<(sbyte X, sbyte Y, sbyte Z)>();
        for (var x = (sbyte)(minX - 1); x <= maxX + 1; x++)
        {
            for (var y = (sbyte)(minY - 1); y <= maxY + 1; y++)
            {
                outside.Add((x, y, (sbyte)(minZ - 1)));
                outside.Add((x, y, (sbyte)(maxZ + 1)));
            }
        }
        for (var x = (sbyte)(minX - 1); x <= maxX + 1; x++)
        {
            for (var z = (sbyte)(minZ - 1); z <= maxZ + 1; z++)
            {
                outside.Add((x, (sbyte)(minY - 1), z));
                outside.Add((x, (sbyte)(maxY + 1), z));
            }
        }
        for (var y = (sbyte)(minY - 1); y <= maxY + 1; y++)
        {
            for (var z = (sbyte)(minZ - 1); z <= maxZ + 1; z++)
            {
                outside.Add(((sbyte)(minX - 1), y, z));
                outside.Add(((sbyte)(maxX + 1), y, z));
            }
        }

        int added;
        do
        {
            added = 0;
            for (var x = minX; x <= maxX ; x++)
            {
                for (var y = minY; y <= maxY; y++)
                {
                    for (var z = minZ; z <= maxZ; z++)
                    {
                        var xyz = (x, y, z);
                        if (cubes.Contains(xyz))
                            continue;

                        if (HasNeighbor(outside, xyz) && outside.Add(xyz))
                            added++;
                    }
                }
            }
        } while (added != 0);

        var outerSides = 0;
        foreach (var (X, Y, Z) in cubes)
        {
            if (!cubes.Contains(((sbyte)(X - 1), Y, Z)) && outside.Contains(((sbyte)(X - 1), Y, Z)))
                outerSides++;
            if (!cubes.Contains(((sbyte)(X + 1), Y, Z)) && outside.Contains(((sbyte)(X + 1), Y, Z)))
                outerSides++;
            if (!cubes.Contains((X, (sbyte)(Y - 1), Z)) && outside.Contains((X, (sbyte)(Y - 1), Z)))
                outerSides++;
            if (!cubes.Contains((X, (sbyte)(Y + 1), Z)) && outside.Contains((X, (sbyte)(Y + 1), Z)))
                outerSides++;
            if (!cubes.Contains((X, Y, (sbyte)(Z - 1))) && outside.Contains((X, Y, (sbyte)(Z - 1))))
                outerSides++;
            if (!cubes.Contains((X, Y, (sbyte)(Z + 1))) && outside.Contains((X, Y, (sbyte)(Z + 1))))
                outerSides++;
        }
        return outerSides;
    }
}
