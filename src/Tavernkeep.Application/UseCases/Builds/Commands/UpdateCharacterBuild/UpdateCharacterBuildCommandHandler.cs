using MediatR;
using Tavernkeep.Application.Interfaces;
using Tavernkeep.Core.Entities.Pathfinder;

namespace Tavernkeep.Application.UseCases.Builds.Commands.UpdateCharacterBuild
{
	public class UpdateCharacterBuildCommandHandler(ICharacterService characterService) : IRequestHandler<UpdateCharacterBuildCommand, Character>
	{
		public async Task<Character> Handle(UpdateCharacterBuildCommand request, CancellationToken cancellationToken)
		{
			var character = await characterService.ApplyBuild(request.CharacterId, request.Build, cancellationToken);
			return character;
		}
	}
}
