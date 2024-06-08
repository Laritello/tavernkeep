using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tavernkeep.Core.Entities;

namespace Tavernkeep.Infrastructure.Data.Configuration
{
    public class ConditionInfoConfiguration : IEntityTypeConfiguration<CondtionInfo>
    {
        public void Configure(EntityTypeBuilder<CondtionInfo> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired();

            builder.HasOne(x => x.Secondary);
            builder.OwnsMany(x => x.Modifiers, b => b.ToJson());
        }
    }
}
