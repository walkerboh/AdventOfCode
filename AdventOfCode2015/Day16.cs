using System.Text.RegularExpressions;

namespace AdventOfCode2015
{
    internal partial class Day16 : CustomDay
    {
        [GeneratedRegex(@"Sue (\d{1,3}): (.*)")]
        private static partial Regex SueRegex();

        private readonly List<Sue> Sues;

        private readonly Sue SueStat = new("Sue 0: children: 3, cats: 7, samoyeds: 2, pomeranians: 3, akitas: 0, vizslas: 0, goldfish: 5, trees: 3, cars: 2, perfumes: 1");

        public Day16()
        {
            Sues = [.. GetInputLines().Select(l => new Sue(l))];
        }

        public override ValueTask<string> Solve_1()
        {
            var sue = Sues.Single(s =>
                (!s.Children.HasValue || s.Children.Value.Equals(SueStat.Children))
                    && (!s.Cats.HasValue || s.Cats.Value.Equals(SueStat.Cats))
                    && (!s.Samoyeds.HasValue || s.Samoyeds.Value.Equals(SueStat.Samoyeds))
                    && (!s.Pomeranians.HasValue || s.Pomeranians.Value.Equals(SueStat.Pomeranians))
                    && (!s.Akitas.HasValue || s.Akitas.Value.Equals(SueStat.Akitas))
                    && (!s.Vizslas.HasValue || s.Vizslas.Value.Equals(SueStat.Vizslas))
                    && (!s.Goldfish.HasValue || s.Goldfish.Value.Equals(SueStat.Goldfish))
                    && (!s.Trees.HasValue || s.Trees.Value.Equals(SueStat.Trees))
                    && (!s.Cars.HasValue || s.Cars.Value.Equals(SueStat.Cars))
                    && (!s.Perfumes.HasValue || s.Perfumes.Value.Equals(SueStat.Perfumes))
            );

            return ValueTask.FromResult(sue.Id.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            var sue = Sues.Single(s =>
                (!s.Children.HasValue || s.Children.Value.Equals(SueStat.Children))
                    && (!s.Cats.HasValue || s.Cats.Value > SueStat.Cats)
                    && (!s.Samoyeds.HasValue || s.Samoyeds.Value.Equals(SueStat.Samoyeds))
                    && (!s.Pomeranians.HasValue || s.Pomeranians.Value < SueStat.Pomeranians)
                    && (!s.Akitas.HasValue || s.Akitas.Value.Equals(SueStat.Akitas))
                    && (!s.Vizslas.HasValue || s.Vizslas.Value.Equals(SueStat.Vizslas))
                    && (!s.Goldfish.HasValue || s.Goldfish.Value < SueStat.Goldfish)
                    && (!s.Trees.HasValue || s.Trees.Value > SueStat.Trees)
                    && (!s.Cars.HasValue || s.Cars.Value.Equals(SueStat.Cars))
                    && (!s.Perfumes.HasValue || s.Perfumes.Value.Equals(SueStat.Perfumes))
            );

            return ValueTask.FromResult(sue.Id.ToString());
        }

        private class Sue
        {
            public Sue(string line)
            {
                var match = SueRegex().Match(line);
                Id = int.Parse(match.Groups[1].Value);

                var stats = match.Groups[2].Value.Split(", ", StringSplitOptions.TrimEntries);
                foreach (var stat in stats)
                {
                    var split = stat.Split(':', StringSplitOptions.TrimEntries);
                    var value = int.Parse(split[1]);

                    switch (split[0])
                    {
                        case "children":
                            Children = value;
                            break;
                        case "cats":
                            Cats = value;
                            break;
                        case "samoyeds":
                            Samoyeds = value;
                            break;
                        case "pomeranians":
                            Pomeranians = value;
                            break;
                        case "akitas":
                            Akitas = value;
                            break;
                        case "vizslas":
                            Vizslas = value;
                            break;
                        case "goldfish":
                            Goldfish = value;
                            break;
                        case "trees":
                            Trees = value;
                            break;
                        case "cars":
                            Cars = value;
                            break;
                        case "perfumes":
                            Perfumes = value;
                            break;
                    }
                }
            }

            public int Id { get; set; }
            public int? Children { get; set; }
            public int? Cats { get; set; }
            public int? Samoyeds { get; set; }
            public int? Pomeranians { get; set; }
            public int? Akitas { get; set; }
            public int? Vizslas { get; set; }
            public int? Goldfish { get; set; }
            public int? Trees { get; set; }
            public int? Cars { get; set; }
            public int? Perfumes { get; set; }
        }
    }
}
