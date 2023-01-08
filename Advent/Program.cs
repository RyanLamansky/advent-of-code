foreach (var result in System.Reflection.Assembly
    .GetExecutingAssembly()
    .GetTypes()
    .Where(type => type.GetInterfaces().Any(i => (i == typeof(Advent.IPuzzle))) && !type.IsInterface)
    .AsParallel()
    .OrderBy(type => type.FullName)
    .Select(Activator.CreateInstance)
    .OfType<Advent.IPuzzle>()
    .Select(puzzle =>
    {
        var result = new System.Text.StringBuilder(puzzle.GetType().Namespace![11..].Replace("Day", ""))
            .Append(": ");

        foreach (var solution in puzzle.Solve())
            result.Append(solution).Append(", ");

        result.Length -= 2;

        return result.ToString();
    }))
{
    Console.WriteLine(result);
}
