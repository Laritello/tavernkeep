using AutoMapper;
using Tavernkeep.Core.Contracts.Users.Dtos;
using Tavernkeep.Core.Entities;

namespace Tavernkeep.Application.Mapping.Profiles
{
	/// <summary>
	/// Mapping profile for the <see cref="User"/> class.
	/// </summary>
	public class UserProfile : Profile
	{
		public UserProfile()
		{
			CreateMap<User, UserDto>()
				.ForMember(x => x.CharactersId, opt => opt.MapFrom(u => u.Characters.Select(x => x.Id)));
		}
	}
}
