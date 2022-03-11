using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceDirector
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
        Task<ServiceResponse> ExecuteAsync(ServiceRequest request, CancellationToken ct = default);
    }
    public class DefaultService : IService
    {
        private static Random _random = new Random();
        public async Task<ServiceResponse> ExecuteAsync(
            ServiceRequest request, 
            CancellationToken ct = default)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(0.001), ct);

            ct.ThrowIfCancellationRequested();

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
        public Task<ServiceResponse> ExecuteAsync(
            ServiceRequest request, 
            CancellationToken ct = default)
        {
            throw new TaskCanceledException();
        }
    }
    public class InvalidOperationExceptionService : IService
    {
        public Task<ServiceResponse> ExecuteAsync(
            ServiceRequest request, 
            CancellationToken ct = default)
        {
            throw new InvalidOperationException();
        }
    }
}
