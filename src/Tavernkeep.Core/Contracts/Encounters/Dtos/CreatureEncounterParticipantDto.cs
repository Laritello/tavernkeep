using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tavernkeep.Core.Contracts.Creatures;

namespace Tavernkeep.Core.Contracts.Encounters.Dtos
{
	public class CreatureEncounterParticipantDto : EncounterParticipantDto
	{
		public required CreatureFullDto Creature { get; set; }
	}
}
