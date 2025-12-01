using AoCHelper;

namespace AdventOfCode2015
{
    internal class Day05 : BaseDay
    {
        private readonly IEnumerable<string> Words;
        private static readonly List<char> Vowels = ['a', 'e', 'i', 'o', 'u'];
        private static readonly List<string> Bads = ["ab", "cd", "pq", "xy"];

        public Day05()
        {
            Words = File.ReadAllLines(InputFilePath);
        }

        public override ValueTask<string> Solve_1()
        {
            var nice = 0;

            foreach (var word in Words)
            {
                var vowels = word.Count(Vowels.Contains);

                if (vowels < 3)
                {
                    continue;
                }

                if (Bads.Any(word.Contains))
                {
                    continue;
                }

                for (var i = 0; i < word.Length - 1; i++)
                {
                    if (word[i] == word[i + 1])
                    {
                        nice++;
                        break;
                    }
                }
            }

            return ValueTask.FromResult(nice.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            var nice = 0;

            foreach (var word in Words)
            {
                var split = false;
                var doubled = false;

                for (var i = 0; i < word.Length - 2 && !(split && doubled); i++)
                {
                    if (!split && word[i] == word[i + 2])
                    {
                        split = true;
                    }

                    if (!doubled && word[(i + 2)..].Contains(word[i..(i + 2)]))
                    {
                        doubled = true;
                    }
                }

                if(split && doubled)
                {
                    nice++;
                }
            }

            return ValueTask.FromResult(nice.ToString());
        }
    }
}
