using System.Text;

namespace AdventOfCode2016
{
    internal class Day04 : CustomDay
    {
        private readonly List<string> Rooms;
        private readonly List<(string room, int id)> ValidRooms = [];

        public Day04()
        {
            Rooms = GetInputLines();
        }

        public override ValueTask<string> Solve_1()
        {
            var total = 0;

            foreach(var room in Rooms)
            {
                var checkIndex = room.IndexOf('[');
                var checkSum = room[checkIndex..].Trim('[', ']');
                var roomSplit = room[..checkIndex].Split('-');
                var id = int.Parse(roomSplit.Last());
                var letters = roomSplit[..^1].SelectMany(x => x);
                var letterGrp = letters.GroupBy(x => x).OrderByDescending(x => x.Count()).ThenBy(x => x.Key);

                var calculatedCheckSum = letterGrp.Select(x => x.Key).Take(5);

                if (checkSum == new string([.. calculatedCheckSum]))
                {
                    total += id;
                    ValidRooms.Add((room[..checkIndex], id));
                }
            }

            return ValueTask.FromResult(total.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            var chars = Enumerable.Range((int)'a', 26).Select(Convert.ToChar).ToArray();

            foreach(var room in ValidRooms)
            {
                var sb = new StringBuilder();

                foreach(var c in room.room)
                {
                    if (c == '-')
                    {
                        sb.Append(' ');
                    }
                    else
                    {
                        sb.Append(chars[(chars.IndexOf(c) + room.id) % chars.Length]);
                    }
                }

                var roomString = sb.ToString();

                if(roomString.Contains("north") && roomString.Contains("pole"))
                {
                    return ValueTask.FromResult(room.id.ToString());
                }
            }

            return ValueTask.FromResult("Not found");
        }
    }
}
