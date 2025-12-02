using AoCHelper;

namespace AdventOfCodeHelpers
{
    public abstract class CustomDay : BaseDay
    {
        protected string GetInputString() => File.ReadAllText(InputFilePath);
        protected IEnumerable<string> GetInputLines() => File.ReadAllLines(InputFilePath);
    }
}
