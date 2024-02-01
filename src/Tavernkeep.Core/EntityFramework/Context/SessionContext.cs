using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Tavernkeep.Core.EntityFramework.Context
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
