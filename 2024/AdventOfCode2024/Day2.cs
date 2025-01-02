namespace AdventOfCode2024
{
    internal class Day2
    {
        private readonly List<List<int>> data;

        public Day2()
        {
            var lines = File.ReadAllLines("Data\\Day2Levels.txt");

            data = lines.Select(line => line.Split(' ').Select(int.Parse).ToList()).ToList();
        }

        public int Problem1()
        {
            return data.Count(RowSafe);

            static bool RowSafe(List<int> row)
            {
                if (row.SequenceEqual(row.Order()) || row.SequenceEqual(row.Order().Reverse()))
                {
                    for (int i = 0; i < row.Count - 1; i++)
                    {
                        var diff = Math.Abs(row[i] - row[i + 1]);

                        if (diff < 1 || diff > 3)
                        {
                            return false;
                        }
                    }

                    return true;
                }

                return false;
            }
        }

        public int Problem2()
        {
            return data.Count(row => RowSafe(row, false));

            bool RowSafe(List<int> row, bool prevError)
            {
                bool stepAsc;
                bool? asc = null;

                for (int i = 0; i < row.Count - 1; i++)
                {
                    var diff = row[i] - row[i + 1];

                    asc ??= diff > 0;
                    stepAsc = diff > 0;
                    var abs = Math.Abs(diff);

                    if (abs < 1 || abs > 3 || asc.Value != stepAsc)
                    {
                        if (prevError)
                        {
                            return false;
                        }
                        else
                        {
                            var j = 0;

                            do
                            {
                                var newRow = new List<int>(row);
                                newRow.RemoveAt(j);
                                if (RowSafe(newRow, true))
                                {
                                    return true;
                                }
                                j++;
                            } while (j < row.Count);

                            return false;
                        }
                    }
                }

                return true;
            }
        }
    }
}
