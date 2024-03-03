using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tavernkeep.Core.Entities.Messages;
using Tavernkeep.Infrastructure.Data.Extensions;

namespace Tavernkeep.Infrastructure.Data.Configuration.Messages
{
    public class SkillRollMessageConfiguration : IEntityTypeConfiguration<SkillRollMessage>
    {
        public void Configure(EntityTypeBuilder<SkillRollMessage> builder)
        {
            builder.OwnsJson(m => m.Skill);
        }
    }
}
