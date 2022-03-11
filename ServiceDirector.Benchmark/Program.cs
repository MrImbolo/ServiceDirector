using BenchmarkDotNet.Running;
using System;

namespace ServiceDirector.Benchmark;

public class Program
{
    static void Main()
    {
        _ = BenchmarkRunner.Run<ServiceDirectorBenchmarks>();
    }
}