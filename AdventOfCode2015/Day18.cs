namespace AdventOfCode2015
{
    internal class Day18 : CustomDay
    {
        private const int GridSize = 100;

        public override ValueTask<string> Solve_1()
        {
            List<List<char>> currentData = [.. GetInputLines().Select(l => l.ToList())];

            for (var round = 0; round < 100; round++)
            {
                List<List<char>> nextData = [];

                for (var i = 0; i < currentData.Count; i++)
                {
                    var row = currentData[i];
                    var newRow = new List<char>();

                    for (var j = 0; j < row.Count; j++)
                    {
                        var neighbors = 0;
                        var minRowBound = Math.Max(0, j - 1);
                        var maxRowBound = Math.Min(row.Count - 1, j + 1) + 1;

                        if (i > 0)
                        {
                            neighbors += currentData[i - 1][minRowBound..maxRowBound].Count(c => c == '#');
                        }

                        if (j > 0 && row[j - 1] == '#')
                        {
                            neighbors++;
                        }

                        if (j < row.Count - 1 && row[j + 1] == '#')
                        {
                            neighbors++;
                        }

                        if (i < row.Count - 1)
                        {
                            neighbors += currentData[i + 1][minRowBound..maxRowBound].Count(c => c == '#');
                        }

                        if (row[j] == '#')
                        {
                            newRow.Add((neighbors == 2 || neighbors == 3) ? '#' : '.');
                        }
                        else
                        {
                            newRow.Add(neighbors == 3 ? '#' : '.');
                        }
                    }

                    nextData.Add(newRow);
                }

                currentData = nextData;
            }

            return ValueTask.FromResult(currentData.SelectMany(x => x).Count(x => x == '#').ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            List<List<char>> currentData = [.. GetInputLines().Select(l => l.ToList())];

            currentData[0][0] = '#';
            currentData[0][^1] = '#';
            currentData[^1][0] = '#';
            currentData[^1][^1] = '#';

            for (var round = 0; round < 100; round++)
            {
                List<List<char>> nextData = [];

                for (var i = 0; i < currentData.Count; i++)
                {
                    var row = currentData[i];
                    var newRow = new List<char>();

                    for (var j = 0; j < row.Count; j++)
                    {
                        if ((i == 0 || i == currentData.Count - 1) && (j == 0 || j == row.Count - 1))
                        {
                            newRow.Add('#');
                            continue;
                        }

                        var neighbors = 0;
                        var minRowBound = Math.Max(0, j - 1);
                        var maxRowBound = Math.Min(row.Count - 1, j + 1) + 1;

                        if (i > 0)
                        {
                            neighbors += currentData[i - 1][minRowBound..maxRowBound].Count(c => c == '#');
                        }

                        if (j > 0 && row[j - 1] == '#')
                        {
                            neighbors++;
                        }

                        if (j < row.Count - 1 && row[j + 1] == '#')
                        {
                            neighbors++;
                        }

                        if (i < row.Count - 1)
                        {
                            neighbors += currentData[i + 1][minRowBound..maxRowBound].Count(c => c == '#');
                        }

                        if (row[j] == '#')
                        {
                            newRow.Add((neighbors == 2 || neighbors == 3) ? '#' : '.');
                        }
                        else
                        {
                            newRow.Add(neighbors == 3 ? '#' : '.');
                        }
                    }

                    nextData.Add(newRow);
                }

                currentData = nextData;
            }

            return ValueTask.FromResult(currentData.SelectMany(x => x).Count(x => x == '#').ToString());
        }
    }
}
