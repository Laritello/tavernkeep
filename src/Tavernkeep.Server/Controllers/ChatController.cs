using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tavernkeep.Application.UseCases.Chat.Commands.DeleteChat;
using Tavernkeep.Application.UseCases.Chat.Commands.SendMessage;
using Tavernkeep.Application.UseCases.Chat.Queries.GetMessages;
using Tavernkeep.Application.UseCases.Chat.Commands.DeleteMessage;
using Tavernkeep.Core.Contracts.Chat.Dtos;
using Tavernkeep.Core.Contracts.Chat.Requests;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Server.Extensions;
using Tavernkeep.Server.Middleware;

namespace Tavernkeep.Server.Controllers
{
    /// <summary>
    /// The <see cref="ChatController"/> class handles chat operations within the application.
    /// </summary>
    /// <param name="mediator">The <see cref="IMediator"/> instance.</param>
    /// <param name="mapper">The <see cref="IMapper"/> instance.</param>
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController(IMediator mediator, IMapper mapper) : ControllerBase
    {
        /// <summary>
        /// Send a message to the chat.
        /// </summary>
        /// <param name="request">Specification for the message.</param>
        /// <returns>Sent message.</returns>
        [Authorize]
        [HttpPost("message")]
        public async Task<MessageDto> SendMessageAsync([FromBody] SendMessageRequest request)
        {
            var message = await mediator.Send(new SendMessageCommand(HttpContext.GetUserId(), request.Content, request.RecipientId));
            return mapper.Map<MessageDto>(message);
        }

        /// <summary>
        /// Get chunk of messages from the chat.
        /// </summary>
        /// <param name="skip">How many messages must be skipped from the end.</param>
        /// <param name="take">How many message must be taken.</param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public async Task<IEnumerable<MessageDto>> GetMessagesAsync([FromQuery] int skip = 0, [FromQuery] int take = 20)
        {
            var messages = await mediator.Send(new GetMessagesQuery(HttpContext.GetUserId(), skip, take));
            return mapper.Map<IEnumerable<MessageDto>>(messages);
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

        /// <summary>
        /// Delete specific message from the chat.
        /// </summary>
        /// <param name="messageId">The ID of the message for deletion.</param>
        /// <returns></returns>
        [Authorize]
        [RequiresRole(UserRole.Moderator)]
        [HttpDelete("{messageId}")]
        public async Task DeleteChatAsync([FromRoute] Guid messageId)
        {
            await mediator.Send(new DeleteMessageCommand(messageId));
        }
    }
}
