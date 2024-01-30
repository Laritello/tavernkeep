using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tavernkeep.Core.Entities;

namespace Tavernkeep.Core.EntityFramework.Configuration
{
    public class SessionConfiguration : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            builder.HasKey(s => s.Id);

            builder.HasIndex(s => s.Name).IsUnique();
            builder.Property(s => s.Name).IsRequired();

            builder.HasMany(s => s.Users).WithOne(u => u.Session);
        }
    }
}
