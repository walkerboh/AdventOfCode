namespace AdventOfCode2024
{
    internal class Day19
    {
        private readonly List<string> towels;
        private readonly List<string> desiredPatterns;
        private readonly Dictionary<string, List<string>> bestMatches = [];
        private readonly HashSet<string> failedPatterns = [];
        private readonly Dictionary<string, long> patternSolutions = [];

        public Day19()
        {
            var lines = File.ReadAllLines("Data\\Day19.txt").ToList();
            towels = lines[0].Split(',').Select(s => s.Trim()).OrderByDescending(s => s.Length).ToList();
            desiredPatterns = lines.Skip(2).ToList();
        }

        public int Problem1()
        {
            var total = 0;

            foreach(var pattern in desiredPatterns)
            {
                var solution = MatchPattern(pattern, []);
                if(solution is not null)
                {
                    total++;
                }
            }

            return total;
        }

        public long Problem2()
        {
            long total = 0;

            foreach (var pattern in desiredPatterns)
            {
                total += MatchPatternRec(pattern);
            }

            return total;
        }

        private List<string>? MatchPattern(string pattern, List<string> usedTowels)
        {
            if(bestMatches.TryGetValue(pattern, out var previousFoundPattern))
            {
                return previousFoundPattern;
            }

            if(failedPatterns.Contains(pattern))
            {
                return null;
            }

            foreach(var towel in towels.Where(pattern.StartsWith))
            {
                if(pattern.Equals(towel))
                {
                    return [towel];
                }

                if(pattern.StartsWith(towel))
                {
                    var towelPattern = MatchPattern(pattern[(towel.Length)..], [..usedTowels, towel]);
                    if(towelPattern is null)
                    {
                        failedPatterns.Add(pattern[(towel.Length)..]);
                        continue;
                    }

                    List<string> newPattern = [towel, ..towelPattern];

                    if(bestMatches.TryGetValue(pattern, out var bestMatch))
                    {
                        if(newPattern.Count < bestMatch.Count)
                        {
                            bestMatches[pattern] = newPattern;
                        }
                    }
                    else
                    {
                        bestMatches[pattern] = newPattern;
                    }
                }
            }

            bestMatches.TryGetValue(pattern, out var match);

            return match;
        }

        private long MatchPatternRec(string desiredPattern)
        {
            if(string.IsNullOrWhiteSpace(desiredPattern))
            {
                return 1;
            }

            if(failedPatterns.Contains(desiredPattern))
            {
                return 0;
            }

            if(patternSolutions.TryGetValue(desiredPattern, out var solutionsCounts))
            {
                return solutionsCounts;
            }

            long totalSolutions = 0;

            var matchingTowels = towels.Where(desiredPattern.StartsWith);

            if (matchingTowels.Any())
            {
                foreach (var towel in towels.Where(desiredPattern.StartsWith))
                {
                    totalSolutions += MatchPatternRec(desiredPattern[(towel.Length)..]);
                }
            }
            else
            {
                failedPatterns.Add(desiredPattern);
                return 0;
            }

            patternSolutions[desiredPattern] = totalSolutions;

            return totalSolutions;
        }
    }
}
