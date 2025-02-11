using Tavernkeep.Core.Entities.Encounters;

namespace Tavernkeep.Core.Specifications.Encounters
{
	public class EncounterFullSpecification : Specification<Encounter>
	{
		public EncounterFullSpecification(Guid id) : base(x => x.Id == id)
		{
			AddInclude(x => x.Participants);
		}
	}
}
