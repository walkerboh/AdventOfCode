using System.Text.RegularExpressions;

namespace AdventOfCode2015
{
    internal partial class Day13 : CustomDay
    {
        [GeneratedRegex(@"(\w+) would (\w+) (\d+) happiness units by sitting next to (\w+)\.")]
        private static partial Regex BonusRegex();

        private readonly IEnumerable<Bonus> Bonuses;

        public Day13()
        {
            Bonuses = GetInputLines().Select(l => new Bonus(l));
        }

        public override ValueTask<string> Solve_1()
        {
            List<List<string>> arrangments = [];
            var people = Bonuses.Select(b => b.Primary).Distinct();

            var permutations = people.GetPermutations(people.Count()).ToList();
            var best = 0;

            foreach(var order in permutations)
            {
                var total = 0;
                var length = order.Count();

                for(var i = 0; i < length; i++)
                {
                    var first = order.ElementAt(i);
                    var second = order.ElementAt((i + 1) % length);

                    var bonus1 = Bonuses.Single(b => b.Primary.Equals(first) && b.Neighbor.Equals(second));
                    var bonus2 = Bonuses.Single(b => b.Primary.Equals(second) && b.Neighbor.Equals(first));

                    total += bonus1.Happiness + bonus2.Happiness;
                }

                if (total > best)
                {
                    best = total;
                }
            }

            return ValueTask.FromResult(best.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            List<List<string>> arrangments = [];
            var people = Bonuses.Select(b => b.Primary).Distinct().ToList();

            List<Bonus> part2Bonuses = [.. Bonuses];

            foreach(var person in people)
            {
                part2Bonuses.Add(new Bonus("You", person, 0));
                part2Bonuses.Add(new Bonus(person, "You", 0));
            }

            people.Add("You");

            var permutations = people.GetPermutations(people.Count()).ToList();
            var best = 0;

            foreach (var order in permutations)
            {
                var total = 0;
                var length = order.Count();

                for (var i = 0; i < length; i++)
                {
                    var first = order.ElementAt(i);
                    var second = order.ElementAt((i + 1) % length);

                    var bonus1 = part2Bonuses.Single(b => b.Primary.Equals(first) && b.Neighbor.Equals(second));
                    var bonus2 = part2Bonuses.Single(b => b.Primary.Equals(second) && b.Neighbor.Equals(first));

                    total += bonus1.Happiness + bonus2.Happiness;
                }

                if (total > best)
                {
                    best = total;
                }
            }

            return ValueTask.FromResult(best.ToString());
        }

        private class Bonus
        {
            public Bonus(string bonus)
            {
                var match = BonusRegex().Match(bonus);

                Primary = match.Groups[1].Value;
                Neighbor = match.Groups[4].Value;
                var pos = match.Groups[2].Value.Equals("gain") ? 1 : -1;
                Happiness = int.Parse(match.Groups[3].Value) * pos;
            }

            public Bonus(string primary, string neighbor, int happiness)
            {
                Primary = primary;
                Neighbor = neighbor;
                Happiness = happiness;
            }

            public string Primary { get; set; }
            public string Neighbor { get; set; }
            public int Happiness { get; set; }
        }
    }
}
