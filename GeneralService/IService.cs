using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GeneralService
{
    public class ServiceRequest
    {
        public string Data { get; set; }
    }
    public class ServiceResponse
    {
        public HttpStatusCode HttpStatusCode { get; set; }
        public string Data { get; set; }
        public string Result { get; set; }
    }
    public interface IService
    {
        Task<ServiceResponse> ExecuteAsync(ServiceRequest request);
    }
    public class DefaultService : IService
    {
        private static Random _random = new Random();
        public async Task<ServiceResponse> ExecuteAsync(ServiceRequest request)
        {
            await Task.Delay(_random.Next(100, 150));

            return new()
            {
                HttpStatusCode = HttpStatusCode.OK,
                Data = request.Data,
                Result = "This is fine..."
            };
        }
    }
    public class TaskCancelledExceptionService : IService
    {
        public Task<ServiceResponse> ExecuteAsync(ServiceRequest request)
        {
            throw new TaskCanceledException();
        }
    }
}
