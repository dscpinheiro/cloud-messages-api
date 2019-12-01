using Messages.Api;
using Messages.Api.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Messages.Tests.Validation
{
    [Trait("Category", "Integration")]
    public class ValidationTests : IClassFixture<WebApplicationFactory<Startup>>, IDisposable
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public ValidationTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "CI");
        }

        public void Dispose() => Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", null);

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
    }
}
