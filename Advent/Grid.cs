using System.Collections;
using System.Diagnostics;

namespace Advent;

/// <summary>
/// Simplifies work involving a 2-dimensional grid, internally wrapping a 2-dimensional array.
/// </summary>
/// <typeparam name="T">The type of the grid's elements.</typeparam>
[DebuggerDisplay("Grid {Width}/{Height}")]
public readonly struct Grid<T> : IEnumerable<(int X, int Y, T Value)> where T : notnull, IEquatable<T>
{
    readonly T[,] values;

    public int Width => values.GetLength(0);

    public int Height => values.GetLength(1);

    /// <summary>
    /// Creates a new <see cref="Grid{T}"/> from the provided input, converted using the provided parser.
    /// </summary>
    /// <param name="input">Puzzle input.</param>
    /// <param name="parser">Converts characters from the input into values of <typeparamref name="T"/>.</param>
    public Grid(IEnumerable<string> input, Func<char, T> parser)
    {
        var rawValues = new List<T[]>();
        rawValues.AddRange(input.Select(line => line.Select(parser).ToArray()));

        var values = this.values = new T[rawValues[0].Length, rawValues.Count];

        for (var y = 0; y < values.GetLength(1); y++)
        {
            var row = rawValues[y];

            for (var x = 0; x < values.GetLength(0); x++)
                values[x, y] = row[x];
        }
    }

    private Grid(T[,] values) => this.values = values;

    /// <summary>
    /// Returns the value at the indicated location or <paramref name="outOfRange"/> if not within the grid.
    /// </summary>
    /// <param name="x">The "x" coordinate.</param>
    /// <param name="y">The "y" coordinate.</param>
    /// <param name="outOfRange">The value to return when <paramref name="x"/> or <paramref name="y"/> are outside of the grid.</param>
    /// <returns>The value at the location or <paramref name="outOfRange"/> if out of range.</returns>
    public T GetOrDefault(int x, int y, T outOfRange) =>
        x < 0 || y < 0 || x >= values.GetLength(0) || y >= values.GetLength(1) ? outOfRange : values[x, y];

    /// <summary>
    /// Returns the value at the indicated location or <paramref name="outOfRange"/> if not within the grid.
    /// </summary>
    /// <param name="location">The x/y coordinates.</param>
    /// <param name="outOfRange">The value to return when <paramref name="location"/> is outside of the grid.</param>
    /// <returns>The value at the location or <paramref name="outOfRange"/> if out of range.</returns>
    public T GetOrDefault((int x, int y) location, T outOfRange)
        => GetOrDefault(location.x, location.y, outOfRange);

    /// <summary>
    /// Uses the provided logic to modify the value at the provided coordinates, if they're in range.
    /// If <paramref name="x"/> or <paramref name="y"/> are out of range, nothing happens.
    /// </summary>
    /// <param name="x">The "x" coordinate.</param>
    /// <param name="y">The "y" coordinate.</param>
    /// <param name="update">The process to update the value at the specified location.</param>
    public void TryModify(int x, int y, Func<T, T> update)
    {
        if (x < 0 || y < 0 || x >= values.GetLength(0) || y >= values.GetLength(1))
            return;

        values[x, y] = update(values[x, y]);
    }

    public bool TrySet((int x, int y) location, T value)
    {
        var (x, y) = location;

        if (x < 0 || y < 0 || x >= values.GetLength(0) || y >= values.GetLength(1))
            return false;

        values[x, y] = value;
        return true;
    }

    public bool IsValidLocation((int x, int y) location) => IsValidLocation(location.x, location.y);

    public bool IsValidLocation(int x, int y) => x >= 0 && y >= 0 && x < values.GetLength(0) && y < values.GetLength(1);

    /// <summary>
    /// Gets or sets the value at the provided location within the grid.
    /// </summary>
    /// <param name="x">The "x" coordinate.</param>
    /// <param name="y">The "y" coordinate.</param>
    /// <returns>The value at the location.</returns>
    /// <exception cref="IndexOutOfRangeException">The coordinates are outside the range of the grid.</exception>
    public T this[int x, int y]
    {
        get => values[x, y];
        set => values[x, y] = value;
    }

    /// <summary>
    /// Gets or sets the value at the provided location within the grid.
    /// </summary>
    /// <param name="location">The x/y coordinates.</param>
    /// <returns>The value at the location.</returns>
    /// <exception cref="IndexOutOfRangeException">The coordinates are outside the range of the grid.</exception>
    public T this[(int x, int y) location]
    {
        get => values[location.x, location.y];
        set => values[location.x, location.y] = value;
    }

    /// <summary>
    /// Enumerates the content of the grid without requiring a heap memory allocation.
    /// </summary>
    public struct Enumerator
    {
        readonly T[,] values;
        int x, y;

        internal Enumerator(Grid<T> grid)
        {
            x = -1;
            this.values = grid.values;
        }

        /// <summary>
        /// Gets the element in the collection at the current position of the enumerator.
        /// </summary>
        public readonly (int X, int Y, T Value) Current => (x, y, values[x, y]);

        /// <summary>
        /// Advances the enumerator to the next element of the collection.
        /// </summary>
        /// <returns>True if the enumerator was successfully advanced to the next element, otherwise false.</returns>
        public bool MoveNext()
        {
            if (x + 1 == values.GetLength(0))
            {
                if (y + 1 == values.GetLength(1))
                    return false;

                x = -1;
                y++;
            }

            x++;
            return true;
        }
    }

    public (int X, int Y) Find(T value)
    {
        foreach (var (x, y, v) in this)
        {
            if (value.Equals(v))
                return (x, y);
        }

        throw new Exception($"Value {value} not found.");
    }

    public Grid<T> Clone() => new((T[,])values.Clone());

    /// <summary>
    /// Provides an enumerator to iterate through the collection.
    /// Modification during enumeration is permitted.
    /// </summary>
    /// <returns>An enumerator to iterate through the collection.</returns>
    public Enumerator GetEnumerator() => new (this);

    IEnumerator<(int X, int Y, T Value)> IEnumerable<(int X, int Y, T Value)>.GetEnumerator()
    {
        var values = this.values;
        for (var y = 0; y < values.GetLength(1); y++)
            for (var x = 0; x < values.GetLength(0); x++)
                yield return (x, y, values[x, y]);
    }

    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<(int X, int Y, T Value)>)this).GetEnumerator();

    public void Write(Action<T> value, Action newLine)
    {
        for (var y = 0; y < this.Height; y++)
        {
            for (var x = 0; x < this.Width; x++)
                value(this[x, y]);

            newLine();
        }
    }
}

public static class GridExtensions
{
    public static void Write(this Grid<char> grid)
        => grid.Write(Console.Write, Console.WriteLine);
}
