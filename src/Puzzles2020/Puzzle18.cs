using System;
using System.Linq;
using AdventOfCode.PuzzleCore;

namespace AdventOfCode.Puzzles2020;

public class Puzzle18 : PuzzleBase
{
    public Puzzle18()
        : base(2020, 18)
    {
    }

    private static string SanitiseExpression(string expression, bool isAdvancedExpression)
    {
        var startIndex = expression.LastIndexOf("(");
        if (startIndex == -1)
        {
            return expression;
        }

        var endIndex = expression.IndexOf(")", startIndex);
        var subExpression = expression.Substring(startIndex + 1, endIndex - startIndex - 1);
        var expressionTotal = isAdvancedExpression ? GetAdvancedExpressionTotal(subExpression) : GetExpressionTotal(subExpression);

        var newExpression = expression.Remove(startIndex, endIndex - startIndex + 1);
        newExpression = newExpression.Insert(startIndex, expressionTotal.ToString());

        if (newExpression.IndexOf("(") > -1)
        {
            newExpression = SanitiseExpression(newExpression, isAdvancedExpression);
        }

        return newExpression;
    }

    private static long GetExpressionTotal(string expression)
    {
        var parts = expression.Split(" ", StringSplitOptions.RemoveEmptyEntries);

        var total = long.Parse(parts[0]);

        string? prevOperator = parts[1];

        for (int i = 2; i < parts.Length; i++)
        {
            var currentPart = parts[i];

            switch (currentPart)
            {
                case "+":
                    prevOperator = currentPart;
                    break;
                case "*":
                    prevOperator = currentPart;
                    break;
                default:
                    switch (prevOperator)
                    {
                        case "+":
                            total += long.Parse(currentPart);
                            break;
                        case "*":
                            total *= long.Parse(currentPart);
                            break;
                    }

                    break;
            }
        }

        return total;
    }

    private static long GetAdvancedExpressionTotal(string expression)
    {
        var total = 0L;

        var parts = expression.Split(" ", StringSplitOptions.RemoveEmptyEntries).ToList();

        var precedenceIndex = parts.IndexOf("+");

        while (precedenceIndex > -1)
        {
            var precedenceTotal = long.Parse(parts[precedenceIndex - 1]) + long.Parse(parts[precedenceIndex + 1]);

            parts.RemoveRange(precedenceIndex - 1, 3);
            parts.Insert(precedenceIndex - 1, precedenceTotal.ToString());

            precedenceIndex = parts.IndexOf("+");
        }

        total = parts.Where(x => x != "*").Select(long.Parse).Aggregate((x, y) => x * y);

        return total;
    }

    private static long GetTotal(string[] lines, bool isAdvancedExpression)
    {
        var total = 0L;

        foreach (var line in lines)
        {
            var sanitised = SanitiseExpression(line, isAdvancedExpression);
            total += isAdvancedExpression ? GetAdvancedExpressionTotal(sanitised) : GetExpressionTotal(sanitised);
        }

        return total;
    }

    public override long Sample1()
    {
        var input = Puzzle18Input.Sample;
        var total = GetTotal(input, false);

        return total;
    }

    public override long Solve1()
    {
        var input = Puzzle18Input.Input;
        var total = GetTotal(input, false);

        return total;
    }

    public override long Solve2()
    {
        var input = Puzzle18Input.Input;
        var total = GetTotal(input, true);

        return total;
    }
}
