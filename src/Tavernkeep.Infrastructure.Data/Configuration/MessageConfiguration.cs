using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities;

namespace Tavernkeep.Infrastructure.Data.Configuration
{
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Created).IsRequired();
            builder.Property(m => m.Content).IsRequired();
            builder.Property(m => m.Type)
                .IsRequired()
                .HasConversion(new EnumToStringConverter<MessageType>());

            builder.HasOne(m => m.Sender).WithMany().IsRequired();
            builder.HasOne(m => m.Recipient).WithMany();
        }
    }
}
