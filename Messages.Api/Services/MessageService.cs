﻿using Messages.Api.Data;
using Messages.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Messages.Api.Services
{
    public class MessageService : IMessageService
    {
        private readonly ApiDbContext _context;
        public MessageService(ApiDbContext context) => _context = context;

        public async Task<Message> Add(Message message)
        {
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            return message;
        }

        public async Task Delete(Message message)
        {
            _context.Remove(message);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Message>> GetAll(int limit, int offset, string term)
        {
            var query = _context.Messages.AsQueryable();
            if (!string.IsNullOrWhiteSpace(term))
            {
                query = query.Where(m => m.Value.ToLower().Contains(term.ToLower()));
            }

            query = query.OrderBy(m => m.Value).Skip(offset).Take(limit);

            return await query.ToListAsync();
        }

        public async Task<Message> GetById(Guid id) => await _context.Messages.SingleOrDefaultAsync(m => m.Id == id);

        public async Task Update(Message message)
        {
            _context.Update(message);
            await _context.SaveChangesAsync();
        }
    }
}
