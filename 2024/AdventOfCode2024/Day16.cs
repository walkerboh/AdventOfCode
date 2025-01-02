namespace AdventOfCode2024
{
    internal class Day16
    {
        private readonly List<List<char>> maze;
        private readonly (int, int) startCoords, endCoords;
        private int bestScore = int.MaxValue;
        private readonly Dictionary<(int, int), int> scores = [];
        private readonly Dictionary<int, HashSet<(int, int)>> pathScores = [];

        public enum Direction { North, East, South, West }

        public Day16()
        {
            maze = File.ReadAllLines("Data\\Day16.txt").Select(l => l.ToList()).ToList();

            for(var i = 0; i < maze.Count; i++)
            {
                for(var j = 0; j < maze[i].Count; j++)
                {
                    if (maze[i][j] == 'S')
                    {
                        startCoords = (i, j);
                    }
                    else if (maze[i][j] == 'E')
                    {
                        endCoords = (i, j);
                    }
                }
            }
        }

        public int Problem1()
        {
            Dfs(startCoords, []);

            return bestScore;
        }

        public int Problem2()
        {
            Dfs(startCoords, []);

            return pathScores[bestScore].Count;
        }

        private void Dfs((int, int) loc, HashSet<(int, int)> visited, Direction curDir = Direction.East, int score = 0)
        {
            visited.Add(loc);

            if (loc == endCoords)
            {
                if (score == 143580)
                {
                    bestScore = score;

                    if(pathScores.TryGetValue(score, out var scoreVisited))
                    {
                        pathScores[score] = [.. scoreVisited, .. visited];
                        Console.WriteLine(pathScores[score].Count);
                    }
                    else
                    {
                        pathScores[score] = [.. visited];
                    }

                    return;
                }
            }

            //if (scores.TryGetValue(loc, out var bestLocScore))
            //{
            //    if (score > bestLocScore)
            //    {
            //        return;
            //    }
            //    else if(score < bestLocScore)
            //    {
            //        scores[loc] = score;
            //    }
            //}
            //else
            //{
            //    scores.Add((loc.Item1, loc.Item2), score);
            //}

            if(score > bestScore)
            {
                return;
            }

            var north = (loc.Item1- 1, loc.Item2);
            var northItem = maze[north.Item1][north.Item2];
            var south = (loc.Item1 + 1, loc.Item2);
            var southItem = maze[south.Item1][south.Item2];
            var east = (loc.Item1, loc.Item2 + 1);
            var eastItem = maze[east.Item1][east.Item2];
            var west = (loc.Item1, loc.Item2 - 1);
            var westItem = maze[west.Item1][west.Item2];

            if ((northItem == '.' || northItem == 'E') && !visited.Contains(north))
            {
                Dfs(north, [..visited, north], Direction.North, score + 1 + GetTurnScore(curDir, Direction.North));
            }
            if ((southItem == '.' || southItem == 'E') && !visited.Contains(south))
            {
                Dfs(south, [.. visited, south], Direction.South, score + 1 + GetTurnScore(curDir, Direction.South));
            }
            if ((eastItem == '.' || eastItem == 'E') && !visited.Contains(east))
            {
                Dfs(east, [.. visited, east], Direction.East, score + 1 + GetTurnScore(curDir, Direction.East));
            }
            if ((westItem == '.' || westItem == 'E') && !visited.Contains(west))
            {
                Dfs(west, [.. visited, west], Direction.West, score + 1 + GetTurnScore(curDir, Direction.West));
            }
        }

        private static int GetTurnScore(Direction curDir, Direction newDir)
        {
            if(curDir == newDir)
            {
                return 0;
            }

            switch (curDir)
            {
                case Direction.North: 
                    switch(newDir)
                    {
                        case Direction.East:
                        case Direction.West:
                            return 1000;
                        case Direction.South:
                            return 2000;
                    }
                    break;
                case Direction.East:
                    switch (newDir)
                    {
                        case Direction.North:
                        case Direction.South:
                            return 1000;
                        case Direction.West:
                            return 2000;
                    }
                    break;
                case Direction.South:
                    switch (newDir)
                    {
                        case Direction.East:
                        case Direction.West:
                            return 1000;
                        case Direction.North:
                            return 2000;
                    }
                    break;
                case Direction.West:
                    switch (newDir)
                    {
                        case Direction.North:
                        case Direction.South:
                            return 1000;
                        case Direction.East:
                            return 2000;
                    }
                    break;
            }

            return int.MaxValue;
        }
    }
}
