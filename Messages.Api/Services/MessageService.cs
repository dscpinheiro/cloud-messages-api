using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Messages.Api.Helpers;
using Messages.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Messages.Api.Services
{
    public class MessageService : IMessageService
    {
        private readonly ApiDbContext _context;

        public MessageService(ApiDbContext context)
        {
            _context = context;
            _context.Database.Migrate();
        }

        public Task<Message> Add(Message message)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Message message)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Message>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Message> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task Update(Message message)
        {
            throw new NotImplementedException();
        }
    }
}