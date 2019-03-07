using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Messages.Api.Helpers;
using Messages.Api.Models;

namespace Messages.Api.Services
{
    public class InMemoryMessageService : IMessageService
    {
        private readonly List<Message> _messages = new List<Message>
        {
            new Message
            {
                Id = Guid.Parse("65c97fad-1cd7-49f7-9ca4-fe19d0eded5b"),
                Value = "Was it a car or a cat I saw?"
            },
            new Message
            {
                Id = Guid.Parse("9eca944a-4f75-4b15-b738-d2a2e0573bfe"),
                Value = "this is not a palindrome"
            }
        };

        public InMemoryMessageService()
        {
            foreach (var message in _messages)
            {
                message.IsPalindrome = message.Value.IsPalindrome();
            }
        }

        public Task<Message> Add(Message message)
        {
            message.Id = Guid.NewGuid();
            _messages.Add(message);

            return Task.FromResult(message);
        }

        public Task Delete(Message message)
        {
            _messages.Remove(message);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<Message>> GetAll()
        {
            return Task.FromResult(_messages.AsEnumerable());
        }

        public Task<Message> GetById(Guid id)
        {
            var message = _messages.SingleOrDefault(m => m.Id == id);
            return Task.FromResult(message);
        }

        public Task Update(Message message)
        {
            var existing = _messages.Single(m => m.Id == message.Id);
            existing = message;

            return Task.CompletedTask;
        }
    }
}
