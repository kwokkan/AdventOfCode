using AdventOfCode.PuzzleCore;

namespace AdventOfCode.Puzzles2015;

public class Puzzle7 : PuzzleBase
{
    private enum Operand
    {
        Value, 
        And,
        Or,
        Not,
        LShift,
        RShift,
    }

    private record struct Operation
    {
        public ushort Value1 { get; set; }

        public ushort? Value2 { get; set; }

        public Operand Operand { get; set; }

        public string Destination { get; set; }
    }

    public Puzzle7()
        : base(2015, 7)
    {
    }

    private static Operation ParseLine(string input, Dictionary<string, ushort> variables)
    {
        var parts = input.Split("->", StringSplitOptions.TrimEntries);

        var ops = parts[0].Split(" ");
        var op = Operand.Value;
        ushort val1 = default;
        ushort? val2 = default;

        if (ops.Length == 1)
        {
            val1 = ushort.TryParse(ops[0], out var pVal1) ? pVal1 : variables[ops[0]];
        }
        else if (ops.Length == 2)
        {
            op = Operand.Not;
            val1 = ushort.TryParse(ops[1], out var pVal1) ? pVal1 : variables[ops[1]];
        }
        else if (ops.Length == 3)
        {
            val1 = ushort.TryParse(ops[0], out var pVal1) ? pVal1 : variables[ops[0]];
            val2 = ushort.TryParse(ops[2], out var pVal2) ? pVal2 : variables[ops[2]];

            op = Enum.Parse<Operand>(ops[1], true);
        }

        var dest = parts[1];

        return new Operation
        {
            Value1 = val1,
            Value2 = val2,
            Operand = op,
            Destination = dest,
        };
    }

    private static void PopulateVariables(string[] input, Dictionary<string, ushort> variables, bool skipB)
    {
        var invalid = new List<string>();

        foreach (var line in input)
        {
            try
            {
                var operation = ParseLine(line, variables);
                ushort newValue = default;

                switch (operation.Operand)
                {
                    case Operand.Value:
                        if (skipB && operation.Destination == "b")
                        {
                            continue;
                        }

                        newValue = operation.Value1;
                        break;
                    case Operand.And:
                        newValue = (ushort)(operation.Value1 & operation.Value2!.Value);
                        break;
                    case Operand.Or:
                        newValue = (ushort)(operation.Value1 | operation.Value2!.Value);
                        break;
                    case Operand.Not:
                        newValue = (ushort)~operation.Value1;
                        break;
                    case Operand.LShift:
                        newValue = (ushort)(operation.Value1 << operation.Value2!.Value);
                        break;
                    case Operand.RShift:
                        newValue = (ushort)(operation.Value1 >> operation.Value2!.Value);
                        break;
                    default:
                        break;
                }

                variables[operation.Destination] = newValue;
            }
            catch
            {
                invalid.Add(line);
            }
        }

        if (invalid.Any())
        {
            PopulateVariables(invalid.ToArray(), variables, skipB);
        }
    }
    
    private static long Calculate1(string[] input, string variable)
    {
        var variables = new Dictionary<string, ushort>();

        PopulateVariables(input, variables, false);

        return variables[variable];
    }

    private static long Calculate2(string[] input, string variable)
    {
        var variables = new Dictionary<string, ushort>();

        PopulateVariables(input, variables, false);

        var newStart = variables[variable];

        variables = new Dictionary<string, ushort>
        {
            { "b", newStart }
        };

        PopulateVariables(input, variables, true);

        return variables[variable];
    }

    public override long ExpectedSample1 => 65079;
    public override long Sample1()
    {
        var input = GetSampleString();

        return Calculate1(input, "i");
    }

    public override long ExpectedAnswer1 => 3176;
    public override long Solve1()
    {
        var input = GetInputString();

        return Calculate1(input, "a");
    }

    public override long ExpectedAnswer2 => 14710;
    public override long Solve2()
    {
        var input = GetInputString();

        return Calculate2(input, "a");
    }
}
