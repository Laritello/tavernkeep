using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Tavernkeep.Infrastructure.Data.Utility;

namespace Tavernkeep.Infrastructure.Data.Context
{
	public class SessionContextFactory : IDesignTimeDbContextFactory<SessionContext>
	{
		public SessionContext CreateDbContext(string[] args)
		{
			var optionsBuilder = new DbContextOptionsBuilder<SessionContext>();
			optionsBuilder.UseSqlite(DatabaseContextUtility.GetConnectionString());

			return new SessionContext(optionsBuilder.Options);
		}
	}
}
