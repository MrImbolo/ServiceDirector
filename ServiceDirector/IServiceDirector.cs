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
        Task<TOut> ExecuteAsync(Func<CancellationToken, Task<TIn>> configure, CancellationToken ct = default);
    }

    public class DefaultServiceDirector : IServiceDirector<IService, ServiceRequest, ServiceResponse>
    {
        private readonly IService _service;

        public DefaultServiceDirector(IService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public async Task<ServiceResponse> ExecuteAsync(
            Func<CancellationToken, Task<ServiceRequest>> configure,
            CancellationToken ct = default)
        {
            var request = await configure(ct);
            var response = await _service.ExecuteAsync(request, ct);
            return response;
        }
    }
}
