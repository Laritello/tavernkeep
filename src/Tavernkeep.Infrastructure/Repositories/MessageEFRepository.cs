using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Repositories;
using Tavernkeep.Infrastructure.Context;

namespace Tavernkeep.Infrastructure.Repositories
{
    public class MessageEFRepository(SessionContext context) : EntityFrameworkRepository<Message>(context), IMessageRepository
    {
    }
}
