using System.ComponentModel.DataAnnotations.Schema;
using Tavernkeep.Core.Entities.Base;

namespace Tavernkeep.Core.Entities.Messages
{
	[Table("Messages")]
	public abstract class Message : GuidEntity
	{
		#region Constructors

		public Message() { }

		#endregion

		#region Properties
		/// <summary>
		/// Displayed name as a sender.
		/// </summary>
		public string DisplayName { get; set; } = default!;
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
