﻿using Microsoft.EntityFrameworkCore;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Repositories;
using Tavernkeep.Infrastructure.Data.Context;

namespace Tavernkeep.Infrastructure.Data.Repositories
{
    public class MessageEFRepository(SessionContext context) : EntityFrameworkRepository<Message>(context), IMessageRepository
    {
        public async Task PurgeMessagesAsync(CancellationToken cancellationToken = default)
        {
            await AsQueryable().ExecuteDeleteAsync(cancellationToken);
        }

        public async Task<List<Message>> GetMessagesChunkAsync(int skip, int take, CancellationToken cancellationToken = default!)
        {
            var query = AsQueryable().OrderByDescending(x => x.Created).Skip(skip).Take(take).Include(x =>x.Sender);
            return await query.ToListAsync(cancellationToken);
        }
    }
}
