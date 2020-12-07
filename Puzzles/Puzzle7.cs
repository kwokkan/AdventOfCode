using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Puzzles
{
    public class Puzzle7 : IPuzzle
    {
        private record Bag
        {
            public string Colour { get; init; }

            public IDictionary<string, int> Bags { get; init; } = new Dictionary<string, int>();

            public Bag(string colour)
            {
                Colour = colour;
            }
        }

        private static IEnumerable<Bag> GetBags(string[] lines)
        {
            foreach (var line in lines)
            {
                var replacedLine = line
                    .Replace(" bags", "")
                    .Replace(" bag", "")
                    .Replace(".", "");
                var bagLines = replacedLine.Split(" contain ");

                var bag = new Bag(bagLines[0]);

                var bagColours = bagLines[1].Split(", ");

                foreach (var colour in bagColours)
                {
                    var colourCount = colour.Split(" ", 2);

                    if (int.TryParse(colourCount[0], out var bagCount))
                    {
                        bag.Bags.Add(colourCount[1], bagCount);
                    }
                }

                yield return bag;
            }
        }

        private static void GetParentBags(IEnumerable<Bag> bags, HashSet<string> foundColours, params string[] colours)
        {
            var foundBags = bags
                .Where(x => x.Bags.Keys.Any(key => colours.Contains(key)))
                .Select(x => x.Colour)
                .ToArray();

            var hasNewBags = foundBags.Select(x => foundColours.Add(x)).ToList().Any(x => x);

            if (hasNewBags)
            {
                GetParentBags(bags, foundColours, foundBags);
            }
        }

        private static void GetChildBags(IEnumerable<Bag> bags, List<KeyValuePair<string, int>> allBags, params Bag[] parentBags)
        {
            foreach (var bag in parentBags)
            {
                allBags.Add(KeyValuePair.Create(bag.Colour, 1));

                var subBags = bag.Bags
                    .SelectMany(x => Enumerable.Repeat(bags.Where(b => b.Colour == x.Key).ToArray(), x.Value))
                    .SelectMany(x => x)
                    .ToArray();

                GetChildBags(bags, allBags, subBags);
            }
        }

        private static string Solve1()
        {
            var bags = GetBags(Puzzle7Input.Input);
            var foundColours = new HashSet<string>();

            GetParentBags(bags, foundColours, "shiny gold");

            return foundColours.Count.ToString();
        }

        private static string Solve2()
        {
            var bags = GetBags(Puzzle7Input.Input);
            var foundColours = new List<KeyValuePair<string, int>>();
            var startingBag = bags.Where(x => x.Colour == "shiny gold").ToArray();

            GetChildBags(bags, foundColours, startingBag);

            // > 136, < 30900
            return (foundColours.Select(x => x.Value).Sum() - startingBag.Count()).ToString();
        }

        Solution IPuzzle.Solve()
        {
            return new Solution
            (
                Solve1(),
                Solve2()
            );
        }
    }
}
