using AoCHelper;
using System.IO.IsolatedStorage;
using System.Runtime.CompilerServices;

namespace AdventOfCode2015
{
    internal class Day03 : BaseDay
    {
        private readonly string Instructions = string.Empty;

        public Day03()
        {
            Instructions = File.ReadAllText(InputFilePath);
        }

        public override ValueTask<string> Solve_1()
        {
            var coord = (X: 0, Y: 0);
            var visited = new HashSet<(int, int)> { coord };

            foreach (var move in Instructions)
            {
                coord = MoveCoords(coord, move);

                visited.Add(coord);
            }

            return ValueTask.FromResult(visited.Count.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            var santa = (X: 0, Y: 0);
            var robo = (X: 0, Y: 0);
            var visited = new HashSet<(int, int)> { santa };
            var isSanta = true;

            foreach (var move in Instructions)
            {
                if (isSanta)
                {
                    santa = MoveCoords(santa, move);
                }
                else
                {
                    robo = MoveCoords(robo, move);
                }

                visited.Add(isSanta ? santa : robo);

                isSanta = !isSanta;
            }

            return ValueTask.FromResult(visited.Count.ToString());
        }

        private static (int X, int Y) MoveCoords((int X, int Y) coord, char dir)
        {
            if (dir == '>')
            {
                coord.X++;
            }
            else if (dir == '<')
            {
                coord.X--;
            }
            else if (dir == '^')
            {
                coord.Y++;
            }
            else if (dir == 'v')
            {
                coord.Y--;
            }

            return coord;
        }
    }
}
