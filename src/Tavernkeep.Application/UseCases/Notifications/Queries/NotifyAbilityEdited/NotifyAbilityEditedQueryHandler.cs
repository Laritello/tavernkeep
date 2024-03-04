using MediatR;
using Microsoft.AspNetCore.SignalR;
using Tavernkeep.Infrastructure.Notifications.Hubs;

namespace Tavernkeep.Application.UseCases.Notifications.Queries.NotifyAbilityEdited
{
    public class NotifyAbilityEditedQueryHandler(IHubContext<CharacterHub, ICharacterHub> context) : IRequestHandler<NotifyAbilityEditedQuery>
    {
        public async Task Handle(NotifyAbilityEditedQuery request, CancellationToken cancellationToken)
        {
            await context.Clients.All.OnAbilityEdited(request.Notification);
        }
    }
}
