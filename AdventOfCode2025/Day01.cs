using AoCHelper;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2025
{
    internal class Day01 : BaseDay
    {
        private readonly IEnumerable<Rotation> Rotations = [];

        public Day01()
        {
            Rotations = File.ReadAllLines(InputFilePath).Select(l => new Rotation(l));
        }

        public override ValueTask<string> Solve_1()
        {
            var pos = 50;
            var count = 0;

            foreach (var rot in Rotations)
            {
                if (rot.Direction == Direction.L)
                {
                    pos -= rot.Distance;
                }
                else
                {
                    pos += rot.Distance;
                }

                while (pos < 0)
                {
                    pos += 100;
                }
                while (pos > 99)
                {
                    pos -= 100;
                }

                if (pos == 0)
                {
                    count++;
                }
            }

            return ValueTask.FromResult(count.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            var pos = 50;
            var count = 0;

            foreach (var rot in Rotations)
            {
                for (var i = 0; i < rot.Distance; i++)
                {
                    pos += (rot.Direction == Direction.R ? 1 : -1);

                    if(pos == 100)
                    {
                        pos = 0;
                    }
                    else if (pos == -1)
                    {
                        pos = 99;
                    }

                    if (pos == 0)
                    {
                        count++;
                    }
                }
            }

            return ValueTask.FromResult(count.ToString());
        }

        private class Rotation(string line)
        {
            public Direction Direction { get; set; } = line[0].Equals('L') ? Direction.L : Direction.R;
            public int Distance { get; set; } = int.Parse(line[1..]);
        }

        enum Direction
        {
            L,
            R
        }
    }
}
