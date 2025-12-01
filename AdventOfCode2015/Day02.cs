using AoCHelper;

namespace AdventOfCode2015
{
    internal class Day02 : BaseDay
    {
        private readonly IEnumerable<Box> Boxes = [];

        public Day02()
        {
            Boxes = File.ReadAllLines(InputFilePath).Select(l => new Box(l));
        }

        public override ValueTask<string> Solve_1()
        {
            long totalArea = 0;

            foreach (var box in Boxes)
            {
                var area1 = box.Length * box.Width;
                var area2 = box.Width * box.Height;
                var area3 = box.Length * box.Height;
                var minArea = new[] { area1, area2, area3 }.Min();

                totalArea += (2 * area1) + (2 * area2) + (2 * area3) + minArea;
            }

            return ValueTask.FromResult(totalArea.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            long totalFeet = 0;

            foreach (var box in Boxes)
            {
                var face1 = (2 * box.Length) + (2 * box.Width);
                var face2 = (2 * box.Width) + (2 * box.Height);
                var face3 = (2 * box.Length) + (2 * box.Height);
                var minFace = new[] { face1, face2, face3 }.Min();

                var area = box.Length * box.Width * box.Height;

                totalFeet += minFace + area;
            }

            return ValueTask.FromResult(totalFeet.ToString());
        }

        private class Box
        {
            public Box(string line)
            {
                var split = line.Split('x').Select(int.Parse).ToList();
                Length = split[0];
                Width = split[1];
                Height = split[2];
            }

            public int Length { get; set; }
            public int Width { get; set; }
            public int Height { get; set; }
        }
    }
}
