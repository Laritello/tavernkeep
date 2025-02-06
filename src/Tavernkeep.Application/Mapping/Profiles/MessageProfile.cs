using AutoMapper;
using Tavernkeep.Core.Contracts.Chat.Dtos;
using Tavernkeep.Core.Entities.Messages;

namespace Tavernkeep.Application.Mapping.Profiles
{
	/// <summary>
	/// Mapping profile for the <see cref="Message"/> class and its derived classes.
	/// </summary>
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
