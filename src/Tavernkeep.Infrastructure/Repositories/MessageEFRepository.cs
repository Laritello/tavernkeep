﻿using Microsoft.EntityFrameworkCore;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Repositories;
using Tavernkeep.Infrastructure.Context;

namespace Tavernkeep.Infrastructure.Repositories
{
    public class MessageEFRepository(SessionContext context) : EntityFrameworkRepository<Message>(context), IMessageRepository
    {
        public async Task<List<Message>> GetMessagesChunkAsync(int skip, int take, CancellationToken cancellationToken = default!)
        {
            var query = AsQueryable().OrderByDescending(x => x.Created).Skip(skip).Take(take).Include(x =>x.Sender);
            return await query.ToListAsync(cancellationToken);
        }
    }
}
