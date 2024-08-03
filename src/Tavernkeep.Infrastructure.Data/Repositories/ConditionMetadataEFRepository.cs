using Tavernkeep.Core.Entities.Conditions;
using Tavernkeep.Infrastructure.Data.Context;

namespace Tavernkeep.Infrastructure.Data.Repositories
{
	public class ConditionMetadataEFRepository(SessionContext context) : EntityFrameworkRepository<ConditionMetadata>(context)
	{

	}
}
