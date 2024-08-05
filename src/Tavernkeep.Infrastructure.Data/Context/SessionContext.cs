using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Text.Json;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Entities.Conditions;

namespace Tavernkeep.Infrastructure.Data.Context
{
	public sealed class SessionContext(DbContextOptions<SessionContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

			modelBuilder.Entity<User>().HasData(new User("admin", "admin", UserRole.Master)
            {
                Id = Guid.NewGuid()
            });
		}
	}
}
