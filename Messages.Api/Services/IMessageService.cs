﻿using Messages.Api.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Messages.Api.Services
{
    public interface IMessageService
    {
        Task<IEnumerable<Message>> GetAll(int limit, int offset, string term);
        Task<Message> GetById(Guid id);
        Task<Message> Add(Message message);
        Task Update(Message message);
        Task Delete(Message message);
    }
}
