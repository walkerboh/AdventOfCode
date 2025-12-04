using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace AdventOfCode2015
{
    internal class Day12 : CustomDay
    {
        private readonly string Json;

        public Day12()
        {
            Json = GetInputString();
        }

        public override ValueTask<string> Solve_1()
        {
            var data = JsonDocument.Parse(Json);

            var numbers = ParseElement(data.RootElement);

            return ValueTask.FromResult(numbers.Sum().ToString());

            IEnumerable<int> ParseElement(JsonElement element)
            {
                var kind = element.ValueKind;

                List<int> numericValues = [];

                if (kind == JsonValueKind.Object)
                {
                    foreach (var obj in element.EnumerateObject())
                    {
                        numericValues.AddRange(ParseElement(obj.Value));
                    }
                }
                else if (kind == JsonValueKind.Array)
                {
                    foreach (var obj in element.EnumerateArray())
                    {
                        numericValues.AddRange(ParseElement(obj));
                    }
                }
                else if (kind == JsonValueKind.Number)
                {
                    if (element.TryGetInt32(out var num))
                    {
                        numericValues.Add(num);
                    }
                }

                return numericValues;
            }
        }

        public override ValueTask<string> Solve_2()
        {
            var data = JsonDocument.Parse(Json);

            var numbers = ParseElement(data.RootElement);

            return ValueTask.FromResult(numbers.Sum().ToString());

            IEnumerable<int> ParseElement(JsonElement element)
            {
                var kind = element.ValueKind;

                List<int> numericValues = [];

                if (kind == JsonValueKind.Object)
                {
                    var strs = element.EnumerateObject()
                        .Where(e => e.Value.ValueKind == JsonValueKind.String)
                        .Select(s => s.Value.GetString());

                    if (!strs.Any(s => s.Equals("red")))
                    {
                        foreach (var obj in element.EnumerateObject())
                        {
                            numericValues.AddRange(ParseElement(obj.Value));
                        }
                    }

                }
                else if (kind == JsonValueKind.Array)
                {
                    foreach (var obj in element.EnumerateArray())
                    {
                        numericValues.AddRange(ParseElement(obj));
                    }
                }
                else if (kind == JsonValueKind.Number)
                {
                    if (element.TryGetInt32(out var num))
                    {
                        numericValues.Add(num);
                    }
                }

                return numericValues;
            }
        }
    }
}
