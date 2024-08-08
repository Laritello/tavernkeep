using AutoMapper;
using Tavernkeep.Core.Contracts.Chat.Dtos;
using Tavernkeep.Core.Entities.Messages;

namespace Tavernkeep.Application.Mapping.Profiles
{
	public class MessageProfile : Profile
	{
		public MessageProfile()
		{
			CreateMap<Message, MessageDto>()
				.Include<TextMessage, TextMessageDto>()
				.Include<RollMessage, RollMessageDto>();

			CreateMap<TextMessage, TextMessageDto>();

			CreateMap<RollMessage, RollMessageDto>()
				.Include<SkillRollMessage, SkillRollMessageDto>();

			CreateMap<SkillRollMessage, SkillRollMessageDto>();
		}
	}
}
