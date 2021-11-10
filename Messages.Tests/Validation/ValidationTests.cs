using Messages.Api;
using Messages.Api.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using Xunit;

namespace Messages.Tests.Validation
{
    [Trait("Category", "Integration")]
    public class ValidationTests : IClassFixture<WebApplicationFactory<Startup>>, IDisposable
    {
        private readonly WebApplicationFactory<Startup> _factory;
        private bool _isDisposed;

        public ValidationTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "CI");
        }

        ~ValidationTests()
        {
            Dispose(false);
        }

        [Theory, ClassData(typeof(InvalidWriteRequestData))]
        public async Task Validation_InvalidRequest_ReturnsBadRequest(string message)
        {
            var client = _factory.CreateClient();
            var request = new WriteMessageRequest { Message = message };
            var response = await client.PostAsJsonAsync("api/messages", request);

            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Theory, ClassData(typeof(ValidWriteRequestData))]
        public async Task Validation_ValidRequest_ReturnsCreated(string message)
        {
            var client = _factory.CreateClient();
            var request = new WriteMessageRequest { Message = message };
            var response = await client.PostAsJsonAsync("api/messages", request);

            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            if (_isDisposed)
            {
                return;
            }

            if (isDisposing)
            {
                Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", null);
            }

            _isDisposed = true;
        }
    }
}
