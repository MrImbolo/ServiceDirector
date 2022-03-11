using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace ServiceDirector.Benchmark
{
    [MemoryDiagnoser]
    [RankColumn]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    public class ServiceDirectorBenchmarks
    {
        readonly DefaultService _service;
        readonly DefaultServiceDirector _director;

        readonly Func<CancellationToken, Task<ServiceRequest>> _configure = 
            async (ct) => await Task.FromResult(new ServiceRequest { Data = "This a sample director request data" });

        public ServiceDirectorBenchmarks()
        {
            _service = new();
            _director = new(_service);
        }

        [Benchmark]
        public async Task ServiceDirectorExecuteAsync()
        {
            _ = await _director.ExecuteAsync(_configure);
        }


        [Benchmark]
        public async Task PlainServiceExecuteAsync()
        {
            _ = await _service.ExecuteAsync(new() { Data = "This is a sample data" });
        }
    }
}
