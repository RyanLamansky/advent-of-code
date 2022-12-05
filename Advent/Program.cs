foreach (var puzzle in System.Reflection.Assembly.GetExecutingAssembly()
    .GetTypes()
    .Where(type => type.GetInterfaces().Any(i => i == typeof(Advent.IPuzzle)) && !type.IsInterface)
    .OrderBy(type => type.FullName)
    .Select(Activator.CreateInstance)
    .OfType<Advent.IPuzzle>())
{
    Console.Write(puzzle.GetType().Namespace![11..].Replace("Day", ""));
    Console.Write(": ");

    if (puzzle is Advent.IPuzzle64 puzzle64)
    {
        Console.Write(puzzle64.RunSamplePart1());
        Console.Write(", ");
        Console.Write(puzzle64.RunInputPart1());
        Console.Write(", ");
        Console.Write(puzzle64.RunSamplePart2());
        Console.Write(", ");
        Console.Write(puzzle64.RunInputPart2());
        Console.WriteLine();
        continue;
    }

    Console.Write(puzzle.RunSamplePart1());
    Console.Write(", ");
    Console.Write(puzzle.RunInputPart1());
    Console.Write(", ");
    Console.Write(puzzle.RunSamplePart2());
    Console.Write(", ");
    Console.Write(puzzle.RunInputPart2());
    Console.WriteLine();
}
