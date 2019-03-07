using System.Threading.Tasks;
using Messages.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace MessagesTests.Controllers
{
    public partial class MessagesControllerTests
    {
        [Fact]
        public async Task GivenIAddAPalindromeMessage_ItReturnsCreated_AndPalindromePropertyIsTrue()
        {
            var actionResult = await _controller.Post(_palindromeMessage);

            var createdResult = actionResult as CreatedAtActionResult;
            Assert.NotNull(createdResult);

            var createdMessage = createdResult.Value as ReadMessageViewModel;
            Assert.NotNull(createdMessage);
            Assert.True(createdMessage.IsPalindrome);
            Assert.Equal(_palindromeMessage.Message, createdMessage.Message);
        }

        [Fact]
        public async Task GivenIAddANonPalindromeMessage_ItReturnsCreated_AndPalindromePropertyIsFalse()
        {
            var actionResult = await _controller.Post(_nonPalindromeMessage);

            var createdResult = actionResult as CreatedAtActionResult;
            Assert.NotNull(createdResult);

            var createdMessage = createdResult.Value as ReadMessageViewModel;
            Assert.NotNull(createdMessage);
            Assert.False(createdMessage.IsPalindrome);
            Assert.Equal(_nonPalindromeMessage.Message, createdMessage.Message);
        }
    }
}