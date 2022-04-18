using AdventOfCode.PuzzleCore;

namespace AdventOfCode.Puzzles2015;

public class Puzzle10 : PuzzleBase
{
    private record struct Grouper
    {
        public List<KeyValuePair<char, int>> Groups { get; private set; }

        public Grouper(string input)
        {
            Groups = new List<KeyValuePair<char, int>>();

            var previousChar = input[0];
            var currentCount = 1;

            for (int i = 1; i < input.Length; i++)
            {
                var currentChar = input[i];

                if (currentChar == previousChar)
                {
                    currentCount++;
                }
                else
                {
                    Groups.Add(KeyValuePair.Create(previousChar, currentCount));

                    previousChar = currentChar;
                    currentCount = 1;
                }
            }

            Groups.Add(KeyValuePair.Create(previousChar, currentCount));
        }

        public void Step()
        {
            var newGroups = new List<KeyValuePair<char, int>>();

            foreach (var group in Groups)
            {
                newGroups.Add(KeyValuePair.Create(group.Key, group.Value));
            }

            Groups = newGroups;
        }

        public string AsString()
        {
            return string.Join(string.Empty, Groups.Select(g => g.Value + g.Key.ToString()));
        }
    }

    public Puzzle10()
        : base(2015, 10)
    {
    }

    private static long Calculate1(string input, int iterations)
    {
        var currentInput = input;

        for (int i = 0; i < iterations; i++)
        {
            var groups = new Grouper(currentInput);
            groups.Step();
            currentInput = groups.AsString();
        }

        return currentInput.Length;
    }

    public override long ExpectedSample1 => 6;
    public override long Sample1()
    {
        var input = GetSampleString()[0];

        return Calculate1(input, 5);
    }

    public override long ExpectedAnswer1 => 360154;
    public override long Solve1()
    {
        var input = GetInputString()[0];

        return Calculate1(input, 40);
    }

    public override long ExpectedAnswer2 => 5103798;
    public override long Solve2()
    {
        var input = GetInputString()[0];

        return Calculate1(input, 50);
    }
}
