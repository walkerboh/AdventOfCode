namespace AdventOfCode2024
{
    internal class Day6
    {
        private readonly List<List<char>> data;
        private readonly int startRow = -1, startCol = -1, height, width;

        public Day6()
        {
            data = File.ReadAllLines("Data\\Day6.txt").Select(s => s.ToCharArray().ToList()).ToList();
            height = data.Count;
            width = data[0].Count;
            for (int i = 0; i < height && startRow == -1; i++)
            {
                for (int j = 0; j < width && startRow == -1; j++)
                {
                    if (data[i][j] == '^')
                    {
                        startRow = i;
                        startCol = j;
                    }
                }
            }
        }

        public int Problem1()
        {
            int curRow = startRow, curCol = startCol;
            var curDir = Direction.Up;
            var visited = new HashSet<(int, int)>();

            while (curRow >= 0 && curRow < height && curCol >= 0 && curCol < width)
            {
                switch (curDir)
                {
                    case Direction.Up:
                        if (curRow == 0 || data[curRow - 1][curCol] != '#')
                        {
                            curRow--;
                            visited.Add((curRow, curCol));
                        }
                        else
                        {
                            curDir = Direction.Right;
                        }
                        break;
                    case Direction.Down:
                        if (curRow == height - 1 || data[curRow + 1][curCol] != '#')
                        {
                            curRow++;
                            visited.Add((curRow, curCol));
                        }
                        else
                        {
                            curDir = Direction.Left;
                        }
                        break;
                    case Direction.Right:
                        if (curCol == width - 1 || data[curRow][curCol + 1] != '#')
                        {
                            curCol++;
                            visited.Add((curRow, curCol));
                        }
                        else
                        {
                            curDir = Direction.Down;
                        }
                        break;
                    case Direction.Left:
                        if (curCol == 0 || data[curRow][curCol - 1] != '#')
                        {
                            curCol--;
                            visited.Add((curRow, curCol));
                        }
                        else
                        {
                            curDir = Direction.Up;
                        }
                        break;
                }
            }

            return visited.Count;
        }

        public int Problem2()
        {
            var total = 0;

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (data[i][j] == '.')
                    {
                        var temp = new List<List<char>>(data.Select(x => x.ToList()));
                        temp[i][j] = '#';

                        if (RunMap(temp))
                        {
                            total++;
                        }
                    }
                }
            }

            return total;

            bool RunMap(List<List<char>> data)
            {
                int curRow = startRow, curCol = startCol;
                var curDir = Direction.Up;
                var visited = new HashSet<(int, int, Direction)>
                {
                    (curRow, curCol, Direction.Up)
                };

                while (curRow >= 0 && curRow < height && curCol >= 0 && curCol < width)
                {
                    switch (curDir)
                    {
                        case Direction.Up:
                            if (curRow == 0 || data[curRow - 1][curCol] != '#')
                            {
                                if (visited.Contains((curRow - 1, curCol, curDir)))
                                {
                                    return true;
                                }

                                curRow--;
                                visited.Add((curRow, curCol, Direction.Up));
                            }
                            else
                            {
                                curDir = Direction.Right;
                            }
                            break;
                        case Direction.Down:
                            if (curRow == height - 1 || data[curRow + 1][curCol] != '#')
                            {
                                if (visited.Contains((curRow + 1, curCol, curDir)))
                                {
                                    return true;
                                }

                                curRow++;
                                visited.Add((curRow, curCol, Direction.Down));
                            }
                            else
                            {
                                curDir = Direction.Left;
                            }
                            break;
                        case Direction.Right:
                            if (curCol == width - 1 || data[curRow][curCol + 1] != '#')
                            {
                                if (visited.Contains((curRow, curCol + 1, curDir)))
                                {
                                    return true;
                                }

                                curCol++;
                                visited.Add((curRow, curCol, Direction.Right));
                            }
                            else
                            {
                                curDir = Direction.Down;
                            }
                            break;
                        case Direction.Left:
                            if (curCol == 0 || data[curRow][curCol - 1] != '#')
                            {
                                if (visited.Contains((curRow, curCol - 1, curDir)))
                                {
                                    return true;
                                }

                                curCol--;
                                visited.Add((curRow, curCol, Direction.Left));
                            }
                            else
                            {
                                curDir = Direction.Up;
                            }
                            break;
                    }
                }

                return false;
            }
        }

        public int Problem2Fail()
        {
            int curRow = startRow, curCol = startCol;
            var curDir = Direction.Up;
            var visited = new HashSet<(int, int, Direction)>
            {
                (curRow, curCol, Direction.Up)
            };
            var obstructions = new List<(int, int)>();

            while (curRow >= 0 && curRow < height && curCol >= 0 && curCol < width)
            {
                switch (curDir)
                {
                    case Direction.Up:
                        if (curRow == 0 || data[curRow - 1][curCol] != '#')
                        {
                            if (visited.Contains((curRow, curCol, Direction.Right)))
                            {
                                obstructions.Add((curRow - 1, curCol));
                            }

                            curRow--;
                            visited.Add((curRow, curCol, Direction.Up));
                        }
                        else
                        {
                            curDir = Direction.Right;
                        }
                        break;
                    case Direction.Down:
                        if (curRow == height - 1 || data[curRow + 1][curCol] != '#')
                        {
                            if (visited.Contains((curRow, curCol, Direction.Left)))
                            {
                                obstructions.Add((curRow + 1, curCol));
                            }

                            curRow++;
                            visited.Add((curRow, curCol, Direction.Down));
                        }
                        else
                        {
                            curDir = Direction.Left;
                        }
                        break;
                    case Direction.Right:
                        if (curCol == width - 1 || data[curRow][curCol + 1] != '#')
                        {
                            if (visited.Contains((curRow, curCol, Direction.Down)))
                            {
                                obstructions.Add((curRow, curCol + 1));
                            }

                            curCol++;
                            visited.Add((curRow, curCol, Direction.Right));
                        }
                        else
                        {
                            curDir = Direction.Down;
                        }
                        break;
                    case Direction.Left:
                        if (curCol == 0 || data[curRow][curCol - 1] != '#')
                        {
                            if (visited.Contains((curRow, curCol, Direction.Up)))
                            {
                                obstructions.Add((curRow, curCol - 1));
                            }

                            curCol--;
                            visited.Add((curRow, curCol, Direction.Left));
                        }
                        else
                        {
                            curDir = Direction.Up;
                        }
                        break;
                }
            }

            return obstructions.Count(o => o != (startRow, startCol) && o.Item1 >= 0 && o.Item1 < height && o.Item2 >= 0 && o.Item2 < width);
        }

        enum Direction
        {
            Up,
            Right,
            Down,
            Left
        }
    }
}
