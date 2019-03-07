using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace MessagesTests.Controllers
{
    public partial class MessagesControllerTests
    {
        [Fact]
        public async Task GivenIDeleteAnExistingMessage_ItReturnsNoContent()
        {
            var getActionResult = await _controller.Get();
            var allItems = getActionResult.Value;

            var toBeDeleted = allItems.First();

            var deleteActionResult = await _controller.Delete(toBeDeleted.Id);
            Assert.IsType<NoContentResult>(deleteActionResult);

            getActionResult = await _controller.Get();
            allItems = getActionResult.Value;
            Assert.DoesNotContain(toBeDeleted, allItems);
        }

        [Fact]
        public async Task GivenITryDeletingAnUnknownMessage_ItReturnsNotFound()
        {
            var actionResult = await _controller.Delete(_nonExistentMessageId);
            Assert.IsType<NotFoundResult>(actionResult);
        }
    }
}