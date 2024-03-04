using MediatR;
using Microsoft.AspNetCore.SignalR;
using Tavernkeep.Infrastructure.Notifications.Hubs;

namespace Tavernkeep.Application.UseCases.Notifications.Queries.NotifySkillEdited
{
    public class NotifySkillEditedQueryHandler(IHubContext<CharacterHub, ICharacterHub> context) : IRequestHandler<NotifySkillEditedQuery>
    {
        public async Task Handle(NotifySkillEditedQuery request, CancellationToken cancellationToken)
        {
            await context.Clients.All.OnSkillEdited(request.Notification);
        }
    }
}
