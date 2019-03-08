using System;
using Messages.Api.Controllers;
using Messages.Api.Models;
using Messages.Api.Services;
using MessagesTests.Services;

namespace MessagesTests.Controllers
{
    public partial class MessagesControllerTests
    {
        private readonly MessagesController _controller;

        private readonly Guid _existentMessageId = Guid.Parse("9eca944a-4f75-4b15-b738-d2a2e0573bfe");
        private readonly Guid _nonExistentMessageId = Guid.Parse("30c9ecb7-2a8f-4cb0-8a23-32b9cf72ec05");

        private readonly WriteMessageRequest _palindromeMessage = new WriteMessageRequest
        {
            Message = "Live on time, emit no evil"
        };

        private readonly WriteMessageRequest _nonPalindromeMessage = new WriteMessageRequest
        {
            Message = "A palindrome is a word, number, sentence, or verse that reads the same backward or forward."
        };

        public MessagesControllerTests()
        {
            var messagesService = new InMemoryMessageService();
            _controller = new MessagesController(messagesService);
        }
    }
}