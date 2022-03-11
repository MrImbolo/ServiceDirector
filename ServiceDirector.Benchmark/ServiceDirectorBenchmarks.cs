using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace ServiceDirector.Benchmark
{
    [MemoryDiagnoser]
    [RankColumn]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    public class ServiceDirectorBenchmarks
    {
        readonly DefaultService _service = new();
        readonly DefaultServiceDirector _director = new DefaultServiceDirector();

        readonly Func<CancellationToken, Task<ServiceRequest>> _configure = 
            async (ct) => await Task.FromResult(new ServiceRequest { Data = "This a sample director request data" });
        
        [Benchmark]
        public async Task ServiceDirectorExecuteAsync()
        {
            _ = await _director.ExecuteAsync(_service, _configure);
        }


        [Benchmark]
        public async Task PlainServiceExecuteAsync()
        {
            _ = await _service.ExecuteAsync(new() { Data = "This is a sample data" });
        }
    }
}
