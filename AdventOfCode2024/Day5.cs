namespace AdventOfCode2024
{
    internal class Day5
    {
        private readonly IEnumerable<Rule> rules;
        private readonly IEnumerable<IList<int>> data;

        public Day5()
        {
            rules = File.ReadAllLines("Data\\Day5Rules.txt").Select(s => new Rule(s));
            data = File.ReadAllLines("Data\\Day5Pages.txt").Select(s => s.Split(',').Select(int.Parse).ToList());
        }

        public int Problem1()
        {
            var valid = data.Where(d => rules.Where(r => r.Applies(d)).All(r => r.Valid(d)));
            return valid.Sum(v => v[v.Count / 2]);
        }

        public int Problem2()
        {
            var invalid = data.Where(d => rules.Where(r => r.Applies(d)).Any(r => !r.Valid(d)));
            var sorted = new List<List<int>>();

            foreach(var row in invalid)
            {
                var sorting = new List<int>(row);

                bool swap;
                int temp;

                for(var i = 0; i < sorting.Count - 1; i++)
                {
                    swap = false;
                    for(var j = 0; j < sorting.Count - i - 1; j++)
                    {
                        if(rules.Any(r => r.Second == sorting[j] && r.First == sorting[j+1]))
                        {
                            temp = sorting[j];
                            sorting[j] = sorting[j + 1];
                            sorting[j + 1] = temp;
                            swap = true;
                        }
                    }

                    if (!swap)
                    {
                        break;
                    }
                }

                sorted.Add(sorting);
            }

            return sorted.Sum(s => s[s.Count / 2]);
        }

        private class Rule
        {
            public readonly int First;
            public readonly int Second;

            public Rule(string rule)
            {
                var items = rule.Split('|').Select(int.Parse);
                First = items.ElementAt(0);
                Second = items.ElementAt(1);
            }

            public bool Applies(IEnumerable<int> pages) => pages.Contains(First) && pages.Contains(Second);
            public bool Applies(int page) => page == First || page == Second;

            public bool Valid(IList<int> pages) => pages.IndexOf(First) < pages.IndexOf(Second);
        }
    }
}
