using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace MessagesTests.Controllers
{
    [Trait("Category", "Unit")]
    public partial class MessagesControllerTests
    {
        [Fact]
        public async Task Get_AllMessages_ReturnsOk()
        {
            var actionResult = await _controller.Get();
            var messages = actionResult.Value;

            Assert.NotNull(messages);
            Assert.NotEmpty(messages);
            Assert.True(messages.Count() < 100);
        }

        [Fact]
        public async Task Get_ExistingMessage_ReturnsOk()
        {
            var actionResult = await _controller.Get(_existentMessageId);
            var message = actionResult.Value;

            Assert.NotNull(message);
            Assert.Equal(_existentMessageId, message.Id);
        }

        [Fact]
        public async Task Get_UnknownMessage_ReturnsNotFound()
        {
            var actionResult = await _controller.Get(_nonExistentMessageId);

            Assert.Null(actionResult.Value);
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }

        [Fact]
        public async Task Get_MessagesWithInvalidOffset_ReturnsBadRequest()
        {
            var actionResult = await _controller.Get(offset: -1);

            Assert.Null(actionResult.Value);
            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        }

        [Fact]
        public async Task Get_MessagesWithValidOffset_ReturnsOk()
        {
            var actionResult = await _controller.Get();
            var allMessages = actionResult.Value;

            actionResult = await _controller.Get(offset: 1);
            var filteredMessages = actionResult.Value;

            Assert.NotEqual(allMessages, filteredMessages);
            Assert.Equal(allMessages.Count() - 1, filteredMessages.Count());
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(1001)]
        public async Task Get_MessagesWithInvalidLimit_ReturnsBadRequest(int invalidLimit)
        {
            var actionResult = await _controller.Get(limit: invalidLimit);

            Assert.Null(actionResult.Value);
            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(1000)]
        public async Task Get_MessagesWithValidLimit_ReturnsOk(int validLimit)
        {
            var actionResult = await _controller.Get(limit: validLimit);
            var filteredMessagesCount = actionResult.Value.Count();

            Assert.True(filteredMessagesCount <= validLimit);
        }

        [Fact]
        public async Task Get_MessagesWithUnknownSearchTerm_ReturnsNoItems()
        {
            var actionResult = await _controller.Get(term: Guid.NewGuid().ToString());
            var filteredMessages = actionResult.Value;

            Assert.Empty(filteredMessages);
        }

        [Fact]
        public async Task Get_MessagesWithValidSearchTerm_ReturnsOk()
        {
            var actionResult = await _controller.Get(term: "palindrome");
            var filteredMessages = actionResult.Value;

            Assert.NotEmpty(filteredMessages);
        }

        [Fact]
        public async Task Get_MessagesWithDifferentCaseTerm_ReturnsOk()
        {
            var actionResult = await _controller.Get(term: "pAlINdroMe");
            var filteredMessages = actionResult.Value;

            Assert.NotEmpty(filteredMessages);
        }
    }
}
