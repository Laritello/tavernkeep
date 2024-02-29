using AutoMapper;
using Tavernkeep.Core.Contracts.Users.Dtos;
using Tavernkeep.Core.Entities;

namespace Tavernkeep.Application.Mapping.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();
        }
    }
}
