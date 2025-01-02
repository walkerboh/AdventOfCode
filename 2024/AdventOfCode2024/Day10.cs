using System.Runtime.CompilerServices;

namespace AdventOfCode2024
{
    internal class Day10
    {
        private readonly List<List<int>> data;
        private readonly int height, width;

        public Day10()
        {
            data = File.ReadAllLines("Data\\Day10.txt").Select(d => d.Select(c => int.Parse(c.ToString())).ToList()).ToList();
            height = data.Count;
            width = data[0].Count;
        }

        public int Problem1()
        {
            var total = 0;

            for (var i = 0; i < height; i++)
            {
                for (var j = 0; j < width; j++)
                {
                    if (data[i][j] != 0)
                    {
                        continue;
                    }

                    var peaks = SearchTile(i, j);

                    total += peaks.Count;
                }
            }

            return total;

            HashSet<(int, int)> SearchTile(int row, int col)
            {
                if (data[row][col] == 9)
                {
                    return [(row, col)];
                }

                var set = new HashSet<(int, int)>();

                if (row + 1 < height && data[row + 1][col] == data[row][col] + 1)
                {
                    set = new HashSet<(int, int)>(set.Union(SearchTile(row + 1, col)));
                }

                if (row - 1 >= 0 && data[row - 1][col] == data[row][col] + 1)
                {
                    set = new HashSet<(int, int)>(set.Union(SearchTile(row - 1, col)));
                }

                if (col + 1 < width && data[row][col + 1] == data[row][col] + 1)
                {
                    set = new HashSet<(int, int)>(set.Union(SearchTile(row, col + 1)));
                }

                if (col - 1 >= 0 && data[row][col - 1] == data[row][col] + 1)
                {
                    set = new HashSet<(int, int)>(set.Union(SearchTile(row, col - 1)));
                }

                return set;
            }
        }

        public int Problem2()
        {
            var total = 0;

            for (var i = 0; i < height; i++)
            {
                for (var j = 0; j < width; j++)
                {
                    if (data[i][j] != 0)
                    {
                        continue;
                    }

                    var routes = SearchTile(i, j, []);

                    total += routes.Count;
                }
            }

            return total;

            HashSet<List<(int, int)>> SearchTile(int row, int col, List<(int, int)> nodes)
            {
                List<(int, int)> visitedNodes = [.. nodes, (row, col)];
                
                if (data[row][col] == 9)
                {
                    return [visitedNodes];
                }

                var set = new HashSet<List<(int, int)>>();

                if (row + 1 < height && data[row + 1][col] == data[row][col] + 1)
                {
                    set = new HashSet<List<(int, int)>>(set.Union(SearchTile(row + 1, col, visitedNodes)));
                }

                if (row - 1 >= 0 && data[row - 1][col] == data[row][col] + 1)
                {
                    set = new HashSet<List<(int, int)>>(set.Union(SearchTile(row - 1, col, visitedNodes)));
                }

                if (col + 1 < width && data[row][col + 1] == data[row][col] + 1)
                {
                    set = new HashSet<List<(int, int)>>(set.Union(SearchTile(row, col + 1, visitedNodes)));
                }

                if (col - 1 >= 0 && data[row][col - 1] == data[row][col] + 1)
                {
                    set = new HashSet<List<(int, int)>>(set.Union(SearchTile(row, col - 1, visitedNodes)));
                }

                return set;
            }
        }
    }
}
