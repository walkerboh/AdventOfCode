namespace AdventOfCode2024
{
    internal class Day21
    {
        private readonly string[] codes;

        public Day21()
        {
            codes = File.ReadAllLines("Data\\Day21.txt");
        }

        public int Problem1()
        {
            var testdata = new string[] { "029A", "980A", "179A", "456A", "379A" };

            var a = testdata.Select(ScoreCode).ToList();

            return 0;

            return codes.Sum(ScoreCode);
        }

        private int ScoreCode(string code)
        {
            var path = ParseKeypadPath(ParseKeypadPath(ParseCodePath(code)));
            var num = int.Parse(code[0..(code.Length - 1)]);

            Console.WriteLine($"{path.Count} * {num} = {path.Count * num}");

            return path.Count * num;
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
            { '0', (3,1) },
            { 'A', (3,2) },
        };

        private List<char> ParseCodePath(string code)
        {
            Coord curPos = (3, 2);
            var path = new List<char>();

            foreach (var digit in code)
            {
                var dest = NumpadPos[digit];

                while (curPos != dest)
                {
                    if (curPos.Row > dest.Row)
                    {
                        path.Add('^');
                        curPos.Row--;
                    }
                    else if (curPos.Col < dest.Col)
                    {
                        path.Add('>');
                        curPos.Col++;
                    }
                    else if (curPos.Row < dest.Row)
                    {
                        path.Add('v');
                        curPos.Row++;
                    }
                    else if (curPos.Col > dest.Col)
                    {
                        path.Add('<');
                        curPos.Col--;
                    }
                }

                path.Add('A');
            }

            return path;
        }

        private readonly Dictionary<char, Coord> KeypadPos = new()
        {
            {'<', (1,0) },
            {'^', (0,1) },
            {'v', (1,1) },
            {'A', (0,2) },
            {'>', (1,2) }
        };

        private List<char> ParseKeypadPath(List<char> path)
        {
            var curPos = KeypadPos['A'];
            var newPath = new List<char>();

            foreach(var key in path)
            {
                var dest = KeypadPos[key];

                while(curPos != dest)
                {
                    if (curPos.Col < dest.Col)
                    {
                        newPath.Add('>');
                        curPos.Col++;
                    }
                    else if (curPos.Row > dest.Row)
                    {
                        newPath.Add('^');
                        curPos.Row--;
                    }
                    else if (curPos.Row < dest.Row)
                    {
                        newPath.Add('v');
                        curPos.Row++;
                    }
                    else if (curPos.Col > dest.Col)
                    {
                        newPath.Add('<');
                        curPos.Col--;
                    }
                }

                newPath.Add('A');
            }

            return newPath;
        }
    }
}
