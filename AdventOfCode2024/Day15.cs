using System.Collections.Specialized;
using System.Data;

namespace AdventOfCode2024
{
    internal class Day15
    {
        private readonly List<Cell> gridData;
        private readonly List<char> moves;

        public Day15()
        {
            gridData = File.ReadAllLines("Data\\Day15Map.txt").SelectMany((l, rind) => l.Select<char, Cell>((c, cind) => new (c, rind, cind))).ToList();
            moves = [.. File.ReadAllText("Data\\Day15Moves.txt")];
        }

        char GetCellContent(int row, int col) => gridData.Single(c => c.Row == row && c.Col == col).Item;

        void SetCellContent(int row, int col, char item) => gridData.Single(c => c.Row == row && c.Col == col).Item = item;
        void SwapCellContent(int row1, int col1, int row2, int col2)
        {
            var temp = GetCellContent(row2, col2);
            SetCellContent(row2, col2, GetCellContent(row1, col1));
            SetCellContent(row1, col1, temp);
        }

        public int Problem1()
        {
            foreach(var move in moves)
            {
                var (_, row, col) = gridData.Single(c => c.Item == '@');

                if(move == '^')
                {
                    var cell = GetCellContent(row - 1, col);

                    if (cell == '#')
                    {
                        continue;
                    }
                    else if(cell == '.')
                    {
                        SetCellContent(row - 1, col, '@');
                        SetCellContent(row, col, '.');
                    }
                    else
                    {
                        int swapRow = row - 2, swapCol = col;
                        while (GetCellContent(swapRow, swapCol) == 'O')
                        {
                            swapRow--;
                        }

                        if(GetCellContent(swapRow, swapCol) == '.')
                        {
                            SwapCellContent(row - 1, col, swapRow, swapCol);
                            SwapCellContent(row, col, row - 1, col);
                        }
                    }
                }
                else if(move == 'v')
                {
                    var cell = GetCellContent(row + 1, col);

                    if (cell == '#')
                    {
                        continue;
                    }
                    else if (cell == '.')
                    {
                        SetCellContent(row + 1, col, '@');
                        SetCellContent(row, col, '.');
                    }
                    else
                    {
                        int swapRow = row + 2, swapCol = col;
                        while (GetCellContent(swapRow, swapCol) == 'O')
                        {
                            swapRow++;
                        }

                        if (GetCellContent(swapRow, swapCol) == '.')
                        {
                            SwapCellContent(row + 1, col, swapRow, swapCol);
                            SwapCellContent(row, col, row + 1, col);
                        }
                    }
                }
                else if (move == '<')
                {
                    var cell = GetCellContent(row, col -1);

                    if (cell == '#')
                    {
                        continue;
                    }
                    else if (cell == '.')
                    {
                        SetCellContent(row, col - 1, '@');
                        SetCellContent(row, col, '.');
                    }
                    else
                    {
                        int swapRow = row, swapCol = col - 2;
                        while (GetCellContent(swapRow, swapCol) == 'O')
                        {
                            swapCol--;
                        }

                        if (GetCellContent(swapRow, swapCol) == '.')
                        {
                            SwapCellContent(row, col - 1, swapRow, swapCol);
                            SwapCellContent(row, col, row, col - 1);
                        }
                    }
                }
                else if (move == '>')
                {
                    var cell = GetCellContent(row, col + 1);

                    if (cell == '#')
                    {
                        continue;
                    }
                    else if (cell == '.')
                    {
                        SetCellContent(row, col + 1, '@');
                        SetCellContent(row, col, '.');
                    }
                    else
                    {
                        int swapRow = row, swapCol = col + 2;
                        while (GetCellContent(swapRow, swapCol) == 'O')
                        {
                            swapCol++;
                        }

                        if (GetCellContent(swapRow, swapCol) == '.')
                        {
                            SwapCellContent(row, col + 1, swapRow, swapCol);
                            SwapCellContent(row, col, row, col + 1);
                        }
                    }
                }
            }

            return gridData.Where(c => c.Item == 'O').Sum(c => c.Row * 100 + c.Col);
        }

        public int Problem2()
        {
            /*
             * Object based design
             * Grid object
             *      List<Wall>
             *      List<Boxes>
             *      Robot
             * Grid has methods to take a box and check if it can move in direction
             *  Recurse with potentially multiple boxes for moves
             *  Box handles it's own movements
             * Maybe a validity checker to ensure no move breaks the grid
             */

            var grid = new Grid();
            

            foreach(var move in moves)
            {
                if(move == '^')
                {
                    var item = grid.GetItemAtLocation(grid.Robot.Row - 1, grid.Robot.Col);

                    if (item is Grid.Wall)
                    {
                        continue;
                    }
                    else if (item is null)
                    {
                        grid.Robot.Row--;
                    }
                    else if (item is Grid.Box box)
                    {
                        if (grid.TryMoveBoxes([box], Grid.Direction.Up))
                        {
                            grid.Robot.Row--;
                        }
                    }
                }
                else if (move == 'v')
                {
                    var item = grid.GetItemAtLocation(grid.Robot.Row + 1, grid.Robot.Col);

                    if (item is Grid.Wall)
                    {
                        continue;
                    }
                    else if (item is null)
                    {
                        grid.Robot.Row++;
                    }
                    else if (item is Grid.Box box)
                    {
                        if (grid.TryMoveBoxes([box], Grid.Direction.Down))
                        {
                            grid.Robot.Row++;
                        }
                    }
                }
                else if (move == '<')
                {
                    var item = grid.GetItemAtLocation(grid.Robot.Row, grid.Robot.Col - 1);

                    if (item is Grid.Wall)
                    {
                        continue;
                    }
                    else if (item is null)
                    {
                        grid.Robot.Col--;
                    }
                    else if (item is Grid.Box box)
                    {
                        if (grid.TryMoveBoxes([box], Grid.Direction.Left))
                        {
                            grid.Robot.Col--;
                        }
                    }
                }
                else if (move == '>')
                {
                    var item = grid.GetItemAtLocation(grid.Robot.Row, grid.Robot.Col + 1);

                    if (item is Grid.Wall)
                    {
                        continue;
                    }
                    else if (item is null)
                    {
                        grid.Robot.Col++;
                    }
                    else if (item is Grid.Box box)
                    {
                        if (grid.TryMoveBoxes([box], Grid.Direction.Right))
                        {
                            grid.Robot.Col++;
                        }
                    }
                }
            }

            return grid.Boxes.Sum(b => b.Gps);
        }

