using System.Text.Json.Serialization;
using Tavernkeep.Core.Contracts.Enums;

namespace Tavernkeep.Core.Contracts.Users.Requests
{
	/// <summary>
	/// Represents a request to edit a user.
	/// </summary>
	public class EditUserRequest
	{
		/// <summary>
		/// The user's login.
		/// </summary>
		public string Login { get; set; } = default!;

		/// <summary>
		/// The user's password.
		/// </summary>
		public string Password { get; set; } = default!;

		/// <summary>
		/// The user's role.
		/// </summary>
		[JsonConverter(typeof(JsonStringEnumConverter))]
		public UserRole Role { get; set; } = default!;
	}
}
