using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tavernkeep.Core.Entities;

namespace Tavernkeep.Core.EntityFramework.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.Login).IsUnique();
            builder.Property(x => x.Login).IsRequired();

            builder.Property(x => x.Password).IsRequired();
        }
    }
}
