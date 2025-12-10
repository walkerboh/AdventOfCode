namespace AdventOfCode2016
{
    internal class Day17 : CustomDay
    {
        private readonly string Passcode;

        private static readonly string OpenChars = "bcdef";

        private const int Up = 0;
        private const int Down = 1;
        private const int Left = 2;
        private const int Right = 3;

        public Day17()
        {
            Passcode = GetInputString();
        }

        public override ValueTask<string> Solve_1()
        {
            var queue = new Queue<((int x, int y) pos, string steps)>();
            queue.Enqueue(((0, 0), ""));
            var goal = (3, 3);

            while (queue.TryDequeue(out var state))
            {
                var hash = $"{Passcode}{state.steps}".Md5Hash();

                if(state.pos.x > 0 && Open(hash, Left))
                {
                    var newPos = (state.pos.x - 1, state.pos.y);

                    queue.Enqueue((newPos, $"{state.steps}L"));
                }

                if (state.pos.x < 3 && Open(hash, Right))
                {
                    var newPos = (state.pos.x + 1, state.pos.y);
                    if (newPos == goal)
                    {
                        return ValueTask.FromResult($"{state.steps}R");
                    }

                    queue.Enqueue((newPos, $"{state.steps}R"));
                }

                if (state.pos.y > 0 && Open(hash, Up))
                {
                    var newPos = (state.pos.x, state.pos.y - 1);

                    queue.Enqueue((newPos, $"{state.steps}U"));
                }

                if (state.pos.y < 3 && Open(hash, Down))
                {
                    var newPos = (state.pos.x, state.pos.y + 1);
                    if (newPos == goal)
                    {
                        return ValueTask.FromResult($"{state.steps}D");
                    }

                    queue.Enqueue((newPos, $"{state.steps}D"));
                }
            }

            return ValueTask.FromResult("Somehow missed an infinite loop");

            static bool Open(string hash, int dir)
            {
                return OpenChars.Contains(hash[dir]);
            }
        }

        public override ValueTask<string> Solve_2()
        {
            var queue = new Queue<((int x, int y) pos, string steps)>();
            queue.Enqueue(((0, 0), ""));
            var goal = (3, 3);
            var longest = 0;

            while (queue.TryDequeue(out var state))
            {
                var hash = $"{Passcode}{state.steps}".Md5Hash();

                if (state.pos.x > 0 && Open(hash, Left))
                {
                    var newPos = (state.pos.x - 1, state.pos.y);
                    if (newPos == goal)
                    {
                        if (state.steps.Length + 1 > longest)
                        {
                            longest = state.steps.Length + 1;
                        }
                    }
                    else
                    {
                        queue.Enqueue((newPos, $"{state.steps}L"));
                    }
                }

                if (state.pos.x < 3 && Open(hash, Right))
                {
                    var newPos = (state.pos.x + 1, state.pos.y);
                    if (newPos == goal)
                    {
                        if (state.steps.Length + 1 > longest)
                        {
                            longest = state.steps.Length + 1;
                        }
                    }
                    else
                    {
                        queue.Enqueue((newPos, $"{state.steps}R"));
                    }
                }

                if (state.pos.y > 0 && Open(hash, Up))
                {
                    var newPos = (state.pos.x, state.pos.y - 1);
                    if (newPos == goal)
                    {
                        if (state.steps.Length + 1 > longest)
                        {
                            longest = state.steps.Length + 1;
                        }
                    }
                    else
                    {
                        queue.Enqueue((newPos, $"{state.steps}U"));
                    }
                }

                if (state.pos.y < 3 && Open(hash, Down))
                {
                    var newPos = (state.pos.x, state.pos.y + 1);
                    if (newPos == goal)
                    {
                        if (state.steps.Length + 1 > longest)
                        {
                            longest = state.steps.Length + 1;
                        }
                    }
                    else
                    {
                        queue.Enqueue((newPos, $"{state.steps}D"));
                    }
                }
            }

            return ValueTask.FromResult(longest.ToString());

            static bool Open(string hash, int dir)
            {
                return OpenChars.Contains(hash[dir]);
            }
        }
    }
}
