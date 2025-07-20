using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tavernkeep.Application.UseCases.Conditions.Queries.GetCondition;
using Tavernkeep.Application.UseCases.Conditions.Queries.GetConditions;
using Tavernkeep.Domain.Contracts.Conditions.Dtos;

namespace Tavernkeep.Server.Controllers
{
	/// <summary>
	/// The <see cref="ConditionsController"/> class handles character operations within the application.
	/// </summary>
	/// <param name="mediator">The <see cref="IMediator"/> instance.</param>
	/// <param name="mapper">The <see cref="IMapper"/> instance.</param>
	[ApiController]
	[Route("/api/[controller]")]
	public class ConditionsController(IMediator mediator, IMapper mapper) : ControllerBase
	{
		/// <summary>
		/// Get all conditions.
		/// </summary>
		/// <returns>List containing all conditions.</returns>
		[Authorize]
		[HttpGet]
		public async Task<List<ConditionDto>> GetAllConditionsAsync()
		{
			var conditions = await mediator.Send(new GetConditionsQuery());
			return mapper.Map<List<ConditionDto>>(conditions);
		}

		/// <summary>
		/// Get condition by name.
		/// </summary>
		/// <param name="name">The name of specified condition.</param>
		/// <returns>Specified condition.</returns>
		[Authorize]
		[HttpGet("{name}")]
		public async Task<ConditionDto> GetConditionAsync([FromRoute] string name)
		{
			var condition = await mediator.Send(new GetConditionQuery(name));
			return mapper.Map<ConditionDto>(condition);
		}
	}
}
