using System.ComponentModel.DataAnnotations.Schema;
using Tavernkeep.Core.Entities.Base;

namespace Tavernkeep.Core.Entities
{
    [Table("RefreshTokens")]
    public class RefreshToken : Entity
    {
        public Guid UserId { get; set; }
        public string Token { get; set; } = default!;
        public DateTime Expires { get; set; }
    }
}
