using Messages.Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Xunit;

namespace Messages.Tests.Controllers
{
    [Trait("Category", "Unit")]
    public partial class MessagesControllerTests
    {
        [Fact]
        public async Task Add_PalindromeMessage_ReturnsCreatedAndPalindromePropertyIsTrue()
        {
            var actionResult = await _controller.Post(_palindromeMessage);

            var createdResult = actionResult as CreatedAtActionResult;
            Assert.NotNull(createdResult);

            var createdMessage = createdResult.Value as ReadMessageResponse;
            Assert.NotNull(createdMessage);
            Assert.True(createdMessage.IsPalindrome);
            Assert.Equal(_palindromeMessage.Message, createdMessage.Message);
        }

        [Fact]
        public async Task Add_NonPalindromeMessage_ReturnsCreatedAndPalindromePropertyIsFalse()
        {
            var actionResult = await _controller.Post(_nonPalindromeMessage);

            var createdResult = actionResult as CreatedAtActionResult;
            Assert.NotNull(createdResult);

            var createdMessage = createdResult.Value as ReadMessageResponse;
            Assert.NotNull(createdMessage);
            Assert.False(createdMessage.IsPalindrome);
            Assert.Equal(_nonPalindromeMessage.Message, createdMessage.Message);
        }
    }
}
