namespace Tavernkeep.Core.Entities.Messages
{
    public class TextMessage : Message
    {
        #region Constructors

        public TextMessage() { }

        #endregion

        #region Properties

        public string Text { get; set; } = default!;

        #endregion
    }
}
