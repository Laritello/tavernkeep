using MediatR;
using Tavernkeep.Core.Entities.Pathfinder;
using Tavernkeep.Core.Services;

namespace Tavernkeep.Application.UseCases.Portraits.Queries.GetPortrait
{
	public class GetPortraitQueryHandler(IPortaitService portaitService) : IRequestHandler<GetPortraitQuery, Portrait>
	{
		private static readonly Portrait defaultPortrait = new()
		{
			Bytes = File.ReadAllBytes(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Images", "default_portrait.jpg")),
			MimeType = "image/jpeg"
		};

		public async Task<Portrait> Handle(GetPortraitQuery request, CancellationToken cancellationToken)
		{
			return await portaitService.GetPortraitAsync(request.CharacterId, cancellationToken) ?? defaultPortrait;
		}
	}
}
