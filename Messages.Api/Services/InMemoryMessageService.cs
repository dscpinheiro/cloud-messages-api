using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Messages.Api.Models;

namespace Messages.Api.Services
{
    public class InMemoryMessageService : IMessageService
    {
        private readonly List<Message> _messages = new List<Message>();

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