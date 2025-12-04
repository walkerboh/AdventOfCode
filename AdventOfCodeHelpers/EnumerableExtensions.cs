namespace AdventOfCodeHelpers
{
    public static class EnumerableExtensions
    {
        // Source - https://stackoverflow.com/a
        // Posted by Pengyang, modified by community. See post 'Timeline' for change history
        // Retrieved 2025-12-03, License - CC BY-SA 3.0

        public static IEnumerable<IEnumerable<T>> GetPermutations<T>(this IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new T[] { t });

            return GetPermutations(list, length - 1)
                .SelectMany(t => list.Where(e => !t.Contains(e)),
                    (t1, t2) => t1.Concat(new T[] { t2 }));
        }

        // Source - https://stackoverflow.com/a
        // Posted by ojlovecd
        // Retrieved 2025-12-04, License - CC BY-SA 3.0

        public static IEnumerable<IEnumerable<T>> GetCombinations<T>(this List<T> list)
        {
            double count = Math.Pow(2, list.Count);
            for (int i = 1; i <= count - 1; i++)
            {
                string str = Convert.ToString(i, 2).PadLeft(list.Count, '0');
                IEnumerable<T> combo = [];
                for (int j = 0; j < str.Length; j++)
                {
                    if (str[j] == '1')
                    {
                        combo = combo.Append(list[j]);
                    }
                }
                yield return combo;
            }
        }

    }
}
