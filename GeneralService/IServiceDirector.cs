using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralService
{
    public interface IServiceDirector<TService, TIn, TOut>
    {
        Task<TOut> ExecuteAsync(TService service, Func<TIn> configure);
    }

    public class DefaultServiceDirector : IServiceDirector<IService, ServiceRequest, ServiceResponse>
    {
        public async Task<ServiceResponse> ExecuteAsync(IService service, Func<ServiceRequest> configure)
        {
            var request = configure();
            var response = await service.ExecuteAsync(request);
            return response;
        }
    }
}
