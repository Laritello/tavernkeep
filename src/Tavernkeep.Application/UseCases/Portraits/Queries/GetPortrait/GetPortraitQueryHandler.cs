using MediatR;
using Tavernkeep.Core.Entities.Pathfinder;
using Tavernkeep.Core.Services;

namespace Tavernkeep.Application.UseCases.Portraits.Queries.GetPortrait
{
	public class GetPortraitQueryHandler(IPortaitService portaitService) : IRequestHandler<GetPortraitQuery, Portrait>
	{
		public async Task<Portrait> Handle(GetPortraitQuery request, CancellationToken cancellationToken)
		{
			return await portaitService.GetPortraitAsync(request.CharacterId, cancellationToken);
		}
	}
}
