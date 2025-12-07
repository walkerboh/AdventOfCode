namespace AdventOfCode2025
{
    internal class Day07 : CustomDay
    {
        private readonly List<string> ManifoldRows;

        public Day07()
        {
            ManifoldRows = GetInputLines();
        }

        public override ValueTask<string> Solve_1()
        {
            var beamColumns = new HashSet<int>();
            var startBeamCol = ManifoldRows.First().IndexOf('S');
            var splits = 0;

            beamColumns.Add(startBeamCol);

            foreach(var row in ManifoldRows[1..])
            {
                for(var col = 0; col < row.Length; col++)
                {
                    if (row[col] == '^' && beamColumns.Contains(col))
                    {
                        splits++;
                        beamColumns.Remove(col);
                        if(col > 0)
                        {
                            beamColumns.Add(col - 1);
                        }

                        if(col < row.Length - 1)
                        {
                            beamColumns.Add(col + 1);
                        }
                    }
                }
            }

            return ValueTask.FromResult(splits.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            var beamPaths = new List<(string path, int col)>();
            var startBeamCol = ManifoldRows.First().IndexOf('S');

            var memo = new Dictionary<(int x, int y), long>();

            var total = GetBranchingPaths((0, startBeamCol));

            return ValueTask.FromResult(total.ToString());

            long GetBranchingPaths((int x, int y) cur)
            {
                if(memo.TryGetValue(cur, out var branchingPaths))
                {
                    return branchingPaths;
                }

                if(cur.x + 1 == ManifoldRows.Count)
                {
                    return 1;
                }

                if (ManifoldRows[cur.x + 1][cur.y] == '^')
                {
                    memo.Add(cur, GetBranchingPaths((cur.x + 1, cur.y - 1)) + GetBranchingPaths((cur.x + 1, cur.y + 1)));
                }
                else
                {
                    memo.Add(cur, GetBranchingPaths((cur.x + 1, cur.y)));
                }

                return memo[cur];
            }

            return ValueTask.FromResult(beamPaths.Count.ToString());
        }
    }
}
