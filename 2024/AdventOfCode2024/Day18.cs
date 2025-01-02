namespace AdventOfCode2024
{
    internal class Day18
    {
        private readonly List<(int, int)> byteLocations;
        private readonly int height = 71, width = 71;

        public Day18()
        {
            byteLocations = File.ReadAllLines("Data\\Day18.txt").Select(ParseInputLine).ToList();
        }

        private (int, int) ParseInputLine(string line)
        {
            var split = line.Split(',').Select(int.Parse).ToList();
            return (split[0], split[1]);
        }

        public int Problem1()
        {
            var bytes = byteLocations.Take(1024);
            return Bfs(bytes).Count;
        }

        public string Problem2()
        {
            var path = Bfs(byteLocations.Take(1024));
            var nextByte = 1024;
            (int, int) badByte = (0, 0);

            while(path.Count > 0)
            {
                badByte = byteLocations[nextByte++];

                if (path.Contains(badByte))
                {
                    path = Bfs(byteLocations.Take(nextByte));
                }
            }

            return $"{badByte.Item1},{badByte.Item2}";
        }

        private List<(int, int)> Bfs(IEnumerable<(int, int)> bytes)
        {
            var visited = new HashSet<(int, int)>();
            var queue = new Queue<(int, int, List<(int, int)>)>();

            queue.Enqueue((0, 0, []));

            while (queue.Count != 0)
            {
                var (row, col, path) = queue.Dequeue();

                if (row == height - 1 && col == width - 1)
                {
                    return path;
                }

                List<(int, int)> newPath = [.. path, (row, col)];

                if (Safe(row + 1, col))
                {
                    queue.Enqueue((row + 1, col, newPath));
                    visited.Add((row + 1, col));
                }
                if (Safe(row - 1, col))
                {
                    queue.Enqueue((row - 1, col, newPath));
                    visited.Add((row - 1, col));
                }
                if (Safe(row, col + 1))
                {
                    queue.Enqueue((row, col + 1, newPath));
                    visited.Add((row, col + 1));
                }
                if (Safe(row, col - 1))
                {
                    queue.Enqueue((row, col - 1, newPath));
                    visited.Add((row, col - 1));
                }
            }

            return [];

            bool Safe(int row, int col)
            {
                return row >= 0 && row < height && col >= 0 && col < width
                    && !visited.Contains((row, col)) && !bytes.Contains((row, col));
            }
        }
    }
}
