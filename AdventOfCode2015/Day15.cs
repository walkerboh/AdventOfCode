using System.Text.RegularExpressions;

namespace AdventOfCode2015
{
    internal partial class Day15 : CustomDay
    {
        [GeneratedRegex(@"(\w+): capacity ([\d\-]+), durability ([\d\-]+), flavor ([\d\-]+), texture ([\d\-]+), calories ([\d\-]+)")]
        private static partial Regex IngredientRegex();

        private readonly Ingredient Frosting;
        private readonly Ingredient Candy;
        private readonly Ingredient Butterscotch;
        private readonly Ingredient Sugar;

        private readonly HashSet<(int frosting, int candy, int butterscotch, int sugar)> Combinations = [];

        public Day15()
        {
            List<Ingredient> ingredients = [.. GetInputLines().Select(l => new Ingredient(l))];

            Frosting = ingredients.Single(i => i.Name == "Frosting");
            Candy = ingredients.Single(i => i.Name == "Candy");
            Butterscotch = ingredients.Single(i => i.Name == "Butterscotch");
            Sugar = ingredients.Single(i => i.Name == "Sugar");

            for (var frosting = 0; frosting <= 100; frosting++)
            {
                for (var candy = 0; candy <= 100 - frosting; candy++)
                {
                    for (var butterscotch = 0; butterscotch <= 100 - candy - frosting; butterscotch++)
                    {
                        for (var sugar = 0; sugar <= 100 - butterscotch - candy - frosting; sugar++)
                        {
                            if (sugar + butterscotch + candy + frosting != 100)
                            {
                                continue;
                            }

                            Combinations.Add((frosting, candy, butterscotch, sugar));
                        }
                    }
                }
            }
        }

        public override ValueTask<string> Solve_1()
        {
            long best = 0;

            foreach (var (frosting, candy, butterscotch, sugar) in Combinations)
            {
                var capacity = Math.Max((frosting * Frosting.Capacity) + (candy * Candy.Capacity) + (butterscotch * Butterscotch.Capacity) + (sugar * Sugar.Capacity), 0);
                var durability = Math.Max((frosting * Frosting.Durability) + (candy * Candy.Durability) + (butterscotch * Butterscotch.Durability) + (sugar * Sugar.Durability), 0);
                var flavor = Math.Max((frosting * Frosting.Flavor) + (candy * Candy.Flavor) + (butterscotch * Butterscotch.Flavor) + (sugar * Sugar.Flavor), 0);
                var texture = Math.Max((frosting * Frosting.Texture) + (candy * Candy.Texture) + (butterscotch * Butterscotch.Texture) + (sugar * Sugar.Texture), 0);

                long score = (long)capacity * durability * flavor * texture;

                if(score > best)
                {
                    best = score;
                }
            }

            return ValueTask.FromResult(best.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            long best = 0;

            foreach (var (frosting, candy, butterscotch, sugar) in Combinations)
            {
                var capacity = Math.Max((frosting * Frosting.Capacity) + (candy * Candy.Capacity) + (butterscotch * Butterscotch.Capacity) + (sugar * Sugar.Capacity), 0);
                var durability = Math.Max((frosting * Frosting.Durability) + (candy * Candy.Durability) + (butterscotch * Butterscotch.Durability) + (sugar * Sugar.Durability), 0);
                var flavor = Math.Max((frosting * Frosting.Flavor) + (candy * Candy.Flavor) + (butterscotch * Butterscotch.Flavor) + (sugar * Sugar.Flavor), 0);
                var texture = Math.Max((frosting * Frosting.Texture) + (candy * Candy.Texture) + (butterscotch * Butterscotch.Texture) + (sugar * Sugar.Texture), 0);
                var calories = Math.Max((frosting * Frosting.Calories) + (candy * Candy.Calories) + (butterscotch * Butterscotch.Calories) + (sugar * Sugar.Calories), 0);

                if(calories != 500)
                {
                    continue;
                }

                long score = (long)capacity * durability * flavor * texture;

                if (score > best)
                {
                    best = score;
                }
            }

            return ValueTask.FromResult(best.ToString());
        }

        private class Ingredient
        {
            public Ingredient(string line)
            {
                var match = IngredientRegex().Match(line);

                Name = match.Groups[1].Value;
                Capacity = int.Parse(match.Groups[2].Value);
                Durability = int.Parse(match.Groups[3].Value);
                Flavor = int.Parse(match.Groups[4].Value);
                Texture = int.Parse(match.Groups[5].Value);
                Calories = int.Parse(match.Groups[6].Value);
            }

            public string Name { get; set; }
            public int Capacity { get; set; }
            public int Durability { get; set; }
            public int Flavor { get; set; }
            public int Texture { get; set; }
            public int Calories { get; set; }
        }
    }
}
