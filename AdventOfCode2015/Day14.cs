using System.Text.RegularExpressions;

namespace AdventOfCode2015
{
    internal partial class Day14 : CustomDay
    {
        [GeneratedRegex(@"(\w+) can fly (\d+) km/s for (\d+) seconds, but then must rest for (\d+) seconds\.")]
        private static partial Regex ReindeerRegex();

        private List<Reindeer> Reindeers;

        public Day14()
        {
            Reindeers = [.. GetInputLines().Select(l => new Reindeer(l))];
        }

        public override ValueTask<string> Solve_1()
        {
            var totalTravelTime = 2503;
            var best = 0;

            foreach(var reindeer in Reindeers)
            {
                var fullIntervals = totalTravelTime / reindeer.TotalInvervalTime;
                var total = fullIntervals * reindeer.TotalIntervalDistance;
                var remainingTime = totalTravelTime % reindeer.TotalInvervalTime;
                total += Math.Min(remainingTime, reindeer.TravelTime) * reindeer.Speed;

                if(total > best)
                {
                    best = total;
                }
            }

            return ValueTask.FromResult(best.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            for(var i = 0; i < 2503; i++)
            {
                foreach(var reindeer in Reindeers)
                {
                    if((i % reindeer.TotalInvervalTime) < reindeer.TravelTime)
                    {
                        reindeer.Distance += reindeer.Speed;
                    }
                }

                var maxDistance = Reindeers.Max(r => r.Distance);
                var leaders = Reindeers.Where(r => r.Distance == maxDistance);
                
                foreach(var leader in leaders)
                {
                    leader.Points++;
                }
            }

            var winner = Reindeers.MaxBy(r => r.Points);
            return ValueTask.FromResult(winner.Points.ToString());
        }

        private class Reindeer
        {
            public Reindeer(string line)
            {
                var match = ReindeerRegex().Match(line);

                Name = match.Groups[1].Value;
                Speed = int.Parse(match.Groups[2].Value);
                TravelTime = int.Parse(match.Groups[3].Value);
                RestTime = int.Parse(match.Groups[4].Value);

                Points = 0;
                Distance = 0;
            }

            public string Name { get; set; }
            public int Speed { get; set; }
            public int TravelTime { get; set; }
            public int RestTime { get; set; }

            public int Points { get; set; }
            public int Distance { get; set; }

            public int TotalInvervalTime => TravelTime + RestTime;
            public int TotalIntervalDistance => TravelTime * Speed;
        }
    }
}
