foreach (var result in System.Reflection.Assembly
    .GetExecutingAssembly()
    .GetTypes()
    .Where(type => type.GetInterfaces().Any(i => i == typeof(Advent.IPuzzle)) && !type.IsInterface)
    .AsParallel()
    .OrderBy(type => type.FullName)
    .Select(Activator.CreateInstance)
    .OfType<Advent.IPuzzle>()
    .Select(puzzle =>
    {
        var result = new System.Text.StringBuilder(puzzle.GetType().Namespace![11..].Replace("Day", ""))
            .Append(": ");

        if (puzzle is Advent.IPuzzle64 puzzle64)
        {
            result
                .Append(puzzle64.RunSamplePart1())
                .Append(", ")
                .Append(puzzle64.RunInputPart1())
                .Append(", ")
                .Append(puzzle64.RunSamplePart2())
                .Append(", ")
                .Append(puzzle64.RunInputPart2());
        }
        else if (puzzle is Advent.IPuzzleString puzzleString)
        {
            result
                .Append(puzzleString.RunSamplePart1())
                .Append(", ")
                .Append(puzzleString.RunInputPart1())
                .Append(", ")
                .Append(puzzleString.RunSamplePart2())
                .Append(", ")
                .Append(puzzleString.RunInputPart2());
        }
        else
        {
            result
                .Append(puzzle.RunSamplePart1())
                .Append(", ")
                .Append(puzzle.RunInputPart1())
                .Append(", ")
                .Append(puzzle.RunSamplePart2())
                .Append(", ")
                .Append(puzzle.RunInputPart2());
        }

        return result.ToString();
    }))
{
    Console.WriteLine(result);
}
