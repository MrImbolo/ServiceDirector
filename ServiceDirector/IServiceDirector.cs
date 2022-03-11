using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceDirector
{
    public interface IServiceDirector<TService, TIn, TOut>
    {
        Task<TOut> ExecuteAsync(TService service, Func<CancellationToken, Task<TIn>> configure, CancellationToken ct = default);
    }

    public class DefaultServiceDirector : IServiceDirector<IService, ServiceRequest, ServiceResponse>
    {
        public async Task<ServiceResponse> ExecuteAsync(
            IService service,
            Func<CancellationToken, Task<ServiceRequest>> configure,
            CancellationToken ct = default)
        {
            var request = await configure(ct);
            var response = await service.ExecuteAsync(request, ct);
            return response;
        }
    }
}
