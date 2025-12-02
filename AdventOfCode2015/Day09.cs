using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2015
{
    internal partial class Day09 : CustomDay
    {
        private readonly IEnumerable<CityDistance> Distances;
        private readonly IEnumerable<string> Cities;

        public Day09()
        {
            Distances = [.. GetInputLines().Select(l => new CityDistance(l))];
            Cities = [.. Distances.SelectMany(d => d.Cities).Distinct()];
        }

        public override ValueTask<string> Solve_1()
        {
            var shortestRoute = int.MaxValue;

            foreach (var city in Cities)
            {
                Dfs(0, city, []);
            }

            return ValueTask.FromResult(shortestRoute.ToString());

            void Dfs(int totalDistance, string curCity, HashSet<string> visited)
            {
                visited.Add(curCity);

                if (visited.Count == Cities.Count() && totalDistance < shortestRoute)
                {
                    shortestRoute = totalDistance;
                }

                if (totalDistance > shortestRoute)
                {
                    return;
                }

                foreach (var route in Distances.Where(d => (d.City1 == curCity && !visited.Contains(d.City2)) || (d.City2 == curCity && !visited.Contains(d.City1))))
                {
                    Dfs(totalDistance + route.Distance, route.Cities.Except([curCity]).Single(), [.. visited, .. route.Cities]);
                }
            }
        }

        public override ValueTask<string> Solve_2()
        {
            var longestRoute = int.MinValue;

            foreach (var city in Cities)
            {
                Dfs(0, city, []);
            }

            return ValueTask.FromResult(longestRoute.ToString());

            void Dfs(int totalDistance, string curCity, HashSet<string> visited)
            {
                visited.Add(curCity);

                if (visited.Count == Cities.Count() && totalDistance > longestRoute)
                {
                    longestRoute = totalDistance;
                }

                //if (totalDistance < longestRoute)
                //{
                //    return;
                //}

                foreach (var route in Distances.Where(d => (d.City1 == curCity && !visited.Contains(d.City2)) || (d.City2 == curCity && !visited.Contains(d.City1))))
                {
                    Dfs(totalDistance + route.Distance, route.Cities.Except([curCity]).Single(), [.. visited, .. route.Cities]);
                }
            }
        }

        [GeneratedRegex(@"(\w+) to (\w+) = (\d+)")]
        public static partial Regex DistanceRegex();

        private class CityDistance
        {
            public CityDistance(string line)
            {
                var match = DistanceRegex().Match(line);
                City1 = match.Groups[1].Value;
                City2 = match.Groups[2].Value;
                Distance = int.Parse(match.Groups[3].Value);
            }

            public string City1 { get; set; }
            public string City2 { get; set; }
            public int Distance { get; set; }
            public List<string> Cities => [City1, City2];
        }
    }
}
