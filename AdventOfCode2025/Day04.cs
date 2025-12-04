namespace AdventOfCode2025
{
    internal class Day04 : CustomDay
    {
        private readonly List<List<char>> Rows;

        public Day04()
        {
            Rows = [.. GetInputLines().Select(s => s.ToList())];
        }

        public override ValueTask<string> Solve_1()
        {
            var total = 0;

            for(var i = 0; i < Rows.Count; i++)
            {
                var row = Rows[i];

                for(var j = 0; j < row.Count; j++)
                {
                    if (row[j] != '@')
                    {
                        continue;
                    }

                    var neighbors = 0;
                    var minRowBound = Math.Max(0, j - 1);
                    var maxRowBound = Math.Min(row.Count - 1, j + 1) + 1;

                    if (i > 0)
                    {
                        neighbors += Rows[i - 1][minRowBound..maxRowBound].Count(c => c == '@');
                    }

                    if(j > 0 && row[j - 1] == '@')
                    {
                        neighbors++;
                    }

                    if(j < row.Count - 1 && row[j + 1] =='@')
                    {
                        neighbors++;
                    }

                    if(i < row.Count - 1)
                    {
                        neighbors += Rows[i + 1][minRowBound..maxRowBound].Count(c => c == '@');
                    }

                    if(neighbors < 4)
                    {
                        total++;
                    }
                }
            }

            return ValueTask.FromResult(total.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            var total = 0;

            List<List<char>> curRows = [.. Rows];

            var canRemove = CanRemove();

            while(canRemove.Any())
            {
                total += canRemove.Count();

                foreach(var (i, j) in canRemove)
                {
                    curRows[i][j] = '.';
                }

                canRemove = CanRemove();
            }

            return ValueTask.FromResult(total.ToString());

            IEnumerable<(int i, int j)> CanRemove()
            {
                List<(int i, int j)> canRemove = [];

                for (var i = 0; i < curRows.Count; i++)
                {
                    var row = curRows[i];

                    for (var j = 0; j < row.Count; j++)
                    {
                        if (row[j] != '@')
                        {
                            continue;
                        }

                        var neighbors = 0;
                        var minRowBound = Math.Max(0, j - 1);
                        var maxRowBound = Math.Min(row.Count - 1, j + 1) + 1;

                        if (i > 0)
                        {
                            neighbors += curRows[i - 1][minRowBound..maxRowBound].Count(c => c == '@');
                        }

                        if (j > 0 && row[j - 1] == '@')
                        {
                            neighbors++;
                        }

                        if (j < row.Count - 1 && row[j + 1] == '@')
                        {
                            neighbors++;
                        }

                        if (i < row.Count - 1)
                        {
                            neighbors += curRows[i + 1][minRowBound..maxRowBound].Count(c => c == '@');
                        }

                        if (neighbors < 4)
                        {
                            canRemove.Add((i, j));
                        }
                    }
                }

                return canRemove;
            }
        }
    }
}
