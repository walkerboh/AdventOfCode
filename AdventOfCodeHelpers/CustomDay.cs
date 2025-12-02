using AoCHelper;

namespace AdventOfCodeHelpers
{
    public abstract class CustomDay : BaseDay
    {
        protected string GetInputString() => File.ReadAllText(InputFilePath);
        protected IEnumerable<string> GetInputLines() => File.ReadAllLines(InputFilePath);

        public override uint CalculateIndex()
        {
            var typeName = GetType().Name;

            return uint.TryParse(typeName[(typeName.IndexOf(ClassPrefix) + ClassPrefix.Length)..].TrimStart('_').TrimEnd('a', 'b'), out var index)
                ? index
                : default;
        }
    }
}
