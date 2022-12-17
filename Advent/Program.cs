foreach (var result in System.Reflection.Assembly
    .GetExecutingAssembly()
    .GetTypes()
    .Where(type => type.GetInterfaces().Any(i => (i == typeof(Advent.IPuzzle)) || i == typeof(Advent.IBytePuzzle)) && !type.IsInterface)
    .AsParallel()
    .OrderBy(type => type.FullName)
    .Select(Activator.CreateInstance)
    .Select(puzzle =>
    {
        var result = new System.Text.StringBuilder(puzzle!.GetType().Namespace![11..].Replace("Day", ""))
            .Append(": ");

        if (puzzle is Advent.IBytePuzzle bytePuzzle)
        {
            result
                .Append(bytePuzzle.RunSamplePart1())
                .Append(", ")
                .Append(bytePuzzle.RunInputPart1())
                .Append(", ")
                .Append(bytePuzzle.RunSamplePart2())
                .Append(", ")
                .Append(bytePuzzle.RunInputPart2());
        }
        else if (puzzle is Advent.IPuzzle64 puzzle64)
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
        else if (puzzle is Advent.IPuzzle puzzle32)
        {
            result
                .Append(puzzle32.RunSamplePart1())
                .Append(", ")
                .Append(puzzle32.RunInputPart1())
                .Append(", ")
                .Append(puzzle32.RunSamplePart2())
                .Append(", ")
                .Append(puzzle32.RunInputPart2());
        }

        return result.ToString();
    }))
{
    Console.WriteLine(result);
}
