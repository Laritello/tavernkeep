﻿namespace Tavernkeep.Core.Contracts.Authentication.Responses
{
	public class AuthenticationResponse
	{
		public string? AccessToken { get; set; }
		public string? RefreshToken { get; set; }
	}
}
