using ServiceDirector;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ServiceDirectorTests
{
    public class DefaultServiceDirectorTests
    {
        [Fact]
        public async Task ExecuteAsync_Ok_IsBeingExecutedOk()
        {
            // assign
            var director = new DefaultServiceDirector();
            var service = new DefaultService();

            ServiceRequest request2 = new() { Data = "This a sample director request data" };

            Debug.WriteLine($"Request with sample data '{request2.Data}' created. Sending using service director...");
            
            // act
            var response2 = await director.ExecuteAsync(service, async (ct) =>
            {
                return await Task.FromResult(request2);
            });

            Debug.WriteLine($"Send completed. Response is '{response2.HttpStatusCode}:{response2.Result}'");

            // assert
            Assert.True(response2.Data == request2.Data, "Invalid data passthrough");
            Assert.True(response2.HttpStatusCode == HttpStatusCode.OK);
            Assert.True(!string.IsNullOrEmpty(response2.Result), "Invalid  response result");
        }
        
        [Fact]
        public async Task ExecuteAsync_ComplexAsyncConfigure_IsBeingExecutedOk()
        {
            // assign
            var director = new DefaultServiceDirector();
            var service = new DefaultService();

            ServiceRequest request2 = new() { Data = "This a sample director request data" };

            Debug.WriteLine($"Request with sample data '{request2.Data}' created. Sending using service director...");

            // act
            var response2 = await director.ExecuteAsync(service, async (ct) =>
            {
                var res = await Task.WhenAll(
                    service.ExecuteAsync(new() { Data = "1" }, ct),
                    service.ExecuteAsync(new() { Data = "2" }, ct),
                    service.ExecuteAsync(new() { Data = "3" }, ct));

                request2.Data += res.Aggregate("", (r, x) => r += "," + x.Data);

                return await Task.FromResult(request2);
            });

            Debug.WriteLine($"Send completed. Response is '{response2.HttpStatusCode}:{response2.Result}'");

            // assert
            Assert.True(response2.Data == request2.Data, "Invalid data passthrough");
            Assert.True(response2.HttpStatusCode == HttpStatusCode.OK);
            Assert.True(!string.IsNullOrEmpty(response2.Result), "Invalid  response result");
        }


        [Fact]
        public async Task ExecuteAsync_TaskCancelledByCaller_TaskCancelledExceptionThrown()
        {
            // assign
            var director = new DefaultServiceDirector();
            var service = new DefaultService();
            using var cte = new CancellationTokenSource();

            cte.Cancel();

            ServiceRequest request2 = new() { Data = "This a sample director request data" };

            // act
            // assert

            await Assert.ThrowsAsync<TaskCanceledException>(async () =>
            {
                await director.ExecuteAsync(service, async (ct) =>
                {
                    return await Task.FromResult(request2);
                }, cte.Token);
            });
        }

        [Fact]
        public async Task ExecuteAsync_TaskCancelledExceptionThrown_ExceptionThrown()
        {
            // assign
            var director = new DefaultServiceDirector();
            var service = new TaskCancelledExceptionService();

            ServiceRequest request2 = new() { Data = "This a sample director request data" };

            // act
            // assert

            await Assert.ThrowsAsync<TaskCanceledException>(async () =>
            {
                await director.ExecuteAsync(service, async (ct) =>
                {
                    return await Task.FromResult(request2);
                }, CancellationToken.None);
            });
        }

        [Fact]
        public async Task ExecuteAsync_InvalidOperationExcecption_ExceptionThrown()
        {
            // assign
            var director = new DefaultServiceDirector();
            var service = new InvalidOperationExceptionService();

            ServiceRequest request2 = new() { Data = "This a sample director request data" };

            // act
            // assert

            await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await director.ExecuteAsync(service, async (ct) =>
                {
                    return await Task.FromResult(request2);
                }, CancellationToken.None);
            });
        }
    }
}
