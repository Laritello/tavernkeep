using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tavernkeep.Core.Entities;

namespace Tavernkeep.Core.EntityFramework.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.HasIndex(u => u.Login).IsUnique();

            builder.Property(u => u.Login).IsRequired();
            builder.Property(u => u.Password).IsRequired();
        }
    }
}
