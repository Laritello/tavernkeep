using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tavernkeep.Core.Entities.Messages;

namespace Tavernkeep.Infrastructure.Data.Configuration.Messages
{
	public class SkillRollMessageConfiguration : IEntityTypeConfiguration<SkillRollMessage>
	{
		public void Configure(EntityTypeBuilder<SkillRollMessage> builder)
		{
			builder.OwnsOne(m => m.Skill, b => b.ToJson());
		}
	}
}
