using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceDirector.Benchmark
{
    [MemoryDiagnoser]
    [RankColumn]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    public class ServiceDirectorBenchmarks
    {
        [Benchmark]
        public static async Task ServiceDirectorExecuteAsync()
        {
            var service = new DefaultService();
            var director = new DefaultServiceDirector();

            ServiceRequest request2 = new() { Data = "This a sample director request data" };

            _ = await director.ExecuteAsync(service, async (ct) =>
            {
                return await Task.FromResult(request2);
            });
        }


        [Benchmark]
        public static async Task PlainServiceExecuteAsync()
        {
            var service = new DefaultService();
            ServiceRequest request1 = new() { Data = "This is a sample data" };

            _ = await service.ExecuteAsync(request1);
        }
    }
}
