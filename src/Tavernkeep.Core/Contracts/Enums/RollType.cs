namespace Tavernkeep.Core.Contracts.Enums
{
    public enum RollType
    {
        /// <summary>
        /// Everyone can see the notification and the result.
        /// </summary>
        Public,

        /// <summary>
        /// Everyone can see the notification, but only the master can see the result.
        /// </summary>
        Secret,

        /// <summary>
        /// Only master and performer of the roll can see the notification and the result.
        /// </summary>
        Private
    }
}
