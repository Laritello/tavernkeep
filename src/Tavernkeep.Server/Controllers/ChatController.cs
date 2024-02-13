using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tavernkeep.Application.Actions.Chat.Commands.SendMessage;
using Tavernkeep.Application.Actions.Chat.Queries.GetMessages;
using Tavernkeep.Core.Contracts.Authentication;
using Tavernkeep.Core.Contracts.Chat;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Exceptions;

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
        [HttpPost]
        public async Task<Message> SendMessageAsync([FromBody] SendMessageRequest request)
        {
            var senderId = HttpContext.User.FindFirst(JwtCustomClaimNames.UserId) 
                ?? throw new BusinessLogicException("You must be authorized to send messages.");

            var message = await mediator.Send(new SendMessageCommand(Guid.Parse(senderId.Value), request.Type, request.Content));
            return message;
        }

        /// <summary>
        /// Get chunk of messages from the chat.
        /// </summary>
        /// <param name="skip">How many messages must be skipped from the end.</param>
        /// <param name="take">How many message must be taken.</param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public async Task<List<Message>> GetMessagesAsync([FromQuery] int skip, [FromQuery] int take)
        {
            var messages = await mediator.Send(new GetMessagesQuery(skip, take));
            return messages;
        }
    }
}
