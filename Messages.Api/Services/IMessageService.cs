using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Messages.Api.Models;

namespace Messages.Api.Services
{
    public interface IMessageService
    {
        Task<IEnumerable<Message>> GetAll();
        Task<Message> GetById(Guid id);
        Task<Message> Add(Message message);
        Task Update(Message message);
        Task Delete(Message message);
    }
}