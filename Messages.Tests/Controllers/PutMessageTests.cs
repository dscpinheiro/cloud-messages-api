using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Messages.Tests.Controllers
{
    [Trait("Category", "Unit")]
    public partial class MessagesControllerTests
    {
        [Fact]
        public async Task Update_MessageToBeAPalindrome_ReturnsNoContentAndPalindromePropertyIsTrue()
        {
            var getMessageActionResult = await _controller.Get(_existentMessageId);
            var currentMessage = getMessageActionResult.Value;

            var updateActionResult = await _controller.Put(_existentMessageId, _palindromeMessage);
            Assert.IsType<NoContentResult>(updateActionResult);

            getMessageActionResult = await _controller.Get(_existentMessageId);
            var updatedMessage = getMessageActionResult.Value;

            Assert.Equal(currentMessage.Id, updatedMessage.Id);
            Assert.False(currentMessage.IsPalindrome);
            Assert.True(updatedMessage.IsPalindrome);
            Assert.Equal(_palindromeMessage.Message, updatedMessage.Message);
        }

        [Fact]
        public async Task Update_UnknownMessage_ReturnsNotFound()
        {
            var actionResult = await _controller.Put(_nonExistentMessageId, _palindromeMessage);
            Assert.IsType<NotFoundResult>(actionResult);
        }
    }
}
