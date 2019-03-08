using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Messages.Api.Helpers;
using Messages.Api.Models;
using Messages.Api.Services;

namespace MessagesTests.Services
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
            },
            new Message
            {
                Id = Guid.Parse("118d9937-46c5-4d9c-b027-43840be00224"),
                Value = "Norma is as selfless as I am, Ron."
            },
            new Message
            {
                Id = Guid.Parse("4e153648-db6e-4d37-a322-388177f20b0b"),
                Value = "Data is the new language of business"
            },
            new Message
            {
                Id = Guid.Parse("e32959df-eb7b-4eaf-9dc5-7b0757f8badc"),
                Value = "Data should be explored, not just queried."
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

        public Task<IEnumerable<Message>> GetAll(int limit, int offset, string term)
        {
            var query = _messages.AsEnumerable();
            if (!string.IsNullOrEmpty(term))
            {
                // The string class has a .Contains method, but it's case sensitive.
                // In order to replicate the behavior when talking to a real database,
                // this comparison approach is used: https://stackoverflow.com/a/15464440/
                query = query.Where(m => CultureInfo.CurrentCulture.CompareInfo.IndexOf(m.Value, term, CompareOptions.IgnoreCase) >= 0);
            }

            query = query.OrderBy(m => m.Value).Skip(offset).Take(limit);

            return Task.FromResult(query);
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
