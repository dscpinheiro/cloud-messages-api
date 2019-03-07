using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace MessagesTests.Controllers
{
    public partial class MessagesControllerTests
    {
        [Fact]
        public async Task GivenIGetAllMessages_ItReturnsOk()
        {
            var actionResult = await _controller.Get();
            var messages = actionResult.Value;

            Assert.NotNull(messages);
            Assert.True(messages.Any());
        }

        [Fact]
        public async Task GivenIGetAnExistingMessage_ItReturnsOk()
        {
            var actionResult = await _controller.Get(_existentMessageId);
            var message = actionResult.Value;

            Assert.NotNull(message);
            Assert.Equal(_existentMessageId, message.Id);
        }

        [Fact]
        public async Task GivenITryGettingAnUnknownMessage_ItReturnsNotFound()
        {
            var actionResult = await _controller.Get(_nonExistentMessageId);

            Assert.Null(actionResult.Value);
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }
    }
}