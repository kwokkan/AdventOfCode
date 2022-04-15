using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode.PuzzleCore;

namespace AdventOfCode.Puzzles2020;

public class Puzzle4 : PuzzleBase
{
    private record Passport
    {
        private readonly static Regex HclRegex = new Regex("#[a-f0-9]{6}");
        private readonly static Regex PidRegex = new Regex("^\\d{9}$");

        private IList<string> Fields { get; init; } = new List<string>();
        private IList<string> Values { get; init; } = new List<string>();

        public Passport(IEnumerable<string> fields, IEnumerable<string> values) => (Fields, Values) = (new List<string>(fields), new List<string>(values));

        private static bool InRange(string value, int min, int max)
        {
            return int.TryParse(value, out var val) && val >= min && val <= max;
        }

        public bool HasFields()
        {
            return Fields.Count == 8 || (Fields.Count == 7 && !Fields.Any(x => x == "cid"));
        }

        public bool IsValid()
        {
            if (Fields.Count != Values.Count)
            {
                return false;
            }

            for (int i = 0; i < Fields.Count; i++)
            {
                var currentField = Fields[i];
                var currentValue = Values[i];

                switch (currentField)
                {
                    case "byr":
                        {
                            if (!InRange(currentValue, 1920, 2002))
                            {
                                return false;
                            }

                            break;
                        }
                    case "iyr":
                        {
                            if (!InRange(currentValue, 2010, 2020))
                            {
                                return false;
                            }

                            break;
                        }
                    case "eyr":
                        {
                            if (!InRange(currentValue, 2020, 2030))
                            {
                                return false;
                            }

                            break;
                        }
                    case "hgt":
                        {
                            if (currentValue.EndsWith("cm") && currentValue.Length == 5 && InRange(currentValue.Substring(0, 3), 150, 193))
                            {
                                break;
                            }
                            else if (currentValue.EndsWith("in") && currentValue.Length == 4 && InRange(currentValue.Substring(0, 2), 59, 76))
                            {
                                break;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    case "hcl":
                        {
                            if (!HclRegex.IsMatch(currentValue))
                            {
                                return false;
                            }

                            break;
                        }
                    case "ecl":
                        {
                            var colours = new List<string>()
                            {
                                "amb",
                                "blu",
                                "brn",
                                "gry",
                                "grn",
                                "hzl",
                                "oth",
                            };

                            if (!colours.Contains(currentValue))
                            {
                                return false;
                            }

                            break;
                        }
                    case "pid":
                        {
                            if (!PidRegex.IsMatch(currentValue))
                            {
                                return false;
                            }

                            break;
                        }
                    case "cid":
                        {
                            break;
                        }
                    default:
                        return false;
                }
            }

            return true;
        }
    }

    public Puzzle4()
        : base(2020, 4)
    {
    }

    private static IEnumerable<Passport> GetPassports(string[] lines)
    {
        var allFields = new List<string>();
        var allValues = new List<string>();

        foreach (var line in lines)
        {
            if (line == string.Empty)
            {
                var passport = new Passport(allFields, allValues);

                allFields.Clear();
                allValues.Clear();

                yield return passport;

                continue;
            }

            var parts = line.Split(' ');
            var fields = parts.Select(x => x.Split(':')[0]).ToArray();
            var values = parts.Select(x => x.Split(':')[1]).ToArray();
            allFields.AddRange(fields);
            allValues.AddRange(values);
        }

        yield return new Passport(allFields, allValues);
    }

    public override long Solve1()
    {
        var passports = GetPassports(Puzzle4Input.Input).ToList();
        var count = 0;

        foreach (var passport in passports)
        {
            if (passport.HasFields())
            {
                count++;
            }
        }

        return count;
    }

    public override long Solve2()
    {
        var passports = GetPassports(Puzzle4Input.Input).ToList();
        var count = 0;

        foreach (var passport in passports)
        {
            if (passport.HasFields() && passport.IsValid())
            {
                count++;
            }
        }

        return count;
    }
}
