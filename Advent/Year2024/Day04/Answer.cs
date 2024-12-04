namespace Advent.Year2024.Day04;

public sealed class Answer : IPuzzle<Grid<char>, int>
{
    public Grid<char> Parse(IEnumerable<string> input)
        => new(input, c => c);

    public int Part1(Grid<char> parsed)
    {
        var width = parsed.Width;
        var height = parsed.Height;

        var found = 0;

        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                if (parsed[x, y] != 'X')
                    continue;

                switch (parsed.GetOrDefault(x + 1, y, ' ')) // Right
                {
                    case 'M':
                        switch (parsed.GetOrDefault(x + 2, y, ' '))
                        {
                            case 'A':
                                switch (parsed.GetOrDefault(x + 3, y, ' '))
                                {
                                    case 'S':
                                        found++;
                                        break;
                                }
                                break;
                        }
                        break;
                }

                switch (parsed.GetOrDefault(x + 1, y + 1, ' ')) // Diagonal L+D
                {
                    case 'M':
                        switch (parsed.GetOrDefault(x + 2, y + 2, ' '))
                        {
                            case 'A':
                                switch (parsed.GetOrDefault(x + 3, y + 3, ' '))
                                {
                                    case 'S':
                                        found++;
                                        break;
                                }
                                break;
                        }
                        break;
                }

                switch (parsed.GetOrDefault(x, y + 1, ' ')) // Down
                {
                    case 'M':
                        switch (parsed.GetOrDefault(x, y + 2, ' '))
                        {
                            case 'A':
                                switch (parsed.GetOrDefault(x, y + 3, ' '))
                                {
                                    case 'S':
                                        found++;
                                        break;
                                }
                                break;
                        }
                        break;
                }

                switch (parsed.GetOrDefault(x - 1, y + 1, ' ')) // Diagonal L+D
                {
                    case 'M':
                        switch (parsed.GetOrDefault(x - 2, y + 2, ' '))
                        {
                            case 'A':
                                switch (parsed.GetOrDefault(x - 3, y + 3, ' '))
                                {
                                    case 'S':
                                        found++;
                                        break;
                                }
                                break;
                        }
                        break;
                }

                switch (parsed.GetOrDefault(x - 1, y, ' ')) // Left
                {
                    case 'M':
                        switch (parsed.GetOrDefault(x - 2, y, ' '))
                        {
                            case 'A':
                                switch (parsed.GetOrDefault(x - 3, y, ' '))
                                {
                                    case 'S':
                                        found++;
                                        break;
                                }
                                break;
                        }
                        break;
                }

                switch (parsed.GetOrDefault(x - 1, y - 1, ' ')) // Diagonal L+U
                {
                    case 'M':
                        switch (parsed.GetOrDefault(x - 2, y - 2, ' '))
                        {
                            case 'A':
                                switch (parsed.GetOrDefault(x - 3, y - 3, ' '))
                                {
                                    case 'S':
                                        found++;
                                        break;
                                }
                                break;
                        }
                        break;
                }

                switch (parsed.GetOrDefault(x, y - 1, ' ')) // Up
                {
                    case 'M':
                        switch (parsed.GetOrDefault(x, y - 2, ' '))
                        {
                            case 'A':
                                switch (parsed.GetOrDefault(x, y - 3, ' '))
                                {
                                    case 'S':
                                        found++;
                                        break;
                                }
                                break;
                        }
                        break;
                }

                switch (parsed.GetOrDefault(x + 1, y - 1, ' ')) // Diagonal R+U
                {
                    case 'M':
                        switch (parsed.GetOrDefault(x + 2, y - 2, ' '))
                        {
                            case 'A':
                                switch (parsed.GetOrDefault(x + 3, y - 3, ' '))
                                {
                                    case 'S':
                                        found++;
                                        break;
                                }
                                break;
                        }
                        break;
                }
            }
        }

        return found;
    }

    public int Part2(Grid<char> parsed)
    {
        var width = parsed.Width;
        var height = parsed.Height;

        var found = 0;

        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                if (parsed[x, y] != 'A')
                    continue;

                switch (parsed.GetOrDefault(x - 1, y - 1, ' '))
                {
                    case 'M':
                        if (parsed.GetOrDefault(x + 1, y - 1, ' ') == 'M' && parsed.GetOrDefault(x - 1, y + 1, ' ') == 'S' && parsed.GetOrDefault(x + 1, y + 1, ' ') == 'S')
                            found++;
                        if (parsed.GetOrDefault(x + 1, y - 1, ' ') == 'S' && parsed.GetOrDefault(x - 1, y + 1, ' ') == 'M' && parsed.GetOrDefault(x + 1, y + 1, ' ') == 'S')
                            found++;
                        break;

                    case 'S':
                        if (parsed.GetOrDefault(x + 1, y - 1, ' ') == 'S' && parsed.GetOrDefault(x - 1, y + 1, ' ') == 'M' && parsed.GetOrDefault(x + 1, y + 1, ' ') == 'M')
                            found++;
                        if (parsed.GetOrDefault(x + 1, y - 1, ' ') == 'M' && parsed.GetOrDefault(x - 1, y + 1, ' ') == 'S' && parsed.GetOrDefault(x + 1, y + 1, ' ') == 'M')
                            found++;
                        break;
                }
            }
        }

        return found;
    }
}
