namespace AdventOfCode2015
{
    internal class Day24 : CustomDay
    {
        private readonly List<long> Weights;

        public Day24()
        {
            Weights = [.. GetInputLines().Select(long.Parse)];
        }

        public override ValueTask<string> Solve_1()
        {
            var groupSize = Weights.Sum() / 3;

            // Starting at 5 based on analysis of required size and input data
            for (var i = 5; i < Weights.Count; i++)
            {
                var combos = Weights.GetCombinations(i).Distinct();
                var valid = combos.Where(c => c.Sum() == groupSize).ToList();

                if (valid.Any())
                {
                    var minQe = valid.Min(c => c.Aggregate((res, cur) => res * cur));
                    return ValueTask.FromResult(minQe.ToString());
                }
            }

            return ValueTask.FromResult("No valid grouping found");
        }

        public override ValueTask<string> Solve_2()
        {
            var groupSize = Weights.Sum() / 4;

            // Starting at 4 based on analysis of required size and input data
            for (var i = 4; i < Weights.Count; i++)
            {
                var combos = Weights.GetCombinations(i).Distinct();
                var valid = combos.Where(c => c.Sum() == groupSize).ToList();

                if (valid.Any())
                {
                    var minQe = valid.Min(c => c.Aggregate((res, cur) => res * cur));
                    return ValueTask.FromResult(minQe.ToString());
                }
            }

            return ValueTask.FromResult("No valid grouping found");
        }
    }
}
