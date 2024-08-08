using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tavernkeep.Core.Entities.Messages;

namespace Tavernkeep.Infrastructure.Data.Configuration.Messages
{
	public class TextMessageConfiguration : IEntityTypeConfiguration<TextMessage>
	{
		public void Configure(EntityTypeBuilder<TextMessage> builder)
		{
			builder.Property(m => m.Text).IsRequired();
			builder.HasOne(m => m.Recipient).WithMany().HasForeignKey(x => x.RecipientId);
		}
	}
}
