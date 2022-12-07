namespace Advent.Year2022.Day07;

public sealed class Answer : IPuzzle
{
    private sealed class ParsedDirectory
    {
        public readonly string Name;
        public readonly ParsedDirectory? Parent;

        public readonly List<(string Name, int Size)> Files = new();
        public readonly List<ParsedDirectory> Directories = new();

        public ParsedDirectory(string name, ParsedDirectory? parent = null)
        {
            this.Name = name;
            this.Parent = parent;
        }

        public override string ToString() => Name;

        public IEnumerable<ParsedDirectory> SelfAndChildren() => Directories.SelectMany(dir => dir.SelfAndChildren()).Prepend(this);

        public int Size => SelfAndChildren().SelectMany(dir => dir.Files.Select(file => file.Size)).Sum();
    }

    private static ParsedDirectory Parse(IEnumerable<string> input)
    {
        using var lines = input.GetEnumerator();

        lines.MoveNext(); // Read first line.
        lines.MoveNext(); // Always "$ cd /", so we can just assume we're at the root and move on.

        var currentPath = new Stack<ParsedDirectory>();
        var root = new ParsedDirectory("");
        currentPath.Push(root);
        var line = lines.Current;

        while (true)
        {
            if (line == "$ ls")
            {
                bool moved;
                while ((moved = lines.MoveNext()) && (line = lines.Current)[0] != '$')
                {
                    if (line.StartsWith("dir"))
                        continue;

                    var parts = line.Split(' ');
                    currentPath.Peek().Files.Add((parts[1], int.Parse(parts[0])));
                };

                if (moved)
                    continue;

                return root;
            }

            if (line.StartsWith("$ cd "))
            {
                var targetPath = line["$ cd ".Length..];
                if (targetPath == "..")
                    currentPath.Pop();
                else
                {
                    var parent = currentPath.Peek();
                    var child = new ParsedDirectory(line["$ cd ".Length..], parent);
                    parent.Directories.Add(child);
                    currentPath.Push(child);
                }

                lines.MoveNext();
                line = lines.Current;
                continue;
            }

            throw new Exception();
        }
    }

    public int Part1(IEnumerable<string> input) => Parse(input)
        .SelfAndChildren()
        .Select(dir => dir.Size)
        .Where(size => size <= 100000)
        .First();

    public int Part2(IEnumerable<string> input)
    {
        var root = Parse(input);
        const int totalSpace = 70000000, spaceNeeded = 30000000;
        var spaceUsed =  root.Size;
        var unusedSpace = totalSpace - spaceUsed;
        var spaceToRecover = spaceNeeded - unusedSpace;

        return root
            .SelfAndChildren()
            .Select(dir => dir.Size)
            .Where(size => size >= spaceToRecover)
            .OrderBy(size => size)
            .First();
    }
}
