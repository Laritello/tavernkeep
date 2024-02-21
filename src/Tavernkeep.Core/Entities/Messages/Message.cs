using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Tavernkeep.Core.Entities.Base;

namespace Tavernkeep.Core.Entities.Messages
{
    [Table("Messages")]
    [JsonDerivedType(typeof(TextMessage), typeDiscriminator: nameof(TextMessage))]
    [JsonDerivedType(typeof(RollMessage), typeDiscriminator: nameof(RollMessage))]
    public abstract class Message : Entity
    {
        #region Constructors

        public Message() { }

        #endregion

        #region Properties
        public Guid SenderId { get; set; }
        public User Sender { get; set; } = default!;
        public Guid? RecipientId { get; set; }
        public User? Recipient { get; set; }
        public DateTime Created { get; set; }

        #endregion
    }
}
