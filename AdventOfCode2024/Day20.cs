namespace AdventOfCode2024
{
    internal class Day20
    {
        private readonly List<List<char>> map;
        private readonly List<(int Row, int Col)> path = [];

        public Day20()
        {
            map = File.ReadAllLines("Data\\Day20.txt").Select(l => l.ToList()).ToList();

            int curRow = 0, curCol = 0;

            for(var i = 0; i < map.Count; i++)
            {
                for(var j = 0; j < map[i].Count; j++)
                {
                    if (map[i][j] == 'S')
                    {
                        curRow = i;
                        curCol = j;
                    }
                }
            }

            var validChars = new char[] { '.', 'E' };
            var visited = new HashSet<(int, int)>();

            while (map[curRow][curCol] != 'E')
            {
                path.Add((curRow, curCol));
                visited.Add((curRow, curCol));
                
                if(curRow - 1 >= 0 && !visited.Contains((curRow - 1, curCol)) && validChars.Contains(map[curRow - 1][curCol]))
                {
                    curRow--;
                }
                else if (curRow + 1 < map.Count && !visited.Contains((curRow + 1, curCol)) && validChars.Contains(map[curRow + 1][curCol]))
                {
                    curRow++;
                }
                else if (curCol - 1 >= 0 && !visited.Contains((curRow, curCol - 1)) && validChars.Contains(map[curRow][curCol - 1]))
                {
                    curCol--;
                }
                else if (curCol + 1 < map[curRow].Count && !visited.Contains((curRow, curCol + 1)) && validChars.Contains(map[curRow][curCol + 1]))
                {
                    curCol++;
                }
            }

            path.Add((curRow, curCol));
        }

        public int Problem1()
        {
            return FindSkips(2);
        }

        public int Problem2()
        {
            return FindSkips(20);
        }

        private int FindSkips(int steps)
        {
            var total = 0;

            for (var i = 0; i < path.Count - 100; i++)
            {
                var (curRow, curCol) = path[i];

                var possible = path.Skip(i + 100).Select(node => NodeWithDist(path[i], node, steps)).Where(node => node.Dist <= 20);

                total += possible.Count(p => path.IndexOf(p.Node) - i >= 100 + p.Dist);
            }
            
            return total;
        }

        private static ((int Row, int Col) Node, int Dist) NodeWithDist((int Row, int Col) cur, (int Row, int Col) dest, int steps) =>
            (dest, Math.Abs(cur.Row - dest.Row) + Math.Abs(cur.Col - dest.Col));
    }
}
