using System.ComponentModel.DataAnnotations;

namespace AdventOfCode2024
{
    internal class Day21
    {
        private readonly string[] codes;
        private readonly Dictionary<(Coord, Coord), List<List<char>>> keypadMemo = [];
        private readonly Dictionary<(Coord, Coord), List<List<char>>> NumpadMemo = [];


        public Day21()
        {
            codes = File.ReadAllLines("Data\\Day21.txt");
        }

        public int Problem1()
        {
            return codes.Sum(ScoreCode);
        }

        private int ScoreCode(string code)
        {
            var pathLength = int.MaxValue;

            foreach(var robot1Path in CombinePaths(ParseCodePaths([..code], NumpadPos, NumpadMemo)))
            {
                foreach(var robot2Path in CombinePaths(ParseCodePaths(robot1Path, KeypadPos, keypadMemo)))
                {
                    foreach(var robot3Path in CombinePaths(ParseCodePaths(robot2Path, KeypadPos, keypadMemo)))
                    {
                        pathLength = Math.Min(pathLength, robot3Path.Count);
                    }
                }
            }

            var num = int.Parse(code[0..(code.Length - 1)]);
            return pathLength * num;
        }

        private readonly Dictionary<char, Coord> NumpadPos = new()
        {
            { '7', (0,0) },
            { '8', (0,1) },
            { '9', (0,2) },
            { '4', (1,0) },
            { '5', (1,1) },
            { '6', (1,2) },
            { '1', (2,0) },
            { '2', (2,1) },
            { '3', (2,2) },
            { 'X', (3,0) },
            { '0', (3,1) },
            { 'A', (3,2) },
        };

        private static List<List<List<char>>> ParseCodePaths(List<char> code, Dictionary<char, Coord> keypadCoords, 
            Dictionary<(Coord, Coord), List<List<char>>> memo)
        {
            Coord curPos = keypadCoords['A'];
            var allPaths = new List<List<List<char>>>();

            foreach (var digit in code)
            {
                var digitPaths = new List<List<char>>();
                var dest = keypadCoords[digit];

                if(memo.TryGetValue((curPos, dest), out var paths))
                {
                    allPaths.AddRange(paths);
                    curPos = dest;
                    continue;
                }

                var queue = new Queue<(Coord, List<char>)>();
                queue.Enqueue((curPos, []));

                while (queue.Count > 0)
                {
                    var (cur, path) = queue.Dequeue();

                    if (cur == dest)
                    {
                        digitPaths.Add([..path, 'A']);
                        continue;
                    }

                    if (cur == keypadCoords['X'])
                    {
                        continue;
                    }

                    if(cur.Row > dest.Row)
                    {
                        queue.Enqueue(((cur.Row - 1, cur.Col), [.. path, '^']));
                    }
                    else if(cur.Row < dest.Row)
                    {
                        queue.Enqueue(((cur.Row + 1, cur.Col), [.. path, 'v']));
                    }

                    if(cur.Col > dest.Col)
                    {
                        queue.Enqueue(((cur.Row, cur.Col - 1), [.. path, '<']));
                    }
                    else if(cur.Col < dest.Col)
                    {
                        queue.Enqueue(((cur.Row, cur.Col + 1), [.. path, '>']));
                    }
                }

                memo[(curPos, dest)] = digitPaths;
                allPaths.Add(digitPaths);
                curPos = dest;
            }

            return allPaths;
        }

        private readonly Dictionary<char, Coord> KeypadPos = new()
        {
            { 'X', (0,0) },
            { '<', (1,0) },
            { '^', (0,1) },
            { 'v', (1,1) },
            { 'A', (0,2) },
            { '>', (1,2) }
        };

        private static List<List<char>> CombinePaths(List<List<List<char>>> paths)
        {
            var combinedPaths = new List<List<char>>
            {
                new()
            };

            foreach(var symbolPaths in paths)
            {
                combinedPaths = combinedPaths.SelectMany(cp => symbolPaths, (cp, sp) => new List<char>([..cp, ..sp])).ToList();
            }

            return [..combinedPaths.GroupBy(p => p.Count).OrderBy(p => p.Key).First()];
        }
    }
}
