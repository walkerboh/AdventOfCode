
using System.Runtime.CompilerServices;

namespace AdventOfCode2025
{
    internal class Day09 : CustomDay
    {
        private readonly List<Coord> TileCoords;
        (Coord?, Coord?) largestCoordPair = (null, null);

        public Day09()
        {
            TileCoords = [.. GetInputLines().Select(x => x.Split(',')).Select(s => new Coord(int.Parse(s[0]), int.Parse(s[1])))];
        }

        public override ValueTask<string> Solve_1()
        {
            long largest = 0;

            for (var i = 0; i < TileCoords.Count - 1; i++)
            {
                for (var j = 0; j < TileCoords.Count; j++)
                {
                    var area = Area(TileCoords[i], TileCoords[j]);

                    if(area > largest)
                    {
                        largest = area;
                        largestCoordPair = (TileCoords[i], TileCoords[j]);
                    }
                }
            }

            return ValueTask.FromResult(largest.ToString());   
        }

        // Not my own solution, learning AABB collision from the following sources:
        // For understanding the concept: https://kishimotostudios.com/articles/aabb_collision/
        // For how to implement for this puzzle: https://aoc.csokavar.hu/2025/9/
        public override ValueTask<string> Solve_2()
        {
            // Cartesian Product
            var allRectangles = TileCoords.SelectMany(a => TileCoords.Where(b => b != a).Select(b => CoordsToRectangle(a, b))).OrderByDescending(Area).ToList();

            var boundaries = TileCoords.Zip(TileCoords.Prepend(TileCoords.Last())).Select(a => CoordsToRectangle(a.First, a.Second)).ToList();

            var largest = allRectangles.First(r => boundaries.All(b => !AabbCollision(r, b)));

            return ValueTask.FromResult(Area(largest).ToString());

            bool AabbCollision(Rectangle a, Rectangle b)
            {
                var aIsToTheRight = a.Left >= b.Right;
                var aIsToTheLeft = a.Right <= b.Left;
                var aIsAbove = a.Bottom <= b.Top;
                var aIsBelow = a.Top >= b.Bottom;

                return !(aIsToTheRight || aIsToTheLeft || aIsAbove || aIsBelow);
            }
        }

        private static long Area(Coord tile1, Coord tile2)
        {
            return Math.Abs((tile1.X - tile2.X + 1) * (tile1.Y - tile2.Y + 1));
        }

        private static long Area(Rectangle rec)
        {
            return (rec.Bottom - rec.Top + 1) * (rec.Right - rec.Left + 1);
        }

        private static Rectangle CoordsToRectangle(Coord a, Coord b)
        {
            var top = Math.Min(a.Y, b.Y);
            var bottom = Math.Max(a.Y, b.Y);
            var left = Math.Min(a.X, b.X);
            var right = Math.Max(a.X, b.X);
            return new(left, right, top, bottom);
        }

        private record Coord(long X, long Y);
        private record Rectangle(long Left, long Right, long Top, long Bottom);
    }
}
