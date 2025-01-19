using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tavernkeep.Core.Entities.Messages;

namespace Tavernkeep.Infrastructure.Data.Configuration.Messages
{
	public class SavingThrowRollMessageConfiguration : IEntityTypeConfiguration<SavingThrowRollMessage>
	{
		public void Configure(EntityTypeBuilder<SavingThrowRollMessage> builder)
		{
			builder.OwnsOne(m => m.SavingThrow, b => b.ToJson());
		}
	}
}
