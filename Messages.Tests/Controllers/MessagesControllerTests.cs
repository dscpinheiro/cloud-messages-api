using System;
using System.Collections.Generic;
using Messages.Api.Controllers;
using Messages.Api.Data;
using Messages.Api.Models;
using Messages.Api.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MessagesTests.Controllers
{
    public partial class MessagesControllerTests : IDisposable
    {
        private readonly MessagesController _controller;
        private readonly ApiDbContext _context;

        private readonly List<Message> _sampleMessages = new List<Message>
        {
            new Message
            {
                Id = Guid.Parse("65c97fad-1cd7-49f7-9ca4-fe19d0eded5b"),
                Value = "Was it a car or a cat I saw?",
                IsPalindrome = true
            },
            new Message
            {
                Id = Guid.Parse("9eca944a-4f75-4b15-b738-d2a2e0573bfe"),
                Value = "this is not a palindrome",
                IsPalindrome = false
            },
            new Message
            {
                Id = Guid.Parse("118d9937-46c5-4d9c-b027-43840be00224"),
                Value = "Norma is as selfless as I am, Ron.",
                IsPalindrome = true
            },
            new Message
            {
                Id = Guid.Parse("4e153648-db6e-4d37-a322-388177f20b0b"),
                Value = "Data is the new language of business",
                IsPalindrome = false
            },
            new Message
            {
                Id = Guid.Parse("e32959df-eb7b-4eaf-9dc5-7b0757f8badc"),
                Value = "Data should be explored, not just queried.",
                IsPalindrome = false
            }
        };

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
            _context = new ApiDbContext(CreateInMemoryOptions());
            _context.AddRange(_sampleMessages);
            _context.SaveChanges();

            var messagesService = new MessageService(_context);
            _controller = new MessagesController(messagesService);
        }

        private static DbContextOptions<ApiDbContext> CreateInMemoryOptions()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            return new DbContextOptionsBuilder<ApiDbContext>()
                .UseInMemoryDatabase(databaseName: "UnitTestsDatabase")
                .UseInternalServiceProvider(serviceProvider)
                .Options;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}