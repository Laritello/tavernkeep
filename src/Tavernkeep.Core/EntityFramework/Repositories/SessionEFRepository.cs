using Tavernkeep.Core.Entities;
using Tavernkeep.Core.EntityFramework.Context;
using Tavernkeep.Core.Repositories;

namespace Tavernkeep.Core.EntityFramework.Repositories
{
    public class SessionEFRepository(ApplicationContext context) : EntityFrameworkRepository<Session>(context), ISessionRepository
    {

    }
}
