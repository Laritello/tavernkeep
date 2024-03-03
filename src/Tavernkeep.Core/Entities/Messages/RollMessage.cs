using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Rolls;

namespace Tavernkeep.Core.Entities.Messages
{
    public class RollMessage : Message
    {
        #region Constructors

        public RollMessage() { }

        #endregion

        #region Properties

        public RollResult Result { get; set; }
        public RollType RollType { get; set; }

        #endregion

        #region Methods

        public override bool CheckVisbility(User user)
        {
            return RollType == RollType.Public || user.Role == UserRole.Master || SenderId == user.Id;
        }

        #endregion
    }
}
