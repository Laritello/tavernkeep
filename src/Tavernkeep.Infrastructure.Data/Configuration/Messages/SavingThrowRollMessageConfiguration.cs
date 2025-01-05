using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Tavernkeep.Core.Entities.Messages;
using Tavernkeep.Infrastructure.Data.Extensions;

namespace Tavernkeep.Infrastructure.Data.Configuration.Messages
{
	public class SavingThrowRollMessageConfiguration : IEntityTypeConfiguration<SavingThrowRollMessage>
	{
		public void Configure(EntityTypeBuilder<SavingThrowRollMessage> builder)
		{
			builder.OwnsJson(m => m.SavingThrow);
		}
	}
}
