
namespace AdventOfCode2015
{
    internal class Day17 : CustomDay
    {
        private readonly List<int> Containers;
        private readonly IEnumerable<IEnumerable<int>> Combos;
        private readonly int Target = 150;

        public Day17()
        {
            Containers = [.. GetInputLines().Select(int.Parse)];
            Combos = Containers.GetCombinations();
        }

        public override ValueTask<string> Solve_1()
        {
            return ValueTask.FromResult(Combos.Count(c => c.Sum() == Target).ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            var valid = Combos.Where(c => c.Sum() == Target);
            var minSize = valid.Min(c => c.Count());
            return ValueTask.FromResult(valid.Count(v => v.Count() == minSize).ToString());
        }
    }
}
