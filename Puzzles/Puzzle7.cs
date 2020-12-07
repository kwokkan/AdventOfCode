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
            for (int i = 0; i < lines.Length; i++)
            {
                lines[i] = lines[i]
                    .Replace(" bags", "")
                    .Replace(" bag", "")
                    .Replace(".", "");
            }

            foreach (var line in lines)
            {
                var bagLines = line.Split(" contain ");

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

        private static void GetChildBags(IEnumerable<Bag> bags, ref int counter, params Bag[] parentBags)
        {
            foreach (var bag in parentBags)
            {
                counter++;

                foreach (var subBag in bag.Bags)
                {
                    var repeatBag = bags.First(b => b.Colour == subBag.Key);

                    for (int i = 0; i < subBag.Value; i++)
                    {
                        GetChildBags(bags, ref counter, repeatBag);
                    }
                }
            }
        }

        private static string Solve1()
        {
            var bags = GetBags(Puzzle7Input.Input).ToList();
            var foundColours = new HashSet<string>();

            GetParentBags(bags, foundColours, "shiny gold");

            return foundColours.Count.ToString();
        }

        private static string Solve2()
        {
            var bags = GetBags(Puzzle7Input.Input).ToList();
            var foundColours = 0;
            var startingBag = bags.Where(x => x.Colour == "shiny gold").ToArray();

            GetChildBags(bags, ref foundColours, startingBag);

            return (foundColours - startingBag.Length).ToString();
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
