
namespace AdventOfCode2025
{
    internal class Day08 : CustomDay
    {
        private readonly List<Box> Boxes = [];

        public Day08()
        {
            var lines = GetInputLines();
            foreach(var line in lines)
            {
                var data = line.Split(',', (StringSplitOptions)3).Select(int.Parse).ToList();
                Boxes.Add(new Box(data[0], data[1], data[2]));
            }
        }

        public override ValueTask<string> Solve_1()
        {
            List<(Box box1, Box box2, double distance)> distances = [];

            for (var i = 0; i < Boxes.Count - 1; i++)
            {
                for(var j = i + 1; j < Boxes.Count; j++)
                {
                    distances.Add((Boxes[i], Boxes[j], GetDistance(Boxes[i], Boxes[j])));
                }
            }

            List<HashSet<Box>> circuits = [];

            foreach(var (box1, box2, distance) in distances.OrderBy(x => x.distance).Take(1000))
            {
                var circuit1 = circuits.SingleOrDefault(x => x.Contains(box1));
                var circuit2 = circuits.SingleOrDefault(x => x.Contains(box2));

                if(circuit1 is not null && circuit2 is not null)
                {
                    if (circuit1 == circuit2)
                    {
                        continue;
                    }

                    var newCircuit = circuit1.Union(circuit2);
                    circuits.Remove(circuit1);
                    circuits.Remove(circuit2);
                    circuits.Add([.. newCircuit]);
                }
                else if(circuit1 is not null)
                {
                    circuit1.Add(box2);
                }
                else if(circuit2 is not null)
                {
                    circuit2.Add(box1);
                }
                else
                {
                    circuits.Add([box1, box2]);
                }
            }

            var largest = circuits.OrderByDescending(x => x.Count).Take(3).Select(x => (long)x.Count).Aggregate((res, cur) => res * cur);

            return ValueTask.FromResult(largest.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            List<(Box box1, Box box2, double distance)> distances = [];

            for (var i = 0; i < Boxes.Count - 1; i++)
            {
                for (var j = i + 1; j < Boxes.Count; j++)
                {
                    distances.Add((Boxes[i], Boxes[j], GetDistance(Boxes[i], Boxes[j])));
                }
            }

            List<List<Box>> circuits = [];
            (Box box1, Box box2) lastConnection = default;

            foreach (var (box1, box2, distance) in distances.OrderBy(x => x.distance))
            {
                var circuit1 = circuits.SingleOrDefault(x => x.Contains(box1));
                var circuit2 = circuits.SingleOrDefault(x => x.Contains(box2));

                if (circuit1 is not null && circuit2 is not null)
                {
                    if(circuit1 == circuit2)
                    {
                        continue;
                    }

                    var newCircuit = circuit1.Union(circuit2).Distinct().ToList();
                    circuits.Remove(circuit1);
                    circuits.Remove(circuit2);
                    circuits.Add(newCircuit);
                }
                else if (circuit1 is not null)
                {
                    circuit1.Add(box2);
                }
                else if (circuit2 is not null)
                {
                    circuit2.Add(box1);
                }
                else
                {
                    circuits.Add([box1, box2]);
                }

                lastConnection = (box1, box2);
            }

            return ValueTask.FromResult(((long)lastConnection.box1.X * lastConnection.box2.X).ToString());
        }

        private static double GetDistance(Box box1, Box box2)
        {
            return Math.Sqrt(Math.Pow(box1.X - box2.X, 2) + Math.Pow(box1.Y - box2.Y, 2) + Math.Pow(box1.Z - box2.Z, 2));
        }

        private record Box(int X, int Y, int Z);
    }
}
