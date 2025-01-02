namespace AdventOfCode2024
{
    internal class Day11
    {
        private readonly IList<long> data;

        public Day11()
        {
            data = File.ReadAllText("Data\\Day11.txt").Split(' ').Select(long.Parse).ToList();
        }

        public int Problem1()
        {
            var stones = new List<long>(data);

            for(var i = 0; i < 25; i++)
            {
                var newStones = new List<long>();

                foreach(var stone in stones)
                {
                    if(stone == 0)
                    {
                        newStones.Add(1);
                        continue;
                    }

                    var stoneString = $"{stone}";

                    if (stoneString.Length % 2 == 0)
                    {
                        newStones.Add(long.Parse(stoneString[0..(stoneString.Length / 2)]));
                        newStones.Add(long.Parse(stoneString[(stoneString.Length / 2)..]));
                        continue;
                    }

                    newStones.Add(stone * 2024);
                }

                stones = newStones;
            }

            return stones.Count;
        }

        public long Problem2()
        {
            var stones = data.GroupBy(d => d).ToDictionary(d => d.Key, d => d.LongCount());

            for (var i = 0; i < 75; i++)
            {
                var newStones = new Dictionary<long, long>();

                foreach (var stone in stones)
                {
                    if (stone.Key == 0)
                    {
                        newStones.TryGetValue(1, out var count);
                        newStones[1] = count + stone.Value;
                        continue;
                    }

                    var stoneString = $"{stone.Key}";

                    if (stoneString.Length % 2 == 0)
                    {
                        var newStone = long.Parse(stoneString[0..(stoneString.Length / 2)]);

                        newStones.TryGetValue(newStone, out var count1);
                        newStones[newStone] = count1 + stone.Value;

                        newStone = long.Parse(stoneString[(stoneString.Length / 2)..]);

                        newStones.TryGetValue(newStone, out var count2);
                        newStones[newStone] = count2 + stone.Value;

                        continue;
                    }

                    var multStone = stone.Key * 2024;

                    newStones.TryGetValue(multStone, out var currentCount);
                    newStones[multStone] = currentCount + stone.Value;
                }

                stones = newStones;
            }

            return stones.Sum(s => s.Value);
        }
    }
}
