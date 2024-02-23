using Tavernkeep.Core.Contracts.Enums;

namespace Tavernkeep.Core.Entities.Messages
{
    public class RollMessage : Message
    {
        #region Constructors

        public RollMessage() { }

        #endregion

        #region Properties

        public int Result { get; set; }
        public RollType RollType { get; set; }

        #endregion

        #region Methods

        public override bool CheckVisbility(User user)
        {
            return RollType == RollType.Open || user.Role == UserRole.Master || SenderId == user.Id;
        }

        #endregion
    }
}
