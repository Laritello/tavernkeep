using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Tavernkeep.Infrastructure.Data.Context
{
    public sealed class SessionContext(DbContextOptions<SessionContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
