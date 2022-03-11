using GeneralService;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GeneralServiceTests
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
            var response2 = await director.ExecuteAsync(service, () =>
            {
                return request2;
            });

            Debug.WriteLine($"Send completed. Response is '{response2.HttpStatusCode}:{response2.Result}'");

            // assert
            Assert.True(response2.Data == request2.Data, "Invalid data passthrough");
            Assert.True(response2.HttpStatusCode == HttpStatusCode.OK);
            Assert.True(!string.IsNullOrEmpty(response2.Result), "Invalid  response result");
        }
    }
}
