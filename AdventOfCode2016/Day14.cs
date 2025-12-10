using System.Runtime.CompilerServices;

namespace AdventOfCode2016
{
    internal class Day14 : CustomDay
    {
        private readonly string Salt;

        public Day14()
        {
            Salt = GetInputString();
        }

        public override ValueTask<string> Solve_1()
        {
            var hashQueue = new Queue<Hash>();
            var ind = 0;
            var found = 0;

            while(hashQueue.Count < 1000)
            {
                hashQueue.Enqueue(new Hash(ind, $"{Salt}{ind++}".Md5Hash()));
            }

            while(true)
            {
                var nextPossible = hashQueue.Dequeue();
                hashQueue.Enqueue(new Hash(ind, $"{Salt}{ind++}".Md5Hash()));

                if (nextPossible.Triple.HasValue)
                {
                    if (hashQueue.Any(h => h.Quints.Contains(nextPossible.Triple.Value)))
                    {
                        if(++found == 64)
                        {
                            return ValueTask.FromResult(nextPossible.Index.ToString());
                        }
                    }
                }
            }
        }

        public override ValueTask<string> Solve_2()
        {
            var hashQueue = new Queue<Hash>();
            var ind = 0;
            var found = 0;

            while (hashQueue.Count < 1000)
            {
                hashQueue.Enqueue(new Hash(ind, LoopedHash(ind++)));
            }

            while (true)
            {
                var nextPossible = hashQueue.Dequeue();
                hashQueue.Enqueue(new Hash(ind, LoopedHash(ind++)));

                if (nextPossible.Triple.HasValue)
                {
                    if (hashQueue.Any(h => h.Quints.Contains(nextPossible.Triple.Value)))
                    {
                        if (++found == 64)
                        {
                            return ValueTask.FromResult(nextPossible.Index.ToString());
                        }
                    }
                }
            }

            string LoopedHash(int ind)
            {
                var hash = $"{Salt}{ind}".Md5Hash();

                for(var i = 0; i < 2016; i++)
                {
                    hash = hash.Md5Hash();
                }

                return hash;
            }
        }

        private class Hash
        {
            public Hash(int index, string input)
            {
                Index = index;
                HashValue = input;

                for (var i = 0; i < input.Length - 2; i++)
                {
                    if (input[i] == input[i + 1] && input[i + 1] == input[i + 2])
                    {
                        if (!Triple.HasValue)
                        {
                            Triple = input[i];
                        }

                        if (i < input.Length - 4 && input[i + 2] == input[i + 3] && input[i + 3] == input[i + 4])
                        {
                            Quints.Add(input[i]);
                        }
                    }
                }
            }

            public int Index { get; set; }
            public string HashValue { get; set; }
            public char? Triple { get; set; }
            public List<char> Quints { get; set; } = [];
        }
    }
}
