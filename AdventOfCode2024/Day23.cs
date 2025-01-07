using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode2024
{
    internal class Day23
    {
        private readonly List<Connection> connections;

        public Day23()
        {
            connections = File.ReadAllLines("Data\\Day23.txt").Select(l => new Connection(l.Split('-'))).ToList();
        }

        public int Problem1()
        {
            var groupings = GetGroupings();

            var sets = new HashSet<List<string>>(new ListCompare());

            foreach (var (c1, links) in groupings)
            {
                foreach (var c2 in links)
                {
                    foreach (var c3 in groupings[c2])
                    {
                        if (!links.Contains(c3))
                        {
                            continue;
                        }

                        if (c1.StartsWith('t') || c2.StartsWith('t') || c3.StartsWith('t'))
                        {
                            sets.Add([c1, c2, c3]);
                        }
                    }
                }
            }

            return sets.Count;
        }

        public string Problem2()
        {
            var groupings = GetGroupings();

            // Learning Bron–Kerbosch
            HashSet<string> cliques = [];

            foreach(var computer in groupings.Keys)
            {
                BronKerbosch([computer], [.. groupings[computer]], [], cliques);
            }

            return cliques.MaxBy(c => c.Length) ?? "";

            void BronKerbosch(HashSet<string> R, HashSet<string> P, HashSet<string> X, HashSet<string> O)
            {
                // P and X are empty, R is a maximal set, add to output
                if(P.Count == 0 && X.Count == 0)
                {
                    O.Add(string.Join(",", R.Order()));
                    return;
                }

                HashSet<string> cloneP = [..P];
                foreach(var v in P)
                {
                    var c = groupings[v];
                    BronKerbosch([.. R.Union([v])], [.. cloneP.Intersect(c)], [.. X.Intersect(c)], O);
                    cloneP.Remove(v);
                    X.Add(v);
                }
            }
        }

        private Dictionary<string, HashSet<string>> GetGroupings()
        {
            var groupings = new Dictionary<string, HashSet<string>>();

            foreach (var connection in connections)
            {
                if (!groupings.ContainsKey(connection.Computer1))
                {
                    groupings[connection.Computer1] = [];
                }
                groupings[connection.Computer1].Add(connection.Computer2);

                if (!groupings.ContainsKey(connection.Computer2))
                {
                    groupings[connection.Computer2] = [];
                }
                groupings[connection.Computer2].Add(connection.Computer1);
            }

            return groupings;
        }

        private class Connection(string[] computers)
        {
            public string Computer1 { get; set; } = computers[0];
            public string Computer2 { get; set; } = computers[1];

            public IEnumerable<string> Computers => [Computer1, Computer2];
            public bool HasComputer(string comp) => comp == Computer1 || comp == Computer2;
            public bool Intersects(Connection con) => con.HasComputer(Computer1) || con.HasComputer(Computer2);
        }

        private class ListCompare : IEqualityComparer<List<string>>
        {
            public bool Equals(List<string>? x, List<string>? y)
            {
                if (x is null || y is null)
                {
                    return false;
                }
                return x.Order().SequenceEqual(y.Order());
            }

            public int GetHashCode([DisallowNull] List<string> obj)
            {
                var ordered = obj.Order().ToList();
                return HashCode.Combine(ordered[0], ordered[1], ordered[2]);
            }
        }
    }
}
