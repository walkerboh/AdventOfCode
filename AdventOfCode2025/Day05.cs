namespace AdventOfCode2025
{
    internal class Day05 : CustomDay
    {
        private readonly List<(long lower, long upper)> Fresh = [];
        private readonly HashSet<long> Available = [];

        public Day05()
        {
            foreach(var line in GetInputLines())
            {
                if(string.IsNullOrEmpty(line))
                {
                    continue;
                }
                else if (line.Contains('-'))
                {
                    var split = line.Split('-').Select(long.Parse).ToList();
                    Fresh.Add((split[0], split[1]));
                }
                else
                {
                    Available.Add(long.Parse(line));
                }
            }
        }

        public override ValueTask<string> Solve_1()
        {
            return ValueTask.FromResult(Available.Count(a => Fresh.Any(f => f.lower < a && f.upper > a)).ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            var sortedFresh = Fresh.OrderBy(f => f.lower).ToList();
            var compactFresh = new List<(long lower, long upper)>();
            (long lower, long upper) curRange = sortedFresh.First();

            foreach (var range in sortedFresh[1..])
            {
                if(range.lower <= curRange.upper)
                {
                    if(range.upper > curRange.upper)
                    {
                        curRange.upper = range.upper;
                    }
                }
                else
                {
                    compactFresh.Add(curRange);
                    curRange = range;
                }
            }

            compactFresh.Add(curRange);

            long total = 0;

            foreach (var range in compactFresh)
            {
                total += range.upper - range.lower + 1;
            }

            return ValueTask.FromResult(total.ToString());

            // Not sure what was wrong with this, but the above is much simpler
            //List<(long lower, long upper)> compactFresh = [];

            //foreach(var range in Fresh)
            //{
            //    if(compactFresh.Contains(range))
            //    {
            //        continue;
            //    }

            //    var lowerOverlap = compactFresh.SingleOrDefault(f => f.lower <= range.lower && f.upper >= range.lower);
            //    var upperOverlap = compactFresh.SingleOrDefault(f => f.lower <= range.upper && f.upper >= range.upper);
            //    (long lower, long upper) newRange;

            //    if(lowerOverlap == (0, 0) && upperOverlap == (0,0))
            //    {
            //        newRange = range;
            //    }
            //    else if(lowerOverlap != (0,0) && upperOverlap != (0,0))
            //    {
            //        newRange = (lowerOverlap.lower, upperOverlap.upper);
            //        compactFresh.Remove(lowerOverlap);
            //        compactFresh.Remove(upperOverlap);
            //    }
            //    else if(lowerOverlap != (0,0))
            //    {
            //        newRange = (lowerOverlap.lower, range.upper);
            //        compactFresh.Remove(lowerOverlap);
            //    }
            //    else
            //    {
            //        newRange = (range.lower, upperOverlap.upper);
            //        compactFresh.Remove(upperOverlap);
            //    }

            //    compactFresh.Add(newRange);
            //}

            //BigInteger total = 0;

            //foreach(var range in compactFresh)
            //{
            //    total += range.upper - range.lower + 1;
            //}

            //return ValueTask.FromResult(total.ToString());
        }
    }
}
