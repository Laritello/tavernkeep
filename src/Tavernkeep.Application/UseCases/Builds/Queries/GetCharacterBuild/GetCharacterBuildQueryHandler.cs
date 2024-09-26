using MediatR;
using Tavernkeep.Application.Interfaces;
using Tavernkeep.Core.Entities.Pathfinder.Builds;
using Tavernkeep.Core.Exceptions;

namespace Tavernkeep.Application.UseCases.Builds.Queries.GetCharacterBuild
{
	public class GetCharacterBuildQueryHandler(ICharacterService characterService) : IRequestHandler<GetCharacterBuildQuery, BuildDetails>
	{
		public async Task<BuildDetails> Handle(GetCharacterBuildQuery request, CancellationToken cancellationToken)
		{
			var build = await characterService.GetBuild(request.CharacterId, cancellationToken)
				?? throw new BusinessLogicException("Character with specified ID doesn't exist.");

			return build;
		}
	}
}
