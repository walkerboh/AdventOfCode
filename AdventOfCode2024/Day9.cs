namespace AdventOfCode2024
{
    internal class Day9
    {
        private readonly string diskMap;
        private readonly List<(int, bool, int)> numericMap;

        public Day9()
        {
            diskMap = File.ReadAllText("Data\\Day9.txt");
            var id = 0;
            numericMap = diskMap.Select((c, ind) => (int.Parse(c.ToString()), ind % 2 == 0, ind % 2 == 0 ? id++ : 0)).ToList();
        }

        public long Problem1()
        {
            var disk = new List<long>();
            var id = 0;
            var file = true;

            foreach (var size in diskMap)
            {
                int.Parse(size.ToString()).Times(() => disk.Add(file ? id : -1));
                id += file ? 1 : 0;
                file = !file;
            }

            var nextEmpty = disk.IndexOf(-1);
            var ind = disk.Count - 1;

            while (ind > nextEmpty)
            {
                if (disk[ind] != -1)
                {
                    disk[nextEmpty] = disk[ind];
                    disk[ind] = -1;
                    nextEmpty = disk.IndexOf(-1);
                }

                ind--;
            }

            return disk.Select((val, ind) => val < 0 ? 0 : val * ind).Sum();
        }

        public long Problem2()
        {
            var ind = numericMap.FindLastIndex(i => i.Item2);

            while (ind > 0)
            {
                if (numericMap[ind].Item2)
                {
                    var file = numericMap[ind];
                    var freeSpace = numericMap.FindIndex(i => i.Item1 >= file.Item1 && !i.Item2);
                    if (freeSpace > -1 && freeSpace < ind)
                    {
                        numericMap[ind] = (numericMap[ind].Item1, false, 0);
                        numericMap[freeSpace] = (numericMap[freeSpace].Item1 - file.Item1, false, numericMap[freeSpace].Item3);
                        numericMap.Insert(freeSpace, (file.Item1, true, file.Item3));
                    }
                }

                ind--;
            }

            var disk = new List<long>();

            foreach(var space in numericMap)
            {
                space.Item1.Times(() => disk.Add(space.Item3));
            }

            return disk.Select((val, ind) => val < 0 ? 0 : val * ind).Sum();
        }
    }
}
