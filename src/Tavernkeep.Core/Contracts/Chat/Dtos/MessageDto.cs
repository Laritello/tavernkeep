﻿using System.Text.Json.Serialization;
using Tavernkeep.Core.Contracts.Users.Dtos;
using Tavernkeep.Core.Entities.Messages;

namespace Tavernkeep.Core.Contracts.Chat.Dtos
{
	[JsonDerivedType(typeof(TextMessageDto), typeDiscriminator: nameof(TextMessage))]
	[JsonDerivedType(typeof(RollMessageDto), typeDiscriminator: nameof(RollMessage))]
	[JsonDerivedType(typeof(SkillRollMessageDto), typeDiscriminator: nameof(SkillRollMessage))]
	public abstract class MessageDto
	{
		public Guid Id { get; set; }
		public Guid? CharacterId { get; set; }
		public string DisplayName { get; set; } = default!;
		public UserDto Sender { get; set; } = default!;
		public DateTime Created { get; set; }
	}
}
