using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tavernkeep.Core.Entities.Messages;

namespace Tavernkeep.Infrastructure.Data.Configuration.Messages
{
	internal class MessageConfiguration : IEntityTypeConfiguration<Message>
	{
		public void Configure(EntityTypeBuilder<Message> builder)
		{
			builder.UseTphMappingStrategy();

			builder.HasKey(m => m.Id);

			builder.Property(m => m.Created).IsRequired();
			builder.HasOne(m => m.Sender).WithMany().HasForeignKey(x => x.SenderId).IsRequired();
		}
	}
}
