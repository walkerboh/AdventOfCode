namespace AdventOfCode2024
{
    public static class Extensions
    {
        /// <summary>
        /// https://stackoverflow.com/a/3932432
        /// </summary>
        /// <param name="count"></param>
        /// <param name="action"></param>
        public static void Times(this int count, Action action)
        {
            for (int i = 0; i < count; i++)
            {
                action();
            }
        }

        public static bool IsInteger(this decimal d)
        {
            return d % 1 == 0;
        }
    }
}
