using System;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace MessagesTests.Validation
{
    public class ValidationFixture : IDisposable
    {
        public TestServer Server { get; private set; }
        public HttpClient Client { get; private set; }

        public ValidationFixture()
        {
            Server = new TestServer(new WebHostBuilder().UseStartup<TestStartup>());
            Client = Server.CreateClient();
        }

        public void Dispose() => Client.Dispose();
    }
}