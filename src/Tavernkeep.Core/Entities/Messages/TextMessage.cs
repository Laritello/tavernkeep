using Tavernkeep.Core.Contracts.Enums;

namespace Tavernkeep.Core.Entities.Messages
{
    public class TextMessage : Message
    {
        #region Constructors

        public TextMessage() { }

        #endregion

        #region Properties

        public string Text { get; set; } = default!;
        public Guid? RecipientId { get; set; }
        public User? Recipient { get; set; }

        #endregion

        #region Methods

        public override bool CheckVisbility(User user)
        {
            return SenderId == user.Id || RecipientId == null || RecipientId == user.Id;
        }

        #endregion
    }
}
