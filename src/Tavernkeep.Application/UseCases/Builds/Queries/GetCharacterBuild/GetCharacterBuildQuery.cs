using MediatR;
using Tavernkeep.Core.Entities.Pathfinder.Builds;

namespace Tavernkeep.Application.UseCases.Builds.Queries.GetCharacterBuild
{
	public class GetCharacterBuildQuery(Guid characterId) : IRequest<BuildDetails>
	{
		public Guid CharacterId { get; set; } = characterId;
	}
}
