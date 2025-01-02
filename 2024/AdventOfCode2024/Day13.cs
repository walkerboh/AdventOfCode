using System.Text.RegularExpressions;

namespace AdventOfCode2024
{
    internal partial class Day13
    {
        private readonly List<Configuration> configurations = [];

        public Day13()
        {
            var lines = File.ReadAllLines("Data\\Day13.txt");
            var line = 0;
            while (line < lines.Length)
            {
                var a = ButtonRegex().Match(lines[line]);
                var b = ButtonRegex().Match(lines[line + 1]);
                var prize = PrizeRegex().Match(lines[line + 2]);
                configurations.Add(new(a, b, prize));
                line += 4;
            }
        }

        public int Problem1()
        {
            var total = 0;

            foreach(var config in configurations)
            {
                var b = ((config.Prize.Y * config.ButtonA.X) - (config.Prize.X * config.ButtonA.Y)) / ((config.ButtonB.Y * config.ButtonA.X)- (config.ButtonB.X * config.ButtonA.Y));
                var a = (config.Prize.X - (b * config.ButtonB.X)) / config.ButtonA.X;
                
                if(a.IsInteger() && a <= 100 && b.IsInteger() && b <= 100)
                {
                    total += (int)a * 3 + (int)b;
                }
            }

            return total;
        }

        public long Problem2()
        {
            var increase = 10000000000000;
            long total = 0;

            foreach (var config in configurations)
            {
                config.Prize.X += increase;
                config.Prize.Y += increase;

                var b = ((config.Prize.Y * config.ButtonA.X) - (config.Prize.X * config.ButtonA.Y)) / ((config.ButtonB.Y * config.ButtonA.X) - (config.ButtonB.X * config.ButtonA.Y));
                var a = (config.Prize.X - (b * config.ButtonB.X)) / config.ButtonA.X;

                if (a.IsInteger() && b.IsInteger())
                {
                    total += (long)a * 3 + (long)b;
                }
            }

            return total;
        }

        private class Configuration(Match a, Match b, Match prize)
        {
            public Coords ButtonA { get; set; } = new(a);
            public Coords ButtonB { get; set; } = new(b);
            public Coords Prize { get; set; } = new(prize);
        }

        private class Coords(Match match)
        {
            public decimal X { get; set; } = decimal.Parse(match.Groups[1].Value);
            public decimal Y { get; set; } = decimal.Parse(match.Groups[2].Value);
        }

        [GeneratedRegex(@"X\+(\d+), Y\+(\d+)")]
        private static partial Regex ButtonRegex();

        [GeneratedRegex(@"X=(\d+), Y=(\d+)")]
        private static partial Regex PrizeRegex();
    }
}
