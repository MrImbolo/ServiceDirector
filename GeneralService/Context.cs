using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralService
{
    public class Context
    {
        public static async Task Main()
        {
            var service = new DefaultService();
            ServiceRequest request1 = new() { Data = "This is a sample data" };


            Console.WriteLine($"Request with sample data '{request1.Data}' created. Sending using service itself...");
            var response1 = await service.ExecuteAsync(request1);

            Console.WriteLine($"Send completed. Response is '{response1.HttpStatusCode}:{response1.Result}'");

            
            
            
            var director = new DefaultServiceDirector();

            ServiceRequest request2 = new() { Data = "This a sample director request data" };
            
            Console.WriteLine($"Request with sample data '{request2.Data}' created. Sending using service director...");
            var response2 = await director.ExecuteAsync(service, async (ct) =>
            {
                return await Task.FromResult(request2);
            });

            Console.WriteLine($"Send completed. Response is '{response2.HttpStatusCode}:{response2.Result}'");
        }
    }
}
