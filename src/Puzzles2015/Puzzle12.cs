using System.Text.Json;
using System.Text.RegularExpressions;
using AdventOfCode.PuzzleCore;

namespace AdventOfCode.Puzzles2015;

public class Puzzle12 : PuzzleBase
{
    private static readonly Regex _regex = new Regex("\\-?\\d+");

    public Puzzle12()
        : base(2015, 12)
    {
    }

    private static long Calculate1(string input)
    {
        var matches = _regex.Matches(input);

        return matches.Select(x => int.Parse(x.Value)).Sum();
    }

    private static void SumJson2(JsonElement element, ref long sum)
    {
        if (element.ValueKind == JsonValueKind.Object)
        {
            var properties = element.EnumerateObject();

            var hasRed = properties.Any(x => x.Value.ValueKind == JsonValueKind.String && x.Value.GetString() == "red");

            if (hasRed)
            {
                return;
            }

            foreach (var property in properties)
            {
                if (property.Value.ValueKind == JsonValueKind.Number)
                {
                    sum += property.Value.GetInt64();
                }
                else if (property.Value.ValueKind == JsonValueKind.Object)
                {
                    SumJson2(property.Value, ref sum);
                }
                else if (property.Value.ValueKind == JsonValueKind.Array)
                {
                    SumJson2(property.Value, ref sum);
                }
            }
        }
        else if (element.ValueKind == JsonValueKind.Array)
        {
            foreach (var node in element.EnumerateArray())
            {
                if (node.ValueKind == JsonValueKind.Number)
                {
                    sum += node.GetInt64();
                }
                else if (node.ValueKind == JsonValueKind.Object)
                {
                    SumJson2(node, ref sum);
                }
                else if (node.ValueKind == JsonValueKind.Array)
                {
                    SumJson2(node, ref sum);
                }
            }
        }
    }

    private static long Calculate2(string input)
    {
        var document = JsonDocument.Parse(input);
        var sum = 0L;

        SumJson2(document.RootElement, ref sum);

        return sum;
    }

    public override long ExpectedSample1 => 3;
    public override long Sample1()
    {
        var input = GetSampleString()[0];

        return Calculate1(input);
    }

    public override long ExpectedAnswer1 => 119433;
    public override long Solve1()
    {
        var input = GetInputString()[0];

        return Calculate1(input);
    }

    public override long ExpectedAnswer2 => 68466; 
    public override long Solve2()
    {
        var input = GetInputString()[0];

        return Calculate2(input);
    }
}
