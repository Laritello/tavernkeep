using Microsoft.AspNetCore.SignalR;
using Tavernkeep.Infrastructure.Notifications.Notifications;

namespace Tavernkeep.Infrastructure.Notifications.Hubs
{
    public interface ICharacterHub
    {
        Task OnAbilityEdited(AbilityEditedNotification notification);
        Task OnSkillEdited(SkillEditedNotification notification);
    }

    public class CharacterHub : Hub<ICharacterHub>
    {
        public async Task SendAbilityEditedNotification(AbilityEditedNotification notification) 
            => await Clients.All.OnAbilityEdited(notification);

        public async Task SendSkillEditedNotification(SkillEditedNotification notification)
            => await Clients.All.OnSkillEdited(notification);
    }
}
