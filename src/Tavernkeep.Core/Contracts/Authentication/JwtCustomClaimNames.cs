namespace Tavernkeep.Core.Contracts.Authentication
{
	public struct JwtCustomClaimNames
	{
		/// <summary>
		/// The name for a claim that specifies the id of a user.
		/// </summary>
		public const string UserId = "userId";

		/// <summary>
		/// The name for a claim that specifies the login of a user.
		/// </summary>
		public const string UserLogin = "userLogin";

		/// <summary>
		/// The name for a claim that specifies the role of a user.
		/// </summary>
		public const string UserRole = "userRole";
	}
}
