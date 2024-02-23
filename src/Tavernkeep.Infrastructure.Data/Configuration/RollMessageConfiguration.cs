using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Messages;

namespace Tavernkeep.Infrastructure.Data.Configuration
{
    public class RollMessageConfiguration : IEntityTypeConfiguration<RollMessage>
    {
        public void Configure(EntityTypeBuilder<RollMessage> builder)
        {
            builder.Property(m => m.RollType)
                .IsRequired()
                .HasConversion(new EnumToStringConverter<RollType>());

            builder.Property(m => m.Result).IsRequired();
        }
    }
}
