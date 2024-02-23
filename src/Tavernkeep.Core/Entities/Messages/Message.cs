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
        public DateTime Created { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Checks whether message visible for the user or not.
        /// </summary>
        /// <param name="user"><see cref="User"/> that requested permission to see the message.</param>
        /// <returns>Message visibility.</returns>
        public abstract bool CheckVisbility(User user);

        #endregion
    }
}
