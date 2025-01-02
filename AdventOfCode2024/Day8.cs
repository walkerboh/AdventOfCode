namespace AdventOfCode2024
{
    internal class Day8
    {
        private readonly List<List<char>> data;
        private readonly List<(char, int, int)> signals = [];
        private readonly int height, width;

        public Day8()
        {
            data = File.ReadAllLines("Data\\Day8.txt").Select(l => l.ToCharArray().ToList()).ToList();
            height = data.Count;
            width = data[0].Count;
            for (int i = 0; i < height; i++)
            {
                for(int j = 0; j < width; j++)
                {
                    if (data[i][j] != '.')
                    {
                        signals.Add((data[i][j], i, j));
                    }
                }
            }
            
        }

        public int Problem1()
        {
            var antinodes = new HashSet<(int, int)>();

            var grouped = signals.GroupBy(s => s.Item1);

            foreach(var symbol in grouped)
            {
                for(var i = 0; i < symbol.Count() - 1; i++)
                {
                    var signal = symbol.ToList()[i];

                    foreach(var other in symbol.ToList()[(i+1)..])
                    {
                        var xDist = signal.Item2 - other.Item2;
                        var yDist = signal.Item3 - other.Item3;

                        int newX, newY;

                        newX = signal.Item2 + xDist;
                        newY = signal.Item3 + yDist;

                        if(newX >= 0 && newX < height && newY >= 0 && newY < width)
                        {
                            antinodes.Add((newX, newY));
                        }

                        newX = other.Item2 - xDist;
                        newY = other.Item3 - yDist;

                        if (newX >= 0 && newX < height && newY >= 0 && newY < width)
                        {
                            antinodes.Add((newX, newY));
                        }
                    }
                }
            }

            return antinodes.Count;
        }

        public int Problem2()
        {
            var antinodes = new HashSet<(int, int)>();

            var grouped = signals.GroupBy(s => s.Item1);

            foreach (var symbol in grouped)
            {
                for (var i = 0; i < symbol.Count() - 1; i++)
                {
                    var signal = symbol.ToList()[i];
                    antinodes.Add((signal.Item2, signal.Item3));

                    foreach (var other in symbol.ToList()[(i + 1)..])
                    {
                        antinodes.Add((other.Item2, other.Item3));

                        var xDist = signal.Item2 - other.Item2;
                        var yDist = signal.Item3 - other.Item3;

                        int newX, newY;

                        newX = signal.Item2 + xDist;
                        newY = signal.Item3 + yDist;

                        while (newX >= 0 && newX < height && newY >= 0 && newY < width)
                        {
                            antinodes.Add((newX, newY));
                            newX += xDist;
                            newY += yDist;
                        }

                        newX = other.Item2 - xDist;
                        newY = other.Item3 - yDist;

                        while (newX >= 0 && newX < height && newY >= 0 && newY < width)
                        {
                            antinodes.Add((newX, newY));
                            newX -= xDist;
                            newY -= yDist;
                        }
                    }
                }
            }

            return antinodes.Count;
        }
    }
}
