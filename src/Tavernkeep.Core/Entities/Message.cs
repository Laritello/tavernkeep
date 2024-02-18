using System.ComponentModel.DataAnnotations.Schema;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Base;

namespace Tavernkeep.Core.Entities
{
    [Table("Messages")]
    public class Message : Entity
    {

        #region Constructors

        public Message() : this(MessageType.Text, string.Empty) { }

        public Message(MessageType type, string content)
        {
            Type = type;
            Content = content;
        }

        #endregion

        #region Properties

        public User Sender { get; set; } = default!;
        public User? Recipient { get; set; }
        public MessageType Type { get; set; }
        public DateTime Created { get; set; }
        public string Content { get; set; }

        #endregion
    }
}
