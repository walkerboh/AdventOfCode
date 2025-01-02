namespace AdventOfCode2024
{
    internal class Day12
    {
        private readonly List<List<char>> data;

        public Day12()
        {
            data = File.ReadAllLines("Data\\Day12.txt").Select(l => l.ToCharArray().ToList()).ToList();
        }

        public int Problem1()
        {
            var perimiters = new Dictionary<(int, int), int>();
            var regions = new Dictionary<int, List<(char, int, int)>>();

            var regionId = 0;

            for (var i = 0; i < data.Count; i++)
            {
                for (var j = 0; j < data[i].Count; j++)
                {
                    var perimeter = 0;
                    var plot = data[i][j];

                    if (i - 1 < 0 || data[i - 1][j] != plot)
                    {
                        perimeter++;
                    }
                    if (i + 1 >= data.Count || data[i + 1][j] != plot)
                    {
                        perimeter++;
                    }
                    if (j - 1 < 0 || data[i][j - 1] != plot)
                    {
                        perimeter++;
                    }
                    if (j + 1 >= data[i].Count || data[i][j + 1] != plot)
                    {
                        perimeter++;
                    }

                    perimiters.Add((i, j), perimeter);

                    var existingRegions = regions.Where(r => r.Value.Any(coord => coord.Item1 == plot
                        && (coord.Item2 == i + 1 && coord.Item3 == j
                            || coord.Item2 == i - 1 && coord.Item3 == j
                            || coord.Item2 == i && coord.Item3 == j + 1
                            || coord.Item2 == i && coord.Item3 == j - 1)))
                        .ToList();

                    if (existingRegions.Count == 0)
                    {
                        regions.Add(regionId++, [(plot, i, j)]);
                    }

                    if (existingRegions.Count == 1)
                    {
                        existingRegions.First().Value.Add((plot, i, j));
                    }

                    if (existingRegions.Count > 1)
                    {
                        var region = existingRegions.First();

                        region.Value.Add((plot, i, j));

                        var others = existingRegions[1..];

                        foreach (var r in others)
                        {
                            region.Value.AddRange(r.Value);
                            regions.Remove(r.Key);
                        }
                    }
                }
            }

            int total = 0;

            foreach (var region in regions)
            {
                var area = region.Value.Count;
                var permineter = perimiters.Join(region.Value, a => a.Key, b => (b.Item2, b.Item3), (a, b) => a.Value).Sum();
                total += area * permineter;
            }

            return total;
        }

        public int Problem2()
        {
            var perimiters = new Dictionary<(int, int), int>();
            var regions = new Dictionary<int, List<(char, int, int)>>();

            var regionId = 0;

            for (var i = 0; i < data.Count; i++)
            {
                for (var j = 0; j < data[i].Count; j++)
                {
                    var perimeter = 0;
                    var plot = data[i][j];

                    if (i - 1 < 0 || data[i - 1][j] != plot)
                    {
                        perimeter++;
                    }
                    if (i + 1 >= data.Count || data[i + 1][j] != plot)
                    {
                        perimeter++;
                    }
                    if (j - 1 < 0 || data[i][j - 1] != plot)
                    {
                        perimeter++;
                    }
                    if (j + 1 >= data[i].Count || data[i][j + 1] != plot)
                    {
                        perimeter++;
                    }

                    perimiters.Add((i, j), perimeter);

                    var existingRegions = regions.Where(r => r.Value.Any(coord => coord.Item1 == plot
                        && (coord.Item2 == i + 1 && coord.Item3 == j
                            || coord.Item2 == i - 1 && coord.Item3 == j
                            || coord.Item2 == i && coord.Item3 == j + 1
                            || coord.Item2 == i && coord.Item3 == j - 1)))
                        .ToList();

                    if (existingRegions.Count == 0)
                    {
                        regions.Add(regionId++, [(plot, i, j)]);
                    }

                    if (existingRegions.Count == 1)
                    {
                        existingRegions.First().Value.Add((plot, i, j));
                    }

                    if (existingRegions.Count > 1)
                    {
                        var region = existingRegions.First();

                        region.Value.Add((plot, i, j));

                        var others = existingRegions[1..];

                        foreach (var r in others)
                        {
                            region.Value.AddRange(r.Value);
                            regions.Remove(r.Key);
                        }
                    }
                }
            }

            int total = 0;

            List<(int, int)> offsets = [(1, 1), (1, -1), (-1, 1), (-1, -1)];


            // Credit to https://advent-of-code.xavd.id/writeups/2024/day/12/ for helping with the corner logic when my brain couldn't nail it down
            foreach (var region in regions)
            {
                var area = region.Value.Count;
                var corners = 0;

                foreach (var plot in region.Value)
                {
                    var (letter, row, col) = plot;

                    foreach(var offset in offsets)
                    {
                        var rowIn = region.Value.Contains((letter, row + offset.Item1, col));
                        var colIn = region.Value.Contains((letter, row, col + offset.Item2));
                        var diagIn = region.Value.Contains((letter, row + offset.Item1, col + offset.Item2));

                        if(!rowIn && !colIn)
                        {
                            corners++;
                        }

                        if(rowIn && colIn && !diagIn)
                        {
                            corners++;
                        }
                    }
                }

                total += area * corners;
            }

            return total;
        }
    }
}
