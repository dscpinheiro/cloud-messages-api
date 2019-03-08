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
            if (!string.IsNullOrEmpty(term))
            {
                query = query.Where(m => EF.Functions.ILike(m.Value, $"%{term}%"));
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