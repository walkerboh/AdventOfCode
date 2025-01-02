namespace AdventOfCode2024
{
    internal class Day4
    {
        private readonly List<List<char>> data;
        private readonly int height;
        private readonly int width;

        public Day4()
        {
            data = File.ReadAllLines(@"Data\\Day4.txt").Select(s => s.ToCharArray().ToList()).ToList();
            height = data.Count;
            width = data[0].Count;
        }

        public int Problem1()
        {
            var count = 0;

            for (int row = 0; row < data.Count; row++)
            {
                for (int col = 0; col < data.Count; col++)
                {
                    if (data[row][col] != 'X')
                    {
                        continue;
                    }

                    // UP
                    if (row >= 3 && $"{data[row][col]}{data[row - 1][col]}{data[row - 2][col]}{data[row - 3][col]}" == "XMAS")
                    {
                        count++;
                    }

                    // UP - RIGHT
                    if (row >= 3 && col < width - 3 && $"{data[row][col]}{data[row - 1][col + 1]}{data[row - 2][col + 2]}{data[row - 3][col + 3]}" == "XMAS")
                    {
                        count++;
                    }

                    // RIGHT
                    if (col < width - 3 && $"{data[row][col]}{data[row][col + 1]}{data[row][col + 2]}{data[row][col + 3]}" == "XMAS")
                    {
                        count++;
                    }

                    // DOWN - RIGHT
                    if (row < height - 3 && col < width - 3 && $"{data[row][col]}{data[row + 1][col + 1]}{data[row + 2][col + 2]}{data[row + 3][col + 3]}" == "XMAS")
                    {
                        count++;
                    }

                    // DOWN
                    if (row < height - 3 && $"{data[row][col]}{data[row + 1][col]}{data[row + 2][col]}{data[row + 3][col]}" == "XMAS")
                    {
                        count++;
                    }
                    // DOWN - LEFT
                    if (row < height - 3 && col >= 3 && $"{data[row][col]}{data[row + 1][col - 1]}{data[row + 2][col - 2]}{data[row + 3][col - 3]}" == "XMAS")
                    {
                        count++;
                    }
                    // LEFT
                    if (col >= 3 && $"{data[row][col]}{data[row][col - 1]}{data[row][col - 2]}{data[row][col - 3]}" == "XMAS")
                    {
                        count++;
                    }
                    // UP - LEFT
                    if (row >= 3 && col >= 3 && $"{data[row][col]}{data[row - 1][col - 1]}{data[row - 2][col - 2]}{data[row - 3][col - 3]}" == "XMAS")
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        public int Problem2()
        {
            var count = 0;

            for (int row = 1; row < data.Count - 1; row++)
            {
                for (int col = 1; col < data.Count - 1; col++)
                {
                    if (data[row][col] != 'A')
                    {
                        continue;
                    }

                    // \-diag
                    var diag1 = $"{data[row - 1][col - 1]}{data[row][col]}{data[row + 1][col + 1]}";
                    if (diag1 == "SAM" || diag1 == "MAS")
                    {
                        // /-diag
                        var diag2 = $"{data[row + 1][col - 1]}{data[row][col]}{data[row - 1][col + 1]}";
                        if(diag2 == "SAM" || diag2 == "MAS")
                        {
                            count++;
                        }
                    }
                }
            }

            return count;
        }

    }
}
