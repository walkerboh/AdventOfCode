namespace AdventOfCode2016
{
    internal class Day13 : CustomDay
    {
        private readonly int FavoriteNumber;

        public Day13()
        {
            FavoriteNumber = int.Parse(GetInputString());
        }

        public override ValueTask<string> Solve_1()
        {
            var seen = new HashSet<(int, int)>();
            var goal = (x: 31, y: 39);
            var queue = new PriorityQueue<(int x, int y), int>();
            queue.Enqueue((1, 1), 0);

            while(queue.TryDequeue(out var pos, out var steps))
            {
                seen.Add(pos);

                if(!seen.Contains((pos.x + 1, pos.y)) && !IsWall(pos.x + 1, pos.y))
                {
                    if((pos.x + 1, pos.y) == goal)
                    {
                        return ValueTask.FromResult((steps + 1).ToString());
                    }

                    queue.Enqueue((pos.x + 1, pos.y), steps + 1);
                }

                if (!seen.Contains((pos.x - 1, pos.y)) && !IsWall(pos.x - 1, pos.y))
                {
                    if ((pos.x - 1, pos.y) == goal)
                    {
                        return ValueTask.FromResult((steps + 1).ToString());
                    }

                    queue.Enqueue((pos.x - 1, pos.y), steps + 1);
                }

                if (!seen.Contains((pos.x, pos.y + 1)) && !IsWall(pos.x, pos.y + 1))
                {
                    if ((pos.x, pos.y + 1) == goal)
                    {
                        return ValueTask.FromResult((steps + 1).ToString());
                    }

                    queue.Enqueue((pos.x, pos.y + 1), steps + 1);
                }

                if (!seen.Contains((pos.x, pos.y - 1)) && !IsWall(pos.x, pos.y - 1))
                {
                    if ((pos.x, pos.y - 1) == goal)
                    {
                        return ValueTask.FromResult((steps + 1).ToString());
                    }

                    queue.Enqueue((pos.x, pos.y - 1), steps + 1);
                }
            }

            return ValueTask.FromResult("No path found");
        }

        public override ValueTask<string> Solve_2()
        {
            var seen = new HashSet<(int, int)>();
            var queue = new PriorityQueue<(int x, int y), int>();
            queue.Enqueue((1, 1), 0);

            while (queue.TryDequeue(out var pos, out var steps))
            {
                seen.Add(pos);

                if(steps == 50)
                {
                    continue;
                }

                if (!seen.Contains((pos.x + 1, pos.y)) && !IsWall(pos.x + 1, pos.y))
                {
                    queue.Enqueue((pos.x + 1, pos.y), steps + 1);
                }

                if (pos.x > 0 && !seen.Contains((pos.x - 1, pos.y)) && !IsWall(pos.x - 1, pos.y))
                {
                    queue.Enqueue((pos.x - 1, pos.y), steps + 1);
                }

                if (!seen.Contains((pos.x, pos.y + 1)) && !IsWall(pos.x, pos.y + 1))
                {
                    queue.Enqueue((pos.x, pos.y + 1), steps + 1);
                }

                if (pos.y > 0 && !seen.Contains((pos.x, pos.y - 1)) && !IsWall(pos.x, pos.y - 1))
                {
                    queue.Enqueue((pos.x, pos.y - 1), steps + 1);
                }
            }

            return ValueTask.FromResult(seen.Count.ToString());
        }

        private bool IsWall(int x, int y)
        {
            var num = FavoriteNumber + ((x * x) + (3 * x) + (2 * x * y) + y + (y * y));
            var ones = Convert.ToString(num, 2).Count(c => c == '1');
            return (ones % 2) == 1;
        }
    }
}
