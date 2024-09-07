using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tavernkeep.Application.UseCases.Glossary.Query.GetAncestries;
using Tavernkeep.Application.UseCases.Glossary.Query.GetBackgrounds;
using Tavernkeep.Application.UseCases.Glossary.Query.GetClasses;
using Tavernkeep.Core.Contracts.Glossary.Dtos;
using Tavernkeep.Core.Entities.Templates;

namespace Tavernkeep.Server.Controllers
{
	/// <summary>
	///  The <see cref="GlossaryController"/> class that returns glossary data.
	/// </summary>
	/// <param name="mediator">The <see cref="IMediator"/> instance.</param>
	[ApiController]
	[Route("api/[controller]")]
	public class GlossaryController(IMediator mediator, IMapper mapper) : ControllerBase
	{
		/// <summary>
		/// Get all ancestries.
		/// </summary>
		/// <returns><see cref="List{AncestryTemplateShortDto}"/> of all available ancestries.</returns>
		[HttpGet("ancestries")]
		public async Task<List<AncestryTemplateShortDto>> GetAncestriesAsync()
		{
			var ancestries = await mediator.Send(new GetAncestriesQuery());
			return mapper.Map<List<AncestryTemplateShortDto>>(ancestries);
		}

		/// <summary>
		/// Get all backgrounds.
		/// </summary>
		/// <returns><see cref="List{BackgroundTemplateShortDto}"/> of all available backgrounds.</returns>
		[HttpGet("backgrounds")]
		public async Task<List<BackgroundTemplateShortDto>> GetBackgroundsAsync()
		{
			var backgrounds = await mediator.Send(new GetBackgroundsQuery());
			return mapper.Map<List<BackgroundTemplateShortDto>>(backgrounds);
		}

		/// <summary>
		/// Get all classes.
		/// </summary>
		/// <returns><see cref="List{ClassTemplateShortDto}"/> of all available classes.</returns>
		[HttpGet("classes")]
		public async Task<List<ClassTemplateShortDto>> GetClassesAsync()
		{
			var classes = await mediator.Send(new GetClassesQuery());
			return mapper.Map<List<ClassTemplateShortDto>>(classes);
		}
	}
}
