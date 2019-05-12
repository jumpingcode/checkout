using Microsoft.AspNetCore.TestHost;

namespace Checkout.IntegrationTests
{
    public class IntegrationTestServerProvider
    {
        protected IntegrationTestServerProvider()
        {
            var host = Program.CreateWebHostBuilder();
            Server = new TestServer(host);
        }

        protected TestServer Server { get; set; }
    }
}
