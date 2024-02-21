using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tavernkeep.Application.Actions.Chat.Commands.DeleteChat;
using Tavernkeep.Application.Actions.Chat.Commands.SendMessage;
using Tavernkeep.Application.Actions.Chat.Queries.GetMessages;
using Tavernkeep.Core.Contracts.Chat.Requests;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Messages;
using Tavernkeep.Server.Extensions;
using Tavernkeep.Server.Middleware;

namespace Tavernkeep.Server.Controllers
{
    /// <summary>
    /// The <see cref="ChatController"/> class handles chat operations within the application.
    /// </summary>
    /// <param name="mediator">The mediator instance.</param>
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController(IMediator mediator) : ControllerBase
    {
        /// <summary>
        /// Send a message to the chat.
        /// </summary>
        /// <param name="request">Specification for the message.</param>
        /// <returns>Sent message.</returns>
        [Authorize]
        [HttpPost("message")]
        public async Task<Message> SendMessageAsync([FromBody] SendMessageRequest request)
        {
            return await mediator.Send(new SendMessageCommand(HttpContext.GetUserId(), request.Content, request.RecipientId));
        }

        /// <summary>
        /// Get chunk of messages from the chat.
        /// </summary>
        /// <param name="skip">How many messages must be skipped from the end.</param>
        /// <param name="take">How many message must be taken.</param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public async Task<IEnumerable<Message>> GetMessagesAsync([FromQuery] int skip, [FromQuery] int take)
        {
            return await mediator.Send(new GetMessagesQuery(HttpContext.GetUserId(), skip, take));
        }

        /// <summary>
        /// Delete all messages from the chat.
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [RequiresRole(UserRole.Moderator)]
        [HttpDelete]
        public async Task DeleteChatAsync()
        {
            await mediator.Send(new DeleteChatCommand());
        }
    }
}
