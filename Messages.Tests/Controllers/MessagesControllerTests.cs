using Messages.Api.Controllers;
using Messages.Api.Data;
using Messages.Api.Models;
using Messages.Api.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Messages.Tests.Controllers
{
    public partial class MessagesControllerTests : IDisposable
    {
        private readonly MessagesController _controller;
        private readonly ApiDbContext _context;
        private bool _isDisposed;

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
                Id = Guid.Parse("d497d810-eab8-4c61-a0fc-62f3f679bb97"),
                Value = "Noël a trop par rapport à Léon",
                IsPalindrome = true
            },
            new Message
            {
                Id = Guid.Parse("7dc21730-62f2-4462-81ed-d6e36ee1daec"),
                Value = "Te pék, láttál képet?",
                IsPalindrome = true
            },
            new Message
            {
                Id = Guid.Parse("d7c14bf3-5d4f-4d20-aaef-460896010641"),
                Value = "Eh ! ça va la vache",
                IsPalindrome = true
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

        ~MessagesControllerTests()
        {
            Dispose(false);
        }

        private static DbContextOptions<ApiDbContext> CreateInMemoryOptions()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            return new DbContextOptionsBuilder<ApiDbContext>()
                .UseInMemoryDatabase(databaseName: "UnitTestsDB")
                .UseInternalServiceProvider(serviceProvider)
                .Options;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            if (_isDisposed)
            {
                return;
            }

            if (isDisposing)
            {
                _context.Dispose();
            }

            _isDisposed = true;
        }
    }
}
