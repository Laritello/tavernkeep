using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Tavernkeep.Core.Entities.Messages;

namespace Tavernkeep.Infrastructure.Data.Configuration
{
    internal class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.UseTphMappingStrategy();

            builder.HasKey(m => m.Id);

            builder.Property(m => m.Created).IsRequired();
        }
    }
}
