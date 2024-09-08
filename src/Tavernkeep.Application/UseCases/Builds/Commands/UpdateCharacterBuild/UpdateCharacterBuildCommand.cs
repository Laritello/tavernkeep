using MediatR;
using Tavernkeep.Core.Entities.Pathfinder;
using Tavernkeep.Core.Entities.Pathfinder.Builds;

namespace Tavernkeep.Application.UseCases.Builds.Commands.UpdateCharacterBuild
{
	public record UpdateCharacterBuildCommand(Guid characterId, BuildDetails build) : IRequest<Character>
	{
		public Guid CharacterId { get; set; } = characterId;
		public BuildDetails Build { get; set; } = build;
	}
}
