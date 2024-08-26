using MediatR;
using Tavernkeep.Core.Entities.Pathfinder.Builds;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;

namespace Tavernkeep.Application.UseCases.Builds.Queries.GetCharacterBuild
{
	public class GetCharacterBuildQueryHandler(ICharacterRepository characterRepository) : IRequestHandler<GetCharacterBuildQuery, Build>
	{
		public async Task<Build> Handle(GetCharacterBuildQuery request, CancellationToken cancellationToken)
		{
			var character = await characterRepository.GetFullCharacterAsync(request.CharacterId, cancellationToken)
				?? throw new BusinessLogicException("Character with specified ID doesn't exist.");

			return character.Build;
		}
	}
}
