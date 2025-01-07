using AdventOfCode2024;
using System.Diagnostics;

var start = Stopwatch.GetTimestamp();
Console.WriteLine(new Day24().Problem1());
Console.WriteLine($"{Stopwatch.GetElapsedTime(start)}");

//var runtimes = new List<long>();

//var stopwatch = new Stopwatch();

//var day7 = new Day7();

//for(var i = 0; i < 100; i++)
//{
//    stopwatch.Reset();
//    stopwatch.Start();
//    day7.Problem1Iterative();
//    stopwatch.Stop();
//    runtimes.Add(stopwatch.ElapsedMilliseconds);
//}

//Console.WriteLine($"Iterative: {runtimes.Sum() / 100m}ms avg");

//runtimes.Clear();

//for (var i = 0; i < 100; i++)
//{
//    stopwatch.Reset();
//    stopwatch.Start();
//    day7.Problem1Recursive();
//    stopwatch.Stop();
//    runtimes.Add(stopwatch.ElapsedMilliseconds);
//}

//Console.WriteLine($"Recursive: {runtimes.Sum() / 100m}ms avg");