using Tavernkeep.Domain.Entities.Encounters;

namespace Tavernkeep.Domain.Specifications.Encounters
{
	public class EncounterFullSpecification : Specification<Encounter>
	{
		public EncounterFullSpecification(Guid id) : base(x => x.Id == id)
		{
			AddInclude(x => x.Participants);
		}
	}
}
