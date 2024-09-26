using Microsoft.EntityFrameworkCore;
using Tavernkeep.Core.Entities.Messages;
using Tavernkeep.Core.Repositories;
using Tavernkeep.Core.Specifications;
using Tavernkeep.Infrastructure.Data.Context;
using Tavernkeep.Infrastructure.Data.Extensions;
using Tavernkeep.Infrastructure.Data.Repositories.Base;

namespace Tavernkeep.Infrastructure.Data.Repositories
{
	public class MessageEFRepository(SessionContext context) : EntityFrameworkGuidRepository<Message>(context), IMessageRepository
	{
		public async Task PurgeMessagesAsync(CancellationToken cancellationToken = default)
		{
			await AsQueryable().ExecuteDeleteAsync(cancellationToken);
		}

		public async Task<List<Message>> GetMessagesChunkAsync(int skip, int take, ISpecification<Message> specification = default!, CancellationToken cancellationToken = default)
		{
			var query = AsQueryable();

			if (specification != null)
				query = EntityFrameworkSpecificationEvaluator<Message>.GetQuery(query, specification);

			query = query.OrderByDescending(x => x.Created).Skip(skip).Take(take).Include(x => x.Sender).Include(x => ((TextMessage)x).Recipient).Reverse();
			return await query.ToListAsync(cancellationToken);
		}
	}
}
