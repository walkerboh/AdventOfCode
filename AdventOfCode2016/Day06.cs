using System.Text;

namespace AdventOfCode2016
{
    internal class Day06 : CustomDay
    {
        private readonly List<string> Lines;

        public Day06()
        {
            Lines = GetInputLines();
        }

        public override ValueTask<string> Solve_1()
        {
            var sb = new StringBuilder();

            for(var i = 0; i < Lines[0].Length; i++)
            {
                sb.Append(Lines.Select(l => l[i]).GroupBy(x => x).OrderByDescending(x => x.Count()).First().Key);
            }

            return ValueTask.FromResult(sb.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            var sb = new StringBuilder();

            for (var i = 0; i < Lines[0].Length; i++)
            {
                sb.Append(Lines.Select(l => l[i]).GroupBy(x => x).OrderBy(x => x.Count()).First().Key);
            }

            return ValueTask.FromResult(sb.ToString());
        }
    }
}