        private class Grid
        {
            public List<Wall> Walls = [];
            public List<Box> Boxes = [];
            public RobotC Robot;

            public Grid()
            {
                int row = 0, col;

                foreach (var line in File.ReadAllLines("Data\\Day15Map.txt"))
                {
                    col = 0;

                    foreach (var c in line)
                    {
                        if (c == '#')
                        {
                            Walls.Add(new(row, col++));
                            Walls.Add(new(row, col++));
                        }
                        else if (c == 'O')
                        {
                            Boxes.Add(new Box(row, col++, col++));
                        }
                        else if (c == '@')
                        {
                            Robot = new (row, col);
                            col += 2;
                        }
                        else
                        {
                            col += 2;
                        }
                    }

                    row++;
                }

                Robot ??= new(0, 0);
            }

            public IItem? GetItemAtLocation(int row, int col)
            {
                IItem? blocker = null;

                blocker ??= Walls.SingleOrDefault(w => w.Row == row && w.Col == col);
                blocker ??= Boxes.SingleOrDefault(b => b.Intersects(row, col));

                return blocker;
            }

            public bool TryMoveBoxes(IEnumerable<Box> boxes, Direction dir)
            {
                var blockers = new HashSet<IItem>();

                if(dir == Direction.Right)
                {
                    foreach(var box in boxes)
                    {
                        var item = GetItemAtLocation(box.Row, box.Col2 + 1);
                        if(item is not null)
                        {
                            blockers.Add(item);
                        }
                    }
                }
                else if(dir == Direction.Left)
                {
                    foreach (var box in boxes)
                    {
                        var item = GetItemAtLocation(box.Row, box.Col1 - 1);
                        if (item is not null)
                        {
                            blockers.Add(item);
                        }
                    }
                }
                else if (dir == Direction.Up)
                {
                    foreach (var box in boxes)
                    {
                        var item = GetItemAtLocation(box.Row - 1, box.Col1);
                        if (item is not null)
                        {
                            blockers.Add(item);
                        }
                        item = GetItemAtLocation(box.Row - 1, box.Col2);
                        if (item is not null)
                        {
                            blockers.Add(item);
                        }
                    }
                }
                else if (dir == Direction.Down)
                {
                    foreach (var box in boxes)
                    {
                        var item = GetItemAtLocation(box.Row + 1, box.Col1);
                        if (item is not null)
                        {
                            blockers.Add(item);
                        }
                        item = GetItemAtLocation(box.Row + 1, box.Col2);
                        if (item is not null)
                        {
                            blockers.Add(item);
                        }
                    }
                }

                if(blockers.Count == 0)
                {
                    foreach (var box in boxes)
                    {
                        box.Move(dir);
                    }
                    return true;
                }
                else if (blockers.All(b => b is Box))
                {
                    if (TryMoveBoxes(blockers.Cast<Box>(), dir))
                    {
                        foreach (var box in boxes)
                        {
                            box.Move(dir);
                        }
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }

            }

            public class RobotC(int row, int col) : IItem
            {
                public int Row { get; set; } = row;
                public int Col { get; set; } = col;
            }

            public class Wall(int row, int col) : IItem
            {
                public int Row { get; set; } = row;
                public int Col { get; set; } = col;
            }

            public class Box(int row, int col1, int col2) : IItem
            {
                public int Col1 { get; set; } = col1;
                public int Col2 { get; set; } = col2;
                public int Row { get; set; } = row;
                public int Gps => Row * 100 + Col1;

                public bool Intersects(int row, int col)
                {
                    return row == Row && (col == Col1 || col == Col2);
                }

                public void Move(Direction dir)
                {
                    switch (dir)
                    {
                        case Direction.Up:
                            Row--;
                            break;
                        case Direction.Down:
                            Row++;
                            break;
                        case Direction.Right:
                            Col1++;
                            Col2++;
                            break;
                        case Direction.Left:
                            Col1--;
                            Col2--;
                            break;
                    }
                }
            }

            public interface IItem
            { 
            }

            public enum Direction
            {
                Up, Down, Left, Right
            }
        }

        private class Cell(char item, int row, int col)
        {
            public char Item { get; set; } = item;
            public int Row { get; set; } = row;
            public int Col { get; set; } = col;

            public void Deconstruct(out char item, out int row, out int col)
            {
                item = Item;
                row = Row;
                col = Col;
            }
        }
    }
}
