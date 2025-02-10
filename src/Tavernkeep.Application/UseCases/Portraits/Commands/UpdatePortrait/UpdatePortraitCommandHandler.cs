using MediatR;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Services;

namespace Tavernkeep.Application.UseCases.Portraits.Commands.UpdatePortrait
{
	public class UpdatePortraitCommandHandler(IPortaitService portaitService) : IRequestHandler<UpdatePortraitCommand>
	{
		private static readonly string[] allowedMimeTypes = ["image/jpeg", "image/png", "image/gif"];

		public async Task Handle(UpdatePortraitCommand request, CancellationToken cancellationToken)
		{
			if (!allowedMimeTypes.Contains(request.File.ContentType))
				throw new BusinessLogicException("Invalid file type.");

			// Read file data into a byte array
			using var stream = new MemoryStream();
			await request.File.CopyToAsync(stream, cancellationToken);
			var bytes = stream.ToArray();

			await portaitService.UpdatePortraitAsync(request.CharacterId, bytes, request.File.ContentType, cancellationToken);
		}
	}
}
