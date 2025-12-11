namespace AdventOfCode2025
{
    internal class Day11 : CustomDay
    {
        private readonly List<Device> Devices;

        public Day11()
        {
            Devices = [.. GetInputLines().Select(l => new Device(l))];
        }

        public override ValueTask<string> Solve_1()
        {
            var memo = new Dictionary<string, int>();

            PathsOut("you");

            return ValueTask.FromResult(memo["you"].ToString());

            int PathsOut(string deviceName)
            {
                if(deviceName == "out")
                {
                    return 1;
                }

                if(memo.TryGetValue(deviceName, out var count))
                {
                    return count;
                }

                var device = Devices.Single(d => d.Name == deviceName);

                var paths = 0;

                foreach (var connection in device.Connections)
                {
                    paths += PathsOut(connection);
                }

                memo[deviceName] = paths;

                return paths;
            }
        }

        public override ValueTask<string> Solve_2()
        {
            var memo = new Dictionary<string, long>();

            var pathsSvrFft = PathsOut("svr", "fft", "dac", "out");
            memo.Clear();
            var pathsSvrDac = PathsOut("svr", "dac", "fft", "out");
            memo.Clear();
            var pathsFftDac = PathsOut("fft", "dac", "svr", "out");
            memo.Clear();
            var pathsDacFft = PathsOut("dac", "fft", "svr", "out");
            memo.Clear();
            var pathsFftOut = PathsOut("fft", "out", "svr", "dac");
            memo.Clear();
            var pathsDacOut = PathsOut("dac", "out", "svr", "fft");

            var total = (pathsSvrDac * pathsDacFft * pathsFftOut) + (pathsSvrFft * pathsFftDac * pathsDacOut);

            return ValueTask.FromResult(total.ToString());

            long PathsOut(string deviceName, string target, params string[] denial)
            {
                if (deviceName == target)
                {
                    return 1;
                }
                else if (denial.Contains(deviceName))
                {
                    return 0;
                }

                if (memo.TryGetValue(deviceName, out var count))
                {
                    return count;
                }

                var device = Devices.Single(d => d.Name == deviceName);

                long paths = 0;

                foreach (var connection in device.Connections)
                {
                    paths += PathsOut(connection, target, denial);
                }

                memo[deviceName] = paths;

                return paths;
            }
        }

        private class Device
        {
            public Device(string input)
            {
                var colonIndex = input.IndexOf(':');
                Name = input[0..colonIndex];
                Connections = [.. input[(colonIndex + 1)..].Split(' ', (StringSplitOptions)3)];
            }

            public string Name { get; set; }
            public List<string> Connections { get; set; }
        }
    }
}
