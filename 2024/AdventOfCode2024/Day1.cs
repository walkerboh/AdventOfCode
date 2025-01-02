namespace AdventOfCode2024
{
    public class Day1
    {
        private readonly IEnumerable<int> listOne;
        private readonly IEnumerable<int> listTwo;

        public Day1()
        {
            var lines = File.ReadAllLines("Data\\Day1.txt").Select(s => s.Split(' ', StringSplitOptions.RemoveEmptyEntries));
            listOne = lines.Select(l => int.Parse(l[0]));
            listTwo = lines.Select(l => int.Parse(l[1]));
        }

        public int Problem1()
        {
            var sortedListOne = listOne.Order();
            var sortedListTwo = listTwo.Order();

            var diffs = sortedListOne.Zip(sortedListTwo, (a, b) => Math.Abs(a - b));

            return diffs.Sum();
        }

        public int Problem2()
        {
            var listTwoCounts = listTwo.GroupBy(i => i);

            var count = 0;

            foreach(var item in listOne)
            {
                count += (item * listTwoCounts.SingleOrDefault(i => i.Key == item)?.Count() ?? 0);
            }

            return count;
        }
    }
}
