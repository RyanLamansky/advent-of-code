namespace Advent.Year2024.Day17;

using System.Diagnostics;
using Parsed = (uint A, uint B, uint C, byte[] Program);
using Result = string;

public enum OpCode : byte
{
    adv,
    bxl,
    bst,
    jnz,
    bxc,
    @out,
    bdv,
    cdv,
}

public sealed class Answer : IPuzzle<Parsed, Result>
{
    public Parsed Parse(IEnumerable<string> input)
    {
        using var lines = input.GetEnumerator();

        lines.MoveNext();
        var a = uint.Parse(lines.Current.AsSpan(12));
        lines.MoveNext();
        var b = uint.Parse(lines.Current.AsSpan(12));
        lines.MoveNext();
        var c = uint.Parse(lines.Current.AsSpan(12));

        lines.MoveNext();
        lines.MoveNext();
        var program = lines.Current[9..].Split(',').Select(byte.Parse).ToArray();

        return (a, b, c, program);
    }

    static IEnumerable<byte> RunProgram(byte[] program, uint[] registers, uint a)
    {
        registers[0] = 0;
        registers[1] = 1;
        registers[2] = 2;
        registers[3] = 3;
        registers[4] = a;
        registers[5] = 0;
        registers[6] = 0;

        for (var i = 0; i + 1 < program.Length; i += 2)
        {
            var opCode = (OpCode)program[i];
            var operand = program[i + 1];

            switch (opCode)
            {
                case OpCode.adv:
                    registers[4] = registers[4] / (uint)Math.Pow(2, registers[operand]);
                    continue;
                case OpCode.bxl:
                    registers[5] = registers[5] ^ operand;
                    continue;
                case OpCode.bst:
                    registers[5] = registers[operand] % 8;
                    continue;
                case OpCode.jnz:
                    if (registers[4] == 0)
                        continue;

                    i = operand - 2;
                    continue;
                case OpCode.bxc:
                    registers[5] = registers[5] ^ registers[6];
                    continue;
                case OpCode.@out:
                    yield return ((byte)(registers[operand] % 8));
                    continue;
                case OpCode.bdv:
                    registers[5] = registers[4] / (uint)Math.Pow(2, registers[operand]);
                    continue;
                case OpCode.cdv:
                    registers[6] = registers[4] / (uint)Math.Pow(2, registers[operand]);
                    continue;
            }
        }
    }

    public Result Part1(Parsed parsed)
        => string.Join(',', RunProgram(parsed.Program, new uint[8], parsed.A));

    public Result Part2(Parsed parsed)
    {
        var program = parsed.Program;

        // This returns the correct answer of 117440 for sample2.txt.
        // At the time of writing, it checks 20 million values per second on 7800X3D.
        // It's not fast enough to fully evaluate the full range of possibilities.
        // Brute force is definitely the wrong way to solve this.

        if (parsed.A != 2024) //sample2.txt's "A" value.
            return "?";

        Span<ulong> registers = stackalloc ulong[8];
        registers[0] = 0;
        registers[1] = 1;
        registers[2] = 2;
        registers[3] = 3;

        var start = Stopwatch.GetTimestamp();
        var outputInterval = Stopwatch.Frequency * 5;
        var next = start + outputInterval;

        for (ulong a = 0; a < ulong.MaxValue; a++)
        {
            if (Stopwatch.GetTimestamp() > next)
            {
                Console.WriteLine(a.ToString("#,##0"));
                start = Stopwatch.GetTimestamp();
                next = start + outputInterval;
            }

            var index = 0;

            registers[4] = a;
            registers[5] = 0;
            registers[6] = 0;

            for (var i = 0; i + 1 < program.Length; i += 2)
            {
                var opCode = (OpCode)program[i];
                var operand = program[i + 1];

                switch (opCode)
                {
                    case OpCode.adv:
                        registers[4] = registers[4] / (ulong)Math.Pow(2, registers[operand]);
                        continue;
                    case OpCode.bxl:
                        registers[5] = registers[5] ^ operand;
                        continue;
                    case OpCode.bst:
                        registers[5] = registers[operand] % 8;
                        continue;
                    case OpCode.jnz:
                        if (registers[4] == 0)
                            continue;

                        i = operand - 2;
                        continue;
                    case OpCode.bxc:
                        registers[5] = registers[5] ^ registers[6];
                        continue;
                    case OpCode.@out:
                        if (program[index++] != ((byte)(registers[operand] % 8)))
                            goto Nope;

                        continue;
                    case OpCode.bdv:
                        registers[5] = registers[4] / (ulong)Math.Pow(2, registers[operand]);
                        continue;
                    case OpCode.cdv:
                        registers[6] = registers[4] / (ulong)Math.Pow(2, registers[operand]);
                        continue;
                }
            }

            if (index == program.Length)
                return a.ToString();
        Nope:;
        }

        return "?";
    }
}
