using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Messages.Api.Models;
using Xunit;

namespace MessagesTests.Validation
{
    public class ValidationTests : IClassFixture<ValidationFixture>
    {
        private readonly ValidationFixture _fixture;

        public ValidationTests(ValidationFixture fixture) => _fixture = fixture;

        [Theory, ClassData(typeof(InvalidWriteRequestData))]
        public async Task Validation_InvalidRequest_ReturnsBadRequest(string message)
        {
            var request = new WriteMessageRequest { Message = message };
            var response = await _fixture.Client.PostAsJsonAsync("api/messages", request);

            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Theory, ClassData(typeof(ValidWriteRequestData))]
        public async Task Validation_ValidRequest_ReturnsCreated(string message)
        {
            var request = new WriteMessageRequest { Message = message };
            var response = await _fixture.Client.PostAsJsonAsync("api/messages", request);

            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }
    }
}