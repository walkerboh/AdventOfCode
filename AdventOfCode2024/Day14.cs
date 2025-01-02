using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2024
{
    internal partial class Day14
    {
        private readonly List<Robot> data;
        //private readonly int height = 103, width = 101;
        private readonly int height = 103, width = 101;

        public Day14()
        {
            data = File.ReadAllLines("Data\\Day14.txt").Select(s => new Robot(RobotRegex().Match(s))).ToList();
        }

        public int Problem1()
        {
            foreach (var robot in data)
            {
                robot.X = (robot.X + (robot.VX * 100)) % width;
                if (robot.X < 0)
                {
                    robot.X += width;
                }
                robot.Y = (robot.Y + (robot.VY * 100)) % height;
                if (robot.Y < 0)
                {
                    robot.Y += height;
                }
            }

            var quad1 = data.Count(d => d.X < width / 2 && d.Y < height / 2);
            var quad2 = data.Count(d => d.X > width / 2 && d.Y < height / 2);
            var quad3 = data.Count(d => d.X < width / 2 && d.Y > height / 2);
            var quad4 = data.Count(d => d.X > width / 2 && d.Y > height / 2);

            return quad1 * quad2 * quad3 * quad4;
        }

        public int Problem2()
        {
            var robots = new List<Robot>(data);
            var seconds = 0;
            while (seconds < width * height)
            {
                foreach (var robot in data)
                {
                    robot.X = (robot.X + robot.VX) % width;
                    if (robot.X < 0)
                    {
                        robot.X += width;
                    }
                    robot.Y = (robot.Y + robot.VY) % height;
                    if (robot.Y < 0)
                    {
                        robot.Y += height;
                    }
                }

                seconds++;

                var regions = new Dictionary<int, List<(int, int)>>();
                var regionId = 0;

                for (var i = 0; i < height; i++)
                {
                    for (var j = 0; j < width; j++)
                    {
                        if (robots.Any(r => r.X == i && r.Y == j))
                        {
                            var existingRegions = regions.Where(r => r.Value.Any(coord => (coord.Item1 == i + 1 && coord.Item2 == j
                                || coord.Item1 == i - 1 && coord.Item2 == j
                                || coord.Item1 == i && coord.Item2 == j + 1
                                || coord.Item1 == i && coord.Item2 == j - 1)))
                            .ToList();

                            if (existingRegions.Count == 0)
                            {
                                regions.Add(regionId++, [(i, j)]);
                            }

                            if (existingRegions.Count == 1)
                            {
                                existingRegions.First().Value.Add((i, j));
                            }

                            if (existingRegions.Count > 1)
                            {
                                var region = existingRegions.First();

                                region.Value.Add((i, j));

                                var others = existingRegions[1..];

                                foreach (var r in others)
                                {
                                    region.Value.AddRange(r.Value);
                                    regions.Remove(r.Key);
                                }
                            }
                        }
                    }
                }

                if (regions.Any(r => r.Value.Count > 10))
                {
                    Console.WriteLine($"{seconds} seconds");
                    for (var i = 0; i < height; i++)
                    {
                        for (var j = 0; j < width; j++)
                        {
                            var count = robots.Count(r => r.X == j && r.Y == i);
                            Console.Write(count == 0 ? " " : "█");
                        }
                        Console.WriteLine();
                    }

                    Console.WriteLine();
                    Console.WriteLine();
                }
            }

            return 0;
        }

        private class Robot(Match match)
        {
            public int X { get; set; } = int.Parse(match.Groups[1].Value);
            public int Y { get; set; } = int.Parse(match.Groups[2].Value);
            public int VX { get; set; } = int.Parse(match.Groups[3].Value);
            public int VY { get; set; } = int.Parse(match.Groups[4].Value);
        }

        [GeneratedRegex(@"p=(\d+),(\d+) v=(-?\d+),(-?\d+)")]
        private static partial Regex RobotRegex();
    }
}
