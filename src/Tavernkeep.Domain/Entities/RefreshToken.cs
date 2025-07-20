using System.ComponentModel.DataAnnotations.Schema;
using Tavernkeep.Domain.Entities.Base;

namespace Tavernkeep.Domain.Entities
{
	[Table("RefreshTokens")]
	public class RefreshToken : GuidEntity
	{
		public Guid UserId { get; set; }
		public string Token { get; set; } = default!;
		public DateTime Expires { get; set; }
	}
}
